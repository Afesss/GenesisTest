using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Scroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform[] _contentObjects;
        [Header("Settings")]
        [SerializeField] private float _autoScrollOfsset;

        private const int MaxMagnitude = 824;
        //private const int MaxMagnitude = 30;
        private const int Offset = 500;
        private const int TimeToScrollFalse = 3;
        private const int AutoScrollSpeed = 10;
        
        private bool _isScrolling;
        private bool _isTouch;
        private Transform _nearestObject;
        private Vector3 _newContentPosition;
        private void Update()
        {
            if (_isScrolling)
            {
                if (!_isTouch && _content.position != _newContentPosition)
                {
                    _content.position = Vector3.Lerp(_content.position, _newContentPosition, 
                        AutoScrollSpeed * Time.deltaTime);
                }
                return;
            }
                
            _content.localPosition += Vector3.right * (_autoScrollOfsset * Time.deltaTime);
        }

        public void OnViewScroll()
        {
            foreach (Transform contentObj in _contentObjects)
            {
                Vector3 directionVector = transform.position - contentObj.transform.position;
                if (directionVector.magnitude / _canvas.transform.localScale.x> MaxMagnitude)
                {
                    int direction = directionVector.x > 0 ? 1 : -1;
                    int childIndex = directionVector.x > 0 ? _content.childCount - 1 : 0;
                    Transform child = _content.GetChild(childIndex);
                    int offset = Offset * direction;
                    contentObj.localPosition = child.localPosition + new Vector3(offset, 0, 0);
                    contentObj.SetSiblingIndex(childIndex);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isScrolling = true;
            _isTouch = true;
            StopAllCoroutines();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isTouch = false;
            
            CalculateNewContentPosition();
            
            StartCoroutine(WaitToTouchFalse());
        }

        private IEnumerator WaitToTouchFalse()
        {
            yield return new WaitForSeconds(TimeToScrollFalse);
            _isScrolling = false;
        }

        private void CalculateNewContentPosition()
        {
            float distance = float.MaxValue;
            foreach (Transform contentObj in _contentObjects)
            {
                float magnitude = (transform.position - contentObj.transform.position).magnitude;
                if (magnitude < distance)
                {
                    distance = magnitude;
                    _nearestObject = contentObj;
                }
            }

            Vector3 directionVector = transform.position - _nearestObject.position;
            _newContentPosition = _content.position + new Vector3(directionVector.x, 0, 0);
        }
    }
}

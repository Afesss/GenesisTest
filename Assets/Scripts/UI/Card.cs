using System;
using DG.Tweening;
using Particle;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardParticle _particle;
        [SerializeField] private Image _image;
        
        public event Action<Card> Disabled;
        public int Index { get; private set; }
        
        private Button _button;

        private Tween _tween;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(DisableWithParticle);
        }

        public void Activate(int index)
        {
            Index = index;
        }

        public void SmoothLocalMove(Vector3 localPos)
        {
            if(transform.localPosition != localPos)
                _tween = transform.DOLocalMove(localPos, 1);
        }

        public void RestartCard(Vector3 localPos)
        {
            _tween?.Kill();
            transform.localPosition = localPos;
            gameObject.SetActive(true);
        }

        public Vector3 GetLocalPosition()
        {
            return transform.localPosition;
        }

        private void DisableWithParticle()
        {
            GameObject obj = Instantiate(_particle.gameObject, transform.position, 
                Quaternion.Euler(0, 180, 0));
            obj.GetComponent<CardParticle>().Activate(_image.sprite);
            gameObject.SetActive(false);
            Disabled?.Invoke(this);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }
    }
}

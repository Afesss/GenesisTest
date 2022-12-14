using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProfileWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text[] _allText;
        [SerializeField] private Image _background;
        [SerializeField] private Image _closeBackground;
        [SerializeField] private Image _windowBackgorund;

        private const float Duration = 0.5f;
    
        private void Start()
        {
            _closeButton.onClick.AddListener(CloseWindow);
        }

        public void ShowWindow()
        {
            _window.SetActive(true);
            foreach (TMP_Text text in _allText)
            {
                text.DOFade(1, Duration);
            }

            _background.DOFade(0.5f, Duration);
            _closeBackground.DOFade(1, Duration);
            _windowBackgorund.DOFade(0.8f, Duration);
        }

        private void CloseWindow()
        {
            foreach (TMP_Text text in _allText)
            {
                text.DOFade(0, Duration);
            }

            _background.DOFade(0, Duration);
            _windowBackgorund.DOFade(0, Duration);
            _closeBackground.DOFade(0, Duration)
                .OnComplete(() => { _window.SetActive(false); });
        }
    }
}

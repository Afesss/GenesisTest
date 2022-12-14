using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainUIWindow : MonoBehaviour
    {
        [Header("Button")] 
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        
        [Header("Reference")] 
        [SerializeField] private SettingsWindow _settingsWindow;

        private void Start()
        {
            _settingsButton.onClick.AddListener(_settingsWindow.ShowWindow);
            _exitButton.onClick.AddListener(Application.Quit);
        }
    }
}

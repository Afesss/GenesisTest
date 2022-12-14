using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ProfileWindow _profileWindow;
        [SerializeField] private GameObject _window;

        [Header("Buttons")]
        [SerializeField] private Button _showProfileButton;
        [SerializeField] private Button _closeButton;
        
        [Header("Sliders")]
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private AudioService _music;
        private AudioService _sound;
        private SaveLoadService _saveLoadService;
        
        [Inject]
        private void Construct(ServiceRegistrator serviceRegistrator)
        {
            _music = serviceRegistrator.Music;
            _sound = serviceRegistrator.Sound;
            _saveLoadService = serviceRegistrator.SaveLoadService;
        }
        
        private void Start()
        {
            _showProfileButton.onClick.AddListener(_profileWindow.ShowWindow);
            _closeButton.onClick.AddListener(CloseWindow);
            _musicSlider.onValueChanged.AddListener(MusicVolumeChanged);
            _musicSlider.value = _saveLoadService.Data.MusicVolume;
            _soundSlider.onValueChanged.AddListener(SoundVolumeChanged);
            _soundSlider.value = _saveLoadService.Data.SondVolume;
        }

        public void ShowWindow()
        {
            _window.SetActive(true);
        }

        private void MusicVolumeChanged(float volume)
        {
            _music.SetVolume(volume);
            _saveLoadService.SaveMusicVolume((int)volume);
        }

        private void SoundVolumeChanged(float volume)
        {
            _sound.SetVolume(volume);
            _saveLoadService.SaveSoundVolume((int)volume);
        }

        private void CloseWindow()
        {
            _window.SetActive(false);
        }
    }
}

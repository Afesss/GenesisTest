using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure
{
    public class ServiceRegistrator : MonoBehaviour
    {
        [SerializeField] private AudioService _musicService;
        [SerializeField] private AudioService _soundService;
        [SerializeField] private Curtain _curtain;

        public Curtain Curtain => _curtain;

        public AudioService Music => _musicService;
        public AudioService Sound => _soundService;
        
        public SceneLoader SceneLoader { get; private set; }
        public AssetBundleService AssetBundleService { get; private set; }
        public SaveLoadService SaveLoadService { get; private set; }
        public void Construct()
        {
            SceneLoader = new SceneLoader();
            AssetBundleService = new AssetBundleService();
            _curtain.Construct(AssetBundleService);
            SaveLoadService = new SaveLoadService();
            Music.Construct(SaveLoadService.Data.MusicVolume);
            Sound.Construct(SaveLoadService.Data.SondVolume);
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Services
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private Image _loadingImage;
        [SerializeField] private TMP_Text _loadingText;
        private AssetBundleService _assetBundleService;

        public void Construct(AssetBundleService assetBundleService)
        {
            _assetBundleService = assetBundleService;
        }

        public void ShowCurtain()
        {
            gameObject.SetActive(true);
        }

        public void HideCurtain()
        {
            gameObject.SetActive(false);
        }

        public IEnumerator ShowLoadingPercent()
        {
            while (!_assetBundleService.IsLoaded)
            {
                _loadingImage.fillAmount = _assetBundleService.LoadProgress;
                _loadingText.text = $"{_assetBundleService.LoadProgress * 100:0}%";
                yield return null;
            } 
            _loadingImage.fillAmount = _assetBundleService.LoadProgress;
            _loadingText.text = $"{_assetBundleService.LoadProgress * 100:0}%";
        }
    }
}

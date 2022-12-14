using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrap : MonoInstaller
    {
        [SerializeField] private ServiceRegistrator _serviceRegistrator;

        public override void InstallBindings()
        {
            Container.BindInstance(_serviceRegistrator).AsSingle();
        }

        private void Awake()
        {
            _serviceRegistrator.Construct();
            _serviceRegistrator.Curtain.ShowCurtain();
            _serviceRegistrator.SceneLoader.Load(SceneLoader.FirstScene, OnLoaded);
        }

        private void OnLoaded()
        {
            StartCoroutine(_serviceRegistrator.AssetBundleService.DownloadBundle(BundlesOnLoaded));
            StartCoroutine(_serviceRegistrator.Curtain.ShowLoadingPercent());
        }

        private void BundlesOnLoaded()
        {
            _serviceRegistrator.Music.Play(_serviceRegistrator.AssetBundleService.BackgroundMusic);
            StartCoroutine(LoadSecondScene());
        }

        private IEnumerator LoadSecondScene()
        {
            yield return null;
            _serviceRegistrator.SceneLoader.Load(SceneLoader.SecondScene, _serviceRegistrator.Curtain.HideCurtain);
        }
        
    }
}
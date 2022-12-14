using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Infrastructure.Services
{
    public class AssetBundleService
    {
        public float LoadProgress { get; private set; }
        public bool IsLoaded { get; private set; }
        
        public AudioClip BackgroundMusic { get; private set; }
        
        public Sprite Jack { get; private set; }
        public Sprite Lady { get; private set; }
        public Sprite King { get; private set; }
        
        private const string URL = "https://drive.google.com/uc?export=download&id=1vPW_xSDwTEHsVg8MHqzbgioMTqcP8tX-";
        
        public IEnumerator DownloadBundle(Action OnLoaded = null)
        {
            IsLoaded = false;
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(URL);

            www.SendWebRequest();
            while (!www.isDone)
            {
                LoadProgress = www.downloadProgress;
                yield return null;
            }

            LoadProgress = 1;
            var bundle = DownloadHandlerAssetBundle.GetContent(www);
            BackgroundMusic = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as AudioClip;
            
            Texture2D texture = bundle.LoadAsset(bundle.GetAllAssetNames()[1]) as Texture2D;
            Jack = CreateSprite(texture);
            texture = bundle.LoadAsset(bundle.GetAllAssetNames()[2]) as Texture2D;
            King = CreateSprite(texture);
            texture = bundle.LoadAsset(bundle.GetAllAssetNames()[3]) as Texture2D;
            Lady = CreateSprite(texture);
            OnLoaded?.Invoke();

            IsLoaded = true;
        }

        private Sprite CreateSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, 400, 400), new Vector2(0.5f, 0.5f));
        }
    }
}

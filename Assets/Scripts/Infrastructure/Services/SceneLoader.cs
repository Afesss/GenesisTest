using System;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader
    {
        public const string FirstScene = "FirstScene";
        public const string SecondScene = "SecondScene";
        public void Load(string sceneName, Action loadedCallback = null)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                loadedCallback?.Invoke();
                return;
            }
            
            var operation = SceneManager.LoadSceneAsync(sceneName);

            operation.completed += asyncOperation => loadedCallback?.Invoke();
        }
    }
}

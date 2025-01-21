using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;

namespace FSF.Collection{
    #nullable enable
        public static class GlobalSceneLoader
        {
            public static bool isLoading, isUnLoading;

        #region load
            public static async UniTask<bool> LoadScene(string sceneName, 
            Action<object> awaitOperation, 
            Action? onComplete,
            LoadSceneMode loadSceneMode = LoadSceneMode.Single){
                if(isLoading){return false;}
                isLoading = true;
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
                operation.allowSceneActivation = false;
                await UniTask.RunOnThreadPool(awaitOperation, operation);
                operation.allowSceneActivation = true;
                isLoading = false;
                onComplete?.Invoke();
                return true;
            }
        #endregion
        #region unload
            public static async UniTask<bool> UnLoadScene(string sceneName, 
            Action<object> awaitOperation,
            Action? onComplete){
                AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
                if(isUnLoading){return false;}
                isUnLoading = true;
                operation.allowSceneActivation = false;
                await UniTask.RunOnThreadPool(awaitOperation, operation);
                operation.allowSceneActivation = true;
                isUnLoading = false;
                onComplete?.Invoke();
                return true;
            }
            #endregion
    }
    #nullable restore
}

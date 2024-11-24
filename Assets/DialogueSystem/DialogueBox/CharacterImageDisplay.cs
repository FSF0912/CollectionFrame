using UnityEngine.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace FSF.CollectionFrame.GalFrame
{
    public class CharacterImageDisplay : MonoBehaviour
    {
        public Image First, Second;
        public float fadeDuration = 0.4f;
        bool firstControl = true;
        private CancellationTokenSource cts;

        private void Start() {
            First.color = Color.white;
            Second.color = Color.white.WithAlpha(0);
            cts = new();
        }

        private void OnDestroy() {
            cts.Cancel();
            cts.Dispose();
        }

        public void SwitchImage(Sprite target){
            if(firstControl){Second.sprite = target;}
            else{First.sprite = target;}
            cts.Cancel();
            SwitchCharacters(cts.Token).Forget();
        }

        public async UniTask SwitchCharacters(CancellationToken cancellationToken)
        {
            float elapsedTime = 0.0f;
            try{
                while (elapsedTime < fadeDuration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                    if(firstControl){
                        First.color = Color.white.WithAlpha(1 - alpha);
                        Second.color = Color.white.WithAlpha(alpha);
                    }
                    else{
                        Second.color = Color.white.WithAlpha(1 - alpha);
                        First.color = Color.white.WithAlpha(alpha);
                    }
                    await UniTask.Yield(cancellationToken);
                }

                if(firstControl){
                    First.color = Color.white.WithAlpha(0);
                    Second.color = Color.white.WithAlpha(1);
                    firstControl = false;
                }
                else{
                    First.color = Color.white.WithAlpha(1);
                    Second.color = Color.white.WithAlpha(0);
                    firstControl = true;
                }
            }
            catch(OperationCanceledException){}
        }
    }
}   
using FSF.Collection.Utilities;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FSF.Collection;

namespace FSF.DialogueSystem{
    public class ImageSwitcher : MonoBehaviour{
        public int characterDefindID = 0;
        public Image targetImage1, targetImage2;
        public CustomizeRectInfos customizeRectInfos;
        /// <summary>
        /// 为true时，image1在上层(层级中最下面)，i
        /// mage2在下层(层级中最上面)，反之...
        /// </summary>
        bool switcher;
        Tween image1_Tween, image2_Tween;

        private void Awake() {
            targetImage1.transform.SetAsLastSibling();
            switcher = true;
            targetImage1.color = Color.white.WithAlpha(1f);
            targetImage2.color = Color.white.WithAlpha(0);
        }

        public void OutputImage(Sprite target){
            image1_Tween?.Kill();
            image2_Tween?.Kill();
            float switchTime = Dialogue_Configs.characterSwitchDuration;
            if(switcher){//切换为image2,image1渐隐,image2渐显。？
                targetImage2.sprite = target;
                targetImage2.transform.SetAsLastSibling();
                //DOFade。。。
                image1_Tween = targetImage1.DOFade(0, switchTime);
                image2_Tween = targetImage2.DOFade(1, switchTime);
                //
                switcher = false;
            }
            else{//切换为image1,image2渐隐,image1渐显？。
                targetImage1.sprite = target;
                targetImage1.transform.SetAsLastSibling();
                //
                image1_Tween = targetImage1.DOFade(1, switchTime);
                image2_Tween = targetImage2.DOFade(0, switchTime);
                //
                switcher = true;
            }
        }

        public void Interrupt(){
            image1_Tween?.Kill();
            image2_Tween?.Kill();
            targetImage1.color = Color.white.WithAlpha(switcher ? 255 : 0);
            targetImage2.color = Color.white.WithAlpha(switcher ? 0 : 255);
            
        }
    }
}

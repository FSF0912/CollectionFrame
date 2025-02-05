using FSF.Collection.Utilities;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FSF.Collection;

namespace FSF.DialogueSystem
{
    public class Character : MonoBehaviour
    {
        public int characterDefindID = 0;
        public Image targetImage1, targetImage2;
        //public CustomizeRectInfos customizeRectInfos;
        /// <summary>
        /// 为true时，image1在上层(层级中最下面)，i
        /// mage2在下层(层级中最上面)，反之...
        /// </summary>
        bool switcher;
        RectTransform selfRTransform;
        Vector2 tempPosition;
        Tween image1_Tween, image2_Tween;
        Tween movementTween, distanceTween;

        private void Awake()
        {
            targetImage1.transform.SetAsLastSibling();
            switcher = true;
            targetImage1.color = Color.white.WithAlpha(1f);
            targetImage2.color = Color.white.WithAlpha(0);
            selfRTransform = this.transform as RectTransform;
        }

        public void OutputImage(Sprite target)
        {
            image1_Tween?.Kill();
            image2_Tween?.Kill();
            float switchTime = Dialogue_Configs.characterSwitchDuration;
            if (switcher)
            {//切换为image2,image1渐隐,image2渐显。？
                targetImage2.sprite = target;
                targetImage2.transform.SetAsLastSibling();
                //DOFade。。。
                if(targetImage1.sprite)
                {
                    image1_Tween = targetImage1.DOFade(0, switchTime);
                }
                if(targetImage2.sprite)
                {
                    image2_Tween = targetImage2.DOFade(1, switchTime);
                }
                //
                switcher = false;
            }
            else
            {//切换为image1,image2渐隐,image1渐显？。
                targetImage1.sprite = target;
                targetImage1.transform.SetAsLastSibling();
                //
                if(targetImage1.sprite)
                {
                    image1_Tween = targetImage1.DOFade(1, switchTime);
                }
                if(targetImage2.sprite)
                {
                    image2_Tween = targetImage2.DOFade(0, switchTime);
                }
                //
                switcher = true;
            }
        }

        public void Interrupt()
        {
            image1_Tween?.Kill();
            image2_Tween?.Kill();
            movementTween?.Kill();
            distanceTween?.Kill();
            targetImage1.color = Color.white.WithAlpha(switcher ? 255 : 0);
            targetImage2.color = Color.white.WithAlpha(switcher ? 0 : 255);
            selfRTransform.anchoredPosition = tempPosition;
        }

        public void Animate(CharacterOption option)
        {
            movementTween?.Kill();
            distanceTween?.Kill();
            switch (option.motionMode)
            {
                case MotionMode.None:
                    break;

                case MotionMode.LeftEnter:
                    selfRTransform.anchoredPosition = new(-1920, 0);
                    MoveToZero();
                    break;

                case MotionMode.RightEnter:
                    selfRTransform.anchoredPosition = new(1920, 0);
                    MoveToZero();
                    break;

                case MotionMode.BottomEnter:
                    selfRTransform.anchoredPosition = new(0, -1080);
                    MoveToZero();
                    break;

                case MotionMode.LeftEscape:
                    MoveToAppointedPosition(new(-1920, 0));
                    break;

                case MotionMode.RightEscape:
                    MoveToAppointedPosition(new(1920, 0));
                    break;

                case MotionMode.Shake:
                    movementTween = selfRTransform.DOShakeAnchorPos(option.action_Duration);
                    movementTween.SetEase(option.action_Ease);
                    break;

                case MotionMode.ToCenter:
                    MoveToZero();
                    break;

                case MotionMode.ToLeft:
                    MoveToAppointedPosition(new(-500, 0));
                    break;

                case MotionMode.ToRight:
                    MoveToAppointedPosition(new(500, 0));
                    break;

                case MotionMode.Custom:
                    if (option.useOrigin)
                    {
                        selfRTransform.anchoredPosition = option.origin;
                    }
                    movementTween = selfRTransform.DOAnchorPos(option.appointedPosition, option.action_Duration);
                    movementTween.SetEase(option.action_Ease);
                    break;

                default: break;
            }

            switch (option.distanceMode)
            { 
                case Distance.None:
                    break;

                case Distance.Normal:

                    break;

                case Distance.Far:

                    break;

                case Distance.Near:

                    break;
            }

            void MoveToZero()
            {
                MoveToAppointedPosition(Vector2.zero);
            }

            void MoveToAppointedPosition(Vector2 pos)
            {
                movementTween = selfRTransform.DOAnchorPos(pos, option.action_Duration);
                movementTween.SetEase(option.action_Ease);
                tempPosition = pos;
            }
        }
    }
}

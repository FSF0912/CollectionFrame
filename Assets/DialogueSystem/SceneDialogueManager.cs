using FSF.CollectionFrame;
using UnityEngine;
using UnityEngine.Events;

namespace FSF.GalFrame{
    public enum OperationMode{
        Dialogue,
        CGView
    };
    public enum EnvironmentMode{
        Keep = -1,
        Normal = 0,
        NightFall = 2,
        Night = 3
    };

    public enum backGroundViewMode{
        Normal = 0,
        ScaleMax = 1,
        ViewFromRight_to_Left = 2,
        ViewFromLeft_to_Right = 3,
        ViewFromTop_to_Bottom = 4,
        ViewFromButton_to_Top = 5
    };
    #nullable enable
    [System.Serializable]
        public struct SingleAction{
            public OperationMode operationMode;
    #region 文本显示&语音播放
            public string char_Name;
            public string char_Dialogue;
            public AudioClip? char_Voice;
            
    #endregion
    #region 场景显示&效果
            public Sprite? backGround;
            public bool Shaking;
            public backGroundViewMode viewMode;
            public EnvironmentMode environmentMode;
    #endregion
            public int[] addedCharacter;
            public int[] removedCharacter;


        
    #nullable restore
        public sealed class SceneDialogueManager : MonoSingleTon<SceneDialogueManager>{

        }
    }
}
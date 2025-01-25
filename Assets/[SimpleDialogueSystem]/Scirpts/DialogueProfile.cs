using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FSF.DialogueSystem{
    #region Structures
    [System.Serializable]
    public struct SingleAction{
        public string name;
        public string dialogue;
        public AudioClip audio;
        
        public Sprite backGround;
        public EnvironmentSettings environmentSettings;
        public ImageOption[] imageOptions;

        public SingleAction(string name, string dialogue, ImageOption[] options, 
        AudioClip audio, Sprite backGround, EnvironmentSettings settings){
            this.name = name;
            this.dialogue = dialogue;
            imageOptions = options;
            this.audio = audio;
            this.backGround = backGround;
            environmentSettings = settings;
        }

        
    }
    //
    [System.Serializable]
    public class ImageOption{
        public int characterDefindID;
        public Sprite characterImage;
        public MotionMode motionMode = MotionMode.Default;
        public float action_Duration = 0.6f;
        public Vector2 appointedPosition = Vector2.zero;
        
    }
    //
    #endregion

    #region Enums
    public enum EnvironmentSettings{
        Default,
        Day,
        Twilight,
        Night
    };

    public enum MotionMode{
        Default,
        LeftEnter,
        RightEnter,
        BottomEnter,
        LeftEscape,
        RightEscape,
        Shake,
        ToCenter,
        ToLeft,
        ToRight,
        ToAppointedPosition
    };

    public enum Distance{
        Default,
        Normal,
        Far,
        Near
    };
#endregion



    [CreateAssetMenu(fileName = "DialogueProfile", menuName = "FSF_Custom/DialogueSystem/DialogueProfile", order = 30000)]
    public class DialogueProfile : ScriptableObject{
        public SingleAction[] actions;
        public SingleAction this[int index]{
            get{
                return actions[index];
            }
        }
    }
    #region Editor
    #if UNITY_EDITOR

    #endif
    #endregion
}
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FSF.DialogueSystem
{
    #region Structures
    [System.Serializable]
    public struct SingleAction
    {
        public string name;
        public string dialogue;
        public AudioClip audio;
        
        public Sprite backGround;
        public EnvironmentSettings environmentSettings;
        public CharacterOption[] imageOptions;

        public SingleAction(string name, string dialogue, CharacterOption[] options, 
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
    public class CharacterOption
    {
        public int characterDefindID;
        public Sprite characterImage;
        public MotionMode motionMode = MotionMode.None;
        public Distance distanceMode = Distance.None;
        public float action_Duration = 0.6f;
        public Ease action_Ease = Ease.InOutSine;
        [Header("Custom Options")]
        public bool useOrigin = false;
        public Vector2 origin = Vector2.zero;
        public Vector2 appointedPosition = Vector2.zero;
        
    }
    //
    #endregion

    #region Enums
    public enum EnvironmentSettings
    {
        None,
        Day,
        Twilight,
        Night
    };

    public enum MotionMode
    {
        None,
        LeftEnter,
        RightEnter,
        BottomEnter,
        LeftEscape,
        RightEscape,
        Shake,
        ToCenter,
        ToLeft,
        ToRight,
        Custom
    };

    public enum Distance
    {
        None,
        Normal,
        Far,
        Near
    };
#endregion



    [CreateAssetMenu(fileName = "DialogueProfile", menuName = "FSF_Custom/DialogueSystem/DialogueProfile", order = 30000)]
    public class DialogueProfile : ScriptableObject
    {
        public SingleAction[] actions;
        public SingleAction this[int index]
        {
            get
            {
                return actions[index];
            }
        }
    }
    #region Editor
    #if UNITY_EDITOR

    #endif
    #endregion
}
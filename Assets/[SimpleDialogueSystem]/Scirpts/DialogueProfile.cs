using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace FSF.DialogueSystem{
    #region structures
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
    public struct ImageOption{
        public int characterDefindID;
        public Sprite characterImage;
        
    }
    #endregion
    public enum EnvironmentSettings{
        Default,
        Day,
        Twilight,
        Night
    };




    [CreateAssetMenu(fileName = "DialogueProfile", menuName = "FSF_Custom/DialogueSystem/DialogueProfile", order = 30000)]
    public class DialogueProfile : ScriptableObject{
        public SingleAction[] actions;
        public SingleAction this[int index]{
            get{
                return actions[index];
            }
        }
    }
}
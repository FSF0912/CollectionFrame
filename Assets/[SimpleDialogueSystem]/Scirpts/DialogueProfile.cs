using System;
using UnityEngine;

namespace FSF.DialogueSystem{
    [System.Serializable]
    public struct SingleAction{
        [HideInInspector] public string name;
        [HideInInspector]public string dialogue;
        [HideInInspector]public AudioClip audio;
        [HideInInspector]public Sprite image;

        public SingleAction(string name, string dialogue, Sprite image, AudioClip audio){
            this.name = name;
            this.dialogue = dialogue;
            this.image = image;
            this.audio = audio;
        }
    }

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
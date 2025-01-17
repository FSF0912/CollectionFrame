using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FSF.GalFrame
{
    public class TextShow : MonoBehaviour
    {
        public Text nameText, dialogueText;
        bool done = true;
        string currentName,currentDialogue;
        Tween typerTween;
        public void ShowText(string characterName, string dialogue){
            if(done){
                typerTween?.Kill();
                currentDialogue = new(dialogue);
                currentName = new(characterName);
                nameText.text = characterName;
                dialogueText.text = string.Empty;
                typerTween = dialogueText.DOText(dialogue, dialogue.Length * 0.09f);
            }
            else{
                typerTween?.Kill();
                dialogueText.text = currentDialogue;
                nameText.text = currentName;
                currentDialogue = dialogue;
                currentName = characterName;
            }
        }

    }
}

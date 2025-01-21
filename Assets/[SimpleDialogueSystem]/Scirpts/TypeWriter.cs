using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FSF.Collection;
using UnityEngine;
using UnityEngine.UI;

namespace FSF.DialogueSystem{
    public class TypeWriter : MonoSingleTon<TypeWriter>
    {
        public Text Name, Dialogue;
        Tween typer;
        string current_Dialogue;
        bool done = true;

        public void OutputText(string name, string dialogue){
            if(done){
                typer?.Kill();
                current_Dialogue = dialogue;
                Name.text = name;
                Dialogue.text = string.Empty;
                typer = Dialogue.DOText(dialogue, dialogue.Length * Dialogue_Configs.textTypeDuration).SetEase(Ease.Linear).OnComplete(()=>{
                    done = true;
                });
                done = false;
            }
            else{
                typer?.Kill();
                Dialogue.text = current_Dialogue;
                current_Dialogue = dialogue;
                done = true;
            }
        }

        public void OutputText(string name, string dialogue, Action callBack){
            if(done){
                current_Dialogue = dialogue;
                Name.text = name;
                Dialogue.text = string.Empty;
                typer = Dialogue.DOText(dialogue, dialogue.Length * Dialogue_Configs.textTypeDuration).OnComplete(()=>{
                    callBack();
                });
            }
            else{
                typer?.Kill();
                Dialogue.text = current_Dialogue;
                current_Dialogue = dialogue;
            }
        }
    }
}

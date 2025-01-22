using FSF.Collection;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace FSF.DialogueSystem{
    public class DialogueManager : MonoSingleTon<DialogueManager>
    {
        public DialogueProfile profile;
        public AudioSource audioSource;
        public KeyCode[] activedKeys = {KeyCode.Space, KeyCode.Return, KeyCode.F};
        [Space(7.00F)] public ImageSwitcher[] characterShowers;
        public ImageSwitcher backGround;
        int index = 0;
        [HideInInspector] public bool allowInput = true;

        bool inputResult{
            get{
                if(!allowInput){return false;}
                else{
                    if(Input.GetMouseButtonDown(0)){return true;}
                    else{
                        foreach(var item in activedKeys){
                            if(Input.GetKeyDown(item)){return true;}
                        }
                        return false;
                    }
                }
            }
        }

        private void Start() {
            ShowDialogue();
        }

        private void Update() {
            if(inputResult){
                ShowDialogue();
            }
        }

        public void ShowDialogue(){
            if(index >= profile.actions.Length){
                //return;
                index = 0;
            }
            SingleAction current = profile[index];
            if(TypeWriter.Instance.OutputText(current.name, current.dialogue)){
                foreach (var item in characterShowers)
                {
                    foreach(var item1 in current.imageOptions){
                        if(item1.characterDefindID == item.characterDefindID){
                            item.OutputImage(item1.characterImage);
                        }
                    }
                }

                if(current.backGround != null){
                    backGround.OutputImage(current.backGround);
                }
            }
            else{
                foreach (var item in characterShowers)
                {
                    item.Interrupt();
                }
                backGround.Interrupt();
            }

            
            if(current.audio != null){
                audioSource.Stop();
                audioSource.clip = current.audio;
                audioSource.Play();
            }
            index++;
        }
    }
}

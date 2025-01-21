using FSF.Collection;
using UnityEngine;

namespace FSF.DialogueSystem{
    public class DialogueManager : MonoSingleTon<DialogueManager>
    {
        public DialogueProfile profile;
        public AudioSource audioSource;
        public KeyCode[] activedKeys = {KeyCode.Space, KeyCode.Return, KeyCode.F};
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

        private void Update() {
            if(inputResult){
                ShowDialogue();
            }
        }

        public void ShowDialogue(){
            if(index >= profile.actions.Length - 1){
                index = 0;
            }
            SingleAction current = profile[index];
            TypeWriter.Instance.OutputText(current.name, current.dialogue);
            CharacterShower.Instance.OutputImage(current.image);
            if(current.audio != null){
                audioSource.Stop();
                audioSource.clip = current.audio;
                audioSource.Play();
            }
            index++;
        }
    }
}

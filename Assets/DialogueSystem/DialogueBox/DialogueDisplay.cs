using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using egl = UnityEditor.EditorGUILayout;
#endif

namespace FSF.CollectionFrame.GalFrame
{
    public enum TextMotionMode{
        None = 0,
        Large = 2,
        Small = 4,
        Shake = 8,
        LargeAndShake = Large * Shake,
        SmallAndSake = Small * Shake
    };

    [Serializable]
    public struct SingleDisplay{
        public string name;
        [TextArea(2, int.MaxValue)]public string displayString;
        public TextMotionMode textMotionMode;
        [Space(10)]
        public string characterDefinedName;
        public string characterImage;
        public Vector2 moveTo;

        public SingleDisplay(string name, string displayString, TextMotionMode textMotionMode,
        string characterDefinedName, string characterImage, Vector2 moveTo){
            this.name = name;
            this.displayString = displayString;
            this.textMotionMode = textMotionMode;
            this.characterDefinedName = characterDefinedName;
            this.characterImage = characterImage;
            this.moveTo = moveTo;
        }
    }

    public class DialogueDisplay : MonoBehaviour
    {
        public TextMeshProUGUI Text_Name, Text_Main;
        public TMPShaker MainTextShaker;
        public Button activedButton;
        public KeyCode[] anyActivedKeys = {KeyCode.Return, KeyCode.Space};
        public List<SingleDisplay> displays = new();
        int textIndex = -1;
        Tween text_Tween;

        private void Start() {
            activedButton?.onClick.AddListener(UpdateText);
            MainTextShaker = Text_Main.GetComponent<TMPShaker>();
        }

        private void Update() {
            for(int i = 0; i < anyActivedKeys.Length; i++){
                if(Input.GetKeyDown(anyActivedKeys[i])){
                    UpdateText();
                }
            }
        }

        void UpdateText(){
            if(textIndex >= displays.Count - 1){return;}
            textIndex++;
            text_Tween?.Kill();
            string targetText = displays[textIndex].displayString;
            switch(displays[textIndex].textMotionMode){
                case TextMotionMode.Large:
                targetText = $"<size=140%>{targetText}";
                //MainTextShaker.SetShake(false);
                break;
                case TextMotionMode.Small:
                targetText = $"<size=65%>{targetText}";
                //MainTextShaker.SetShake(false);
                break;
                
                case TextMotionMode.LargeAndShake:
                //MainTextShaker.SetShake(true);
                targetText = $"<size=140%>{targetText}";
                break;
                case TextMotionMode.SmallAndSake:
                //MainTextShaker.SetShake(true);
                targetText = $"<size=65%>{targetText}";
                break;
                case TextMotionMode.Shake:
                //MainTextShaker.SetShake(true);
                break;
            }
            Text_Name.text = displays[textIndex].name;
            text_Tween?.Kill();
            text_Tween = Text_Main.DOText(targetText, targetText.Length * DialogueValues.CharIntervalTime);
            text_Tween.SetEase(Ease.Linear);
        }

    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(DialogueDisplay))]
    public class DialogueDisplayEditor : Editor{
        public static Type TMP_Type = typeof(TextMeshProUGUI);
        SerializedObject serializedObj;
        SerializedProperty anyActivedKeys_Property,
        displays_Property;

        private void OnEnable(){
            serializedObj = new(target);
            anyActivedKeys_Property = serializedObj.FindProperty("anyActivedKeys");
            displays_Property = serializedObj.FindProperty("displays");
        }

        public override void OnInspectorGUI()
        {
            var T = target as DialogueDisplay;
            //if(T == null){return;}

            serializedObj.Update();
            using(new egl.VerticalScope(EditorStyles.helpBox)){
                egl.LabelField("Normal Settings",style : EditorStyles.boldLabel);
                T.Text_Name = (TextMeshProUGUI)egl.ObjectField("Character Name Shower", T.Text_Name, TMP_Type, true);
                T.Text_Main = (TextMeshProUGUI)egl.ObjectField("Dialogue Text Shower", T.Text_Main, TMP_Type, true);
                T.activedButton = (Button)egl.ObjectField("Actived Button", T.activedButton, typeof(Button), true);
                egl.PropertyField(anyActivedKeys_Property);
            }
            egl.Space(10);
            egl.PropertyField(displays_Property);


            if(GUI.changed){
                EditorUtility.SetDirty(target);
            }
            serializedObj.ApplyModifiedProperties();
        }
    }
    #endif
}

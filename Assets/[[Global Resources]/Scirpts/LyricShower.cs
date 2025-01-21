using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using FSF.Collection.Utilities;

namespace  FSF.Collection{
    public class LyricShower : MonoBehaviour
    {
        [SerializeField] private Text targetText;
        [SerializeField] private AudioSource audioSource;
        public List<LyricValueKey> keys;
        public TextAsset LyricAsset;
        bool Showing = true;
        int index = 0;

        public void GetLyrics(){
            keys = LyricSpliter.Split(LyricAsset);
        }

        private void Awake() {
            if(keys.Count <= 0){GetLyrics();}
        }

        private void Update() {
            if(Showing){
                if(audioSource.time >= keys[index + 1 > keys.Count ? index : index + 1].Time){
                    index++;
                    targetText.text = keys[index].Lyric;
                }
            }
        }
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(LyricShower))]
    public class LyricShowerEditor : Editor{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var T = target as LyricShower;
            if ((T.keys == null || T.keys.Count <= 0) && T.LyricAsset != null)
            {
                if (GUILayout.Button("Get Lyrics"))
                {
                    T.GetLyrics();
                }
            }
        }
    }
    #endif
}

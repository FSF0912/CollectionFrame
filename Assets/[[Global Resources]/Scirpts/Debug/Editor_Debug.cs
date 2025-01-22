#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using egl = UnityEditor.EditorGUILayout;
using UnityEngine.UIElements;

namespace FSF.Collection{
    public class Editor_Debug : MonoSingleTon<Editor_Debug>
    {
        public KeyCode ReStartSceneCode = KeyCode.R;

        protected override void OnAwake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update() {
            if(Input.GetKeyDown(ReStartSceneCode)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    [CustomEditor(typeof(Editor_Debug))]
    public class Editor_Debug_Editor : Editor{
        SerializedProperty re_Key;

        private void OnEnable() {
            re_Key = serializedObject.FindProperty("ReStartSceneCode");
        }

        public override void OnInspectorGUI()
        {
            var T = target as Editor_Debug;
            if(T == null){return;}
            serializedObject.Update();
            using(var VerticalScope = new egl.VerticalScope(EditorStyles.helpBox)){
                egl.LabelField("↓Restart Scene Code↓");
                egl.PropertyField(re_Key, GUIContent.none);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}

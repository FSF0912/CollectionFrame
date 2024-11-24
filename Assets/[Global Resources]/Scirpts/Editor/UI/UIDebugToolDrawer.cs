using UnityEngine;
using UnityEditor;
using egl = UnityEditor.EditorGUILayout;

namespace FSF.CollectionFrame
{
    [CustomEditor(typeof(UIDebugTool))]
    public class UIDebugToolDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            UIDebugTool T = target as UIDebugTool;

            T.RunInPlayMode = egl.ToggleLeft("Run In Play Mode", T.RunInPlayMode);
            egl.Space();
            T.EnableCanvasSwitcher = egl.ToggleLeft("Enable Canvas Switcher", T.EnableCanvasSwitcher);
            if (T.EnableCanvasSwitcher)
            {
                T.targetCanvas = (Canvas)egl.ObjectField("Target Canvas", T.targetCanvas, typeof(Canvas), true);
            
                if(T.targetCanvas != null){egl.HelpBox("CG Switcher is working...", MessageType.None);}
            }







        
            if (GUI.changed)
            {
                EditorUtility.SetDirty(T);
            }
        }
    }
}

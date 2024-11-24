using UnityEditor;

namespace FSF.CollectionFrame.GalFrame{

    public class GraphEditorWindow : EditorWindow{
        [MenuItem("FSF/Edit Graph")]
        public static void Open()
        {
            GetWindow<GraphEditorWindow>("SampleGraphView");
        }
    }
}

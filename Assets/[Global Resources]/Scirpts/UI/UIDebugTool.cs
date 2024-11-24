using System.Linq;
using UnityEditor;
using UnityEngine;
namespace FSF.CollectionFrame
{
#nullable enable
    [ExecuteAlways]
    public class UIDebugTool : MonoBehaviour
    {
        public bool RunInPlayMode = false;
        //canvas
        public bool EnableCanvasSwitcher = false;
        public Canvas? targetCanvas;
        public CanvasGroup[]? canvasGroups;

        private void Update() {
        
#if UNITY_EDITOR
            if(EditorApplication.isPlaying){
                if(RunInPlayMode){MethodBody();}
            }
            else{MethodBody();}
#endif
            void MethodBody(){
                //canvasgroup
                if(!EnableCanvasSwitcher){return;}
                if(targetCanvas == null){return;}
                canvasGroups = targetCanvas.GetComponentsInChildren<CanvasGroup>();
                if(canvasGroups == null){return;}
                 CanvasGroup? currentCG = Selection.activeGameObject?.GetComponent<CanvasGroup>();
                if(currentCG == null){return;}
                if(canvasGroups.Contains(currentCG)){
                    foreach (var item in canvasGroups)
                    {
                        item.alpha = 0;
                    }
                    currentCG.alpha = 1;
                }
                //
            }
        }
    }
#nullable disable
}

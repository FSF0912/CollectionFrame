using UnityEditor;
using UnityEngine;

namespace FSF.Editor
{
    public class CombineMeshes : UnityEditor.Editor
    {
        private static GameObject targetMesh;
        private static string savedName = "_mesh";
        [MenuItem("Tools/Combine Mesh/Combine Selected Mesh")]
        public static void CombineSelectedMesh()
        {
            targetMesh = Selection.activeGameObject;
            MeshRenderer[] meshRenderers = targetMesh.GetComponentsInChildren<MeshRenderer>();

            Material[] materialList = new Material[meshRenderers.Length];
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                materialList[i] = meshRenderers[i].sharedMaterial;
            }

            MeshFilter[] meshFilters = targetMesh.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }
            GameObject g = new GameObject();
            g.transform.name = targetMesh.transform.name + savedName;
            g.transform.SetParent(targetMesh.transform.parent);
            g.transform.SetSiblingIndex(targetMesh.transform.GetSiblingIndex() + 1);
            g.AddComponent<MeshFilter>().mesh = new Mesh();
            g.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, false);
            g.gameObject.SetActive(true);
            g.AddComponent<MeshRenderer>().sharedMaterials = materialList;
        }

        [MenuItem("Tools/Combine Mesh/Save Selected Mesh To Asset")]
        public static void SaveSelectedMeshToAsset()
        {
            Mesh mesh = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh;
            AssetDatabase.CreateAsset(mesh, "Assets/" + targetMesh.transform.name + savedName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.ProBuilder;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UObject = UnityEngine.Object;

namespace NCat.Editor
{
	public static class ProBuilderEditorUtility
	{
		private static IEnumerable<Face> ConvertTrianglesToFaces(IEnumerable<int> triangles)
		{
			var faces = new List<Face>();
			var list = triangles.ToList();
			for (int i = 0; i < list.Count; i += 3)
			{
				var face = new Face(
					new[]
					{
						list[i],
						list[i + 1], 
						list[i + 2]
					});
				faces.Add(face);
			}
			return faces.ToArray();
		}

		private static ProBuilderMesh ToProBuilderMesh(
			this GameObject go, 
			Transform parent)
		{
			if (!go.TryGetComponent<MeshFilter>(out var meshFilter))
			{
				throw new NullReferenceException($"{go} does not have a MeshFilter.");
			}
			
			var mesh = meshFilter.sharedMesh;
			if (mesh == null)
			{
				throw new NullReferenceException("MeshFilter must have a Mesh.");
			}
			
			var pbMesh = ProBuilderMesh.Create(
				mesh.vertices,
				ConvertTrianglesToFaces(mesh.triangles)
			);
			
			pbMesh.RebuildWithPositionsAndFaces(pbMesh.positions, pbMesh.faces);
			
			var pbTrans = pbMesh.transform;
			var trans = go.transform;
				
			pbTrans.SetParent(parent);
				
			pbTrans.position = trans.position;
			pbTrans.rotation = trans.rotation;
			pbTrans.localScale = trans.lossyScale;

			pbTrans.name = go.name;
				
			var meshRenderer = go.GetComponent<MeshRenderer>();
			if (meshRenderer)
			{
				var pbMeshRenderer = pbMesh.gameObject.GetComponent<MeshRenderer>();
				pbMeshRenderer.sharedMaterial = meshRenderer.sharedMaterial;
			}
				
			return pbMesh;
		}

		/// <summary>
		/// 将所选的对象转换为合并后的 ProBuilderMesh
		/// </summary>
		/// <returns>合并后的 ProBuilderMesh</returns>
		[MenuItem("Tools/ProBuilder/To Combine object", false, 30000)]
		public static ProBuilderMesh SelectedObjectToCombineObject()
		{
			var gameObjects = Selection.gameObjects;
			if (gameObjects.Length == 0) return null;
			var rootParent = gameObjects[0].transform.root;
			if (rootParent == gameObjects[0].transform)
			{
				rootParent = null;
			}
			var parent = new GameObject("ProBuilder").transform;
			parent.SetParent(rootParent);
			
			var pbMeshes = new List<ProBuilderMesh>();
			foreach (var go in gameObjects)
			{
				try
				{
					pbMeshes.Add(go.ToProBuilderMesh(parent));
				}
				catch (NullReferenceException ex)
				{
					Debug.LogException(ex);
				}
			}
			
			// From MergeObjects.DoAction()
			
			var curMesh = pbMeshes[0];
			if (pbMeshes.Count < 2)
			{
				curMesh.name += "_Combine";
				curMesh.transform.SetParent(rootParent);
				UObject.DestroyImmediate(parent.gameObject);
				return curMesh;
			}
			var res = CombineMeshes.Combine(pbMeshes, curMesh);

			if (res != null)
			{
				foreach (var mesh in res)
				{
					mesh.Optimize();
					if (mesh == curMesh) continue;
					
					var gameObject = mesh.gameObject;
					gameObject.name = Selection.activeGameObject.name + "-Merged";
					Undo.RegisterCreatedObjectUndo(gameObject, "Merge Objects");
				}

				// Delete donor objects if they are not part of the result 
				foreach (var t in pbMeshes
					         .Where(t => t != null && res.Contains(t) == false))
				{
					Undo.DestroyObjectImmediate(t.gameObject);
				}
				
				curMesh.name += "_Combine";
				curMesh.transform.SetParent(rootParent);
				UObject.DestroyImmediate(parent.gameObject);
			}

			ProBuilderEditor.Refresh();

			return curMesh;
		}
		
		/// <summary>
		/// 将所选的对象转换为合并后的 ProBuilderMesh 并应用变换
		/// </summary>
		[MenuItem("Tools/ProBuilder/To Combine object and freeze transform", false, 30000)]
		public static void SelectedObjectToCombineObjectFreezeTransform()
		{
			var pbMesh = SelectedObjectToCombineObject();
			if (!pbMesh) return;

			var verts = pbMesh.VerticesInWorldSpace();

			var pbTrans = pbMesh.transform;
			pbTrans.position = Vector3.zero;
			pbTrans.rotation = Quaternion.identity;
			pbTrans.localScale = Vector3.one;

			foreach (var face in pbMesh.faces)
				face.manualUV = true;

			pbMesh.positions = verts;

			pbMesh.ToMesh();
			pbMesh.Refresh();
			pbMesh.Optimize();
			
			pbMesh.name += "_Freeze";
		}

		/// <summary>
		/// 将所选的对象转换为 ProBuilderMesh
		/// <remarks>转换后的 ProBuilderMesh 将会储存到 ProBuilder (GameObject) 下</remarks>
		/// </summary>
		[MenuItem("Tools/ProBuilder/To ProBuilder meshes", false, 30000)]
		public static void SelectedObjectsToProBuilderMeshes()
		{
			var gameObjects = Selection.gameObjects;
			if (gameObjects.Length == 0) return;
			var rootParent = gameObjects[0].transform.root;
			if (rootParent == gameObjects[0].transform)
			{
				rootParent = null;
			}
			var parent = new GameObject("ProBuilder").transform;
			parent.SetParent(rootParent);
			foreach (var go in gameObjects)
			{
				try
				{
					go.ToProBuilderMesh(parent);
				}
				catch (NullReferenceException ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		[MenuItem("Tools/ProBuilder/Fix invalid ProBuilder meshes", false, 30000)]
		public static void FixInvalidProBuilderMesh()
		{
			foreach (var go in Selection.gameObjects)
			{
				if (!go.TryGetComponent<ProBuilderMesh>(out var mesh)) continue;
				mesh.RebuildWithPositionsAndFaces(mesh.positions, mesh.faces);
			}
		}
	}
}
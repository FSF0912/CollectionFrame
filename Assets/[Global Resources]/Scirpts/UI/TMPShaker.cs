using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace FSF.CollectionFrame
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMPShaker : MonoBehaviour
    {
        TextMeshProUGUI text;
        /// <summary>
        /// 速度（时间间隔）
        /// </summary>
        public float shakeSpeed = 0.05f;

        /// <summary>
        /// 幅度
        /// </summary>
        public float shakeAmount = 1f;
        Coroutine coroutine;

        private Vector3[] m_rawVertex{
            get {return GetRawVertex();}
        }
        

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.ForceMeshUpdate();
        
        }

        public void SetShake(bool shaking){
            if(shaking){
                if(coroutine != null){StopCoroutine(coroutine);}
                coroutine = StartCoroutine(ShakeText());
            }
            else if(coroutine != null){
                StopCoroutine(coroutine);
            }
        }

        private Vector3[] GetRawVertex()
        {
            if(text.textInfo.characterCount > 0)
            {
                TMP_CharacterInfo charInfo = text.textInfo.characterInfo[0];
                TMP_MeshInfo meshInfo = text.textInfo.meshInfo[charInfo.materialReferenceIndex];

                Vector3[] rawVertex = new Vector3[meshInfo.vertices.Length];
                for (int i = 0; i < meshInfo.vertices.Length; i++)
                {
                    rawVertex[i] = new Vector3(meshInfo.vertices[i].x, meshInfo.vertices[i].y, meshInfo.vertices[i].z);
                }
                return rawVertex;
            }
            return null;
        }

        IEnumerator ShakeText()
        {
            while (true)
            {
                for (int i = 0; i < text.textInfo.characterCount; i++)
                {
                    TMP_CharacterInfo currentCharInfo = text.textInfo.characterInfo[i];
                    TMP_MeshInfo meshInfo = text.textInfo.meshInfo[currentCharInfo.materialReferenceIndex];
                    int vertexCount;
                    if (i < text.textInfo.characterCount - 1)
                    {
                        TMP_CharacterInfo nextCharInfo = text.textInfo.characterInfo[i + 1];
                        vertexCount = nextCharInfo.vertexIndex - currentCharInfo.vertexIndex;
                    }
                    else
                    {
                        vertexCount = meshInfo.vertices.Length - currentCharInfo.vertexIndex;
                    }
                    int vertexIndex = currentCharInfo.vertexIndex;

                    int mult = 100;
                    float xOffset = GetRandom((int)-shakeAmount * mult, (int)shakeAmount * mult, i);
                    float yOffset = GetRandom((int)-shakeAmount * mult, (int)shakeAmount * mult, i + text.textInfo.characterCount);
                    Vector3 offset = new Vector3(xOffset, yOffset) / 100f;

                    Vector3[] vertices = meshInfo.vertices;
                    for (int j = vertexIndex; j < vertexIndex + vertexCount; j++)
                    {
                        vertices[j] = m_rawVertex[j] + offset;
                    }
                }

                text.UpdateVertexData();
                yield return new WaitForSeconds(shakeSpeed);
            }
        }

        public int GetMemory(object o)
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);
            IntPtr addr = GCHandle.ToIntPtr(h);
            return int.Parse(addr.ToString());
        }

        public float GetRandom(int min, int Max, int iSeed)
        {
            System.Random rd = new System.Random(GetMemory(iSeed));
            return (rd.Next(min, Max));
        }
    }
}

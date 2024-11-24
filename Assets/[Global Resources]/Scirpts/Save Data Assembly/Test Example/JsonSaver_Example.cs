using System;
using UnityEngine;

namespace SaveDataAssembly.Demo
{
    internal sealed class JsonSaver_Example : MonoBehaviour
    {
        internal TEST TEST_DEMO;
        [Serializable]internal class TEST { }

        /// <summary>
        /// Initialzation
        /// </summary>
        internal void Start()
        {
            bool condition = true;//�������
            JsonArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
            {
                //�����¼�

            }, () =>
            {
                //�ļ�ӵ���¼�

            }, () =>
            {
                //�����¼�

            });
        }

        /// <summary>
        /// Data Operation
        /// </summary>
        /// <param name="LevelNameKey"></param>
        internal void SaveData(string LevelNameKey)
        {
            JsonArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //д������ݲ���

            });
        }
        internal void GetData(string LevelNameKey)
        {
            JsonArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = JsonArchivePreservation<TEST>.ReadJsonData;

                //��ȡ�����ݲ���

            });
        }
        internal void WriteData(TEST DataStruct) => JsonArchivePreservation<TEST>.WriteJsonData(DataStruct);
        internal TEST ReadJsonData => JsonArchivePreservation<TEST>.ReadJsonData;
    }
}
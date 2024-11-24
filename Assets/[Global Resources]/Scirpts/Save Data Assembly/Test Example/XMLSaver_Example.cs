using System;
using UnityEngine;

namespace SaveDataAssembly.Demo
{
    internal sealed class XMLSaver_Example : MonoBehaviour
    {
        internal TEST TEST_DEMO;
        [Serializable]internal class TEST { }

        /// <summary>
        /// Initialzation
        /// </summary>
        internal void Start()
        {
            bool condition = true;//�������
            XMLArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
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
            XMLArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //д������ݲ���

            });
        }
        internal void GetData(string LevelNameKey)
        {
            XMLArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = XMLArchivePreservation<TEST>.ReadXMLData;

                //��ȡ�����ݲ���

            });
        }
        internal void WriteData(TEST DataStruct) => XMLArchivePreservation<TEST>.WriteXMLData(DataStruct);
        internal TEST ReadJsonData => XMLArchivePreservation<TEST>.ReadXMLData;
    }
}
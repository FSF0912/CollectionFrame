using System;
using UnityEngine;

namespace SaveDataAssembly.Demo
{
    internal class BinarySystemSaver_Example : MonoBehaviour
    {
        internal TEST TEST_DEMO;
        [Serializable] internal class TEST { }
        internal void Start()
        {
            bool condition = true;//�������
            BinarySystemArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
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
        internal void SaveData(string LevelNameKey)
        {
            BinarySystemArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //д������ݲ���

            });
        }
        internal void GetData(string LevelNameKey)
        {
            BinarySystemArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = BinarySystemArchivePreservation<TEST>.ReadeBinarySystemData;

                //��ȡ�����ݲ���

            });
        }
        internal void WriteData(TEST DataStruct) => BinarySystemArchivePreservation<TEST>.WriteBinarySystemData(DataStruct);
        internal TEST ReadJsonData => BinarySystemArchivePreservation<TEST>.ReadeBinarySystemData;
    }
}
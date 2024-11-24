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
            bool condition = true;//检查条件
            BinarySystemArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
            {
                //报错事件

            }, () =>
            {
                //文件拥有事件

            }, () =>
            {
                //更新事件

            });
        }
        internal void SaveData(string LevelNameKey)
        {
            BinarySystemArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //写入的数据操作

            });
        }
        internal void GetData(string LevelNameKey)
        {
            BinarySystemArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = BinarySystemArchivePreservation<TEST>.ReadeBinarySystemData;

                //读取的数据操作

            });
        }
        internal void WriteData(TEST DataStruct) => BinarySystemArchivePreservation<TEST>.WriteBinarySystemData(DataStruct);
        internal TEST ReadJsonData => BinarySystemArchivePreservation<TEST>.ReadeBinarySystemData;
    }
}
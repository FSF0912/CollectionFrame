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
            bool condition = true;//检查条件
            JsonArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
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

        /// <summary>
        /// Data Operation
        /// </summary>
        /// <param name="LevelNameKey"></param>
        internal void SaveData(string LevelNameKey)
        {
            JsonArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //写入的数据操作

            });
        }
        internal void GetData(string LevelNameKey)
        {
            JsonArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = JsonArchivePreservation<TEST>.ReadJsonData;

                //读取的数据操作

            });
        }
        internal void WriteData(TEST DataStruct) => JsonArchivePreservation<TEST>.WriteJsonData(DataStruct);
        internal TEST ReadJsonData => JsonArchivePreservation<TEST>.ReadJsonData;
    }
}
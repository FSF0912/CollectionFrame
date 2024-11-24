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
            bool condition = true;//检查条件
            XMLArchivePreservation<TEST>.Initialzation(condition, TEST_DEMO, () =>
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
            XMLArchivePreservation<TEST>.WriteLevelData(LevelNameKey, TEST_DEMO, () =>
            {
                //写入的数据操作

            });
        }
        internal void GetData(string LevelNameKey)
        {
            XMLArchivePreservation<TEST>.ReadLevelData(LevelNameKey, () =>
            {
                TEST ReadJsonData = XMLArchivePreservation<TEST>.ReadXMLData;

                //读取的数据操作

            });
        }
        internal void WriteData(TEST DataStruct) => XMLArchivePreservation<TEST>.WriteXMLData(DataStruct);
        internal TEST ReadJsonData => XMLArchivePreservation<TEST>.ReadXMLData;
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace FSF.CollectionFrame{
    public static class NormalUtility
    {
        /// <summary>
        /// return RectTransform from component.
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static RectTransform rectTransform(this Component go){
            return go.transform as RectTransform;
        }
    }
}
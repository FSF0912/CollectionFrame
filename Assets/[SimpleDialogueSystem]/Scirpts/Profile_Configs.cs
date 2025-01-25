using UnityEngine;

namespace FSF.DialogueSystem{
    public static class Dialogue_Configs{
        public static float textTypeDuration = 0.03f;
        public static float characterSwitchDuration = 0.6f;

        #region Transform params
        //switcher Rect control params
        public static readonly float normal_Anc_PosY = -550;
        public static readonly Vector2 normal_Anc_Position = new Vector2(0, -550);
        public static readonly Vector2 left_Anc_Position = new Vector2(-600, -550);
        public static readonly Vector2 right_Anc_Position = new Vector2(600, -550);
        public static readonly Vector2 default_sizeDelta = new Vector2(0, -1100);
        #endregion
    }
}
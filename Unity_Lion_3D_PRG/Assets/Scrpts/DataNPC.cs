using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// 腳本化物件系統：存放NPC名稱、對話資料與音效
    /// </summary>
    [CreateAssetMenu(menuName ="AScript/NPC Data")]
    public class DataNPC : ScriptableObject
    {
        [SerializeField, Header("角色名稱")]
        public string names = "";
        [SerializeField, Header("代表色彩")]
        public Color color;
        [SerializeField, Header("設定對話"), NonReorderable]
        public DataDialogue[] dialog;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [SerializeField, Header("對話內容")]
        public string content;
        [SerializeField, Header("對話音效")]
        public AudioClip sound;
    }
}

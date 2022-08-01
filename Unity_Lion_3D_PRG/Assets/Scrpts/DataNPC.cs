using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// �}���ƪ���t�ΡG�s��NPC�W�١B��ܸ�ƻP����
    /// </summary>
    [CreateAssetMenu(menuName ="AScript/NPC Data")]
    public class DataNPC : ScriptableObject
    {
        [SerializeField, Header("����W��")]
        public string names = "";
        [SerializeField, Header("�N���m")]
        public Color color;
        [SerializeField, Header("�]�w���"), NonReorderable]
        public DataDialogue[] dialog;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [SerializeField, Header("��ܤ��e")]
        public string content;
        [SerializeField, Header("��ܭ���")]
        public AudioClip sound;
    }
}

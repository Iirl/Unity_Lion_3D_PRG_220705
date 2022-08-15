using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    [CreateAssetMenu(menuName = "AScript/Enemy Data", fileName = "DataEnemy", order = 1)]
    public class DataEnemy : ScriptableObject

    {
        [SerializeField, Header("�ĤH�W��")]
        public string name="";
        [SerializeField, Header("��q"), Range(100,20000)]
        public float hp=100;
        [SerializeField, Header("�����O"), Range(0,999)]
        public float attack=10;
        [SerializeField, Header("�l�ܽd��"),Range(0,500)]
        public float rangeTrack=50;
        [SerializeField, Header("�����d��"),Range(0,250)]
        public float rangeAttack=25;
        [SerializeField, Header("�����t��"),Range(0,100)]
        public float speedWalk=4;
        [SerializeField, Header("�������v"),Range(0,1)]
        public float probilityItem=0.5f;
        [SerializeField, Header("���ݮɶ��d��")]
        public Vector2 waitTimeRange;
        [SerializeField, Header("�l�ܹ�H")]
        public LayerMask target_msak;
        [SerializeField, Header("�����ɶ�"), Range(1, 9)]
        public float intervalAtkTime;
        [SerializeField, Header("�����D��")]
        private GameObject DropItem;
    }
}
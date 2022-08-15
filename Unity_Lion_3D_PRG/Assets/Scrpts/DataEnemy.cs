using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    [CreateAssetMenu(menuName = "AScript/Enemy Data", fileName = "DataEnemy", order = 1)]
    public class DataEnemy : ScriptableObject

    {
        [SerializeField, Header("敵人名稱")]
        public string name="";
        [SerializeField, Header("血量"), Range(100,20000)]
        public float hp=100;
        [SerializeField, Header("攻擊力"), Range(0,999)]
        public float attack=10;
        [SerializeField, Header("追蹤範圍"),Range(0,500)]
        public float rangeTrack=50;
        [SerializeField, Header("攻擊範圍"),Range(0,250)]
        public float rangeAttack=25;
        [SerializeField, Header("走路速度"),Range(0,100)]
        public float speedWalk=4;
        [SerializeField, Header("掉落機率"),Range(0,1)]
        public float probilityItem=0.5f;
        [SerializeField, Header("等待時間範圍")]
        public Vector2 waitTimeRange;
        [SerializeField, Header("追蹤對象")]
        public LayerMask target_msak;
        [SerializeField, Header("攻擊時間"), Range(1, 9)]
        public float intervalAtkTime;
        [SerializeField, Header("掉落道具")]
        private GameObject DropItem;
    }
}
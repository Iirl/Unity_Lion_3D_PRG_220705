using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    [CreateAssetMenu(menuName = "AScript/Attack Data", fileName = "DataAtks", order = 1)]
    public class DataAtk : ScriptableObject
    {
        [SerializeField, Header("¶Ë®`­È"),Range(0,1000)]
        public float[] attack;
        [SerializeField, Header("§ðÀ»©µ¿ð"), Range(0, 10)]
        public float attackInterval;
        [SerializeField, Header("§ðÀ»½d³ò")]
        public Vector3 attackArea = Vector3.one;
        [SerializeField]
        public Vector3 attackOffset;
        [SerializeField]
        public Color attackRangeColor = new Color(1,0,0,0.5f);
        [SerializeField, Header("¥Ø¼Ð¼Ä¤H")]
        public LayerMask targetMask;
        [SerializeField, Header("§ðÀ»°Êµe")]
        public AnimationClip attackAnimation;
        public float attackDelay => attackAnimation.length - attackInterval;
    }
}

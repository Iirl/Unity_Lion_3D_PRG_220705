using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    [CreateAssetMenu(menuName = "AScript/Attack Data", fileName = "DataAtks", order = 1)]
    public class DataAtk : ScriptableObject
    {
        [SerializeField, Header("�ˮ`��"),Range(0,1000)]
        public float[] attack;
        [SerializeField, Header("��������"), Range(0, 10)]
        public float attackInterval;
        [SerializeField, Header("�����d��")]
        public Vector3 attackArea = Vector3.one;
        [SerializeField]
        public Vector3 attackOffset;
        [SerializeField]
        public Color attackRangeColor = new Color(1,0,0,0.5f);
        [SerializeField, Header("�ؼмĤH")]
        public LayerMask targetMask;
        [SerializeField, Header("�����ʵe")]
        public AnimationClip attackAnimation;
        public float attackDelay => attackAnimation.length - attackInterval;
    }
}

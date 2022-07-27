using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR.Target { 

    /// <summary>
    /// �Ϥ��޲z��
    /// </summary>
public class CardManage : MonoBehaviour
{
    [SerializeField, Header("�Ϥ����ѥؼ�")]
    private DefaultObserverEventHandler observerKIDs;

        private void Awake()
        {
            observerKIDs.OnTargetFound.AddListener(CardFond);
            observerKIDs.OnTargetLost.AddListener(CardLost);
        }

        /// <summary>
        /// �d���o�{��
        /// </summary>
        private void CardFond()
        {
            print("<color=green>Find the Card!</color>");
        }

        /// <summary>
        /// �d��������
        /// </summary>
        private void CardLost()
        {
            print("<color=red>Lost the Card!</color>");

        }
}
}
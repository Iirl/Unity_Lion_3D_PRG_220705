using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR.Target { 

    /// <summary>
    /// 圖片管理器
    /// </summary>
public class CardManage : MonoBehaviour
{
    [SerializeField, Header("圖片辨識目標")]
    private DefaultObserverEventHandler observerKIDs;

        private void Awake()
        {
            observerKIDs.OnTargetFound.AddListener(CardFond);
            observerKIDs.OnTargetLost.AddListener(CardLost);
        }

        /// <summary>
        /// 卡片發現時
        /// </summary>
        private void CardFond()
        {
            print("<color=green>Find the Card!</color>");
        }

        /// <summary>
        /// 卡片消失時
        /// </summary>
        private void CardLost()
        {
            print("<color=red>Lost the Card!</color>");

        }
}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace AR.Target
{

    /// <summary>
    /// 圖片管理器
    /// </summary>
    public class CardManage : MonoBehaviour
    {
        [SerializeField, Header("圖片辨識目標")]
        private DefaultObserverEventHandler observerKIDs;
        [SerializeField, Header("角色動畫")]
        private Animator ani;
        string victory = "actVictory";
        [SerializeField, Header("攻擊動畫")]
        private Button btn_attack;
        string attack = "actAttack";
        [SerializeField, Header("跳躍動畫")]
        private VirtualButtonBehaviour vbbJump;
        string jump = "actJump";
        // 用 Find 取得音樂元件。
        private AudioSource bgm;


        private void Awake()
        {
            // 監聽事件追加
            observerKIDs.OnTargetFound.AddListener(CardFond);
            observerKIDs.OnTargetLost.AddListener(CardLost);
            btn_attack.onClick.AddListener(Attack);
            vbbJump.RegisterOnButtonPressed(PressJump);
            // 自動尋找元件
            bgm = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        }

        private void PressJump(VirtualButtonBehaviour obj)
        {
            print("Jump");
            ani.SetTrigger(jump);
        }

        /// <summary>
        /// 卡片發現時
        /// </summary>
        private void CardFond()
        {
            print("<color=green>Find the Card!</color>");
            ani.SetTrigger(victory);
            bgm.Play();
        }

        /// <summary>
        /// 卡片消失時
        /// </summary>
        private void CardLost()
        {
            print("<color=red>Lost the Card!</color>");
            bgm.Stop();

        }

        private void Attack()
        {
            print("<color=#ffb303>Attack!</color>");
            ani.SetTrigger(attack);
        }
    }
}
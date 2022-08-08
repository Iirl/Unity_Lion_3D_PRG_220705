using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// NPC 對話系統
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
        [SerializeField, Header("對話資料")]
        DataNPC npcData;
        [SerializeField, Header("NPC Camera")]
        GameObject npcCam;
        ThirdPersonalController playerCtrl;
        AttackController playerAtk;
        DiaSystem diaSystem;
        Animator aniTalkTips,ani;
        string parTalk = "TalkIn",parNpcTalk = "isTalk";
        bool isTrigger;

        private void Awake()
        {
            aniTalkTips = GameObject.Find("對話提示系統").GetComponent<Animator>();
            ani = GetComponent<Animator>();
            diaSystem = FindObjectOfType<DiaSystem>();
        }
        #region 動態事件處理
        private void Update()
        {
             if (Input.GetKeyDown(KeyCode.E) && isTrigger)
            {
                StartCoroutine(OpenDialog());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerTraggerEvent(true, other.gameObject);            
                
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerTraggerEvent(false, other.gameObject);
        }
        #endregion

        /// <summary>
        /// 當玩家進入判定區域時，抓取資料。
        /// </summary>
        /// <param name="enter">是否進入</param>
        /// <param name="obj">玩家物件</param>
        private void PlayerTraggerEvent(bool enter, GameObject obj)
        {
            if (obj.tag.Contains("Player")) {
                aniTalkTips.SetBool(parTalk, enter);
                isTrigger = enter;
                playerCtrl = obj.GetComponent<ThirdPersonalController>();
                playerAtk = obj.GetComponent<AttackController>();
            }
            print($"進入狀態:{isTrigger}");
        }

        /// <summary>
        /// 開啟對話框 
        /// </summary>
        /// <returns>等到對話框結束之後就結束鎖定狀態</returns>
        private IEnumerator OpenDialog()
        {
            // 開始對話
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            playerCtrl.StopMove();
            aniTalkTips.SetBool(parTalk, false);
            ani.SetBool(parNpcTalk, isTrigger);
            isTrigger = false; // 切換狀態
            yield return StartCoroutine(diaSystem.StartDialogSystem(npcData,StateReset));
            // 對話結束
            // 方法一：我使用協同程序等待對話系統結束的方式來實作結束等待，底下可以做結束後的動作。
            // 方法二：用委派傳方法給對話系統，這樣對話系統結束之後就會直接執行該方法。
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;

        }
        /// <summary>
        /// 動畫狀態回復狀態
        /// </summary>
        private void StateReset()
        {
            //放入要做的事
            ani.SetBool(parNpcTalk, false);
        }
    }
}
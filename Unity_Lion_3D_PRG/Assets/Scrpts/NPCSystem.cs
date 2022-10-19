using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
        [SerializeField]
        GameObject missionObj, stageObj;
        [SerializeField]
        CheckPoint relectionCheckPoint;
        CinemachineVirtualCamera npcCam_machine;
        // 取得玩家資料與控制器元件
        ThirdPersonalController playerCtrl;
        AttackController playerAtk;
        Vector3 Player_loc;
        Quaternion Self_rot;
        // 對話系統處理
        DiaSystem diaSystem;
        Animator aniTalkTips,ani;
        string parTalk = "TalkIn",parNpcTalk = "isTalk";
        bool isTrigger;
        bool isFirst = true;
        /////////////////////////
        public void SetNPCData(DataNPC dpc) => npcData = dpc;

        private void Awake()
        {
            aniTalkTips = GameObject.Find("對話提示系統").GetComponent<Animator>();
            ani = GetComponent<Animator>();
            diaSystem = FindObjectOfType<DiaSystem>();
            npcCam_machine = npcCam.GetComponent<CinemachineVirtualCamera>();
            //
            Self_rot = transform.rotation;
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
                npcCam_machine.m_Follow = obj.transform;
                aniTalkTips.SetBool(parTalk, enter);
                Player_loc = obj.transform.position;
                isTrigger = enter;
                playerCtrl = obj.GetComponent<ThirdPersonalController>();
                playerAtk = obj.GetComponent<AttackController>();
            }
            //print($"進入狀態:{isTrigger}");
        }

        /// <summary>
        /// 開啟對話框 
        /// </summary>
        /// <returns>等到對話框結束之後就結束鎖定狀態</returns>
        private IEnumerator OpenDialog()
        {
            // 開始對話
            transform.LookAt(Player_loc);
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            playerCtrl.StopMove();
            aniTalkTips.SetBool(parTalk, false);
            ani.SetBool(parNpcTalk, isTrigger);
            yield return new WaitForSeconds(0.5f);
            npcCam_machine.m_Follow = null;
            isTrigger = false; // 切換狀態
            yield return StartCoroutine(diaSystem.StartDialogSystem(npcData,StateReset));
            // 對話結束
            // 方法一：我使用協同程序等待對話系統結束的方式來實作結束等待，底下可以做結束後的動作。
            // 方法二：用委派傳方法給對話系統，這樣對話系統結束之後就會直接執行該方法。
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            isTrigger = true; // 切換狀態

        }
        /// <summary>
        /// 動畫狀態回復狀態
        /// </summary>
        private void StateReset()
        {
            //放入要做的事
            ani.SetBool(parNpcTalk, false);
            transform.rotation = Self_rot;
            if (isFirst & missionObj)
            {
                missionObj.SetActive(true);
                stageObj.SetActive(false);
                if (relectionCheckPoint) relectionCheckPoint.DisableNPC();
                isFirst = false;

            }
        }
    }
}
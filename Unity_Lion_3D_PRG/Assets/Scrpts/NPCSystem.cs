using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace agi
{
    /// <summary>
    /// NPC ��ܨt��
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
        [SerializeField, Header("��ܸ��")]
        DataNPC npcData;
        [SerializeField, Header("NPC Camera")]
        GameObject npcCam;
        [SerializeField]
        GameObject missionObj, stageObj;
        [SerializeField]
        CheckPoint relectionCheckPoint;
        CinemachineVirtualCamera npcCam_machine;
        // ���o���a��ƻP�������
        ThirdPersonalController playerCtrl;
        AttackController playerAtk;
        Vector3 Player_loc;
        Quaternion Self_rot;
        // ��ܨt�γB�z
        DiaSystem diaSystem;
        Animator aniTalkTips,ani;
        string parTalk = "TalkIn",parNpcTalk = "isTalk";
        bool isTrigger;
        bool isFirst = true;
        /////////////////////////
        public void SetNPCData(DataNPC dpc) => npcData = dpc;

        private void Awake()
        {
            aniTalkTips = GameObject.Find("��ܴ��ܨt��").GetComponent<Animator>();
            ani = GetComponent<Animator>();
            diaSystem = FindObjectOfType<DiaSystem>();
            npcCam_machine = npcCam.GetComponent<CinemachineVirtualCamera>();
            //
            Self_rot = transform.rotation;
        }
        #region �ʺA�ƥ�B�z
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
        /// ���a�i�J�P�w�ϰ�ɡA�����ơC
        /// </summary>
        /// <param name="enter">�O�_�i�J</param>
        /// <param name="obj">���a����</param>
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
            //print($"�i�J���A:{isTrigger}");
        }

        /// <summary>
        /// �}�ҹ�ܮ� 
        /// </summary>
        /// <returns>�����ܮص�������N������w���A</returns>
        private IEnumerator OpenDialog()
        {
            // �}�l���
            transform.LookAt(Player_loc);
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            playerCtrl.StopMove();
            aniTalkTips.SetBool(parTalk, false);
            ani.SetBool(parNpcTalk, isTrigger);
            yield return new WaitForSeconds(0.5f);
            npcCam_machine.m_Follow = null;
            isTrigger = false; // �������A
            yield return StartCoroutine(diaSystem.StartDialogSystem(npcData,StateReset));
            // ��ܵ���
            // ��k�@�G�ڨϥΨ�P�{�ǵ��ݹ�ܨt�ε������覡�ӹ�@�������ݡA���U�i�H�������᪺�ʧ@�C
            // ��k�G�G�Ωe���Ǥ�k����ܨt�ΡA�o�˹�ܨt�ε�������N�|��������Ӥ�k�C
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            isTrigger = true; // �������A

        }
        /// <summary>
        /// �ʵe���A�^�_���A
        /// </summary>
        private void StateReset()
        {
            //��J�n������
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
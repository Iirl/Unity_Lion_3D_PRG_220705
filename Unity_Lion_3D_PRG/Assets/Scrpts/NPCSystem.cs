using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ThirdPersonalController playerCtrl;
        AttackController playerAtk;
        DiaSystem diaSystem;
        Animator aniTalkTips,ani;
        string parTalk = "TalkIn",parNpcTalk = "isTalk";
        bool isTrigger;

        private void Awake()
        {
            aniTalkTips = GameObject.Find("��ܴ��ܨt��").GetComponent<Animator>();
            ani = GetComponent<Animator>();
            diaSystem = FindObjectOfType<DiaSystem>();
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
                aniTalkTips.SetBool(parTalk, enter);
                isTrigger = enter;
                playerCtrl = obj.GetComponent<ThirdPersonalController>();
                playerAtk = obj.GetComponent<AttackController>();
            }
            print($"�i�J���A:{isTrigger}");
        }

        /// <summary>
        /// �}�ҹ�ܮ� 
        /// </summary>
        /// <returns>�����ܮص�������N������w���A</returns>
        private IEnumerator OpenDialog()
        {
            // �}�l���
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;
            playerCtrl.StopMove();
            aniTalkTips.SetBool(parTalk, false);
            ani.SetBool(parNpcTalk, isTrigger);
            isTrigger = false; // �������A
            yield return StartCoroutine(diaSystem.StartDialogSystem(npcData,StateReset));
            // ��ܵ���
            // ��k�@�G�ڨϥΨ�P�{�ǵ��ݹ�ܨt�ε������覡�ӹ�@�������ݡA���U�i�H�������᪺�ʧ@�C
            // ��k�G�G�Ωe���Ǥ�k����ܨt�ΡA�o�˹�ܨt�ε�������N�|��������Ӥ�k�C
            npcCam.SetActive(isTrigger);
            playerAtk.enabled = !isTrigger;
            playerCtrl.enabled = !isTrigger;

        }
        /// <summary>
        /// �ʵe���A�^�_���A
        /// </summary>
        private void StateReset()
        {
            //��J�n������
            ani.SetBool(parNpcTalk, false);
        }
    }
}
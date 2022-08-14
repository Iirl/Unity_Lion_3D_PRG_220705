using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace agi
{
    /// <summary>
    /// ��ܨt�Ϊ���@�G
    /// �H�J�X��ܮءBNPC���Ū��
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class DiaSystem : MonoBehaviour
    {
        [SerializeField, Header("��ܮصe��")]
        private CanvasGroup dialog;
        [SerializeField, Header("�H���W��")]
        private TextMeshProUGUI nameNPC;
        [SerializeField, Header("�����C��")]
        private Image nameNPC_img;
        [SerializeField, Header("��ܤ��e")]
        private TextMeshProUGUI contentNPC;
        [SerializeField, Header("��ܮخɶ�"), Range(0f, 1f)]
        private float second;
        [SerializeField, Range(0f, 0.5f)]
        private float typefTimes = 0.2f;
        [SerializeField, Range(0f, 1f)]
        private float autoTimes = 0.5f;
        [SerializeField, Header("��ܼаO")]
        private GameObject trangle;
        //[Header("��ܸ��")]
        private DataNPC npcData;
        /// <summary>
        /// 
        /// </summary>
        private AudioSource ads;


        private void DNClear()
        {
            contentNPC.text = "";
            nameNPC.text = "";
        }
        /// <summary>
        /// �Ұʹ�ܮ�
        /// </summary>
        /// <returns></returns>
        /// �ϥΩe���Ө��oNPC System ����k�C
        // �N��ܨt�γ]�����}�A�A�ϥΩe���ӱ����ǻ��L�Ӫ���k
        public delegate void DialogueFinshFunction();
        public IEnumerator StartDialogSystem(DataNPC npcData, DialogueFinshFunction callback)
        {
            this.npcData = npcData;
            nameNPC.text = npcData.names;
            nameNPC_img.color = npcData.color;
            //print("Open Dialog");
            yield return StartCoroutine(FadeEffect(true));

            for (int i = 0; i < npcData.dialog.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));                
                while (!Input.GetKeyDown(KeyCode.E)) yield return null;
                if (Input.GetKeyDown(KeyCode.E)) ads.Stop();
                trangle.SetActive(false);
            }

            yield return StartCoroutine(FadeEffect(false));
            callback();
        }
        /// <summary>
        /// ���r�ĪG
        /// </summary>
        /// <param name="idxDia"></param>
        /// <returns></returns>
        private IEnumerator TypeEffect(int idxDia)
        {
            try
            {   // ��ʤ]�i�H�~����檺�{����o
                if (npcData.dialog[idxDia].sound!=null) ads.PlayOneShot(npcData.dialog[idxDia].sound);

            }
            catch (System.Exception)   {            }
            string content = npcData.dialog[idxDia].content;
            /// ���r�ĪG
            contentNPC.text = "";
            for (int i = 0; i < content.Length; i++)
            {
                contentNPC.text += content[i];
                yield return new WaitForSeconds(typefTimes);


            }
            trangle.SetActive(true);
            yield return new WaitForSeconds(autoTimes);
        }
        /// <summary>
        /// �H�J�H�X�\��
        /// </summary>
        /// <param name="isIn"></param>
        /// <returns></returns>
        private IEnumerator FadeEffect(bool isIn)
        {
            float apai = isIn ? 0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                dialog.alpha += apai;
                yield return new WaitForSeconds(second);
            }
            if (!isIn) contentNPC.text = "";
            CanvasCtrl(dialog, isIn);
        }
        /// <summary>
        /// �e���s�ն}��
        /// </summary>
        /// <param name="cvs">��ܮت��s��</param>
        /// <param name="on">�O�_��ܡA�w�]���_</param>
        private void CanvasCtrl(CanvasGroup cvs, bool on = false)
        {
            cvs.alpha = on ? 1 : 0;
            cvs.interactable = on;
            cvs.blocksRaycasts = on;
        }


        #region Test
        private IEnumerator TestIE()
        {
            print("First");
            yield return new WaitForSeconds(2);
            print("Second");
        }
        #endregion

        #region �ƥ�s��
        private void Awake()
        {
            ads = GetComponent<AudioSource>();
            DNClear();
            //StartCoroutine(StartDialogSystem());
        }
        #endregion
    }

}

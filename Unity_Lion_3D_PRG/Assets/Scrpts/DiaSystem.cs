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
        [SerializeField, Header("��ܮخɶ�"), Range(0f,1f)]
        private float second;
        [SerializeField, Header("��ܼаO")]
        private GameObject trangle;

        private AudioSource ads;

        public DataNPC npcData;


        private void DNClear()
        {
            contentNPC.text = "";
            nameNPC.text = "";
        }

        private IEnumerator DialogLoad()
        {
            nameNPC.text = npcData.names;
            nameNPC_img.color = npcData.color;
            ads.PlayOneShot(npcData.dialog[0].sound);
            string content = npcData.dialog[0].content;
            for (int i = 0; i < content.Length; i++)
            {
                contentNPC.text += content[i];
                yield return new WaitForSeconds(0.01f);

            }
            trangle.SetActive(true);
        }
        private IEnumerator FadIN()
        {
            for (int i = 0; i < 10; i++)
            {
                dialog.alpha += 0.1f;
                yield return new WaitForSeconds(second);
            }
            CanvasCtrl(dialog, true);
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
            StartCoroutine(FadIN());
            StartCoroutine(DialogLoad());
        }
        #endregion
    }

}

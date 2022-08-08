using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace agi
{
    /// <summary>
    /// 對話系統的實作：
    /// 淡入出對話框、NPC資料讀取
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class DiaSystem : MonoBehaviour
    {
        [SerializeField, Header("對話框畫布")]
        private CanvasGroup dialog;
        [SerializeField, Header("人物名稱")]
        private TextMeshProUGUI nameNPC;
        [SerializeField, Header("底圖顏色")]
        private Image nameNPC_img;
        [SerializeField, Header("對話內容")]
        private TextMeshProUGUI contentNPC;
        [SerializeField, Header("對話框時間"), Range(0f, 1f)]
        private float second;
        [SerializeField, Range(0f, 0.5f)]
        private float typefTimes = 0.2f;
        [SerializeField, Range(0f, 1f)]
        private float autoTimes = 0.5f;
        [SerializeField, Header("對話標記")]
        private GameObject trangle;
        [Header("對話資料")]
        public DataNPC npcData;
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
        /// 啟動對話框
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartDialogSystem()
        {
            nameNPC.text = npcData.names;
            nameNPC_img.color = npcData.color;
            yield return StartCoroutine(FadeEffect(true));

            for (int i = 0; i < npcData.dialog.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));
                while (!Input.GetKeyDown(KeyCode.E)) yield return null;
                trangle.SetActive(false);
            }

            yield return StartCoroutine(FadeEffect(false));

        }
        private IEnumerator TypeEffect(int idxDia)
        {
            ads.PlayOneShot(npcData.dialog[idxDia].sound);
            string content = npcData.dialog[idxDia].content;
            /// 打字效果
            contentNPC.text = "";
            for (int i = 0; i < content.Length; i++)
            {
                contentNPC.text += content[i];
                yield return new WaitForSeconds(typefTimes);


            }
            trangle.SetActive(true);
            yield return new WaitForSeconds(autoTimes);
        }
        private IEnumerator FadeEffect(bool isIn)
        {
            float apai = isIn ? 0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                dialog.alpha += apai;
                yield return new WaitForSeconds(second);
            }
            CanvasCtrl(dialog, isIn);
        }
        /// <summary>
        /// 畫布群組開關
        /// </summary>
        /// <param name="cvs">對話框的群組</param>
        /// <param name="on">是否顯示，預設為否</param>
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

        #region 事件群組
        private void Awake()
        {
            ads = GetComponent<AudioSource>();
            DNClear();
            StartCoroutine(StartDialogSystem());
        }
        #endregion
    }

}

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
        [SerializeField, Header("對話框時間"), Range(0f,1f)]
        private float second;
        [SerializeField, Header("對話標記")]
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
            StartCoroutine(FadIN());
            StartCoroutine(DialogLoad());
        }
        #endregion
    }

}

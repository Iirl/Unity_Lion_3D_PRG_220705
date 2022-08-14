using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace AGI
{
    public class MangerFinal : MonoBehaviour
    {

        [SerializeField, Header("畫布")]
        CanvasGroup cv_group;
        [SerializeField, Header("結束標題")]
        TextMeshProUGUI textProEndTilte;
        [SerializeField, Header("背景音樂")]
        private AudioSource audioSource_bgMusic;
        [SerializeField, Header("音效"), Tooltip("結束音效")]
        private AudioClip audioClip_End;
        [SerializeField, Tooltip("撞擊音效")]
        private AudioClip audioClip_Explor;
        [SerializeField, Tooltip("死亡音效")]
        private AudioClip audioClip_Dead;

        /// <summary>
        /// 設定結束標題的文字
        /// </summary>
        [SerializeField, Header("結束文字")]
        public string finalString;

        /// <summary>
        /// 淡入效果
        /// </summary>
        private void FadeIn()
        {
            cv_group.alpha += 0.1f;
            if ( cv_group.alpha >= 1)
            {
                cv_group.interactable = true;
                cv_group.blocksRaycasts = true;
                CancelInvoke("FadeIn");
            }
        }
        
        public string SceneTimeCal()
        {
            float passTime = Time.timeSinceLevelLoad;
            string unit = "sec";
            if (passTime > 3600) { passTime /= 3600; unit = "hour"; }
            else if (passTime > 60) { passTime /= 60; unit = "min"; }
             
            return $"\n Pass Time is {passTime.ToString("0.00")} {unit}";
        }
        public void StopMusic()
        {
            audioSource_bgMusic.Stop();
            audioSource_bgMusic.PlayOneShot(audioClip_End, 1);

        }

        public void FX_Shock()
        {
            audioSource_bgMusic.PlayOneShot(audioClip_Explor, 1);
        }
        public void FX_Shock(float i)
        {
            audioSource_bgMusic.Pause();
            audioSource_bgMusic.PlayOneShot(audioClip_Dead, 1);
            audioSource_bgMusic.volume = i;
            audioSource_bgMusic.PlayDelayed(5);

        }
        public void ExitGame()
        {
            Application.Quit();
            print("離開遊戲唷");
        }

        public void Replay()
        {
            SceneManager.LoadScene(0);
        }

        #region EventOn

        private void Start()
        {
            InvokeRepeating("FadeIn", 0, 0.1f);
            textProEndTilte.text = finalString;
        }

        #endregion
    }

}
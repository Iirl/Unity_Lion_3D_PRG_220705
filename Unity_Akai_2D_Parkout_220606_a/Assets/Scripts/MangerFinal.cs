using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace AGI
{
    public class MangerFinal : MonoBehaviour
    {

        [SerializeField, Header("�e��")]
        CanvasGroup cv_group;
        [SerializeField, Header("�������D")]
        TextMeshProUGUI textProEndTilte;
        [SerializeField, Header("�I������")]
        private AudioSource audioSource_bgMusic;
        [SerializeField, Header("����"), Tooltip("��������")]
        private AudioClip audioClip_End;
        [SerializeField, Tooltip("��������")]
        private AudioClip audioClip_Explor;
        [SerializeField, Tooltip("���`����")]
        private AudioClip audioClip_Dead;

        /// <summary>
        /// �]�w�������D����r
        /// </summary>
        [SerializeField, Header("������r")]
        public string finalString;

        /// <summary>
        /// �H�J�ĪG
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
            print("���}�C����");
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
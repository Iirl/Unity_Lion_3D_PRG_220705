using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    public class CanvasEffect : MonoBehaviour
    {
        [SerializeField, Header("�ĪG�ɶ�"), Range(0.01f, 5f)]
        private float second;
        [SerializeField, Header("�H�Ʈɶ�"), Range(0.01f, 0.5f)]
        private float alphaTime;
        [SerializeField, Header("�e���s��")]
        private CanvasGroup canvasGroup;

        /// <summary>
        /// �H�J�H�X�\��
        /// </summary>
        /// <param name="isIn"></param>
        /// <returns></returns>
        private IEnumerator FadeEffect(CanvasGroup cvg, bool isIn)
        {
            float waitTime = alphaTime * second;
            alphaTime = isIn ? alphaTime: -alphaTime;            
            //print(second / alphaTime);
            for (int i = 0; i < Mathf.Abs( 1 / alphaTime)+1; i++)
            {
                cvg.alpha += alphaTime;
                yield return new WaitForSeconds(waitTime);
            }
            //if (!isIn) contentNPC.text = "";
            CanvasCtrl(cvg, isIn);
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

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>() ?? null;
        }
        // Start is called before the first frame update
        void Start()
        {
            if(canvasGroup) StartCoroutine(FadeEffect(canvasGroup,  false));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
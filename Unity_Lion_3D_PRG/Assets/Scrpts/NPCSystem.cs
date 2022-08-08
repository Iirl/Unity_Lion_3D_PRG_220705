using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// NPC 對話系統
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
        [SerializeField, Header("對話資料")]
        DataNPC npcData;

        Animator aniTalkTips;
        string parTalk = "TalkIn";

        private void Awake()
        {
            aniTalkTips = GameObject.Find("對話提示系統").GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerTraggerEvent(true, other.tag);
                
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerTraggerEvent(false, other.tag);

        }

        private void PlayerTraggerEvent(bool enter, string name)
        {
            if (name.Contains("Player"))
                aniTalkTips.SetBool(parTalk, enter);

        }
    }
}
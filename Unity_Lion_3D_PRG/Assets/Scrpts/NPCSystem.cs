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

        Animator aniTalkTips;
        string parTalk = "TalkIn";

        private void Awake()
        {
            aniTalkTips = GameObject.Find("��ܴ��ܨt��").GetComponent<Animator>();
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
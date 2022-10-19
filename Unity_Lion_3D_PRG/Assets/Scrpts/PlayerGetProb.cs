using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace agi
{
    public class PlayerGetProb : MonoBehaviour
    {
        ThirdPersonalController tpc;
        ObjectPoolSystem objectPool;
        [SerializeField]
        DataNPC setNPCData;
        PlayerHealthSystem playerHPSYS;
        [SerializeField, Header("任務文字")]
        TextMeshProUGUI uiMissionCount;
        [SerializeField, Header("錢幣文字")]
        TextMeshProUGUI uiCoin;
        //
        private bool missionOff;
        private int coin = 0;
        private int dropWepon = 0;
        private int maxWepon = 3;
        private int goalSet;
        private int goalTarget;
        public int GoalSet { get => goalSet; set => goalSet = value; }
        public int GoalTarget { get => goalTarget; set => goalTarget = value; }

        private void Awake()
        {
            tpc = GetComponent<ThirdPersonalController>();
            playerHPSYS = GetComponent<PlayerHealthSystem>();
            objectPool = GameObject.Find("掉落物件池系統").GetComponent<ObjectPoolSystem>();
        }
        private void Start()
        {
            uiMissionCount.text = dropWepon.ToString() + "/" + maxWepon;
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.tag.Contains("ItemPool"))
            {
                objectPool.RelaseObj(hit.gameObject);
                string itemName = hit.gameObject.name;
                if (itemName.Contains("Health")) playerHPSYS.GetHeal(20);
                if (itemName.Contains("Coin")) GetCoin();
                if (itemName.Contains("Wepon")) GetDPWP();
                if (itemName.Contains("Endurance")) playerHPSYS.GetHeal(100);
            }
        }

        private void GetCoin()
        {
            tpc.PlayTrack(8);
            uiCoin.text = (++coin).ToString();
        }
        private void GetDPWP()
        {
            tpc.PlayTrack(9);
            if (missionOff) return;
            if (dropWepon < maxWepon) dropWepon++;
            if (dropWepon >= maxWepon)
            {
                GameObject.Find("Guilder2").GetComponent<NPCSystem>().SetNPCData(setNPCData);
                missionOff = true;
            }
            uiMissionCount.text = (dropWepon).ToString() + "/" + maxWepon;
            
        }
    }
}

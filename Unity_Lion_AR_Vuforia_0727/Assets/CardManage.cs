using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace AR.Target
{

    /// <summary>
    /// �Ϥ��޲z��
    /// </summary>
    public class CardManage : MonoBehaviour
    {
        [SerializeField, Header("�Ϥ����ѥؼ�")]
        private DefaultObserverEventHandler observerKIDs;
        [SerializeField, Header("����ʵe")]
        private Animator ani;
        string victory = "actVictory";
        [SerializeField, Header("�����ʵe")]
        private Button btn_attack;
        string attack = "actAttack";
        [SerializeField, Header("���D�ʵe")]
        private VirtualButtonBehaviour vbbJump;
        string jump = "actJump";
        // �� Find ���o���֤���C
        private AudioSource bgm;


        private void Awake()
        {
            // ��ť�ƥ�l�[
            observerKIDs.OnTargetFound.AddListener(CardFond);
            observerKIDs.OnTargetLost.AddListener(CardLost);
            btn_attack.onClick.AddListener(Attack);
            vbbJump.RegisterOnButtonPressed(PressJump);
            // �۰ʴM�䤸��
            bgm = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        }

        private void PressJump(VirtualButtonBehaviour obj)
        {
            print("Jump");
            ani.SetTrigger(jump);
        }

        /// <summary>
        /// �d���o�{��
        /// </summary>
        private void CardFond()
        {
            print("<color=green>Find the Card!</color>");
            ani.SetTrigger(victory);
            bgm.Play();
        }

        /// <summary>
        /// �d��������
        /// </summary>
        private void CardLost()
        {
            print("<color=red>Lost the Card!</color>");
            bgm.Stop();

        }

        private void Attack()
        {
            print("<color=#ffb303>Attack!</color>");
            ani.SetTrigger(attack);
        }
    }
}
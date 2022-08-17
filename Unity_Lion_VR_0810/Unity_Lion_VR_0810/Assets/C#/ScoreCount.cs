using TMPro;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// �p�����t�ΡA�b�x�ؤ��߭p����ơC
    /// </summary>
    public class ScoreCount : MonoBehaviour
    {
        [SerializeField, Header("�p��y�骫��")]
        private GameObject baseketBall;
        private TextMeshProUGUI boardScore;
        public static int score;

        private void Awake()
        {
            boardScore = GameObject.Find("ScoreA").GetComponent<TextMeshProUGUI>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains(baseketBall.name))
            {
                score += 2;
                boardScore.text = "Score " + score;
            }
        }

        private void ChangeScore(int src)
        {
            score = src;
        }
    }
}
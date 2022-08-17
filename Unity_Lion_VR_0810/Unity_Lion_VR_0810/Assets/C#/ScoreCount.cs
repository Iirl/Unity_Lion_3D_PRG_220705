using TMPro;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// 計分器系統，在籃框中心計算分數。
    /// </summary>
    public class ScoreCount : MonoBehaviour
    {
        [SerializeField, Header("計算球體物件")]
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
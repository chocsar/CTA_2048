using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingElement : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text rankText;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetRank(int rank)
    {
        rankText.text = rank.ToString();
    }
}

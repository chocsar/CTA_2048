using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingWindowModel : MonoBehaviour
{
    private List<int> rankingList = new List<int>();

    public void SetRanking(List<int> ranking)
    {
        rankingList = ranking;
    }
    public List<int> GetRanking()
    {
        return rankingList;
    }

    public List<int> LoadRanking()
    {
        return ScoreManager.Instance.LoadRanking();
    }
}

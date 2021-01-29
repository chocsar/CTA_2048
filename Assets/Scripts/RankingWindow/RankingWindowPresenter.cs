using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingWindowPresenter : MonoBehaviour
{
    private RankingWindowModel rankingWindowModel;
    private RankingWindowView rankingWindowView;

    void Awake()
    {
        rankingWindowModel = GetComponent<RankingWindowModel>();
        rankingWindowView = GetComponent<RankingWindowView>();
    }

    public void OpenWindow()
    {
        rankingWindowModel = GetComponent<RankingWindowModel>();
        rankingWindowView = GetComponent<RankingWindowView>();

        rankingWindowModel.SetRanking(rankingWindowModel.LoadRanking());
        rankingWindowView.SetRanking(rankingWindowModel.GetRanking());
        rankingWindowView.OpenWindow();
    }


}

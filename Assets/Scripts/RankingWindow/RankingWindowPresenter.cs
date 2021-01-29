using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RankingWindowPresenter : MonoBehaviour
{
    private RankingWindowModel rankingWindowModel;
    private RankingWindowView rankingWindowView;

    public void Initialize()
    {
        rankingWindowModel = GetComponent<RankingWindowModel>();
        rankingWindowView = GetComponent<RankingWindowView>();

        rankingWindowModel.ChangeRankingEvent.Subscribe(rankingWindowView.SetRanking);
    }

    public void OpenWindow()
    {
        rankingWindowModel.SetRanking(rankingWindowModel.LoadRanking());
        rankingWindowView.OpenWindow();
    }

    public bool IsOpenWindow()
    {
        return rankingWindowView.IsOpenWindow();
    }



}

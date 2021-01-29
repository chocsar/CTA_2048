using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class RankingWindowModel : MonoBehaviour
{

    public IObservable<List<int>> ChangeRankingEvent => scoreRankingSubject;

    private List<int> scoreRankingList = new List<int>();
    private Subject<List<int>> scoreRankingSubject = new Subject<List<int>>();

    public void SetRanking(List<int> ranking)
    {
        scoreRankingList = ranking;
        scoreRankingSubject.OnNext(scoreRankingList);
    }
    public List<int> GetRanking()
    {
        return scoreRankingList;
    }

    public List<int> LoadRanking()
    {
        return ScoreManager.Instance.LoadRanking();
    }
}

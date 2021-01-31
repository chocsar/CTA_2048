using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RankingWindowView : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject rankingElementPrefab;

    private List<RankingElement> generatedRankingElements = new List<RankingElement>();

    void Start()
    {
        //ボタンの入力を監視
        closeButton.OnClickAsObservable().Subscribe(_ => CloseWindow());
    }

    /// <summary>
    /// ランキングウィンドウを開く
    /// </summary>
    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ランキングウィンドウがアクティブかどうか返す
    /// </summary>
    /// <returns>アクティブかどうか</returns>
    public bool IsOpenWindow()
    {
        return gameObject.activeSelf;
    }

    public void SetRanking(List<int> ranking)
    {
        int rankingCount = ranking.Count;
        int generatedCount = generatedRankingElements.Count;

        //不要な要素を削除
        DeleteElements(ranking.Count);

        //要素の作成
        for (int rank = 0; rank < rankingCount; rank++)
        {
            int score = ranking[rank];

            if (rank >= generatedCount)
            {
                //要素を新たに生成する
                RankingElement rankingElement = Instantiate(rankingElementPrefab).GetComponent<RankingElement>();
                rankingElement.transform.SetParent(content.transform);
                rankingElement.transform.localScale = new Vector3(1, 1, 1);
                //リストに追加
                generatedRankingElements.Add(rankingElement);
            }

            //値の設定
            generatedRankingElements[rank].SetScore(score);
            generatedRankingElements[rank].SetRank(rank + 1);
        }
    }

    /// <summary>
    /// 不要なランキング要素を削除する
    /// </summary>
    /// <param name="rankingCount">ランキングの数</param>
    private void DeleteElements(int rankingCount)
    {
        int generatedCount = generatedRankingElements.Count;

        if (rankingCount < generatedCount)
        {
            int removeNum = generatedCount - rankingCount;
            for (int i = 1; i <= removeNum; i++)
            {
                RankingElement element = generatedRankingElements[generatedCount - i];
                generatedRankingElements.RemoveAt(generatedCount - i);
                Destroy(element.gameObject);
            }
        }
    }

    /// <summary>
    /// ランキングウィンドウを閉じる
    /// </summary>
    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}

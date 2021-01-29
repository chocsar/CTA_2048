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

    void Start()
    {
        //ボタンの入力を監視
        closeButton.OnClickAsObservable().Subscribe(_ => CloseWindow());
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public bool IsOpenWindow()
    {
        return gameObject.activeSelf;
    }

    public void SetRanking(List<int> scoreList)
    {
        //元の要素の削除
        foreach (Transform element in content.transform)
        {
            Destroy(element.gameObject);
        }

        //ランキング要素の生成
        int rank = 1;
        foreach (int score in scoreList)
        {
            GameObject rankingElementObject = Instantiate(rankingElementPrefab) as GameObject;
            rankingElementObject.transform.SetParent(content.transform);
            rankingElementObject.transform.localScale = new Vector3(1, 1, 1);

            RankingElement rankingElement = rankingElementObject.GetComponent<RankingElement>();
            rankingElement.SetScore(score);
            rankingElement.SetRank(rank);

            rank++;
        }
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}

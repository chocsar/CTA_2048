using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Result : MonoBehaviour
{
    [SerializeField] private Text resultText;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject rankingElementPrefab;

    private void Start()
    {
        resultText.text = ScoreManager.Instance.LoadScore().ToString();
        SetRanking();
    }

    private void SetRanking()
    {
        List<int> ranking = ScoreManager.Instance.LoadRanking();

        int rank = 0;
        foreach (int score in ranking)
        {
            RankingElement rankingElement = Instantiate(rankingElementPrefab).GetComponent<RankingElement>();
            rankingElement.transform.SetParent(content.transform);
            rankingElement.transform.localScale = new Vector3(1, 1, 1);

            rankingElement.SetScore(score);
            rankingElement.SetRank(rank + 1);

            rank++;
        }
    }

    public void OnClickRetryButton()
    {
        SceneController.Instance.LoadScene(SceneNames.InGame);
    }
}

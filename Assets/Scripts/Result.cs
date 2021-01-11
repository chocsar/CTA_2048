using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Text resultText;

    private void Start()
    {
        resultText.text = ScoreManager.Instance.LoadScore().ToString();
    }

    public void OnClickRetryButton()
    {
        SceneController.Instance.LoadScene(SceneNames.InGame);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameView : MonoBehaviour
{
    public event Action InputRightKey;
    public event Action InputLeftKey;
    public event Action InputUpKey;
    public event Action InputDownKey;

    [SerializeField] private Text scoreText;

    private void Start()
    {

    }

    private void Update()
    {
        InputKey();
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //nullチェック
            InputRightKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeftKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InputUpKey?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InputDownKey?.Invoke();
        }
    }

}

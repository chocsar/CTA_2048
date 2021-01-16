using System.Collections;
using System;
using UnityEngine;

public class MenuWindowView : MonoBehaviour
{
    public event Action OnClickRestartButton;

    private void Start()
    {
        CloseWindow();
    }

    /// <summary>
    /// Windowを非表示にする
    /// </summary>
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// リスタートボタンを押した時の処理
    /// </summary>
    public void ClickRestartButton()
    {
        OnClickRestartButton?.Invoke();
    }

    /// <summary>
    /// メニューが表示されているかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool IsOpenWindow()
    {
        return gameObject.activeSelf;
    }

}

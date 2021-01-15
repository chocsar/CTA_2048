using System.Collections;
using System;
using UnityEngine;

public class MenuWindowView : MonoBehaviour
{
    public event Action OnClickRestartButton;
    [SerializeField] private GameObject menuWindow;

    /// <summary>
    /// メニューが表示されているかどうか
    /// </summary>
    private bool isActive;

    private void Start()
    {
        //初期化
        CloseWindow();
    }

    /// <summary>
    /// Windowを表示する
    /// </summary>
    public void OpenWindow()
    {
        menuWindow.SetActive(true);
        isActive = true;
    }

    /// <summary>
    /// Windowを非表示にする
    /// </summary>
    public void CloseWindow()
    {
        menuWindow.SetActive(false);
        isActive = false;
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
    public bool IsActive()
    {
        return isActive;
    }

}

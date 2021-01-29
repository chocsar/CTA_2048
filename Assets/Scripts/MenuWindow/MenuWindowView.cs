using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuWindowView : MonoBehaviour
{
    public IObservable<Unit> ClickRestartButtonEvent => restartButton.OnClickAsObservable();

    [SerializeField] private Button closeButton;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        //Buttonの入力を監視
        closeButton.OnClickAsObservable().Subscribe(_ => CloseWindow());
    }

    /// <summary>
    /// Windowを表示する
    /// </summary>
    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// メニューが表示されているかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool IsOpenWindow()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Windowを非表示にする
    /// </summary>
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }



}

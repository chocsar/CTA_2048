using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuWindowView : MonoBehaviour
{
    public IObservable<Unit> ClickRestartButtonEvent
    {
        get { return restartButtonSubject; }
    }

    [SerializeField] private Button closeButton;
    [SerializeField] private Button restartButton;
    private Subject<Unit> restartButtonSubject = new Subject<Unit>();

    private void Start()
    {
        //Buttonの入力を監視
        closeButton.OnClickAsObservable().Subscribe(_ => CloseWindow());
        restartButton.OnClickAsObservable().Subscribe(_ => ClickRestartButton());
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
    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// リスタートボタンを押した時の処理
    /// </summary>
    private void ClickRestartButton()
    {
        restartButtonSubject.OnNext(Unit.Default);
        CloseWindow();
    }

    

}

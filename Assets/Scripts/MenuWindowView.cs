using System.Collections;
using System;
using UnityEngine;
using UniRx;

public class MenuWindowView : MonoBehaviour
{
    public IObservable<Unit> ClickRestartButtonEvent
    {
        get { return restartButtonSubject; }
    }

    private Subject<Unit> restartButtonSubject = new Subject<Unit>();

    /// <summary>
    /// Windowを非表示にする
    /// </summary>
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Windowを表示する
    /// </summary>
    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// リスタートボタンを押した時の処理
    /// </summary>
    public void ClickRestartButton()
    {
        restartButtonSubject.OnNext(Unit.Default);
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

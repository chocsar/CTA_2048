using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneInput : IInput
{
    /// <summary>
    /// フリック入力判定になる移動距離
    /// </summary>
    public float flickDistance = 50f;

    private Vector2 inputStartPosition;
    private Vector2 inputEndPosition;
    private bool isFlicked;

    public InputDirection GetInput()
    {
        InputDirection inputDirection = InputDirection.None;

        if (Input.touchCount == 0) return inputDirection;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            inputStartPosition = Input.mousePosition;
            isFlicked = false;
        }
        else if (touch.phase == TouchPhase.Moved && !isFlicked)
        {
            inputEndPosition = Input.mousePosition;
            inputDirection = DeterminInputDirection();
        }
        else if (touch.phase == TouchPhase.Ended && !isFlicked)
        {
            inputEndPosition = Input.mousePosition;
            inputDirection = DeterminInputDirection();
        }

        return inputDirection;

    }

    /// <summary>
    /// フリック入力の方向を判定する
    /// </summary>
    /// <returns>入力方向</returns>
    private InputDirection DeterminInputDirection()
    {
        Vector2 moveVector = inputEndPosition - inputStartPosition;

        //フリック距離が短い場合
        if (Mathf.Abs(moveVector.x) <= flickDistance && Mathf.Abs(moveVector.y) <= flickDistance)
        {
            return InputDirection.None;
        }

        isFlicked = true;

        if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
        {
            if (moveVector.x > 0)
            {
                return InputDirection.Right;
            }
            else
            {
                return InputDirection.Left;
            }
        }
        else
        {
            if (moveVector.y > 0)
            {
                return InputDirection.Up;
            }
            else
            {
                return InputDirection.Down;
            }
        }
    }
}




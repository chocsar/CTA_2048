using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneInput : IInput
{
    public int inputNumber;
    public bool isFlicked;

    /// <summary>
    /// フリック入力判定になる移動距離
    /// </summary>
    public float flickDistance = 50f;

    private Vector2 inputStartPosition;
    private Vector2 inputEndPosition;

    public int GetInput()
    {
        inputNumber = 0;

        if (Input.GetMouseButtonDown(0))
        {
            inputStartPosition = Input.mousePosition;
            isFlicked = false;
        }
        if (Input.GetMouseButton(0) && !isFlicked)
        {
            inputEndPosition = Input.mousePosition;
            isFlicked = DeterminInput();
        }
        if (Input.GetMouseButtonUp(0) && !isFlicked)
        {
            inputEndPosition = Input.mousePosition;
            isFlicked = DeterminInput();
        }

        return inputNumber;
    }

    /// <summary>
    /// フリック入力を判定するメソッド
    /// </summary>
    /// <returns>フリック入力したかどうか</returns>
    private bool DeterminInput()
    {
        Vector2 moveVector = inputEndPosition - inputStartPosition;

        if (Mathf.Abs(moveVector.x) <= flickDistance && Mathf.Abs(moveVector.y) <= flickDistance)
        {
            return false;
        }

        if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
        {
            if (moveVector.x > 0)
            {
                inputNumber = 1;
            }
            else
            {
                inputNumber = 2;
            }
        }
        else
        {
            if (moveVector.y > 0)
            {
                inputNumber = 3;
            }
            else
            {
                inputNumber = 4;
            }
        }

        return true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneInputManager : SingletonMonoBehaviour<SmartphoneInputManager>, IInput
{
    public bool isInputRight;
    public bool isInputLeft;
    public bool isInputUp;
    public bool isInputDown;
    public bool isFlicked;

    /// <summary>
    /// フリック入力判定になる移動距離
    /// </summary>
    public float flickDistance = 50f;

    private Vector2 inputStartPosition;
    private Vector2 inputEndPosition;
    private void Update()
    {
        isInputRight = false;
        isInputLeft = false;
        isInputUp = false;
        isInputDown = false;

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
    }

    public bool InputRight()
    {
        return isInputRight;
    }
    public bool InputLeft()
    {
        return isInputLeft;
    }
    public bool InputUp()
    {
        return isInputUp;
    }
    public bool InputDown()
    {
        return isInputDown;
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
                isInputRight = true;
            }
            else
            {
                isInputLeft = true;
            }
        }
        else
        {
            if (moveVector.y > 0)
            {
                isInputUp = true;
            }
            else
            {
                isInputDown = true;
            }
        }

        return true;
    }

}

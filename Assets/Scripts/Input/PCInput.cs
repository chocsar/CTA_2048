using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : IInput
{
    public int GetInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }
}

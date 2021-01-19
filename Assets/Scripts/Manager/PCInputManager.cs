using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputManager : SingletonMonoBehaviour<PCInputManager>, IInput
{
    public bool InputRight()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
    public bool InputLeft()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    public bool InputUp()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
    public bool InputDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    public void LoadScene(str sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGame : ASingleton<SceneGame>
{
    public void PauseScene()
    {
        Time.timeScale = 0;
    }

    public void ContinueScene()
    {
        Time.timeScale = 1;
    }
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject about;
    [SerializeField] private GameObject instructions;

    public void StartGame()
    {
        SceneGame.Instance.LoadScene(1);
    }

    public void OnAboutGame()
    {
        about.SetActive(true);
    }
    public void OffAboutGame()
    {
        about.SetActive(false);
    }
    public void OnInstructionGame()
    {
        instructions.SetActive(true);
    }
    public void OffInstructionGame()
    {
        instructions.SetActive(false);
    }
}

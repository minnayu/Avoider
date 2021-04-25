using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //void Start()
    //{
    //    SceneManager.LoadScene("Menu");
    //}
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Playing Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}

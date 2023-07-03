using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class MenuHelper : MonoBehaviour
{
    public GameObject menuUI;    

    public void OpenMenu()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        menuUI.SetActive(false);
    }

    public void LoadScene(int scene)
    {        
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void LoadScene(int level)
    {
        Debug.Log("clicou");
        SceneManager.LoadScene(level);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    void Start()
    {
        Invoke("InitGame", 1.5f);
    }

    public void InitGame()
    {
        SceneManager.LoadScene("Game");
    }
}

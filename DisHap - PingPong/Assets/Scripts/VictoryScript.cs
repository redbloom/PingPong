using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMainMenu", 3.0f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

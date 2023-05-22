using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMenu : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
     
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

       
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

    
        SceneManager.LoadScene("Main_Menu");
    }
}
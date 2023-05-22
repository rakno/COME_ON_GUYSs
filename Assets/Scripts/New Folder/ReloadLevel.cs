using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    private AudioClip clip;
    public AudioSource clipSource;

    private void Start()
    {
        clipSource.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Increment the scene index or set it to 0 if it exceeds the total scene count
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene("MainGame");
    }
}
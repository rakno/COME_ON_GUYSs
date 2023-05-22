using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AIWIN : MonoBehaviour
{

    private bool hasCollided = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("FinishLine"))
        {
            hasCollided = true;
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Increment the scene index or set it to 0 if it exceeds the total scene count
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene("Loss");
    }
}

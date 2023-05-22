using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prefa : MonoBehaviour
{


    public string prefabName;

    public void OnButtonClick()
    {
        PlayerPrefs.SetString("SelectedPrefab", prefabName);
        SceneManager.LoadScene("MainGame");
    }
}



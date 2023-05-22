using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public GameObject Skin_ID;
    public void OnButtonClick()
    {
        PrefabSelection.selectedPrefab = Skin_ID;
        SceneManager.LoadScene("MainGame");
    }
}

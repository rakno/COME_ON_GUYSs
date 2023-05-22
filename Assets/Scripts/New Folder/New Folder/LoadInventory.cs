using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInventory : MonoBehaviour
{
    public void LoaddingScene(string sceneName)
    {
        SceneManager.LoadScene("InventoryMenu");
    }
}

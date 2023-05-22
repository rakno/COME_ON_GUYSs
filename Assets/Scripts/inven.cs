using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inven : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    void Start()
    {
        string selectedPrefab = PlayerPrefs.GetString("SelectedPrefab");
        GameObject prefabToSpawn = null;

        if (selectedPrefab == "Prefab1")
        {
            prefabToSpawn = prefab1;
        }
        else if (selectedPrefab == "Prefab2")
        {
            prefabToSpawn = prefab2;
        }
        else if (selectedPrefab == "Prefab3")
        {
            prefabToSpawn = prefab3;
        }

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log(selectedPrefab);
        }
    }

}

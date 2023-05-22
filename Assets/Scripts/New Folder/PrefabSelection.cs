using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSelection : MonoBehaviour
{
    public static GameObject selectedPrefab;
    public GameObject skinPosition;
    GameObject newGameObject;



    private void Start()
    {
        //selectedPrefab = Resources.Load<GameObject>("SkinPrefabs 1/Player 1");
        Debug.Log(skinPosition.transform.position);
        Debug.Log("Skin loaded");

        newGameObject = Instantiate(selectedPrefab, skinPosition.transform);
        newGameObject.transform.position = skinPosition.transform.position;

    }

    
}

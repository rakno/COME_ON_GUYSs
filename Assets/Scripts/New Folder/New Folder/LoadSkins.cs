using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadSkins : MonoBehaviour
{
    public GameObject skinButton;
    public GameObject ButtonParent;

    void Start()
    {
        
        Object[] prefabs = Resources.LoadAll("SkinPrefabs", typeof(GameObject));
        int i = 1;
        foreach (Object obj in prefabs)
        {


            GameObject newSkinButton = Instantiate(skinButton, ButtonParent.transform);
            Text SkinNumber = newSkinButton.GetComponentInChildren<Text>();
            SkinNumber.text = i.ToString();
            GameObject prefab = (GameObject)obj;
            newSkinButton.GetComponent<ButtonAction>().Skin_ID = prefab;
            i++;

        }

    }
}

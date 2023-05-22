using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillZone : MonoBehaviour
{
    public Image[] heartImages;
    public int hearts = 3; 

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterControls>().LoadCheckPoint();
            hearts--;
            UpdateHeartUI();
            if (hearts == 0)
            {
                SceneManager.LoadScene("Loss");
            }
        }
    }

    void UpdateHeartUI()
    {
       
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < hearts)
            {
                heartImages[i].enabled = true; 
            }
            else
            {
                heartImages[i].enabled = false; 
            }
        }
    }
}

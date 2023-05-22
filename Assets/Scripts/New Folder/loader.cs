using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loader : MonoBehaviour
{

    public Slider loadingSlider;
    private float timer;
    private float duration = 7f;
    private bool isLoading = false;

    private void Start()
    {
        timer = 0f;
        isLoading = true;
    }

    private void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                timer = duration;
                isLoading = false;
                SceneManager.LoadScene("Main_Menu");
            }

            loadingSlider.value = timer / duration;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLoading : MonoBehaviour
{
    private AudioClip themeSong;
    public AudioSource themesongSource;


    private void Start()
    {
        themesongSource.Play();
    }
    public void LoaddingScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}

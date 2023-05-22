using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFile : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioClip clip;
    public AudioSource clipSource;

    private void OnTriggerEnter(Collider other)
    {
      
        
            clipSource.Play();
        
    }
    
}

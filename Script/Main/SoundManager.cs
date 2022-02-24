using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
            }
            return instance;
        }
    }

    public AudioSource music, sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

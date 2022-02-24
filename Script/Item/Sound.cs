using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip audioClip;
    public void PlaySound()
    {
        if (GameManager.Instance.settingData.soundSwitch && audioClip != null)
            SoundManager.Instance.sound.PlayOneShot(audioClip); 
    }
}

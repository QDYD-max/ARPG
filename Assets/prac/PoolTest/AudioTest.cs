using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.LoadMusic("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioSource source = AudioManager.Instance.LoadSoundEffect("Click");
            source.PlayOneShot(source.clip);
        }
    }
}

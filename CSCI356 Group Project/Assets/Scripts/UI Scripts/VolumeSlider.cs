using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private AudioSource volumeCheck;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        volumeCheck = GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { SetUniversalVolume(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUniversalVolume()
    {
        AudioListener.volume = slider.normalizedValue;
        volumeCheck.Play();
    }
}

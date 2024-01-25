using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    public Slider MainSlider;
    // Start is called before the first frame update
    void Start()
    {
        MainSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = MainSlider.value;
    }
}

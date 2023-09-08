using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAmmoCounter : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] ammos;
    public Text ammoCountText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // To Find How Many Ammos are in the Level
        ammos = GameObject.FindGameObjectsWithTag("Ammo");
        // To Display Ammos Left
        ammoCountText.text = "Ammos Left : " + ammos.Length.ToString();
    }
}

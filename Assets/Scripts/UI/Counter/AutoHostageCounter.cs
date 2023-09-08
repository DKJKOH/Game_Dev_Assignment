using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoHostageCounter : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] hostages;
    public Text hostageCountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To Find How Many Hostages are in the Level
        hostages = GameObject.FindGameObjectsWithTag("Hostage");
        // To Display Hostages Left
        hostageCountText.text = "Hostages Left : " + hostages.Length.ToString();
    }
}

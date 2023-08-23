using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostageCounter : MonoBehaviour
{
    public int hostage;
    public Text hostageDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Display of hostage amount
        hostageDisplay.text = "Hostages Left: " + hostage.ToString();
    }

    // Get hostage amount for display
    public int getHostage()
    {
        return hostage;
    }

    // Update Hostage amount
    public void setHostage(int NewHostage)
    {
        hostage = NewHostage;
    }
}

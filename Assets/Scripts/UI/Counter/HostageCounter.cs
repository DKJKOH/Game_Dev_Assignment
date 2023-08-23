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
        hostageDisplay.text = "Hostages Left: " + hostage.ToString();
    }

    public int getHostage()
    {
        return hostage;
    }

    public void setHostage(int NewHostage)
    {
        hostage = NewHostage;
    }
}

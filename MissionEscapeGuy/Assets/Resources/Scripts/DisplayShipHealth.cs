using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplaySHipHealth : MonoBehaviour
{
    private Scrollbar slider;

    void Start()
    {
        slider = slider == null ? GetComponent<Scrollbar>() : slider;
    }

    void Update()
    {
        slider.size = (float)playerData.spaceShipHealth / 50f;
    }
}

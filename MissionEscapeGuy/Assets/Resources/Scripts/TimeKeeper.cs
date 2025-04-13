using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TimeKeeper : NetworkBehaviour
{
    public float time;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = text == null ? GetComponent<TextMeshProUGUI>() : text;
        time = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(Math.Ceiling(time).ToString());
        time -= Time.deltaTime;
    }

}

using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    public static float time;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 180f;
        text = text == null ? GetComponent<TextMeshProUGUI>() : text;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(Math.Ceiling(time).ToString());

        if (NetworkManager.Singleton.IsHost)
        {
            time -= Time.deltaTime;
        }
    }

}

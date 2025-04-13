using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = text == null ? GetComponent<TextMeshProUGUI>() : text;
        time = 180;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(Math.Ceiling(time).ToString());
        time -= Time.deltaTime;
    }
}

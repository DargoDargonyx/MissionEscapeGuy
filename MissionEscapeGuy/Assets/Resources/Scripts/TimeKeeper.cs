using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float time;
    TextMeshPro text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = Time.deltaTime * 180;
        text = text == null ? GetComponent<TextMeshPro>() : text;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(Math.Ceiling(time).ToString());
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}

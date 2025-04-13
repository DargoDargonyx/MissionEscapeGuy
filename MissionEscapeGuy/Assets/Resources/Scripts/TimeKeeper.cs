using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeKeeper : MonoBehaviour
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

        if (time <= 0)
        {
            SceneManager.LoadScene("EndMenu");
            //Scene endScene = SceneManager.GetSceneByName("EndMenu");
            //SceneManager.SetActiveScene(endScene);
        }
    }

}

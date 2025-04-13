using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.Collections;
using Unity.Netcode;

public class UsernameScript : MonoBehaviour
{
    private Scrollbar slider;
    private Camera camera;
    public TMP_Text userDisplay;
    public GameObject myPlayer;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
        userDisplay.text = myPlayer.GetComponent<TheGuy>().username.Value.ToString();
    }
}

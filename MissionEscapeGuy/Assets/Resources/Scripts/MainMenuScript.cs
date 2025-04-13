using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class HelloWorldManager : MonoBehaviour
{

    [SerializeField] private GameObject hostButton;
    public void OnPlayButtonClicked() 
    {
        hostButton.GetComponent<Animator>().SetBool("buttonPressed", true);
        SceneManager.LoadSceneAsync("GameWorld");
    }

}
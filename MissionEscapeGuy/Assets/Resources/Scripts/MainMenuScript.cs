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
    [SerializeField] private GameObject joinButton;
    [SerializeField] private GameObject joinIPField;

    public void OnHostButtonClicked() 
    {
        hostButton.GetComponent<Animator>().SetBool("buttonPressed", true);
        MasterController.isHost = true;
        Invoke("loadGameWorld", 0.5f);
    }

    public void OnClientButtonClicked() 
    {
        joinButton.GetComponent<Animator>().SetBool("buttonPressed", true);
        MasterController.isHost = false;
        MasterController.connectTo = joinIPField.GetComponent<TMP_InputField>().text;
        Invoke("loadGameWorld", 0.5f);
    }

    private void loadGameWorld()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }

}
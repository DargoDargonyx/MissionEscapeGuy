using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class HelloWorldManager : MonoBehaviour
{
    public void OnHostButtonClicked() 
    {
        MasterController.isHost = true;
        SceneManager.LoadSceneAsync("GameWorld");
    }

    public void OnClientButtonClicked() 
    {
        MasterController.isHost = false;
        SceneManager.LoadSceneAsync("GameWorld");
    }
}
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class HelloWorldManager : MonoBehaviour
{
    VisualElement rootVisualElement;
    Button hostButton;
    Button clientButton;
    Label statusLabel;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootVisualElement = uiDocument.rootVisualElement;
        
        hostButton = CreateButton("HostButton", "Host");
        clientButton = CreateButton("ClientButton", "Client");
        statusLabel = CreateLabel("StatusLabel", "Not Connected");
        
        rootVisualElement.Clear();
        rootVisualElement.Add(hostButton);
        rootVisualElement.Add(clientButton);
        rootVisualElement.Add(statusLabel);
        
        hostButton.clicked += OnHostButtonClicked;
        clientButton.clicked += OnClientButtonClicked;
    }
    
    void OnDisable()
    {
        hostButton.clicked -= OnHostButtonClicked;
        clientButton.clicked -= OnClientButtonClicked;
    }

    void OnHostButtonClicked() 
    {
        MasterController.isHost = true;
        SceneManager.LoadSceneAsync("GameWorld");
    }

    void OnClientButtonClicked() 
    {
        MasterController.isHost = false;
        SceneManager.LoadSceneAsync("GameWorld");
    }

    // Disclaimer: This is not the recommended way to create and stylize the UI elements, it is only utilized for the sake of simplicity.
    // The recommended way is to use UXML and USS. Please see this link for more information: https://docs.unity3d.com/Manual/UIE-USS.html
    private Button CreateButton(string name, string text)
    {
        var button = new Button();
        button.name = name;
        button.text = text;
        button.style.width = 240;
        button.style.backgroundColor = Color.white;
        button.style.color = Color.black;
        button.style.unityFontStyleAndWeight = FontStyle.Bold;
        return button;
    }

    private Label CreateLabel(string name, string content)
    {
        var label = new Label();
        label.name = name;
        label.text = content;
        label.style.color = Color.black;
        label.style.fontSize = 18;
        return label;
    }

    void SetStartButtons(bool state)
    {
        hostButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        clientButton.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
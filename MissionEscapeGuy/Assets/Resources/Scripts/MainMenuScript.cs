using UnityEngine;
using UnityEngine.SceneManagement;
public class HelloWorldManager : MonoBehaviour
{

    [SerializeField] private GameObject hostButton;
    public void OnPlayButtonClicked() 
    {
        if (buttonHasBeenPressed())
        {
            hostButton.GetComponent<Animator>().SetBool("buttonPressed", true);
            SceneManager.LoadSceneAsync("GameWorld");
        }
    }

    private bool buttonHasBeenPressed()
    {
        return playerData.isPurple 
            || playerData.isBlue 
            || playerData.isRed 
            || playerData.isGreen 
            || playerData.isOrange;
    }

    public void purpleButton()
    {
        playerData.isPurple = true;
        playerData.isBlue = false;
        playerData.isRed = false;
        playerData.isGreen = false;
        playerData.isOrange = false;
    }
    public void blueButton()
    {
        playerData.isPurple = false;
        playerData.isBlue = true;
        playerData.isRed = false;
        playerData.isGreen = false;
        playerData.isOrange = false;
    }
    public void redButton()
    {
        playerData.isPurple = false;
        playerData.isBlue = false;
        playerData.isRed = true;
        playerData.isGreen = false;
        playerData.isOrange = false;
    }
    public void greenButton()
    {
        playerData.isPurple = false;
        playerData.isBlue = false;
        playerData.isRed = false;
        playerData.isGreen = true;
        playerData.isOrange = false;
    }
    public void orangeButton()
    {
        playerData.isPurple = false;
        playerData.isBlue = false;
        playerData.isRed = false;
        playerData.isGreen = false;
        playerData.isOrange = true;
    }

}
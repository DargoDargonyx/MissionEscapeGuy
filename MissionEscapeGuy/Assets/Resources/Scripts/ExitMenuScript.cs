using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject ExitGameButton;
    [SerializeField] private GameObject RestartGameButton;

    public void onExitButtonClicked()
    {
        Invoke("quitGame", 0.5f);
        ExitGameButton.GetComponent<Animator>().SetBool("buttonPressed", true);
    }
    public void onRestartButtonClicked()
    {
        Invoke("loadMainMenu", 0.5f);
        RestartGameButton.GetComponent<Animator>().SetBool("buttonPressed", true);
    }
    private void quitGame()
    {
        Application.Quit();
    }
    private void loadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

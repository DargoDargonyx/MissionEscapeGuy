using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TheGuy[] players;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets all the players in the game
        // Needs to be adapted to multiplayer use
        // You got this Pierce! 
        players ??= GetComponentsInChildren<TheGuy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

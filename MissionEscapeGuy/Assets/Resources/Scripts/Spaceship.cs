using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private const float MAX_HEALTH = 50f;
    public float health;

    void Start()
    {
        health = MAX_HEALTH;
    }

    void Update()
    {
        playerData.spaceShipHealth = health;
        if (health == 0)
        {
            SceneManager.LoadSceneAsync("EndMenu");
        }
    }

    public void takeDamage(float damage)
    {
        if (health >= damage)
        {
            health -= damage;
        }
        else
        {
            health = 0f;
        }
    }

}

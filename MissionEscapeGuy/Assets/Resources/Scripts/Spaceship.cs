using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private const float MAX_HEALTH = 50f;
    private float health;

    void Start()
    {
        health = MAX_HEALTH;
    }

    void Update()
    {
        
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
        Debug.Log(health);
    }

}

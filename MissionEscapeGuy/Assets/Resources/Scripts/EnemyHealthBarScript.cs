using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Scrollbar slider;
    private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.size = currentHealth / maxHealth;
    }

    void Start()
    {
        slider = slider == null ? GetComponent<Scrollbar>() : slider;
        camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }
}

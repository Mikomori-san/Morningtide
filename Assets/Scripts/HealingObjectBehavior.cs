using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObjectBehavior : MonoBehaviour
{
    public float floatStrength = 0.5f; // You can adjust this to change the floating strength
    public float rotationSpeed = 20f; // You can adjust this to change the rotation speed

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = initialPosition + new Vector3(0.0f, Mathf.Sin(Time.time) * floatStrength, 0.0f);
        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

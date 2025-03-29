using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Public variable to adjust rotation speed from the Inspector
    public float rotationSpeed = 100f;
    
    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}


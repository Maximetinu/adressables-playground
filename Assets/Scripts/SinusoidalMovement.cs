using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 1f;
    public float offset = 0f;
    public float amplitude = 1f;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }
    
    void Update()
    {
        transform.position = originalPosition + direction * amplitude * Mathf.Sin(Time.time * speed + offset);
    }
}

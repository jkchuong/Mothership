using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float minSpeed = 3f;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody2d.velocity = new Vector2(0, -Random.Range(minSpeed, maxSpeed));
    }
}

using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Cat : MonoBehaviour
{


    public float speed { get; private set; } = 1.0f;
    public float damage { get; private set; } = 10.0f;
    public float force { get; private set; } = 1.0f;

    private void Awake()
    {

    }
}
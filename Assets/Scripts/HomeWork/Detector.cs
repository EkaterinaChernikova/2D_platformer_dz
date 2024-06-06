using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Detector : MonoBehaviour
{
    private Vector2 _targetPosition;
    private Rigidbody2D _targetRigidbody2D;
    private PlayerHealth _targetHealth;

    public bool isDetected { get; private set; } = false;
    public bool isTouched { get; private set; } = false;
    public bool isDead { get; private set; } = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            isTouched = true;
            collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D targetRigidbody2D);
            _targetRigidbody2D = targetRigidbody2D;
            isDead = component.GetValue() == 0 ? true : false;
            _targetHealth = component;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            isTouched = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            isDetected = true;
            _targetPosition = collision.transform.position;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            isDetected = false;
        }
    }

    public Vector2 GetTargetPosition()
    {
        return _targetPosition;
    }

    public Rigidbody2D GetTargetRigidbody()
    {
        return _targetRigidbody2D;
    }

    public PlayerHealth GetTargetHealth()
    {
        return _targetHealth;
    }
}
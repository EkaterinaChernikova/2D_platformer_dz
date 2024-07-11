using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]

public class Detector : MonoBehaviour
{
    public Transform targetTransform { get; private set; }
    public PlayerHealth targetHealth { get; private set; }
    public Rigidbody2D playerRigidbody2D { get; private set; }

    public event Action<bool> onTargetDetected;
    public event Action<bool> onTargetTouched;
    public event Action<bool> onTargetDead;

    private bool IsTargetDead()
    {
        return targetHealth.GetValue() == 0 ? true : false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            onTargetTouched?.Invoke(true);
            targetTransform = collision.transform;
            targetHealth = playerHealth;
            playerRigidbody2D = collision.transform.GetComponent<Rigidbody2D>();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            onTargetTouched?.Invoke(false); 
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            onTargetDetected?.Invoke(true);
            targetTransform = collision.transform;
            targetHealth = playerHealth;
            onTargetDead?.Invoke(IsTargetDead());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            onTargetDetected?.Invoke(false);
        }
    }
}
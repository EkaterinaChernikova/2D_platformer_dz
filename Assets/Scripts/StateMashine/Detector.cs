using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private PatrolState _patrolState;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            _patrolState.SetPlayerDetected(true);
            //component.TakeDamage(_damage);
            //collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D);
            //rigidbody2D.AddForce((Vector2.up + (_isMoveLeft ? Vector2.left : Vector2.right)) * _force, ForceMode2D.Impulse);
        }
    }
}

using UnityEngine;

public class Medkit : MonoBehaviour
{
    private float _healCount = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeHeal(_healCount);
            Destroy(gameObject);
        }
    }
}

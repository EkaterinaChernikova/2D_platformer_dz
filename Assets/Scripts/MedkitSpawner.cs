using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _medkit;
    [SerializeField] private float _timer = 10.0f;

    private float _passTime = 0.0f;
    private float _spawnHeight = 6.0f;
    private float _spawnBorder = 5.0f;

    private void Update()
    {
        _passTime += Time.deltaTime;

        if (_passTime > _timer)
        {
            _passTime = 0.0f;

            SpawnMedkit();
        }
    }

    private void SpawnMedkit()
    {
        Instantiate(_medkit, new Vector2(Random.Range(-_spawnBorder, _spawnBorder), _spawnHeight), Quaternion.identity);
    }
}

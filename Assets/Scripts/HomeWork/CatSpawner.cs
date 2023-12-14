using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cat;
    [SerializeField] private Transform _spawnPoint_01;
    [SerializeField] private Transform _spawnPoint_02;
    [SerializeField] private float _timer = 10.0f;

    private Transform[] _spawnPoints;
    private float _passTime = 0;

    private void Start()
    {
        _spawnPoints = new Transform[] { _spawnPoint_01, _spawnPoint_02 };
    }

    private void Update()
    {
        _passTime += Time.deltaTime;

        if (_passTime > _timer)
        {
            _passTime = 0.0f;

            SpawnCats();
        }
    }

    private void SpawnCats()
    {
        foreach (Transform point in _spawnPoints)
        {
            Instantiate(_cat, point);
        }
    }
}

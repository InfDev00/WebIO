using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public Vector2 spawnRange;
    public float spawnDelay;

    private float _time = 100f;

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if (_time > spawnDelay)
        {
            Spawn();
            _time = 0f;
        }
    }

    private void Spawn()
    {
        var x = Random.Range(0, spawnRange.x);
        var y = Random.Range(0, spawnRange.y);

        var position = new Vector3(x, 0, y);

        var newObj = Instantiate(obj, transform);
        newObj.transform.localPosition = position;
    }
}
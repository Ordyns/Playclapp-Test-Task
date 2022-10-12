using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public event System.Action<Cube> CubeSpawned;

    public bool IsActive { get; set; }

    [SerializeField] private GameObjectPool<Cube> objectPool;

    private float _spawnDelay;
    private float _timeToNextSpawn;

    private void Update() {
        if(IsActive && _spawnDelay > 0){
            if(_timeToNextSpawn > 0){
                _timeToNextSpawn -= Time.deltaTime;
            }
            else{
                Spawn();
                
                _timeToNextSpawn = _spawnDelay;
            }
        }
    }

    private void Spawn(){
        Cube cube = objectPool.Get();
        cube.transform.position = Vector3.zero;
        CubeSpawned?.Invoke(cube);
    }

    public void SetSpawnDelay(float delay){
        _spawnDelay = delay;
        _timeToNextSpawn = 0;
    }
}

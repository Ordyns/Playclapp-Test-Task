using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public event System.Action<Cube> CubeSpawned;

    [SerializeField] private Transform cubesParent;
    [SerializeField] private Cube cubePrefab;
    [Space]
    private float _spawnDelay;

    private float _timeToNextSpawn;

    private void Update() {
        if(gameObject.activeSelf){
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
        Cube cube = Instantiate(cubePrefab, cubesParent);
        CubeSpawned?.Invoke(cube);
    }

    public void SetSpawnDelay(float delay){
        _spawnDelay = delay;
    }
}

using UnityEngine;

public class CubesPipelineViewModel : MonoBehaviour
{
    [field:SerializeField] public float SpawnDelay { get; set; }
    [field:SerializeField] public float CubeSpeed { get; set; }
    [field:SerializeField] public float CubeDistance { get; set; }
    [Space]
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private CubesMover cubesMover;

    private void Awake() {
        cubeSpawner.SetSpawnDelay(SpawnDelay);
        cubeSpawner.CubeSpawned += OnCubeSpawned;

        cubesMover.CubeMoved += OnCubeMoved;
    }

    private void OnCubeMoved(Cube cube){
        Destroy(cube.gameObject);
    }

    private void OnCubeSpawned(Cube cube){
        cubesMover.AddCube(cube, CubeSpeed, CubeDistance);
    }
}

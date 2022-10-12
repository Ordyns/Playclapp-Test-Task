using UnityEngine;

public class CubesPipelineViewModel
{
    public ObservableProperty<float> SpawnDelay { get; set; } = new ObservableProperty<float>();
    public ObservableProperty<float> CubeSpeed { get; set; } = new ObservableProperty<float>();
    public ObservableProperty<float> CubeDistance { get; set; } = new ObservableProperty<float>();
    [Space]
    private CubeSpawner _cubeSpawner;
    private CubesMover _cubesMover;

    public CubesPipelineViewModel(CubeSpawner cubeSpawner, CubesMover cubesMover) {
        _cubeSpawner = cubeSpawner;
        _cubesMover = cubesMover;

        UpdateCubeSpawnerSettings();
        cubeSpawner.CubeSpawned += OnCubeSpawned;

        UpdateCubesMoverSettings();
        cubesMover.CubeMoved += OnCubeMoved;

        SpawnDelay.Changed += UpdateCubeSpawnerSettings;
        CubeSpeed.Changed += UpdateCubesMoverSettings;
        CubeDistance.Changed += UpdateCubesMoverSettings;
    }

    private void OnCubeMoved(Cube cube){
        cube.gameObject.SetActive(false);
    }

    private void OnCubeSpawned(Cube cube){
        _cubesMover.AddCube(cube, CubeSpeed.Value, CubeDistance.Value);
    }

    private void UpdateCubesMoverSettings() => _cubesMover.SetSettings(CubeSpeed.Value, CubeDistance.Value);
    private void UpdateCubeSpawnerSettings() => _cubeSpawner.SetSpawnDelay(SpawnDelay.Value);
}

using UnityEngine;

public class CubesPipelineViewModel
{
    public ObservableProperty<float> SpawnDelay { get; set; } = new ObservableProperty<float>();
    public ObservableProperty<float> CubeSpeed { get; set; } = new ObservableProperty<float>();
    public ObservableProperty<float> CubeDistance { get; set; } = new ObservableProperty<float>();
    [Space]
    private CubeSpawner _cubeSpawner;
    private CubesMover _cubesMover;
    private DistanceVisualizer _distanceVisualizer;

    public CubesPipelineViewModel(CubeSpawner cubeSpawner, CubesMover cubesMover, DistanceVisualizer distanceVisualizer) {
        _cubeSpawner = cubeSpawner;
        _cubesMover = cubesMover;
        _distanceVisualizer = distanceVisualizer;

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

    private void UpdateCubesMoverSettings(){
        _cubesMover.SetSettings(CubeSpeed.Value, CubeDistance.Value);
        PipelineSettingsUpdated();
    }

    private void UpdateCubeSpawnerSettings(){
        _cubeSpawner.SetSpawnDelay(SpawnDelay.Value);
        PipelineSettingsUpdated();
    }

    private void PipelineSettingsUpdated(){
        _cubeSpawner.IsActive = CubeSpeed.Value > 0;
        _distanceVisualizer.SetDistance(CubeDistance.Value);
    }
}

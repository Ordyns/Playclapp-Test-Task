using UnityEngine;

public class MainSceneInstaller : MonoBehaviour
{
    // Tried to create an installer similar to Zenject installers.
    // 
    // We can't expose interface in Inspector without custom attribute or Editor,
    // therefore I declared arrays of MonoBehaviour and it's of course not the best solution
    [SerializeField] private MonoBehaviour[] cubesPipelineViewModelUsers;
    [Space]
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private CubesMover cubesMover;
    [Space]
    [SerializeField] private DistanceVisualizer distanceVisualizer;

    private void Awake() {
        CubesPipelineViewModel cubesPipelineViewModel = new CubesPipelineViewModel(cubeSpawner, cubesMover, distanceVisualizer);
        Inject(cubesPipelineViewModel, cubesPipelineViewModelUsers);
    }

    private void Inject<T>(T item, MonoBehaviour[] to){
        foreach (var user in to){
            if(user is IInitializable<T> initializable)
                initializable.Initialize(item);
            else
                Debug.LogError($"{user.name} doesn't implement IInitializable<{item.GetType().Name}> interface");
        }
    }
}

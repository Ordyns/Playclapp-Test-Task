using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PipelineSettingsPanel : MonoBehaviour, IInitializable<CubesPipelineViewModel>
{
    [SerializeField] private TMP_InputField spawnDelayInputField;
    [SerializeField] private TMP_InputField speedInputField;
    [SerializeField] private TMP_InputField distanceInputField;

    public void Initialize(CubesPipelineViewModel cubesPipelineViewModel){
        InitializeInputField(spawnDelayInputField, cubesPipelineViewModel.SpawnDelay);
        InitializeInputField(speedInputField, cubesPipelineViewModel.CubeSpeed);
        InitializeInputField(distanceInputField, cubesPipelineViewModel.CubeDistance);
    }

    private void InitializeInputField(TMP_InputField inputField, ObservableProperty<float> viewModelProperty){
        viewModelProperty.Value = float.Parse(inputField.text);
        inputField.onValueChanged.AddListener((value) => {
            viewModelProperty.Value = float.TryParse(value, out float result) ? result : 0;
        });
    }
}

using UnityEngine;
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
        inputField.onValueChanged.AddListener((value) => OnInputFieldValueChanged(inputField, viewModelProperty));
    }

    private void OnInputFieldValueChanged(TMP_InputField inputField, ObservableProperty<float> viewModelProperty){
        float input = 0;
        if(float.TryParse(inputField.text, out input)){
            if(input < 0){
                input = 0;
                inputField.SetTextWithoutNotify(input.ToString());
            }
        }

        viewModelProperty.Value = input;
    }
}

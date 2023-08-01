using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterDataConfig _propertiesConfig;

    private Button _selectButton;

    private void Awake()
    {
        _selectButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(SelectCharacter);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(SelectCharacter);
    }

    private void SelectCharacter()
    {
        if (_propertiesConfig == null) return;

        PlayerData.SelectCharacter(_propertiesConfig);
    }
}

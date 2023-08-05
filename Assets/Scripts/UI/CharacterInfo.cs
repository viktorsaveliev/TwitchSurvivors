using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text[] _properties;

    private void OnEnable()
    {
        ResetInfo();

        PlayerData.OnCharacterSelected += UpdateInfo;
    }

    private void OnDisable()
    {
        PlayerData.OnCharacterSelected -= UpdateInfo;
    }

    private void UpdateInfo(CharacterDataConfig character)
    {
        _icon.sprite = character.IconLeft;
        _icon.color = Color.white;

        _name.text = character.Name;
        _description.text = character.Description;

        int index = 0;
        foreach (var kvp in character.GetProperties())
        {
            string percents = kvp.Value > 0 ? $"<color=#34DE54>+{kvp.Value}%</color>" : $"<color=#DD3D33>{kvp.Value}%</color>";
            _properties[index].text = $"{percents} {kvp.Key}";
            index++;
        }
    }
    
    private void ResetInfo()
    {
        _icon.sprite = null;
        _name.text = "-";
        _description.text = "-";

        foreach (var propertie in _properties)
        {
            propertie.text = "-";
        }
    }
}

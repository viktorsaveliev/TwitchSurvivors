using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInterface : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    [SerializeField] private Image _expProgress;
    [SerializeField] private TMP_Text _expText;

    public void Init()
    {
        _player.Experience.OnExpValueChanged += UpdateExpUI;
    }

    private void UpdateExpUI()
    {
        _expText.text = $"{_player.Experience.Exp} / {_player.Experience.ExpForNewLevel}";

        float percentage = _player.Experience.Exp / (float)_player.Experience.ExpForNewLevel;
        _expProgress.fillAmount = percentage;
    }
}

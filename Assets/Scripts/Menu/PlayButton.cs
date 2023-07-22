using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    private Button _playButton;

    private void Awake()
    {
        _playButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        if (PlayerData.SelectedCharacter == null)
        {
            print("Надо выбрать типа");
            return;
        }

        SceneLoader scene = new();
        scene.LoadLevel();
    }
}

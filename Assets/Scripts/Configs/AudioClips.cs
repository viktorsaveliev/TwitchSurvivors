using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Clips Config")]
public class AudioClips : ScriptableObject
{
    [SerializeField] private AudioClip _onEnter;
    [SerializeField] private AudioClip _onClick;

    public AudioClip OnEnter => _onEnter;
    public AudioClip OnClick => _onClick;
}

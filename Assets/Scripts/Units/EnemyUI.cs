using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TMP_Text _nickname;

    private void Awake()
    {
        _enemy.OnNicknameChanged += SetNickname;
    }

    private void SetNickname(string nickname)
    {
        _nickname.text = nickname;
    }
}

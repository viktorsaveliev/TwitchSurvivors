using UnityEngine;
using TMPro;

[RequireComponent(typeof(Enemy))]
public class EnemyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nickname;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnNicknameChanged += SetNickname;
    }

    private void SetNickname(string nickname)
    {
        _nickname.text = nickname;
    }
}

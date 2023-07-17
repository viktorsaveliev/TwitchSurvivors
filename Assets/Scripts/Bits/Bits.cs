using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bits : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private int _givesExperience = 2;
    private int _givesMoney = 2;

    public void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateData(Vector2 position, int givesExp, Sprite sprite)
    {
        transform.position = position;
        _givesExperience = _givesMoney = givesExp;
        _sprite.sprite = sprite;

        Show();
    }

    public void Pickup(PlayerUnit player)
    {
        player.Experience.GiveExp(_givesExperience);
        Money.Give(_givesMoney);

        transform.DOScale(0, 0.2f);
        transform.DOMove(player.transform.position, 0.2f).OnComplete(()
            => Hide());
    }

    private void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(1, 0.4f).SetEase(Ease.OutBack);
    }

    private void Hide()
    {
        //transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
            gameObject.SetActive(false);
    }
}

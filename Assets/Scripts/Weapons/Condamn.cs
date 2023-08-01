using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class Condamn : Weapon, IChargesUser
{
    [SerializeField] private TMP_Text _text;

    private Camera _camera;

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetCooldown(35f);
                break;

            case 2:
                SetCooldown(25f);
                break;

            case 3:
                SetCooldown(20f);
                break;

            case 4:
                SetCooldown(15f);
                break;
        }

        UpdatePrice();
    }

    public override void Init()
    {
        Name = "ОСУЖДАЮ";
        BasicPrice = CurrentPrice = 40;

        SetCooldown(45f);

        _camera = Camera.main;
    }

    public bool IsVisibleOnCamera(Transform target)
    {
        if (!target.TryGetComponent<Collider2D>(out var collider)) return false;

        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_camera), collider.bounds);
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (CurrentCooldown > Time.time) return;

        Enemy[] allEnemies = enemyCounter.GetAllEnemies();

        foreach (Enemy enemy in allEnemies)
        {
            if (!enemy.gameObject.activeSelf || !IsVisibleOnCamera(enemy.transform)) continue;

            enemy.Health.TakeDamage(1000);
        }

        StartCoroutine(ShakeAndFadeOutText());
        ActivateCooldown();
    }

    private IEnumerator ShakeAndFadeOutText()
    {
        Sequence shakeSequence = DOTween.Sequence();

        float shakeDuration = 2f;
        float shakeStrength = 2f;
        int shakeVibrato = 10;

        _text.transform.localScale = Vector2.zero;
        _text.gameObject.SetActive(true);
        _text.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        shakeSequence.Append(_text.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato));

        yield return new WaitForSeconds(shakeDuration / 2);

        Vector3 originalScale = _text.transform.localScale;
        _text.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            _text.gameObject.SetActive(false);
        });
    }
}

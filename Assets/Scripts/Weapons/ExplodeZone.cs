using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ExplodeZone : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _progress;
    [SerializeField] private ParticleSystem _explode;

    private SpriteRenderer _zone;
    private readonly List<Unit> _targets = new();

    private float _delayForExplode = 1f;
    private int _damage = 35;

    private bool _isActive;

    public void Init(float delayForExplode, int damage)
    {
        _zone = GetComponent<SpriteRenderer>();

        _delayForExplode = delayForExplode;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            if (_isActive)
            {
                GiveDamage(unit);
            }
            else if (!_targets.Contains(unit))
            {
                _targets.Add(unit);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            if (_targets.Contains(unit))
            {
                _targets.Remove(unit);
            }
        }
    }

    public void Active(Vector2 position)
    {
        _zone.DOFade(0.4f, 0.5f);
        transform.position = position;

        gameObject.SetActive(true);
        _progress.gameObject.SetActive(true);

        _progress.transform.localScale = Vector2.zero;
        _progress.transform.DOScale(1f, _delayForExplode)
            .OnComplete(() => StartCoroutine(Explode()));
    }

    private IEnumerator Explode()
    {
        _isActive = true;

        _explode.Play();
        _progress.gameObject.SetActive(false);

        if (_targets.Count > 0)
        {
            for(int i = 0; i < _targets.Count; i++)
            {
                GiveDamage(_targets[i]);
            }
        }

        yield return new WaitForSeconds(2f);

        _targets.Clear();
        _explode.Stop();
        _isActive = false;

        _zone.DOFade(0, 0.5f).OnComplete(() =>
            gameObject.SetActive(false));
    }

    private void GiveDamage(Unit unit)
    {
        unit.Health.TakeDamage(_damage);
    }
}

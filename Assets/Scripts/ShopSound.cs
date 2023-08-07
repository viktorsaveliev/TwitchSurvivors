using UnityEngine;

public class ShopSound : MonoBehaviour
{
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _openShopSound;

    private void OnEnable()
    {
        _shopUI.OnShopOpened += PlaySound;
    }

    private void OnDisable()
    {
        _shopUI.OnShopOpened -= PlaySound;
    }

    private void PlaySound()
    {
        _audio.clip = _openShopSound;
        _audio.Play();
    }
}

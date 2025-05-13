using System;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Action<GameObject> OnItemBoxCrashed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.ProjectileTag)||collision.CompareTag(Define.PlayerTag))
        {
            OnItemBoxCrashed?.Invoke(gameObject);
            SoundManager.Instance.ItemGainSound.Play();
            gameObject.SetActive(false);
        }
    }
}

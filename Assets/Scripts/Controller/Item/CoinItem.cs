using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            SoundManager.Instance.CoinItemGainSound.Play();

            UI_Play.Instance.CoinCount += 10;
            gameObject.SetActive(false);
        }
    }
}

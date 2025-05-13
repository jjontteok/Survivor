using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource EnemyDeadSound;
    public AudioSource PlayerDeadSound;
    public AudioSource SpikedBallSound;
    public AudioSource BoomerangSound;
    public AudioSource RocketExplosionSound;
    public AudioSource ItemGainSound;
    public AudioSource HealthItemGainSound;
    public AudioSource CoinItemGainSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

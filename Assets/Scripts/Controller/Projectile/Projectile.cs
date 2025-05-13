using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CharacterInfo ProjectileInfo;

    private void Awake()
    {
        Initialize();
    }
    protected virtual CharacterInfo GetProjectileInfo()
    {
        return ProjectileInfo;
    }

    protected virtual void Initialize()
    {

    }
}

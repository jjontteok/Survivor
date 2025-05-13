using UnityEngine;

public interface IDamageable
{
    string Tag { get; set; }
    bool AnyDamage(float damage, GameObject damageCauser, 
        int projectile=(int)Define.EProjectile.SpikedBall);
}

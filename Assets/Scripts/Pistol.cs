using UnityEngine;

public class Pistol : Weapons
{
    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    new void Start()
    {
        base.Start();

        gunDamage = gunItem.gunDamage;
        fireSpeed = gunItem.fireSpeed;
        bulletsPerShot = gunItem.bulletsPerShot;
        magazineSize = gunItem.magazineSize;
        bulletsLeft = magazineSize;
        reloadTime = gunItem.reloadTime;
        bulletSpread = gunItem.bulletSpread;
    }

    new void Update()
    {
        base.Update();
    }
    
}

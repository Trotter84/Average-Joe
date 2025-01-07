using UnityEngine;

[CreateAssetMenu(fileName = "GunItem", menuName = "Scriptable Objects/GunItem")]
public class GunItem : ScriptableObject
{
    public GameObject prefab;
    public string gunName;
    public int gunID;
    public float gunDamage;
    public int magazineSize;
    public int bulletsLeft;
    public float reloadTime;
    public float bulletSpread;

    [System.Serializable]
    public class GunInstance
    {
        public GunItem gunType;
        public string gunName;
        public int gunID;
        public float gunDamage;
        public int magazineSize;
        public int bulletsLeft;
        public float reloadTime;
        public float bulletSpread;

        public GunInstance(GunItem gunItem)
        {
            {
                gunItem.gunName = "Pistol";
                gunItem.gunID = 0;
                gunItem.gunDamage = 1.5f;
                gunItem.magazineSize = 9;
                gunItem.bulletsLeft = gunItem.magazineSize;
                gunItem.reloadTime = 1.5f;
                gunItem.bulletSpread = 0.1f;
            }
            {
                gunItem.gunName = "Assault";
                gunItem.gunID = 1;
                gunItem.gunDamage = 1.0f;
                gunItem.magazineSize = 32;
                gunItem.bulletsLeft = gunItem.magazineSize;
                gunItem.reloadTime = 2.5f;
                gunItem.bulletSpread = 0.2f;
            }
            {
                gunItem.gunName = "Shotgun";
                gunItem.gunID = 2;
                gunItem.gunDamage = 0.5f;
                gunItem.magazineSize = 9;
                gunItem.bulletsLeft = gunItem.magazineSize;
                gunItem.reloadTime = 3.0f;
                gunItem.bulletSpread = 0.5f;
            }
        }

    }
}

        // GunItem Pistol = new GunItem();
        // Pistol.gunName = "Pistol";
        // Pistol.gunID = 0;
        // Pistol.gunDamage = 1.5f;
        // Pistol.magazineSize = 9;
        // Pistol.bulletsLeft = Pistol.magazineSize;
        // Pistol.reloadTime = 1.5f;
        // Pistol.bulletSpread = 0.1f;

        // GunItem Assualt = new GunItem();
        // Assualt.gunName = "Assault";
        // Assualt.gunID = 1;
        // Assualt.gunDamage = 1.0f;
        // Assualt.magazineSize = 32;
        // Assualt.bulletsLeft = Assualt.magazineSize;
        // Assualt.reloadTime = 2.5f;
        // Assualt.bulletSpread = 0.2f;

        // GunItem Shotgun = new GunItem();
        // Shotgun.gunName = "Shotgun";
        // Shotgun.gunID = 2;
        // Shotgun.gunDamage = 0.5f;
        // Shotgun.magazineSize = 5;
        // Shotgun.bulletsLeft= Shotgun.magazineSize;
        // Shotgun.reloadTime = 3.5f;
        // Shotgun.bulletSpread = 0.5f;


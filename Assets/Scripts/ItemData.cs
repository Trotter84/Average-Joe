using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public int magazineSize;
    public float reloadingTime;
    public int bulletDamage;
    public bool allowButtonHold;
    public float fireRate;
    public int bulletsPerShot;
    [TextArea]
    public string itemDescription;
}

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public float reloadTime;
    public int bulletsLeft;

    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        reloadTime = itemData.reloadingTime;
        bulletsLeft = itemData.magazineSize;
    }
}

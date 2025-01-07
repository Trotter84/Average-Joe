using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FixedInventoryItem", menuName = "Scriptable Objects/FixedInventoryItem")]
public class FixedInventoryItem : ScriptableObject
{

    public List<ItemInstance> items = new();

//TODO: Come back here.
        public string itemName;
        public GameObject prefab;
        public int magazineSize;
        public float reloadingTime;
        public int bulletDamage;
        public bool allowButtonHold;
        public float fireRate;
        public int bulletsPerShot;
}

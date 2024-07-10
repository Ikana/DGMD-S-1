using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<GameObject> items = new List<GameObject>();

    public float startX = 2.4f;
    public float startY = -1.15f;
    public float startZ = -1f;

    public float moveSpeed = 1f;
    public float spawnInterval = 2f;
    public float killZoneX = 5f;

    private float spawnTimer = 0f;
    private bool destroyed = false;



    public ItemTypeEnum.ItemType itemType;

    void Start()
    {
        SpawnItem();
    }

    void Update()
    {
        items.RemoveAll(item => item == null);

        foreach (GameObject item in items)
        {
            item.transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (item.transform.position.x > killZoneX)
            {
                if (!destroyed)
                {
                    if (itemType.Equals(ItemTypeEnum.ItemType.Copper))
                        Singleton.instance.copperUnlocked = true;
                    else if (itemType.Equals(ItemTypeEnum.ItemType.Iron))
                        Singleton.instance.ironUnlocked = true;
                    else if (itemType.Equals(ItemTypeEnum.ItemType.Coil))
                        Singleton.instance.coilUnlocked = true;
                    else if (itemType.Equals(ItemTypeEnum.ItemType.CopperPlate))
                        Singleton.instance.copperPlateUnlocked = true;
                    else if (itemType.Equals(ItemTypeEnum.ItemType.IronPlate))
                        Singleton.instance.ironPlateUnlocked = true;
                    else if (itemType.Equals(ItemTypeEnum.ItemType.Circuit))
                        Singleton.instance.circuitUnlocked = true;
                    destroyed = true;
                }

                Destroy(item);
            }
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnItem();
            spawnTimer = 0f;
        }
    }

    public void SpawnItem()
    {
        if (itemType.Equals(ItemTypeEnum.ItemType.CopperPlate) &&
            (!Singleton.instance.copperPlateAssemblerInPlace || !Singleton.instance.copperUnlocked))
            return;

        if (itemType.Equals(ItemTypeEnum.ItemType.IronPlate) &&
            (!Singleton.instance.ironPlateAssemblerInPlace || !Singleton.instance.ironUnlocked))
            return;
        if (itemType.Equals(ItemTypeEnum.ItemType.Coil) &&
            (!Singleton.instance.coilAssemblerInPlace || !Singleton.instance.copperPlateUnlocked))
            return;
        if (itemType.Equals(ItemTypeEnum.ItemType.Circuit) &&
            (!Singleton.instance.circuitAssemblerInPlace || !Singleton.instance.coilUnlocked || !Singleton.instance.ironPlateUnlocked)
            )
            return;

        GameObject item = Instantiate(itemPrefab, transform);
        item.transform.localPosition = new Vector3(startX, startY, startZ);
        items.Add(item);
    }
}

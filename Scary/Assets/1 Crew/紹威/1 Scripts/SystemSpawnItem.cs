using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEI
{
    /// <summary>
    /// 生成物件
    /// </summary>
    public class SystemSpawnItem : MonoBehaviour
    {
        public Transform spawnPoint;
        public float spawnTime = 1.5f;
        public GameObject[] Items;
        public int ItemIndex;
        GameObject temporaryValue;

        private void Start()
        {
            InvokeRepeating("SpawnItems", spawnTime, spawnTime);
        }
        
        void SpawnItems()
        {
            ItemIndex= Random.Range(0, Items.Length);
            temporaryValue = Instantiate(Items[ItemIndex],spawnPoint.position,spawnPoint.rotation);
            temporaryValue.GetComponent<SystemDestroyObj>().objIndex = ItemIndex;
        }

    }
}

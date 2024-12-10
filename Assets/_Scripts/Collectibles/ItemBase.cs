using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
//using MoreMountains.Feedbacks;

namespace Items
{
    public class ItemBase : MonoBehaviour
    {
        public ItemManager itemManager { get; private set; }
        public PlayerController_Astronaut player { get; private set; }
        public MeshRenderer meshRenderer { get; private set; }
        public Collider coll { get; private set; }

        public ItemType itemType;

        public string tagPlayer = "Player";
        public string tagBat = "Bat";

        public float timeToDestroy;
        
        void OnValidate()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
            itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            coll = GetComponent<Collider>();  
        }

        void Awake()
        {
            //feedbacks = GetComponentInChildren<MMF_Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(tagPlayer))
            {
                Collected();
            }

            else if (other.transform.CompareTag(tagBat))
            {
                Consumed();
            }
        }

        protected virtual void Collected()
        {
            itemManager.AddItemByType(itemType);

            meshRenderer.enabled = false;  
            coll.enabled = false;
            Destroy(gameObject, timeToDestroy);
        }

        protected virtual void Consumed()
        {
            meshRenderer.enabled = false;
            Destroy(gameObject, timeToDestroy);
        }
    }
    
}

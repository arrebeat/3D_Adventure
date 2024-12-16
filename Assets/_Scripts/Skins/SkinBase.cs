using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Skins
{
    public class SkinBase : MonoBehaviour
    {
        public PlayerController_Astronaut player { get; private set; }
        public SkinManager skinManager { get; private set; }
        public SkinType skinType;
        public string compareTag = "Player";

        public Vector3 rotationVector = new Vector3(1, .5f, 0);
        public float rotationDuration = .01f;

        void Awake()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
            skinManager = GameObject.Find("SkinManager").GetComponent<SkinManager>();
        }

        void Start()
        {
            gameObject.transform.DORotate(rotationVector, rotationDuration).SetLoops(-1, LoopType.Incremental);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            HideObject();

            var setup = skinManager.GetSetupByType(skinType);
            player.skinSwitcher.SwitchSkin(setup);
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }

}

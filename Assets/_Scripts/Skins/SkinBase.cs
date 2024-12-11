using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public class SkinBase : MonoBehaviour
    {
        public PlayerController_Astronaut player { get; private set; }
        public SkinManager skinManager { get; private set; }
        public SkinType skinType;
        public string compareTag = "Player";

        void Awake()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
            skinManager = GameObject.Find("SkinManager").GetComponent<SkinManager>();
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

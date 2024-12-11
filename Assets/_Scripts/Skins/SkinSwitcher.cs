using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public class SkinSwitcher : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Material material;
        

        [NaughtyAttributes.Button]
        private void SwitchMaterial()
        {
            mesh.material = material;
        }

        public void SwitchSkin(SkinSetup setup)
        {
            mesh.material = setup.material;
        }
    }

}

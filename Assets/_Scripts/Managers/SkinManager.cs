using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public enum SkinType
    {
        REGULAR,
        SPEED,
        SUPER
    }

    public class SkinManager : MonoBehaviour
    {
        public List<SkinSetup> skinSetups;

        public SkinSetup GetSetupByType(SkinType skinType)
        {
            return skinSetups.Find(x => x.skinType == skinType);
        }
    }

    [System.Serializable]
    public class SkinSetup
    {
        public SkinType skinType;
        public Material material;
    }

}

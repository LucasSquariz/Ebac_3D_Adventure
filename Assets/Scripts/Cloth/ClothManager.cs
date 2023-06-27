using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        STRONG
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType type)
        {
            return clothSetups.Find(i => i.clothType == type);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}


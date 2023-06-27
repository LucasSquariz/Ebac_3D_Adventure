using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float speedModifier = 2f;
        public override void Collect()
        {
            base.Collect(); 
            Player.Instance.ChangeSpeed(speedModifier, duration);
        }
    }

}

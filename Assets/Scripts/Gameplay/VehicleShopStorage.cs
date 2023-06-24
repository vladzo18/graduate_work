using System;
using System.Collections.Generic;
using GameState;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "VehicleStorage", menuName = "VehicleStorage", order = 0)]
    public class VehicleShopStorage : ScriptableObject
    {
        public List<VehicleShopStorageDescriptor> VehicleDescriptors;
    }

    [Serializable]
    public class VehicleShopStorageDescriptor
    {
        public VehicleType Type;
        public GameObject ThreeDModel;
        public Sprite Icon;
        public int Price;
        public string Name;
    }
}
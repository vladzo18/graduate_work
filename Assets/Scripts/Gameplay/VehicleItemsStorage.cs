using System;
using System.Collections.Generic;
using GameState;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "VehicleItemsStorage", menuName = "VehicleItemsStorage", order = 0)]
    public class VehicleItemsStorage : ScriptableObject
    {
        public List<VehicleItemDescriptor> VehicleItemDescriptors;
    }

    [Serializable]
    public class VehicleItemDescriptor
    {
        public VehicleType Type;
        public GameObject VehicleEntity;
        public GameObject ItemPrefab;
    }
}
using System;
using System.Collections.Generic;
using GameState;
using UnityEngine;

namespace Menu.MapsShop
{
    [CreateAssetMenu(fileName = "MapsStorage", menuName = "MapsStorage", order = 0)]
    public class MapsStorage : ScriptableObject
    {
        public List<MapsStorageDescriptor> _MapsStorageDescriptors;
    }

    [Serializable]
    public class MapsStorageDescriptor
    {
        public MapType MapType;
        public string Name;
        public Sprite icon;
        public int price;
    }
}
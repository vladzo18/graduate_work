using System;
using System.Collections.Generic;
using AudioSystem;
using Gameplay.EnemyCarSystem;
using GameState;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "MapsGameStorage", menuName = "MapsGameStorage", order = 0)]
    public class MapsGameStorage : ScriptableObject
    {
        public List<MapsGameStorageDescriptor> MapsGameStorageDescriptors;
    }

    [Serializable]
    public class MapsGameStorageDescriptor
    {
        public MapType MapType;
        public List<EnemyCar> EnemyCars;
        public Material RoadMaterial;
        public Material EnviromentMaterial;
        public GameObject EnviromentLayerPrefab;
        public AudioEnum _mapMusic;
    }
}
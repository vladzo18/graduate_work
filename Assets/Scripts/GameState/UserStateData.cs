using System;
using System.Collections.Generic;

namespace GameState
{
    [Serializable]
    public class UserStateData
    {
        public bool IsPlayed;
        public int PointsAmount;
        public VehicleType CurrentVehicleType;
        public MapType CurrentMapType;
        public List<VehicleType> AvalibleVehicleTypes;
        public List<MapType> AvalibleMapTypes;
        public Rating Rating = new Rating();
    }
}
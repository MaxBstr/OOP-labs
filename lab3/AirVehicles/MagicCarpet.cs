using System;

namespace Races.AirVehicles
{
    internal class MagicCarpet : AirTransport
    {
        protected override double Speed { get; } = 10;

        protected override double DistanceReducer { get; } = 1; 

        public override double CalcTime(double distance)
        {
            if (distance < 1000)
                return distance * DistanceReducer / Speed;
            
            if (distance < 5000)
                return distance * (DistanceReducer - 0.03) / Speed;
            
            if (distance < 10000)
                return distance * (DistanceReducer - 0.1) / Speed;

            return distance * (DistanceReducer - 0.05) / Speed;
        }

        public override string ToString()
        {
            return "Alladin`s carpet wins!!!";
        }
    }
}
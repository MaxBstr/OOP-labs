using System;

namespace Races.AirVehicles
{
    internal class MagicBroom : AirTransport
    {
        protected override double Speed { get; } = 20;
        protected override double DistanceReducer { get; } = 0.01; 
        
        public override double CalcTime(double distance)
        {
            if (distance < 1000)
                return distance / Speed;
            
            var calcReduce = (int)distance / 1000;
            return distance * (1 - calcReduce * DistanceReducer) / Speed;
        }

        public override string ToString()
        {
            return "Harry`s broom wins!!!";
        }
    }
}
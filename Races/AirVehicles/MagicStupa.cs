using System;

namespace Races.AirVehicles
{
    internal class MagicStupa : AirTransport
    {
        protected override double Speed { get; } = 8;
        protected override double DistanceReducer { get; } = 0.06; 
        
        public override double CalcTime(double distance)
        {
            return distance * (1.0 - DistanceReducer) / Speed;
        }

        public override string ToString()
        {
            return "Granny`s Yaga stupa wins!!!";
        }
        
    }
}
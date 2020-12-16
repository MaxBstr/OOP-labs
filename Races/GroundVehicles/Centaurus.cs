namespace Races.GroundVehicles
{
    internal class Centaurus : GroundTransport
    {
        protected override double Speed { get; } = 15;
        protected override double RestDuration { get; set; } = 2;
        protected override double RestInterval { get; } = 8;

        public override double CalcTime(double distance)
        {
            if (distance <= Speed * RestInterval) //if distance < distance before rest
                return distance / Speed;

            //continious race
            double time = 0;
            while (distance >= Speed * RestInterval)
            {
                distance -= Speed * RestInterval;
                time += RestInterval + RestDuration; //8 - time before rest, 2 - restDuration 
            }

            if (distance != 0)
                time += distance / Speed;

            return time;
        }

        public override string ToString()
        {
            return "Centaurus Fredi wins!!!";
        }
    }
}

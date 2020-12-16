namespace Races.GroundVehicles
{
    internal class VehicleBoots : GroundTransport
    {
        protected override double Speed { get; } = 6;
        protected override double RestDuration { get; set; } = 10;
        protected override double RestInterval { get; } = 60;

        public override double CalcTime(double distance)
        {
            if (distance <= Speed * RestInterval) //if distance < distance before rest
                return distance / Speed;
            
            //1st stop
            distance -= Speed * RestInterval;
            var time = RestInterval + RestDuration;
            
            //continious race
            while (distance >= Speed * RestInterval)
            {
                distance -= Speed * RestInterval;
                time += RestInterval + RestDuration;
            }

            if (distance != 0)
                time += distance / Speed;

            return time;
        }

        public override string ToString()
        {
            return "Super-puper-mega-crazy boots win!!!";
        }
    }
}
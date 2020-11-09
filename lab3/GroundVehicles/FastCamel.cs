namespace Races.GroundVehicles
{
    internal class FastCamel : GroundTransport
    {
        protected override double Speed { get; } = 40;
        protected override double RestDuration { get; set; } = 5;
        protected override double RestInterval { get; } = 10;

        public override double CalcTime(double distance)
        {
            if (distance <= Speed * RestInterval) //if distance < distance before rest
                return distance / Speed;
            
            //1st stop
            distance -= Speed * RestInterval; 
            var time = RestDuration + RestInterval; //5 - restDuration, 10 - time before Rest

            if (distance <= Speed * RestInterval)
            {
                time += distance / Speed;
                return time;
            }

            //2st stop
            RestDuration = 6.5;
            distance -= Speed * RestInterval;
            time = RestDuration + RestInterval; // 6.5 - restDuration, 10 - time before Rest

            //continious race
            RestDuration = 8;
            while (distance >= Speed * RestInterval)
            {
                distance -= Speed * RestInterval;
                time += RestInterval + RestDuration; //10 - time before Rest, 8 - restDuration
            }
            
            if (distance != 0)
                time += distance / Speed;

            return time;
        }

        public override string ToString()
        {
            return "FastCamel Seryozha wins!!!";
        }
    }
}
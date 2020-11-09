namespace Races.GroundVehicles
{
    internal class BactrianCamel : GroundTransport
        {
            protected override double Speed { get; } = 10;
            protected override double RestDuration { get; set; } = 5;
            protected override double RestInterval { get; } = 30;

            public override double CalcTime(double distance)
            {
                double time = 0;
                if (distance <= Speed * RestInterval) //if distance < distance before rest
                    return distance / Speed;
            
                //1st stop
                distance -= Speed * RestInterval;
                time = RestInterval + RestDuration; //30 - time before Rest, 5 - restDuration
                RestDuration = 8;
            
                //continious race
                while (distance >= Speed * RestInterval)
                {
                    distance -= Speed * RestInterval;
                    time += RestInterval + RestDuration; //30 - time before Rest, 8 - restDuration
                }

                if (distance != 0)
                    time += distance / Speed;

                return time;
            }

            public override string ToString()
            {
                return "Two-heaped camel Mayatin wins!!!";
            }
        }
    }

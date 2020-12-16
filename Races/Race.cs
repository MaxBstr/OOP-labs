using System;
using System.Collections.Generic;

namespace Races
{
    internal class Race <T> where T : Transport
    {
        //air, ground, all
        private readonly double _raceDistance;
        private readonly List<T> _vehicles = new List<T>();

        public Race(double raceDistance)
        {
            this._raceDistance = raceDistance;
        }

        public void RegisterVehicle(params T[] vehicles)
        {
            if (vehicles.Length == 0)
                throw new Exception("No vehicles were found!");
            
            foreach (var veh in vehicles)
            {
                _vehicles.Add(veh);
            }
        }

        public Transport StartRace()
        {
            if (_vehicles.Count == 0)
                throw new Exception("You can`t start race without racers!");
            
            var resultTime = double.MaxValue;
            Transport winner = null;

            foreach (var vehicle in _vehicles)
            {
                var time = vehicle.CalcTime(_raceDistance);
                if (time >= resultTime) continue;
                
                resultTime = time;
                winner = vehicle;
            }

            return winner;
        }

    }
}
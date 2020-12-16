using System;
using Races.AirVehicles;
using Races.GroundVehicles;

namespace Races
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //===========================================
            //================AIR FIGHT==================
            //===========================================
            var airRace = new Race<AirTransport>(16120);
            airRace.RegisterVehicle
                (
                new MagicBroom(), 
                new MagicCarpet(), 
                new MagicStupa()
                );

            var airWinner = airRace.StartRace();
            Console.WriteLine(airWinner);
            
            //===========================================
            //===============GROUND FIGHT================
            //===========================================
            var groundRace = new Race<GroundTransport>(25671);
            groundRace.RegisterVehicle
                (
                new Centaurus(),
                new BactrianCamel(),
                new FastCamel(),
                new VehicleBoots()
                );


            var groundWinner = groundRace.StartRace();
            Console.WriteLine(groundWinner);

            //===========================================
            //================MIXED FIGHT================
            //===========================================
            var mixedRace = new Race<Transport>(43560);
            mixedRace.RegisterVehicle
            (
                new Centaurus(),
                new BactrianCamel(),
                new FastCamel(),
                new VehicleBoots(),

                new MagicBroom(),
                new MagicCarpet(),
                new MagicStupa()
            );

                var mixedWinner = mixedRace.StartRace();
            Console.WriteLine(mixedWinner);
        }
    }
}
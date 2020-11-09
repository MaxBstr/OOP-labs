namespace Races
{
    internal abstract class Transport
    {
        protected abstract double Speed { get; }
        public abstract double CalcTime(double distance);

    }

    internal abstract class GroundTransport : Transport
    {
        protected abstract double RestInterval { get; }
        protected abstract double RestDuration { get; set; }
    }
    internal abstract class AirTransport : Transport
    {
        protected abstract double DistanceReducer { get; }
    }
    
}
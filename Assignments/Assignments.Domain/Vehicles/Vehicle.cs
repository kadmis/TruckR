using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Vehicles
{
    public class Vehicle : IEntity<Guid>
    {
        public Guid Id { get; private set; }

        private string _manufacturer;

        private string _model;

        private double _load;

        private double _fuelConsumption;

        private string _fuelType;

        private Vehicle()
        {

        }
        public static Vehicle Create(string manufacturer, string model, double load, double fuelConsumption, string fuelType)
        {
            return new Vehicle
            {
                Id = Guid.NewGuid(),
                _manufacturer = manufacturer,
                _model = model,
                _load = load
            };
        }
    }
}

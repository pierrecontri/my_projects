using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing.CarModelizing
{
    class CostKm
    {
        protected double _km;
        protected double _price;

        public double km
        {
            get { return this._km; }
            set { this._km = value; }
        }

        public double price
        {
            get { return this._price; }
            set { this._price = value; }
        }

        public CostKm()
        {
        }

        public CostKm(double kilometers, double price)
        {
            this.km = kilometers;
            this.price = price;
        }
    }
}

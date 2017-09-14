using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing.CarModelizing
{
    class Wearpart : XmlExp
    {
        private string _name;
        private double _price = 0.0;
        private double _periodicity = 0.0;

        public Wearpart(string name)
        {
            this._name = name;
        }

        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public double price
        {
            get { return this._price; }
            set
            {
                try
                {
                    this._price = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._price);
                }
            }
        }

        public double periodicity
        {
            get { return this._periodicity; }
            set
            {
                try
                {
                    this._periodicity = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._periodicity);
                }
            }
        }

        public string getInfos()
        {
            return this.name + " " + this.price.ToString() + " " + this.periodicity.ToString();
        }

        public override string ToString()
        {
            return this.name;
        }

    }
}

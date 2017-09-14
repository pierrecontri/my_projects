using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing
{
    class Fuel : ObjectModelized
    {
        private double _price = 0.0;

        public Fuel()
        {
        }

        public Fuel(string name)
        {
            this.name = name;
        }

        public Fuel(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public override String getInfos()
        {
            return this.name + " " + this._price.ToString();
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

        public static new Dictionary<String, ObjectModelized> ListObjectModelized
        {
            get
            {
                return ObjectModelized.getListObjectModelizedByType("Fuel");
            }
        }
    }
}

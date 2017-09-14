using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing
{
    class Fuel : ObjectModelized
    {
        private float _price = 0.0F;

        public Fuel()
        {
        }

        public Fuel(string name)
        {
            this.name = name;
        }

        public Fuel(string name, float price)
        {
            this.name = name;
            this.price = price;
        }

        public override String getInfos()
        {
            return this.name + " " + this._price.ToString();
        }

        public float price
        {
            get { return this._price; }
            set
            {
                    try
                    {
                        this._price = (float)value;
                    }
                    catch (Exception /*ex*/)
                    {
                        float.TryParse(value.ToString(), out this._price);
                    }
            }
        }
    }
}

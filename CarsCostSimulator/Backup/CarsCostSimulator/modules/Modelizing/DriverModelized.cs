using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing
{
    class Driver : ObjectModelized
    {
        private float _kmperyear = 0.0F;
        private float _maxkilometers = 0.0F;
        private string _drivertype = string.Empty;

        public Driver(string name)
        {
            this.name = name;
        }

        public Drivertype drivertype
        {
            get
            {
                if (Drivertype.arrayObj.ContainsKey(this._drivertype))
                    return (Drivertype)Drivertype.arrayObj[this._drivertype];
                else
                    return new Drivertype(this._drivertype);
            }

            set
            {
                this._drivertype = value.ToString();
            }
        }

        public float kmperyear
        {
            get
            {
                return this._kmperyear;
            }
            set
            {
                try
                {
                    this._kmperyear = (float)value;
                }
                catch (Exception /*ex*/)
                {
                    float.TryParse(value.ToString(), out this._kmperyear);
                }
            }
        }

        public float maxkilometers
        {
            get
            {
                return this._maxkilometers;
            }

            set
            {
                try
                {
                    this._maxkilometers = (float)value;
                }
                catch (Exception /*ex*/)
                {
                    float.TryParse(value.ToString(), out this._maxkilometers);
                }
            }
        }
    }

    class Drivertype : ObjectModelized
    {
        private float _drivingcoefficient = 1.0F;
        public float drivingcoefficient
        {
            get { return this._drivingcoefficient; }
            set
            {
                try
                {
                    this._drivingcoefficient = (float)value;
                }
                catch (Exception /*ex*/)
                {
                    float.TryParse(value.ToString(), out this._drivingcoefficient);
                }
            }
        }

        public Drivertype(string name)
        {
            this.name = name;
        }
    }
}

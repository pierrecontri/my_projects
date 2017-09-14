using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing
{
    class Driver : ObjectModelized
    {
        private double _kmperyear = 0.0;
        private double _maxkilometers = 0.0;
        private string _drivertype = string.Empty;


        public Driver()
        {
            this.name = "defaultDriver";
        }

        public Driver(string name)
        {
            this.name = name;
        }

        public Drivertype drivertype
        {
            get
            {
                if (Drivertype.arrayObj.ContainsKey(this._drivertype))
                    return (Drivertype)Drivertype.ListObjectModelized[this._drivertype];
                else
                    return new Drivertype(this._drivertype);
            }

            set
            {
                this._drivertype = value.ToString();
            }
        }

        public double kmperyear
        {
            get
            {
                return this._kmperyear;
            }
            set
            {
                try
                {
                    this._kmperyear = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._kmperyear);
                }
            }
        }

        public double maxkilometers
        {
            get
            {
                return this._maxkilometers;
            }

            set
            {
                try
                {
                    this._maxkilometers = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._maxkilometers);
                }
            }
        }

        public static new Dictionary<String, ObjectModelized> ListObjectModelized
        {
            get
            {
                return ObjectModelized.getListObjectModelizedByType("Driver");
            }
        }

    }

    class Drivertype : ObjectModelized
    {
        private double _drivingcoefficient = 1.0;
        public double drivingcoefficient
        {
            get { return this._drivingcoefficient; }
            set
            {
                try
                {
                    this._drivingcoefficient = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._drivingcoefficient);
                }
            }
        }

        public Drivertype()
        {
            this.name = "builder";
        }

        public Drivertype(string name)
        {
            this.name = name;
        }

        public static new Dictionary<String, ObjectModelized> ListObjectModelized
        {
            get
            {
                return ObjectModelized.getListObjectModelizedByType("Drivertype");
            }
        }
    }
}

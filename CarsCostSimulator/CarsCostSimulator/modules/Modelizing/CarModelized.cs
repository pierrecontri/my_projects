using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsCostSimulator.modules;
using CarsCostSimulator.modules.Modelizing.CarModelizing;

namespace CarsCostSimulator.modules.Modelizing
{
     class Car : ObjectModelized
    {
        private static double _maxkilometers = 301000.0;
        private static double _espacementKm = 1000.0;
        private static double _nbKmParAnnee = 47000.0;
        //private static int _maxSizeLengthName = 1;
        private static Dictionary<string, int> ids = new Dictionary<string, int>();

        private double _price = 0.0;
        private List<CostKm> _costs = new List<CostKm>();
        private List<Wearpart> _wearparts = new List<Wearpart>();
        private Fuel _fuel = null;
        private Driver _driver = null;
        private Drivertype _drivertype = null;
        private double _tanksize = 0.0;
        private double _insuranceprice = 0.0;
        private double _consumption = 0.0;

        public List<Wearpart> Wearparts
        {
            get { return this._wearparts; }
        }

        public Car()
        {
        }

        public Car(string name)
        {
            this.name = name;
        }

        public Fuel fuel
        {
            get
            {
                if (this._fuel == null)
                    return new Fuel("N/A");
                return _fuel;
            }
            set
            {
                this._fuel = value;
            }
        }

        public double price
        {
            get
            {
                return this._price;
            }
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

        public double consumption
        {
            get
            {
                return this._consumption;
            }
            set
            {
                try
                {
                    this._consumption = (double)value;
                }
                catch (Exception /*ex*/)
                {
                    double.TryParse(value.ToString(), out this._consumption);
                }
            }
        }

        public Driver driver
        {
            get { return this._driver; }
            set
            {
                this._driver = value;
            }
        }

        public Drivertype drivertype
        {
            get { return this._drivertype; }
            set { this._drivertype = value; }
        }

        public double tanksize
        {
            get { return this._tanksize; }
            set { this._tanksize = value; }
        }

        public double insuranceprice
        {
            get { return this._insuranceprice; }
            set { this._insuranceprice = value; }
        }

        public List<CostKm> Costs
        {
            get
            {
                int drvId = (this.driver != null) ? this.driver.GetHashCode() : -1;
                int tmpId = this.GetHashCode() + this.fuel.GetHashCode() + drvId;
                if (Car.ids.ContainsKey(this.name) && Car.ids[this.name] != tmpId)
                    this._costs.Clear();
                if (this._costs.Count == 0)
                {
                    this.calculCouts();
                    Car.ids[this.name] = tmpId;
                }

                return this._costs;
            }
        }

        public void calculCouts()
        {
            this._costs.Clear();
            List<CostKm> tmpCalc = this._costs;
            CostKm firstCost = new CostKm();
            firstCost.km = 0.0;
            firstCost.price = this._price;
            tmpCalc.Add(firstCost);

            double drivingcoef = 1.0;
            // get driving coeeficient if it's possible
            try
            {
                if (this.driver != null && this.driver.drivertype != null)
                    drivingcoef = this.driver.drivertype.drivingcoefficient;
            }
            catch (Exception /* nullExp */) { }

            while ((tmpCalc.LastOrDefault().km + _espacementKm) < Car._maxkilometers)
            {
                // get previous cost and include it into new data
                CostKm nextCostKm = new CostKm(tmpCalc.LastOrDefault().km + _espacementKm,tmpCalc.LastOrDefault().price);
                // cout par kilometres
                if (this.fuel is Fuel)
                    nextCostKm.price += nextCostKm.km * this.consumption / 100.0 * this.fuel.price * drivingcoef;
                // calcul des prix des pieces d'usure (entretien, couroie, ...)
                foreach (Wearpart pceUsure in this.Wearparts)
                {
                    if (pceUsure.periodicity != 0 && (nextCostKm.km % pceUsure.periodicity) == 0.0)
                        nextCostKm.price += pceUsure.price * drivingcoef;
                }
                // cout par annee assurance
                if (((nextCostKm.km + Car._nbKmParAnnee) % Car._nbKmParAnnee) == 0)
                    nextCostKm.price += this.insuranceprice;

                // ajout cout global
                tmpCalc.Add(nextCostKm);
            }
        }

        public override string getInfos()
        {
            return (this.ToString() + ";" + this.price.ToString() + ";" + this.consumption.ToString() + ";" + this.fuel.getInfos());
        }

        public static new Dictionary<String, ObjectModelized> ListObjectModelized
        {
            get
            {
                return ObjectModelized.getListObjectModelizedByType("Car");
            }
        }
    }

}

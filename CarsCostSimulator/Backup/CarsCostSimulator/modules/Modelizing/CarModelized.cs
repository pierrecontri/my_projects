using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsCostSimulator.modules.Modelizing
{
    class Car : ObjectModelized
    {
        private static float _maxkilometers = 301000F;
        private static float _espacementKm = 1000F;
        private static float _nbKmParAnnee = 47000F;
        private static int _maxSizeLengthName = 1;
        private static Dictionary<string, int> ids = new Dictionary<string, int>();

        private float _price = 0.0F;
        private List<float> _costs = new List<float>();
        private List<Wearpart> _wearparts = new List<Wearpart>();
        private Fuel _fuel = null;
        private Driver _driver = null;
        private Drivertype _drivertype = null;
        private float _tanksize = 0.0F;
        private float _insuranceprice = 0.0F;
        private float _consumption = 0.0F;

        public List<Wearpart> Wearparts
        {
            get { return this._wearparts; }
        }

        public Car(string name)
        {
            this.name = name;
            this.Wearparts.Add(new Wearpart("tty"));
            this.Wearparts.Add(new Wearpart("tty2"));
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

        public float price
        {
            get
            {
                return this._price;
            }
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

        public float consumption
        {
            get
            {
                return this._consumption;
            }
            set
            {
                try
                {
                    this._consumption = (float)value;
                }
                catch (Exception /*ex*/)
                {
                    float.TryParse(value.ToString(), out this._consumption);
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

        public float tanksize
        {
            get { return this._tanksize; }
            set { this._tanksize = value; }
        }

        public float insuranceprice
        {
            get { return this._insuranceprice; }
            set { this._insuranceprice = value; }
        }

        public List<float> Costs
        {
            get
            {
                int tmpId = this.GetHashCode() + this.fuel.GetHashCode() + this.driver.GetHashCode();
                if (Car.ids[this.name] != tmpId)
                    this._costs.Clear();
                if (this._costs.Count == 0)
                {
                    this._costs = this.calculCouts();
                    Car.ids[this.name] = tmpId;
                }

                return this._costs;
            }
        }

        public List<float> calculCouts()
        {
            List<float> tmpCalc = new List<float>();
            tmpCalc.Add(this.price);
            float kmParcourus = Car._espacementKm;
            float drivingcoef = 1.0F;
            // get driving coeeficient if it's possible
            try
            {
                drivingcoef = this.driver.drivertype.drivingcoefficient;
            }
            catch (Exception /* nullExp */) { }

            while (kmParcourus < Car._maxkilometers)
            {
                // get previous cost
                float coutActuel = tmpCalc[-1];
                // cout par kilometres
                if (this.fuel is Fuel)
                    coutActuel = coutActuel + kmParcourus * this.consumption / 100.0F * this.fuel.price * drivingcoef;
                // calcul des prix des pieces d'usure (entretien, couroie, ...)
                foreach (Wearpart pceUsure in this.Wearparts)
                {
                    if (pceUsure.periodicity != 0 && (kmParcourus % pceUsure.periodicity) == 0.0F)
                        coutActuel += pceUsure.price * drivingcoef;
                }
                // cout par annee assurance
                if (((kmParcourus + Car._nbKmParAnnee) % Car._nbKmParAnnee) == 0)
                    coutActuel += this.insuranceprice;
                // ajout cout global
                tmpCalc.Add(coutActuel);
                // !!! increment !!!
                kmParcourus += kmParcourus;
            }

            return new List<float>();
        }

        public override string getInfos()
        {
            return (this.ToString() + ";" + this.price.ToString() + ";" + this.consumption.ToString() + ";" + this.fuel.getInfos());
        }
    }

    class Wearpart : XmlExp
    {
        private string _name;
        private float _price = 0.0F;
        private float _periodicity = 0.0F;

        public Wearpart(string name)
        {
            this._name = name;
        }

        public string name
        {
            get { return this._name; }
            set { this._name = value; }
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

        public float periodicity
        {
            get { return this._periodicity; }
            set
            {
                try
                {
                    this._periodicity = (float)value;
                }
                catch (Exception /*ex*/)
                {
                    float.TryParse(value.ToString(), out this._periodicity);
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

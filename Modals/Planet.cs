using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GalaxyCatalog.Modals
{
    
    public class Planet: ICountList
    {
        // fields
        private string planetName;
        private string planetType; 
        private bool life;
        List<Moon> Moons;
        static int instances = 0;

        //constructors

        public Planet(string planetName, string planetType, bool life) {

            if (PlanetTypeCheck(planetType) != "none")
            {
                this.planetName = planetName;
                this.planetType = PlanetTypeCheck(planetType);
                this.life = life;

                this.Moons = new List<Moon>();

                instances++;
            }
            else
            {
                Console.WriteLine("Грешка във входните данни! Планетата не е въведена.");
            }
        }

        //properties
        public string PlanetName { get { return this.planetName;  } }

        //methods
        private string PlanetTypeCheck(string pt) {

            string[] planetTypesList = { "terrestrial", "giant planet", "ice giant", "mesoplanet", "mini-neptune", "planetar", "super-earth", "super-jupiter", "sub-earth" };        
            
            if (  Array.Exists(planetTypesList, ele => ele == pt) )
            {
                return pt;
            }
            else
            {
                Console.WriteLine($"Въведената стойност за <planet type> НЕ отговаря на изискванията! \n" +
                    $" Моля въведете отново като посочите някой от следните: "+String.Join(",", planetTypesList));
                return "none";
            }            
        }

        public void addMoon(Moon m)
        {
            this.Moons.Add(m);

            //sorting after Add
            //this.Moons = this.Moons.OrderBy(m => m.MoonName).ToList();
        }

        public static int GetActiveInstances()
        {
            return instances;
        }

        int ICountList.GetActiveInstances()
        {
            return Planet.GetActiveInstances();
        }


        public string GetInnerList(string view)
        {
            StringBuilder result = new StringBuilder();

            if (this.Moons.Count > 0)
            {
                switch (view)
                {
                    case "commas":

                        result.Append($"{ String.Join(", ", this.Moons) }");

                        break;

                    case "full":
                        //result.Append("\n");
                        foreach (Moon m in this.Moons)
                        {
                            result.Append($"\n           \u25a0 {m.ToString() }");
                        }
                        break;
                }
            }
            else
            {
                if (view == "full") { result.Append("none"); }
            }

            return result.ToString();
        }


        public override string ToString()
        {
            StringBuilder result = new StringBuilder(); 
            result.Append($"      \u25cb Name: {planetName} \n");
            result.Append($"        Type: {planetType} \n");
            result.Append($"        Support life: {(life ? "yes" : "no")} \n");
            result.Append($"        Moons: ");
            result.Append($"{ this.GetInnerList("full") }");

            return result.ToString();
        }


    } // class end
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GalaxyCatalog.Modals;

namespace GalaxyCatalog
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string commandToDo, gName, sName, planetName, moonName;
            int commandLength;
            string[] comandVals;

            char[] separators = new char[] { '[', ']' };

            //sazdavame kataloga
            GalaxyBook galaxyBook = new GalaxyBook();

           
            //Console.WriteLine("\u25a0 \t \u25a0");

            do
            {
                //read commands
                commandToDo = Console.ReadLine();

                if (String.IsNullOrEmpty(commandToDo))
                { break; }
                else
                {
                    //break to words
                    string[] commandWords = commandToDo.Split(' ');
                    commandLength = commandWords.Length;

                    /*foreach (var word in words)
                    {
                        Console.WriteLine($"{word},");
                    } */


                    // spored daljinata na komandata
                    switch (commandLength)
                    {
                        case 1:
                            // stat and exit
                            switch (commandWords[0])
                            {
                                case "stats":                                   
                                    galaxyBook.printStat();                                    
                                    break;                              
                                
                                case "exit":
                                    Console.WriteLine("\n");
                                    break;

                                default:
                                    Console.WriteLine("Unknown Command");
                                    break; //exit
                            }
                            break;

                        default:
                            //
                            
                            switch (commandWords[0])
                            {
                                case "add":

                                    switch (commandWords[1])
                                    {
                                        
                                        // ---------------------------- START ADD Galaxy ----------------------------//
                                        case "galaxy":
                                            comandVals = commandToDo.Split(']')[1].Trim(' ').Split(' ');

                                            string gt = commandToDo.Split('[', ']')[2].Split(' ')[1].Trim();
                                            GalaxyTypes gTobj = (GalaxyTypes) Enum.Parse(typeof(GalaxyTypes), gt);
                                            float age = float.Parse(comandVals[1].Substring(0, comandVals[1].Length - 1), CultureInfo.InvariantCulture);
                                            char ageS = char.Parse(comandVals[1].Substring(comandVals[1].Length - 1));
                                            gName = commandToDo.Split('[', ']')[1].Trim();
                                            //Console.WriteLine(gt);

                                            Galaxy g = new Galaxy(gName, gTobj, age, ageS);
                                            //Console.WriteLine(g);

                                            // add Galaxy
                                            galaxyBook.Galaxies.Add(g);

                                            break;
                                        // ---------------------------- END ADD Galaxy ----------------------------//

                                        // ---------------------------- START ADD STAR ----------------------------//
                                        case "star":
                                            comandVals = commandToDo.Split('[', ']');
                                            gName = commandToDo.Split('[', ']')[1];
                                            sName = commandToDo.Split('[', ']')[3];
                                            string[] sParm = commandToDo.Split('[', ']')[4].Trim().Split(" ");

                                            float mass = float.Parse(sParm[0], CultureInfo.InvariantCulture);
                                            float size = float.Parse(sParm[1], CultureInfo.InvariantCulture);
                                            int temp = int.Parse(sParm[2]);
                                            float luminosity = float.Parse(sParm[3], CultureInfo.InvariantCulture);

                                           Star currStar = new Star(sName, mass, size , temp, luminosity);

                                          //  Console.WriteLine(currStar.ToString());

                                           // foreach (string s in sParm)
                                           // { Console.WriteLine( s ); }
                                           // Console.WriteLine($"-{mass}-");

                                               if (galaxyBook.Galaxies.Any(g => g.GalaxyName == gName))
                                               {
                                                   galaxyBook.Galaxies.Find(g => g.GalaxyName == gName).addStar(currStar);
                                               }
                                               else
                                               {
                                                   galaxyBook.Galaxies.Add(new Galaxy(gName, new List<Star>() { currStar } ));
                                               } 


                                            break;
                                        // ---------------------------- END ADD STAR ----------------------------//

                                        // ---------------------------- START add PLANET ----------------------------//
                                        case "planet":
                                            comandVals = commandToDo.Split('[',']');
                                          
                                            sName = comandVals[1];
                                            planetName = comandVals[3].Trim();
                                            string[] planetParam = comandVals[4].Trim().Split(' ');
                                            //<support life>
                                            bool life = planetParam.Last() == "yes" ? true : false;
                                            //vzima <planet type>
                                            string pt = String.Join(" ", planetParam.SkipLast(1));

                                            //foreach (string s in planetParam){ Console.WriteLine( $"-{s}-" ); }
                                            // Console.WriteLine(life);

                                            Planet currPlanet = new Planet(planetName, pt, life);

                                            //Console.WriteLine(currPlanet.ToString());


                                            if (galaxyBook.Galaxies.Any(g => g.Stars.Any(s => s.StarName == sName) ) ) 
                                            {                                          
                                                foreach (Galaxy glx in galaxyBook.Galaxies)
                                                {
                                                    if ( glx.Stars.Any(s => s.StarName == sName))
                                                    {
                                                        glx.Stars.Find(s => s.StarName == sName).addPlanet(currPlanet);
                                                    }
                                                    
                                                }
                                            }
                                            else {
                                                Console.WriteLine($" Трябва да добавите Звездата първо към съответната галактика! ");  
                                            }
                                             
                                            break;
                                        // ---------------------------- END add PLANET ----------------------------//

                                        // ---------------------------- START add MOON ----------------------------//
                                        case "moon":
                                            comandVals = commandToDo.Split('[', ']');
                                            planetName = comandVals[1].Trim();
                                            moonName = comandVals[3].Trim();

                                            Moon currMoon = new Moon(moonName);

                                            // foreach (string s in comandVals) { Console.WriteLine( $"-{s}-" ); }

                                            if (galaxyBook.Galaxies.Any(g => g.Stars.Any(s => s.Planets.Any( p => p.PlanetName == planetName ))))
                                            {
                                                foreach (Galaxy glx in galaxyBook.Galaxies)
                                                {
                                                    foreach (Star s in glx.Stars)
                                                    {
                                                        if (s.Planets.Any(x => x.PlanetName == planetName) )
                                                        { s.Planets.Find(p => p.PlanetName == planetName).addMoon(currMoon);  }
                                                        
                                                     }
                                                }
                                            }
                                            else {
                                                Console.WriteLine("Необходимо е да добавите Планета, звезда към Галактиката първо");
                                            }

                                                break;
                                        // ---------------------------- END add MOON ----------------------------//
                                    }
                                    break;

                                // ---------------------------- START LIST ----------------------------//
                                case "list": 
                                    switch (commandWords[1])
                                    {
                                        case "galaxies":
                                            galaxyBook.printGalaxies();
                                            break;

                                        case "stars":
                                            galaxyBook.printStars();

                                            break;

                                        case "planets":
                                            galaxyBook.printPlanets();
                                            break;

                                        case "moons":
                                            galaxyBook.printMoons();
                                            break;
                                    }
                                    break;
                                // ---------------------------- END LIST ----------------------------//

                                // ---------------------------- START PRINT ----------------------------//
                                case "print":

                                    gName = commandToDo.Split('[', ']')[1];

                                    if (galaxyBook.Galaxies.Any(g => g.GalaxyName == gName))
                                    {
                                        Console.WriteLine( galaxyBook.Galaxies.Find(g => g.GalaxyName == gName).ToString() );
                                    }
                                    else
                                    { Console.WriteLine($"Няма въведена галактика: {gName}"); }

                                    break;
                                // ---------------------------- END PRINT ----------------------------//

                                default:
                                    Console.WriteLine("Unknown Command");
                                    break;

                            } 
                            break;                       
                    }
                }
            }
            while ( !commandToDo.Equals("exit"));           
        
            
        
        
        } //end Main
    } // end class
}

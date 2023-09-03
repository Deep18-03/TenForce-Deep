using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        public double AverageMoonGravity { get; set; }

        public Planet(PlanetDto planetDto)
        {
            try
            {
                Id = planetDto.Id;
                SemiMajorAxis = planetDto.SemiMajorAxis;
                Moons = new Collection<Moon>();
                if (planetDto.Moons != null)
                {
                    //Declare variable to do calculate moon gravity 
                    double MoonGravity = 0;
                    double TotalMoonGravity = 0;
                    int moonCount = 0;

                    foreach (MoonDto moonDto in planetDto.Moons)
                    {
                        Moons.Add(new Moon(moonDto));
                        var moonMass = moonDto.MassValue * Math.Pow(10, moonDto.MassExponent);
                        //Get MoonGravity and Add all moons gravity of same planet
                        if (moonDto.MeanRadius != 0 && moonDto.Gravity != 0)
                        {
                            MoonGravity = (moonDto.Gravity * moonMass) / moonDto.MeanRadius;
                            TotalMoonGravity = TotalMoonGravity + MoonGravity;
                        }
                        moonCount++;
                    }
                    //Calculate AverageMoonGravity
                    AverageMoonGravity = TotalMoonGravity / moonCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}

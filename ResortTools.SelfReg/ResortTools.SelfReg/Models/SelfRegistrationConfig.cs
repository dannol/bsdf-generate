using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortTools.SelfReg
{
    // These classes must match the JSON structre in appsettings.json as it is filled from this file
    // in Startup.cs 
    public class Step
    {
        public String Name { get; set; }
        public String RouteName { get; set; }
        public String Route { get; set; }
        public int StepNumber { get; set; }
        public Boolean DisplayProgress { get; set; }
        public Boolean NextStepOnComplete { get; set; }

        public Boolean StepComplete { get; set; }

    }

    //Parent class for all location configurations
    public class LocationConfig
    {
        public string LocationName { get; set; }
        public Boolean PrintOnComplete { get; set; }

        //Registration steps
        public Step[] Steps { get; set; }
    }

    //Configuration for Pass Office
    public class PassOfficeConfig : LocationConfig
    {
        //Pass Office specific configurations go here
    }

    //Configuration for Rental Office
    public class RentalOfficeConfig : LocationConfig
    {
        //Rental Offce specific configurations go here
    }

    //Configuration for Ski School
    public class SkiSchoolConfig : LocationConfig
    {
        //Ski School specific configurations go here
    }

    //Overall Configuration for the client
    public class SelfRegistrationConfig
    {
        public PassOfficeConfig PassOfficeConfig { get; set; }
        public RentalOfficeConfig RentalOfficeConfig { get; set; }
        public SkiSchoolConfig SkiSchoolConfig { get; set; }

    }
}

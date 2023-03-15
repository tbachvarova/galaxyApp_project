using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyCatalog.Modals
{
    public class Moon: ICountList
    {
        // fields
        private string moonName;
        static int instances = 0;

        //constructors
        public Moon(string moonName)
        {
            this.moonName = moonName;

            instances++;
        }

        //properties
        public string MoonName {
            get { return this.moonName;  }
        }

        //methods
        public static int GetActiveInstances()
        {
            return instances;
        }
        int ICountList.GetActiveInstances()
        {
            return Moon.GetActiveInstances();
        }

        public string GetInnerList(string view)
        {
            return "";
        }


        public override string ToString()
        {
            return this.moonName;
        }

    }
}

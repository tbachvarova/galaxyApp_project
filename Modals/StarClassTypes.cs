using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyCatalog.Modals
{
    public class StarClassTypes
    {

        private StarClassTypeDef[] listStarClasses = new StarClassTypeDef[] {
            new StarClassTypeDef('M', 2400, 3700, 0, 0.08f, 0.08f, 0.45f, 0, 0.7f),
            new StarClassTypeDef('K', 3700, 5200, 0.08f, 0.6f, 0.45f, 0.8f, 0.7f, 0.96f),
            new StarClassTypeDef('G', 5200, 6000, 0.6f, 1.5f, 0.8f, 1.04f, 0.96f, 1.15f),
            new StarClassTypeDef('F', 6000, 7500, 1.5f, 5f, 1.04f, 1.4f, 1.15f, 1.4f),
            new StarClassTypeDef('A', 7500, 10000, 5f, 25f, 1.4f, 2.1f, 1.4f, 1.8f),
            new StarClassTypeDef('B', 10000, 30000, 25f, 30000f, 2.1f, 16f, 1.8f, 6.6f),
            new StarClassTypeDef('O', 30000, int.MaxValue, 30000f, float.MaxValue, 16f, float.MaxValue, 6.6f, float.MaxValue),

        };

        // calc Star class
        public char getStarClass(int k, float l, float m, float r)
        {
            for (int i = 0; i < listStarClasses.Length; i++)
            {
                if ( (k >= listStarClasses[i].KMin && k < listStarClasses[i].KMax)
                    && (l >= listStarClasses[i].LMin && l < listStarClasses[i].LMax)
                    && (m >= listStarClasses[i].MMin && m < listStarClasses[i].MMax)
                    && (r >= listStarClasses[i].RMin && r < listStarClasses[i].RMax)
                    )
                {
                    return listStarClasses[i].TypeName;
                }                
            }

            return '-';
        }
        




    }
}

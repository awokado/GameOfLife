using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGameOfLife_1
{
    /*This class will handle al params for populating methods*/
    public class PopulatingParams
    {
        //**********************************MEMBERS**********************************//
        public int aliveProbability { get; private set; } = 5;         //def probab of beeing alive =)


        //**********************************METHODS**********************************//
        /// <summary>
        /// Default constructor
        /// </summary>
        public PopulatingParams()
        {
            LogConsole.WriteLine("PopulatingParams constructor");
            aliveProbability = 50;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aliveProbability">Probability of single cell beeing alive</param>
        public PopulatingParams(int aliveProbability) : this()
        {
            this.aliveProbability = aliveProbability;
        }
    }
}

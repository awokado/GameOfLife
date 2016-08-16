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

        public int aliveProbability { get; private set; } = 50;         //probab of beeing alive =)

        public PopulatingParams()
        {
            LogConsole.WriteLine("PopulatingParams constructor");
            aliveProbability = 50;
        }

        /*test of calling constructor*/
        public PopulatingParams(int aliveProbability) : this()
        {
            this.aliveProbability = aliveProbability;
        }
    }
}

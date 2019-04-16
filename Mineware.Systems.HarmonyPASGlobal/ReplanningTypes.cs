using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineware.Systems.Planning
{

    public class ReplanningTypes
    {
        public replanningType getReplanningType(int theID)
        {
            replanningType theType = replanningType.rpNone;

            switch (theID)
            {
                case 1:
                    theType = replanningType.rpStopWp;
                    break;
                case 2:
                    theType = replanningType.rpNewWP;
                    break;
                case 3:
                    theType = replanningType.rpCrewChnages;
                    break;
                case 4:
                    theType = replanningType.rpCallCahnges;
                    break;
                case 5:
                    theType = replanningType.rpMoveWP;
                    break;
                case 6:
                    theType = replanningType.rpStartWP;
                    break;

                case 7:
                    theType = replanningType.rpMiningMethodChange;
                    break;

                case 8:
                    theType = replanningType.rpDrillRig ;
                    break;
                case 9:
                    theType = replanningType.rpDeleteWorkplace;
                    break;
            }

            return theType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Curs18apr
{

    public enum CofeeMakerStates
    {
        Pending = 0,

        WaitForSelection,

        PreparateBeverage,

        WaitForBeveragePickup
    }

    public enum CofeeMakerActions
    {
        InsertCoin = 0,

        SelectBeverage,

        BeverageReady,

        BeveragePickedUp
    }

    public class CofeeMaker : StateMachine<CofeeMakerStates, CofeeMakerActions>
    {
        public CofeeMaker()
            : base(
                CofeeMakerStates.Pending,
                CofeeMaker.GetConfiguration())
        {

        }

        private static Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>[] GetConfiguration()
        {
            return new[]
            {
                new Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>(
                    CofeeMakerStates.Pending, CofeeMakerActions.InsertCoin, CofeeMakerStates.WaitForSelection),
                new Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>(
                    CofeeMakerStates.WaitForSelection, CofeeMakerActions.InsertCoin, CofeeMakerStates.WaitForSelection),
                new Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>(
                    CofeeMakerStates.WaitForSelection, CofeeMakerActions.SelectBeverage, CofeeMakerStates.PreparateBeverage),
                new Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>(
                    CofeeMakerStates.PreparateBeverage, CofeeMakerActions.BeverageReady, CofeeMakerStates.WaitForBeveragePickup),
                new Tuple<CofeeMakerStates, CofeeMakerActions, CofeeMakerStates>(
                    CofeeMakerStates.WaitForBeveragePickup, CofeeMakerActions.BeveragePickedUp, CofeeMakerStates.Pending),
            };
        }

        protected override void OnAfterChangeCurrentState(CofeeMakerActions action, CofeeMakerStates oldState)
        {
            if ((this.CurrentState == CofeeMakerStates.PreparateBeverage) &&
                (action == CofeeMakerActions.SelectBeverage))
            {
                Thread.Sleep(1000);
                this.HandleAction(CofeeMakerActions.BeverageReady);
            }
        }
    }
}

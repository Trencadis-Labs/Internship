using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs18apr
{
    public class StateMachine<TState, TAction>
        where TState : struct
        where TAction : struct
    {
        private Tuple<TState, TAction, TState>[] configuration;

        public StateMachine(
            TState initialState,
            params Tuple<TState, TAction, TState>[] configuration
            )
        {
            this.StartState = initialState;

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (configuration.Length <= 0)
            {
                throw new ArgumentException("Configuration must have at least one element");
            }

            this.configuration = configuration;

            this.CurrentState = this.StartState;
        }

        public TState StartState
        {
            get;
            private set;
        }

        public TState CurrentState
        {
            get;
            private set;
        }

        public void HandleAction(TAction action)
        {
            var configEntry = this.FindConfigForAction(action);
            if (configEntry != null)
            {
                var oldState = this.CurrentState;
                var newState = configEntry.Item3;

                this.OnBeforeChangeCurrentState(action, newState);

                this.CurrentState = newState;

                this.OnAfterChangeCurrentState(action, oldState);
            }
            else
            {
                this.OnUnhandledAction(action);
            }
        }

        protected virtual void OnUnhandledAction(TAction action)
        {

        }

        protected virtual void OnBeforeChangeCurrentState(TAction action, TState nextState)
        {

        }

        protected virtual void OnAfterChangeCurrentState(TAction action, TState oldState)
        {

        }

        private Tuple<TState, TAction, TState> FindConfigForAction(TAction action)
        {
            foreach (var configEntry in this.configuration)
            {
                if (configEntry.Item1.Equals(this.CurrentState) &&
                    configEntry.Item2.Equals(action))
                {
                    return configEntry;
                }
            }

            return null;
        }
    }
}

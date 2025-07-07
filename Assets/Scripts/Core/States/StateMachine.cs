using System.Collections.Generic;

namespace Core.States
{
    public interface IStateMachine
    {
        void SwitchToState(StateType state);
        void InitStates(params IState[] states);
    }
    
    public class StateMachine : IStateMachine
    {
        private Dictionary<StateType, IState> _states = new();

        private IState _current;
        
        public void SwitchToState(StateType state)
        {
            var nextState = _states[state];

            _current?.Exit();
            _current = nextState;
            _current.Enter();
        }

        public void InitStates(params IState[] states)
        {
            foreach (var state in states)
            {
                _states[state.State] = state;
            }
        }
    }
}
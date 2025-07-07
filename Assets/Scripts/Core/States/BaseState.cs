namespace Core.States
{
    public interface IState
    {
        StateType State { get; }

        void Enter();
        void Exit();
    }

    public abstract class BaseState : IState
    {
        public abstract StateType State { get; }

        public abstract void Enter();

        public abstract void Exit();

    }
}
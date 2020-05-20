namespace StateMachines
{
    public class StateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T owner { get; private set; }

        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        public void ChangeState(State<T> newState)
        {
            if(currentState != null)
                currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState();
        }
    }

    public abstract class State<T>
    {
        public State(T owner)
        {
            this.owner = owner;
        }

        protected T owner;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}

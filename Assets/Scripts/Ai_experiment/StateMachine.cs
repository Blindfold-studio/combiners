namespace StateSystem
{
    public class StateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T owner;

        public StateMachine(T owner)
        {
            this.owner = owner;
            currentState = null;
        }

        public void ChangeState (State<T> newState)
        {
            if (currentState != null)
            {
                currentState.ExitState(this.owner);
            }

            currentState = newState;
            currentState.EnterState(this.owner);
        }

        public void Update ()
        {
            currentState.ExecuteState(this.owner);
        }

        public void FixedUpdate ()
        {
            currentState.FixedUpdateExecuteState(this.owner);
        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExecuteState(T owner);
        public abstract void FixedUpdateExecuteState(T owner);
        public abstract void ExitState(T owner);
    }
}

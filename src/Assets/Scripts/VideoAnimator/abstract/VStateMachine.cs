
using StateMachines;
using UnityEngine;
using UnityEngine.Video;

namespace VStateMachines
{
    public abstract class VStateMachine<T> : MonoBehaviour
    {
        public VState<T> currentState { get; private set; }

        public void ChangeState(VState<T> newState)
        {
            if (currentState != null)
                currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }

    }

    public abstract class VState<T>: State<T>
    {
        protected VState(T owner) : base(owner) { }

        public bool exitFlag;

        public abstract void FrameUpdated(VideoPlayer source, long frameIdx);
        public abstract void LoopReached(VideoPlayer source);
    }

    public abstract class VDirectionalState<T>: VState<T>
    {
        protected VDirectionalState(T owner) : base(owner) { }

        protected LookDirection lookDirection;
    }

}


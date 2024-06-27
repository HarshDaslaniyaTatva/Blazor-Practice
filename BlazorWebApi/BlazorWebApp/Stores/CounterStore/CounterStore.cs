using System;

namespace BlazorWebApp.Stores.CounterStore
{
    public class CounterState
    {
        public int Count { get; }

        public CounterState(int count)
        {
            Count = count;
        }
    }

    public class CounterStore
    {
        private CounterState _state;
        private Action _listeners = () => { };
        private readonly IActionDispatcher actionDispatcher;

        public CounterStore(IActionDispatcher actionDispatcher)
        {
            _state = new CounterState(0);
            this.actionDispatcher = actionDispatcher;
            this.actionDispatcher.Subscript(HandleActions);
        }
        //~CounterStore()
        //{
        //    this.actionDispatcher.Unsubscript(HandleActions);
        //}
        public CounterState GetState()
        {
            return _state;
        }

        private void HandleActions(IAction action)
        {
            switch (action.Name)
            {
                case IncrementAction.INCREMENT:
                    IncrementCount();
                    break;
                case DecrementAction.DECREMENT:
                    IncrementCount();
                    break;
                default:
                    IncrementCount();
                    break;
            }
        }

        private void IncrementCount()
        {
            int count = _state.Count;
            count++;
            _state = new CounterState(count);
            BroadcastStateChange();
        }

        private void DecrementCount()
        {
            int count = _state.Count;
            count--;
            _state = new CounterState(count);
            BroadcastStateChange();
        }

        public void AddStateChangeListener(Action listener)
        {
            _listeners += listener;
        }

        public void RemoveStateChangeListener(Action listener)
        {
            _listeners -= listener;
        }

        private void BroadcastStateChange()
        {
            _listeners.Invoke();
        }
    }
}

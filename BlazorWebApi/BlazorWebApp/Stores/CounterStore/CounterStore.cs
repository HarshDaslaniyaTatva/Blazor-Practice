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

        public CounterStore()
        {
            _state = new CounterState(0);
        }

        public CounterState GetState()
        {
            return _state;
        }

        public void IncrementCount()
        {
            int count = _state.Count;
            count++;
            _state = new CounterState(count);
            BroadcastStateChange();
        }

        public void DecrementCount()
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

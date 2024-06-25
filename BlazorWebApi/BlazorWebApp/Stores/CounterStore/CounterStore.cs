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
            int count = this._state.Count;
            count++;
            this._state = new CounterState(count);
            BroadcastStateChange();
        }

        public void DecrementCount()
        {
            int count = this._state.Count;
            count--;
            this._state = new CounterState(count);
            BroadcastStateChange();
        }
        #region observer patterns

        private Action _listeners = () =>{};

        public  void AddStateChangeListener(Action listener)
        {
            _listeners += listener;
        }
        public void RemoveStateChangeListener(Action listener)
        {
            _listeners = listener;
        }

        public void BroadcastStateChange()
        {
            _listeners.Invoke();
        }
        #endregion
    }
}

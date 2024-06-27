namespace BlazorWebApp.Stores
{
    public class ActionDispatcher : IActionDispatcher
    {
        private Action<IAction> _registerredActionHandlers;

        public void Subscript(Action<IAction> actionHandler)
        {
            _registerredActionHandlers += actionHandler;
        }
        public void Unsubscript(Action<IAction> actionHandler)
        {
            _registerredActionHandlers -= actionHandler;
        }
        public void Dispatch(IAction action)
        {
            _registerredActionHandlers?.Invoke(action);
        }

    }
}

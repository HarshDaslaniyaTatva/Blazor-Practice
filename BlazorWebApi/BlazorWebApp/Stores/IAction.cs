namespace BlazorWebApp.Stores
{
    public interface IAction
    {
        public const string INCREMENT = "INCREMENT";

        public string Name => INCREMENT;
    }
}

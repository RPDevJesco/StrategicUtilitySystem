namespace StrategicUtilitySystem
{
    public interface IAction
    {
        string Name { get; }
        double CalculateUtility(IContext context);
    }
}
namespace StrategicUtilitySystem
{
    public class GameAction : IAction
    {
        public string Name { get; private set; }

        private readonly Func<IContext, double> _calculateUtility;

        public GameAction(string name, Func<IContext, double> calculateUtility)
        {
            Name = name;
            _calculateUtility = calculateUtility;
        }

        public double CalculateUtility(IContext context)
        {
            return _calculateUtility(context);
        }
    }
}
using System.Collections.Concurrent;

namespace StrategicUtilitySystem
{
    public class DecisionMaker
    {
        private readonly UtilityCache _utilityCache = new();
        private double _actionThreshold = 0.5;
        private int _maxDegreeOfParallelism = Environment.ProcessorCount;

        public DecisionMaker(int? maxDegreeOfParallelism = null)
        {
            if (maxDegreeOfParallelism.HasValue)
            {
                _maxDegreeOfParallelism = maxDegreeOfParallelism.Value;
            }
        }

        public async Task<IAction> ChooseBestActionAsync(IEnumerable<IAction> actions, IContext context)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = _maxDegreeOfParallelism };
            var actionUtilities = new ConcurrentBag<(IAction, double)>();

            Parallel.ForEach(actions, options, action =>
            {
                var utility = _utilityCache.GetOrCalculateUtility(action, context);
                actionUtilities.Add((action, utility));
            });

            var bestAction = actionUtilities
                .Where(a => a.Item2 >= _actionThreshold)
                .OrderByDescending(a => a.Item2)
                .FirstOrDefault().Item1;

            return bestAction;
        }

        public void AdjustThreshold(double newThreshold)
        {
            _actionThreshold = newThreshold;
        }
    }
}
using System.Collections.Concurrent;

namespace StrategicUtilitySystem
{
    public class UtilityCache
    {
        private readonly ConcurrentDictionary<string, double> _cache = new();
        private readonly ConcurrentDictionary<string, int> _selectionCount = new(); // Track how often a decision is selected

        public double GetOrCalculateUtility(IAction action, IContext context)
        {
            var key = $"{action.Name}:{context.GetUniqueIdentifier()}";
            if (!_cache.TryGetValue(key, out var utility))
            {
                utility = action.CalculateUtility(context);
                _cache[key] = utility;
                _selectionCount[key] = 0; // Initialize selection count
            }
            return utility;
        }

        public void IncrementSelectionCount(string actionName, IContext context)
        {
            var key = $"{actionName}:{context.GetUniqueIdentifier()}";
            if (_selectionCount.ContainsKey(key))
            {
                _selectionCount[key]++;
            }
        }

        public int GetSelectionCount(string actionName, IContext context)
        {
            var key = $"{actionName}:{context.GetUniqueIdentifier()}";
            return _selectionCount.TryGetValue(key, out var count) ? count : 0;
        }
    }
}
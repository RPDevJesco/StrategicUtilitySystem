namespace StrategicUtilitySystem
{
    public class UtilityCache
    {
        private readonly Dictionary<string, double> _cache = new();

        public double GetOrCalculateUtility(IAction action, IContext context)
        {
            // Concatenate action.Name and context.GetUniqueIdentifier() to form a unique key
            var key = $"{action.Name}:{context.GetUniqueIdentifier()}";
            if (!_cache.TryGetValue(key, out var utility))
            {
                utility = action.CalculateUtility(context);
                _cache[key] = utility;
            }
            return utility;
        }
    }
}
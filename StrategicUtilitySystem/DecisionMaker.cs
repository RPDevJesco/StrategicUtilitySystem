using System.Collections.Concurrent;

namespace StrategicUtilitySystem
{
    public class DecisionMaker
{
    private readonly UtilityCache _utilityCache = new();
    private double _actionThreshold = 0.5;
    private int _maxDegreeOfParallelism = Environment.ProcessorCount;
    private int _selectionThreshold = 5; // Threshold for considering an action successful

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

        if (bestAction != null)
        {
            _utilityCache.IncrementSelectionCount(bestAction.Name, context);
            AdjustThresholdBasedOnSelectionCount(bestAction.Name, context);
        }

        return bestAction;
    }

    public IAction GetBestActionBelowThreshold(IEnumerable<IAction> actions, IContext context)
    {
        return actions.Select(action => (action, utility: _utilityCache.GetOrCalculateUtility(action, context)))
            .OrderByDescending(a => a.utility)
            .FirstOrDefault().action;
    }

    private void AdjustThresholdBasedOnSelectionCount(string actionName, IContext context)
    {
        var selectionCount = _utilityCache.GetSelectionCount(actionName, context);
        if (selectionCount > _selectionThreshold)
        {
            // If an action is selected frequently, increase the threshold to explore other options
            _actionThreshold += 0.1; // Increment by a small amount
        }
        else
        {
            // If no action is consistently selected, decrease the threshold to be more inclusive
            _actionThreshold -= 0.1; // Decrement by a small amount
        }
    }

    public void AdjustThreshold(double newThreshold)
    {
        _actionThreshold = newThreshold;
    }
    
    public double GetCurrentThreshold()
    {
        return _actionThreshold;
    }
}
}
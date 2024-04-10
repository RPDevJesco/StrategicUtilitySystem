using StrategicUtilitySystem;

class Program
{
    static async Task Main(string[] args)
    {
        var actions = new List<IAction>
        {
            new GameAction("Explore", context =>
            {
                if (context is GameContext gameContext && gameContext.EnvironmentType == "Forest")
                {
                    return 0.8; // Higher utility in forests
                }
                return 0.5; // Default utility
            }),
            // Adjust utility based on number of enemies
            new GameAction("Attack", context => context is GameContext { NumberOfEnemies: > 2 } ? 0.6 : 0.4)
        };
        
        var context = new GameContext(numberOfEnemies: 3, numberOfAllies: 1, resourceAvailability: 0.5, environmentType: "Forest");
        Console.WriteLine($"Context Summary: {context?.GetContextSummary()}");
        var decisionMaker = new DecisionMaker();

        // Choose the best action based on the initial context
        var bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);
        Console.WriteLine($"Best action: {bestAction?.Name}");

        // Example of adjusting threshold and re-evaluating the best action
        decisionMaker.AdjustThreshold(0.7);
        bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);
        Console.WriteLine($"Best action with adjusted threshold: {bestAction?.Name}");
    }
}
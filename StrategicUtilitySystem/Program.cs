using StrategicUtilitySystem;

class Program
{
    static double CalculateRiskRewardCost(double weight1, double reward)
    {
        return weight1 * reward;
    }
    
    static double CostWeightRiskReward(double weight1, double reward, double  weight2, double risk, double weight3, double cost)
    {
        return CalculateRiskRewardCost(weight1, reward) - CalculateRiskRewardCost(weight2, risk) - CalculateRiskRewardCost(weight3, cost);
    }
    
    static async Task Main(string[] args)
    {
        Random random = new Random();
        var actions = new List<IAction>
        {
            new GameAction("Explore", context =>
            {
                if (context is GameContext gameContext && gameContext.EnvironmentType == "Forest")
                {
                    return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10)); // Higher utility in forests
                }
                return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
            }),
            new GameAction("Attack", context =>
            {
                if (context is GameContext gameContext && gameContext.NumberOfEnemies > 2)
                {
                    return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
                }
                return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
            }),
            new GameAction("Defend", context =>
            {
                if (context is GameContext gameContext && gameContext.NumberOfAllies < gameContext.NumberOfEnemies)
                {
                    return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
                }
                return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
            }),
            new GameAction("Gather Resources", context =>
            {
                if (context is GameContext gameContext && gameContext.ResourceAvailability > 0.5)
                {
                    return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
                }
                return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
            }),
            new GameAction("Retreat", context =>
            {
                if (context is GameContext gameContext && gameContext.NumberOfAllies < 2)
                {
                    return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
                }
                return CostWeightRiskReward(random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10),random.Next(0, 10));
            })
        };

        var enemyNum = new Random().Next(0, 10);
        var allyNum = new Random().Next(0, 10);
        var resourceNum = new Random().Next(0, 10);
        var context = new GameContext(numberOfEnemies: enemyNum, numberOfAllies: allyNum, resourceAvailability: resourceNum, environmentType: "Forest");
        Console.WriteLine($"Context Summary: {context.GetContextSummary()}");
        var decisionMaker = new DecisionMaker();

        while (true)
        {
            var bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);
            if (bestAction != null)
            {
                Console.WriteLine($"Best action: {bestAction.Name}");
            }
            else
            {
                // Choose the action with the highest utility below the threshold
                bestAction = decisionMaker.GetBestActionBelowThreshold(actions, context);
                Console.WriteLine($"Best action below threshold: {bestAction?.Name ?? "None"}");
            }
            Console.WriteLine($"Current Threshold: {decisionMaker.GetCurrentThreshold()}");

            // Simulate a change in context or environment
            // For example, randomly change the number of enemies
            context.NumberOfEnemies = new Random().Next(0, 10);
            context.NumberOfAllies = new Random().Next(0, 10);
            context.ResourceAvailability = new Random().Next(0, 10);
            Console.WriteLine($"Updated Context Summary: {context.GetContextSummary()}");

            // Add a delay to simulate time passing between decisions
            await Task.Delay(1000);
        }
    }
}
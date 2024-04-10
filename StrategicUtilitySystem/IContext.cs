namespace StrategicUtilitySystem
{
    public interface IContext
    {
        // Example properties that might be relevant for decision-making
        int NumberOfEnemies { get; }
        int NumberOfAllies { get; }
        double ResourceAvailability { get; }
        string EnvironmentType { get; }
        
        string GetContextSummary();
        
        string GetUniqueIdentifier();
    }
}
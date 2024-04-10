namespace StrategicUtilitySystem
{
    public class GameContext : IContext
    {
        public int NumberOfEnemies { get; set; }
        public int NumberOfAllies { get; set; }
        public double ResourceAvailability { get; set; }
        public string EnvironmentType { get; set; }
        
        public GameContext(int numberOfEnemies, int numberOfAllies, double resourceAvailability, string environmentType)
        {
            NumberOfEnemies = numberOfEnemies;
            NumberOfAllies = numberOfAllies;
            ResourceAvailability = resourceAvailability;
            EnvironmentType = environmentType;
        }
        
        public string GetContextSummary()
        {
            return $"Enemies: {NumberOfEnemies}, Allies: {NumberOfAllies}, Resources: {ResourceAvailability}, Environment: {EnvironmentType}";
        }
        
        public string GetUniqueIdentifier()
        {
            return $"Enemies:{NumberOfEnemies}-Allies:{NumberOfAllies}-Resources:{ResourceAvailability}-Environment:{EnvironmentType}";
        }
    }
}
using StrategicUtilitySystem;

namespace StrategicUtilitySystemTest
{
    [TestFixture]
    public class DecisionMakerTests
    {
        [Test]
        public async Task ChooseBestActionAsync_ReturnsBestActionBasedOnUtility()
        {
            // Arrange
            var actions = new List<IAction>
            {
                new GameAction("LowUtilityAction", context => 0.3),
                new GameAction("HighUtilityAction", context => 0.7)
            };
            var context = new GameContext(1, 1, 1.0, "TestEnvironment");
            var decisionMaker = new DecisionMaker();

            // Act
            var bestAction = await decisionMaker.ChooseBestActionAsync(actions, context);

            // Assert
            Assert.AreEqual("HighUtilityAction", bestAction.Name);
        }
    }
}
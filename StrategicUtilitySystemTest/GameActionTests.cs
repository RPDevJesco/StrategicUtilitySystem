using StrategicUtilitySystem;

namespace StrategicUtilitySystemTests
{
    [TestFixture]
    public class GameActionTests
    {
        [Test]
        public void CalculateUtility_ReturnsCorrectValue()
        {
            // Arrange
            var action = new GameAction("TestAction", context => 0.5);
            var context = new GameContext(1, 1, 1.0, "TestEnvironment");

            // Act
            var utility = action.CalculateUtility(context);

            // Assert
            Assert.AreEqual(0.5, utility);
        }
    }
}
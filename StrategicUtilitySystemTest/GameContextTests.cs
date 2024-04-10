using StrategicUtilitySystem;

namespace StrategicUtilitySystemTest
{
    [TestFixture]
    public class UtilityCacheTests
    {
        [Test]
        public void GetOrCalculateUtility_CachesAndReturnsUtility()
        {
            // Arrange
            var cache = new UtilityCache();
            var action = new GameAction("TestAction", context => 0.6);
            var context = new GameContext(2, 3, 0.75, "Desert");

            // Act
            var firstCallUtility = cache.GetOrCalculateUtility(action, context);
            var secondCallUtility = cache.GetOrCalculateUtility(action, context);

            // Assert
            Assert.AreEqual(0.6, firstCallUtility);
            Assert.AreEqual(firstCallUtility, secondCallUtility); // Ensure the utility is the same on the second call, indicating it was cached
        }

        [Test]
        public void GetOrCalculateUtility_CalculatesUtilityForNewContext()
        {
            // Arrange
            var cache = new UtilityCache();
            var action = new GameAction("TestAction", context => context is GameContext { NumberOfEnemies: > 1 } ? 0.7 : 0.4);
            var context1 = new GameContext(2, 3, 0.75, "Desert");
            var context2 = new GameContext(1, 3, 0.75, "Desert");

            // Act
            var utility1 = cache.GetOrCalculateUtility(action, context1);
            var utility2 = cache.GetOrCalculateUtility(action, context2);

            // Assert
            Assert.AreEqual(0.7, utility1); // Utility should be 0.7 for context1 with more than 1 enemy
            Assert.AreEqual(0.4, utility2); // Utility should be 0.4 for context2 with 1 or fewer enemies
        }
    }
}
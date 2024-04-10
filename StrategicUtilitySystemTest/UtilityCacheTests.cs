using StrategicUtilitySystem;

namespace StrategicUtilitySystemTest
{
    [TestFixture]
    public class GameContextTests
    {
        [Test]
        public void GetContextSummary_ReturnsCorrectSummary()
        {
            // Arrange
            var context = new GameContext(2, 3, 0.75, "Desert");

            // Act
            var summary = context.GetContextSummary();

            // Assert
            Assert.AreEqual("Enemies: 2, Allies: 3, Resources: 0.75, Environment: Desert", summary);
        }

        [Test]
        public void GetUniqueIdentifier_ReturnsCorrectIdentifier()
        {
            // Arrange
            var context = new GameContext(2, 3, 0.75, "Desert");

            // Act
            var identifier = context.GetUniqueIdentifier();

            // Assert
            Assert.AreEqual("Enemies:2-Allies:3-Resources:0.75-Environment:Desert", identifier);
        }
    }
}
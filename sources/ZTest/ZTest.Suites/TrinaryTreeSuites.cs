namespace ZTest.Suites
{
    using NUnit.Framework;

    using ZTest.Library;

    public class TrinaryTreeSuites
    {
        [Test]
        public void Add_ConstructTree_ShouldStoreConstructed()
        {
            // Arrange
            TrinaryTree<int> tree = new TrinaryTree<int>();
            var nodes = new [] { 5, 4, 9, 5, 7, 2, 2 };

            // Act
            foreach (int i in nodes)
            {
                tree.Add(i);
            }

            // Assert
            Assert.AreEqual("-2-2-4-5-5-7-9", tree.ToStringInternal());
        }
    }
}

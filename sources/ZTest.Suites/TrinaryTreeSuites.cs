namespace ZTest.Suites
{
    using NUnit.Framework;

    using ZTest.Library;

    public class TrinaryTreeSuites
    {
        /// <summary>
        /// Test cases for <see cref="Remove_TestCase"/> method.
        /// </summary>
        public object[] RemoveTestCaseSource = new object[]
            {
                // 1. Create tree with one root item and remove it
                new object[] 
                { 
                    new int[] { 1 },
                    1, 
                    string.Empty
                },

                // 2. Create tree with 2 items and remove root
                new object[] 
                { 
                    new int[] { 2, 1 },
                    2, 
                    "1"
                },

                // 3. Create tree with 2 items and remove root
                new object[] 
                { 
                    new int[] { 2, 3 },
                    2, 
                    "3"
                },

                // 4. Create tree with 2 items and remove left
                new object[] 
                { 
                    new int[] { 2, 1 },
                    1, 
                    "2"
                },

                // 5. Create tree with 2 items and remove right
                new object[] 
                { 
                    new int[] { 2, 3 },
                    3, 
                    "2"
                },

                // 6. Create tree with root item and node on middle and remove item on middle
                new object[] 
                { 
                    new int[] { 2, 1, 3, 2 },
                    2, 
                    "1-2-3"
                },

                // 7. Create tree with 3 items and remove root
                new object[] 
                { 
                    new int[] { 2, 1, 3 },
                    2, 
                    "1-3"
                },

                // 8. Remove non-root item with only left node
                new object[] 
                { 
                    new int[] { 1, 10, 5 },
                    10, 
                    "1-5"
                },

                // 9. Remove non-root item with only right node
                new object[] 
                { 
                    new int[] { 1, 10, 20 },
                    10, 
                    "1-20"
                },

                // 10. Remove non-root item with middle node
                new object[] 
                { 
                    new int[] { 1, 10, 10 },
                    10, 
                    "1-10"
                },

                // 11. Remove non-root item with left and right node
                new object[] 
                { 
                    new int[] { 1, 10, 5, 15 },
                    10, 
                    "1-5-15"
                },

                // 12. Remove non-root item with left and right node (new node has middle nodes)
                new object[] 
                { 
                    new int[] { 1, 10, 5, 5, 15 },
                    10, 
                    "1-5-5-15"
                },
            };

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
            Assert.AreEqual("2-2-4-5-5-7-9", tree.ToStringInternal());
        }

        [Test]
        [TestCaseSource("RemoveTestCaseSource")]
        public void Remove_TestCase(int[] nodes, int valueToRemove, string result)
        {
            // Arrange
            TrinaryTree<int> tree = new TrinaryTree<int>();
            foreach (int v in nodes)
            {
                tree.Add(v);
            }

            // Act
            bool removed = tree.Remove(valueToRemove);

            // Assert
            Assert.IsTrue(removed, "Value should be removed");
            Assert.AreEqual(result, tree.ToStringInternal(), "Final state of tree is not expected");
        }
    }
}

namespace ZTest.Library
{
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Tri-nary tree data structure. This data structure is similar to binary tree,
    /// with only difference that each node has middle branch which stores nodes with the same
    /// value.
    /// </summary>
    public class TrinaryTree<T> where T : struct, IComparable<T>
    {
        private Node root;

        /// <summary>
        /// Add value to tri-nary tree structure.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(T value)
        {
            if (this.root == null)
            {
                this.root = new Node(value);
            }
            else
            {
                this.AddValue(this.root, value);
            }
        }

        /// <summary>
        /// Remove value from tri-nary tree structure.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><value>false</value> if value could not be found.</returns>
        public bool Remove(T value)
        {
            if (this.root == null)
            {
                return false;
            }

            return this.RemoveValue(null, this.root, value);
        }

        /// <summary>
        /// Internal methods, which helps us to test current data structure.
        /// </summary>
        /// <returns>String representation</returns>
        internal string ToStringInternal()
        {
            StringBuilder result = new StringBuilder();

            if (this.root != null)
            {
                this.ToStringInternal(this.root, result);
            }

            return result.ToString();
        }

        private void ToStringInternal(Node node, StringBuilder output)
        {
            if (node.Left != null)
            {
                this.ToStringInternal(node.Left, output);
            }

            for (int i = 0; i < (node.MiddleDepth + 1); i++)
            {
                if (output.Length > 0)
                {
                    output.Append('-');
                }

                output.Append(node.Value);
            }

            if (node.Right != null)
            {
                this.ToStringInternal(node.Right, output);
            }
        }

        private void AddValue(Node node, T value)
        {
            int c = value.CompareTo(node.Value);
            if (c == 0)
            {
                // see remarks to Node.MiddleDepth
                // this is how we track number of nodes in middle.
                node.MiddleDepth++;
            }
            else if (c > 0)
            {
                if (node.Right == null)
                {
                    node.Right = new Node(value);
                }
                else
                {
                    this.AddValue(node.Right, value);
                }
            }
            else
            {
                if (node.Left == null)
                {
                    node.Left = new Node(value);
                }
                else
                {
                    this.AddValue(node.Left, value);
                }
            }
        }

        private bool RemoveValue(Node parent, Node node, T value)
        {
            if (node != null)
            {
                int c = value.CompareTo(node.Value);
                if (c == 0)
                {
                    if (node.MiddleDepth > 0)
                    {
                        node.MiddleDepth--;
                    }
                    else
                    {
                        if (node.Left != null && node.Right != null)
                        {
                            // If left and right node of current node are not null
                            // we need to find smallest node on right and use it as a new current node
                            Node minNodeParent = node;
                            Node minNode = node.Right;
                            while (minNode.Left != null)
                            {
                                minNodeParent = minNode;
                                minNode = minNode.Left;
                            }

                            // Exchange values of minimum node on right and current node.
                            node.Value = minNode.Value;
                            node.MiddleDepth = minNode.MiddleDepth;

                            // To make sure that we can delete minimum node we just need to 
                            // set middle depth to 0.
                            minNode.MiddleDepth = 0;
                            if (!this.RemoveValue(minNodeParent, minNode, minNode.Value))
                            {
                                Debug.Fail("We cannot find child minimum node.");
                                throw new ApplicationException(Strings.ErrMsg_UnexpectedState);
                            }
                        }
                        else if (parent == null)
                        {
                            // This is case when node is root.
                            this.root = node.Left ?? node.Right;
                        }
                        else if (parent.Left == node)
                        {
                            parent.Left = node.Left ?? node.Right;
                        }
                        else if (parent.Right == node)
                        {
                            parent.Right = node.Left ?? node.Right;
                        }
                        else
                        {
                            Debug.Fail("How this can be possible that parent does not contains child node?");
                            throw new ApplicationException(Strings.ErrMsg_UnexpectedState);
                        }
                    }

                    return true;
                }
                else if (c > 0)
                {
                    return this.RemoveValue(node, node.Right, value);
                }
                else
                {
                    return this.RemoveValue(node, node.Left, value);
                }
            }

            return false;
        }

        /// <summary>
        /// Structure represents node in Tri-nary tree.
        /// </summary>
        /// <remarks>
        /// Because on middle node you can put only the same value as we have on current node
        /// we just track information about how many nodes we have in the middle with <see cref="Node.MiddleDepth"/>
        /// </remarks>
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public Node Left { get; set; }
            
            public Node Right { get; set; }
            
            public int MiddleDepth { get; set; }

            public T Value { get; set; }
        }
    }
}

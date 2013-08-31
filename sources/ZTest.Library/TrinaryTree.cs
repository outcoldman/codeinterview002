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

        private void AddValue(Node node, T value)
        {
            int c = value.CompareTo(node.Value);
            if (c == 0)
            {
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

        public bool Remove(T value)
        {
            if (this.root == null)
            {
                return false;
            }

            int c = value.CompareTo(this.root.Value);
            if (c == 0)
            {
                if (this.root.MiddleDepth > 0)
                {
                    this.root.MiddleDepth--;
                }
                else
                {
                    if (this.root.Left == null && this.root.Right == null)
                    {
                        this.root = null;
                    }
                    else
                    {
                        this.root = this.root.Right ?? this.root.Left;
                    }
                }

                return true;
            }
            else if (c > 0)
            {
                return this.RemoveValue(this.root, this.root.Right, value);
            }
            else
            {
                return this.RemoveValue(this.root, this.root.Left, value);
            }

            return false;
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
                            /*
                            Node minNodeParent = parent;
                            Node minNode = node;
                            while (minNode.Left != null)
                            {
                                minNodeParent = minNode;
                                minNode = minNode.Left;
                            }
                            */
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
                        }
                    }

                    return true;
                }
                else if (c > 0)
                {
                    this.RemoveValue(node, node.Right, value);
                }
                else
                {
                    this.RemoveValue(node, node.Left, value);
                }
            }

            return false;
        }

        private Node FindMinNode(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindMinNode(node.Left);
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
                output.AppendFormat("-{0}", node.Value);
            }

            if (node.Right != null)
            {
                this.ToStringInternal(node.Right, output);
            }
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public Node Left { get; set; }
            
            public Node Right { get; set; }
            
            public int MiddleDepth { get; set; }

            public T Value { get; private set; }
        }

    }
}

using System;
using System.Collections.Generic;

namespace BasicUnit.Trie
{
    /// <summary>
    /// Node of the trie.
    /// Stores a link to multiple children of type TrieNode each associated with a key of type Char.
    /// If the node terminates a string then it must also store a non-null value of type V.
    /// </summary>
    /// <typeparam name="V">Value associated with a character.</typeparam>
	class TrieNode<V> where V : class 
    {

        /// <summary>
        /// The value stored by this node. If not null then the node terminates a string.
        /// </summary>
        public V Value { get; set; }

        /// <summary>
        /// Get the key which was associated with this node.
        /// </summary>
        public Char Key { get; private set; }

        /// <summary>
        /// Get the parent of this node.
        /// </summary>
        public TrieNode<V> Parent { get; private set; }

        private Dictionary<Char, TrieNode<V>> children;

        /// <summary>
        /// Create an empty node with no children and null value.
        /// </summary>
        /// <param name="parent">Parent node of this node.</param>
        public TrieNode(TrieNode<V> parent, Char key)
        {
            this.Key = key;
            this.Value = null;
            this.Parent = parent;
            this.children = new Dictionary<Char, TrieNode<V>>();
        }

        /// <summary>
        /// Get a child of this node which is associated with a key.
        /// </summary>
        /// <param name="key">Key associated with the child of interest.</param>
        /// <returns>The child or null if no child is associated with the given key.</returns>
        public TrieNode<V> GetChild(char key)
        {
            if (this.children.ContainsKey(key))
            {
                return this.children[key];
            }
            return null;
        }

        /// <summary>
        /// Check whether or not this node terminates a string and stores a value.
        /// </summary>
        /// <returns>Whether node stores a value.</returns>
        public bool IsTerminater()
        {
            return this.Value != null;
        }

        /// <summary>
        /// Get the number of children this node has.
        /// </summary>
        /// <returns>Number of children.</returns>
        public int NumChildren()
        {
            return this.children.Count;
        }

        /// <summary>
        /// Check whether or not this node has any children.
        /// </summary>
        /// <returns>True if node does not have children, false otherwise.</returns>
        public bool IsLeaf()
        {
            return this.NumChildren() == 0;
        }

        /// <summary>
        /// Check whether or not one of the children of this node uses the given key.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if a child with given key exists, false otherwise.</returns>
        public bool ContainsKey(char key)
        {
            return this.children.ContainsKey(key);
        }

        /// <summary>
        /// Add a child node associated with a key to this node and return the node.
        /// </summary>
        /// <param name="key">Key to associated with the child node.</param>
        /// <returns>If given key already exists then return the existing child node, else return the new child node.</returns>
        public TrieNode<V> AddChild(char key)
        {
            if (this.children.ContainsKey(key))
            {
                return this.children[key];
            }
            else
            {
                TrieNode<V> newChild = new TrieNode<V>(this, key);
                this.children.Add(key, newChild);
                return newChild;
            }
        }

        /// <summary>
        /// Remove the child of a node associated with a key along with all its descendents.
        /// </summary>
        /// <param name="key">The key associated with the child to remove.</param>
        public void RemoveChild(char key)
        {
            this.children.Remove(key);
        }

        /// <summary>
        /// Get a list of values contained in this node and all its descendants.
        /// </summary>
        /// <returns>A List of values.</returns>
        public List<V> PrefixMatches()
        {
            if (this.IsLeaf())
            {
                if (this.IsTerminater())
                {
                    return new List<V>(new V[] { this.Value });
                }
                else
                {
                    return new List<V>();
                }
            }
            else
            {
                List<V> values = new List<V>();
                foreach (TrieNode<V> node in this.children.Values)
                {
                    values.AddRange(node.PrefixMatches());
                }

                if (this.IsTerminater())
                {
                    values.Add(this.Value);
                }
                
                return values;
            }
        }

    }
}
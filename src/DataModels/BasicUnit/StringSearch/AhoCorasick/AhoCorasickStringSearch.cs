using System.Collections;
using System.Collections.Generic;

namespace Gridsum.NLP.StringSearch.AhoCorasick
{
	/// <summary>
	/// Class for searching string for one or multiple 
	/// keywords using efficient Aho-Corasick search algorithm
	/// </summary>
	public class AhoCorasickStringSearch : IStringSearchAlgorithm
	{
		#region Objects

		/// <summary>
		/// Tree node representing character and its 
		/// transition and failure function
		/// </summary>
		class TreeNode
		{
			#region Constructor & Methods

			/// <summary>
			/// Initialize tree node with specified character
			/// </summary>
			/// <param name="parent">Parent node</param>
			/// <param name="c">Character</param>
			public TreeNode(TreeNode parent, char c)
			{
				this._char = c; this._parent = parent;
				this._results = new List<string>();
				this._resultsAr = new string[] { };

				this._transitionsAr = new TreeNode[] { };
				this._transHash = new Dictionary<char, TreeNode>();
			}


			/// <summary>
			/// Adds pattern ending in this node
			/// </summary>
			/// <param name="result">Pattern</param>
			public void AddResult(string result)
			{
				if (this._results.Contains(result))
				{
					return;
				}
				this._results.Add(result);
				this._resultsAr = (string[])this._results.ToArray();
			}

			/// <summary>
			/// Adds trabsition node
			/// </summary>
			/// <param name="node">Node</param>
			public void AddTransition(TreeNode node)
			{
				this._transHash.Add(node.Char, node);
				TreeNode[] ar = new TreeNode[this._transHash.Values.Count];
				this._transHash.Values.CopyTo(ar, 0);
				this._transitionsAr = ar;
			}


			/// <summary>
			/// Returns transition to specified character (if exists)
			/// </summary>
			/// <param name="c">Character</param>
			/// <returns>Returns TreeNode or null</returns>
			public TreeNode GetTransition(char c)
			{
				TreeNode treeNode = null;
				if (this._transHash.TryGetValue(c, out treeNode))
				{
					return treeNode;
				}

				return null;
			}


			/// <summary>
			/// Returns true if node contains transition to specified character
			/// </summary>
			/// <param name="c">Character</param>
			/// <returns>True if transition exists</returns>
			public bool ContainsTransition(char c)
			{
				return this.GetTransition(c) != null;
			}

			#endregion

			#region Properties

			private char _char;
			private TreeNode _parent;
			private TreeNode _failure;
			private List<string> _results;
			private TreeNode[] _transitionsAr;
			private string[] _resultsAr;
			private Dictionary<char, TreeNode> _transHash;

			/// <summary>
			/// Character
			/// </summary>
			public char Char
			{
				get { return this._char; }
			}


			/// <summary>
			/// Parent tree node
			/// </summary>
			public TreeNode Parent
			{
				get { return this._parent; }
			}


			/// <summary>
			/// Failure function - descendant node
			/// </summary>
			public TreeNode Failure
			{
				get { return this._failure; }
				set { this._failure = value; }
			}


			/// <summary>
			/// Transition function - list of descendant nodes
			/// </summary>
			public TreeNode[] Transitions
			{
				get { return this._transitionsAr; }
			}


			/// <summary>
			/// Returns list of patterns ending by this letter
			/// </summary>
			public string[] Results
			{
				get { return this._resultsAr; }
			}

			#endregion
		}

		#endregion

		#region Local fields

		/// <summary>
		/// Root of keyword tree
		/// </summary>
		private TreeNode _root;

		/// <summary>
		/// Keywords to search for
		/// </summary>
		private string[] _keywords;

		#endregion

		#region Initialization

		/// <summary>
		/// Initialize search algorithm (Build keyword tree)
		/// </summary>
		/// <param name="keywords">Keywords to search for</param>
		public AhoCorasickStringSearch(string[] keywords)
		{
			this.Keywords = keywords;
		}


		/// <summary>
		/// Initialize search algorithm with no keywords
		/// (Use Keywords property)
		/// </summary>
		public AhoCorasickStringSearch()
		{ }

		#endregion

		#region Implementation

		/// <summary>
		/// Build tree from specified keywords
		/// </summary>
		void BuildTree()
		{
			// Build keyword tree and transition function
			this._root = new TreeNode(null, ' ');
			foreach (string p in this._keywords)
			{
				// add pattern to tree
				TreeNode nd = this._root;
				foreach (char c in p)
				{
					TreeNode ndNew = null;
					foreach (TreeNode trans in nd.Transitions)
						if (trans.Char == c) { ndNew = trans; break; }

					if (ndNew == null)
					{
						ndNew = new TreeNode(nd, c);
						nd.AddTransition(ndNew);
					}
					nd = ndNew;
				}
				nd.AddResult(p);
			}

			// Find failure functions
			ArrayList nodes = new ArrayList();
			// level 1 nodes - fail to root node
			foreach (TreeNode nd in this._root.Transitions)
			{
				nd.Failure = this._root;
				foreach (TreeNode trans in nd.Transitions) nodes.Add(trans);
			}
			// other nodes - using BFS
			while (nodes.Count != 0)
			{
				ArrayList newNodes = new ArrayList();
				foreach (TreeNode nd in nodes)
				{
					TreeNode r = nd.Parent.Failure;
					char c = nd.Char;

					while (r != null && !r.ContainsTransition(c)) r = r.Failure;
					if (r == null)
						nd.Failure = this._root;
					else
					{
						nd.Failure = r.GetTransition(c);
						foreach (string result in nd.Failure.Results)
							nd.AddResult(result);
					}

					// add child nodes to BFS list 
					foreach (TreeNode child in nd.Transitions)
						newNodes.Add(child);
				}
				nodes = newNodes;
			}
			this._root.Failure = this._root;
		}


		#endregion

		#region Methods & Properties

		/// <summary>
		/// Keywords to search for (setting this property is slow, because
		/// it requieres rebuilding of keyword tree)
		/// </summary>
		public string[] Keywords
		{
			get { return this._keywords; }
			set
			{
				this._keywords = value;
				this.BuildTree();
			}
		}


		/// <summary>
		/// Searches passed text and returns all occurrences of any keyword
		/// </summary>
		/// <param name="text">Text to search</param>
		/// <returns>Array of occurrences</returns>
		public StringSearchResult[] FindAll(string text)
		{
			ArrayList ret = new ArrayList();
			TreeNode ptr = this._root;
			int index = 0;

			while (index < text.Length)
			{
				TreeNode trans = null;
				while (trans == null)
				{
					trans = ptr.GetTransition(text[index]);
					if (ptr == this._root) break;
					if (trans == null) ptr = ptr.Failure;
				}
				if (trans != null) ptr = trans;

				foreach (string found in ptr.Results)
					ret.Add(new StringSearchResult(index - found.Length + 1, found));
				index++;
			}
			return (StringSearchResult[])ret.ToArray(typeof(StringSearchResult));
		}


		/// <summary>
		/// Searches passed text and returns first occurrence of any keyword
		/// </summary>
		/// <param name="text">Text to search</param>
		/// <returns>First occurrence of any keyword (or StringSearchResult.Empty if text doesn't contain any keyword)</returns>
		public StringSearchResult FindFirst(string text)
		{
			ArrayList ret = new ArrayList();
			TreeNode ptr = this._root;
			int index = 0;

			while (index < text.Length)
			{
				TreeNode trans = null;
				while (trans == null)
				{
					trans = ptr.GetTransition(text[index]);
					if (ptr == this._root) break;
					if (trans == null) ptr = ptr.Failure;
				}
				if (trans != null) ptr = trans;

				foreach (string found in ptr.Results)
					return new StringSearchResult(index - found.Length + 1, found);
				index++;
			}
			return StringSearchResult.Empty;
		}


		/// <summary>
		/// Searches passed text and returns true if text contains any keyword
		/// </summary>
		/// <param name="text">Text to search</param>
		/// <returns>True when text contains any keyword</returns>
		public bool ContainsAny(string text)
		{
			TreeNode ptr = this._root;
			int index = 0;

			while (index < text.Length)
			{
				TreeNode trans = null;
				while (trans == null)
				{
					trans = ptr.GetTransition(text[index]);
					if (ptr == this._root) break;
					if (trans == null) ptr = ptr.Failure;
				}
				if (trans != null) ptr = trans;

				if (ptr.Results.Length > 0) return true;
				index++;
			}
			return false;
		}

		#endregion
	}
}
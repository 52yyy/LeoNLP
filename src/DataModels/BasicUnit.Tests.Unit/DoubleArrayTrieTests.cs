using System;
using System.Threading.Tasks;

using BasicUnit.DoubleArrayTrie;

using NUnit.Framework;

namespace BasicUnit.Tests.Unit
{
	[TestFixture]
	public class DATrieTests
	{
		[Test]
		[Description("插入新词测试")]
		public void IndexDatCase()
		{
			DATrie trie = new DATrie();
			trie.Index("bachelor");
			trie.Index("badge");

			DATrieNode it;
			it = trie.Search("asd");
			Assert.IsNull(it);
			it = trie.Search("badge");
			Assert.IsTrue(it.Word == "badge");
		}

		[Test]
		[Description("插入新词，Tail表冲突测试")]
		public void IndexDatTailCollisionCase()
		{
			DATrie trie = new DATrie();
			trie.Index("bachelor");
			trie.Index("badge");
			DATrieNode it;
			it = trie.Search("asd");
			Assert.IsNull(it);
			it = trie.Search("badge");
			Assert.IsTrue(it.Word == "badge");
		}

		[Test]
		public void DaTrieTest2()
		{
			DATrie trie = new DATrie();
			trie.Index("bachelor");
			trie.Index("jar");
			trie.Index("badge");
			trie.Index("刚才那个版本是1.3的 刚找到了这个是2.0的");
			trie.Index("中国人");
			trie.Index("中国");
			trie.Index("中国人民解放军");
			Assert.IsTrue(trie.SaveToFile("Models/ToyCase.model"));
			DATrieNode it;
			it = trie.Search("asd");
			Assert.IsNull(it);
			it = trie.Search("bachelor");
			Assert.IsTrue(it.Word == "bachelor");
			it = trie.Search("jar");
			Assert.IsTrue(it.Word == "jar");
			it = trie.Search("badge");
			Assert.IsTrue(it.Word == "badge");
			it = trie.Search("刚才那个版本是1.3的 刚找到了这个是2.0的");
			Assert.IsTrue(it.Word == "刚才那个版本是1.3的 刚找到了这个是2.0的");
			it = trie.Search("中国人");
			Assert.IsTrue(it.Word == "中国人");
			it = trie.Search("中国");
			Assert.IsTrue(it.Word == "中国");
			it = trie.Search("中国人民解放军");
			Assert.IsTrue(it.Word == "中国人民解放军");
			it = trie.Search("中国");
			Assert.IsTrue(it.Word == "中国");
			it = trie.Search("中国");
			Assert.IsTrue(it.Word == "中国");
			it = trie.Search("中国");
			Assert.IsTrue(it.Word == "中国");

			trie.Delete("中国");
			it = trie.Search("中国");
			Assert.IsNull(it);

			trie.Index("bachelor");
			trie.Delete("bachelor");
			it = trie.Search("bachelor");
			Assert.IsNull(it);
		}

		[Test]
		[Description("Tail重复插入测试")]
		public void TailDatCase()
		{
			DATrie trie = new DATrie();
			Console.WriteLine(trie.Index("ABCD"));
			Console.WriteLine(trie.Index("AB"));
			Console.WriteLine(trie.Index("AB"));
			Console.WriteLine(trie.Index("AB"));
			Console.WriteLine(trie.Index("AB"));
			DATrieNode it;
			it = trie.Search("ABCD");
			Assert.IsTrue(it.Word == "ABCD");
			it = trie.Search("AB");
			Assert.IsTrue(it.Word == "AB");
		}

		[Test]
		[Description("测试多线程查询")]
		public void ConcurrentCase()
		{
			DATrie trie = new DATrie();
			Console.WriteLine(trie.Index("ABCD"));
			Console.WriteLine(trie.Index("AB"));
			Task task1 = new Task(
				() =>
				{
					for (int i = 0; i < 100; i++)
					{
						Console.WriteLine(trie.Search("AB"));
					}
				});
			Task task2 = new Task(
				() =>
				{
					for (int i = 0; i < 100; i++)
					{
						Console.WriteLine(trie.Search("ABCD"));
					}
				});
			task1.Start();
			task2.Start();
			Task.WaitAll(task1, task2);
		}
	}
}
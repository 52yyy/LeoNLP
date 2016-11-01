using System.Diagnostics;

namespace LDA
{
	/// <summary>
	///		词
	/// </summary>
	[DebuggerDisplay("Id = {Id}, TopicId = {TopicId}")]
	public class Vector
	{
		public Vector(int id, int topicId)
		{
			this.Id = id;
			this.TopicId = topicId;
		}

		/// <summary>
		///		由词生成的唯一Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		词所属的TopicId
		/// </summary>
		public int TopicId { get; set; }
	}
}
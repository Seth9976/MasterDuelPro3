using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects.</summary>
	// Token: 0x020001EB RID: 491
	[DefaultMember("Item")]
	[Serializable]
	public class CodeCommentStatementCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to add. </param>
		// Token: 0x06000BDB RID: 3035 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeCommentStatement value)
		{
			return base.List.Add(value);
		}
	}
}

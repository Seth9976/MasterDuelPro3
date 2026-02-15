using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeStatement" /> objects.</summary>
	// Token: 0x02000217 RID: 535
	[DefaultMember("Item")]
	[Serializable]
	public class CodeStatementCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeStatement" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> object to add. </param>
		// Token: 0x06000C9A RID: 3226 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeStatement value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeExpression" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to add. </param>
		// Token: 0x06000C9B RID: 3227 RVA: 0x0003B669 File Offset: 0x00039869
		public int Add(CodeExpression value)
		{
			return this.Add(new CodeExpressionStatement(value));
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeStatement" /> objects to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000C9C RID: 3228 RVA: 0x0003B678 File Offset: 0x00039878
		public void AddRange(CodeStatement[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}
	}
}

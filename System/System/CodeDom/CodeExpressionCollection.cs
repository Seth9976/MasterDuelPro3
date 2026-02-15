using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeExpression" /> objects.</summary>
	// Token: 0x020001F8 RID: 504
	[DefaultMember("Item")]
	[Serializable]
	public class CodeExpressionCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeExpression" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to add. </param>
		// Token: 0x06000C03 RID: 3075 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeExpression" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000C04 RID: 3076 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		public void AddRange(CodeExpression[] value)
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

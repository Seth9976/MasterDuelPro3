using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeDirective" /> objects.</summary>
	// Token: 0x020001F4 RID: 500
	[Serializable]
	public class CodeDirectiveCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeDirective" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeDirective" /> at the index position.</returns>
		/// <param name="index">The index position to access.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of index positions for the collection. </exception>
		// Token: 0x17000259 RID: 601
		public CodeDirective this[int index]
		{
			get
			{
				return (CodeDirective)base.List[index];
			}
		}
	}
}

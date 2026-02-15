using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects.</summary>
	// Token: 0x02000223 RID: 547
	[Serializable]
	public class CodeTypeParameterCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the collection object to access.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170002B7 RID: 695
		public CodeTypeParameter this[int index]
		{
			get
			{
				return (CodeTypeParameter)base.List[index];
			}
		}
	}
}

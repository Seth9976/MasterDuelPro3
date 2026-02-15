using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeReference" /> objects.</summary>
	// Token: 0x020001D8 RID: 472
	[Serializable]
	public class CodeTypeReferenceCollection : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeReference" /> at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x1700022C RID: 556
		public CodeTypeReference this[int index]
		{
			get
			{
				return (CodeTypeReference)base.List[index];
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to add. </param>
		// Token: 0x06000B91 RID: 2961 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeTypeReference value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type name.</summary>
		/// <param name="value">The name of a data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection. </param>
		// Token: 0x06000B92 RID: 2962 RVA: 0x0003A51E File Offset: 0x0003871E
		public void Add(string value)
		{
			this.Add(new CodeTypeReference(value));
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type.</summary>
		/// <param name="value">The data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection. </param>
		// Token: 0x06000B93 RID: 2963 RVA: 0x0003A52D File Offset: 0x0003872D
		public void Add(Type value)
		{
			this.Add(new CodeTypeReference(value));
		}
	}
}

using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeAttributeArgument" /> objects.</summary>
	// Token: 0x020001DF RID: 479
	[DefaultMember("Item")]
	[Serializable]
	public class CodeAttributeArgumentCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to add. </param>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeAttributeArgument value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003A6CC File Offset: 0x000388CC
		public void AddRange(CodeAttributeArgument[] value)
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

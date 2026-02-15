using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects.</summary>
	// Token: 0x020001E1 RID: 481
	[DefaultMember("Item")]
	[Serializable]
	public class CodeAttributeDeclarationCollection : CollectionBase
	{
		/// <summary>Adds a <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object with the specified value to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to add. </param>
		// Token: 0x06000BBC RID: 3004 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeAttributeDeclaration value)
		{
			return base.List.Add(value);
		}
	}
}

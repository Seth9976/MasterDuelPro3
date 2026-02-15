using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
	// Token: 0x0200021D RID: 541
	[DefaultMember("Item")]
	[Serializable]
	public class CodeTypeDeclarationCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to add. </param>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeTypeDeclaration value)
		{
			return base.List.Add(value);
		}
	}
}

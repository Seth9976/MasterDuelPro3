using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects.</summary>
	// Token: 0x0200022F RID: 559
	[DefaultMember("Item")]
	[Serializable]
	public class CompilerErrorCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to the error collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to add. </param>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CompilerError value)
		{
			return base.List.Add(value);
		}
	}
}

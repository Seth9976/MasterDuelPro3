using System;
using System.Collections;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
	// Token: 0x0200020C RID: 524
	[DefaultMember("Item")]
	[Serializable]
	public class CodeParameterDeclarationExpressionCollection : CollectionBase
	{
		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to add. </param>
		// Token: 0x06000C7D RID: 3197 RVA: 0x0003A510 File Offset: 0x00038710
		public int Add(CodeParameterDeclarationExpression value)
		{
			return base.List.Add(value);
		}
	}
}

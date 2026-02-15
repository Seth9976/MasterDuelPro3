using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x020001F2 RID: 498
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0003AB75 File Offset: 0x00038D75
		public CodeExpression Expression { get; }
	}
}

using System;

namespace System.Linq.Expressions
{
	/// <summary>Specifies what kind of jump this <see cref="T:System.Linq.Expressions.GotoExpression" /> represents.</summary>
	// Token: 0x02000099 RID: 153
	public enum GotoExpressionKind
	{
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a jump to some location.</summary>
		// Token: 0x040001A1 RID: 417
		Goto,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a return statement.</summary>
		// Token: 0x040001A2 RID: 418
		Return,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a break statement.</summary>
		// Token: 0x040001A3 RID: 419
		Break,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a continue statement.</summary>
		// Token: 0x040001A4 RID: 420
		Continue
	}
}

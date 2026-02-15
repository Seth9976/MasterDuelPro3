using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000330 RID: 816
	internal abstract class AstNode
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600252C RID: 9516
		public abstract AstNode.AstType Type { get; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600252D RID: 9517
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x02000331 RID: 817
		public enum AstType
		{
			// Token: 0x040011CE RID: 4558
			Axis,
			// Token: 0x040011CF RID: 4559
			Operator,
			// Token: 0x040011D0 RID: 4560
			Filter,
			// Token: 0x040011D1 RID: 4561
			ConstantOperand,
			// Token: 0x040011D2 RID: 4562
			Function,
			// Token: 0x040011D3 RID: 4563
			Group,
			// Token: 0x040011D4 RID: 4564
			Root,
			// Token: 0x040011D5 RID: 4565
			Variable,
			// Token: 0x040011D6 RID: 4566
			Error
		}
	}
}

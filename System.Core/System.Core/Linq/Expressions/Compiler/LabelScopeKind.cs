using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000F3 RID: 243
	internal enum LabelScopeKind
	{
		// Token: 0x04000275 RID: 629
		Statement,
		// Token: 0x04000276 RID: 630
		Block,
		// Token: 0x04000277 RID: 631
		Switch,
		// Token: 0x04000278 RID: 632
		Lambda,
		// Token: 0x04000279 RID: 633
		Try,
		// Token: 0x0400027A RID: 634
		Catch,
		// Token: 0x0400027B RID: 635
		Finally,
		// Token: 0x0400027C RID: 636
		Filter,
		// Token: 0x0400027D RID: 637
		Expression
	}
}

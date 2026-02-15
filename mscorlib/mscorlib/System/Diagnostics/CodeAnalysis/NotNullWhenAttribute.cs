using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006EF RID: 1775
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x060037DA RID: 14298 RVA: 0x000DB787 File Offset: 0x000D9987
		public NotNullWhenAttribute(bool returnValue)
		{
			this.<ReturnValue>k__BackingField = returnValue;
		}
	}
}

using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000598 RID: 1432
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[CLSCompliant(false)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x06002B0D RID: 11021 RVA: 0x000AAC73 File Offset: 0x000A8E73
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x040015D6 RID: 5590
		private readonly string[] _transformNames;
	}
}

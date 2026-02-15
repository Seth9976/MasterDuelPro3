using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>A dynamic call site base class. This type is used as a parameter type to the dynamic site targets.</summary>
	// Token: 0x02000110 RID: 272
	public class CallSite
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0002497A File Offset: 0x00022B7A
		internal CallSite(CallSiteBinder binder)
		{
			this._binder = binder;
		}

		/// <summary>Class responsible for binding dynamic operations on the dynamic site.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> object responsible for binding dynamic operations.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00024989 File Offset: 0x00022B89
		public CallSiteBinder Binder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x040002DF RID: 735
		internal readonly CallSiteBinder _binder;

		// Token: 0x040002E0 RID: 736
		internal bool _match;
	}
}

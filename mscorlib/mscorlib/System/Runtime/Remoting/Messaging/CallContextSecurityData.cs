using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00096EA6 File Offset: 0x000950A6
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00096EAE File Offset: 0x000950AE
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00096EB7 File Offset: 0x000950B7
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00096EC2 File Offset: 0x000950C2
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x040011BE RID: 4542
		private IPrincipal _principal;
	}
}

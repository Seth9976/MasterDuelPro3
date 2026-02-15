using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x020003D1 RID: 977
	internal class ServiceNameStore
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x00067414 File Offset: 0x00065614
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x04000F64 RID: 3940
		private List<string> serviceNames;

		// Token: 0x04000F65 RID: 3941
		private ServiceNameCollection serviceNameCollection;
	}
}

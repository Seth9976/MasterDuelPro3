using System;
using System.Security;

namespace System.Runtime
{
	// Token: 0x0200001F RID: 31
	internal static class PartialTrustHelpers
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000037C5 File Offset: 0x000019C5
		internal static bool ShouldFlowSecurityContext
		{
			get
			{
				return SecurityManager.CurrentThreadRequiresSecurityContextCapture();
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002630 File Offset: 0x00000830
		internal static bool IsInFullTrust()
		{
			return true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002630 File Offset: 0x00000830
		internal static bool HasEtwPermissions()
		{
			return true;
		}
	}
}

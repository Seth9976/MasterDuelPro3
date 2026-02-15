using System;

namespace System.Net
{
	// Token: 0x020003A3 RID: 931
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x00063EDF File Offset: 0x000620DF
		private SystemNetworkCredential()
			: base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x04000E64 RID: 3684
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}

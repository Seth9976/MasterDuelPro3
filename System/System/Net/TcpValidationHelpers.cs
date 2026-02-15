using System;

namespace System.Net
{
	// Token: 0x02000380 RID: 896
	internal static class TcpValidationHelpers
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		public static bool ValidatePortNumber(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}

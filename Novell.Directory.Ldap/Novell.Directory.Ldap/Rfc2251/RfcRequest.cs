using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200008F RID: 143
	public interface RfcRequest
	{
		// Token: 0x06000481 RID: 1153
		RfcRequest dupRequest(string base_Renamed, string filter, bool reference);

		// Token: 0x06000482 RID: 1154
		string getRequestDN();
	}
}

using System;

namespace System.Net
{
	// Token: 0x020003CA RID: 970
	internal class HeaderInfo
	{
		// Token: 0x0600182F RID: 6191 RVA: 0x00066969 File Offset: 0x00064B69
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04000F4D RID: 3917
		internal readonly bool IsRequestRestricted;

		// Token: 0x04000F4E RID: 3918
		internal readonly bool IsResponseRestricted;

		// Token: 0x04000F4F RID: 3919
		internal readonly HeaderParser Parser;

		// Token: 0x04000F50 RID: 3920
		internal readonly string HeaderName;

		// Token: 0x04000F51 RID: 3921
		internal readonly bool AllowMultiValues;
	}
}

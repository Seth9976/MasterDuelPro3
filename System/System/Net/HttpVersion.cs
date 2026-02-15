using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x02000383 RID: 899
	public class HttpVersion
	{
		// Token: 0x04000D98 RID: 3480
		public static readonly Version Unknown = new Version(0, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x04000D99 RID: 3481
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x04000D9A RID: 3482
		public static readonly Version Version11 = new Version(1, 1);

		// Token: 0x04000D9B RID: 3483
		public static readonly Version Version20 = new Version(2, 0);
	}
}

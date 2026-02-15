using System;

namespace System
{
	/// <summary>Specifies the parts of a <see cref="T:System.Uri" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F9 RID: 249
	[Flags]
	public enum UriComponents
	{
		/// <summary>The <see cref="P:System.Uri.Scheme" /> data.</summary>
		// Token: 0x04000408 RID: 1032
		Scheme = 1,
		/// <summary>The <see cref="P:System.Uri.UserInfo" /> data.</summary>
		// Token: 0x04000409 RID: 1033
		UserInfo = 2,
		/// <summary>The <see cref="P:System.Uri.Host" /> data.</summary>
		// Token: 0x0400040A RID: 1034
		Host = 4,
		/// <summary>The <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x0400040B RID: 1035
		Port = 8,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> data.</summary>
		// Token: 0x0400040C RID: 1036
		Path = 16,
		/// <summary>The <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x0400040D RID: 1037
		Query = 32,
		/// <summary>The <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x0400040E RID: 1038
		Fragment = 64,
		/// <summary>The <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x0400040F RID: 1039
		StrongPort = 128,
		/// <summary>The normalized form of the <see cref="P:System.Uri.Host" />.</summary>
		// Token: 0x04000410 RID: 1040
		NormalizedHost = 256,
		/// <summary>Specifies that the delimiter should be included.</summary>
		// Token: 0x04000411 RID: 1041
		KeepDelimiter = 1073741824,
		/// <summary>The complete <see cref="T:System.Uri" /> context that is needed for Uri Serializers. The context includes the IPv6 scope.</summary>
		// Token: 0x04000412 RID: 1042
		SerializationInfoString = -2147483648,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, <see cref="P:System.Uri.Query" />, and <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x04000413 RID: 1043
		AbsoluteUri = 127,
		/// <summary>The <see cref="P:System.Uri.Host" /> and <see cref="P:System.Uri.Port" /> data. If no port data is in the Uri and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000414 RID: 1044
		HostAndPort = 132,
		/// <summary>The <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000415 RID: 1045
		StrongAuthority = 134,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000416 RID: 1046
		SchemeAndServer = 13,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, and <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000417 RID: 1047
		HttpRequestUrl = 61,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> and <see cref="P:System.Uri.Query" /> data. Also see <see cref="P:System.Uri.PathAndQuery" />. </summary>
		// Token: 0x04000418 RID: 1048
		PathAndQuery = 48
	}
}

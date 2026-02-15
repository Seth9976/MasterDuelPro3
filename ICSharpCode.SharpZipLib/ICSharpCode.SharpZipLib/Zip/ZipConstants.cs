using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000012 RID: 18
	public static class ZipConstants
	{
		// Token: 0x04000054 RID: 84
		public const int VersionMadeBy = 51;

		// Token: 0x04000055 RID: 85
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 51;

		// Token: 0x04000056 RID: 86
		public const int VersionStrongEncryption = 50;

		// Token: 0x04000057 RID: 87
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;

		// Token: 0x04000058 RID: 88
		public const int VERSION_AES = 51;

		// Token: 0x04000059 RID: 89
		public const int VersionZip64 = 45;

		// Token: 0x0400005A RID: 90
		public const int VersionBZip2 = 46;

		// Token: 0x0400005B RID: 91
		public const int LocalHeaderBaseSize = 30;

		// Token: 0x0400005C RID: 92
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;

		// Token: 0x0400005D RID: 93
		public const int Zip64DataDescriptorSize = 24;

		// Token: 0x0400005E RID: 94
		public const int DataDescriptorSize = 16;

		// Token: 0x0400005F RID: 95
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;

		// Token: 0x04000060 RID: 96
		public const int CentralHeaderBaseSize = 46;

		// Token: 0x04000061 RID: 97
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;

		// Token: 0x04000062 RID: 98
		public const int EndOfCentralRecordBaseSize = 22;

		// Token: 0x04000063 RID: 99
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;

		// Token: 0x04000064 RID: 100
		public const int CryptoHeaderSize = 12;

		// Token: 0x04000065 RID: 101
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;

		// Token: 0x04000066 RID: 102
		public const int Zip64EndOfCentralDirectoryLocatorSize = 20;

		// Token: 0x04000067 RID: 103
		public const int LocalHeaderSignature = 67324752;

		// Token: 0x04000068 RID: 104
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 67324752;

		// Token: 0x04000069 RID: 105
		public const int SpanningSignature = 134695760;

		// Token: 0x0400006A RID: 106
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 134695760;

		// Token: 0x0400006B RID: 107
		public const int SpanningTempSignature = 808471376;

		// Token: 0x0400006C RID: 108
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 808471376;

		// Token: 0x0400006D RID: 109
		public const int DataDescriptorSignature = 134695760;

		// Token: 0x0400006E RID: 110
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 134695760;

		// Token: 0x0400006F RID: 111
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 33639248;

		// Token: 0x04000070 RID: 112
		public const int CentralHeaderSignature = 33639248;

		// Token: 0x04000071 RID: 113
		public const int Zip64CentralFileHeaderSignature = 101075792;

		// Token: 0x04000072 RID: 114
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 101075792;

		// Token: 0x04000073 RID: 115
		public const int Zip64CentralDirLocatorSignature = 117853008;

		// Token: 0x04000074 RID: 116
		public const int ArchiveExtraDataSignature = 117853008;

		// Token: 0x04000075 RID: 117
		public const int CentralHeaderDigitalSignature = 84233040;

		// Token: 0x04000076 RID: 118
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 84233040;

		// Token: 0x04000077 RID: 119
		public const int EndOfCentralDirectorySignature = 101010256;

		// Token: 0x04000078 RID: 120
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 101010256;
	}
}

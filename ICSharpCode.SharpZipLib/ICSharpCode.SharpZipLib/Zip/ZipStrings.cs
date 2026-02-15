using System;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000052 RID: 82
	public static class ZipStrings
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000C28F File Offset: 0x0000A48F
		public static StringCodec GetStringCodec()
		{
			if (!ZipStrings.compatibilityMode)
			{
				return StringCodec.Default;
			}
			return ZipStrings.CompatCodec;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000C2A3 File Offset: 0x0000A4A3
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static int CodePage
		{
			get
			{
				return ZipStrings.CompatCodec.CodePage;
			}
			set
			{
				ZipStrings.CompatCodec = new StringCodec(ZipStrings.CompatCodec.ForceZipLegacyEncoding, Encoding.GetEncoding(value))
				{
					ZipArchiveCommentEncoding = ZipStrings.CompatCodec.ZipArchiveCommentEncoding,
					ZipCryptoEncoding = ZipStrings.CompatCodec.ZipCryptoEncoding
				};
				ZipStrings.compatibilityMode = true;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000C2FD File Offset: 0x0000A4FD
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static int SystemDefaultCodePage
		{
			get
			{
				return StringCodec.SystemDefaultCodePage;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000C304 File Offset: 0x0000A504
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000C313 File Offset: 0x0000A513
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static bool UseUnicode
		{
			get
			{
				return !ZipStrings.CompatCodec.ForceZipLegacyEncoding;
			}
			set
			{
				ZipStrings.CompatCodec = new StringCodec(!value, ZipStrings.CompatCodec.LegacyEncoding)
				{
					ZipArchiveCommentEncoding = ZipStrings.CompatCodec.ZipArchiveCommentEncoding,
					ZipCryptoEncoding = ZipStrings.CompatCodec.ZipCryptoEncoding
				};
				ZipStrings.compatibilityMode = true;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C353 File Offset: 0x0000A553
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		private static bool HasUnicodeFlag(int flags)
		{
			return ((GeneralBitFlags)flags).HasFlag(GeneralBitFlags.UnicodeText);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C36A File Offset: 0x0000A56A
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static string ConvertToString(byte[] data, int count)
		{
			return ZipStrings.CompatCodec.ZipOutputEncoding.GetString(data, 0, count);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C37E File Offset: 0x0000A57E
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static string ConvertToString(byte[] data)
		{
			return ZipStrings.CompatCodec.ZipOutputEncoding.GetString(data);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C390 File Offset: 0x0000A590
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			return ZipStrings.CompatCodec.ZipEncoding(ZipStrings.HasUnicodeFlag(flags)).GetString(data, 0, count);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C3AA File Offset: 0x0000A5AA
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			return ZipStrings.CompatCodec.ZipEncoding(ZipStrings.HasUnicodeFlag(flags)).GetString(data);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C3C2 File Offset: 0x0000A5C2
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static byte[] ConvertToArray(string str)
		{
			return ZipStrings.ConvertToArray(0, str);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		[Obsolete("Use ZipFile/Zip*Stream StringCodec instead")]
		public static byte[] ConvertToArray(int flags, string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return ZipStrings.CompatCodec.ZipEncoding(ZipStrings.HasUnicodeFlag(flags)).GetBytes(str);
			}
			return Empty.Array<byte>();
		}

		// Token: 0x04000191 RID: 401
		private static StringCodec CompatCodec = StringCodec.Default;

		// Token: 0x04000192 RID: 402
		private static bool compatibilityMode;
	}
}

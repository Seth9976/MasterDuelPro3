using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000053 RID: 83
	public class StringCodec
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		internal StringCodec(bool forceLegacyEncoding, Encoding legacyEncoding)
		{
			this.LegacyEncoding = legacyEncoding;
			this.ForceZipLegacyEncoding = forceLegacyEncoding;
			this.ZipArchiveCommentEncoding = legacyEncoding;
			this.ZipCryptoEncoding = legacyEncoding;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000C421 File Offset: 0x0000A621
		public static StringCodec Default
		{
			get
			{
				return new StringCodec(false, StringCodec.SystemDefaultEncoding);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C42E File Offset: 0x0000A62E
		public static StringCodec FromCodePage(int codePage)
		{
			return new StringCodec(false, Encoding.GetEncoding(codePage));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C43C File Offset: 0x0000A63C
		public static StringCodec FromEncoding(Encoding encoding)
		{
			return new StringCodec(false, encoding);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C445 File Offset: 0x0000A645
		public static StringCodec WithStrictSpecEncoding()
		{
			return new StringCodec(false, Encoding.GetEncoding(437));
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000C457 File Offset: 0x0000A657
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000C45F File Offset: 0x0000A65F
		public bool ForceZipLegacyEncoding { get; internal set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000C468 File Offset: 0x0000A668
		public static Encoding DefaultZipCryptoEncoding
		{
			get
			{
				return StringCodec.SystemDefaultEncoding;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000C46F File Offset: 0x0000A66F
		public Encoding ZipOutputEncoding
		{
			get
			{
				return this.ZipEncoding(!this.ForceZipLegacyEncoding);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C480 File Offset: 0x0000A680
		public Encoding ZipEncoding(bool unicode)
		{
			if (!unicode)
			{
				return this.LegacyEncoding;
			}
			return StringCodec.UnicodeZipEncoding;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C491 File Offset: 0x0000A691
		public Encoding ZipInputEncoding(GeneralBitFlags flags)
		{
			return this.ZipEncoding(!this.ForceZipLegacyEncoding && flags.HasAny(GeneralBitFlags.UnicodeText));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C4AF File Offset: 0x0000A6AF
		public Encoding ZipInputEncoding(int flags)
		{
			return this.ZipInputEncoding((GeneralBitFlags)flags);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public Encoding LegacyEncoding { get; internal set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
		public int CodePage
		{
			get
			{
				return this.LegacyEncoding.CodePage;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
		public static int SystemDefaultCodePage
		{
			get
			{
				return StringCodec.SystemDefaultEncoding.CodePage;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
		public static Encoding SystemDefaultEncoding
		{
			get
			{
				return Encoding.GetEncoding(0);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000C4EA File Offset: 0x0000A6EA
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000C4F2 File Offset: 0x0000A6F2
		public Encoding ZipArchiveCommentEncoding { get; internal set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000C503 File Offset: 0x0000A703
		public Encoding ZipCryptoEncoding { get; internal set; }

		// Token: 0x06000295 RID: 661 RVA: 0x0000C50C File Offset: 0x0000A70C
		public StringCodec WithZipArchiveCommentEncoding(Encoding commentEncoding)
		{
			return new StringCodec(this.ForceZipLegacyEncoding, this.LegacyEncoding)
			{
				ZipArchiveCommentEncoding = commentEncoding,
				ZipCryptoEncoding = this.ZipCryptoEncoding
			};
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C532 File Offset: 0x0000A732
		public StringCodec WithZipCryptoEncoding(Encoding cryptoEncoding)
		{
			return new StringCodec(this.ForceZipLegacyEncoding, this.LegacyEncoding)
			{
				ZipArchiveCommentEncoding = this.ZipArchiveCommentEncoding,
				ZipCryptoEncoding = cryptoEncoding
			};
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C558 File Offset: 0x0000A758
		public StringCodec WithForcedLegacyEncoding()
		{
			return new StringCodec(true, this.LegacyEncoding)
			{
				ZipArchiveCommentEncoding = this.ZipArchiveCommentEncoding,
				ZipCryptoEncoding = this.ZipCryptoEncoding
			};
		}

		// Token: 0x04000195 RID: 405
		public static readonly Encoding UnicodeZipEncoding = Encoding.UTF8;

		// Token: 0x04000196 RID: 406
		public const int ZipSpecCodePage = 437;
	}
}

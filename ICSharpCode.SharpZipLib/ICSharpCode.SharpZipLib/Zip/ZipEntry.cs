using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000016 RID: 22
	public class ZipEntry
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000032E7 File Offset: 0x000014E7
		public ZipEntry(string name)
			: this(name, 0, 51, CompressionMethod.Deflated, true)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032F5 File Offset: 0x000014F5
		internal ZipEntry(string name, int versionRequiredToExtract)
			: this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated, true)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003304 File Offset: 0x00001504
		internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method, bool unicode)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 65535)
			{
				throw new ArgumentException("Name is too long", "name");
			}
			if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
			{
				throw new ArgumentOutOfRangeException("versionRequiredToExtract");
			}
			this.DateTime = DateTime.Now;
			this.name = name;
			this.versionMadeBy = (ushort)madeByInfo;
			this.versionToExtract = (ushort)versionRequiredToExtract;
			this.method = method;
			this.IsUnicodeText = unicode;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000033A0 File Offset: 0x000015A0
		[Obsolete("Use Clone instead")]
		public ZipEntry(ZipEntry entry)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.known = entry.known;
			this.name = entry.name;
			this.size = entry.size;
			this.compressedSize = entry.compressedSize;
			this.crc = entry.crc;
			this.dateTime = entry.DateTime;
			this.method = entry.method;
			this.comment = entry.comment;
			this.versionToExtract = entry.versionToExtract;
			this.versionMadeBy = entry.versionMadeBy;
			this.externalFileAttributes = entry.externalFileAttributes;
			this.flags = entry.flags;
			this.zipFileIndex = entry.zipFileIndex;
			this.offset = entry.offset;
			this.forceZip64_ = entry.forceZip64_;
			if (entry.extra != null)
			{
				this.extra = new byte[entry.extra.Length];
				Array.Copy(entry.extra, 0, this.extra, 0, entry.extra.Length);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000034C1 File Offset: 0x000016C1
		public bool HasCrc
		{
			get
			{
				return (this.known & ZipEntry.Known.Crc) > ZipEntry.Known.None;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000034CE File Offset: 0x000016CE
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000034D7 File Offset: 0x000016D7
		public bool IsCrypted
		{
			get
			{
				return this.HasFlag(GeneralBitFlags.Encrypted);
			}
			set
			{
				this.SetFlag(GeneralBitFlags.Encrypted, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000034E1 File Offset: 0x000016E1
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000034EE File Offset: 0x000016EE
		public bool IsUnicodeText
		{
			get
			{
				return this.HasFlag(GeneralBitFlags.UnicodeText);
			}
			set
			{
				this.SetFlag(GeneralBitFlags.UnicodeText, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000034FC File Offset: 0x000016FC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003504 File Offset: 0x00001704
		internal byte CryptoCheckValue
		{
			get
			{
				return this.cryptoCheckValue_;
			}
			set
			{
				this.cryptoCheckValue_ = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000350D File Offset: 0x0000170D
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003515 File Offset: 0x00001715
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000351E File Offset: 0x0000171E
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003526 File Offset: 0x00001726
		public long ZipFileIndex
		{
			get
			{
				return this.zipFileIndex;
			}
			set
			{
				this.zipFileIndex = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000352F File Offset: 0x0000172F
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003537 File Offset: 0x00001737
		public long Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003540 File Offset: 0x00001740
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003555 File Offset: 0x00001755
		public int ExternalFileAttributes
		{
			get
			{
				if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None)
				{
					return this.externalFileAttributes;
				}
				return -1;
			}
			set
			{
				this.externalFileAttributes = value;
				this.known |= ZipEntry.Known.ExternalAttributes;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000356D File Offset: 0x0000176D
		public int VersionMadeBy
		{
			get
			{
				return (int)(this.versionMadeBy & 255);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000357B File Offset: 0x0000177B
		public bool IsDOSEntry
		{
			get
			{
				return this.HostSystem == 0 || this.HostSystem == 10;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003594 File Offset: 0x00001794
		private bool HasDosAttributes(int attributes)
		{
			bool flag = false;
			if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None)
			{
				flag |= (this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes;
			}
			return flag;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000035D2 File Offset: 0x000017D2
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000035E2 File Offset: 0x000017E2
		public int HostSystem
		{
			get
			{
				return (this.versionMadeBy >> 8) & 255;
			}
			set
			{
				this.versionMadeBy &= 255;
				this.versionMadeBy |= (ushort)((value & 255) << 8);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003610 File Offset: 0x00001810
		public int Version
		{
			get
			{
				if (this.versionToExtract != 0)
				{
					return (int)(this.versionToExtract & 255);
				}
				if (this.AESKeySize > 0)
				{
					return 51;
				}
				if (CompressionMethod.BZip2 == this.method)
				{
					return 46;
				}
				if (this.CentralHeaderRequiresZip64)
				{
					return 45;
				}
				if (CompressionMethod.Deflated == this.method || this.IsDirectory || this.IsCrypted)
				{
					return 20;
				}
				if (this.HasDosAttributes(8))
				{
					return 11;
				}
				return 10;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003680 File Offset: 0x00001880
		public bool CanDecompress
		{
			get
			{
				return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 46 || this.Version == 51) && this.IsCompressionMethodSupported();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036DB File Offset: 0x000018DB
		public void ForceZip64()
		{
			this.forceZip64_ = true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036E4 File Offset: 0x000018E4
		public bool IsZip64Forced()
		{
			return this.forceZip64_;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000036EC File Offset: 0x000018EC
		public bool LocalHeaderRequiresZip64
		{
			get
			{
				bool flag = this.forceZip64_;
				if (!flag)
				{
					ulong num = this.compressedSize;
					if (this.versionToExtract == 0 && this.IsCrypted)
					{
						num += (ulong)((long)this.EncryptionOverheadSize);
					}
					flag = (this.size >= (ulong)(-1) || num >= (ulong)(-1)) && (this.versionToExtract == 0 || this.versionToExtract >= 45);
				}
				return flag;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003750 File Offset: 0x00001950
		public bool CentralHeaderRequiresZip64
		{
			get
			{
				return this.LocalHeaderRequiresZip64 || this.offset >= (long)((ulong)(-1));
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000376C File Offset: 0x0000196C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000384C File Offset: 0x00001A4C
		public long DosTime
		{
			get
			{
				if ((this.known & ZipEntry.Known.Time) == ZipEntry.Known.None)
				{
					return 0L;
				}
				uint num = (uint)this.DateTime.Year;
				uint num2 = (uint)this.DateTime.Month;
				uint num3 = (uint)this.DateTime.Day;
				uint num4 = (uint)this.DateTime.Hour;
				uint num5 = (uint)this.DateTime.Minute;
				uint num6 = (uint)this.DateTime.Second;
				if (num < 1980U)
				{
					num = 1980U;
					num2 = 1U;
					num3 = 1U;
					num4 = 0U;
					num5 = 0U;
					num6 = 0U;
				}
				else if (num > 2107U)
				{
					num = 2107U;
					num2 = 12U;
					num3 = 31U;
					num4 = 23U;
					num5 = 59U;
					num6 = 59U;
				}
				return (long)((ulong)((((num - 1980U) & 127U) << 25) | (num2 << 21) | (num3 << 16) | (num4 << 11) | (num5 << 5) | (num6 >> 1)));
			}
			set
			{
				uint num = (uint)value;
				uint num2 = Math.Min(59U, 2U * (num & 31U));
				uint num3 = Math.Min(59U, (num >> 5) & 63U);
				uint num4 = Math.Min(23U, (num >> 11) & 31U);
				uint num5 = Math.Max(1U, Math.Min(12U, (uint)(value >> 21) & 15U));
				uint num6 = ((num >> 25) & 127U) + 1980U;
				int num7 = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num6, (int)num5), (int)((value >> 16) & 31L)));
				this.DateTime = new DateTime((int)num6, (int)num5, num7, (int)num4, (int)num3, (int)num2, DateTimeKind.Unspecified);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000038E1 File Offset: 0x00001AE1
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000038E9 File Offset: 0x00001AE9
		public DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
			set
			{
				this.dateTime = value;
				this.known |= ZipEntry.Known.Time;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003900 File Offset: 0x00001B00
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003908 File Offset: 0x00001B08
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003911 File Offset: 0x00001B11
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003926 File Offset: 0x00001B26
		public long Size
		{
			get
			{
				if ((this.known & ZipEntry.Known.Size) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)this.size;
			}
			set
			{
				this.size = (ulong)value;
				this.known |= ZipEntry.Known.Size;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000393D File Offset: 0x00001B3D
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003952 File Offset: 0x00001B52
		public long CompressedSize
		{
			get
			{
				if ((this.known & ZipEntry.Known.CompressedSize) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)this.compressedSize;
			}
			set
			{
				this.compressedSize = (ulong)value;
				this.known |= ZipEntry.Known.CompressedSize;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003969 File Offset: 0x00001B69
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003982 File Offset: 0x00001B82
		public long Crc
		{
			get
			{
				if ((this.known & ZipEntry.Known.Crc) == ZipEntry.Known.None)
				{
					return -1L;
				}
				return (long)((ulong)this.crc & (ulong)(-1));
			}
			set
			{
				if (((ulong)this.crc & 18446744069414584320UL) != 0UL)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.crc = (uint)value;
				this.known |= ZipEntry.Known.Crc;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000039B8 File Offset: 0x00001BB8
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000039C0 File Offset: 0x00001BC0
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000039C9 File Offset: 0x00001BC9
		internal CompressionMethod CompressionMethodForHeader
		{
			get
			{
				if (this.AESKeySize <= 0)
				{
					return this.method;
				}
				return CompressionMethod.WinZipAES;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000039DD File Offset: 0x00001BDD
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000039E8 File Offset: 0x00001BE8
		public byte[] ExtraData
		{
			get
			{
				return this.extra;
			}
			set
			{
				if (value == null)
				{
					this.extra = null;
					return;
				}
				if (value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.extra = new byte[value.Length];
				Array.Copy(value, 0, this.extra, 0, value.Length);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003A34 File Offset: 0x00001C34
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003A90 File Offset: 0x00001C90
		public int AESKeySize
		{
			get
			{
				switch (this._aesEncryptionStrength)
				{
				case 0:
					return 0;
				case 1:
					return 128;
				case 2:
					return 192;
				case 3:
					return 256;
				default:
					throw new ZipException("Invalid AESEncryptionStrength " + this._aesEncryptionStrength.ToString());
				}
			}
			set
			{
				if (value == 0)
				{
					this._aesEncryptionStrength = 0;
					return;
				}
				if (value == 128)
				{
					this._aesEncryptionStrength = 1;
					return;
				}
				if (value != 256)
				{
					throw new ZipException("AESKeySize must be 0, 128 or 256: " + value.ToString());
				}
				this._aesEncryptionStrength = 3;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003AE0 File Offset: 0x00001CE0
		internal byte AESEncryptionStrength
		{
			get
			{
				return (byte)this._aesEncryptionStrength;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003AE9 File Offset: 0x00001CE9
		internal int AESSaltLen
		{
			get
			{
				return this.AESKeySize / 16;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003AF4 File Offset: 0x00001CF4
		internal int AESOverheadSize
		{
			get
			{
				return 12 + this.AESSaltLen;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003AFF File Offset: 0x00001CFF
		internal int EncryptionOverheadSize
		{
			get
			{
				if (!this.IsCrypted)
				{
					return 0;
				}
				if (this._aesEncryptionStrength != 0)
				{
					return this.AESOverheadSize;
				}
				return 12;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B1C File Offset: 0x00001D1C
		internal void ProcessExtraData(bool localHeader)
		{
			ZipExtraData zipExtraData = new ZipExtraData(this.extra);
			if (zipExtraData.Find(1))
			{
				this.forceZip64_ = true;
				if (zipExtraData.ValueLength < 4)
				{
					throw new ZipException("Extra data extended Zip64 information length is invalid");
				}
				if (this.size == (ulong)(-1))
				{
					this.size = (ulong)zipExtraData.ReadLong();
				}
				if (this.compressedSize == (ulong)(-1))
				{
					this.compressedSize = (ulong)zipExtraData.ReadLong();
				}
				if (!localHeader && this.offset == (long)((ulong)(-1)))
				{
					this.offset = zipExtraData.ReadLong();
				}
			}
			else if ((this.versionToExtract & 255) >= 45 && (this.size == (ulong)(-1) || this.compressedSize == (ulong)(-1)))
			{
				throw new ZipException("Zip64 Extended information required but is missing.");
			}
			this.DateTime = ZipEntry.GetDateTime(zipExtraData) ?? this.DateTime;
			if (this.method == CompressionMethod.WinZipAES)
			{
				this.ProcessAESExtraData(zipExtraData);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C08 File Offset: 0x00001E08
		private static DateTime? GetDateTime(ZipExtraData extraData)
		{
			ExtendedUnixData data = extraData.GetData<ExtendedUnixData>();
			if (data != null && data.Include.HasFlag(ExtendedUnixData.Flags.ModificationTime))
			{
				return new DateTime?(data.ModificationTime);
			}
			return null;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C4C File Offset: 0x00001E4C
		private void ProcessAESExtraData(ZipExtraData extraData)
		{
			if (!extraData.Find(39169))
			{
				throw new ZipException("AES Extra Data missing");
			}
			this.versionToExtract = 51;
			int valueLength = extraData.ValueLength;
			if (valueLength < 7)
			{
				throw new ZipException("AES Extra Data Length " + valueLength.ToString() + " invalid.");
			}
			int num = extraData.ReadShort();
			extraData.ReadShort();
			int num2 = extraData.ReadByte();
			int num3 = extraData.ReadShort();
			this._aesVer = num;
			this._aesEncryptionStrength = num2;
			this.method = (CompressionMethod)num3;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003CD1 File Offset: 0x00001ED1
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003CD9 File Offset: 0x00001ED9
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				if (value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
				}
				this.comment = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003D04 File Offset: 0x00001F04
		public bool IsDirectory
		{
			get
			{
				return (this.name.Length > 0 && (this.name[this.name.Length - 1] == '/' || this.name[this.name.Length - 1] == '\\')) || this.HasDosAttributes(16);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003D61 File Offset: 0x00001F61
		public bool IsFile
		{
			get
			{
				return !this.IsDirectory && !this.HasDosAttributes(8);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003D77 File Offset: 0x00001F77
		public bool IsCompressionMethodSupported()
		{
			return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D84 File Offset: 0x00001F84
		public object Clone()
		{
			ZipEntry zipEntry = (ZipEntry)base.MemberwiseClone();
			if (this.extra != null)
			{
				zipEntry.extra = new byte[this.extra.Length];
				Array.Copy(this.extra, 0, zipEntry.extra, 0, this.extra.Length);
			}
			return zipEntry;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003900 File Offset: 0x00001B00
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public static bool IsCompressionMethodSupported(CompressionMethod method)
		{
			return method == CompressionMethod.Deflated || method == CompressionMethod.Stored || method == CompressionMethod.BZip2;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public static string CleanName(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			if (Path.IsPathRooted(name))
			{
				name = name.Substring(Path.GetPathRoot(name).Length);
			}
			name = name.Replace("\\", "/");
			while (name.Length > 0 && name[0] == '/')
			{
				name = name.Remove(0, 1);
			}
			return name;
		}

		// Token: 0x04000094 RID: 148
		private ZipEntry.Known known;

		// Token: 0x04000095 RID: 149
		private int externalFileAttributes;

		// Token: 0x04000096 RID: 150
		private ushort versionMadeBy;

		// Token: 0x04000097 RID: 151
		private string name;

		// Token: 0x04000098 RID: 152
		private ulong size;

		// Token: 0x04000099 RID: 153
		private ulong compressedSize;

		// Token: 0x0400009A RID: 154
		private ushort versionToExtract;

		// Token: 0x0400009B RID: 155
		private uint crc;

		// Token: 0x0400009C RID: 156
		private DateTime dateTime;

		// Token: 0x0400009D RID: 157
		private CompressionMethod method;

		// Token: 0x0400009E RID: 158
		private byte[] extra;

		// Token: 0x0400009F RID: 159
		private string comment;

		// Token: 0x040000A0 RID: 160
		private int flags;

		// Token: 0x040000A1 RID: 161
		private long zipFileIndex;

		// Token: 0x040000A2 RID: 162
		private long offset;

		// Token: 0x040000A3 RID: 163
		private bool forceZip64_;

		// Token: 0x040000A4 RID: 164
		private byte cryptoCheckValue_;

		// Token: 0x040000A5 RID: 165
		private int _aesVer;

		// Token: 0x040000A6 RID: 166
		private int _aesEncryptionStrength;

		// Token: 0x02000017 RID: 23
		[Flags]
		private enum Known : byte
		{
			// Token: 0x040000A8 RID: 168
			None = 0,
			// Token: 0x040000A9 RID: 169
			Size = 1,
			// Token: 0x040000AA RID: 170
			CompressedSize = 2,
			// Token: 0x040000AB RID: 171
			Crc = 4,
			// Token: 0x040000AC RID: 172
			Time = 8,
			// Token: 0x040000AD RID: 173
			ExternalAttributes = 16
		}
	}
}

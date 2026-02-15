using System;
using System.Buffers;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000078 RID: 120
	public class TarHeader
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x00013A98 File Offset: 0x00011C98
		public TarHeader()
		{
			this.Magic = "ustar";
			this.Version = " ";
			this.Name = "";
			this.LinkName = "";
			this.UserId = TarHeader.defaultUserId;
			this.GroupId = TarHeader.defaultGroupId;
			this.UserName = TarHeader.defaultUser;
			this.GroupName = TarHeader.defaultGroupName;
			this.Size = 0L;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00013B0B File Offset: 0x00011D0B
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00013B13 File Offset: 0x00011D13
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00013B0B File Offset: 0x00011D0B
		[Obsolete("Use the Name property instead", true)]
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00013B2A File Offset: 0x00011D2A
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00013B32 File Offset: 0x00011D32
		public int Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00013B3B File Offset: 0x00011D3B
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00013B43 File Offset: 0x00011D43
		public int UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00013B4C File Offset: 0x00011D4C
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00013B54 File Offset: 0x00011D54
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00013B5D File Offset: 0x00011D5D
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00013B65 File Offset: 0x00011D65
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be less than zero");
				}
				this.size = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00013B83 File Offset: 0x00011D83
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00013B8C File Offset: 0x00011D8C
		public DateTime ModTime
		{
			get
			{
				return this.modTime;
			}
			set
			{
				if (value < TarHeader.dateTime1970)
				{
					throw new ArgumentOutOfRangeException("value", "ModTime cannot be before Jan 1st 1970");
				}
				this.modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00013BEB File Offset: 0x00011DEB
		public int Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013BF3 File Offset: 0x00011DF3
		public bool IsChecksumValid
		{
			get
			{
				return this.isChecksumValid;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00013BFB File Offset: 0x00011DFB
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00013C03 File Offset: 0x00011E03
		public byte TypeFlag
		{
			get
			{
				return this.typeFlag;
			}
			set
			{
				this.typeFlag = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013C0C File Offset: 0x00011E0C
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00013C14 File Offset: 0x00011E14
		public string LinkName
		{
			get
			{
				return this.linkName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.linkName = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00013C2B File Offset: 0x00011E2B
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00013C33 File Offset: 0x00011E33
		public string Magic
		{
			get
			{
				return this.magic;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.magic = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00013C4A File Offset: 0x00011E4A
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00013C52 File Offset: 0x00011E52
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.version = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00013C69 File Offset: 0x00011E69
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00013C74 File Offset: 0x00011E74
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				if (value != null)
				{
					this.userName = value.Substring(0, Math.Min(32, value.Length));
					return;
				}
				string text = "user";
				if (text.Length > 32)
				{
					text = text.Substring(0, 32);
				}
				this.userName = text;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00013CC0 File Offset: 0x00011EC0
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00013CC8 File Offset: 0x00011EC8
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				if (value == null)
				{
					this.groupName = "None";
					return;
				}
				this.groupName = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00013CE0 File Offset: 0x00011EE0
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public int DevMajor
		{
			get
			{
				return this.devMajor;
			}
			set
			{
				this.devMajor = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00013CF1 File Offset: 0x00011EF1
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00013CF9 File Offset: 0x00011EF9
		public int DevMinor
		{
			get
			{
				return this.devMinor;
			}
			set
			{
				this.devMinor = value;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013D02 File Offset: 0x00011F02
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00013D0C File Offset: 0x00011F0C
		public void ParseBuffer(byte[] header, Encoding nameEncoding)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			int num = 0;
			Span<byte> span = header.AsSpan<byte>();
			this.name = TarHeader.ParseName(span.Slice(num, 100), nameEncoding);
			num += 100;
			this.mode = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.UserId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.GroupId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.Size = TarHeader.ParseBinaryOrOctal(header, num, 12);
			num += 12;
			this.ModTime = TarHeader.GetDateTimeFromCTime(TarHeader.ParseOctal(header, num, 12));
			num += 12;
			this.checksum = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.TypeFlag = header[num++];
			this.LinkName = TarHeader.ParseName(span.Slice(num, 100), nameEncoding);
			num += 100;
			this.Magic = TarHeader.ParseName(span.Slice(num, 6), nameEncoding);
			num += 6;
			if (this.Magic == "ustar")
			{
				this.Version = TarHeader.ParseName(span.Slice(num, 2), nameEncoding);
				num += 2;
				this.UserName = TarHeader.ParseName(span.Slice(num, 32), nameEncoding);
				num += 32;
				this.GroupName = TarHeader.ParseName(span.Slice(num, 32), nameEncoding);
				num += 32;
				this.DevMajor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				this.DevMinor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				string text = TarHeader.ParseName(span.Slice(num, 155), nameEncoding);
				if (!string.IsNullOrEmpty(text))
				{
					this.Name = text + "/" + this.Name;
				}
			}
			this.isChecksumValid = this.Checksum == TarHeader.MakeCheckSum(header);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00013EF9 File Offset: 0x000120F9
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void ParseBuffer(byte[] header)
		{
			this.ParseBuffer(header, null);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013F03 File Offset: 0x00012103
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public void WriteHeader(byte[] outBuffer)
		{
			this.WriteHeader(outBuffer, null);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00013F10 File Offset: 0x00012110
		public void WriteHeader(byte[] outBuffer, Encoding nameEncoding)
		{
			if (outBuffer == null)
			{
				throw new ArgumentNullException("outBuffer");
			}
			int i = 0;
			i = TarHeader.GetNameBytes(this.Name, outBuffer, i, 100, nameEncoding);
			i = TarHeader.GetOctalBytes((long)this.mode, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.UserId, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.GroupId, outBuffer, i, 8);
			i = TarHeader.GetBinaryOrOctalBytes(this.Size, outBuffer, i, 12);
			i = TarHeader.GetOctalBytes((long)TarHeader.GetCTime(this.ModTime), outBuffer, i, 12);
			int num = i;
			for (int j = 0; j < 8; j++)
			{
				outBuffer[i++] = 32;
			}
			outBuffer[i++] = this.TypeFlag;
			i = TarHeader.GetNameBytes(this.LinkName, outBuffer, i, 100, nameEncoding);
			i = TarHeader.GetAsciiBytes(this.Magic, 0, outBuffer, i, 6, nameEncoding);
			i = TarHeader.GetNameBytes(this.Version, outBuffer, i, 2, nameEncoding);
			i = TarHeader.GetNameBytes(this.UserName, outBuffer, i, 32, nameEncoding);
			i = TarHeader.GetNameBytes(this.GroupName, outBuffer, i, 32, nameEncoding);
			if (this.TypeFlag == 51 || this.TypeFlag == 52)
			{
				i = TarHeader.GetOctalBytes((long)this.DevMajor, outBuffer, i, 8);
				i = TarHeader.GetOctalBytes((long)this.DevMinor, outBuffer, i, 8);
			}
			while (i < outBuffer.Length)
			{
				outBuffer[i++] = 0;
			}
			this.checksum = TarHeader.ComputeCheckSum(outBuffer);
			TarHeader.GetCheckSumOctalBytes((long)this.checksum, outBuffer, num, 8);
			this.isChecksumValid = true;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014072 File Offset: 0x00012272
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00014080 File Offset: 0x00012280
		public override bool Equals(object obj)
		{
			TarHeader tarHeader = obj as TarHeader;
			return tarHeader != null && (this.name == tarHeader.name && this.mode == tarHeader.mode && this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId && this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && this.Checksum == tarHeader.Checksum && this.TypeFlag == tarHeader.TypeFlag && this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && this.Version == tarHeader.Version && this.UserName == tarHeader.UserName && this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor) && this.DevMinor == tarHeader.DevMinor;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000141AD File Offset: 0x000123AD
		internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
		{
			TarHeader.userIdAsSet = userId;
			TarHeader.defaultUserId = userId;
			TarHeader.userNameAsSet = userName;
			TarHeader.defaultUser = userName;
			TarHeader.groupIdAsSet = groupId;
			TarHeader.defaultGroupId = groupId;
			TarHeader.groupNameAsSet = groupName;
			TarHeader.defaultGroupName = groupName;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000141DF File Offset: 0x000123DF
		internal static void RestoreSetValues()
		{
			TarHeader.defaultUserId = TarHeader.userIdAsSet;
			TarHeader.defaultUser = TarHeader.userNameAsSet;
			TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
			TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001420C File Offset: 0x0001240C
		private static long ParseBinaryOrOctal(byte[] header, int offset, int length)
		{
			if (header[offset] >= 128)
			{
				long num = 0L;
				for (int i = length - 8; i < length; i++)
				{
					num = (num << 8) | (long)((ulong)header[offset + i]);
				}
				return num;
			}
			return TarHeader.ParseOctal(header, offset, length);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001424C File Offset: 0x0001244C
		public static long ParseOctal(byte[] header, int offset, int length)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			long num = 0L;
			bool flag = true;
			int num2 = offset + length;
			int num3 = offset;
			while (num3 < num2 && header[num3] != 0)
			{
				if (header[num3] != 32 && header[num3] != 48)
				{
					goto IL_0038;
				}
				if (!flag)
				{
					if (header[num3] != 32)
					{
						goto IL_0038;
					}
					break;
				}
				IL_0046:
				num3++;
				continue;
				IL_0038:
				flag = false;
				num = (num << 3) + (long)(header[num3] - 48);
				goto IL_0046;
			}
			return num;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000142A8 File Offset: 0x000124A8
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static string ParseName(byte[] header, int offset, int length)
		{
			return TarHeader.ParseName(header.AsSpan<byte>().Slice(offset, length), null);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000142D0 File Offset: 0x000124D0
		public unsafe static string ParseName(ReadOnlySpan<byte> header, Encoding encoding)
		{
			StringBuilder stringBuilder = StringBuilderPool.Instance.Rent();
			int num = 0;
			if (encoding == null)
			{
				for (int i = 0; i < header.Length; i++)
				{
					byte b = *header[i];
					if (b == 0)
					{
						break;
					}
					stringBuilder.Append((char)b);
				}
			}
			else
			{
				int num2 = 0;
				while (num2 < header.Length && *header[num2] != 0)
				{
					num2++;
					num++;
				}
				string @string = encoding.GetString(header.ToArray(), 0, num);
				stringBuilder.Append(@string);
			}
			string text = stringBuilder.ToString();
			StringBuilderPool.Instance.Return(stringBuilder);
			return text;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014367 File Offset: 0x00012567
		public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001437A File Offset: 0x0001257A
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetNameBytes(name, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00014388 File Offset: 0x00012588
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i;
			if (encoding != null)
			{
				ReadOnlySpan<char> readOnlySpan = name.AsSpan().Slice(nameOffset, Math.Min(name.Length - nameOffset, length));
				char[] array = ArrayPool<char>.Shared.Rent(readOnlySpan.Length);
				readOnlySpan.CopyTo(array);
				int bytes = encoding.GetBytes(array, 0, readOnlySpan.Length, buffer, bufferOffset);
				ArrayPool<char>.Shared.Return(array, false);
				i = Math.Min(bytes, length);
			}
			else
			{
				for (i = 0; i < length; i++)
				{
					if (nameOffset + i >= name.Length)
					{
						break;
					}
					buffer[bufferOffset + i] = (byte)name[nameOffset + i];
				}
			}
			while (i < length)
			{
				buffer[bufferOffset + i] = 0;
				i++;
			}
			return bufferOffset + length;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00014457 File Offset: 0x00012657
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014463 File Offset: 0x00012663
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name.ToString(), 0, buffer, offset, length, encoding);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014492 File Offset: 0x00012692
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
		{
			return TarHeader.GetNameBytes(name, buffer, offset, length, null);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001449E File Offset: 0x0001269E
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length, Encoding encoding)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name, 0, buffer, offset, length, encoding);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000144C8 File Offset: 0x000126C8
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			return TarHeader.GetAsciiBytes(toAdd, nameOffset, buffer, bufferOffset, length, null);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000144D8 File Offset: 0x000126D8
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length, Encoding encoding)
		{
			if (toAdd == null)
			{
				throw new ArgumentNullException("toAdd");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i;
			if (encoding == null)
			{
				for (i = 0; i < length; i++)
				{
					if (nameOffset + i >= toAdd.Length)
					{
						break;
					}
					buffer[bufferOffset + i] = (byte)toAdd[nameOffset + i];
				}
			}
			else
			{
				char[] array = toAdd.ToCharArray();
				byte[] bytes = encoding.GetBytes(array, nameOffset, Math.Min(toAdd.Length - nameOffset, length));
				i = Math.Min(bytes.Length, length);
				Array.Copy(bytes, 0, buffer, bufferOffset, i);
			}
			while (i < length)
			{
				buffer[bufferOffset + i] = 0;
				i++;
			}
			return bufferOffset + length;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014578 File Offset: 0x00012778
		public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i = length - 1;
			buffer[offset + i] = 0;
			i--;
			if (value > 0L)
			{
				long num = value;
				while (i >= 0)
				{
					if (num <= 0L)
					{
						break;
					}
					buffer[offset + i] = 48 + (byte)(num & 7L);
					num >>= 3;
					i--;
				}
			}
			while (i >= 0)
			{
				buffer[offset + i] = 48;
				i--;
			}
			return offset + length;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000145E0 File Offset: 0x000127E0
		private static int GetBinaryOrOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (value > 8589934591L)
			{
				for (int i = length - 1; i > 0; i--)
				{
					buffer[offset + i] = (byte)value;
					value >>= 8;
				}
				buffer[offset] = 128;
				return offset + length;
			}
			return TarHeader.GetOctalBytes(value, buffer, offset, length);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014628 File Offset: 0x00012828
		private static void GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014638 File Offset: 0x00012838
		private static int ComputeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < buffer.Length; i++)
			{
				num += (int)buffer[i];
			}
			return num;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001465C File Offset: 0x0001285C
		private static int MakeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < 148; i++)
			{
				num += (int)buffer[i];
			}
			for (int j = 0; j < 8; j++)
			{
				num += 32;
			}
			for (int k = 156; k < buffer.Length; k++)
			{
				num += (int)buffer[k];
			}
			return num;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000146AC File Offset: 0x000128AC
		private static int GetCTime(DateTime dateTime)
		{
			return (int)((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000146D8 File Offset: 0x000128D8
		private static DateTime GetDateTimeFromCTime(long ticks)
		{
			DateTime dateTime;
			try
			{
				dateTime = new DateTime(TarHeader.dateTime1970.Ticks + ticks * 10000000L);
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = TarHeader.dateTime1970;
			}
			return dateTime;
		}

		// Token: 0x040002DA RID: 730
		public const int NAMELEN = 100;

		// Token: 0x040002DB RID: 731
		public const int MODELEN = 8;

		// Token: 0x040002DC RID: 732
		public const int UIDLEN = 8;

		// Token: 0x040002DD RID: 733
		public const int GIDLEN = 8;

		// Token: 0x040002DE RID: 734
		public const int CHKSUMLEN = 8;

		// Token: 0x040002DF RID: 735
		public const int CHKSUMOFS = 148;

		// Token: 0x040002E0 RID: 736
		public const int SIZELEN = 12;

		// Token: 0x040002E1 RID: 737
		public const int MAGICLEN = 6;

		// Token: 0x040002E2 RID: 738
		public const int VERSIONLEN = 2;

		// Token: 0x040002E3 RID: 739
		public const int MODTIMELEN = 12;

		// Token: 0x040002E4 RID: 740
		public const int UNAMELEN = 32;

		// Token: 0x040002E5 RID: 741
		public const int GNAMELEN = 32;

		// Token: 0x040002E6 RID: 742
		public const int DEVLEN = 8;

		// Token: 0x040002E7 RID: 743
		public const int PREFIXLEN = 155;

		// Token: 0x040002E8 RID: 744
		public const byte LF_OLDNORM = 0;

		// Token: 0x040002E9 RID: 745
		public const byte LF_NORMAL = 48;

		// Token: 0x040002EA RID: 746
		public const byte LF_LINK = 49;

		// Token: 0x040002EB RID: 747
		public const byte LF_SYMLINK = 50;

		// Token: 0x040002EC RID: 748
		public const byte LF_CHR = 51;

		// Token: 0x040002ED RID: 749
		public const byte LF_BLK = 52;

		// Token: 0x040002EE RID: 750
		public const byte LF_DIR = 53;

		// Token: 0x040002EF RID: 751
		public const byte LF_FIFO = 54;

		// Token: 0x040002F0 RID: 752
		public const byte LF_CONTIG = 55;

		// Token: 0x040002F1 RID: 753
		public const byte LF_GHDR = 103;

		// Token: 0x040002F2 RID: 754
		public const byte LF_XHDR = 120;

		// Token: 0x040002F3 RID: 755
		public const byte LF_ACL = 65;

		// Token: 0x040002F4 RID: 756
		public const byte LF_GNU_DUMPDIR = 68;

		// Token: 0x040002F5 RID: 757
		public const byte LF_EXTATTR = 69;

		// Token: 0x040002F6 RID: 758
		public const byte LF_META = 73;

		// Token: 0x040002F7 RID: 759
		public const byte LF_GNU_LONGLINK = 75;

		// Token: 0x040002F8 RID: 760
		public const byte LF_GNU_LONGNAME = 76;

		// Token: 0x040002F9 RID: 761
		public const byte LF_GNU_MULTIVOL = 77;

		// Token: 0x040002FA RID: 762
		public const byte LF_GNU_NAMES = 78;

		// Token: 0x040002FB RID: 763
		public const byte LF_GNU_SPARSE = 83;

		// Token: 0x040002FC RID: 764
		public const byte LF_GNU_VOLHDR = 86;

		// Token: 0x040002FD RID: 765
		public const string TMAGIC = "ustar";

		// Token: 0x040002FE RID: 766
		public const string GNU_TMAGIC = "ustar  ";

		// Token: 0x040002FF RID: 767
		private const long timeConversionFactor = 10000000L;

		// Token: 0x04000300 RID: 768
		private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x04000301 RID: 769
		private string name;

		// Token: 0x04000302 RID: 770
		private int mode;

		// Token: 0x04000303 RID: 771
		private int userId;

		// Token: 0x04000304 RID: 772
		private int groupId;

		// Token: 0x04000305 RID: 773
		private long size;

		// Token: 0x04000306 RID: 774
		private DateTime modTime;

		// Token: 0x04000307 RID: 775
		private int checksum;

		// Token: 0x04000308 RID: 776
		private bool isChecksumValid;

		// Token: 0x04000309 RID: 777
		private byte typeFlag;

		// Token: 0x0400030A RID: 778
		private string linkName;

		// Token: 0x0400030B RID: 779
		private string magic;

		// Token: 0x0400030C RID: 780
		private string version;

		// Token: 0x0400030D RID: 781
		private string userName;

		// Token: 0x0400030E RID: 782
		private string groupName;

		// Token: 0x0400030F RID: 783
		private int devMajor;

		// Token: 0x04000310 RID: 784
		private int devMinor;

		// Token: 0x04000311 RID: 785
		internal static int userIdAsSet;

		// Token: 0x04000312 RID: 786
		internal static int groupIdAsSet;

		// Token: 0x04000313 RID: 787
		internal static string userNameAsSet;

		// Token: 0x04000314 RID: 788
		internal static string groupNameAsSet = "None";

		// Token: 0x04000315 RID: 789
		internal static int defaultUserId;

		// Token: 0x04000316 RID: 790
		internal static int defaultGroupId;

		// Token: 0x04000317 RID: 791
		internal static string defaultGroupName = "None";

		// Token: 0x04000318 RID: 792
		internal static string defaultUser;
	}
}

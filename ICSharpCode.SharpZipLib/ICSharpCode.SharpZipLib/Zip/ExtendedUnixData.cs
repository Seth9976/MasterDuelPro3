using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001E RID: 30
	public class ExtendedUnixData : ITaggedData
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004243 File Offset: 0x00002443
		public ushort TagID
		{
			get
			{
				return 21589;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000424C File Offset: 0x0000244C
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				this._flags = (ExtendedUnixData.Flags)memoryStream.ReadByte();
				if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0)
				{
					int num = memoryStream.ReadLEInt();
					this._modificationTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, num, 0);
					if (count <= 5)
					{
						return;
					}
				}
				if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
				{
					int num2 = memoryStream.ReadLEInt();
					this._lastAccessTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, num2, 0);
				}
				if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags)0)
				{
					int num3 = memoryStream.ReadLEInt();
					this._createTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, num3, 0);
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004338 File Offset: 0x00002538
		public byte[] GetData()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.WriteByte((byte)this._flags);
				if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0)
				{
					int num = (int)(this._modificationTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
					memoryStream.WriteLEInt(num);
				}
				if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
				{
					int num2 = (int)(this._lastAccessTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
					memoryStream.WriteLEInt(num2);
				}
				if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags)0)
				{
					int num3 = (int)(this._createTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
					memoryStream.WriteLEInt(num3);
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004428 File Offset: 0x00002628
		public static bool IsValidValue(DateTime value)
		{
			return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000445F File Offset: 0x0000265F
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004467 File Offset: 0x00002667
		public DateTime ModificationTime
		{
			get
			{
				return this._modificationTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.ModificationTime;
				this._modificationTime = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004491 File Offset: 0x00002691
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004499 File Offset: 0x00002699
		public DateTime AccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.AccessTime;
				this._lastAccessTime = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000044C3 File Offset: 0x000026C3
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000044CB File Offset: 0x000026CB
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.CreateTime;
				this._createTime = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000044F5 File Offset: 0x000026F5
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000044FD File Offset: 0x000026FD
		public ExtendedUnixData.Flags Include
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x040000BE RID: 190
		private ExtendedUnixData.Flags _flags;

		// Token: 0x040000BF RID: 191
		private DateTime _modificationTime = new DateTime(1970, 1, 1);

		// Token: 0x040000C0 RID: 192
		private DateTime _lastAccessTime = new DateTime(1970, 1, 1);

		// Token: 0x040000C1 RID: 193
		private DateTime _createTime = new DateTime(1970, 1, 1);

		// Token: 0x0200001F RID: 31
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x040000C3 RID: 195
			ModificationTime = 1,
			// Token: 0x040000C4 RID: 196
			AccessTime = 2,
			// Token: 0x040000C5 RID: 197
			CreateTime = 4
		}
	}
}

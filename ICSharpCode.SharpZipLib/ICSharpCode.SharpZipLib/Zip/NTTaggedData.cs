using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000020 RID: 32
	public class NTTaggedData : ITaggedData
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004544 File Offset: 0x00002744
		public ushort TagID
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004548 File Offset: 0x00002748
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				memoryStream.ReadLEInt();
				while (memoryStream.Position < memoryStream.Length)
				{
					int num = memoryStream.ReadLEShort();
					int num2 = memoryStream.ReadLEShort();
					if (num == 1)
					{
						if (num2 >= 24)
						{
							long num3 = memoryStream.ReadLELong();
							this._lastModificationTime = DateTime.FromFileTimeUtc(num3);
							long num4 = memoryStream.ReadLELong();
							this._lastAccessTime = DateTime.FromFileTimeUtc(num4);
							long num5 = memoryStream.ReadLELong();
							this._createTime = DateTime.FromFileTimeUtc(num5);
							break;
						}
						break;
					}
					else
					{
						memoryStream.Seek((long)num2, SeekOrigin.Current);
					}
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000045F0 File Offset: 0x000027F0
		public byte[] GetData()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.WriteLEInt(0);
				memoryStream.WriteLEShort(1);
				memoryStream.WriteLEShort(24);
				memoryStream.WriteLELong(this._lastModificationTime.ToFileTimeUtc());
				memoryStream.WriteLELong(this._lastAccessTime.ToFileTimeUtc());
				memoryStream.WriteLELong(this._createTime.ToFileTimeUtc());
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004670 File Offset: 0x00002870
		public static bool IsValidValue(DateTime value)
		{
			bool flag = true;
			try
			{
				value.ToFileTimeUtc();
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000046A0 File Offset: 0x000028A0
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000046A8 File Offset: 0x000028A8
		public DateTime LastModificationTime
		{
			get
			{
				return this._lastModificationTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastModificationTime = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000046C4 File Offset: 0x000028C4
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000046CC File Offset: 0x000028CC
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._createTime = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000046E8 File Offset: 0x000028E8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000046F0 File Offset: 0x000028F0
		public DateTime LastAccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastAccessTime = value;
			}
		}

		// Token: 0x040000C6 RID: 198
		private DateTime _lastAccessTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040000C7 RID: 199
		private DateTime _lastModificationTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040000C8 RID: 200
		private DateTime _createTime = DateTime.FromFileTimeUtc(0L);
	}
}

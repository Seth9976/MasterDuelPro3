using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000022 RID: 34
	public sealed class ZipExtraData : IDisposable
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000473B File Offset: 0x0000293B
		public ZipExtraData()
		{
			this.Clear();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004749 File Offset: 0x00002949
		public ZipExtraData(byte[] data)
		{
			if (data == null)
			{
				this._data = Empty.Array<byte>();
				return;
			}
			this._data = data;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004767 File Offset: 0x00002967
		public byte[] GetEntryData()
		{
			if (this.Length > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			return (byte[])this._data.Clone();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004791 File Offset: 0x00002991
		public void Clear()
		{
			if (this._data == null || this._data.Length != 0)
			{
				this._data = Empty.Array<byte>();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000047AF File Offset: 0x000029AF
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000047BC File Offset: 0x000029BC
		public Stream GetStreamForTag(int tag)
		{
			Stream stream = null;
			if (this.Find(tag))
			{
				stream = new MemoryStream(this._data, this._index, this._readValueLength, false);
			}
			return stream;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000047F0 File Offset: 0x000029F0
		public T GetData<T>() where T : class, ITaggedData, new()
		{
			T t = new T();
			if (this.Find((int)t.TagID))
			{
				t.SetData(this._data, this._readValueStart, this._readValueLength);
				return t;
			}
			return default(T);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000483E File Offset: 0x00002A3E
		public int ValueLength
		{
			get
			{
				return this._readValueLength;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004846 File Offset: 0x00002A46
		public int CurrentReadIndex
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000484E File Offset: 0x00002A4E
		public int UnreadCount
		{
			get
			{
				if (this._readValueStart > this._data.Length || this._readValueStart < 4)
				{
					throw new ZipException("Find must be called before calling a Read method");
				}
				return this._readValueStart + this._readValueLength - this._index;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004888 File Offset: 0x00002A88
		public bool Find(int headerID)
		{
			this._readValueStart = this._data.Length;
			this._readValueLength = 0;
			this._index = 0;
			int num = this._readValueStart;
			int num2 = headerID - 1;
			while (num2 != headerID && this._index < this._data.Length - 3)
			{
				num2 = this.ReadShortInternal();
				num = this.ReadShortInternal();
				if (num2 != headerID)
				{
					this._index += num;
				}
			}
			bool flag = num2 == headerID && this._index + num <= this._data.Length;
			if (flag)
			{
				this._readValueStart = this._index;
				this._readValueLength = num;
			}
			return flag;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004926 File Offset: 0x00002B26
		public void AddEntry(ITaggedData taggedData)
		{
			if (taggedData == null)
			{
				throw new ArgumentNullException("taggedData");
			}
			this.AddEntry((int)taggedData.TagID, taggedData.GetData());
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004948 File Offset: 0x00002B48
		public void AddEntry(int headerID, byte[] fieldData)
		{
			if (headerID > 65535 || headerID < 0)
			{
				throw new ArgumentOutOfRangeException("headerID");
			}
			int num = ((fieldData == null) ? 0 : fieldData.Length);
			if (num > 65535)
			{
				throw new ArgumentOutOfRangeException("fieldData", "exceeds maximum length");
			}
			int num2 = this._data.Length + num + 4;
			if (this.Find(headerID))
			{
				num2 -= this.ValueLength + 4;
			}
			if (num2 > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			this.Delete(headerID);
			byte[] array = new byte[num2];
			this._data.CopyTo(array, 0);
			int num3 = this._data.Length;
			this._data = array;
			this.SetShort(ref num3, headerID);
			this.SetShort(ref num3, num);
			if (fieldData != null)
			{
				fieldData.CopyTo(array, num3);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A0B File Offset: 0x00002C0B
		public void StartNewEntry()
		{
			this._newEntry = new MemoryStream();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004A18 File Offset: 0x00002C18
		public void AddNewEntry(int headerID)
		{
			byte[] array = this._newEntry.ToArray();
			this._newEntry = null;
			this.AddEntry(headerID, array);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004A40 File Offset: 0x00002C40
		public void AddData(byte data)
		{
			this._newEntry.WriteByte(data);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004A4E File Offset: 0x00002C4E
		public void AddData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._newEntry.Write(data, 0, data.Length);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004A6E File Offset: 0x00002C6E
		public void AddLeShort(int toAdd)
		{
			this._newEntry.WriteByte((byte)toAdd);
			this._newEntry.WriteByte((byte)(toAdd >> 8));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004A8C File Offset: 0x00002C8C
		public void AddLeInt(int toAdd)
		{
			this.AddLeShort((int)((short)toAdd));
			this.AddLeShort((int)((short)(toAdd >> 16)));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004AA1 File Offset: 0x00002CA1
		public void AddLeLong(long toAdd)
		{
			this.AddLeInt((int)(toAdd & (long)((ulong)(-1))));
			this.AddLeInt((int)(toAdd >> 32));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004ABC File Offset: 0x00002CBC
		public bool Delete(int headerID)
		{
			bool flag = false;
			if (this.Find(headerID))
			{
				flag = true;
				int num = this._readValueStart - 4;
				byte[] array = new byte[this._data.Length - (this.ValueLength + 4)];
				Array.Copy(this._data, 0, array, 0, num);
				int num2 = num + this.ValueLength + 4;
				Array.Copy(this._data, num2, array, num, this._data.Length - num2);
				this._data = array;
			}
			return flag;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004B30 File Offset: 0x00002D30
		public long ReadLong()
		{
			this.ReadCheck(8);
			return ((long)this.ReadInt() & (long)((ulong)(-1))) | ((long)this.ReadInt() << 32);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B50 File Offset: 0x00002D50
		public int ReadInt()
		{
			this.ReadCheck(4);
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8) + ((int)this._data[this._index + 2] << 16) + ((int)this._data[this._index + 3] << 24);
			this._index += 4;
			return num;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004BB7 File Offset: 0x00002DB7
		public int ReadShort()
		{
			this.ReadCheck(2);
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return num;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public int ReadByte()
		{
			int num = -1;
			if (this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index)
			{
				num = (int)this._data[this._index];
				this._index++;
			}
			return num;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004C41 File Offset: 0x00002E41
		public void Skip(int amount)
		{
			this.ReadCheck(amount);
			this._index += amount;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004C58 File Offset: 0x00002E58
		private void ReadCheck(int length)
		{
			if (this._readValueStart > this._data.Length || this._readValueStart < 4)
			{
				throw new ZipException("Find must be called before calling a Read method");
			}
			if (this._index > this._readValueStart + this._readValueLength - length)
			{
				throw new ZipException("End of extra data");
			}
			if (this._index + length < 4)
			{
				throw new ZipException("Cannot read before start of tag");
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private int ReadShortInternal()
		{
			if (this._index > this._data.Length - 2)
			{
				throw new ZipException("End of extra data");
			}
			int num = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return num;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D1B File Offset: 0x00002F1B
		private void SetShort(ref int index, int source)
		{
			this._data[index] = (byte)source;
			this._data[index + 1] = (byte)(source >> 8);
			index += 2;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004D3D File Offset: 0x00002F3D
		public void Dispose()
		{
			if (this._newEntry != null)
			{
				this._newEntry.Dispose();
			}
		}

		// Token: 0x040000C9 RID: 201
		private int _index;

		// Token: 0x040000CA RID: 202
		private int _readValueStart;

		// Token: 0x040000CB RID: 203
		private int _readValueLength;

		// Token: 0x040000CC RID: 204
		private MemoryStream _newEntry;

		// Token: 0x040000CD RID: 205
		private byte[] _data;
	}
}

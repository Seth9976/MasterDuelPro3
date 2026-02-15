using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001D RID: 29
	public class RawTaggedData : ITaggedData
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000041E7 File Offset: 0x000023E7
		public RawTaggedData(ushort tag)
		{
			this._tag = tag;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000041F6 File Offset: 0x000023F6
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000041FE File Offset: 0x000023FE
		public ushort TagID
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004207 File Offset: 0x00002407
		public void SetData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._data = new byte[count];
			Array.Copy(data, offset, this._data, 0, count);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004232 File Offset: 0x00002432
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004232 File Offset: 0x00002432
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000423A File Offset: 0x0000243A
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x040000BC RID: 188
		private ushort _tag;

		// Token: 0x040000BD RID: 189
		private byte[] _data;
	}
}

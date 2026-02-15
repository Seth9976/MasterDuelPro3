using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000023 RID: 35
	public class KeysRequiredEventArgs : EventArgs
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00004D52 File Offset: 0x00002F52
		public KeysRequiredEventArgs(string name)
		{
			this.fileName = name;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004D61 File Offset: 0x00002F61
		public KeysRequiredEventArgs(string name, byte[] keyValue)
		{
			this.fileName = name;
			this.key = keyValue;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004D77 File Offset: 0x00002F77
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004D7F File Offset: 0x00002F7F
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004D87 File Offset: 0x00002F87
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly string fileName;

		// Token: 0x040000CF RID: 207
		private byte[] key;
	}
}

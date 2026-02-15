using System;
using System.Text;

namespace UnityWebSocket
{
	// Token: 0x02000008 RID: 8
	public class MessageEventArgs : EventArgs
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002190 File Offset: 0x00000390
		internal MessageEventArgs(Opcode opcode, byte[] rawData)
		{
			this.Opcode = opcode;
			this._rawData = rawData;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000021A6 File Offset: 0x000003A6
		internal MessageEventArgs(Opcode opcode, string data)
		{
			this.Opcode = opcode;
			this._data = data;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000021C4 File Offset: 0x000003C4
		internal Opcode Opcode { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000021CD File Offset: 0x000003CD
		public string Data
		{
			get
			{
				this.SetData();
				return this._data;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021DB File Offset: 0x000003DB
		public byte[] RawData
		{
			get
			{
				this.SetRawData();
				return this._rawData;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000021E9 File Offset: 0x000003E9
		public bool IsBinary
		{
			get
			{
				return this.Opcode == Opcode.Binary;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021F4 File Offset: 0x000003F4
		public bool IsText
		{
			get
			{
				return this.Opcode == Opcode.Text;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000021FF File Offset: 0x000003FF
		private void SetData()
		{
			if (this._data != null)
			{
				return;
			}
			if (this.RawData == null)
			{
				return;
			}
			this._data = Encoding.UTF8.GetString(this.RawData);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002229 File Offset: 0x00000429
		private void SetRawData()
		{
			if (this._rawData != null)
			{
				return;
			}
			if (this._data == null)
			{
				return;
			}
			this._rawData = Encoding.UTF8.GetBytes(this._data);
		}

		// Token: 0x0400001A RID: 26
		private byte[] _rawData;

		// Token: 0x0400001B RID: 27
		private string _data;
	}
}

using System;
using System.IO;

namespace MDPro3
{
	// Token: 0x0200121C RID: 4636
	public class BinaryMaster
	{
		// Token: 0x06008973 RID: 35187 RVA: 0x0010A5B8 File Offset: 0x001087B8
		public BinaryMaster(byte[] raw = null)
		{
			if (raw == null)
			{
				this.memstream = new MemoryStream();
			}
			else
			{
				this.memstream = new MemoryStream(raw);
			}
			this.reader = new BinaryReader(this.memstream);
			this.writer = new BinaryWriter(this.memstream);
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x0010A609 File Offset: 0x00108809
		public void Set(byte[] raw)
		{
			this.memstream = new MemoryStream(raw);
			this.reader = new BinaryReader(this.memstream);
			this.writer = new BinaryWriter(this.memstream);
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x0010A639 File Offset: 0x00108839
		public byte[] Get()
		{
			return this.memstream.ToArray();
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x0010A646 File Offset: 0x00108846
		public int GetLength()
		{
			return (int)this.memstream.Length;
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x0010A654 File Offset: 0x00108854
		public override string ToString()
		{
			string return_value = "";
			byte[] bytes = this.Get();
			for (int i = 0; i < bytes.Length; i++)
			{
				string text = return_value;
				int num = (int)bytes[i];
				return_value = text + num.ToString();
				if (i < bytes.Length - 1)
				{
					return_value += ",";
				}
			}
			return return_value;
		}

		// Token: 0x0400C481 RID: 50305
		private MemoryStream memstream;

		// Token: 0x0400C482 RID: 50306
		public BinaryReader reader;

		// Token: 0x0400C483 RID: 50307
		public BinaryWriter writer;
	}
}

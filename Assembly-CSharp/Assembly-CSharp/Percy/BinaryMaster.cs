using System;
using System.IO;

namespace Percy
{
	// Token: 0x020011DA RID: 4570
	internal class BinaryMaster
	{
		// Token: 0x060087D3 RID: 34771 RVA: 0x000F76FC File Offset: 0x000F58FC
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

		// Token: 0x060087D4 RID: 34772 RVA: 0x000F774D File Offset: 0x000F594D
		public void Set(byte[] raw)
		{
			this.memstream = new MemoryStream(raw);
			this.reader = new BinaryReader(this.memstream);
			this.writer = new BinaryWriter(this.memstream);
		}

		// Token: 0x060087D5 RID: 34773 RVA: 0x000F777D File Offset: 0x000F597D
		public byte[] Get()
		{
			return this.memstream.ToArray();
		}

		// Token: 0x060087D6 RID: 34774 RVA: 0x000F778A File Offset: 0x000F598A
		public int GetLength()
		{
			return (int)this.memstream.Length;
		}

		// Token: 0x060087D7 RID: 34775 RVA: 0x000F7798 File Offset: 0x000F5998
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

		// Token: 0x0400C24F RID: 49743
		private MemoryStream memstream;

		// Token: 0x0400C250 RID: 49744
		public BinaryReader reader;

		// Token: 0x0400C251 RID: 49745
		public BinaryWriter writer;
	}
}

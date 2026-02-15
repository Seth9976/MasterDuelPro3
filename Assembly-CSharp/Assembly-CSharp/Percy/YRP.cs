using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Percy
{
	// Token: 0x020011EA RID: 4586
	public class YRP
	{
		// Token: 0x06008829 RID: 34857 RVA: 0x000F97E0 File Offset: 0x000F79E0
		public byte[] GetNamePacket()
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			if (this.playerData.Count == 4)
			{
				this.WriteUnicode(writer, this.playerData[0].name, 50);
				this.WriteUnicode(writer, this.playerData[1].name, 50);
				this.WriteUnicode(writer, this.playerData[0].name, 50);
				this.WriteUnicode(writer, this.playerData[2].name, 50);
				this.WriteUnicode(writer, this.playerData[3].name, 50);
				this.WriteUnicode(writer, this.playerData[2].name, 50);
			}
			else
			{
				this.WriteUnicode(writer, this.playerData[0].name, 50);
				this.WriteUnicode(writer, this.playerData[0].name, 50);
				this.WriteUnicode(writer, this.playerData[0].name, 50);
				this.WriteUnicode(writer, this.playerData[1].name, 50);
				this.WriteUnicode(writer, this.playerData[1].name, 50);
				this.WriteUnicode(writer, this.playerData[1].name, 50);
			}
			writer.Write(this.opt >> 16);
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			binaryWriter.Write(235);
			binaryWriter.Write(stream.ToArray());
			return ((MemoryStream)binaryWriter.BaseStream).ToArray();
		}

		// Token: 0x0600882A RID: 34858 RVA: 0x000F9988 File Offset: 0x000F7B88
		private void WriteUnicode(BinaryWriter writer, string text, int len)
		{
			byte[] unicode = Encoding.Unicode.GetBytes(text);
			byte[] result = new byte[len * 2];
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = 204;
			}
			int max = len * 2 - 2;
			Array.Copy(unicode, result, (unicode.Length > max) ? max : unicode.Length);
			result[unicode.Length] = 0;
			result[unicode.Length + 1] = 0;
			writer.Write(result);
		}

		// Token: 0x0600882B RID: 34859 RVA: 0x000F99EE File Offset: 0x000F7BEE
		public bool IsNew()
		{
			return this.ID == 846230137;
		}

		// Token: 0x0400C2FF RID: 49919
		public long DataSize;

		// Token: 0x0400C300 RID: 49920
		public int DrawCount;

		// Token: 0x0400C301 RID: 49921
		public int Flag;

		// Token: 0x0400C302 RID: 49922
		public List<byte[]> gameData = new List<byte[]>();

		// Token: 0x0400C303 RID: 49923
		public int Hash;

		// Token: 0x0400C304 RID: 49924
		public int ID;

		// Token: 0x0400C305 RID: 49925
		public uint opt;

		// Token: 0x0400C306 RID: 49926
		public List<YRP.PlayerData> playerData = new List<YRP.PlayerData>();

		// Token: 0x0400C307 RID: 49927
		public byte[] Props = new byte[8];

		// Token: 0x0400C308 RID: 49928
		public uint Seed;

		// Token: 0x0400C309 RID: 49929
		public uint[] SeedsV2 = new uint[8];

		// Token: 0x0400C30A RID: 49930
		public int StartHand;

		// Token: 0x0400C30B RID: 49931
		public int StartLp;

		// Token: 0x0400C30C RID: 49932
		public int Version;

		// Token: 0x020011EB RID: 4587
		public class PlayerData
		{
			// Token: 0x0400C30D RID: 49933
			public string name;

			// Token: 0x0400C30E RID: 49934
			public List<int> main = new List<int>();

			// Token: 0x0400C30F RID: 49935
			public List<int> extra = new List<int>();
		}
	}
}

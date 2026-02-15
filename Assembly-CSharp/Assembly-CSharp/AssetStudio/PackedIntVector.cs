using System;

namespace AssetStudio
{
	// Token: 0x0200009E RID: 158
	public class PackedIntVector
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000C82C File Offset: 0x0000AA2C
		public PackedIntVector(ObjectReader reader)
		{
			this.m_NumItems = reader.ReadUInt32();
			int numData = reader.ReadInt32();
			this.m_Data = reader.ReadBytes(numData);
			reader.AlignStream();
			this.m_BitSize = reader.ReadByte();
			reader.AlignStream();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C878 File Offset: 0x0000AA78
		public int[] UnpackInts()
		{
			int[] data = new int[this.m_NumItems];
			int indexPos = 0;
			int bitPos = 0;
			int i = 0;
			while ((long)i < (long)((ulong)this.m_NumItems))
			{
				int bits = 0;
				data[i] = 0;
				while (bits < (int)this.m_BitSize)
				{
					data[i] |= this.m_Data[indexPos] >> bitPos << bits;
					int num = Math.Min((int)this.m_BitSize - bits, 8 - bitPos);
					bitPos += num;
					bits += num;
					if (bitPos == 8)
					{
						indexPos++;
						bitPos = 0;
					}
				}
				data[i] &= (1 << (int)this.m_BitSize) - 1;
				i++;
			}
			return data;
		}

		// Token: 0x0400054C RID: 1356
		public uint m_NumItems;

		// Token: 0x0400054D RID: 1357
		public byte[] m_Data;

		// Token: 0x0400054E RID: 1358
		public byte m_BitSize;
	}
}

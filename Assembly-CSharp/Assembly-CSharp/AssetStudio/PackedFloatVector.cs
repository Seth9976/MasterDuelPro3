using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200009D RID: 157
	public class PackedFloatVector
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public PackedFloatVector(ObjectReader reader)
		{
			this.m_NumItems = reader.ReadUInt32();
			this.m_Range = reader.ReadSingle();
			this.m_Start = reader.ReadSingle();
			int numData = reader.ReadInt32();
			this.m_Data = reader.ReadBytes(numData);
			reader.AlignStream();
			this.m_BitSize = reader.ReadByte();
			reader.AlignStream();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000C724 File Offset: 0x0000A924
		public float[] UnpackFloats(int itemCountInChunk, int chunkStride, int start = 0, int numChunks = -1)
		{
			int bitPos = (int)this.m_BitSize * start;
			int indexPos = bitPos / 8;
			bitPos %= 8;
			float scale = 1f / this.m_Range;
			if (numChunks == -1)
			{
				numChunks = (int)(this.m_NumItems / (uint)itemCountInChunk);
			}
			int end = chunkStride * numChunks / 4;
			List<float> data = new List<float>();
			for (int index = 0; index != end; index += chunkStride / 4)
			{
				for (int i = 0; i < itemCountInChunk; i++)
				{
					uint x = 0U;
					int bits = 0;
					while (bits < (int)this.m_BitSize)
					{
						x |= (uint)((uint)(this.m_Data[indexPos] >> bitPos) << bits);
						int num = Math.Min((int)this.m_BitSize - bits, 8 - bitPos);
						bitPos += num;
						bits += num;
						if (bitPos == 8)
						{
							indexPos++;
							bitPos = 0;
						}
					}
					x &= (1U << (int)this.m_BitSize) - 1U;
					data.Add(x / (scale * (float)((1 << (int)this.m_BitSize) - 1)) + this.m_Start);
				}
			}
			return data.ToArray();
		}

		// Token: 0x04000547 RID: 1351
		public uint m_NumItems;

		// Token: 0x04000548 RID: 1352
		public float m_Range;

		// Token: 0x04000549 RID: 1353
		public float m_Start;

		// Token: 0x0400054A RID: 1354
		public byte[] m_Data;

		// Token: 0x0400054B RID: 1355
		public byte m_BitSize;
	}
}

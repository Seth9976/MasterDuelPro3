using System;

namespace AssetStudio
{
	// Token: 0x0200009F RID: 159
	public class PackedQuatVector
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000C920 File Offset: 0x0000AB20
		public PackedQuatVector(ObjectReader reader)
		{
			this.m_NumItems = reader.ReadUInt32();
			int numData = reader.ReadInt32();
			this.m_Data = reader.ReadBytes(numData);
			reader.AlignStream();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public Quaternion[] UnpackQuats()
		{
			Quaternion[] data = new Quaternion[this.m_NumItems];
			int indexPos = 0;
			int bitPos = 0;
			int i = 0;
			while ((long)i < (long)((ulong)this.m_NumItems))
			{
				uint flags = 0U;
				int bits = 0;
				while (bits < 3)
				{
					flags |= (uint)((uint)(this.m_Data[indexPos] >> bitPos) << bits);
					int num = Math.Min(3 - bits, 8 - bitPos);
					bitPos += num;
					bits += num;
					if (bitPos == 8)
					{
						indexPos++;
						bitPos = 0;
					}
				}
				flags &= 7U;
				Quaternion q = default(Quaternion);
				float sum = 0f;
				for (int j = 0; j < 4; j++)
				{
					if ((ulong)(flags & 3U) != (ulong)((long)j))
					{
						int bitSize = (((ulong)(((flags & 3U) + 1U) % 4U) == (ulong)((long)j)) ? 9 : 10);
						uint x = 0U;
						bits = 0;
						while (bits < bitSize)
						{
							x |= (uint)((uint)(this.m_Data[indexPos] >> bitPos) << bits);
							int num2 = Math.Min(bitSize - bits, 8 - bitPos);
							bitPos += num2;
							bits += num2;
							if (bitPos == 8)
							{
								indexPos++;
								bitPos = 0;
							}
						}
						x &= (1U << bitSize) - 1U;
						q[j] = x / (0.5f * (float)((1 << bitSize) - 1)) - 1f;
						sum += q[j] * q[j];
					}
				}
				int lastComponent = (int)(flags & 3U);
				q[lastComponent] = (float)Math.Sqrt((double)(1f - sum));
				if ((flags & 4U) != 0U)
				{
					q[lastComponent] = -q[lastComponent];
				}
				data[i] = q;
				i++;
			}
			return data;
		}

		// Token: 0x0400054F RID: 1359
		public uint m_NumItems;

		// Token: 0x04000550 RID: 1360
		public byte[] m_Data;
	}
}

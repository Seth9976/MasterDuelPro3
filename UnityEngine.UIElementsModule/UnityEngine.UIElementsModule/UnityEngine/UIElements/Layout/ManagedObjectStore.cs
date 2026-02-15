using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000572 RID: 1394
	internal class ManagedObjectStore<T> where T : class
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x0009884C File Offset: 0x00096A4C
		public ManagedObjectStore()
		{
			this.m_Chunks = new List<T[]> { new T[2048] };
			this.m_Length = 1;
			this.m_Free = new Queue<int>();
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00098894 File Offset: 0x00096A94
		public T GetValue(int index)
		{
			bool flag = index == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				int chunkIndex = index / 2048;
				int indexInChunk = index % 2048;
				t = this.m_Chunks[chunkIndex][indexInChunk];
			}
			return t;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000988E0 File Offset: 0x00096AE0
		public void UpdateValue(ref int index, T value)
		{
			bool flag = index != 0;
			if (flag)
			{
				bool flag2 = value != null;
				if (flag2)
				{
					int chunkIndex = index / 2048;
					int indexInChunk = index % 2048;
					this.m_Chunks[chunkIndex][indexInChunk] = value;
				}
				else
				{
					this.m_Free.Enqueue(index);
					int chunkIndex2 = index / 2048;
					int indexInChunk2 = index % 2048;
					this.m_Chunks[chunkIndex2][indexInChunk2] = default(T);
					index = 0;
				}
			}
			else
			{
				bool flag3 = value != null;
				if (flag3)
				{
					bool flag4 = this.m_Free.Count > 0;
					if (flag4)
					{
						index = this.m_Free.Dequeue();
						int chunkIndex3 = index / 2048;
						int indexInChunk3 = index % 2048;
						this.m_Chunks[chunkIndex3][indexInChunk3] = value;
					}
					else
					{
						int length = this.m_Length;
						this.m_Length = length + 1;
						index = length;
						bool flag5 = index >= this.m_Chunks.Count * 2048;
						if (flag5)
						{
							this.m_Chunks.Add(new T[2048]);
						}
						int chunkIndex4 = index / 2048;
						int indexInChunk4 = index % 2048;
						this.m_Chunks[chunkIndex4][indexInChunk4] = value;
					}
				}
			}
		}

		// Token: 0x04001365 RID: 4965
		private int m_Length;

		// Token: 0x04001366 RID: 4966
		private readonly List<T[]> m_Chunks;

		// Token: 0x04001367 RID: 4967
		private readonly Queue<int> m_Free;
	}
}

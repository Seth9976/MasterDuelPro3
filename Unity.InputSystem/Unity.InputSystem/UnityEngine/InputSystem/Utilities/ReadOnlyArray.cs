using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200025D RID: 605
	public struct ReadOnlyArray<TValue> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00063BAB File Offset: 0x00061DAB
		public ReadOnlyArray(TValue[] array)
		{
			this.m_Array = array;
			this.m_StartIndex = 0;
			this.m_Length = ((array != null) ? array.Length : 0);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00063BCA File Offset: 0x00061DCA
		public ReadOnlyArray(TValue[] array, int index, int length)
		{
			this.m_Array = array;
			this.m_StartIndex = index;
			this.m_Length = length;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00063BE4 File Offset: 0x00061DE4
		public TValue[] ToArray()
		{
			TValue[] result = new TValue[this.m_Length];
			if (this.m_Length > 0)
			{
				Array.Copy(this.m_Array, this.m_StartIndex, result, 0, this.m_Length);
			}
			return result;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00063C20 File Offset: 0x00061E20
		public int IndexOf(Predicate<TValue> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (predicate(this.m_Array[this.m_StartIndex + i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00063C6A File Offset: 0x00061E6A
		public ReadOnlyArray<TValue>.Enumerator GetEnumerator()
		{
			return new ReadOnlyArray<TValue>.Enumerator(this.m_Array, this.m_StartIndex, this.m_Length);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00063C83 File Offset: 0x00061E83
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00063C83 File Offset: 0x00061E83
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00063C90 File Offset: 0x00061E90
		public static implicit operator ReadOnlyArray<TValue>(TValue[] array)
		{
			return new ReadOnlyArray<TValue>(array);
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00063C98 File Offset: 0x00061E98
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170005F7 RID: 1527
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.m_Array == null)
				{
					throw new InvalidOperationException();
				}
				return this.m_Array[this.m_StartIndex + index];
			}
		}

		// Token: 0x04000CA5 RID: 3237
		internal TValue[] m_Array;

		// Token: 0x04000CA6 RID: 3238
		internal int m_StartIndex;

		// Token: 0x04000CA7 RID: 3239
		internal int m_Length;

		// Token: 0x0200025E RID: 606
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06001614 RID: 5652 RVA: 0x00063CDB File Offset: 0x00061EDB
			internal Enumerator(TValue[] array, int index, int length)
			{
				this.m_Array = array;
				this.m_IndexStart = index - 1;
				this.m_IndexEnd = index + length;
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x06001615 RID: 5653 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x06001616 RID: 5654 RVA: 0x00063D02 File Offset: 0x00061F02
			public bool MoveNext()
			{
				if (this.m_Index < this.m_IndexEnd)
				{
					this.m_Index++;
				}
				return this.m_Index != this.m_IndexEnd;
			}

			// Token: 0x06001617 RID: 5655 RVA: 0x00063D31 File Offset: 0x00061F31
			public void Reset()
			{
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001618 RID: 5656 RVA: 0x00063D3F File Offset: 0x00061F3F
			public TValue Current
			{
				get
				{
					if (this.m_Index == this.m_IndexEnd)
					{
						throw new InvalidOperationException("Iterated beyond end");
					}
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001619 RID: 5657 RVA: 0x00063D6B File Offset: 0x00061F6B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000CA8 RID: 3240
			private readonly TValue[] m_Array;

			// Token: 0x04000CA9 RID: 3241
			private readonly int m_IndexStart;

			// Token: 0x04000CAA RID: 3242
			private readonly int m_IndexEnd;

			// Token: 0x04000CAB RID: 3243
			private int m_Index;
		}
	}
}

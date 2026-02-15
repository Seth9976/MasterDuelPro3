using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003D RID: 61
	[DebuggerDisplay("Size = {size} Capacity = {capacity}")]
	public class DynamicArray<T> where T : new()
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00007832 File Offset: 0x00005A32
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000783A File Offset: 0x00005A3A
		public int size { get; protected set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00007843 File Offset: 0x00005A43
		public int capacity
		{
			get
			{
				return this.m_Array.Length;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000784D File Offset: 0x00005A4D
		public DynamicArray()
		{
			this.m_Array = new T[32];
			this.size = 0;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00007869 File Offset: 0x00005A69
		public DynamicArray(int size)
		{
			this.m_Array = new T[size];
			this.size = size;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00007884 File Offset: 0x00005A84
		public DynamicArray(int capacity, bool resize)
		{
			this.m_Array = new T[capacity];
			this.size = (resize ? capacity : 0);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000078A5 File Offset: 0x00005AA5
		public DynamicArray(DynamicArray<T> deepCopy)
		{
			this.m_Array = new T[deepCopy.size];
			this.size = deepCopy.size;
			Array.Copy(deepCopy.m_Array, this.m_Array, this.size);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000078E1 File Offset: 0x00005AE1
		public void Clear()
		{
			this.size = 0;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000078EA File Offset: 0x00005AEA
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000078FC File Offset: 0x00005AFC
		public int Add(in T value)
		{
			int index = this.size;
			if (index >= this.m_Array.Length)
			{
				T[] newArray = new T[Math.Max(this.m_Array.Length * 2, 1)];
				Array.Copy(this.m_Array, newArray, this.m_Array.Length);
				this.m_Array = newArray;
			}
			this.m_Array[index] = value;
			int size = this.size;
			this.size = size + 1;
			this.BumpVersion();
			return index;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00007978 File Offset: 0x00005B78
		public unsafe void AddRange(DynamicArray<T> array)
		{
			int addedSize = array.size;
			this.Reserve(this.size + addedSize, true);
			for (int i = 0; i < addedSize; i++)
			{
				T[] array2 = this.m_Array;
				int size = this.size;
				this.size = size + 1;
				array2[size] = *array[i];
			}
			this.BumpVersion();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000079D8 File Offset: 0x00005BD8
		public void Insert(int index, T item)
		{
			if (index == this.size)
			{
				this.Add(in item);
				return;
			}
			this.Resize(this.size + 1, true);
			Array.Copy(this.m_Array, index, this.m_Array, index + 1, this.size - index);
			this.m_Array[index] = item;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00007A34 File Offset: 0x00005C34
		public bool Remove(T item)
		{
			int index = this.IndexOf(item);
			if (index != -1)
			{
				this.RemoveAt(index);
				return true;
			}
			return false;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00007A58 File Offset: 0x00005C58
		public void RemoveAt(int index)
		{
			if (index != this.size - 1)
			{
				Array.Copy(this.m_Array, index + 1, this.m_Array, index, this.size - index - 1);
			}
			int size = this.size;
			this.size = size - 1;
			this.BumpVersion();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00007AA5 File Offset: 0x00005CA5
		public void RemoveRange(int index, int count)
		{
			if (count == 0)
			{
				return;
			}
			Array.Copy(this.m_Array, index + count, this.m_Array, index, this.size - index - count);
			this.size -= count;
			this.BumpVersion();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			for (int i = startIndex; i < this.size; i++)
			{
				if (match(this.m_Array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00007B18 File Offset: 0x00005D18
		public int IndexOf(T item, int index, int count)
		{
			int i = index;
			while (i < this.size && count > 0)
			{
				if (this.m_Array[i].Equals(item))
				{
					return i;
				}
				i++;
				count--;
			}
			return -1;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00007B64 File Offset: 0x00005D64
		public int IndexOf(T item, int index)
		{
			for (int i = index; i < this.size; i++)
			{
				if (this.m_Array[i].Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00007BA6 File Offset: 0x00005DA6
		public int IndexOf(T item)
		{
			return this.IndexOf(item, 0);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public void Resize(int newSize, bool keepContent = false)
		{
			this.Reserve(newSize, keepContent);
			this.size = newSize;
			this.BumpVersion();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00007BC7 File Offset: 0x00005DC7
		public void ResizeAndClear(int newSize)
		{
			if (newSize > this.m_Array.Length)
			{
				this.Reserve(newSize, false);
			}
			else
			{
				Array.Clear(this.m_Array, 0, newSize);
			}
			this.size = newSize;
			this.BumpVersion();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public void Reserve(int newCapacity, bool keepContent = false)
		{
			if (newCapacity > this.m_Array.Length)
			{
				if (keepContent)
				{
					T[] newArray = new T[newCapacity];
					Array.Copy(this.m_Array, newArray, this.m_Array.Length);
					this.m_Array = newArray;
					return;
				}
				this.m_Array = new T[newCapacity];
			}
		}

		// Token: 0x1700002D RID: 45
		public ref T this[int index]
		{
			get
			{
				return ref this.m_Array[index];
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00007C50 File Offset: 0x00005E50
		[Obsolete("This is deprecated because it returns an incorrect value. It may returns an array with elements beyond the size. Please use Span/ReadOnly if you want safe raw access to the DynamicArray memory.", false)]
		public static implicit operator T[](DynamicArray<T> array)
		{
			return array.m_Array;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00007C58 File Offset: 0x00005E58
		public static implicit operator ReadOnlySpan<T>(DynamicArray<T> array)
		{
			return new ReadOnlySpan<T>(array.m_Array, 0, array.size);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static implicit operator Span<T>(DynamicArray<T> array)
		{
			return new Span<T>(array.m_Array, 0, array.size);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00007C80 File Offset: 0x00005E80
		public DynamicArray<T>.Iterator GetEnumerator()
		{
			return new DynamicArray<T>.Iterator(this);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00007C88 File Offset: 0x00005E88
		public DynamicArray<T>.RangeEnumerable SubRange(int first, int numItems)
		{
			return new DynamicArray<T>.RangeEnumerable
			{
				iterator = new DynamicArray<T>.RangeEnumerable.RangeIterator(this, first, numItems)
			};
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00005704 File Offset: 0x00003904
		protected internal void BumpVersion()
		{
		}

		// Token: 0x040000C4 RID: 196
		protected T[] m_Array;

		// Token: 0x0200003E RID: 62
		public struct Iterator
		{
			// Token: 0x06000442 RID: 1090 RVA: 0x00007CAD File Offset: 0x00005EAD
			public Iterator(DynamicArray<T> setOwner)
			{
				this.owner = setOwner;
				this.index = -1;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x00007CBD File Offset: 0x00005EBD
			public ref T Current
			{
				get
				{
					return this.owner[this.index];
				}
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x00007CD0 File Offset: 0x00005ED0
			public bool MoveNext()
			{
				this.index++;
				return this.index < this.owner.size;
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x00007CF3 File Offset: 0x00005EF3
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x040000C6 RID: 198
			private readonly DynamicArray<T> owner;

			// Token: 0x040000C7 RID: 199
			private int index;
		}

		// Token: 0x0200003F RID: 63
		public struct RangeEnumerable
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x00007CFC File Offset: 0x00005EFC
			public DynamicArray<T>.RangeEnumerable.RangeIterator GetEnumerator()
			{
				return this.iterator;
			}

			// Token: 0x040000C8 RID: 200
			public DynamicArray<T>.RangeEnumerable.RangeIterator iterator;

			// Token: 0x02000040 RID: 64
			public struct RangeIterator
			{
				// Token: 0x06000447 RID: 1095 RVA: 0x00007D04 File Offset: 0x00005F04
				public RangeIterator(DynamicArray<T> setOwner, int first, int numItems)
				{
					this.owner = setOwner;
					this.first = first;
					this.index = first - 1;
					this.last = first + numItems;
				}

				// Token: 0x1700002F RID: 47
				// (get) Token: 0x06000448 RID: 1096 RVA: 0x00007D26 File Offset: 0x00005F26
				public ref T Current
				{
					get
					{
						return this.owner[this.index];
					}
				}

				// Token: 0x06000449 RID: 1097 RVA: 0x00007D39 File Offset: 0x00005F39
				public bool MoveNext()
				{
					this.index++;
					return this.index < this.last;
				}

				// Token: 0x0600044A RID: 1098 RVA: 0x00007D57 File Offset: 0x00005F57
				public void Reset()
				{
					this.index = this.first - 1;
				}

				// Token: 0x040000C9 RID: 201
				private readonly DynamicArray<T> owner;

				// Token: 0x040000CA RID: 202
				private int index;

				// Token: 0x040000CB RID: 203
				private int first;

				// Token: 0x040000CC RID: 204
				private int last;
			}
		}

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x0600044C RID: 1100
		public delegate int SortComparer(T x, T y);
	}
}

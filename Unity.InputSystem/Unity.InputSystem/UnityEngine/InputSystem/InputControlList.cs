using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200007A RID: 122
	[DebuggerDisplay("Count = {Count}")]
	public struct InputControlList<TControl> : IList<TControl>, ICollection<TControl>, IEnumerable<TControl>, IEnumerable, IReadOnlyList<TControl>, IReadOnlyCollection<TControl>, IDisposable where TControl : InputControl
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00017497 File Offset: 0x00015697
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001749F File Offset: 0x0001569F
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000174BC File Offset: 0x000156BC
		public int Capacity
		{
			get
			{
				if (!this.m_Indices.IsCreated)
				{
					return 0;
				}
				return this.m_Indices.Length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Capacity cannot be negative", "value");
				}
				if (value == 0)
				{
					if (this.m_Count != 0)
					{
						this.m_Indices.Dispose();
					}
					this.m_Count = 0;
					return;
				}
				Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
				ArrayHelpers.Resize<ulong>(ref this.m_Indices, value, allocator);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001751C File Offset: 0x0001571C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019F RID: 415
		public TControl this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				return InputControlList<TControl>.FromIndex(this.m_Indices[index]);
			}
			set
			{
				if (index < 0 || index >= this.m_Count)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} entries", index, this.m_Count));
				}
				this.m_Indices[index] = InputControlList<TControl>.ToIndex(value);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000175C6 File Offset: 0x000157C6
		public InputControlList(Allocator allocator, int initialCapacity = 0)
		{
			this.m_Allocator = allocator;
			this.m_Indices = default(NativeArray<ulong>);
			this.m_Count = 0;
			if (initialCapacity != 0)
			{
				this.Capacity = initialCapacity;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000175EC File Offset: 0x000157EC
		public InputControlList(IEnumerable<TControl> values, Allocator allocator = Allocator.Persistent)
		{
			this = new InputControlList<TControl>(allocator, 0);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (TControl value in values)
			{
				this.Add(value);
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001764C File Offset: 0x0001584C
		public InputControlList(params TControl[] values)
		{
			this = default(InputControlList<TControl>);
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int count = values.Length;
			this.Capacity = Mathf.Max(count, 10);
			for (int i = 0; i < count; i++)
			{
				this.Add(values[i]);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001769C File Offset: 0x0001589C
		public unsafe void Resize(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "Size cannot be negative");
			}
			if (this.Capacity < size)
			{
				this.Capacity = size;
			}
			if (size > this.Count)
			{
				UnsafeUtility.MemSet((void*)((byte*)this.m_Indices.GetUnsafePtr<ulong>() + this.Count * 8), byte.MaxValue, (long)(size - this.Count));
			}
			this.m_Count = size;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00017704 File Offset: 0x00015904
		public void Add(TControl item)
		{
			ulong index = InputControlList<TControl>.ToIndex(item);
			Allocator allocator = ((this.m_Allocator != Allocator.Invalid) ? this.m_Allocator : Allocator.Persistent);
			ArrayHelpers.AppendWithCapacity<ulong>(ref this.m_Indices, ref this.m_Count, index, 10, allocator);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00017740 File Offset: 0x00015940
		public void AddSlice<TList>(TList list, int count = -1, int destinationIndex = -1, int sourceIndex = 0) where TList : IReadOnlyList<TControl>
		{
			if (count < 0)
			{
				count = list.Count;
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (sourceIndex + count > list.Count)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count of {0} elements starting at index {1} exceeds length of list of {2}", count, sourceIndex, list.Count));
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			for (int i = 0; i < count; i++)
			{
				this.m_Indices[destinationIndex + i] = InputControlList<TControl>.ToIndex(list[sourceIndex + i]);
			}
			this.m_Count += count;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00017840 File Offset: 0x00015A40
		public void AddRange(IEnumerable<TControl> list, int count = -1, int destinationIndex = -1)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (count < 0)
			{
				count = list.Count<TControl>();
			}
			if (destinationIndex < 0)
			{
				destinationIndex = this.Count;
			}
			if (count == 0)
			{
				return;
			}
			if (this.Capacity < this.m_Count + count)
			{
				this.Capacity = Math.Max(this.m_Count + count, 10);
			}
			if (destinationIndex < this.Count)
			{
				NativeArray<ulong>.Copy(this.m_Indices, destinationIndex, this.m_Indices, destinationIndex + count, this.Count - destinationIndex);
			}
			foreach (TControl element in list)
			{
				this.m_Indices[destinationIndex++] = InputControlList<TControl>.ToIndex(element);
				this.m_Count++;
				count--;
				if (count == 0)
				{
					break;
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00017928 File Offset: 0x00015B28
		public bool Remove(TControl item)
		{
			if (this.m_Count == 0)
			{
				return false;
			}
			ulong index = InputControlList<TControl>.ToIndex(item);
			for (int i = 0; i < this.m_Count; i++)
			{
				if (this.m_Indices[i] == index)
				{
					ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001797C File Offset: 0x00015B7C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index {0} is out of range in list with {1} elements", index, this.m_Count));
			}
			ArrayHelpers.EraseAtWithCapacity<ulong>(this.m_Indices, ref this.m_Count, index);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000179CE File Offset: 0x00015BCE
		public void CopyTo(TControl[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000179D5 File Offset: 0x00015BD5
		public int IndexOf(TControl item)
		{
			return this.IndexOf(item, 0, -1);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000179E0 File Offset: 0x00015BE0
		public unsafe int IndexOf(TControl item, int startIndex, int count = -1)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be negative");
			}
			if (this.m_Count == 0)
			{
				return -1;
			}
			if (count < 0)
			{
				count = Mathf.Max(this.m_Count - startIndex, 0);
			}
			if (startIndex + count > this.m_Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			ulong index = InputControlList<TControl>.ToIndex(item);
			ulong* indices = (ulong*)this.m_Indices.GetUnsafeReadOnlyPtr<ulong>();
			for (int i = 0; i < count; i++)
			{
				if (indices[startIndex + i] == index)
				{
					return startIndex + i;
				}
			}
			return -1;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000179CE File Offset: 0x00015BCE
		public void Insert(int index, TControl item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00017A65 File Offset: 0x00015C65
		public void Clear()
		{
			this.m_Count = 0;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00017A6E File Offset: 0x00015C6E
		public bool Contains(TControl item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00017A7D File Offset: 0x00015C7D
		public bool Contains(TControl item, int startIndex, int count = -1)
		{
			return this.IndexOf(item, startIndex, count) != -1;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00017A90 File Offset: 0x00015C90
		public void SwapElements(int index1, int index2)
		{
			if (index1 < 0 || index1 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index1");
			}
			if (index2 < 0 || index2 >= this.m_Count)
			{
				throw new ArgumentOutOfRangeException("index2");
			}
			if (index1 != index2)
			{
				this.m_Indices.SwapElements(index1, index2);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00017AE0 File Offset: 0x00015CE0
		public void Sort<TCompare>(int startIndex, int count, TCompare comparer) where TCompare : IComparer<TControl>
		{
			if (startIndex < 0 || startIndex >= this.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (startIndex + count >= this.Count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (int i = 1; i < count; i++)
			{
				int j = i;
				while (j > 0 && comparer.Compare(this[j - 1], this[j]) < 0)
				{
					this.SwapElements(j, j - 1);
					j--;
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00017B60 File Offset: 0x00015D60
		public TControl[] ToArray(bool dispose = false)
		{
			TControl[] result = new TControl[this.m_Count];
			for (int i = 0; i < this.m_Count; i++)
			{
				result[i] = this[i];
			}
			if (dispose)
			{
				this.Dispose();
			}
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00017BA4 File Offset: 0x00015DA4
		internal void AppendTo(ref TControl[] array, ref int count)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				ArrayHelpers.AppendWithCapacity<TControl>(ref array, ref count, this[i], 10);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00017BD3 File Offset: 0x00015DD3
		public void Dispose()
		{
			if (this.m_Indices.IsCreated)
			{
				this.m_Indices.Dispose();
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00017BED File Offset: 0x00015DED
		public IEnumerator<TControl> GetEnumerator()
		{
			return new InputControlList<TControl>.Enumerator(this);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00017BFF File Offset: 0x00015DFF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00017C08 File Offset: 0x00015E08
		public override string ToString()
		{
			if (this.Count == 0)
			{
				return "()";
			}
			StringBuilder builder = new StringBuilder();
			builder.Append('(');
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					builder.Append(',');
				}
				builder.Append(this[i]);
			}
			builder.Append(')');
			return builder.ToString();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00017C74 File Offset: 0x00015E74
		private static ulong ToIndex(TControl control)
		{
			if (control == null)
			{
				return ulong.MaxValue;
			}
			InputDevice device = control.device;
			int deviceId = device.m_DeviceId;
			int controlIndex = ((device != control) ? (device.m_ChildrenForEachControl.IndexOfReference(control, -1) + 1) : 0);
			ulong num = (ulong)((ulong)((long)deviceId) << 32);
			ulong unsignedControlIndex = (ulong)((long)controlIndex);
			return num | unsignedControlIndex;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00017CCC File Offset: 0x00015ECC
		private static TControl FromIndex(ulong index)
		{
			if (index == 18446744073709551615UL)
			{
				return default(TControl);
			}
			int num = (int)(index >> 32);
			int controlIndex = (int)(index & (ulong)(-1));
			InputDevice device = InputSystem.GetDeviceById(num);
			if (device == null)
			{
				return default(TControl);
			}
			if (controlIndex == 0)
			{
				return (TControl)((object)device);
			}
			return (TControl)((object)device.m_ChildrenForEachControl[controlIndex - 1]);
		}

		// Token: 0x040002BF RID: 703
		private int m_Count;

		// Token: 0x040002C0 RID: 704
		private NativeArray<ulong> m_Indices;

		// Token: 0x040002C1 RID: 705
		private readonly Allocator m_Allocator;

		// Token: 0x040002C2 RID: 706
		private const ulong kInvalidIndex = 18446744073709551615UL;

		// Token: 0x0200007B RID: 123
		private struct Enumerator : IEnumerator<TControl>, IEnumerator, IDisposable
		{
			// Token: 0x060005E7 RID: 1511 RVA: 0x00017D1F File Offset: 0x00015F1F
			public unsafe Enumerator(InputControlList<TControl> list)
			{
				this.m_Count = list.m_Count;
				this.m_Current = -1;
				this.m_Indices = (ulong*)((this.m_Count > 0) ? list.m_Indices.GetUnsafeReadOnlyPtr<ulong>() : null);
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x00017D52 File Offset: 0x00015F52
			public bool MoveNext()
			{
				if (this.m_Current >= this.m_Count)
				{
					return false;
				}
				this.m_Current++;
				return this.m_Current != this.m_Count;
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x00017D83 File Offset: 0x00015F83
			public void Reset()
			{
				this.m_Current = -1;
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x00017D8C File Offset: 0x00015F8C
			public unsafe TControl Current
			{
				get
				{
					if (this.m_Indices == null)
					{
						throw new InvalidOperationException("Enumerator is not valid");
					}
					return InputControlList<TControl>.FromIndex(this.m_Indices[this.m_Current]);
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060005EB RID: 1515 RVA: 0x00017DB9 File Offset: 0x00015FB9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x040002C3 RID: 707
			private unsafe readonly ulong* m_Indices;

			// Token: 0x040002C4 RID: 708
			private readonly int m_Count;

			// Token: 0x040002C5 RID: 709
			private int m_Current;
		}
	}
}

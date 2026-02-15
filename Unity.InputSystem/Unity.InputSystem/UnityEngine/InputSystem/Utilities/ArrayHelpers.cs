using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000231 RID: 561
	internal static class ArrayHelpers
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0005DA7C File Offset: 0x0005BC7C
		public static int LengthSafe<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0005DA86 File Offset: 0x0005BC86
		public static void Clear<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0005DA96 File Offset: 0x0005BC96
		public static void Clear<TValue>(this TValue[] array, int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0005DAA4 File Offset: 0x0005BCA4
		public static void Clear<TValue>(this TValue[] array, ref int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
			count = 0;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0005DAB6 File Offset: 0x0005BCB6
		public static void EnsureCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (capacity == 0)
			{
				return;
			}
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			if (array.Length - count >= capacity)
			{
				return;
			}
			ArrayHelpers.DuplicateWithCapacity<TValue>(ref array, count, capacity, capacityIncrement);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0005DAE4 File Offset: 0x0005BCE4
		public static void DuplicateWithCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			TValue[] newArray = new TValue[count + Math.Max(capacity, capacityIncrement)];
			Array.Copy(array, newArray, count);
			array = newArray;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0005DB20 File Offset: 0x0005BD20
		public static bool Contains<TValue>(TValue[] array, TValue value)
		{
			if (array == null)
			{
				return false;
			}
			EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
			for (int i = 0; i < array.Length; i++)
			{
				if (comparer.Equals(array[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0005DB59 File Offset: 0x0005BD59
		public static bool ContainsReference<TValue>(this TValue[] array, TValue value) where TValue : class
		{
			return array != null && array.ContainsReference(array.Length, value);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0005DB6A File Offset: 0x0005BD6A
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, count) != -1;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005DB7A File Offset: 0x0005BD7A
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int startIndex, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, startIndex, count) != -1;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0005DB8C File Offset: 0x0005BD8C
		public static bool HaveDuplicateReferences<TFirst>(this TFirst[] first, int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				TFirst element = first[i];
				for (int j = i + 1; j < count - i; j++)
				{
					if (element == first[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005DBD4 File Offset: 0x0005BDD4
		public static bool HaveEqualElements<TValue>(TValue[] first, TValue[] second, int count = 2147483647)
		{
			if (first == null || second == null)
			{
				return second == first;
			}
			int lengthFirst = Math.Min(count, first.Length);
			int lengthSecond = Math.Min(count, second.Length);
			if (lengthFirst != lengthSecond)
			{
				return false;
			}
			EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
			for (int i = 0; i < lengthFirst; i++)
			{
				if (!comparer.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0005DC34 File Offset: 0x0005BE34
		public static int IndexOf<TValue>(TValue[] array, TValue value, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (comparer.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0005DC78 File Offset: 0x0005BE78
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate)
		{
			if (array == null)
			{
				return -1;
			}
			int length = array.Length;
			for (int i = 0; i < length; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005DCAC File Offset: 0x0005BEAC
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			int end = startIndex + ((count < 0) ? (array.Length - startIndex) : count);
			for (int i = startIndex; i < end; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005DCEB File Offset: 0x0005BEEB
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int count = -1) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, 0, count);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0005DCF8 File Offset: 0x0005BEF8
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int startIndex, int count) where TFirst : TSecond where TSecond : class
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0005DD3C File Offset: 0x0005BF3C
		public static int IndexOfValue<TValue>(this TValue[] array, TValue value, int startIndex = 0, int count = -1) where TValue : struct, IEquatable<TValue>
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (value.Equals(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005DD80 File Offset: 0x0005BF80
		public static void Resize<TValue>(ref NativeArray<TValue> array, int newSize, Allocator allocator) where TValue : struct
		{
			int oldSize = array.Length;
			if (oldSize == newSize)
			{
				return;
			}
			if (newSize == 0)
			{
				if (array.IsCreated)
				{
					array.Dispose();
				}
				array = default(NativeArray<TValue>);
				return;
			}
			NativeArray<TValue> newArray = new NativeArray<TValue>(newSize, allocator, NativeArrayOptions.ClearMemory);
			if (oldSize != 0)
			{
				UnsafeUtility.MemCpy(newArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)(UnsafeUtility.SizeOf<TValue>() * ((newSize < oldSize) ? newSize : oldSize)));
				array.Dispose();
			}
			array = newArray;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
		public static int Append<TValue>(ref TValue[] array, TValue value)
		{
			if (array == null)
			{
				array = new TValue[1];
				array[0] = value;
				return 0;
			}
			int length = array.Length;
			Array.Resize<TValue>(ref array, length + 1);
			array[length] = value;
			return length;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0005DE30 File Offset: 0x0005C030
		public static int Append<TValue>(ref TValue[] array, IEnumerable<TValue> values)
		{
			if (array == null)
			{
				array = values.ToArray<TValue>();
				return 0;
			}
			int oldLength = array.Length;
			int valueCount = values.Count<TValue>();
			Array.Resize<TValue>(ref array, oldLength + valueCount);
			int index = oldLength;
			foreach (TValue value in values)
			{
				array[index++] = value;
			}
			return oldLength;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005DEA8 File Offset: 0x0005C0A8
		public static int AppendToImmutable<TValue>(ref TValue[] array, TValue[] values)
		{
			if (array == null)
			{
				array = values;
				return 0;
			}
			if (values != null && values.Length != 0)
			{
				int oldCount = array.Length;
				int valueCount = values.Length;
				Array.Resize<TValue>(ref array, oldCount + valueCount);
				Array.Copy(values, 0, array, oldCount, valueCount);
				return oldCount;
			}
			return array.Length;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
		public static int AppendWithCapacity<TValue>(ref TValue[] array, ref int count, TValue value, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[capacityIncrement];
				array[0] = value;
				count++;
				return 0;
			}
			int capacity = array.Length;
			if (capacity == count)
			{
				capacity += capacityIncrement;
				Array.Resize<TValue>(ref array, capacity);
			}
			int index = count;
			array[index] = value;
			count++;
			return index;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005DF3C File Offset: 0x0005C13C
		public static int AppendListWithCapacity<TValue, TValues>(ref TValue[] array, ref int length, TValues values, int capacityIncrement = 10) where TValues : IReadOnlyList<TValue>
		{
			int numToAdd = values.Count;
			if (array == null)
			{
				int size = Math.Max(numToAdd, capacityIncrement);
				array = new TValue[size];
				for (int i = 0; i < numToAdd; i++)
				{
					array[i] = values[i];
				}
				length += numToAdd;
				return 0;
			}
			int capacity = array.Length;
			if (capacity < length + numToAdd)
			{
				capacity += Math.Max(length + numToAdd, capacityIncrement);
				Array.Resize<TValue>(ref array, capacity);
			}
			int index = length;
			for (int j = 0; j < numToAdd; j++)
			{
				array[index + j] = values[j];
			}
			length += numToAdd;
			return index;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005DFF0 File Offset: 0x0005C1F0
		public static int AppendWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, TValue value, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length == count)
			{
				ArrayHelpers.GrowBy<TValue>(ref array, (capacityIncrement > 1) ? capacityIncrement : 1, allocator);
			}
			int index = count;
			array[index] = value;
			count++;
			return index;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0005E02C File Offset: 0x0005C22C
		public static void InsertAt<TValue>(ref TValue[] array, int index, TValue value)
		{
			if (array != null)
			{
				int oldLength = array.Length;
				Array.Resize<TValue>(ref array, oldLength + 1);
				if (index != oldLength)
				{
					Array.Copy(array, index, array, index + 1, oldLength - index);
				}
				array[index] = value;
				return;
			}
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			array = new TValue[1];
			array[0] = value;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005E088 File Offset: 0x0005C288
		public static void InsertAtWithCapacity<TValue>(ref TValue[] array, ref int count, int index, TValue value, int capacityIncrement = 10)
		{
			ArrayHelpers.EnsureCapacity<TValue>(ref array, count, count + 1, capacityIncrement);
			if (index != count)
			{
				Array.Copy(array, index, array, index + 1, count - index);
			}
			array[index] = value;
			count++;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0005E0C0 File Offset: 0x0005C2C0
		public static void PutAtIfNotSet<TValue>(ref TValue[] array, int index, Func<TValue> valueFn)
		{
			if (array.LengthSafe<TValue>() < index + 1)
			{
				Array.Resize<TValue>(ref array, index + 1);
			}
			if (EqualityComparer<TValue>.Default.Equals(array[index], default(TValue)))
			{
				array[index] = valueFn();
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005E110 File Offset: 0x0005C310
		public static int GrowBy<TValue>(ref TValue[] array, int count)
		{
			if (array == null)
			{
				array = new TValue[count];
				return 0;
			}
			int oldLength = array.Length;
			Array.Resize<TValue>(ref array, oldLength + count);
			return oldLength;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005E13C File Offset: 0x0005C33C
		public static int GrowBy<TValue>(ref NativeArray<TValue> array, int count, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			int length = array.Length;
			if (length == 0)
			{
				array = new NativeArray<TValue>(count, allocator, NativeArrayOptions.ClearMemory);
				return 0;
			}
			NativeArray<TValue> newArray = new NativeArray<TValue>(length + count, allocator, NativeArrayOptions.ClearMemory);
			UnsafeUtility.MemCpy(newArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)length * (long)UnsafeUtility.SizeOf<TValue>());
			array.Dispose();
			array = newArray;
			return length;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005E19C File Offset: 0x0005C39C
		public static int GrowWithCapacity<TValue>(ref TValue[] array, ref int count, int growBy, int capacityIncrement = 10)
		{
			if (((array != null) ? array.Length : 0) < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005E1C6 File Offset: 0x0005C3C6
		public static int GrowWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, int growBy, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement, allocator);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005E1F0 File Offset: 0x0005C3F0
		public static TValue[] Join<TValue>(TValue value, params TValue[] values)
		{
			int length = 0;
			if (value != null)
			{
				length++;
			}
			if (values != null)
			{
				length += values.Length;
			}
			if (length == 0)
			{
				return null;
			}
			TValue[] array = new TValue[length];
			int index = 0;
			if (value != null)
			{
				array[index++] = value;
			}
			if (values != null)
			{
				Array.Copy(values, 0, array, index, values.Length);
			}
			return array;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005E248 File Offset: 0x0005C448
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second) where TValue : IEquatable<TValue>
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return first;
			}
			List<TValue> merged = new List<TValue>();
			merged.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!merged.Exists((TValue x) => x.Equals(secondValue)))
				{
					merged.Add(secondValue);
				}
			}
			return merged.ToArray();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005E2B4 File Offset: 0x0005C4B4
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second, IEqualityComparer<TValue> comparer)
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return null;
			}
			List<TValue> merged = new List<TValue>();
			merged.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!merged.Exists((TValue x) => comparer.Equals(secondValue)))
				{
					merged.Add(secondValue);
				}
			}
			return merged.ToArray();
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0005E334 File Offset: 0x0005C534
		public static void EraseAt<TValue>(ref TValue[] array, int index)
		{
			int length = array.Length;
			if (index == 0 && length == 1)
			{
				array = null;
				return;
			}
			if (index < length - 1)
			{
				Array.Copy(array, index + 1, array, index, length - index - 1);
			}
			Array.Resize<TValue>(ref array, length - 1);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005E374 File Offset: 0x0005C574
		public static void EraseAtWithCapacity<TValue>(this TValue[] array, ref int count, int index)
		{
			if (index < count - 1)
			{
				Array.Copy(array, index + 1, array, index, count - index - 1);
			}
			array[count - 1] = default(TValue);
			count--;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0005E3B4 File Offset: 0x0005C5B4
		public unsafe static void EraseAtWithCapacity<TValue>(NativeArray<TValue> array, ref int count, int index) where TValue : struct
		{
			if (index < count - 1)
			{
				int elementSize = UnsafeUtility.SizeOf<TValue>();
				byte* arrayPtr = (byte*)array.GetUnsafePtr<TValue>();
				UnsafeUtility.MemCpy((void*)(arrayPtr + elementSize * index), (void*)(arrayPtr + elementSize * (index + 1)), (long)((count - index - 1) * elementSize));
			}
			count--;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005E3F8 File Offset: 0x0005C5F8
		public static bool Erase<TValue>(ref TValue[] array, TValue value)
		{
			int index = ArrayHelpers.IndexOf<TValue>(array, value, 0, -1);
			if (index != -1)
			{
				ArrayHelpers.EraseAt<TValue>(ref array, index);
				return true;
			}
			return false;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0005E420 File Offset: 0x0005C620
		public static void EraseAtByMovingTail<TValue>(TValue[] array, ref int count, int index)
		{
			if (index != count - 1)
			{
				array[index] = array[count - 1];
			}
			if (count >= 1)
			{
				array[count - 1] = default(TValue);
			}
			count--;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005E464 File Offset: 0x0005C664
		public static TValue[] Copy<TValue>(TValue[] array)
		{
			if (array == null)
			{
				return null;
			}
			int length = array.Length;
			TValue[] result = new TValue[length];
			Array.Copy(array, result, length);
			return result;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0005E48C File Offset: 0x0005C68C
		public static TValue[] Clone<TValue>(TValue[] array) where TValue : ICloneable
		{
			if (array == null)
			{
				return null;
			}
			int count = array.Length;
			TValue[] result = new TValue[count];
			for (int i = 0; i < count; i++)
			{
				result[i] = (TValue)((object)array[i].Clone());
			}
			return result;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005E4D8 File Offset: 0x0005C6D8
		public static TNew[] Select<TOld, TNew>(TOld[] array, Func<TOld, TNew> converter)
		{
			if (array == null)
			{
				return null;
			}
			int length = array.Length;
			TNew[] result = new TNew[length];
			for (int i = 0; i < length; i++)
			{
				result[i] = converter(array[i]);
			}
			return result;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005E518 File Offset: 0x0005C718
		private static void Swap<TValue>(ref TValue first, ref TValue second)
		{
			TValue temp = first;
			first = second;
			second = temp;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005E540 File Offset: 0x0005C740
		public static void MoveSlice<TValue>(TValue[] array, int sourceIndex, int destinationIndex, int count)
		{
			if (count <= 0 || sourceIndex == destinationIndex)
			{
				return;
			}
			int elementCount;
			if (destinationIndex > sourceIndex)
			{
				elementCount = destinationIndex + count - sourceIndex;
			}
			else
			{
				elementCount = sourceIndex + count - destinationIndex;
			}
			if (elementCount == count * 2)
			{
				for (int i = 0; i < count; i++)
				{
					ArrayHelpers.Swap<TValue>(ref array[sourceIndex + i], ref array[destinationIndex + i]);
				}
				return;
			}
			int swapCount = elementCount - 1;
			int dst = destinationIndex;
			for (int j = 0; j < swapCount; j++)
			{
				ArrayHelpers.Swap<TValue>(ref array[dst], ref array[sourceIndex]);
				if (destinationIndex > sourceIndex)
				{
					dst -= count;
					if (dst < sourceIndex)
					{
						dst = destinationIndex + count - Math.Abs(sourceIndex - dst);
					}
				}
				else
				{
					dst += count;
					if (dst >= sourceIndex + count)
					{
						dst = destinationIndex + (dst - (sourceIndex + count));
					}
				}
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005E5E8 File Offset: 0x0005C7E8
		public static void EraseSliceWithCapacity<TValue>(ref TValue[] array, ref int length, int index, int count)
		{
			if (count < length)
			{
				Array.Copy(array, index + count, array, index, length - index - count);
			}
			for (int i = 0; i < count; i++)
			{
				array[length - i - 1] = default(TValue);
			}
			length -= count;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005E635 File Offset: 0x0005C835
		public static void SwapElements<TValue>(this TValue[] array, int index1, int index2)
		{
			MemoryHelpers.Swap<TValue>(ref array[index1], ref array[index2]);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005E64C File Offset: 0x0005C84C
		public static void SwapElements<TValue>(this NativeArray<TValue> array, int index1, int index2) where TValue : struct
		{
			TValue temp = array[index1];
			array[index1] = array[index2];
			array[index2] = temp;
		}
	}
}

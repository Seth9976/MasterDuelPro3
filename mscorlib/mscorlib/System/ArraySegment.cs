using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics.Hashing;
using System.Reflection;

namespace System
{
	/// <summary>Delimits a section of a one-dimensional array.</summary>
	/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C0 RID: 192
	[DefaultMember("Item")]
	[Serializable]
	public readonly struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000189EE File Offset: 0x00016BEE
		public static ArraySegment<T> Empty { get; } = new ArraySegment<T>(new T[0]);

		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits all the elements in the specified array.</summary>
		/// <param name="array">The array to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x000189F5 File Offset: 0x00016BF5
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits the specified range of the elements in the specified array.</summary>
		/// <param name="array">The array containing the range of elements to delimit.</param>
		/// <param name="offset">The zero-based index of the first element in the range.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> do not specify a valid range in <paramref name="array" />.</exception>
		// Token: 0x060004B2 RID: 1202 RVA: 0x00018A17 File Offset: 0x00016C17
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null || offset > array.Length || count > array.Length - offset)
			{
				ThrowHelper.ThrowArraySegmentCtorValidationFailedExceptions(array, offset, count);
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		/// <summary>Gets the original array containing the range of elements that the array segment delimits.</summary>
		/// <returns>The original array that was passed to the constructor, and that contains the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00018A47 File Offset: 0x00016C47
		public T[] Array
		{
			get
			{
				return this._array;
			}
		}

		/// <summary>Gets the position of the first element in the range delimited by the array segment, relative to the start of the original array.</summary>
		/// <returns>The position of the first element in the range delimited by the <see cref="T:System.ArraySegment`1" />, relative to the start of the original array.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00018A4F File Offset: 0x00016C4F
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the number of elements in the range delimited by the array segment.</summary>
		/// <returns>The number of elements in the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00018A57 File Offset: 0x00016C57
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00018A5F File Offset: 0x00016C5F
		public ArraySegment<T>.Enumerator GetEnumerator()
		{
			this.ThrowInvalidOperationIfDefault();
			return new ArraySegment<T>.Enumerator(this);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060004B7 RID: 1207 RVA: 0x00018A72 File Offset: 0x00016C72
		public override int GetHashCode()
		{
			if (this._array == null)
			{
				return 0;
			}
			return global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.Combine(5381, this._offset), this._count) ^ this._array.GetHashCode();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00018AA5 File Offset: 0x00016CA5
		public void CopyTo(T[] destination, int destinationIndex)
		{
			this.ThrowInvalidOperationIfDefault();
			global::System.Array.Copy(this._array, this._offset, destination, destinationIndex, this._count);
		}

		/// <summary>Determines whether the specified object is equal to the current instance.</summary>
		/// <returns>true if the specified object is a <see cref="T:System.ArraySegment`1" /> structure and is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with the current instance.</param>
		// Token: 0x060004B9 RID: 1209 RVA: 0x00018AC6 File Offset: 0x00016CC6
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ArraySegment`1" /> structure to be compared with the current instance.</param>
		// Token: 0x060004BA RID: 1210 RVA: 0x00018ADE File Offset: 0x00016CDE
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.ArraySegment`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the array segment is read-only.</exception>
		// Token: 0x1700006D RID: 109
		T IList<T>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
			set
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._array[this._offset + index] = value;
			}
		}

		/// <summary>Determines the index of a specific item in the array segment.</summary>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
		/// <param name="item">The object to locate in the array segment.</param>
		// Token: 0x060004BD RID: 1213 RVA: 0x00018B68 File Offset: 0x00016D68
		int IList<T>.IndexOf(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			int num = global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		/// <summary>Inserts an item into the array segment at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the array segment.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the array segment.</exception>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x060004BE RID: 1214 RVA: 0x00018BA2 File Offset: 0x00016DA2
		void IList<T>.Insert(int index, T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		/// <summary>Removes the array segment item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the array segment.</exception>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x00018BA2 File Offset: 0x00016DA2
		void IList<T>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		/// <summary>Gets the element at the specified index of the array segment.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.ArraySegment`1" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
		// Token: 0x1700006E RID: 110
		T IReadOnlyList<T>.this[int index]
		{
			get
			{
				this.ThrowInvalidOperationIfDefault();
				if (index < 0 || index >= this._count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._array[this._offset + index];
			}
		}

		/// <summary>Gets a value that indicates whether the array segment  is read-only.</summary>
		/// <returns>true if the array segment is read-only; otherwise, false.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000C091 File Offset: 0x0000A291
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Adds an item to the array segment.</summary>
		/// <param name="item">The object to add to the array segment.</param>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x060004C2 RID: 1218 RVA: 0x00018BA2 File Offset: 0x00016DA2
		void ICollection<T>.Add(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		/// <summary>Removes all items from the array segment.</summary>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only. </exception>
		// Token: 0x060004C3 RID: 1219 RVA: 0x00018BA2 File Offset: 0x00016DA2
		void ICollection<T>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException();
		}

		/// <summary>Determines whether the array segment contains a specific value.</summary>
		/// <returns>true if <paramref name="item" /> is found in the array segment; otherwise, false.</returns>
		/// <param name="item">The object to locate in the array segment.</param>
		// Token: 0x060004C4 RID: 1220 RVA: 0x00018BA9 File Offset: 0x00016DA9
		bool ICollection<T>.Contains(T item)
		{
			this.ThrowInvalidOperationIfDefault();
			return global::System.Array.IndexOf<T>(this._array, item, this._offset, this._count) >= 0;
		}

		/// <summary>Removes the first occurrence of a specific object from the array segment.</summary>
		/// <returns>true if <paramref name="item" /> was successfully removed from the array segment; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the array segment.</returns>
		/// <param name="item">The object to remove from the array segment.</param>
		/// <exception cref="T:System.NotSupportedException">The array segment is read-only.</exception>
		// Token: 0x060004C5 RID: 1221 RVA: 0x00018BCF File Offset: 0x00016DCF
		bool ICollection<T>.Remove(T item)
		{
			ThrowHelper.ThrowNotSupportedException();
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the array segment.</summary>
		/// <returns>An enumerator that can be used to iterate through the array segment.</returns>
		// Token: 0x060004C6 RID: 1222 RVA: 0x00018BD7 File Offset: 0x00016DD7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through an array segment.</summary>
		/// <returns>An enumerator that can be used to iterate through the array segment.</returns>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00018BD7 File Offset: 0x00016DD7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00018BE4 File Offset: 0x00016DE4
		private void ThrowInvalidOperationIfDefault()
		{
			if (this._array == null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NullArray);
			}
		}

		// Token: 0x040002BA RID: 698
		private readonly T[] _array;

		// Token: 0x040002BB RID: 699
		private readonly int _offset;

		// Token: 0x040002BC RID: 700
		private readonly int _count;

		// Token: 0x020000C1 RID: 193
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060004CA RID: 1226 RVA: 0x00018C07 File Offset: 0x00016E07
			internal Enumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment.Array;
				this._start = arraySegment.Offset;
				this._end = arraySegment.Offset + arraySegment.Count;
				this._current = arraySegment.Offset - 1;
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00018C47 File Offset: 0x00016E47
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00018C75 File Offset: 0x00016E75
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumNotStarted();
					}
					if (this._current >= this._end)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumEnded();
					}
					return this._array[this._current];
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x00018CAE File Offset: 0x00016EAE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00018CBB File Offset: 0x00016EBB
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00002C89 File Offset: 0x00000E89
			public void Dispose()
			{
			}

			// Token: 0x040002BD RID: 701
			private readonly T[] _array;

			// Token: 0x040002BE RID: 702
			private readonly int _start;

			// Token: 0x040002BF RID: 703
			private readonly int _end;

			// Token: 0x040002C0 RID: 704
			private int _current;
		}
	}
}

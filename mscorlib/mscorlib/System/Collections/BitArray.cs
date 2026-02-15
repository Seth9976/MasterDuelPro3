using System;
using System.Threading;

namespace System.Collections
{
	/// <summary>Manages a compact array of bit values, which are represented as Booleans, where true indicates that the bit is on (1) and false indicates the bit is off (0).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000717 RID: 1815
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to false.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero. </exception>
		// Token: 0x0600390F RID: 14607 RVA: 0x000DE345 File Offset: 0x000DC545
		public BitArray(int length)
			: this(length, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to the specified value.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />. </param>
		/// <param name="defaultValue">The Boolean value to assign to each bit. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero. </exception>
		// Token: 0x06003910 RID: 14608 RVA: 0x000DE350 File Offset: 0x000DC550
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Non-negative number required.");
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = (defaultValue ? (-1) : 0);
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of 32-bit integers.</summary>
		/// <param name="values">An array of integers containing the values to copy, where each integer represents 32 consecutive bits. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="values" /> is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x06003911 RID: 14609 RVA: 0x000DE3C4 File Offset: 0x000DC5C4
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(SR.Format("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", 32), "values");
			}
			this.m_array = new int[values.Length];
			Array.Copy(values, 0, this.m_array, 0, values.Length);
			this.m_length = values.Length * 32;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="bits">The <see cref="T:System.Collections.BitArray" /> to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is null. </exception>
		// Token: 0x06003912 RID: 14610 RVA: 0x000DE440 File Offset: 0x000DC640
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			Array.Copy(bits.m_array, 0, this.m_array, 0, arrayLength);
			this.m_length = bits.m_length;
			this._version = bits._version;
		}

		/// <summary>Gets or sets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <param name="index">The zero-based index of the value to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.BitArray.Count" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008FE RID: 2302
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		/// <summary>Gets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <param name="index">The zero-based index of the value to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003915 RID: 14613 RVA: 0x000DE4BA File Offset: 0x000DC6BA
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (this.m_array[index / 32] & (1 << index % 32)) != 0;
		}

		/// <summary>Sets the bit at a specific position in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="index">The zero-based index of the bit to set. </param>
		/// <param name="value">The Boolean value to assign to the bit. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003916 RID: 14614 RVA: 0x000DE4F8 File Offset: 0x000DC6F8
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		/// <summary>Sets all bits in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="value">The Boolean value to assign to all bits. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003917 RID: 14615 RVA: 0x000DE574 File Offset: 0x000DC774
		public void SetAll(bool value)
		{
			int num = (value ? (-1) : 0);
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		/// <summary>Performs the bitwise OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The current instance containing the result of the bitwise OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.BitArray" /> with which to perform the bitwise OR operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003918 RID: 14616 RVA: 0x000DE5BC File Offset: 0x000DC7BC
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Gets or sets the number of elements in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value that is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x000DE631 File Offset: 0x000DC831
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x000DE63C File Offset: 0x000DC83C
		public int Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					Array.Resize<int>(ref this.m_array, arrayLength);
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.BitArray" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.BitArray" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.BitArray" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.BitArray" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600391B RID: 14619 RVA: 0x000DE6FC File Offset: 0x000DC8FC
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			int[] array2 = array as int[];
			if (array2 != null)
			{
				int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
				int num2 = this.m_length % 32;
				if (num2 == 0)
				{
					Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32));
					return;
				}
				Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32) - 1);
				array2[index + num] = this.m_array[num] & ((1 << num2) - 1);
				return;
			}
			else if (array is byte[])
			{
				int num3 = this.m_length % 8;
				int num4 = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < num4)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (num3 > 0)
				{
					num4--;
				}
				byte[] array3 = (byte[])array;
				for (int i = 0; i < num4; i++)
				{
					array3[index + i] = (byte)((this.m_array[i / 4] >> i % 4 * 8) & 255);
				}
				if (num3 > 0)
				{
					int num5 = num4;
					array3[index + num5] = (byte)((this.m_array[num5 / 4] >> num5 % 4 * 8) & ((1 << num3) - 1));
					return;
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException("Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].", "array");
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				bool[] array4 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array4[index + j] = ((this.m_array[j / 32] >> j % 32) & 1) != 0;
				}
				return;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000DE631 File Offset: 0x000DC831
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000DE8E4 File Offset: 0x000DCAE4
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.BitArray" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600391F RID: 14623 RVA: 0x000DE906 File Offset: 0x000DCB06
		public object Clone()
		{
			return new BitArray(this);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003920 RID: 14624 RVA: 0x000DE90E File Offset: 0x000DCB0E
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000DE916 File Offset: 0x000DCB16
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x04001E89 RID: 7817
		private int[] m_array;

		// Token: 0x04001E8A RID: 7818
		private int m_length;

		// Token: 0x04001E8B RID: 7819
		private int _version;

		// Token: 0x04001E8C RID: 7820
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000718 RID: 1816
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06003922 RID: 14626 RVA: 0x000DE925 File Offset: 0x000DCB25
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x06003923 RID: 14627 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06003924 RID: 14628 RVA: 0x000DE948 File Offset: 0x000DCB48
			public virtual bool MoveNext()
			{
				ICollection collection = this.bitarray;
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.index < collection.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = collection.Count;
				return false;
			}

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06003925 RID: 14629 RVA: 0x000DE9BE File Offset: 0x000DCBBE
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this.index >= ((ICollection)this.bitarray).Count)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this.currentElement;
				}
			}

			// Token: 0x06003926 RID: 14630 RVA: 0x000DE9FD File Offset: 0x000DCBFD
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.index = -1;
			}

			// Token: 0x04001E8D RID: 7821
			private BitArray bitarray;

			// Token: 0x04001E8E RID: 7822
			private int index;

			// Token: 0x04001E8F RID: 7823
			private int version;

			// Token: 0x04001E90 RID: 7824
			private bool currentElement;
		}
	}
}

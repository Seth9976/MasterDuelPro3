using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Represents a mutable string of characters. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002F6 RID: 758
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class StringBuilder : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class.</summary>
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00065795 File Offset: 0x00063995
		public StringBuilder()
		{
			this.m_MaxCapacity = int.MaxValue;
			this.m_ChunkChars = new char[16];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified capacity.</summary>
		/// <param name="capacity">The suggested starting size of this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001AC9 RID: 6857 RVA: 0x000657B5 File Offset: 0x000639B5
		public StringBuilder(int capacity)
			: this(capacity, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		// Token: 0x06001ACA RID: 6858 RVA: 0x000657C3 File Offset: 0x000639C3
		public StringBuilder(string value)
			: this(value, 16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001ACB RID: 6859 RVA: 0x000657CE File Offset: 0x000639CE
		public StringBuilder(string value, int capacity)
			: this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="startIndex">The position within <paramref name="value" /> where the substring begins. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is not a position within <paramref name="value" />. </exception>
		// Token: 0x06001ACC RID: 6860 RVA: 0x000657E8 File Offset: 0x000639E8
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format("'{0}' must be non-negative.", "length"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			capacity = Math.Max(capacity, length);
			this.m_ChunkChars = new char[capacity];
			this.m_ChunkLength = length;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				StringBuilder.ThreadSafeCopy(ptr + startIndex, this.m_ChunkChars, 0, length);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class that starts with a specified capacity and can grow to a specified maximum.</summary>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <param name="maxCapacity">The maximum number of characters the current string can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxCapacity" /> is less than one, <paramref name="capacity" /> is less than zero, or <paramref name="capacity" /> is greater than <paramref name="maxCapacity" />. </exception>
		// Token: 0x06001ACD RID: 6861 RVA: 0x000658C8 File Offset: 0x00063AC8
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity exceeds maximum capacity.");
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", "MaxCapacity must be one or greater.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = new char[capacity];
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00065944 File Offset: 0x00063B44
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException("The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length.");
			}
			if (!flag)
			{
				num = Math.Min(Math.Max(16, text.Length), num2);
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException("The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length.");
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = new char[num];
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
			this.m_ChunkPrevious = null;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001ACF RID: 6863 RVA: 0x00065A74 File Offset: 0x00063C74
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		/// <summary>Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.</summary>
		/// <returns>The maximum number of characters that can be contained in the memory allocated by the current instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.-or- The value specified for a set operation is greater than the maximum capacity. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00065ACE File Offset: 0x00063CCE
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00065AE0 File Offset: 0x00063CE0
		public int Capacity
		{
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity must be positive.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity exceeds maximum capacity.");
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (this.Capacity != value)
				{
					char[] array = new char[value - this.m_ChunkOffset];
					Array.Copy(this.m_ChunkChars, 0, array, 0, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		/// <summary>Gets the maximum capacity of this instance.</summary>
		/// <returns>The maximum number of characters this instance can hold.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00065B65 File Offset: 0x00063D65
		public int MaxCapacity
		{
			get
			{
				return this.m_MaxCapacity;
			}
		}

		/// <summary>Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder" /> is at least the specified value.</summary>
		/// <returns>The new capacity of this instance.</returns>
		/// <param name="capacity">The minimum capacity to ensure. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AD3 RID: 6867 RVA: 0x00065B6D File Offset: 0x00063D6D
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		/// <summary>Converts the value of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00065B9C File Offset: 0x00063D9C
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (;;)
				{
					if (stringBuilder.m_ChunkLength > 0)
					{
						char[] chunkChars = stringBuilder.m_ChunkChars;
						int chunkOffset = stringBuilder.m_ChunkOffset;
						int chunkLength = stringBuilder.m_ChunkLength;
						if (chunkLength + chunkOffset > text.Length || chunkLength > chunkChars.Length)
						{
							break;
						}
						fixed (char* ptr2 = &chunkChars[0])
						{
							char* ptr3 = ptr2;
							string.wstrcpy(ptr + chunkOffset, ptr3, chunkLength);
						}
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						return text;
					}
				}
				throw new ArgumentOutOfRangeException("chunkLength", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		/// <summary>Converts the value of a substring of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as the specified substring of this instance.</returns>
		/// <param name="startIndex">The starting position of the substring in this instance. </param>
		/// <param name="length">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the length of the current instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AD5 RID: 6869 RVA: 0x00065C4C File Offset: 0x00063E4C
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be larger than length of string.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			string text2;
			string text = (text2 = string.FastAllocateString(length));
			char* ptr = text2;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			this.CopyTo(startIndex, new Span<char>((void*)ptr, length), length);
			return text;
		}

		/// <summary>Removes all characters from the current <see cref="T:System.Text.StringBuilder" /> instance.</summary>
		/// <returns>An object whose <see cref="P:System.Text.StringBuilder.Length" /> is 0 (zero).</returns>
		// Token: 0x06001AD6 RID: 6870 RVA: 0x00065CD7 File Offset: 0x00063ED7
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		/// <summary>Gets or sets the length of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>The length of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00065CE1 File Offset: 0x00063EE1
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x00065CF0 File Offset: 0x00063EF0
		public int Length
		{
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Length cannot be less than zero.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					int num2 = Math.Min(this.Capacity, Math.Max(this.Length * 6 / 5, this.m_ChunkChars.Length)) - stringBuilder.m_ChunkOffset;
					if (num2 > stringBuilder.m_ChunkChars.Length)
					{
						char[] array = new char[num2];
						Array.Copy(stringBuilder.m_ChunkChars, 0, array, 0, stringBuilder.m_ChunkLength);
						this.m_ChunkChars = array;
					}
					else
					{
						this.m_ChunkChars = stringBuilder.m_ChunkChars;
					}
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		/// <summary>Gets or sets the character at the specified character position in this instance.</summary>
		/// <returns>The Unicode character at position <paramref name="index" />.</returns>
		/// <param name="index">The position of the character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while setting a character. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while getting a character. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E8 RID: 744
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The character to append. </param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="repeatCount" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001ADB RID: 6875 RVA: 0x00065E90 File Offset: 0x00064090
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "Count cannot be less than zero.");
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.Length + repeatCount;
			if (num > this.m_MaxCapacity || num < repeatCount)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "The length cannot be greater than the capacity.");
			}
			int num2 = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num2 < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num2++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num2;
					this.ExpandByABlock(repeatCount);
					num2 = 0;
				}
			}
			this.m_ChunkLength = num2;
			return this;
		}

		/// <summary>Appends the string representation of a specified subarray of Unicode characters to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">A character array. </param>
		/// <param name="startIndex">The starting position in <paramref name="value" />. </param>
		/// <param name="charCount">The number of characters to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.-or- <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> + <paramref name="charCount" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001ADC RID: 6876 RVA: 0x00065F20 File Offset: 0x00064120
		public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value must be positive.");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("charCount", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, charCount);
					return this;
				}
			}
		}

		/// <summary>Appends a copy of the specified string to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001ADD RID: 6877 RVA: 0x00065FA0 File Offset: 0x000641A0
		public unsafe StringBuilder Append(string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				int num = chunkLength + length;
				if (num < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						fixed (string text = value)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (char* ptr2 = &chunkChars[chunkLength])
							{
								string.wstrcpy(ptr2, ptr, length);
							}
						}
					}
					this.m_ChunkLength = num;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00066038 File Offset: 0x00064238
		private unsafe void AppendHelper(string value)
		{
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr, value.Length);
			}
		}

		/// <summary>Appends a copy of a specified substring to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string that contains the substring to append. </param>
		/// <param name="startIndex">The starting position of the substring within <paramref name="value" />. </param>
		/// <param name="count">The number of characters in <paramref name="value" /> to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="count" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero.-or- <paramref name="startIndex" /> less than zero.-or- <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001ADF RID: 6879 RVA: 0x00066068 File Offset: 0x00064268
		public unsafe StringBuilder Append(string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				char* ptr = value;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr + startIndex, count);
				return this;
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000660F2 File Offset: 0x000642F2
		public StringBuilder Append(StringBuilder value)
		{
			if (value != null && value.Length != 0)
			{
				return this.AppendCore(value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00066110 File Offset: 0x00064310
		public StringBuilder Append(StringBuilder value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (count > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this.AppendCore(value, startIndex, count);
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00066184 File Offset: 0x00064384
		private StringBuilder AppendCore(StringBuilder value, int startIndex, int count)
		{
			if (value == this)
			{
				return this.Append(value.ToString(startIndex, count));
			}
			if (this.Length + count > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("Capacity", "Capacity exceeds maximum capacity.");
			}
			while (count > 0)
			{
				int num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				if (num == 0)
				{
					this.ExpandByABlock(count);
					num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				}
				value.CopyTo(startIndex, new Span<char>(this.m_ChunkChars, this.m_ChunkLength, num), num);
				this.m_ChunkLength += num;
				startIndex += num;
				count -= num;
			}
			return this;
		}

		/// <summary>Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE3 RID: 6883 RVA: 0x00066231 File Offset: 0x00064431
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006623E File Offset: 0x0006443E
		public StringBuilder AppendLine(string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00066254 File Offset: 0x00064454
		public void CopyTo(int sourceIndex, Span<char> destination, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException("Source string was not long enough. Check sourceIndex and count.");
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					num3 = Math.Min(num3, stringBuilder.m_ChunkLength);
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after insertion has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <param name="count">The number of times to insert <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> times <paramref name="count" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE6 RID: 6886 RVA: 0x00066308 File Offset: 0x00064508
		public unsafe StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (string.IsNullOrEmpty(value) || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			char* ptr = value;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			while (count > 0)
			{
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
				count--;
			}
			return this;
		}

		/// <summary>Removes the specified range of characters from this instance.</summary>
		/// <returns>A reference to this instance after the excise operation has completed.</returns>
		/// <param name="startIndex">The zero-based position in this instance where removal begins. </param>
		/// <param name="length">The number of characters to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, or <paramref name="startIndex" /> + <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE7 RID: 6887 RVA: 0x000663B8 File Offset: 0x000645B8
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified Boolean value to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Boolean value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006642E File Offset: 0x0006462E
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified Unicode character to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Unicode character to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AE9 RID: 6889 RVA: 0x00066440 File Offset: 0x00064640
		public StringBuilder Append(char value)
		{
			if (this.m_ChunkLength < this.m_ChunkChars.Length)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				this.m_ChunkLength = chunkLength + 1;
				chunkChars[chunkLength] = value;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified 8-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AEA RID: 6890 RVA: 0x00066482 File Offset: 0x00064682
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.AppendSpanFormattable<sbyte>(value);
		}

		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AEB RID: 6891 RVA: 0x0006648B File Offset: 0x0006468B
		public StringBuilder Append(byte value)
		{
			return this.AppendSpanFormattable<byte>(value);
		}

		/// <summary>Appends the string representation of a specified 16-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AEC RID: 6892 RVA: 0x00066494 File Offset: 0x00064694
		public StringBuilder Append(short value)
		{
			return this.AppendSpanFormattable<short>(value);
		}

		/// <summary>Appends the string representation of a specified 32-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AED RID: 6893 RVA: 0x0006649D File Offset: 0x0006469D
		public StringBuilder Append(int value)
		{
			return this.AppendSpanFormattable<int>(value);
		}

		/// <summary>Appends the string representation of a specified 64-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AEE RID: 6894 RVA: 0x000664A6 File Offset: 0x000646A6
		public StringBuilder Append(long value)
		{
			return this.AppendSpanFormattable<long>(value);
		}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AEF RID: 6895 RVA: 0x000664AF File Offset: 0x000646AF
		public StringBuilder Append(float value)
		{
			return this.AppendSpanFormattable<float>(value);
		}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x000664B8 File Offset: 0x000646B8
		public StringBuilder Append(double value)
		{
			return this.AppendSpanFormattable<double>(value);
		}

		/// <summary>Appends the string representation of a specified decimal number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x000664C1 File Offset: 0x000646C1
		public StringBuilder Append(decimal value)
		{
			return this.AppendSpanFormattable<decimal>(value);
		}

		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000664CA File Offset: 0x000646CA
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.AppendSpanFormattable<ushort>(value);
		}

		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x000664D3 File Offset: 0x000646D3
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.AppendSpanFormattable<uint>(value);
		}

		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x000664DC File Offset: 0x000646DC
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.AppendSpanFormattable<ulong>(value);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000664E5 File Offset: 0x000646E5
		private StringBuilder AppendSpanFormattable<T>(T value) where T : IFormattable
		{
			return this.Append(value.ToString(null, CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified object to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The object to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00066500 File Offset: 0x00064700
		public StringBuilder Append(object value)
		{
			if (value != null)
			{
				return this.Append(value.ToString());
			}
			return this;
		}

		/// <summary>Appends the string representation of the Unicode characters in a specified array to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The array of characters to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF7 RID: 6903 RVA: 0x00066514 File Offset: 0x00064714
		public unsafe StringBuilder Append(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00066544 File Offset: 0x00064744
		public unsafe StringBuilder Append(ReadOnlySpan<char> value)
		{
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr = reference;
					this.Append(ptr, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance. -or-The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AF9 RID: 6905 RVA: 0x00066578 File Offset: 0x00064778
		public unsafe StringBuilder Insert(int index, string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value != null)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AFA RID: 6906 RVA: 0x000665C2 File Offset: 0x000647C2
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001AFB RID: 6907 RVA: 0x000665D0 File Offset: 0x000647D0
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AFC RID: 6908 RVA: 0x000665E1 File Offset: 0x000647E1
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 2. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AFD RID: 6909 RVA: 0x000665F1 File Offset: 0x000647F1
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AFE RID: 6910 RVA: 0x00066602 File Offset: 0x00064802
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An array of objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AFF RID: 6911 RVA: 0x00066615 File Offset: 0x00064815
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006663D File Offset: 0x0006483D
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006664D File Offset: 0x0006484D
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006665F File Offset: 0x0006485F
		private static void FormatError()
		{
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006666C File Offset: 0x0006486C
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			StringBuilder stringBuilder = null;
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_0091;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_0091:
				if (num == length)
				{
					return this;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				string text = null;
				ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
				if (c == ':')
				{
					num++;
					int num4 = num;
					for (;;)
					{
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						num++;
						if (c == '}' || c == '{')
						{
							if (c == '{')
							{
								if (num < length && format[num] == '{')
								{
									num++;
								}
								else
								{
									StringBuilder.FormatError();
								}
							}
							else
							{
								if (num >= length || format[num] != '}')
								{
									break;
								}
								num++;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.Append(format, num4, num - num4 - 1);
							num4 = num;
						}
					}
					num--;
					if (stringBuilder == null || stringBuilder.Length == 0)
					{
						if (num4 != num)
						{
							readOnlySpan = format.AsSpan(num4, num - num4);
						}
					}
					else
					{
						stringBuilder.Append(format, num4, num - num4);
						readOnlySpan = (text = stringBuilder.ToString());
						stringBuilder.Clear();
					}
				}
				if (c != '}')
				{
					StringBuilder.FormatError();
				}
				num++;
				string text2 = null;
				if (customFormatter != null)
				{
					if (readOnlySpan.Length != 0 && text == null)
					{
						text = new string(readOnlySpan);
					}
					text2 = customFormatter.Format(text, obj, provider);
				}
				if (text2 == null)
				{
					ISpanFormattable spanFormattable = obj as ISpanFormattable;
					int num5;
					if (spanFormattable != null && (flag || num3 == 0) && spanFormattable.TryFormat(this.RemainingCurrentChunk, out num5, readOnlySpan, provider))
					{
						this.m_ChunkLength += num5;
						int num6 = num3 - num5;
						if (flag && num6 > 0)
						{
							this.Append(' ', num6);
							continue;
						}
						continue;
					}
					else
					{
						IFormattable formattable = obj as IFormattable;
						if (formattable != null)
						{
							if (readOnlySpan.Length != 0 && text == null)
							{
								text = new string(readOnlySpan);
							}
							text2 = formattable.ToString(text, provider);
						}
						else if (obj != null)
						{
							text2 = obj.ToString();
						}
					}
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				int num7 = num3 - text2.Length;
				if (!flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
				this.Append(text2);
				if (flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
			}
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" />.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B04 RID: 6916 RVA: 0x00066A54 File Offset: 0x00064C54
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance and <paramref name="sb" /> have equal string, <see cref="P:System.Text.StringBuilder.Capacity" />, and <see cref="P:System.Text.StringBuilder.MaxCapacity" /> values; otherwise, false.</returns>
		/// <param name="sb">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B05 RID: 6917 RVA: 0x00066A68 File Offset: 0x00064C68
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Capacity != sb.Capacity || this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_0049:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_007F:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_8;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_0049;
					}
				}
				goto IL_007F;
			}
			Block_8:
			return j < 0;
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <param name="startIndex">The position in this instance where the substring begins. </param>
		/// <param name="count">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> indicates a character position not within this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B06 RID: 6918 RVA: 0x00066B1C File Offset: 0x00064D1C
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "oldValue");
			}
			newValue = newValue ?? string.Empty;
			int length2 = newValue.Length;
			int length3 = oldValue.Length;
			int[] array = null;
			int num = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num2 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num2, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num >= array.Length)
					{
						Array.Resize<int>(ref array, array.Length * 3 / 2 + 4);
					}
					array[num++] = num2;
					num2 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num2++;
					count--;
				}
				if (num2 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num3 = num2 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num, stringBuilder, oldValue.Length, newValue);
					num3 += (newValue.Length - oldValue.Length) * num;
					num = 0;
					stringBuilder = this.FindChunkForIndex(num3);
					num2 = num3 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00066C74 File Offset: 0x00064E74
		[CLSCompliant(false)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", "Count cannot be less than zero.");
			}
			int num = this.Length + valueCount;
			if (num > this.m_MaxCapacity || num < valueCount)
			{
				throw new ArgumentOutOfRangeException("valueCount", "The length cannot be greater than the capacity.");
			}
			int num2 = valueCount + this.m_ChunkLength;
			if (num2 <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num2;
			}
			else
			{
				int num3 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num3 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num3);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num4 = valueCount - num3;
				this.ExpandByABlock(num4);
				StringBuilder.ThreadSafeCopy(value + num3, this.m_ChunkChars, 0, num4);
				this.m_ChunkLength = num4;
			}
			return this;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00066D48 File Offset: 0x00064F48
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00066D8C File Offset: 0x00064F8C
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = (value.Length - removeCount) * replacementsCount;
				StringBuilder stringBuilder = sourceChunk;
				int num2 = replacements[0];
				if (num > 0)
				{
					this.MakeRoom(stringBuilder.m_ChunkOffset + num2, num, out stringBuilder, out num2, true);
				}
				int num3 = 0;
				for (;;)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					int num4 = replacements[num3] + removeCount;
					num3++;
					if (num3 >= replacementsCount)
					{
						break;
					}
					int num5 = replacements[num3];
					if (num != 0)
					{
						fixed (char* ptr2 = &sourceChunk.m_ChunkChars[num4])
						{
							char* ptr3 = ptr2;
							this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr3, num5 - num4);
						}
					}
					else
					{
						num2 += num5 - num4;
					}
				}
				if (num < 0)
				{
					this.Remove(stringBuilder.m_ChunkOffset + num2, -num, out stringBuilder, out num2);
				}
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00066E60 File Offset: 0x00065060
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00066EC0 File Offset: 0x000650C0
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int num = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num);
					indexInChunk += num;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num;
					if (count == 0)
					{
						break;
					}
					value += num;
				}
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00066F28 File Offset: 0x00065128
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					string.wstrcpy(ptr, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00066F6C File Offset: 0x0006516C
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, Span<char> destination, int destinationIndex, int count)
		{
			if (count > 0)
			{
				if (sourceIndex > source.Length || count > source.Length - sourceIndex)
				{
					throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (destinationIndex > destination.Length || count > destination.Length - destinationIndex)
				{
					throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				fixed (char* ptr = &source[sourceIndex])
				{
					char* ptr2 = ptr;
					fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
					{
						string.wstrcpy(reference + destinationIndex, ptr2, count);
					}
				}
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00066FEC File Offset: 0x000651EC
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0006700E File Offset: 0x0006520E
		private Span<char> RemainingCurrentChunk
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Span<char>(this.m_ChunkChars, this.m_ChunkLength, this.m_ChunkChars.Length - this.m_ChunkLength);
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00067030 File Offset: 0x00065230
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk != this)
			{
				return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
			}
			return null;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0006704C File Offset: 0x0006524C
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length > this.m_MaxCapacity || minBlockCharCount + this.Length < minBlockCharCount)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			if (this.m_ChunkOffset + num < num)
			{
				this.m_ChunkChars = null;
				throw new OutOfMemoryException();
			}
			this.m_ChunkChars = new char[num];
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000670E8 File Offset: 0x000652E8
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00067138 File Offset: 0x00065338
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doNotMoveFollowingChars)
		{
			if (count + this.Length > this.m_MaxCapacity || count + this.Length < count)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doNotMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				fixed (char* ptr = &chunk.m_ChunkChars[0])
				{
					char* ptr2 = ptr;
					StringBuilder.ThreadSafeCopy(ptr2, stringBuilder.m_ChunkChars, 0, num);
					int num2 = indexInChunk - num;
					if (num2 >= 0)
					{
						StringBuilder.ThreadSafeCopy(ptr2 + num, chunk.m_ChunkChars, 0, num2);
						indexInChunk = num2;
					}
				}
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0006729C File Offset: 0x0006549C
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = new char[size];
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000672D4 File Offset: 0x000654D4
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int num4 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, num4);
			}
		}

		// Token: 0x04000C98 RID: 3224
		internal char[] m_ChunkChars;

		// Token: 0x04000C99 RID: 3225
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04000C9A RID: 3226
		internal int m_ChunkLength;

		// Token: 0x04000C9B RID: 3227
		internal int m_ChunkOffset;

		// Token: 0x04000C9C RID: 3228
		internal int m_MaxCapacity;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D0 RID: 464
	[ComVisible(true)]
	[Serializable]
	public readonly struct IntPtr : ISerializable, IEquatable<IntPtr>
	{
		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit signed integer. </param>
		// Token: 0x06001223 RID: 4643 RVA: 0x0004994C File Offset: 0x00047B4C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 64-bit pointer.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit signed integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large or too small to represent as an <see cref="T:System.IntPtr" />. </exception>
		// Token: 0x06001224 RID: 4644 RVA: 0x00049956 File Offset: 0x00047B56
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(long value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type. </param>
		// Token: 0x06001225 RID: 4645 RVA: 0x00049960 File Offset: 0x00047B60
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004996C File Offset: 0x00047B6C
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			this.m_value = @int;
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle in this process, measured in bytes. The value of this property is 4 in a 32-bit process, and 8 in a 64-bit process. You can define the process type by setting the /platform switch when you compile your code with the C# and Visual Basic compilers.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004998D File Offset: 0x00047B8D
		public unsafe static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return sizeof(void*);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.IntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06001228 RID: 4648 RVA: 0x00049995 File Offset: 0x00047B95
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToInt64());
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.IntPtr" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001229 RID: 4649 RVA: 0x000499B6 File Offset: 0x00047BB6
		public override bool Equals(object obj)
		{
			return obj is IntPtr && ((IntPtr)obj).m_value == this.m_value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600122A RID: 4650 RVA: 0x000499D5 File Offset: 0x00047BD5
		public override int GetHashCode()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large or too small to represent as a 32-bit signed integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600122B RID: 4651 RVA: 0x000499D5 File Offset: 0x00047BD5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int ToInt32()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer equal to the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600122C RID: 4652 RVA: 0x000499DE File Offset: 0x00047BDE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public long ToInt64()
		{
			if (IntPtr.Size == 4)
			{
				return (long)this.m_value;
			}
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600122D RID: 4653 RVA: 0x000499F8 File Offset: 0x00047BF8
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600122E RID: 4654 RVA: 0x00049A00 File Offset: 0x00047C00
		public override string ToString()
		{
			return this.ToString(null);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of the current <see cref="T:System.IntPtr" /> object.</returns>
		/// <param name="format">A format specification that governs how the current <see cref="T:System.IntPtr" /> object is converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600122F RID: 4655 RVA: 0x00049A0C File Offset: 0x00047C0C
		public string ToString(string format)
		{
			if (IntPtr.Size == 4)
			{
				return this.m_value.ToString(format, null);
			}
			return this.m_value.ToString(format, null);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are equal.</summary>
		/// <returns>true if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001230 RID: 4656 RVA: 0x00049A44 File Offset: 0x00047C44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are not equal.</summary>
		/// <returns>true if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001231 RID: 4657 RVA: 0x00049A56 File Offset: 0x00047C56
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		/// <summary>Converts the value of a 32-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 32-bit signed integer. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001232 RID: 4658 RVA: 0x00049A6B File Offset: 0x00047C6B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of a 64-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 64-bit signed integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.IntPtr" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001233 RID: 4659 RVA: 0x00049A73 File Offset: 0x00047C73
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A pointer to an unspecified type. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001234 RID: 4660 RVA: 0x00049A7B File Offset: 0x00047C7B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 32-bit signed integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit signed integer. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001235 RID: 4661 RVA: 0x00049A83 File Offset: 0x00047C83
		public static explicit operator int(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 64-bit signed integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001236 RID: 4662 RVA: 0x00049A8D File Offset: 0x00047C8D
		public static explicit operator long(IntPtr value)
		{
			return value.ToInt64();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a pointer to an unspecified type.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001237 RID: 4663 RVA: 0x00049A96 File Offset: 0x00047C96
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001238 RID: 4664 RVA: 0x00049A9F File Offset: 0x00047C9F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr Add(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001239 RID: 4665 RVA: 0x00049A9F File Offset: 0x00047C9F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator +(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x0600123A RID: 4666 RVA: 0x00049AAE File Offset: 0x00047CAE
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator -(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00049ABD File Offset: 0x00047CBD
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsNull()
		{
			return this.m_value == null;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00049AC9 File Offset: 0x00047CC9
		bool IEquatable<IntPtr>.Equals(IntPtr other)
		{
			return this.m_value == other.m_value;
		}

		// Token: 0x0400075D RID: 1885
		private unsafe readonly void* m_value;

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400075E RID: 1886
		public static readonly IntPtr Zero;
	}
}

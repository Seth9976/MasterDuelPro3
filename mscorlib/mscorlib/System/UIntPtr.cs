using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F8 RID: 504
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct UIntPtr : ISerializable, IEquatable<UIntPtr>
	{
		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified 64-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		// Token: 0x06001344 RID: 4932 RVA: 0x0004EC85 File Offset: 0x0004CE85
		public UIntPtr(ulong value)
		{
			if (value > (ulong)(-1) && UIntPtr.Size < 8)
			{
				throw new OverflowException(Locale.GetText("This isn't a 64bits machine."));
			}
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UIntPtr" /> structure using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit unsigned integer. </param>
		// Token: 0x06001345 RID: 4933 RVA: 0x0004ECAC File Offset: 0x0004CEAC
		public UIntPtr(uint value)
		{
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type. </param>
		// Token: 0x06001346 RID: 4934 RVA: 0x0004ECB6 File Offset: 0x0004CEB6
		[CLSCompliant(false)]
		public unsafe UIntPtr(void* value)
		{
			this._pointer = value;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.UIntPtr" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001347 RID: 4935 RVA: 0x0004ECC0 File Offset: 0x0004CEC0
		public override bool Equals(object obj)
		{
			if (obj is UIntPtr)
			{
				UIntPtr uintPtr = (UIntPtr)obj;
				return this._pointer == uintPtr._pointer;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001348 RID: 4936 RVA: 0x0004ECED File Offset: 0x0004CEED
		public override int GetHashCode()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001349 RID: 4937 RVA: 0x0004ECF6 File Offset: 0x0004CEF6
		public uint ToUInt32()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equal to the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134A RID: 4938 RVA: 0x0004ECFF File Offset: 0x0004CEFF
		public ulong ToUInt64()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134B RID: 4939 RVA: 0x0004ED08 File Offset: 0x0004CF08
		[CLSCompliant(false)]
		public unsafe void* ToPointer()
		{
			return this._pointer;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134C RID: 4940 RVA: 0x0004ED10 File Offset: 0x0004CF10
		public override string ToString()
		{
			if (UIntPtr.Size >= 8)
			{
				return this._pointer.ToString();
			}
			return this._pointer.ToString();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.UIntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600134D RID: 4941 RVA: 0x0004ED44 File Offset: 0x0004CF44
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("pointer", this._pointer);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are equal.</summary>
		/// <returns>true if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600134E RID: 4942 RVA: 0x0004ED66 File Offset: 0x0004CF66
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer == value2._pointer;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are not equal.</summary>
		/// <returns>true if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600134F RID: 4943 RVA: 0x0004ED78 File Offset: 0x0004CF78
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer != value2._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 64-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001350 RID: 4944 RVA: 0x0004ED8D File Offset: 0x0004CF8D
		public static explicit operator ulong(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 32-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001351 RID: 4945 RVA: 0x0004ED97 File Offset: 0x0004CF97
		public static explicit operator uint(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of a 64-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001352 RID: 4946 RVA: 0x0004EDA1 File Offset: 0x0004CFA1
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to a <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A pointer to an unspecified type. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001353 RID: 4947 RVA: 0x0004EDA9 File Offset: 0x0004CFA9
		[CLSCompliant(false)]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a pointer to an unspecified type.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001354 RID: 4948 RVA: 0x0004EDB1 File Offset: 0x0004CFB1
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.ToPointer();
		}

		/// <summary>Converts the value of a 32-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 32-bit unsigned integer. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001355 RID: 4949 RVA: 0x0004EDBA File Offset: 0x0004CFBA
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle on this platform, measured in bytes. The value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0004998D File Offset: 0x00047B8D
		public unsafe static int Size
		{
			get
			{
				return sizeof(void*);
			}
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001357 RID: 4951 RVA: 0x0004EDC2 File Offset: 0x0004CFC2
		public unsafe static UIntPtr Add(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x06001358 RID: 4952 RVA: 0x0004EDD1 File Offset: 0x0004CFD1
		public unsafe static UIntPtr Subtract(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001359 RID: 4953 RVA: 0x0004EDC2 File Offset: 0x0004CFC2
		public unsafe static UIntPtr operator +(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x0004EDD1 File Offset: 0x0004CFD1
		public unsafe static UIntPtr operator -(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004EDE0 File Offset: 0x0004CFE0
		bool IEquatable<UIntPtr>.Equals(UIntPtr other)
		{
			return this._pointer == other._pointer;
		}

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400097E RID: 2430
		public static readonly UIntPtr Zero = new UIntPtr(0U);

		// Token: 0x0400097F RID: 2431
		private unsafe readonly void* _pointer;
	}
}

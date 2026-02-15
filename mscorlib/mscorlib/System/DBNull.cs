using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a nonexistent value. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private DBNull()
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001C20C File Offset: 0x0001A40C
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException("Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed.");
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.DBNull" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing information required to serialize the <see cref="T:System.DBNull" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.DBNull" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000690 RID: 1680 RVA: 0x0001C21E File Offset: 0x0001A41E
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2);
		}

		/// <summary>Returns an empty string (<see cref="F:System.String.Empty" />).</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000691 RID: 1681 RVA: 0x0001C227 File Offset: 0x0001A427
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Returns an empty string using the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> to be used to format the return value.-or- null to obtain the format information from the current locale setting of the operating system. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000692 RID: 1682 RVA: 0x0001C227 File Offset: 0x0001A427
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		/// <summary>Gets the <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />, which is <see cref="F:System.TypeCode.DBNull" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000693 RID: 1683 RVA: 0x0000F253 File Offset: 0x0000D453
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000694 RID: 1684 RVA: 0x0001C22E File Offset: 0x0001A42E
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000695 RID: 1685 RVA: 0x0001C22E File Offset: 0x0001A42E
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000696 RID: 1686 RVA: 0x0001C22E File Offset: 0x0001A42E
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000697 RID: 1687 RVA: 0x0001C22E File Offset: 0x0001A42E
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000698 RID: 1688 RVA: 0x0001C22E File Offset: 0x0001A42E
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000699 RID: 1689 RVA: 0x0001C22E File Offset: 0x0001A42E
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069A RID: 1690 RVA: 0x0001C22E File Offset: 0x0001A42E
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069B RID: 1691 RVA: 0x0001C22E File Offset: 0x0001A42E
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069C RID: 1692 RVA: 0x0001C22E File Offset: 0x0001A42E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069D RID: 1693 RVA: 0x0001C22E File Offset: 0x0001A42E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069E RID: 1694 RVA: 0x0001C22E File Offset: 0x0001A42E
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600069F RID: 1695 RVA: 0x0001C22E File Offset: 0x0001A42E
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001C22E File Offset: 0x0001A42E
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C22E File Offset: 0x0001A42E
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>Converts the current <see cref="T:System.DBNull" /> object to the specified type.</summary>
		/// <returns>The boxed equivalent of the current <see cref="T:System.DBNull" /> object, if that conversion is supported; otherwise, an exception is thrown and no value is returned. </returns>
		/// <param name="type">The type to convert the current <see cref="T:System.DBNull" /> object to. </param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface and is used to augment the conversion. If null is specified, format information is obtained from the current culture. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060006A2 RID: 1698 RVA: 0x00011F42 File Offset: 0x00010142
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.DBNull" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002F2 RID: 754
		public static readonly DBNull Value = new DBNull();
	}
}

using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents the date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds to be stored in or retrieved from a database. The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure has a different underlying data structure from its corresponding .NET Framework type, <see cref="T:System.DateTime" />, which can represent any time between 12:00:00 AM 1/1/0001 and 11:59:59 PM 12/31/9999, to the accuracy of 100 nanoseconds. <see cref="T:System.Data.SqlTypes.SqlDateTime" /> actually stores the relative difference to 00:00:00 AM 1/1/1900. Therefore, a conversion from "00:00:00 AM 1/1/1900" to an integer will return 0.</summary>
	// Token: 0x020000C4 RID: 196
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00038AD4 File Offset: 0x00036CD4
		private SqlDateTime(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_day = 0;
			this.m_time = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x060009AB RID: 2475 RVA: 0x00038AEB File Offset: 0x00036CEB
		public SqlDateTime(DateTime value)
		{
			this = SqlDateTime.FromDateTime(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters.</summary>
		/// <param name="dayTicks">An integer value that represents the date as ticks. </param>
		/// <param name="timeTicks">An integer value that represents the time as ticks. </param>
		// Token: 0x060009AC RID: 2476 RVA: 0x00038AFC File Offset: 0x00036CFC
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			if (dayTicks < SqlDateTime.s_minDay || dayTicks > SqlDateTime.s_maxDay || timeTicks < SqlDateTime.s_minTime || timeTicks > SqlDateTime.s_maxTime)
			{
				this.m_fNotNull = false;
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			this.m_day = dayTicks;
			this.m_time = timeTicks;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false. </returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00038B50 File Offset: 0x00036D50
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00038B5C File Offset: 0x00036D5C
		private static TimeSpan ToTimeSpan(SqlDateTime value)
		{
			long num = (long)((double)value.m_time / SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			return new TimeSpan((long)value.m_day * 864000000000L + num * 10000L);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00038BA1 File Offset: 0x00036DA1
		private static DateTime ToDateTime(SqlDateTime value)
		{
			return SqlDateTime.s_SQLBaseDate.Add(SqlDateTime.ToTimeSpan(value));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00038BB4 File Offset: 0x00036DB4
		private static SqlDateTime FromTimeSpan(TimeSpan value)
		{
			if (value < SqlDateTime.s_minTimeSpan || value > SqlDateTime.s_maxTimeSpan)
			{
				throw new SqlTypeException(SQLResource.DateTimeOverflowMessage);
			}
			int num = value.Days;
			long num2 = value.Ticks - (long)num * 864000000000L;
			if (num2 < 0L)
			{
				num--;
				num2 += 864000000000L;
			}
			int num3 = (int)((double)num2 / 10000.0 * SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			if (num3 > SqlDateTime.s_maxTime)
			{
				num3 = 0;
				num++;
			}
			return new SqlDateTime(num, num3);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00038C4B File Offset: 0x00036E4B
		private static SqlDateTime FromDateTime(DateTime value)
		{
			if (value == DateTime.MaxValue)
			{
				return SqlDateTime.MaxValue;
			}
			return SqlDateTime.FromTimeSpan(value.Subtract(SqlDateTime.s_SQLBaseDate));
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. This property is read-only.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00038C71 File Offset: 0x00036E71
		public DateTime Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return SqlDateTime.ToDateTime(this);
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the date of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the date that is contained in the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00038C8C File Offset: 0x00036E8C
		public int DayTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_day;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00038CA2 File Offset: 0x00036EA2
		public int TimeTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_time;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts a <see cref="T:System.DateTime" /> structure to a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the combined <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties of the supplied <see cref="T:System.DateTime" /> structure.</returns>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x00038CB8 File Offset: 0x00036EB8
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A String representing the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00038CC0 File Offset: 0x00036EC0
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return SqlDateTime.ToDateTime(this).ToString(null);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x060009B7 RID: 2487 RVA: 0x00038CEF File Offset: 0x00036EEF
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day == y.m_day && x.m_time == y.m_time);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x060009B8 RID: 2488 RVA: 0x00038D30 File Offset: 0x00036F30
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time < y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x00038D8C File Offset: 0x00036F8C
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time > y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x060009BA RID: 2490 RVA: 0x00038DE6 File Offset: 0x00036FE6
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x060009BB RID: 2491 RVA: 0x00038DEF File Offset: 0x00036FEF
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing as Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060009BC RID: 2492 RVA: 0x00038DF8 File Offset: 0x00036FF8
		public int CompareTo(object value)
		{
			if (value is SqlDateTime)
			{
				SqlDateTime sqlDateTime = (SqlDateTime)value;
				return this.CompareTo(sqlDateTime);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDateTime));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Zero This instance is the same as <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Greater than zero This instance is greater than <see cref="T:System.Data.SqlTypes.SqlDateTime" />-or- <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be compared.</param>
		// Token: 0x060009BD RID: 2493 RVA: 0x00038E34 File Offset: 0x00037034
		public int CompareTo(SqlDateTime value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> object.</summary>
		/// <returns>true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x060009BE RID: 2494 RVA: 0x00038E8C File Offset: 0x0003708C
		public override bool Equals(object value)
		{
			if (!(value is SqlDateTime))
			{
				return false;
			}
			SqlDateTime sqlDateTime = (SqlDateTime)value;
			if (sqlDateTime.IsNull || this.IsNull)
			{
				return sqlDateTime.IsNull && this.IsNull;
			}
			return (this == sqlDateTime).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009BF RID: 2495 RVA: 0x00038EE4 File Offset: 0x000370E4
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x060009C0 RID: 2496 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x060009C1 RID: 2497 RVA: 0x00038F0C File Offset: 0x0003710C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			DateTime dateTime = XmlConvert.ToDateTime(reader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
			if (dateTime.Kind != DateTimeKind.Unspecified)
			{
				throw new SqlTypeException(SQLResource.TimeZoneSpecifiedMessage);
			}
			SqlDateTime sqlDateTime = SqlDateTime.FromDateTime(dateTime);
			this.m_day = sqlDateTime.DayTicks;
			this.m_time = sqlDateTime.TimeTicks;
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x060009C2 RID: 2498 RVA: 0x00038F8D File Offset: 0x0003718D
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.Value, SqlDateTime.s_ISO8601_DateTimeFormat));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x060009C3 RID: 2499 RVA: 0x00038FC8 File Offset: 0x000371C8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040003D8 RID: 984
		private bool m_fNotNull;

		// Token: 0x040003D9 RID: 985
		private int m_day;

		// Token: 0x040003DA RID: 986
		private int m_time;

		// Token: 0x040003DB RID: 987
		private static readonly double s_SQLTicksPerMillisecond = 0.3;

		/// <summary>A constant whose value is the number of ticks equivalent to one second.</summary>
		// Token: 0x040003DC RID: 988
		public static readonly int SQLTicksPerSecond = 300;

		/// <summary>A constant whose value is the number of ticks equivalent to one minute.</summary>
		// Token: 0x040003DD RID: 989
		public static readonly int SQLTicksPerMinute = SqlDateTime.SQLTicksPerSecond * 60;

		/// <summary>A constant whose value is the number of ticks equivalent to one hour.</summary>
		// Token: 0x040003DE RID: 990
		public static readonly int SQLTicksPerHour = SqlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x040003DF RID: 991
		private static readonly int s_SQLTicksPerDay = SqlDateTime.SQLTicksPerHour * 24;

		// Token: 0x040003E0 RID: 992
		private static readonly long s_ticksPerSecond = 10000000L;

		// Token: 0x040003E1 RID: 993
		private static readonly DateTime s_SQLBaseDate = new DateTime(1900, 1, 1);

		// Token: 0x040003E2 RID: 994
		private static readonly long s_SQLBaseDateTicks = SqlDateTime.s_SQLBaseDate.Ticks;

		// Token: 0x040003E3 RID: 995
		private static readonly int s_minYear = 1753;

		// Token: 0x040003E4 RID: 996
		private static readonly int s_maxYear = 9999;

		// Token: 0x040003E5 RID: 997
		private static readonly int s_minDay = -53690;

		// Token: 0x040003E6 RID: 998
		private static readonly int s_maxDay = 2958463;

		// Token: 0x040003E7 RID: 999
		private static readonly int s_minTime = 0;

		// Token: 0x040003E8 RID: 1000
		private static readonly int s_maxTime = SqlDateTime.s_SQLTicksPerDay - 1;

		// Token: 0x040003E9 RID: 1001
		private static readonly int s_dayBase = 693595;

		// Token: 0x040003EA RID: 1002
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040003EB RID: 1003
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x040003EC RID: 1004
		private static readonly DateTime s_minDateTime = new DateTime(1753, 1, 1);

		// Token: 0x040003ED RID: 1005
		private static readonly DateTime s_maxDateTime = DateTime.MaxValue;

		// Token: 0x040003EE RID: 1006
		private static readonly TimeSpan s_minTimeSpan = SqlDateTime.s_minDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x040003EF RID: 1007
		private static readonly TimeSpan s_maxTimeSpan = SqlDateTime.s_maxDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x040003F0 RID: 1008
		private static readonly string s_ISO8601_DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x040003F1 RID: 1009
		private static readonly string[] s_dateTimeFormats = new string[] { "MMM d yyyy hh:mm:ss:ffftt", "MMM d yyyy hh:mm:ss:fff", "d MMM yyyy hh:mm:ss:ffftt", "d MMM yyyy hh:mm:ss:fff", "hh:mm:ss:ffftt", "hh:mm:ss:fff", "yyMMdd", "yyyyMMdd" };

		/// <summary>Represents the minimum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040003F2 RID: 1010
		public static readonly SqlDateTime MinValue = new SqlDateTime(SqlDateTime.s_minDay, 0);

		/// <summary>Represents the maximum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040003F3 RID: 1011
		public static readonly SqlDateTime MaxValue = new SqlDateTime(SqlDateTime.s_maxDay, SqlDateTime.s_maxTime);

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040003F4 RID: 1012
		public static readonly SqlDateTime Null = new SqlDateTime(true);
	}
}

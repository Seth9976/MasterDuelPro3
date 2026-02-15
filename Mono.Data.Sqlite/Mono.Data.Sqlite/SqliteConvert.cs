using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000E RID: 14
	public abstract class SqliteConvert
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00008270 File Offset: 0x00006470
		internal SqliteConvert(SQLiteDateFormats fmt)
		{
			this._datetimeFormat = fmt;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008D7C File Offset: 0x00006F7C
		public static byte[] ToUTF8(string sourceText)
		{
			int num = SqliteConvert._utf8.GetByteCount(sourceText) + 1;
			byte[] array = new byte[num];
			num = SqliteConvert._utf8.GetBytes(sourceText, 0, sourceText.Length, array, 0);
			array[num] = 0;
			return array;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public byte[] ToUTF8(DateTime dateTimeValue)
		{
			return SqliteConvert.ToUTF8(this.ToString(dateTimeValue));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008DC6 File Offset: 0x00006FC6
		public virtual string ToString(IntPtr nativestring, int nativestringlen)
		{
			return SqliteConvert.UTF8ToString(nativestring, nativestringlen);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public static string UTF8ToString(IntPtr nativestring, int nativestringlen)
		{
			if (nativestringlen == 0 || nativestring == IntPtr.Zero)
			{
				return string.Empty;
			}
			if (nativestringlen == -1)
			{
				do
				{
					nativestringlen++;
				}
				while (Marshal.ReadByte(nativestring, nativestringlen) != 0);
			}
			byte[] array = new byte[nativestringlen];
			Marshal.Copy(nativestring, array, 0, nativestringlen);
			return SqliteConvert._utf8.GetString(array, 0, nativestringlen);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008E30 File Offset: 0x00007030
		public DateTime ToDateTime(string dateText)
		{
			switch (this._datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
				return new DateTime(Convert.ToInt64(dateText, CultureInfo.InvariantCulture));
			case SQLiteDateFormats.JulianDay:
				return this.ToDateTime(Convert.ToDouble(dateText, CultureInfo.InvariantCulture));
			}
			return DateTime.ParseExact(dateText, SqliteConvert._datetimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008E8F File Offset: 0x0000708F
		public DateTime ToDateTime(double julianDay)
		{
			return DateTime.FromOADate(julianDay - 2415018.5);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008EA1 File Offset: 0x000070A1
		public double ToJulianDay(DateTime value)
		{
			return value.ToOADate() + 2415018.5;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008EB4 File Offset: 0x000070B4
		public string ToString(DateTime dateValue)
		{
			switch (this._datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
				return dateValue.Ticks.ToString(CultureInfo.InvariantCulture);
			case SQLiteDateFormats.JulianDay:
				return this.ToJulianDay(dateValue).ToString(CultureInfo.InvariantCulture);
			}
			return dateValue.ToString(SqliteConvert._datetimeFormats[7], CultureInfo.InvariantCulture);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008F1C File Offset: 0x0000711C
		internal DateTime ToDateTime(IntPtr ptr, int len)
		{
			return this.ToDateTime(this.ToString(ptr, len));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008F2C File Offset: 0x0000712C
		public static string[] Split(string source, char separator)
		{
			char[] array = new char[] { '"', separator };
			char[] array2 = new char[] { '"' };
			int num = 0;
			List<string> list = new List<string>();
			while (source.Length > 0)
			{
				num = source.IndexOfAny(array, num);
				if (num == -1)
				{
					break;
				}
				if (source[num] == array[0])
				{
					num = source.IndexOfAny(array2, num + 1);
					if (num == -1)
					{
						break;
					}
					num++;
				}
				else
				{
					string text = source.Substring(0, num).Trim();
					if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
					{
						text = text.Substring(1, text.Length - 2);
					}
					source = source.Substring(num + 1).Trim();
					if (text.Length > 0)
					{
						list.Add(text);
					}
					num = 0;
				}
			}
			if (source.Length > 0)
			{
				string text = source.Trim();
				if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
				{
					text = text.Substring(1, text.Length - 2);
				}
				list.Add(text);
			}
			string[] array3 = new string[list.Count];
			list.CopyTo(array3, 0);
			return array3;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000090B8 File Offset: 0x000072B8
		public static bool ToBoolean(string source)
		{
			if (string.Compare(source, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(source, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return false;
			}
			string text = source.ToLower();
			if (text != null)
			{
				if (SqliteConvert.<>f__switch$map0 == null)
				{
					SqliteConvert.<>f__switch$map0 = new Dictionary<string, int>(8)
					{
						{ "yes", 0 },
						{ "y", 0 },
						{ "1", 0 },
						{ "on", 0 },
						{ "no", 1 },
						{ "n", 1 },
						{ "0", 1 },
						{ "off", 1 }
					};
				}
				int num;
				if (SqliteConvert.<>f__switch$map0.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 1)
					{
						return false;
					}
				}
			}
			throw new ArgumentException("source");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000091A1 File Offset: 0x000073A1
		internal static Type SQLiteTypeToType(SQLiteType t)
		{
			if (t.Type == DbType.Object)
			{
				return SqliteConvert._affinitytotype[(int)t.Affinity];
			}
			return SqliteConvert.DbTypeToType(t.Type);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000091C8 File Offset: 0x000073C8
		internal static DbType TypeToDbType(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode != TypeCode.Object)
			{
				return SqliteConvert._typetodbtype[(int)typeCode];
			}
			if (typ == typeof(byte[]))
			{
				return DbType.Binary;
			}
			if (typ == typeof(Guid))
			{
				return DbType.Guid;
			}
			return DbType.String;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009212 File Offset: 0x00007412
		internal static int DbTypeToColumnSize(DbType typ)
		{
			return SqliteConvert._dbtypetocolumnsize[(int)typ];
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000921B File Offset: 0x0000741B
		internal static object DbTypeToNumericPrecision(DbType typ)
		{
			return SqliteConvert._dbtypetonumericprecision[(int)typ];
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009224 File Offset: 0x00007424
		internal static object DbTypeToNumericScale(DbType typ)
		{
			return SqliteConvert._dbtypetonumericscale[(int)typ];
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009230 File Offset: 0x00007430
		internal static string DbTypeToTypeName(DbType typ)
		{
			for (int i = 0; i < SqliteConvert._dbtypeNames.Length; i++)
			{
				if (SqliteConvert._dbtypeNames[i].dataType == typ)
				{
					return SqliteConvert._dbtypeNames[i].typeName;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009281 File Offset: 0x00007481
		internal static Type DbTypeToType(DbType typ)
		{
			return SqliteConvert._dbtypeToType[(int)typ];
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000928C File Offset: 0x0000748C
		internal static TypeAffinity TypeToAffinity(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode != TypeCode.Object)
			{
				return SqliteConvert._typecodeAffinities[(int)typeCode];
			}
			if (typ == typeof(byte[]) || typ == typeof(Guid))
			{
				return TypeAffinity.Blob;
			}
			return TypeAffinity.Text;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000092D4 File Offset: 0x000074D4
		internal static DbType TypeNameToDbType(string Name)
		{
			if (string.IsNullOrEmpty(Name))
			{
				return DbType.Object;
			}
			int num = SqliteConvert._typeNames.Length;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(Name, 0, SqliteConvert._typeNames[i].typeName, 0, SqliteConvert._typeNames[i].typeName.Length, true, CultureInfo.InvariantCulture) == 0)
				{
					return SqliteConvert._typeNames[i].dataType;
				}
			}
			return DbType.Object;
		}

		// Token: 0x04000033 RID: 51
		private static string[] _datetimeFormats = new string[]
		{
			"THHmmss", "THHmm", "HH:mm:ss", "HH:mm", "HH:mm:ss.FFFFFFF", "yy-MM-dd", "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss.FFFFFFF", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm",
			"yyyy-MM-ddTHH:mm:ss.FFFFFFF", "yyyy-MM-ddTHH:mm", "yyyy-MM-ddTHH:mm:ss", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddTHHmmssFFFFFFF", "yyyyMMdd"
		};

		// Token: 0x04000034 RID: 52
		private static Encoding _utf8 = new UTF8Encoding();

		// Token: 0x04000035 RID: 53
		internal SQLiteDateFormats _datetimeFormat;

		// Token: 0x04000036 RID: 54
		private static Type[] _affinitytotype = new Type[]
		{
			typeof(object),
			typeof(long),
			typeof(double),
			typeof(string),
			typeof(byte[]),
			typeof(object),
			typeof(DateTime),
			typeof(object)
		};

		// Token: 0x04000037 RID: 55
		private static DbType[] _typetodbtype = new DbType[]
		{
			DbType.Object,
			DbType.Binary,
			DbType.Object,
			DbType.Boolean,
			DbType.SByte,
			DbType.SByte,
			DbType.Byte,
			DbType.Int16,
			DbType.UInt16,
			DbType.Int32,
			DbType.UInt32,
			DbType.Int64,
			DbType.UInt64,
			DbType.Single,
			DbType.Double,
			DbType.Decimal,
			DbType.DateTime,
			DbType.Object,
			DbType.String
		};

		// Token: 0x04000038 RID: 56
		private static int[] _dbtypetocolumnsize = new int[]
		{
			int.MaxValue, int.MaxValue, 1, 1, 8, 8, 8, 8, 8, 16,
			2, 4, 8, int.MaxValue, 1, 4, int.MaxValue, 8, 2, 4,
			8, 8, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue
		};

		// Token: 0x04000039 RID: 57
		private static object[] _dbtypetonumericprecision = new object[]
		{
			DBNull.Value,
			DBNull.Value,
			3,
			DBNull.Value,
			19,
			DBNull.Value,
			DBNull.Value,
			53,
			53,
			DBNull.Value,
			5,
			10,
			19,
			DBNull.Value,
			3,
			24,
			DBNull.Value,
			DBNull.Value,
			5,
			10,
			19,
			53,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x0400003A RID: 58
		private static object[] _dbtypetonumericscale = new object[]
		{
			DBNull.Value,
			DBNull.Value,
			0,
			DBNull.Value,
			4,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			DBNull.Value,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x0400003B RID: 59
		private static SQLiteTypeNames[] _dbtypeNames = new SQLiteTypeNames[]
		{
			new SQLiteTypeNames("INTEGER", DbType.Int64),
			new SQLiteTypeNames("TINYINT", DbType.Byte),
			new SQLiteTypeNames("INT", DbType.Int32),
			new SQLiteTypeNames("VARCHAR", DbType.AnsiString),
			new SQLiteTypeNames("NVARCHAR", DbType.String),
			new SQLiteTypeNames("CHAR", DbType.AnsiStringFixedLength),
			new SQLiteTypeNames("NCHAR", DbType.StringFixedLength),
			new SQLiteTypeNames("FLOAT", DbType.Double),
			new SQLiteTypeNames("REAL", DbType.Single),
			new SQLiteTypeNames("BIT", DbType.Boolean),
			new SQLiteTypeNames("DECIMAL", DbType.Decimal),
			new SQLiteTypeNames("DATETIME", DbType.DateTime),
			new SQLiteTypeNames("BLOB", DbType.Binary),
			new SQLiteTypeNames("UNIQUEIDENTIFIER", DbType.Guid),
			new SQLiteTypeNames("SMALLINT", DbType.Int16)
		};

		// Token: 0x0400003C RID: 60
		private static Type[] _dbtypeToType = new Type[]
		{
			typeof(string),
			typeof(byte[]),
			typeof(byte),
			typeof(bool),
			typeof(decimal),
			typeof(DateTime),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(DateTime),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(double),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};

		// Token: 0x0400003D RID: 61
		private static TypeAffinity[] _typecodeAffinities = new TypeAffinity[]
		{
			TypeAffinity.Null,
			TypeAffinity.Blob,
			TypeAffinity.Null,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.DateTime,
			TypeAffinity.Null,
			TypeAffinity.Text
		};

		// Token: 0x0400003E RID: 62
		private static SQLiteTypeNames[] _typeNames = new SQLiteTypeNames[]
		{
			new SQLiteTypeNames("COUNTER", DbType.Int64),
			new SQLiteTypeNames("AUTOINCREMENT", DbType.Int64),
			new SQLiteTypeNames("IDENTITY", DbType.Int64),
			new SQLiteTypeNames("LONGTEXT", DbType.String),
			new SQLiteTypeNames("LONGCHAR", DbType.String),
			new SQLiteTypeNames("LONGVARCHAR", DbType.String),
			new SQLiteTypeNames("LONG", DbType.Int64),
			new SQLiteTypeNames("TINYINT", DbType.Byte),
			new SQLiteTypeNames("INTEGER", DbType.Int64),
			new SQLiteTypeNames("INT", DbType.Int32),
			new SQLiteTypeNames("VARCHAR", DbType.String),
			new SQLiteTypeNames("NVARCHAR", DbType.String),
			new SQLiteTypeNames("CHAR", DbType.String),
			new SQLiteTypeNames("NCHAR", DbType.String),
			new SQLiteTypeNames("TEXT", DbType.String),
			new SQLiteTypeNames("NTEXT", DbType.String),
			new SQLiteTypeNames("STRING", DbType.String),
			new SQLiteTypeNames("DOUBLE", DbType.Double),
			new SQLiteTypeNames("FLOAT", DbType.Double),
			new SQLiteTypeNames("REAL", DbType.Single),
			new SQLiteTypeNames("BIT", DbType.Boolean),
			new SQLiteTypeNames("YESNO", DbType.Boolean),
			new SQLiteTypeNames("LOGICAL", DbType.Boolean),
			new SQLiteTypeNames("BOOL", DbType.Boolean),
			new SQLiteTypeNames("NUMERIC", DbType.Decimal),
			new SQLiteTypeNames("DECIMAL", DbType.Decimal),
			new SQLiteTypeNames("MONEY", DbType.Decimal),
			new SQLiteTypeNames("CURRENCY", DbType.Decimal),
			new SQLiteTypeNames("TIME", DbType.DateTime),
			new SQLiteTypeNames("DATE", DbType.DateTime),
			new SQLiteTypeNames("SMALLDATE", DbType.DateTime),
			new SQLiteTypeNames("BLOB", DbType.Binary),
			new SQLiteTypeNames("BINARY", DbType.Binary),
			new SQLiteTypeNames("VARBINARY", DbType.Binary),
			new SQLiteTypeNames("IMAGE", DbType.Binary),
			new SQLiteTypeNames("GENERAL", DbType.Binary),
			new SQLiteTypeNames("OLEOBJECT", DbType.Binary),
			new SQLiteTypeNames("GUID", DbType.Guid),
			new SQLiteTypeNames("UNIQUEIDENTIFIER", DbType.Guid),
			new SQLiteTypeNames("MEMO", DbType.String),
			new SQLiteTypeNames("NOTE", DbType.String),
			new SQLiteTypeNames("SMALLINT", DbType.Int16),
			new SQLiteTypeNames("BIGINT", DbType.Int64)
		};
	}
}

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020000ED RID: 237
	internal abstract class DataStorage
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x00043927 File Offset: 0x00041B27
		protected DataStorage(DataColumn column, Type type, object defaultValue, StorageType storageType)
			: this(column, type, defaultValue, DBNull.Value, false, storageType)
		{
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0004393A File Offset: 0x00041B3A
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, StorageType storageType)
			: this(column, type, defaultValue, nullValue, false, storageType)
		{
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0004394C File Offset: 0x00041B4C
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable, StorageType storageType)
		{
			this._column = column;
			this._table = column.Table;
			this._dataType = type;
			this._storageTypeCode = storageType;
			this._defaultValue = defaultValue;
			this._nullValue = nullValue;
			this._isCloneable = isICloneable;
			this._isCustomDefinedType = DataStorage.IsTypeCustomType(this._storageTypeCode);
			this._isStringType = StorageType.String == this._storageTypeCode || StorageType.SqlString == this._storageTypeCode;
			this._isValueType = DataStorage.DetermineIfValueType(this._storageTypeCode, type);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000439D8 File Offset: 0x00041BD8
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this._column.DateTimeMode;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000439E5 File Offset: 0x00041BE5
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this._table.FormatProvider;
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000439F2 File Offset: 0x00041BF2
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				return this.AggregateCount(recordNos);
			}
			return null;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00043A04 File Offset: 0x00041C04
		public object AggregateCount(int[] recordNos)
		{
			int num = 0;
			for (int i = 0; i < recordNos.Length; i++)
			{
				if (!this._dbNullBits.Get(recordNos[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00043A3C File Offset: 0x00041C3C
		protected int CompareBits(int recordNo1, int recordNo2)
		{
			bool flag = this._dbNullBits.Get(recordNo1);
			bool flag2 = this._dbNullBits.Get(recordNo2);
			if (!(flag ^ flag2))
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000C8C RID: 3212
		public abstract int Compare(int recordNo1, int recordNo2);

		// Token: 0x06000C8D RID: 3213
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x06000C8E RID: 3214 RVA: 0x00043A70 File Offset: 0x00041C70
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00043A73 File Offset: 0x00041C73
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this._dbNullBits.Set(dstRecordNo, this._dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x06000C90 RID: 3216
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x06000C91 RID: 3217
		public abstract object Get(int recordNo);

		// Token: 0x06000C92 RID: 3218 RVA: 0x00043A8D File Offset: 0x00041C8D
		protected object GetBits(int recordNo)
		{
			if (this._dbNullBits.Get(recordNo))
			{
				return this._nullValue;
			}
			return this._defaultValue;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00043AAA File Offset: 0x00041CAA
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00043AB1 File Offset: 0x00041CB1
		protected bool HasValue(int recordNo)
		{
			return !this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00043AC2 File Offset: 0x00041CC2
		public virtual bool IsNull(int recordNo)
		{
			return this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06000C96 RID: 3222
		public abstract void Set(int recordNo, object value);

		// Token: 0x06000C97 RID: 3223 RVA: 0x00043AD0 File Offset: 0x00041CD0
		protected void SetNullBit(int recordNo, bool flag)
		{
			this._dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00043ADF File Offset: 0x00041CDF
		public virtual void SetCapacity(int capacity)
		{
			if (this._dbNullBits == null)
			{
				this._dbNullBits = new BitArray(capacity);
				return;
			}
			this._dbNullBits.Length = capacity;
		}

		// Token: 0x06000C99 RID: 3225
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x06000C9A RID: 3226 RVA: 0x00043B02 File Offset: 0x00041D02
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x06000C9B RID: 3227
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x06000C9C RID: 3228 RVA: 0x00043B10 File Offset: 0x00041D10
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00043B20 File Offset: 0x00041D20
		public static DataStorage CreateStorage(DataColumn column, Type dataType, StorageType typeCode)
		{
			if (typeCode != StorageType.Empty || !(null != dataType))
			{
				switch (typeCode)
				{
				case StorageType.Empty:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.Empty);
				case StorageType.DBNull:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.DBNull);
				case StorageType.Boolean:
					return new BooleanStorage(column);
				case StorageType.Char:
					return new CharStorage(column);
				case StorageType.SByte:
					return new SByteStorage(column);
				case StorageType.Byte:
					return new ByteStorage(column);
				case StorageType.Int16:
					return new Int16Storage(column);
				case StorageType.UInt16:
					return new UInt16Storage(column);
				case StorageType.Int32:
					return new Int32Storage(column);
				case StorageType.UInt32:
					return new UInt32Storage(column);
				case StorageType.Int64:
					return new Int64Storage(column);
				case StorageType.UInt64:
					return new UInt64Storage(column);
				case StorageType.Single:
					return new SingleStorage(column);
				case StorageType.Double:
					return new DoubleStorage(column);
				case StorageType.Decimal:
					return new DecimalStorage(column);
				case StorageType.DateTime:
					return new DateTimeStorage(column);
				case StorageType.TimeSpan:
					return new TimeSpanStorage(column);
				case StorageType.String:
					return new StringStorage(column);
				case StorageType.Guid:
					return new ObjectStorage(column, dataType);
				case StorageType.ByteArray:
					return new ObjectStorage(column, dataType);
				case StorageType.CharArray:
					return new ObjectStorage(column, dataType);
				case StorageType.Type:
					return new ObjectStorage(column, dataType);
				case StorageType.DateTimeOffset:
					return new DateTimeOffsetStorage(column);
				case StorageType.BigInteger:
					return new BigIntegerStorage(column);
				case StorageType.Uri:
					return new ObjectStorage(column, dataType);
				case StorageType.SqlBinary:
					return new SqlBinaryStorage(column);
				case StorageType.SqlBoolean:
					return new SqlBooleanStorage(column);
				case StorageType.SqlByte:
					return new SqlByteStorage(column);
				case StorageType.SqlBytes:
					return new SqlBytesStorage(column);
				case StorageType.SqlChars:
					return new SqlCharsStorage(column);
				case StorageType.SqlDateTime:
					return new SqlDateTimeStorage(column);
				case StorageType.SqlDecimal:
					return new SqlDecimalStorage(column);
				case StorageType.SqlDouble:
					return new SqlDoubleStorage(column);
				case StorageType.SqlGuid:
					return new SqlGuidStorage(column);
				case StorageType.SqlInt16:
					return new SqlInt16Storage(column);
				case StorageType.SqlInt32:
					return new SqlInt32Storage(column);
				case StorageType.SqlInt64:
					return new SqlInt64Storage(column);
				case StorageType.SqlMoney:
					return new SqlMoneyStorage(column);
				case StorageType.SqlSingle:
					return new SqlSingleStorage(column);
				case StorageType.SqlString:
					return new SqlStringStorage(column);
				}
				return new ObjectStorage(column, dataType);
			}
			if (typeof(INullable).IsAssignableFrom(dataType))
			{
				return new SqlUdtStorage(column, dataType);
			}
			return new ObjectStorage(column, dataType);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00043D2C File Offset: 0x00041F2C
		internal static StorageType GetStorageType(Type dataType)
		{
			for (int i = 0; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return (StorageType)i;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(dataType);
			if (TypeCode.Object != typeCode)
			{
				return (StorageType)typeCode;
			}
			return StorageType.Empty;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00043D6A File Offset: 0x00041F6A
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.s_storageClassType[(int)storageType];
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00043D73 File Offset: 0x00041F73
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00043D80 File Offset: 0x00041F80
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00043D90 File Offset: 0x00041F90
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00043D9C File Offset: 0x00041F9C
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 26; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00043DD0 File Offset: 0x00041FD0
		private static bool DetermineIfValueType(StorageType typeCode, Type dataType)
		{
			bool flag;
			switch (typeCode)
			{
			case StorageType.Boolean:
			case StorageType.Char:
			case StorageType.SByte:
			case StorageType.Byte:
			case StorageType.Int16:
			case StorageType.UInt16:
			case StorageType.Int32:
			case StorageType.UInt32:
			case StorageType.Int64:
			case StorageType.UInt64:
			case StorageType.Single:
			case StorageType.Double:
			case StorageType.Decimal:
			case StorageType.DateTime:
			case StorageType.TimeSpan:
			case StorageType.Guid:
			case StorageType.DateTimeOffset:
			case StorageType.BigInteger:
			case StorageType.SqlBinary:
			case StorageType.SqlBoolean:
			case StorageType.SqlByte:
			case StorageType.SqlDateTime:
			case StorageType.SqlDecimal:
			case StorageType.SqlDouble:
			case StorageType.SqlGuid:
			case StorageType.SqlInt16:
			case StorageType.SqlInt32:
			case StorageType.SqlInt64:
			case StorageType.SqlMoney:
			case StorageType.SqlSingle:
			case StorageType.SqlString:
				flag = true;
				break;
			case StorageType.String:
			case StorageType.ByteArray:
			case StorageType.CharArray:
			case StorageType.Type:
			case StorageType.Uri:
			case StorageType.SqlBytes:
			case StorageType.SqlChars:
				flag = false;
				break;
			default:
				flag = dataType.IsValueType;
				break;
			}
			return flag;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00043E90 File Offset: 0x00042090
		internal static void ImplementsInterfaces(StorageType typeCode, Type dataType, out bool sqlType, out bool nullable, out bool xmlSerializable, out bool changeTracking, out bool revertibleChangeTracking)
		{
			if (DataStorage.IsSqlType(typeCode))
			{
				sqlType = true;
				nullable = true;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = true;
				return;
			}
			if (typeCode != StorageType.Empty)
			{
				sqlType = false;
				nullable = false;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = false;
				return;
			}
			Tuple<bool, bool, bool, bool> orAdd = DataStorage.s_typeImplementsInterface.GetOrAdd(dataType, DataStorage.s_inspectTypeForInterfaces);
			sqlType = false;
			nullable = orAdd.Item1;
			changeTracking = orAdd.Item2;
			revertibleChangeTracking = orAdd.Item3;
			xmlSerializable = orAdd.Item4;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00043F08 File Offset: 0x00042108
		private static Tuple<bool, bool, bool, bool> InspectTypeForInterfaces(Type dataType)
		{
			return new Tuple<bool, bool, bool, bool>(typeof(INullable).IsAssignableFrom(dataType), typeof(IChangeTracking).IsAssignableFrom(dataType), typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType), typeof(IXmlSerializable).IsAssignableFrom(dataType));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00043F5A File Offset: 0x0004215A
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00043F7E File Offset: 0x0004217E
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00043F94 File Offset: 0x00042194
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00043FB3 File Offset: 0x000421B3
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00043FBC File Offset: 0x000421BC
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00043FC9 File Offset: 0x000421C9
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x06000CAD RID: 3245
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x06000CAE RID: 3246
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x06000CAF RID: 3247
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00043FD3 File Offset: 0x000421D3
		protected void SetNullStorage(BitArray nullbits)
		{
			this._dbNullBits = nullbits;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00043FDC File Offset: 0x000421DC
		internal static Type GetType(string value)
		{
			Type type = Type.GetType(value);
			if (null == type && "System.Numerics.BigInteger" == value)
			{
				type = typeof(BigInteger);
			}
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00044018 File Offset: 0x00042218
		internal static string GetQualifiedName(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x04000523 RID: 1315
		private static readonly Type[] s_storageClassType = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(string),
			typeof(Guid),
			typeof(byte[]),
			typeof(char[]),
			typeof(Type),
			typeof(DateTimeOffset),
			typeof(BigInteger),
			typeof(Uri),
			typeof(SqlBinary),
			typeof(SqlBoolean),
			typeof(SqlByte),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlDateTime),
			typeof(SqlDecimal),
			typeof(SqlDouble),
			typeof(SqlGuid),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlMoney),
			typeof(SqlSingle),
			typeof(SqlString)
		};

		// Token: 0x04000524 RID: 1316
		internal readonly DataColumn _column;

		// Token: 0x04000525 RID: 1317
		internal readonly DataTable _table;

		// Token: 0x04000526 RID: 1318
		internal readonly Type _dataType;

		// Token: 0x04000527 RID: 1319
		internal readonly StorageType _storageTypeCode;

		// Token: 0x04000528 RID: 1320
		private BitArray _dbNullBits;

		// Token: 0x04000529 RID: 1321
		private readonly object _defaultValue;

		// Token: 0x0400052A RID: 1322
		internal readonly object _nullValue;

		// Token: 0x0400052B RID: 1323
		internal readonly bool _isCloneable;

		// Token: 0x0400052C RID: 1324
		internal readonly bool _isCustomDefinedType;

		// Token: 0x0400052D RID: 1325
		internal readonly bool _isStringType;

		// Token: 0x0400052E RID: 1326
		internal readonly bool _isValueType;

		// Token: 0x0400052F RID: 1327
		private static readonly Func<Type, Tuple<bool, bool, bool, bool>> s_inspectTypeForInterfaces = new Func<Type, Tuple<bool, bool, bool, bool>>(DataStorage.InspectTypeForInterfaces);

		// Token: 0x04000530 RID: 1328
		private static readonly ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>> s_typeImplementsInterface = new ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>>();
	}
}

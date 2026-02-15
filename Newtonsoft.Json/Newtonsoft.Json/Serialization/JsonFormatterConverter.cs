using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200011E RID: 286
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00027C75 File Offset: 0x00025E75
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00027CA8 File Offset: 0x00025EA8
		private T GetTokenValue<[Nullable(2)] T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)global::System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00027CDC File Offset: 0x00025EDC
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00027D28 File Offset: 0x00025F28
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return global::System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00027D5E File Offset: 0x00025F5E
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00027D67 File Offset: 0x00025F67
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00027D70 File Offset: 0x00025F70
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027D79 File Offset: 0x00025F79
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00027D82 File Offset: 0x00025F82
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00027D8B File Offset: 0x00025F8B
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00027D94 File Offset: 0x00025F94
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00027D9D File Offset: 0x00025F9D
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00027DA6 File Offset: 0x00025FA6
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00027DAF File Offset: 0x00025FAF
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00027DB8 File Offset: 0x00025FB8
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00027DC1 File Offset: 0x00025FC1
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00027DCA File Offset: 0x00025FCA
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00027DD3 File Offset: 0x00025FD3
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00027DDC File Offset: 0x00025FDC
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000554 RID: 1364
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04000555 RID: 1365
		private readonly JsonISerializableContract _contract;

		// Token: 0x04000556 RID: 1366
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}

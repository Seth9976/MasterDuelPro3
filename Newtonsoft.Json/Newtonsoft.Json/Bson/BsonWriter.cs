using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001E5 RID: 485
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : JsonWriter
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00046376 File Offset: 0x00044576
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00046383 File Offset: 0x00044583
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00046391 File Offset: 0x00044591
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000463B5 File Offset: 0x000445B5
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000463D4 File Offset: 0x000445D4
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000463E1 File Offset: 0x000445E1
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00046409 File Offset: 0x00044609
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00046417 File Offset: 0x00044617
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00046425 File Offset: 0x00044625
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00046425 File Offset: 0x00044625
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00046433 File Offset: 0x00044633
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00046446 File Offset: 0x00044646
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00046459 File Offset: 0x00044659
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00046469 File Offset: 0x00044669
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00046489 File Offset: 0x00044689
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00046499 File Offset: 0x00044699
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000464AC File Offset: 0x000446AC
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000464BC File Offset: 0x000446BC
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004654C File Offset: 0x0004474C
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				base.SetWriteState(JsonToken.Integer, null);
				this.AddToken(new BsonBinary(bigInteger.ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004658B File Offset: 0x0004478B
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004659E File Offset: 0x0004479E
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000465B1 File Offset: 0x000447B1
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000465D1 File Offset: 0x000447D1
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000465E8 File Offset: 0x000447E8
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00046614 File Offset: 0x00044814
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004662B File Offset: 0x0004482B
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004665B File Offset: 0x0004485B
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00046671 File Offset: 0x00044871
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00046687 File Offset: 0x00044887
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000466A5 File Offset: 0x000448A5
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000466BC File Offset: 0x000448BC
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000466D4 File Offset: 0x000448D4
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddToken(new BsonString(text, true));
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00046704 File Offset: 0x00044904
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004671B File Offset: 0x0004491B
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00046732 File Offset: 0x00044932
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00046748 File Offset: 0x00044948
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004676D File Offset: 0x0004496D
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00046784 File Offset: 0x00044984
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000467A4 File Offset: 0x000449A4
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000467C0 File Offset: 0x000449C0
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000467E2 File Offset: 0x000449E2
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004680D File Offset: 0x00044A0D
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004683F File Offset: 0x00044A3F
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x0400087A RID: 2170
		private readonly BsonBinaryWriter _writer;

		// Token: 0x0400087B RID: 2171
		private BsonToken _root;

		// Token: 0x0400087C RID: 2172
		private BsonToken _parent;

		// Token: 0x0400087D RID: 2173
		private string _propertyName;
	}
}

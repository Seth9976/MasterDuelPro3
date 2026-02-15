using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200018B RID: 395
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0003B629 File Offset: 0x00039829
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003B64D File Offset: 0x0003984D
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003B655 File Offset: 0x00039855
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003B66C File Offset: 0x0003986C
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003B68D File Offset: 0x0003988D
		public JTokenWriter()
		{
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000153AD File Offset: 0x000135AD
		public override void Flush()
		{
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003B695 File Offset: 0x00039895
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0003B69D File Offset: 0x0003989D
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0003B6B0 File Offset: 0x000398B0
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003B6E0 File Offset: 0x000398E0
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003B731 File Offset: 0x00039931
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003B744 File Offset: 0x00039944
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003B759 File Offset: 0x00039959
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003B761 File Offset: 0x00039961
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003B78E File Offset: 0x0003998E
		private void AddRawValue(object value, JTokenType type, JsonToken token)
		{
			this.AddJValue(new JValue(value, type), token);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003B7A0 File Offset: 0x000399A0
		internal void AddJValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = value ?? JValue.CreateNull();
				this._current = this._value;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003B810 File Offset: 0x00039A10
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003B832 File Offset: 0x00039A32
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddJValue(JValue.CreateNull(), JsonToken.Null);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003B847 File Offset: 0x00039A47
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddJValue(JValue.CreateUndefined(), JsonToken.Undefined);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003B85C File Offset: 0x00039A5C
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddJValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003B872 File Offset: 0x00039A72
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddJValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003B888 File Offset: 0x00039A88
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003B89F File Offset: 0x00039A9F
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003B8B6 File Offset: 0x00039AB6
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003B8CD File Offset: 0x00039ACD
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003B8E3 File Offset: 0x00039AE3
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Integer);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003B8F9 File Offset: 0x00039AF9
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003B90F File Offset: 0x00039B0F
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003B925 File Offset: 0x00039B25
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Boolean);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003B93C File Offset: 0x00039B3C
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003B953 File Offset: 0x00039B53
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003B96C File Offset: 0x00039B6C
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddJValue(new JValue(text), JsonToken.String);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003B99B File Offset: 0x00039B9B
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003B9B2 File Offset: 0x00039BB2
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddRawValue(value, JTokenType.Integer, JsonToken.Integer);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003B9C9 File Offset: 0x00039BC9
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Float);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003B9DF File Offset: 0x00039BDF
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003BA04 File Offset: 0x00039C04
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.Date);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003BA1B File Offset: 0x00039C1B
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value, JTokenType.Bytes), JsonToken.Bytes);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003BA34 File Offset: 0x00039C34
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003BA4B File Offset: 0x00039C4B
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003BA62 File Offset: 0x00039C62
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddJValue(new JValue(value), JsonToken.String);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003BA7C File Offset: 0x00039C7C
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken(null);
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = jtoken as JContainer;
					this._value = jtoken as JValue;
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x04000755 RID: 1877
		private JContainer _token;

		// Token: 0x04000756 RID: 1878
		private JContainer _parent;

		// Token: 0x04000757 RID: 1879
		private JValue _value;

		// Token: 0x04000758 RID: 1880
		private JToken _current;
	}
}

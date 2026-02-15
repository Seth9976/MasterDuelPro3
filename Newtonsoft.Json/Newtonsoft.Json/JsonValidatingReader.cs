using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000073 RID: 115
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600039D RID: 925 RVA: 0x0001530C File Offset: 0x0001350C
		// (remove) Token: 0x0600039E RID: 926 RVA: 0x00015344 File Offset: 0x00013544
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00015379 File Offset: 0x00013579
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00015386 File Offset: 0x00013586
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00015393 File Offset: 0x00013593
		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000153A0 File Offset: 0x000135A0
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000153AD File Offset: 0x000135AD
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000153AF File Offset: 0x000135AF
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000153BC File Offset: 0x000135BC
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000153C9 File Offset: 0x000135C9
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000153DE File Offset: 0x000135DE
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope schemaScope = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return schemaScope;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001540C File Offset: 0x0001360C
		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001541C File Offset: 0x0001361C
		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[] { this._model });
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel jsonSchemaModel2;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out jsonSchemaModel2))
						{
							list.Add(jsonSchemaModel2);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel3.PositionalItemsValidation)
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel3.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0 && jsonSchemaModel3.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel3.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel3.AllowAdditionalItems && jsonSchemaModel3.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel3.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000156DC File Offset: 0x000138DC
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string text = (((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message);
			this.OnValidationEvent(new JsonSchemaException(text, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00015744 File Offset: 0x00013944
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001576A File Offset: 0x0001396A
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001578F File Offset: 0x0001398F
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00015797 File Offset: 0x00013997
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000157BA File Offset: 0x000139BA
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000157C2 File Offset: 0x000139C2
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				JsonReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000157E4 File Offset: 0x000139E4
		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.GetValueOrDefault()))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001583C File Offset: 0x00013A3C
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000158C8 File Offset: 0x00013AC8
		public override int? ReadAsInt32()
		{
			int? num = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000158DB File Offset: 0x00013ADB
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return array;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000158EE File Offset: 0x00013AEE
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015901 File Offset: 0x00013B01
		public override double? ReadAsDouble()
		{
			double? num = this._reader.ReadAsDouble();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00015914 File Offset: 0x00013B14
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._reader.ReadAsBoolean();
			this.ValidateCurrentToken();
			return flag;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00015927 File Offset: 0x00013B27
		public override string ReadAsString()
		{
			string text = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return text;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001593A File Offset: 0x00013B3A
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return dateTime;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001594D File Offset: 0x00013B4D
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015960 File Offset: 0x00013B60
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015988 File Offset: 0x00013B88
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonTokenUtils.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, list));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, list2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
			{
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel = enumerator.Current;
						this.ValidatePropertyName(jsonSchemaModel);
					}
					return;
				}
				break;
			}
			case JsonToken.Comment:
				goto IL_03BD;
			case JsonToken.Raw:
				break;
			case JsonToken.Integer:
			{
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel2 = enumerator.Current;
						this.ValidateInteger(jsonSchemaModel2);
					}
					return;
				}
				goto IL_01D6;
			}
			case JsonToken.Float:
				goto IL_01D6;
			case JsonToken.String:
				goto IL_0222;
			case JsonToken.Boolean:
				goto IL_026E;
			case JsonToken.Null:
				goto IL_02BA;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				goto IL_0306;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
				{
					this.ValidateEndArray(jsonSchemaModel3);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			default:
				goto IL_03BD;
			}
			this.ProcessValue();
			return;
			IL_01D6:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel4 = enumerator.Current;
					this.ValidateFloat(jsonSchemaModel4);
				}
				return;
			}
			IL_0222:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel5 = enumerator.Current;
					this.ValidateString(jsonSchemaModel5);
				}
				return;
			}
			IL_026E:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel6 = enumerator.Current;
					this.ValidateBoolean(jsonSchemaModel6);
				}
				return;
			}
			IL_02BA:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel7 = enumerator.Current;
					this.ValidateNull(jsonSchemaModel7);
				}
				return;
			}
			IL_0306:
			this.WriteToken(this.CurrentSchemas);
			foreach (JsonSchemaModel jsonSchemaModel8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(jsonSchemaModel8);
			}
			this.Pop();
			return;
			IL_03BD:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00015DBC File Offset: 0x00013FBC
		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag = schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0;
				if (!flag)
				{
					if (!schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonTokenUtils.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (schemaScope.UniqueArrayItems.Contains(token, JToken.EqualityComparer))
						{
							this.RaiseError("Non-unique array item at index {0}.".FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), schemaScope.Schemas.First((JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else if (schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), Array.Empty<JsonConverter>());
								this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001600C File Offset: 0x0001420C
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				if (requiredProperties.Values.Any((bool v) => !v))
				{
					IEnumerable<string> enumerable = from kv in requiredProperties
						where !kv.Value
						select kv.Key;
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", enumerable)), schema);
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000160C8 File Offset: 0x000142C8
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null)
			{
				int num = arrayItemCount;
				int? num2 = schema.MaximumItems;
				if ((num > num2.GetValueOrDefault()) & (num2 != null))
				{
					this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
			}
			if (schema.MinimumItems != null)
			{
				int num3 = arrayItemCount;
				int? num2 = schema.MinimumItems;
				if ((num3 < num2.GetValueOrDefault()) & (num2 != null))
				{
					this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00016189 File Offset: 0x00014389
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000161A2 File Offset: 0x000143A2
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000161BC File Offset: 0x000143BC
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null)
			{
				int length = text.Length;
				int? num = schema.MaximumLength;
				if ((length > num.GetValueOrDefault()) & (num != null))
				{
					this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
				}
			}
			if (schema.MinimumLength != null)
			{
				int length2 = text.Length;
				int? num = schema.MinimumLength;
				if ((length2 < num.GetValueOrDefault()) & (num != null))
				{
					this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
				}
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000162F8 File Offset: 0x000144F8
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				bool flag;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					if (!Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0))
					{
						flag = bigInteger != 0L;
					}
					else
					{
						flag = bigInteger % new BigInteger(schema.DivisibleBy.Value) != 0L;
					}
				}
				else
				{
					flag = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault());
				}
				if (flag)
				{
					this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00016510 File Offset: 0x00014710
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				JsonValidatingReader.SchemaScope currentScope = this._currentScope;
				int arrayItemCount = currentScope.ArrayItemCount;
				currentScope.ArrayItemCount = arrayItemCount + 1;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000165E4 File Offset: 0x000147E4
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? num3 = schema.Maximum;
				if ((num2 > num3.GetValueOrDefault()) & (num3 != null))
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num4 = num;
					num3 = schema.Maximum;
					if ((num4 == num3.GetValueOrDefault()) & (num3 != null))
					{
						this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num5 = num;
				double? num3 = schema.Minimum;
				if ((num5 < num3.GetValueOrDefault()) & (num3 != null))
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num6 = num;
					num3 = schema.Minimum;
					if ((num6 == num3.GetValueOrDefault()) & (num3 != null))
					{
						this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000167AD File Offset: 0x000149AD
		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000167BB File Offset: 0x000149BB
		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 4.440892098500626E-15;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000167D0 File Offset: 0x000149D0
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00016854 File Offset: 0x00014A54
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string text in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000168D0 File Offset: 0x00014AD0
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000168E0 File Offset: 0x00014AE0
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000168F0 File Offset: 0x00014AF0
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00016930 File Offset: 0x00014B30
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00016954 File Offset: 0x00014B54
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00016978 File Offset: 0x00014B78
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040002B1 RID: 689
		private readonly JsonReader _reader;

		// Token: 0x040002B2 RID: 690
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x040002B3 RID: 691
		private JsonSchema _schema;

		// Token: 0x040002B4 RID: 692
		private JsonSchemaModel _model;

		// Token: 0x040002B5 RID: 693
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x040002B7 RID: 695
		private static readonly IList<JsonSchemaModel> EmptySchemaList = new List<JsonSchemaModel>();

		// Token: 0x02000074 RID: 116
		private class SchemaScope
		{
			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060003D1 RID: 977 RVA: 0x000169A8 File Offset: 0x00014BA8
			// (set) Token: 0x060003D2 RID: 978 RVA: 0x000169B0 File Offset: 0x00014BB0
			public string CurrentPropertyName { get; set; }

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060003D3 RID: 979 RVA: 0x000169B9 File Offset: 0x00014BB9
			// (set) Token: 0x060003D4 RID: 980 RVA: 0x000169C1 File Offset: 0x00014BC1
			public int ArrayItemCount { get; set; }

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x000169CA File Offset: 0x00014BCA
			public bool IsUniqueArray { get; }

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x000169D2 File Offset: 0x00014BD2
			public IList<JToken> UniqueArrayItems { get; }

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x000169DA File Offset: 0x00014BDA
			// (set) Token: 0x060003D8 RID: 984 RVA: 0x000169E2 File Offset: 0x00014BE2
			public JTokenWriter CurrentItemWriter { get; set; }

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x000169EB File Offset: 0x00014BEB
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060003DA RID: 986 RVA: 0x000169F3 File Offset: 0x00014BF3
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060003DB RID: 987 RVA: 0x000169FB File Offset: 0x00014BFB
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x060003DC RID: 988 RVA: 0x00016A04 File Offset: 0x00014C04
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (schemas.Any((JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = true;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			// Token: 0x060003DD RID: 989 RVA: 0x00016AC4 File Offset: 0x00014CC4
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (((schema != null) ? schema.Properties : null) == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
					where p.Value.Required
					select p.Key;
			}

			// Token: 0x040002B8 RID: 696
			private readonly JTokenType _tokenType;

			// Token: 0x040002B9 RID: 697
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x040002BA RID: 698
			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}

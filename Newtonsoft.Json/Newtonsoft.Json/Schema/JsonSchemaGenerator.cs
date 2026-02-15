using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000145 RID: 325
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaGenerator
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00030E74 File Offset: 0x0002F074
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00030E7C File Offset: 0x0002F07C
		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00030E85 File Offset: 0x0002F085
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00030E9B File Offset: 0x0002F09B
		public IContractResolver ContractResolver
		{
			get
			{
				if (this._contractResolver == null)
				{
					return DefaultContractResolver.Instance;
				}
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00030EA4 File Offset: 0x0002F0A4
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00030EAC File Offset: 0x0002F0AC
		private void Push(JsonSchemaGenerator.TypeSchema typeSchema)
		{
			this._currentSchema = typeSchema.Schema;
			this._stack.Add(typeSchema);
			this._resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00030EDC File Offset: 0x0002F0DC
		private JsonSchemaGenerator.TypeSchema Pop()
		{
			JsonSchemaGenerator.TypeSchema typeSchema = this._stack[this._stack.Count - 1];
			this._stack.RemoveAt(this._stack.Count - 1);
			JsonSchemaGenerator.TypeSchema typeSchema2 = this._stack.LastOrDefault<JsonSchemaGenerator.TypeSchema>();
			if (typeSchema2 != null)
			{
				this._currentSchema = typeSchema2.Schema;
				return typeSchema;
			}
			this._currentSchema = null;
			return typeSchema;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00030F3C File Offset: 0x0002F13C
		public JsonSchema Generate(Type type)
		{
			return this.Generate(type, new JsonSchemaResolver(), false);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00030F4B File Offset: 0x0002F14B
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return this.Generate(type, resolver, false);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00030F56 File Offset: 0x0002F156
		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return this.Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00030F65 File Offset: 0x0002F165
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			this._resolver = resolver;
			return this.GenerateInternal(type, (!rootSchemaNullable) ? Required.Always : Required.Default, false);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00030F94 File Offset: 0x0002F194
		private string GetTitle(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Title : null))
			{
				return cachedAttribute.Title;
			}
			return null;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00030FC4 File Offset: 0x0002F1C4
		private string GetDescription(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Description : null))
			{
				return cachedAttribute.Description;
			}
			DescriptionAttribute attribute = ReflectionUtils.GetAttribute<DescriptionAttribute>(type);
			if (attribute == null)
			{
				return null;
			}
			return attribute.Description;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00031004 File Offset: 0x0002F204
		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Id : null))
			{
				return cachedAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			UndefinedSchemaIdHandling undefinedSchemaIdHandling = this.UndefinedSchemaIdHandling;
			if (undefinedSchemaIdHandling == UndefinedSchemaIdHandling.UseTypeName)
			{
				return type.FullName;
			}
			if (undefinedSchemaIdHandling != UndefinedSchemaIdHandling.UseAssemblyQualifiedName)
			{
				return null;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00031058 File Offset: 0x0002F258
		private JsonSchema GenerateInternal(Type type, Required valueRequired, bool required)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = this.GetTypeId(type, false);
			string typeId2 = this.GetTypeId(type, true);
			if (!StringUtils.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = this._resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !JsonSchemaGenerator.HasFlag(schema.Type, JsonSchemaType.Null))
					{
						schema.Type |= JsonSchemaType.Null;
					}
					if (required)
					{
						bool? required2 = schema.Required;
						bool flag = true;
						if (!((required2.GetValueOrDefault() == flag) & (required2 != null)))
						{
							schema.Required = new bool?(true);
						}
					}
					return schema;
				}
			}
			if (this._stack.Any((JsonSchemaGenerator.TypeSchema tc) => tc.Type == type))
			{
				throw new JsonException("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			JsonContract jsonContract = this.ContractResolver.ResolveContract(type);
			bool flag2 = (jsonContract.Converter ?? jsonContract.InternalConverter) != null;
			this.Push(new JsonSchemaGenerator.TypeSchema(type, new JsonSchema()));
			if (typeId2 != null)
			{
				this.CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				this.CurrentSchema.Required = new bool?(true);
			}
			this.CurrentSchema.Title = this.GetTitle(type);
			this.CurrentSchema.Description = this.GetDescription(type);
			if (flag2)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			}
			else
			{
				switch (jsonContract.ContractType)
				{
				case JsonContractType.Object:
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateObjectSchema(type, (JsonObjectContract)jsonContract);
					break;
				case JsonContractType.Array:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Array, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					JsonArrayAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonArrayAttribute>(type);
					bool flag3 = cachedAttribute == null || cachedAttribute.AllowNullItems;
					Type collectionItemType = ReflectionUtils.GetCollectionItemType(type);
					if (collectionItemType != null)
					{
						this.CurrentSchema.Items = new List<JsonSchema>();
						this.CurrentSchema.Items.Add(this.GenerateInternal(collectionItemType, (!flag3) ? Required.Always : Required.Default, false));
					}
					break;
				}
				case JsonContractType.Primitive:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.GetJsonSchemaType(type, valueRequired));
					JsonSchemaType? type2 = this.CurrentSchema.Type;
					JsonSchemaType jsonSchemaType = JsonSchemaType.Integer;
					if (((type2.GetValueOrDefault() == jsonSchemaType) & (type2 != null)) && type.IsEnum() && !type.IsDefined(typeof(FlagsAttribute), true))
					{
						this.CurrentSchema.Enum = new List<JToken>();
						EnumInfo enumValuesAndNames = EnumUtils.GetEnumValuesAndNames(type);
						for (int i = 0; i < enumValuesAndNames.Names.Length; i++)
						{
							ulong num = enumValuesAndNames.Values[i];
							JToken jtoken = JToken.FromObject(Enum.ToObject(type, num));
							this.CurrentSchema.Enum.Add(jtoken);
						}
					}
					break;
				}
				case JsonContractType.String:
				{
					JsonSchemaType jsonSchemaType2 = ((!ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? JsonSchemaType.String : this.AddNullType(JsonSchemaType.String, valueRequired));
					this.CurrentSchema.Type = new JsonSchemaType?(jsonSchemaType2);
					break;
				}
				case JsonContractType.Dictionary:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					Type type3;
					Type type4;
					ReflectionUtils.GetDictionaryKeyValueTypes(type, out type3, out type4);
					if (type3 != null && this.ContractResolver.ResolveContract(type3).ContractType == JsonContractType.Primitive)
					{
						this.CurrentSchema.AdditionalProperties = this.GenerateInternal(type4, Required.Default, false);
					}
					break;
				}
				case JsonContractType.Dynamic:
				case JsonContractType.Linq:
					this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
					break;
				case JsonContractType.Serializable:
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateISerializableContract(type, (JsonISerializableContract)jsonContract);
					break;
				default:
					throw new JsonException("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContract));
				}
			}
			return this.Pop().Schema;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00031513 File Offset: 0x0002F713
		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002A0CC File Offset: 0x000282CC
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00031520 File Offset: 0x0002F720
		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			this.CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				if (!jsonProperty.Ignored)
				{
					NullValueHandling? nullValueHandling = jsonProperty.NullValueHandling;
					NullValueHandling nullValueHandling2 = NullValueHandling.Ignore;
					bool flag = ((nullValueHandling.GetValueOrDefault() == nullValueHandling2) & (nullValueHandling != null)) || this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || jsonProperty.ShouldSerialize != null || jsonProperty.GetIsSpecified != null;
					JsonSchema jsonSchema = this.GenerateInternal(jsonProperty.PropertyType, jsonProperty.Required, !flag);
					if (jsonProperty.DefaultValue != null)
					{
						jsonSchema.Default = JToken.FromObject(jsonProperty.DefaultValue);
					}
					this.CurrentSchema.Properties.Add(jsonProperty.PropertyName, jsonSchema);
				}
			}
			if (type.IsSealed())
			{
				this.CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00031630 File Offset: 0x0002F830
		private void GenerateISerializableContract(Type type, JsonISerializableContract contract)
		{
			this.CurrentSchema.AllowAdditionalProperties = true;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00031640 File Offset: 0x0002F840
		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			if (value == null)
			{
				return true;
			}
			JsonSchemaType? jsonSchemaType = value & flag;
			if ((jsonSchemaType.GetValueOrDefault() == flag) & (jsonSchemaType != null))
			{
				return true;
			}
			if (flag == JsonSchemaType.Integer)
			{
				jsonSchemaType = value & JsonSchemaType.Float;
				JsonSchemaType jsonSchemaType2 = JsonSchemaType.Float;
				if ((jsonSchemaType.GetValueOrDefault() == jsonSchemaType2) & (jsonSchemaType != null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000316DC File Offset: 0x0002F8DC
		private JsonSchemaType GetJsonSchemaType(Type type, Required valueRequired)
		{
			JsonSchemaType jsonSchemaType = JsonSchemaType.None;
			if (valueRequired != Required.Always && ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = JsonSchemaType.Null;
				if (ReflectionUtils.IsNullableType(type))
				{
					type = Nullable.GetUnderlyingType(type);
				}
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Empty:
			case PrimitiveTypeCode.Object:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Char:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Boolean:
				return jsonSchemaType | JsonSchemaType.Boolean;
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
			case PrimitiveTypeCode.BigInteger:
				return jsonSchemaType | JsonSchemaType.Integer;
			case PrimitiveTypeCode.Single:
			case PrimitiveTypeCode.Double:
			case PrimitiveTypeCode.Decimal:
				return jsonSchemaType | JsonSchemaType.Float;
			case PrimitiveTypeCode.DateTime:
			case PrimitiveTypeCode.DateTimeOffset:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Guid:
			case PrimitiveTypeCode.TimeSpan:
			case PrimitiveTypeCode.Uri:
			case PrimitiveTypeCode.String:
			case PrimitiveTypeCode.Bytes:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.DBNull:
				return jsonSchemaType | JsonSchemaType.Null;
			}
			throw new JsonException("Unexpected type code '{0}' for type '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeCode, type));
		}

		// Token: 0x04000617 RID: 1559
		private IContractResolver _contractResolver;

		// Token: 0x04000618 RID: 1560
		private JsonSchemaResolver _resolver;

		// Token: 0x04000619 RID: 1561
		private readonly IList<JsonSchemaGenerator.TypeSchema> _stack = new List<JsonSchemaGenerator.TypeSchema>();

		// Token: 0x0400061A RID: 1562
		private JsonSchema _currentSchema;

		// Token: 0x02000146 RID: 326
		private class TypeSchema
		{
			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00031810 File Offset: 0x0002FA10
			public Type Type { get; }

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00031818 File Offset: 0x0002FA18
			public JsonSchema Schema { get; }

			// Token: 0x06000A78 RID: 2680 RVA: 0x00031820 File Offset: 0x0002FA20
			public TypeSchema(Type type, JsonSchema schema)
			{
				ValidationUtils.ArgumentNotNull(type, "type");
				ValidationUtils.ArgumentNotNull(schema, "schema");
				this.Type = type;
				this.Schema = schema;
			}
		}
	}
}

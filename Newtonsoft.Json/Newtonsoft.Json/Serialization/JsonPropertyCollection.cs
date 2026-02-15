using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000124 RID: 292
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00028488 File Offset: 0x00026688
		public JsonPropertyCollection(Type type)
			: base(StringComparer.Ordinal)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			this._type = type;
			this._list = (List<JsonProperty>)base.Items;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000284B8 File Offset: 0x000266B8
		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000284C0 File Offset: 0x000266C0
		public void AddProperty(JsonProperty property)
		{
			if (base.Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				bool flag = true;
				if (jsonProperty.Ignored)
				{
					base.Remove(jsonProperty);
					flag = false;
				}
				else if (property.DeclaringType != null && jsonProperty.DeclaringType != null)
				{
					if (property.DeclaringType.IsSubclassOf(jsonProperty.DeclaringType) || (jsonProperty.DeclaringType.IsInterface() && property.DeclaringType.ImplementInterface(jsonProperty.DeclaringType)))
					{
						base.Remove(jsonProperty);
						flag = false;
					}
					if (jsonProperty.DeclaringType.IsSubclassOf(property.DeclaringType) || (property.DeclaringType.IsInterface() && jsonProperty.DeclaringType.ImplementInterface(property.DeclaringType)))
					{
						return;
					}
					if (this._type.ImplementInterface(jsonProperty.DeclaringType) && this._type.ImplementInterface(property.DeclaringType))
					{
						return;
					}
				}
				if (flag)
				{
					throw new JsonSerializationException("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, this._type));
				}
			}
			base.Add(property);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000285F4 File Offset: 0x000267F4
		[return: Nullable(2)]
		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty jsonProperty = this.GetProperty(propertyName, StringComparison.Ordinal);
			if (jsonProperty == null)
			{
				jsonProperty = this.GetProperty(propertyName, StringComparison.OrdinalIgnoreCase);
			}
			return jsonProperty;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00028617 File Offset: 0x00026817
		private bool TryGetProperty(string key, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out JsonProperty item)
		{
			if (base.Dictionary == null)
			{
				item = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, out item);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00028634 File Offset: 0x00026834
		[return: Nullable(2)]
		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			if (comparisonType != StringComparison.Ordinal)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					JsonProperty jsonProperty = this._list[i];
					if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
					{
						return jsonProperty;
					}
				}
				return null;
			}
			JsonProperty jsonProperty2;
			if (this.TryGetProperty(propertyName, out jsonProperty2))
			{
				return jsonProperty2;
			}
			return null;
		}

		// Token: 0x04000588 RID: 1416
		private readonly Type _type;

		// Token: 0x04000589 RID: 1417
		private readonly List<JsonProperty> _list;
	}
}

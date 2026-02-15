using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000107 RID: 263
	[NullableContext(1)]
	[Nullable(0)]
	internal class DefaultReferenceResolver : IReferenceResolver
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x00026630 File Offset: 0x00024830
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase = context as JsonSerializerInternalBase;
			if (jsonSerializerInternalBase == null)
			{
				JsonSerializerProxy jsonSerializerProxy = context as JsonSerializerProxy;
				if (jsonSerializerProxy == null)
				{
					throw new JsonException("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = jsonSerializerProxy.GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002666C File Offset: 0x0002486C
		public object ResolveReference(object context, string reference)
		{
			object obj;
			this.GetMappings(context).TryGetByFirst(reference, out obj);
			return obj;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002668C File Offset: 0x0002488C
		public string GetReference(object context, object value)
		{
			BidirectionalDictionary<string, object> mappings = this.GetMappings(context);
			string text;
			if (!mappings.TryGetBySecond(value, out text))
			{
				this._referenceCount++;
				text = this._referenceCount.ToString(CultureInfo.InvariantCulture);
				mappings.Set(text, value);
			}
			return text;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000266D4 File Offset: 0x000248D4
		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Set(reference, value);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000266E4 File Offset: 0x000248E4
		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		// Token: 0x04000503 RID: 1283
		private int _referenceCount;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000161 RID: 353
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x00033340 File Offset: 0x00031540
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false);
			for (int i = 0; i < this._values.Count; i++)
			{
				await this._values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false);
			}
			await writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003339B File Offset: 0x0003159B
		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000333A8 File Offset: 0x000315A8
		public new static async Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray a = new JArray();
			a.SetLineInfo(reader as IJsonLineInfo, settings);
			await a.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return a;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000333FB File Offset: 0x000315FB
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00033403 File Offset: 0x00031603
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00033406 File Offset: 0x00031606
		public JArray()
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00033419 File Offset: 0x00031619
		public JArray(JArray other)
			: base(other, null)
		{
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0003342E File Offset: 0x0003162E
		internal JArray(JArray other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00033443 File Offset: 0x00031643
		public JArray(params object[] content)
			: this(content)
		{
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0003344C File Offset: 0x0003164C
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00033468 File Offset: 0x00031668
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00033488 File Offset: 0x00031688
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings = null)
		{
			return new JArray(this, settings);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00033491 File Offset: 0x00031691
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0003349C File Offset: 0x0003169C
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00033510 File Offset: 0x00031710
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0003351C File Offset: 0x0003171C
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray jarray2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jarray2 = jarray;
			}
			return jarray2;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00033564 File Offset: 0x00031764
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00033574 File Offset: 0x00031774
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000335B8 File Offset: 0x000317B8
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170001D2 RID: 466
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170001D3 RID: 467
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00033686 File Offset: 0x00031886
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003369C File Offset: 0x0003189C
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = ((base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null);
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000336D0 File Offset: 0x000318D0
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000336D9 File Offset: 0x000318D9
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false, true);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000336E6 File Offset: 0x000318E6
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000336F0 File Offset: 0x000318F0
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003370B File Offset: 0x0003190B
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00033714 File Offset: 0x00031914
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003371C File Offset: 0x0003191C
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00033725 File Offset: 0x00031925
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00016B42 File Offset: 0x00014D42
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003372F File Offset: 0x0003192F
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00033738 File Offset: 0x00031938
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x04000679 RID: 1657
		private readonly List<JToken> _values = new List<JToken>();
	}
}

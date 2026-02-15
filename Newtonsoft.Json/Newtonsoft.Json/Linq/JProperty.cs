using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000173 RID: 371
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x00036FF0 File Offset: 0x000351F0
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00037028 File Offset: 0x00035228
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0003708C File Offset: 0x0003528C
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000370B4 File Offset: 0x000352B4
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000370C0 File Offset: 0x000352C0
		public new static async Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty p = new JProperty((string)reader.Value);
			p.SetLineInfo(reader as IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return p;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00037113 File Offset: 0x00035313
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0003711B File Offset: 0x0003531B
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00037123 File Offset: 0x00035323
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00037130 File Offset: 0x00035330
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken jtoken = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, jtoken, false, true);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003716F File Offset: 0x0003536F
		public JProperty(JProperty other)
			: base(other, null)
		{
			this._name = other.Name;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00037190 File Offset: 0x00035390
		internal JProperty(JProperty other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
			this._name = other.Name;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000371B1 File Offset: 0x000353B1
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000371C4 File Offset: 0x000353C4
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0003721E File Offset: 0x0003541E
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003721E File Offset: 0x0003541E
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003723E File Offset: 0x0003543E
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00037254 File Offset: 0x00035454
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false, copyAnnotations);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000372A1 File Offset: 0x000354A1
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000372AC File Offset: 0x000354AC
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003721E File Offset: 0x0003541E
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000372E0 File Offset: 0x000354E0
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00037313 File Offset: 0x00035513
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JProperty(this, settings);
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0003731C File Offset: 0x0003551C
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0003731F File Offset: 0x0003551F
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00037344 File Offset: 0x00035544
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00037350 File Offset: 0x00035550
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000373A0 File Offset: 0x000355A0
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000373D2 File Offset: 0x000355D2
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000373F2 File Offset: 0x000355F2
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000373FC File Offset: 0x000355FC
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x040006DB RID: 1755
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x040006DC RID: 1756
		private readonly string _name;

		// Token: 0x02000174 RID: 372
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06000C52 RID: 3154 RVA: 0x0003747B File Offset: 0x0003567B
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x0003748A File Offset: 0x0003568A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x00037492 File Offset: 0x00035692
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x0003749B File Offset: 0x0003569B
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x000374A4 File Offset: 0x000356A4
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x000374AF File Offset: 0x000356AF
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06000C58 RID: 3160 RVA: 0x000374C2 File Offset: 0x000356C2
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000374D7 File Offset: 0x000356D7
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00016B42 File Offset: 0x00014D42
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000C5B RID: 3163 RVA: 0x000374E4 File Offset: 0x000356E4
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000C5C RID: 3164 RVA: 0x000374F2 File Offset: 0x000356F2
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x000374FE File Offset: 0x000356FE
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x17000200 RID: 512
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x040006DD RID: 1757
			[Nullable(2)]
			internal JToken _token;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000164 RID: 356
	[NullableContext(1)]
	[Nullable(0)]
	public class JConstructor : JContainer
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x00033BD0 File Offset: 0x00031DD0
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartConstructorAsync(this._name ?? string.Empty, cancellationToken).ConfigureAwait(false);
			for (int i = 0; i < this._values.Count; i++)
			{
				await this._values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false);
			}
			await writer.WriteEndConstructorAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00033C2B File Offset: 0x00031E2B
		public new static Task<JConstructor> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JConstructor.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00033C38 File Offset: 0x00031E38
		public new static async Task<JConstructor> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor c = new JConstructor((string)reader.Value);
			c.SetLineInfo(reader as IJsonLineInfo, settings);
			await c.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return c;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00033C8B File Offset: 0x00031E8B
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00033C93 File Offset: 0x00031E93
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00033CA8 File Offset: 0x00031EA8
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JConstructor jconstructor = content as JConstructor;
			if (jconstructor == null)
			{
				return;
			}
			if (jconstructor.Name != null)
			{
				this.Name = jconstructor.Name;
			}
			JContainer.MergeEnumerableContent(this, jconstructor, settings);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00033CDC File Offset: 0x00031EDC
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00033CE4 File Offset: 0x00031EE4
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return this._name;
			}
			[NullableContext(2)]
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00033CED File Offset: 0x00031EED
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00033CF0 File Offset: 0x00031EF0
		public JConstructor()
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00033D03 File Offset: 0x00031F03
		public JConstructor(JConstructor other)
			: base(other, null)
		{
			this._name = other.Name;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00033D24 File Offset: 0x00031F24
		internal JConstructor(JConstructor other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
			this._name = other.Name;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00033D45 File Offset: 0x00031F45
		public JConstructor(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00033D4F File Offset: 0x00031F4F
		public JConstructor(string name, object content)
			: this(name)
		{
			this.Add(content);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00033D5F File Offset: 0x00031F5F
		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Constructor name cannot be empty.", "name");
			}
			this._name = name;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00033DA0 File Offset: 0x00031FA0
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00033DD3 File Offset: 0x00031FD3
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings = null)
		{
			return new JConstructor(this, settings);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00033DDC File Offset: 0x00031FDC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			int count = this._values.Count;
			for (int i = 0; i < count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		// Token: 0x170001D8 RID: 472
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int num = (int)key;
					return this.GetItem(num);
				}
				throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int num = (int)key;
					this.SetItem(num, value);
					return;
				}
				throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00033EC0 File Offset: 0x000320C0
		internal override int GetDeepHashCode()
		{
			string name = this._name;
			return ((name != null) ? name.GetHashCode() : 0) ^ base.ContentsHashCode();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00033EDB File Offset: 0x000320DB
		public new static JConstructor Load(JsonReader reader)
		{
			return JConstructor.Load(reader, null);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00033EE4 File Offset: 0x000320E4
		public new static JConstructor Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo, settings);
			jconstructor.ReadTokenFrom(reader, settings);
			return jconstructor;
		}

		// Token: 0x0400068A RID: 1674
		[Nullable(2)]
		private string _name;

		// Token: 0x0400068B RID: 1675
		private readonly List<JToken> _values = new List<JToken>();
	}
}

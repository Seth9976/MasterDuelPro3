using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200016C RID: 364
	[NullableContext(1)]
	[Nullable(0)]
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertyChanging
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x00035EBC File Offset: 0x000340BC
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WriteStartObjectAsync(cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.<WriteToAsync>g__AwaitProperties|0_0(task, 0, writer, cancellationToken, converters);
			}
			for (int i = 0; i < this._properties.Count; i++)
			{
				task = this._properties[i].WriteToAsync(writer, cancellationToken, converters);
				if (!task.IsCompletedSuccessfully())
				{
					return this.<WriteToAsync>g__AwaitProperties|0_0(task, i + 1, writer, cancellationToken, converters);
				}
			}
			return writer.WriteEndObjectAsync(cancellationToken);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00035F2D File Offset: 0x0003412D
		public new static Task<JObject> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JObject.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00035F38 File Offset: 0x00034138
		public new static async Task<JObject> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
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
					throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject o = new JObject();
			o.SetLineInfo(reader as IJsonLineInfo, settings);
			await o.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return o;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00035F8B File Offset: 0x0003418B
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000BDC RID: 3036 RVA: 0x00035F94 File Offset: 0x00034194
		// (remove) Token: 0x06000BDD RID: 3037 RVA: 0x00035FCC File Offset: 0x000341CC
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000BDE RID: 3038 RVA: 0x00036004 File Offset: 0x00034204
		// (remove) Token: 0x06000BDF RID: 3039 RVA: 0x0003603C File Offset: 0x0003423C
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00036071 File Offset: 0x00034271
		public JObject()
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00036084 File Offset: 0x00034284
		public JObject(JObject other)
			: base(other, null)
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00036099 File Offset: 0x00034299
		internal JObject(JObject other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000360AE File Offset: 0x000342AE
		public JObject(params object[] content)
			: this(content)
		{
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000360B7 File Offset: 0x000342B7
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000360D4 File Offset: 0x000342D4
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && this._properties.Compare(jobject._properties);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000360FE File Offset: 0x000342FE
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._properties.IndexOfReference(item);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00036111 File Offset: 0x00034311
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck, bool copyAnnotations)
		{
			return (item == null || item.Type != JTokenType.Comment) && base.InsertItem(index, item, skipParentCheck, copyAnnotations);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003612C File Offset: 0x0003432C
		internal override void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name, base.GetType()));
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000361CC File Offset: 0x000343CC
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JObject jobject = content as JObject;
			if (jobject == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
			{
				JProperty jproperty = this.Property(keyValuePair.Key, (settings != null) ? settings.PropertyNameComparison : StringComparison.Ordinal);
				if (jproperty == null)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (keyValuePair.Value != null)
				{
					JContainer jcontainer = jproperty.Value as JContainer;
					if (jcontainer == null || jcontainer.Type != keyValuePair.Value.Type)
					{
						if (!JObject.IsNull(keyValuePair.Value) || (settings != null && settings.MergeNullValueHandling == MergeNullValueHandling.Merge))
						{
							jproperty.Value = keyValuePair.Value;
						}
					}
					else
					{
						jcontainer.Merge(keyValuePair.Value, settings);
					}
				}
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000362B8 File Offset: 0x000344B8
		private static bool IsNull(JToken token)
		{
			if (token.Type == JTokenType.Null)
			{
				return true;
			}
			JValue jvalue = token as JValue;
			return jvalue != null && jvalue.Value == null;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000362E8 File Offset: 0x000344E8
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOfItem(childProperty)));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, childProperty, childProperty, this.IndexOfItem(childProperty)));
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00036339 File Offset: 0x00034539
		internal void InternalPropertyChanging(JProperty childProperty)
		{
			this.OnPropertyChanging(childProperty.Name);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00036347 File Offset: 0x00034547
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JObject(this, settings);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00002E5B File Offset: 0x0000105B
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00036350 File Offset: 0x00034550
		public IEnumerable<JProperty> Properties()
		{
			return this._properties.Cast<JProperty>();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003635D File Offset: 0x0003455D
		[return: Nullable(2)]
		public JProperty Property(string name)
		{
			return this.Property(name, StringComparison.Ordinal);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00036368 File Offset: 0x00034568
		[return: Nullable(2)]
		public JProperty Property(string name, StringComparison comparison)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			if (this._properties.TryGetValue(name, out jtoken))
			{
				return (JProperty)jtoken;
			}
			if (comparison != StringComparison.Ordinal)
			{
				for (int i = 0; i < this._properties.Count; i++)
				{
					JProperty jproperty = (JProperty)this._properties[i];
					if (string.Equals(jproperty.Name, name, comparison))
					{
						return jproperty;
					}
				}
			}
			return null;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000363CF File Offset: 0x000345CF
		[return: Nullable(new byte[] { 0, 1 })]
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in this.Properties()
				select p.Value);
		}

		// Token: 0x170001F3 RID: 499
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		[Nullable(2)]
		public JToken this[string propertyName]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			[param: Nullable(2)]
			set
			{
				JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.OnPropertyChanging(propertyName);
				this.Add(propertyName, value);
				this.OnPropertyChanged(propertyName);
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000364E3 File Offset: 0x000346E3
		public new static JObject Load(JsonReader reader)
		{
			return JObject.Load(reader, null);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000364EC File Offset: 0x000346EC
		public new static JObject Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo, settings);
			jobject.ReadTokenFrom(reader, settings);
			return jobject;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0003656B File Offset: 0x0003476B
		public new static JObject Parse(string json)
		{
			return JObject.Parse(json, null);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00036574 File Offset: 0x00034774
		public new static JObject Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JObject jobject2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JObject jobject = JObject.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jobject2 = jobject;
			}
			return jobject2;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000365BC File Offset: 0x000347BC
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000365CC File Offset: 0x000347CC
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00036610 File Offset: 0x00034810
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00036652 File Offset: 0x00034852
		[NullableContext(2)]
		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, StringComparison.Ordinal);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0003665C File Offset: 0x0003485C
		[NullableContext(2)]
		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName, comparison);
			if (jproperty == null)
			{
				return null;
			}
			return jproperty.Value;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00036676 File Offset: 0x00034876
		public bool TryGetValue(string propertyName, StringComparison comparison, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00036687 File Offset: 0x00034887
		public void Add(string propertyName, [Nullable(2)] JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00036696 File Offset: 0x00034896
		public bool ContainsKey(string propertyName)
		{
			ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
			return this._properties.Contains(propertyName);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000366AF File Offset: 0x000348AF
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000366BC File Offset: 0x000348BC
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000366E0 File Offset: 0x000348E0
		public bool TryGetValue(string propertyName, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out JToken value)
		{
			JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00036707 File Offset: 0x00034907
		[Nullable(new byte[] { 1, 2 })]
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0003670E File Offset: 0x0003490E
		void ICollection<KeyValuePair<string, JToken>>.Add([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00036729 File Offset: 0x00034929
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00036734 File Offset: 0x00034934
		bool ICollection<KeyValuePair<string, JToken>>.Contains([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key, StringComparison.Ordinal);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00036764 File Offset: 0x00034964
		void ICollection<KeyValuePair<string, JToken>>.CopyTo([Nullable(new byte[] { 1, 0, 1, 2 })] KeyValuePair<string, JToken>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00016B42 File Offset: 0x00014D42
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00036820 File Offset: 0x00034A20
		bool ICollection<KeyValuePair<string, JToken>>.Remove([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00033738 File Offset: 0x00031938
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0003683C File Offset: 0x00034A3C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0003684B File Offset: 0x00034A4B
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00036864 File Offset: 0x00034A64
		protected virtual void OnPropertyChanging(string propertyName)
		{
			PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0003687D File Offset: 0x00034A7D
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00036888 File Offset: 0x00034A88
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties([Nullable(new byte[] { 2, 1 })] Attribute[] attributes)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[base.Count];
			int num = 0;
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				array[num] = new JPropertyDescriptor(keyValuePair.Key);
				num++;
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000368F0 File Offset: 0x00034AF0
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0003530B File Offset: 0x0003350B
		[NullableContext(2)]
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0003530B File Offset: 0x0003350B
		[NullableContext(2)]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000368F7 File Offset: 0x00034AF7
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0003530B File Offset: 0x0003350B
		[NullableContext(2)]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0003530B File Offset: 0x0003350B
		[NullableContext(2)]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003530B File Offset: 0x0003350B
		[return: Nullable(2)]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000368FE File Offset: 0x00034AFE
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents([Nullable(new byte[] { 2, 1 })] Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000368FE File Offset: 0x00034AFE
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00036905 File Offset: 0x00034B05
		[NullableContext(2)]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is JPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00036912 File Offset: 0x00034B12
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy());
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00036920 File Offset: 0x00034B20
		[CompilerGenerated]
		private async Task <WriteToAsync>g__AwaitProperties|0_0(Task task, int i, JsonWriter Writer, CancellationToken CancellationToken, JsonConverter[] Converters)
		{
			await task.ConfigureAwait(false);
			while (i < this._properties.Count)
			{
				await this._properties[i].WriteToAsync(Writer, CancellationToken, Converters).ConfigureAwait(false);
				i++;
			}
			await Writer.WriteEndObjectAsync(CancellationToken).ConfigureAwait(false);
		}

		// Token: 0x040006BF RID: 1727
		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		// Token: 0x0200016D RID: 365
		[Nullable(new byte[] { 0, 1 })]
		private class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			// Token: 0x06000C1F RID: 3103 RVA: 0x0003698D File Offset: 0x00034B8D
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, [Nullable(2)] out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x000369A0 File Offset: 0x00034BA0
			public override bool TrySetMember(JObject instance, SetMemberBinder binder, object value)
			{
				JToken jtoken = value as JToken;
				if (jtoken == null)
				{
					jtoken = new JValue(value);
				}
				instance[binder.Name] = jtoken;
				return true;
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x000369CC File Offset: 0x00034BCC
			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return from p in instance.Properties()
					select p.Name;
			}
		}
	}
}

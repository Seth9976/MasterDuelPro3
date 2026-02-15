using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000179 RID: 377
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal class JPropertyKeyedCollection : Collection<JToken>
	{
		// Token: 0x06000C76 RID: 3190 RVA: 0x000379E5 File Offset: 0x00035BE5
		public JPropertyKeyedCollection()
			: base(new List<JToken>())
		{
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000379F2 File Offset: 0x00035BF2
		private void AddKey(string key, JToken item)
		{
			this.EnsureDictionary();
			this._dictionary[key] = item;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00037A08 File Offset: 0x00035C08
		protected void ChangeItemKey(JToken item, string newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw new ArgumentException("The specified item does not exist in this KeyedCollection.");
			}
			string keyForItem = this.GetKeyForItem(item);
			if (!JPropertyKeyedCollection.Comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00037A54 File Offset: 0x00035C54
		protected override void ClearItems()
		{
			base.ClearItems();
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Clear();
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00037A6C File Offset: 0x00035C6C
		public bool Contains(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00037A94 File Offset: 0x00035C94
		private bool ContainsItem(JToken item)
		{
			if (this._dictionary == null)
			{
				return false;
			}
			string keyForItem = this.GetKeyForItem(item);
			JToken jtoken;
			return this._dictionary.TryGetValue(keyForItem, out jtoken);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00037AC1 File Offset: 0x00035CC1
		private void EnsureDictionary()
		{
			if (this._dictionary == null)
			{
				this._dictionary = new Dictionary<string, JToken>(JPropertyKeyedCollection.Comparer);
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00037ADB File Offset: 0x00035CDB
		private string GetKeyForItem(JToken item)
		{
			return ((JProperty)item).Name;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00037AE8 File Offset: 0x00035CE8
		protected override void InsertItem(int index, JToken item)
		{
			this.AddKey(this.GetKeyForItem(item), item);
			base.InsertItem(index, item);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00037B00 File Offset: 0x00035D00
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			JToken jtoken;
			return this._dictionary != null && this._dictionary.TryGetValue(key, out jtoken) && base.Remove(jtoken);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00037B40 File Offset: 0x00035D40
		protected override void RemoveItem(int index)
		{
			string keyForItem = this.GetKeyForItem(base.Items[index]);
			this.RemoveKey(keyForItem);
			base.RemoveItem(index);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00037B6E File Offset: 0x00035D6E
		private void RemoveKey(string key)
		{
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Remove(key);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00037B84 File Offset: 0x00035D84
		protected override void SetItem(int index, JToken item)
		{
			string keyForItem = this.GetKeyForItem(item);
			string keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (JPropertyKeyedCollection.Comparer.Equals(keyForItem2, keyForItem))
			{
				if (this._dictionary != null)
				{
					this._dictionary[keyForItem] = item;
				}
			}
			else
			{
				this.AddKey(keyForItem, item);
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x17000207 RID: 519
		public JToken this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00037C15 File Offset: 0x00035E15
		public bool TryGetValue(string key, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out JToken value)
		{
			if (this._dictionary == null)
			{
				value = null;
				return false;
			}
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00037C31 File Offset: 0x00035E31
		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00037C44 File Offset: 0x00035E44
		public ICollection<JToken> Values
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Values;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00037C57 File Offset: 0x00035E57
		public int IndexOfReference(JToken t)
		{
			return ((List<JToken>)base.Items).IndexOfReference(t);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00037C6C File Offset: 0x00035E6C
		public bool Compare(JPropertyKeyedCollection other)
		{
			if (this == other)
			{
				return true;
			}
			Dictionary<string, JToken> dictionary = this._dictionary;
			Dictionary<string, JToken> dictionary2 = other._dictionary;
			if (dictionary == null && dictionary2 == null)
			{
				return true;
			}
			if (dictionary == null)
			{
				return dictionary2.Count == 0;
			}
			if (dictionary2 == null)
			{
				return dictionary.Count == 0;
			}
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in dictionary)
			{
				JToken jtoken;
				if (!dictionary2.TryGetValue(keyValuePair.Key, out jtoken))
				{
					return false;
				}
				JProperty jproperty = (JProperty)keyValuePair.Value;
				JProperty jproperty2 = (JProperty)jtoken;
				if (jproperty.Value == null)
				{
					return jproperty2.Value == null;
				}
				if (!jproperty.Value.DeepEquals(jproperty2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040006F1 RID: 1777
		private static readonly IEqualityComparer<string> Comparer = StringComparer.Ordinal;

		// Token: 0x040006F2 RID: 1778
		[Nullable(new byte[] { 2, 1, 1 })]
		private Dictionary<string, JToken> _dictionary;
	}
}

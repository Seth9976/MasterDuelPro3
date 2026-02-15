using System;

namespace System.Collections.Generic
{
	/// <summary>Defines a key/value pair that can be set or retrieved.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000754 RID: 1876
	[Serializable]
	public readonly struct KeyValuePair<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structure with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		// Token: 0x06003BA5 RID: 15269 RVA: 0x000E6A53 File Offset: 0x000E4C53
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		/// <summary>Gets the key in the key/value pair.</summary>
		/// <returns>A <paramref name="TKey" /> that is the key of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000E6A63 File Offset: 0x000E4C63
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the value in the key/value pair.</summary>
		/// <returns>A <paramref name="TValue" /> that is the value of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />. </returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000E6A6B File Offset: 0x000E4C6B
		public TValue Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
		/// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
		// Token: 0x06003BA8 RID: 15272 RVA: 0x000E6A73 File Offset: 0x000E4C73
		public override string ToString()
		{
			return KeyValuePair.PairToString(this.Key, this.Value);
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000E6A90 File Offset: 0x000E4C90
		public void Deconstruct(out TKey key, out TValue value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04001F19 RID: 7961
		private readonly TKey key;

		// Token: 0x04001F1A RID: 7962
		private readonly TValue value;
	}
}

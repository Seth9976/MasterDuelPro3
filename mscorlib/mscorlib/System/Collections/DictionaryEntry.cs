using System;

namespace System.Collections
{
	/// <summary>Defines a dictionary key/value pair that can be set or retrieved.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006F4 RID: 1780
	[Serializable]
	public struct DictionaryEntry
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Collections.DictionaryEntry" /> type with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair. </param>
		/// <param name="value">The definition associated with <paramref name="key" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null and the .NET Framework version is 1.0 or 1.1. </exception>
		// Token: 0x060037E1 RID: 14305 RVA: 0x000DB89E File Offset: 0x000D9A9E
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		/// <summary>Gets or sets the key in the key/value pair.</summary>
		/// <returns>The key in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000DB8AE File Offset: 0x000D9AAE
		public object Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets or sets the value in the key/value pair.</summary>
		/// <returns>The value in the key/value pair.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000DB8B6 File Offset: 0x000D9AB6
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001E42 RID: 7746
		private object _key;

		// Token: 0x04001E43 RID: 7747
		private object _value;
	}
}

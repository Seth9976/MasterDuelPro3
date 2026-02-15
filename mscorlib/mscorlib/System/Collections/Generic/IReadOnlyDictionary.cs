using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a generic read-only collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the read-only dictionary. </typeparam>
	/// <typeparam name="TValue">The type of values in the read-only dictionary. </typeparam>
	// Token: 0x02000750 RID: 1872
	public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		/// <summary>Determines whether the read-only dictionary contains an element that has the specified key.</summary>
		/// <returns>true if the read-only dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B9B RID: 15259
		bool ContainsKey(TKey key);

		/// <summary>Gets the value that is associated with the specified key.</summary>
		/// <returns>true if the object that implements the <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> interface contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B9C RID: 15260
		bool TryGetValue(TKey key, out TValue value);

		/// <summary>Gets the element that has the specified key in the read-only dictionary.</summary>
		/// <returns>The element that has the specified key in the read-only dictionary.</returns>
		/// <param name="key">The key to locate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found. </exception>
		// Token: 0x170009B4 RID: 2484
		TValue this[TKey key] { get; }

		/// <summary>Gets an enumerable collection that contains the keys in the read-only dictionary. </summary>
		/// <returns>An enumerable collection that contains the keys in the read-only dictionary.</returns>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003B9E RID: 15262
		IEnumerable<TKey> Keys { get; }

		/// <summary>Gets an enumerable collection that contains the values in the read-only dictionary.</summary>
		/// <returns>An enumerable collection that contains the values in the read-only dictionary.</returns>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003B9F RID: 15263
		IEnumerable<TValue> Values { get; }
	}
}

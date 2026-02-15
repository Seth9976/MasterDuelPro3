using System;

namespace System.Collections
{
	/// <summary>Represents a nongeneric collection of key/value pairs.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006F8 RID: 1784
	public interface IDictionary : ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if the key does not exist.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089F RID: 2207
		object this[object key] { get; set; }

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060037F0 RID: 14320
		ICollection Keys { get; }

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060037F1 RID: 14321
		ICollection Values { get; }

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F2 RID: 14322
		bool Contains(object key);

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F3 RID: 14323
		void Add(object key, object value);

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F4 RID: 14324
		void Clear();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060037F5 RID: 14325
		bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060037F6 RID: 14326
		bool IsFixedSize { get; }

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F7 RID: 14327
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F8 RID: 14328
		void Remove(object key);
	}
}

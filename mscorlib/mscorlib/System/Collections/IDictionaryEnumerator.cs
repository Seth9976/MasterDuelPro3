using System;

namespace System.Collections
{
	/// <summary>Enumerates the elements of a nongeneric dictionary.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006F9 RID: 1785
	public interface IDictionaryEnumerator : IEnumerator
	{
		/// <summary>Gets the key of the current dictionary entry.</summary>
		/// <returns>The key of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060037F9 RID: 14329
		object Key { get; }

		/// <summary>Gets the value of the current dictionary entry.</summary>
		/// <returns>The value of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060037FA RID: 14330
		object Value { get; }

		/// <summary>Gets both the key and the value of the current dictionary entry.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> containing both the key and the value of the current dictionary entry.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060037FB RID: 14331
		DictionaryEntry Entry { get; }
	}
}

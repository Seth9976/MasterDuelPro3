using System;

namespace System.Collections
{
	/// <summary>Defines size, enumerators, and synchronization methods for all nongeneric collections.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006F6 RID: 1782
	public interface ICollection : IEnumerable
	{
		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037E9 RID: 14313
		void CopyTo(Array array, int index);

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060037EA RID: 14314
		int Count { get; }

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060037EB RID: 14315
		object SyncRoot { get; }

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060037EC RID: 14316
		bool IsSynchronized { get; }
	}
}

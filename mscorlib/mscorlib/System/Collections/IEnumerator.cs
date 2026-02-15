using System;

namespace System.Collections
{
	/// <summary>Supports a simple iteration over a non-generic collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006FB RID: 1787
	public interface IEnumerator
	{
		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037FD RID: 14333
		bool MoveNext();

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060037FE RID: 14334
		object Current { get; }

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037FF RID: 14335
		void Reset();
	}
}

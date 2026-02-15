using System;

namespace System.Collections
{
	/// <summary>Represents a non-generic collection of objects that can be individually accessed by index.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006FD RID: 1789
	public interface IList : ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A8 RID: 2216
		object this[int index] { get; set; }

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
		/// <param name="value">The object to add to the <see cref="T:System.Collections.IList" />. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003804 RID: 14340
		int Add(object value);

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003805 RID: 14341
		bool Contains(object value);

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003806 RID: 14342
		void Clear();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003807 RID: 14343
		bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003808 RID: 14344
		bool IsFixedSize { get; }

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003809 RID: 14345
		int IndexOf(object value);

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="value" /> is null reference in the <see cref="T:System.Collections.IList" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380A RID: 14346
		void Insert(int index, object value);

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380B RID: 14347
		void Remove(object value);

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600380C RID: 14348
		void RemoveAt(int index);
	}
}

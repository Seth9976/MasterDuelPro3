using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the base class for a generic read-only collection.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x02000735 RID: 1845
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> class that is a read-only wrapper around the specified list.</summary>
		/// <param name="list">The list to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000E427D File Offset: 0x000E247D
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.list = list;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> instance.</returns>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000E4295 File Offset: 0x000E2495
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.ReadOnlyCollection`1.Count" />. </exception>
		// Token: 0x17000963 RID: 2403
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />. The value can be null for reference types.</param>
		// Token: 0x06003AA3 RID: 15011 RVA: 0x000E42B0 File Offset: 0x000E24B0
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003AA4 RID: 15012 RVA: 0x000E42BE File Offset: 0x000E24BE
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> for the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000E42CD File Offset: 0x000E24CD
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, if found; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be null for reference types.</param>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000E42DA File Offset: 0x000E24DA
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, this property always returns true.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x0000C091 File Offset: 0x0000A291
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000965 RID: 2405
		T IList<T>.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AAA RID: 15018 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void ICollection<T>.Add(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AAB RID: 15019 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void ICollection<T>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AAC RID: 15020 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList<T>.Insert(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>true if <paramref name="value" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AAD RID: 15021 RVA: 0x000E42F1 File Offset: 0x000E24F1
		bool ICollection<T>.Remove(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		/// <summary>Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AAE RID: 15022 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList<T>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003AAF RID: 15023 RVA: 0x000E42FB File Offset: 0x000E24FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, this property always returns false.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000E4308 File Offset: 0x000E2508
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.list as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000E4354 File Offset: 0x000E2554
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.list.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			int count = this.list.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.list[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, this property always returns true.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x0000C091 File Offset: 0x0000A291
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, this property always returns true.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0000C091 File Offset: 0x0000A291
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">Always thrown if the property is set.</exception>
		// Token: 0x1700096A RID: 2410
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000E4463 File Offset: 0x000E2663
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return -1;
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003AB8 RID: 15032 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000E4470 File Offset: 0x000E2670
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of the type specified for the generic type parameter <paramref name="T" />.</exception>
		// Token: 0x06003ABA RID: 15034 RVA: 0x000E449D File Offset: 0x000E269D
		bool IList.Contains(object value)
		{
			return ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of the type specified for the generic type parameter <paramref name="T" />.</exception>
		// Token: 0x06003ABB RID: 15035 RVA: 0x000E44B5 File Offset: 0x000E26B5
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003ABC RID: 15036 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003ABD RID: 15037 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.  This implementation always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x06003ABE RID: 15038 RVA: 0x000E42E8 File Offset: 0x000E24E8
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x04001EE8 RID: 7912
		private IList<T> list;

		// Token: 0x04001EE9 RID: 7913
		[NonSerialized]
		private object _syncRoot;
	}
}

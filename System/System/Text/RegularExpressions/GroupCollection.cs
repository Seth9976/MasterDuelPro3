using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Returns the set of captured groups in a single match.</summary>
	// Token: 0x02000129 RID: 297
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Group>))]
	[Serializable]
	public class GroupCollection : IList<Group>, ICollection<Group>, IEnumerable<Group>, IEnumerable, IReadOnlyList<Group>, IReadOnlyCollection<Group>, IList, ICollection
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0001C8BD File Offset: 0x0001AABD
		internal GroupCollection(Match match, Hashtable caps)
		{
			this._match = match;
			this._captureMap = caps;
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00003BCC File Offset: 0x00001DCC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001C8D3 File Offset: 0x0001AAD3
		public int Count
		{
			get
			{
				return this._match._matchcount.Length;
			}
		}

		/// <summary>Enables access to a member of the collection by integer index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupnum" />.</returns>
		/// <param name="groupnum">The zero-based index of the collection member to be retrieved. </param>
		// Token: 0x170000FD RID: 253
		public Group this[int groupnum]
		{
			get
			{
				return this.GetGroup(groupnum);
			}
		}

		/// <summary>Enables access to a member of the collection by string index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupname" />.</returns>
		/// <param name="groupname">The name of a capturing group. </param>
		// Token: 0x170000FE RID: 254
		public Group this[string groupname]
		{
			get
			{
				if (this._match._regex != null)
				{
					return this.GetGroup(this._match._regex.GroupNumberFromName(groupname));
				}
				return Group.s_emptyGroup;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that contains all <see cref="T:System.Text.RegularExpressions.Group" /> objects in the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</returns>
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001C917 File Offset: 0x0001AB17
		public IEnumerator GetEnumerator()
		{
			return new GroupCollection.Enumerator(this);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001C917 File Offset: 0x0001AB17
		IEnumerator<Group> IEnumerable<Group>.GetEnumerator()
		{
			return new GroupCollection.Enumerator(this);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001C920 File Offset: 0x0001AB20
		private Group GetGroup(int groupnum)
		{
			if (this._captureMap != null)
			{
				int num;
				if (this._captureMap.TryGetValue(groupnum, out num))
				{
					return this.GetGroupImpl(num);
				}
			}
			else if (groupnum < this._match._matchcount.Length && groupnum >= 0)
			{
				return this.GetGroupImpl(groupnum);
			}
			return Group.s_emptyGroup;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001C974 File Offset: 0x0001AB74
		private Group GetGroupImpl(int groupnum)
		{
			if (groupnum == 0)
			{
				return this._match;
			}
			if (this._groups == null)
			{
				this._groups = new Group[this._match._matchcount.Length - 1];
				for (int i = 0; i < this._groups.Length; i++)
				{
					string text = this._match._regex.GroupNameFromNumber(i + 1);
					this._groups[i] = new Group(this._match.Text, this._match._matches[i + 1], this._match._matchcount[i + 1], text);
				}
			}
			return this._groups[groupnum - 1];
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Text.RegularExpressions.Match" /> object to synchronize.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001CA15 File Offset: 0x0001AC15
		public object SyncRoot
		{
			get
			{
				return this._match;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where the copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x060005AD RID: 1453 RVA: 0x0001CA20 File Offset: 0x0001AC20
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001CA60 File Offset: 0x0001AC60
		public void CopyTo(Group[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array[num] = this[i];
				num++;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001CACC File Offset: 0x0001ACCC
		int IList<Group>.IndexOf(Group item)
		{
			EqualityComparer<Group> @default = EqualityComparer<Group>.Default;
			for (int i = 0; i < this.Count; i++)
			{
				if (@default.Equals(this[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList<Group>.Insert(int index, Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList<Group>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000101 RID: 257
		Group IList<Group>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void ICollection<Group>.Add(Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void ICollection<Group>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001CB18 File Offset: 0x0001AD18
		bool ICollection<Group>.Contains(Group item)
		{
			return ((IList<Group>)this).IndexOf(item) >= 0;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001CB03 File Offset: 0x0001AD03
		bool ICollection<Group>.Remove(Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001CB03 File Offset: 0x0001AD03
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001CB27 File Offset: 0x0001AD27
		bool IList.Contains(object value)
		{
			return value is Group && ((ICollection<Group>)this).Contains((Group)value);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001CB3F File Offset: 0x0001AD3F
		int IList.IndexOf(object value)
		{
			if (!(value is Group))
			{
				return -1;
			}
			return ((IList<Group>)this).IndexOf((Group)value);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00003BCC File Offset: 0x00001DCC
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000103 RID: 259
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		internal GroupCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004C9 RID: 1225
		private readonly Match _match;

		// Token: 0x040004CA RID: 1226
		private readonly Hashtable _captureMap;

		// Token: 0x040004CB RID: 1227
		private Group[] _groups;

		// Token: 0x0200012A RID: 298
		private sealed class Enumerator : IEnumerator<Group>, IDisposable, IEnumerator
		{
			// Token: 0x060005C3 RID: 1475 RVA: 0x0001CB57 File Offset: 0x0001AD57
			internal Enumerator(GroupCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001CB70 File Offset: 0x0001AD70
			public bool MoveNext()
			{
				int count = this._collection.Count;
				if (this._index >= count)
				{
					return false;
				}
				this._index++;
				return this._index < count;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001CBAB File Offset: 0x0001ADAB
			public Group Current
			{
				get
				{
					if (this._index < 0 || this._index >= this._collection.Count)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection[this._index];
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001CBE5 File Offset: 0x0001ADE5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0001CBED File Offset: 0x0001ADED
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00002FA0 File Offset: 0x000011A0
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040004CC RID: 1228
			private readonly GroupCollection _collection;

			// Token: 0x040004CD RID: 1229
			private int _index;
		}
	}
}

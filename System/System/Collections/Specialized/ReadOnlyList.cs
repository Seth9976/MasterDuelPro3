using System;

namespace System.Collections.Specialized
{
	// Token: 0x0200030C RID: 780
	internal sealed class ReadOnlyList : IList, ICollection, IEnumerable
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x000549A3 File Offset: 0x00052BA3
		internal ReadOnlyList(IList list)
		{
			this._list = list;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000549B2 File Offset: 0x00052BB2
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00003BCC File Offset: 0x00001DCC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00003BCC File Offset: 0x00001DCC
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000549BF File Offset: 0x00052BBF
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x1700040C RID: 1036
		public object this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x000549DA File Offset: 0x00052BDA
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public int Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public void Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000549E7 File Offset: 0x00052BE7
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000549F5 File Offset: 0x00052BF5
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00054A04 File Offset: 0x00052C04
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00054A11 File Offset: 0x00052C11
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public void Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public void Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x04000B82 RID: 2946
		private readonly IList _list;
	}
}

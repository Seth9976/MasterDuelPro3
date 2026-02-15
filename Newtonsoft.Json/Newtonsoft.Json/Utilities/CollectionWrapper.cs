using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	internal class CollectionWrapper<[Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, IWrappedCollection, IList, ICollection
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0001B6C0 File Offset: 0x000198C0
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			ICollection<T> collection = list as ICollection<T>;
			if (collection != null)
			{
				this._genericCollection = collection;
				return;
			}
			this._list = list;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001B6F7 File Offset: 0x000198F7
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001B711 File Offset: 0x00019911
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001B73A File Offset: 0x0001993A
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001B75B File Offset: 0x0001995B
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001B783 File Offset: 0x00019983
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001B7A8 File Offset: 0x000199A8
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001B7C9 File Offset: 0x000199C9
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001B7EA File Offset: 0x000199EA
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001B828 File Offset: 0x00019A28
		public virtual IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? this._list.Cast<T>()).GetEnumerator();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001B854 File Offset: 0x00019A54
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001B878 File Offset: 0x00019A78
		[NullableContext(2)]
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001B894 File Offset: 0x00019A94
		[NullableContext(2)]
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001B8AC File Offset: 0x00019AAC
		[NullableContext(2)]
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001B8E1 File Offset: 0x00019AE1
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001B902 File Offset: 0x00019B02
		[NullableContext(2)]
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001B934 File Offset: 0x00019B34
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001B955 File Offset: 0x00019B55
		[NullableContext(2)]
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x170000B0 RID: 176
		[Nullable(2)]
		object IList.this[int index]
		{
			[NullableContext(2)]
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			[NullableContext(2)]
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001B9BF File Offset: 0x00019BBF
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00016B42 File Offset: 0x00014D42
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001B9CE File Offset: 0x00019BCE
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		[NullableContext(2)]
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001BA1F File Offset: 0x00019C1F
		[NullableContext(2)]
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001BA51 File Offset: 0x00019C51
		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		// Token: 0x0400039E RID: 926
		[Nullable(2)]
		private readonly IList _list;

		// Token: 0x0400039F RID: 927
		[Nullable(new byte[] { 2, 1 })]
		private readonly ICollection<T> _genericCollection;

		// Token: 0x040003A0 RID: 928
		[Nullable(2)]
		private object _syncRoot;
	}
}

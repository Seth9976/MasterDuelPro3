using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000052 RID: 82
	internal class MessageVector : IList, ICollection, IEnumerable
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000D738 File Offset: 0x0000B938
		internal object[] ObjectArray
		{
			get
			{
				object syncRoot = this.SyncRoot;
				object[] array2;
				lock (syncRoot)
				{
					object[] array = this.ToArray();
					this.Clear();
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D780 File Offset: 0x0000B980
		internal MessageVector(int cap, int incr)
		{
			this._innerList = ArrayList.Synchronized(new ArrayList(cap));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D79C File Offset: 0x0000B99C
		internal Message findMessageById(int msgId)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				for (int i = 0; i < this.Count; i++)
				{
					Message message;
					if ((message = (Message)this[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (message.MessageID == msgId)
					{
						return message;
					}
				}
				throw new FieldAccessException();
			}
			Message message2;
			return message2;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D814 File Offset: 0x0000BA14
		public object[] ToArray()
		{
			return this._innerList.ToArray();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D821 File Offset: 0x0000BA21
		public int Add(object value)
		{
			return this._innerList.Add(value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D82F File Offset: 0x0000BA2F
		public void Clear()
		{
			this._innerList.Clear();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public bool Contains(object value)
		{
			return this._innerList.Contains(value);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D84A File Offset: 0x0000BA4A
		public int IndexOf(object value)
		{
			return this._innerList.IndexOf(value);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D858 File Offset: 0x0000BA58
		public void Insert(int index, object value)
		{
			this._innerList.Insert(index, value);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000D867 File Offset: 0x0000BA67
		public bool IsFixedSize
		{
			get
			{
				return this._innerList.IsFixedSize;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D874 File Offset: 0x0000BA74
		public bool IsReadOnly
		{
			get
			{
				return this._innerList.IsReadOnly;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000D881 File Offset: 0x0000BA81
		public void Remove(object value)
		{
			this._innerList.Remove(value);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public void RemoveAt(int index)
		{
			this._innerList.RemoveAt(index);
		}

		// Token: 0x170000CB RID: 203
		public object this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				this._innerList[index] = value;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D8BA File Offset: 0x0000BABA
		public void CopyTo(Array array, int index)
		{
			this._innerList.CopyTo(array, index);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000D8C9 File Offset: 0x0000BAC9
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D8D6 File Offset: 0x0000BAD6
		public bool IsSynchronized
		{
			get
			{
				return this._innerList.IsSynchronized;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000D8E3 File Offset: 0x0000BAE3
		public object SyncRoot
		{
			get
			{
				return this._innerList.SyncRoot;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public IEnumerator GetEnumerator()
		{
			return this._innerList.GetEnumerator();
		}

		// Token: 0x040001B4 RID: 436
		private readonly ArrayList _innerList;
	}
}

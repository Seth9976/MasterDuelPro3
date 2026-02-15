using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x0200009D RID: 157
	internal sealed class Listeners<TElem> where TElem : class
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x00027826 File Offset: 0x00025A26
		internal Listeners(int ObjectID, Listeners<TElem>.Func<TElem, bool> notifyFilter)
		{
			this._listeners = new List<TElem>();
			this._filter = notifyFilter;
			this._objectID = ObjectID;
			this._listenerReaderCount = 0;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002784E File Offset: 0x00025A4E
		internal bool HasListeners
		{
			get
			{
				return 0 < this._listeners.Count;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002785E File Offset: 0x00025A5E
		internal void Add(TElem listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002786C File Offset: 0x00025A6C
		internal int IndexOfReference(TElem listener)
		{
			return Index.IndexOfReference<TElem>(this._listeners, listener);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002787C File Offset: 0x00025A7C
		internal void Remove(TElem listener)
		{
			int num = this.IndexOfReference(listener);
			this._listeners[num] = default(TElem);
			if (this._listenerReaderCount == 0)
			{
				this._listeners.RemoveAt(num);
				this._listeners.TrimExcess();
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000278C8 File Offset: 0x00025AC8
		internal void Notify<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3, Listeners<TElem>.Action<TElem, T1, T2, T3> action)
		{
			int count = this._listeners.Count;
			if (0 < count)
			{
				int num = -1;
				this._listenerReaderCount++;
				try
				{
					for (int i = 0; i < count; i++)
					{
						TElem telem = this._listeners[i];
						if (this._filter(telem))
						{
							action(telem, arg1, arg2, arg3);
						}
						else
						{
							this._listeners[i] = default(TElem);
							num = i;
						}
					}
				}
				finally
				{
					this._listenerReaderCount--;
				}
				if (this._listenerReaderCount == 0)
				{
					this.RemoveNullListeners(num);
				}
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00027974 File Offset: 0x00025B74
		private void RemoveNullListeners(int nullIndex)
		{
			int num = nullIndex;
			while (0 <= num)
			{
				if (this._listeners[num] == null)
				{
					this._listeners.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x04000316 RID: 790
		private readonly List<TElem> _listeners;

		// Token: 0x04000317 RID: 791
		private readonly Listeners<TElem>.Func<TElem, bool> _filter;

		// Token: 0x04000318 RID: 792
		private readonly int _objectID;

		// Token: 0x04000319 RID: 793
		private int _listenerReaderCount;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x060007C8 RID: 1992
		internal delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x060007CA RID: 1994
		internal delegate TResult Func<T1, TResult>(T1 arg1);
	}
}

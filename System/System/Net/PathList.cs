using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020003E7 RID: 999
	[Serializable]
	internal class PathList
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0006A752 File Offset: 0x00068952
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0006A760 File Offset: 0x00068960
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0006A7F0 File Offset: 0x000689F0
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x1700055F RID: 1375
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0006A854 File Offset: 0x00068A54
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0006A861 File Offset: 0x00068A61
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x04000FDC RID: 4060
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x020003E8 RID: 1000
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x060018DE RID: 6366 RVA: 0x0006A870 File Offset: 0x00068A70
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x04000FDD RID: 4061
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}

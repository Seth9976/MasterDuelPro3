using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a collection container for instances of the <see cref="T:System.Net.Cookie" /> class.</summary>
	// Token: 0x020003E2 RID: 994
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieCollection" /> class.</summary>
		// Token: 0x060018B4 RID: 6324 RVA: 0x0006943C File Offset: 0x0006763C
		public CookieCollection()
		{
			this.m_IsReadOnly = true;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>A <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Net.Cookie" /> to be found. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or <paramref name="index" /> is greater than or equal to <see cref="P:System.Net.CookieCollection.Count" />. </exception>
		// Token: 0x17000555 RID: 1365
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (Cookie)this.m_list[index];
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to a <see cref="T:System.Net.CookieCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null. </exception>
		// Token: 0x060018B6 RID: 6326 RVA: 0x00069494 File Offset: 0x00067694
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.m_version++;
			int num = this.IndexOf(cookie);
			if (num == -1)
			{
				this.m_list.Add(cookie);
				return;
			}
			this.m_list[num] = cookie;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the current instance.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is null. </exception>
		// Token: 0x060018B7 RID: 6327 RVA: 0x000694E4 File Offset: 0x000676E4
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		/// <summary>Gets the number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00069548 File Offset: 0x00067748
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to a <see cref="T:System.Net.CookieCollection" /> is thread safe.</summary>
		/// <returns>true if access to the <see cref="T:System.Net.CookieCollection" /> is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0001AE3D File Offset: 0x0001903D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of a <see cref="T:System.Net.CookieCollection" /> to an instance of the <see cref="T:System.Array" /> class, starting at a particular index.</summary>
		/// <param name="array">The target <see cref="T:System.Array" /> to which the <see cref="T:System.Net.CookieCollection" /> will be copied. </param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060018BB RID: 6331 RVA: 0x00069555 File Offset: 0x00067755
		public void CopyTo(Array array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00069564 File Offset: 0x00067764
		internal DateTime TimeStamp(CookieCollection.Stamp how)
		{
			switch (how)
			{
			case CookieCollection.Stamp.Set:
				this.m_TimeStamp = DateTime.Now;
				break;
			case CookieCollection.Stamp.SetToUnused:
				this.m_TimeStamp = DateTime.MinValue;
				break;
			case CookieCollection.Stamp.SetToMaxUsed:
				this.m_TimeStamp = DateTime.MaxValue;
				break;
			}
			return this.m_TimeStamp;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x000695B4 File Offset: 0x000677B4
		internal bool IsOtherVersionSeen
		{
			get
			{
				return this.m_has_other_versions;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000695BC File Offset: 0x000677BC
		internal int InternalAdd(Cookie cookie, bool isStrict)
		{
			int num = 1;
			if (isStrict)
			{
				IComparer comparer = Cookie.GetComparer();
				int num2 = 0;
				foreach (object obj in this.m_list)
				{
					Cookie cookie2 = (Cookie)obj;
					if (comparer.Compare(cookie, cookie2) == 0)
					{
						num = 0;
						if (cookie2.Variant <= cookie.Variant)
						{
							this.m_list[num2] = cookie;
							break;
						}
						break;
					}
					else
					{
						num2++;
					}
				}
				if (num2 == this.m_list.Count)
				{
					this.m_list.Add(cookie);
				}
			}
			else
			{
				this.m_list.Add(cookie);
			}
			if (cookie.Version != 1)
			{
				this.m_has_other_versions = true;
			}
			return num;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006968C File Offset: 0x0006788C
		internal int IndexOf(Cookie cookie)
		{
			IComparer comparer = Cookie.GetComparer();
			int num = 0;
			foreach (object obj in this.m_list)
			{
				Cookie cookie2 = (Cookie)obj;
				if (comparer.Compare(cookie, cookie2) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00069700 File Offset: 0x00067900
		internal void RemoveAt(int idx)
		{
			this.m_list.RemoveAt(idx);
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x060018C1 RID: 6337 RVA: 0x0006970E File Offset: 0x0006790E
		public IEnumerator GetEnumerator()
		{
			return new CookieCollection.CookieCollectionEnumerator(this);
		}

		// Token: 0x04000FC2 RID: 4034
		internal int m_version;

		// Token: 0x04000FC3 RID: 4035
		private ArrayList m_list = new ArrayList();

		// Token: 0x04000FC4 RID: 4036
		private DateTime m_TimeStamp = DateTime.MinValue;

		// Token: 0x04000FC5 RID: 4037
		private bool m_has_other_versions;

		// Token: 0x04000FC6 RID: 4038
		[OptionalField]
		private bool m_IsReadOnly;

		// Token: 0x020003E3 RID: 995
		internal enum Stamp
		{
			// Token: 0x04000FC8 RID: 4040
			Check,
			// Token: 0x04000FC9 RID: 4041
			Set,
			// Token: 0x04000FCA RID: 4042
			SetToUnused,
			// Token: 0x04000FCB RID: 4043
			SetToMaxUsed
		}

		// Token: 0x020003E4 RID: 996
		private class CookieCollectionEnumerator : IEnumerator
		{
			// Token: 0x060018C2 RID: 6338 RVA: 0x00069716 File Offset: 0x00067916
			internal CookieCollectionEnumerator(CookieCollection cookies)
			{
				this.m_cookies = cookies;
				this.m_count = cookies.Count;
				this.m_version = cookies.m_version;
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00069744 File Offset: 0x00067944
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_count)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					if (this.m_version != this.m_cookies.m_version)
					{
						throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					return this.m_cookies[this.m_index];
				}
			}

			// Token: 0x060018C4 RID: 6340 RVA: 0x000697AC File Offset: 0x000679AC
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cookies.m_version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_count)
				{
					return true;
				}
				this.m_index = this.m_count;
				return false;
			}

			// Token: 0x060018C5 RID: 6341 RVA: 0x00069804 File Offset: 0x00067A04
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04000FCC RID: 4044
			private CookieCollection m_cookies;

			// Token: 0x04000FCD RID: 4045
			private int m_count;

			// Token: 0x04000FCE RID: 4046
			private int m_index = -1;

			// Token: 0x04000FCF RID: 4047
			private int m_version;
		}
	}
}

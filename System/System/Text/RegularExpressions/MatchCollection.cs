using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x0200012D RID: 301
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Match>))]
	[Serializable]
	public class MatchCollection : IList<Match>, ICollection<Match>, IEnumerable<Match>, IEnumerable, IReadOnlyList<Match>, IReadOnlyCollection<Match>, IList, ICollection
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new List<Match>();
			this._done = false;
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases. </returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00003BCC File Offset: 0x00001DCC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001D150 File Offset: 0x0001B350
		public int Count
		{
			get
			{
				this.EnsureInitialized();
				return this._matches.Count;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the <see cref="T:System.Text.RegularExpressions.Match" /> collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />. </exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x1700010B RID: 267
		public virtual Match this[int i]
		{
			get
			{
				if (i < 0)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Match" /> objects within the <see cref="T:System.Text.RegularExpressions.MatchCollection" />.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001D189 File Offset: 0x0001B389
		public IEnumerator GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001D189 File Offset: 0x0001B389
		IEnumerator<Match> IEnumerable<Match>.GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001D194 File Offset: 0x0001B394
		private Match GetMatch(int i)
		{
			if (this._matches.Count > i)
			{
				return this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match.Length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001D235 File Offset: 0x0001B435
		private void EnsureInitialized()
		{
			if (!this._done)
			{
				this.GetMatch(int.MaxValue);
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001AE3D File Offset: 0x0001903D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001D24B File Offset: 0x0001B44B
		public void CopyTo(Array array, int arrayIndex)
		{
			this.EnsureInitialized();
			((ICollection)this._matches).CopyTo(array, arrayIndex);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001D260 File Offset: 0x0001B460
		public void CopyTo(Match[] array, int arrayIndex)
		{
			this.EnsureInitialized();
			this._matches.CopyTo(array, arrayIndex);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001D275 File Offset: 0x0001B475
		int IList<Match>.IndexOf(Match item)
		{
			this.EnsureInitialized();
			return this._matches.IndexOf(item);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList<Match>.Insert(int index, Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList<Match>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x1700010E RID: 270
		Match IList<Match>.this[int index]
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

		// Token: 0x060005ED RID: 1517 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void ICollection<Match>.Add(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void ICollection<Match>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001D292 File Offset: 0x0001B492
		bool ICollection<Match>.Contains(Match item)
		{
			this.EnsureInitialized();
			return this._matches.Contains(item);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001CB03 File Offset: 0x0001AD03
		bool ICollection<Match>.Remove(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001CB03 File Offset: 0x0001AD03
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001D2A6 File Offset: 0x0001B4A6
		bool IList.Contains(object value)
		{
			return value is Match && ((ICollection<Match>)this).Contains((Match)value);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001D2BE File Offset: 0x0001B4BE
		int IList.IndexOf(object value)
		{
			if (!(value is Match))
			{
				return -1;
			}
			return ((IList<Match>)this).IndexOf((Match)value);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00003BCC File Offset: 0x00001DCC
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001CB03 File Offset: 0x0001AD03
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000110 RID: 272
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

		// Token: 0x060005FB RID: 1531 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		internal MatchCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004D9 RID: 1241
		private readonly Regex _regex;

		// Token: 0x040004DA RID: 1242
		private readonly List<Match> _matches;

		// Token: 0x040004DB RID: 1243
		private bool _done;

		// Token: 0x040004DC RID: 1244
		private readonly string _input;

		// Token: 0x040004DD RID: 1245
		private readonly int _beginning;

		// Token: 0x040004DE RID: 1246
		private readonly int _length;

		// Token: 0x040004DF RID: 1247
		private int _startat;

		// Token: 0x040004E0 RID: 1248
		private int _prevlen;

		// Token: 0x0200012E RID: 302
		[Serializable]
		private sealed class Enumerator : IEnumerator<Match>, IDisposable, IEnumerator
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x0001D2D6 File Offset: 0x0001B4D6
			internal Enumerator(MatchCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001D2EC File Offset: 0x0001B4EC
			public bool MoveNext()
			{
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._collection.GetMatch(this._index) == null)
				{
					this._index = -2;
					return false;
				}
				return true;
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001D326 File Offset: 0x0001B526
			public Match Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection.GetMatch(this._index);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001D34D File Offset: 0x0001B54D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001D355 File Offset: 0x0001B555
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x00002FA0 File Offset: 0x000011A0
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040004E1 RID: 1249
			private readonly MatchCollection _collection;

			// Token: 0x040004E2 RID: 1250
			private int _index;
		}
	}
}

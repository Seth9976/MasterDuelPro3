using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020001A9 RID: 425
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00034613 File Offset: 0x00032813
		internal OidEnumerator(OidCollection oids)
		{
			this._oids = oids;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00034629 File Offset: 0x00032829
		public Oid Current
		{
			get
			{
				return this._oids[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0003463C File Offset: 0x0003283C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06000A30 RID: 2608 RVA: 0x00034644 File Offset: 0x00032844
		public bool MoveNext()
		{
			if (this._current >= this._oids.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06000A31 RID: 2609 RVA: 0x0003466C File Offset: 0x0003286C
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04000786 RID: 1926
		private readonly OidCollection _oids;

		// Token: 0x04000787 RID: 1927
		private int _current;
	}
}

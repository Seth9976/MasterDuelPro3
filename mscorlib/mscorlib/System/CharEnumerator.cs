using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System
{
	/// <summary>Supports iterating over a <see cref="T:System.String" /> object and reading its individual characters. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CF RID: 207
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, IEnumerator<char>, IDisposable, ICloneable
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00019FA8 File Offset: 0x000181A8
		internal CharEnumerator(string str)
		{
			this._str = str;
			this._index = -1;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a copy of the current <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000589 RID: 1417 RVA: 0x00019FBE File Offset: 0x000181BE
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Increments the internal index of the current <see cref="T:System.CharEnumerator" /> object to the next character of the enumerated string.</summary>
		/// <returns>true if the index is successfully incremented and within the enumerated string; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058A RID: 1418 RVA: 0x00019FC8 File Offset: 0x000181C8
		public bool MoveNext()
		{
			if (this._index < this._str.Length - 1)
			{
				this._index++;
				this._currentElement = this._str[this._index];
				return true;
			}
			this._index = this._str.Length;
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.CharEnumerator" /> class.</summary>
		// Token: 0x0600058B RID: 1419 RVA: 0x0001A023 File Offset: 0x00018223
		public void Dispose()
		{
			if (this._str != null)
			{
				this._index = this._str.Length;
			}
			this._str = null;
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object. For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />. </summary>
		/// <returns>The boxed Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Enumeration has not started.-or-Enumeration has ended.</exception>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001A045 File Offset: 0x00018245
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>The Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The index is invalid; that is, it is before the first or after the last character of the enumerated string. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001A052 File Offset: 0x00018252
		public char Current
		{
			get
			{
				if (this._index == -1)
				{
					throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
				}
				if (this._index >= this._str.Length)
				{
					throw new InvalidOperationException("Enumeration already finished.");
				}
				return this._currentElement;
			}
		}

		/// <summary>Initializes the index to a position logically before the first character of the enumerated string.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600058E RID: 1422 RVA: 0x0001A08C File Offset: 0x0001828C
		public void Reset()
		{
			this._currentElement = '\0';
			this._index = -1;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000176B9 File Offset: 0x000158B9
		internal CharEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002E3 RID: 739
		private string _str;

		// Token: 0x040002E4 RID: 740
		private int _index;

		// Token: 0x040002E5 RID: 741
		private char _currentElement;
	}
}

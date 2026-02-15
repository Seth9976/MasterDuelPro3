using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a formatter-friendly mechanism for parsing the data in <see cref="T:System.Runtime.Serialization.SerializationInfo" />. This class cannot be inherited.</summary>
	// Token: 0x020004AA RID: 1194
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06002635 RID: 9781 RVA: 0x0009A793 File Offset: 0x00098993
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this._members = members;
			this._data = info;
			this._types = types;
			this._numItems = numItems - 1;
			this._currItem = -1;
			this._current = false;
		}

		/// <summary>Updates the enumerator to the next item.</summary>
		/// <returns>true if a new element is found; otherwise, false.</returns>
		// Token: 0x06002636 RID: 9782 RVA: 0x0009A7C8 File Offset: 0x000989C8
		public bool MoveNext()
		{
			if (this._currItem < this._numItems)
			{
				this._currItem++;
				this._current = true;
			}
			else
			{
				this._current = false;
			}
			return this._current;
		}

		/// <summary>Gets the current item in the collection.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationEntry" /> that contains the current serialization data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration has not started or has already ended. </exception>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x0009A7FC File Offset: 0x000989FC
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the item currently being examined.</summary>
		/// <returns>The item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x0009A80C File Offset: 0x00098A0C
		public SerializationEntry Current
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return new SerializationEntry(this._members[this._currItem], this._data[this._currItem], this._types[this._currItem]);
			}
		}

		/// <summary>Resets the enumerator to the first item.</summary>
		// Token: 0x06002639 RID: 9785 RVA: 0x0009A858 File Offset: 0x00098A58
		public void Reset()
		{
			this._currItem = -1;
			this._current = false;
		}

		/// <summary>Gets the name for the item currently being examined.</summary>
		/// <returns>The item name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x0009A868 File Offset: 0x00098A68
		public string Name
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._members[this._currItem];
			}
		}

		/// <summary>Gets the value of the item currently being examined.</summary>
		/// <returns>The value of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x0009A88A File Offset: 0x00098A8A
		public object Value
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._data[this._currItem];
			}
		}

		/// <summary>Gets the type of the item currently being examined.</summary>
		/// <returns>The type of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x0009A8AC File Offset: 0x00098AAC
		public Type ObjectType
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._types[this._currItem];
			}
		}

		// Token: 0x04001247 RID: 4679
		private readonly string[] _members;

		// Token: 0x04001248 RID: 4680
		private readonly object[] _data;

		// Token: 0x04001249 RID: 4681
		private readonly Type[] _types;

		// Token: 0x0400124A RID: 4682
		private readonly int _numItems;

		// Token: 0x0400124B RID: 4683
		private int _currItem;

		// Token: 0x0400124C RID: 4684
		private bool _current;
	}
}

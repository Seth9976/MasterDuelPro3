using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to iterate through the access control entries (ACEs) in an access control list (ACL). </summary>
	// Token: 0x020003E6 RID: 998
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x060021DF RID: 8671 RVA: 0x0008D36B File Offset: 0x0008B56B
		internal AceEnumerator(GenericAcl owner)
		{
			this.owner = owner;
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection. This property gets the type-friendly version of the object. </summary>
		/// <returns>The current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0008D381 File Offset: 0x0008B581
		public GenericAce Current
		{
			get
			{
				if (this.current >= 0)
				{
					return this.owner[this.current];
				}
				return null;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0008D39F File Offset: 0x0008B59F
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060021E2 RID: 8674 RVA: 0x0008D3A7 File Offset: 0x0008B5A7
		public bool MoveNext()
		{
			if (this.current + 1 == this.owner.Count)
			{
				return false;
			}
			this.current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060021E3 RID: 8675 RVA: 0x0008D3CF File Offset: 0x0008B5CF
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x04001048 RID: 4168
		private GenericAcl owner;

		// Token: 0x04001049 RID: 4169
		private int current = -1;
	}
}

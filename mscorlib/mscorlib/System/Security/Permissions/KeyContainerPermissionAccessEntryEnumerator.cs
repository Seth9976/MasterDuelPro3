using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.</summary>
	// Token: 0x02000364 RID: 868
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x000778E0 File Offset: 0x00075AE0
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		/// <summary>Gets the current entry in the collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed before first calling the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method. The cursor is located before the first object in the collection.-or- The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed after a call to the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method returns false, which indicates that the cursor is located after the last object in the collection. </exception>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000778F4 File Offset: 0x00075AF4
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this.e.Current;
			}
		}

		/// <summary>Gets the current object in the collection.</summary>
		/// <returns>The current object in the collection.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00077906 File Offset: 0x00075B06
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06001E53 RID: 7763 RVA: 0x00077913 File Offset: 0x00075B13
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		// Token: 0x06001E54 RID: 7764 RVA: 0x00077920 File Offset: 0x00075B20
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000176B9 File Offset: 0x000158B9
		internal KeyContainerPermissionAccessEntryEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E0B RID: 3595
		private IEnumerator e;
	}
}

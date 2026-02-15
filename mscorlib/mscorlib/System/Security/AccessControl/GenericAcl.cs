using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.CommonAcl" />, <see cref="T:System.Security.AccessControl.DiscretionaryAcl" />, <see cref="T:System.Security.AccessControl.RawAcl" />, and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x020003F5 RID: 1013
	public abstract class GenericAcl : ICollection, IEnumerable
	{
		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAcl.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002239 RID: 8761
		public abstract int BinaryLength { get; }

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600223A RID: 8762
		public abstract int Count { get; }

		/// <summary>This property is always set to false. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.GenericAce" /> to get or set.</param>
		// Token: 0x170003EA RID: 1002
		public abstract GenericAce this[int index] { get; set; }

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</returns>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600223E RID: 8766
		public abstract byte Revision { get; }

		/// <summary>This property always returns null. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always returns null.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x00002645 File Offset: 0x00000845
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06002240 RID: 8768 RVA: 0x0008E2A4 File Offset: 0x0008C4A4
		public void CopyTo(GenericAce[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || array.Length - index < this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be non-negative integer and must not exceed array length - count");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06002241 RID: 8769 RVA: 0x0008E2FD File Offset: 0x0008C4FD
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((GenericAce[])array, index);
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06002242 RID: 8770
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Returns a new instance of the <see cref="T:System.Security.AccessControl.AceEnumerator" /> class.</summary>
		/// <returns>The <see cref="T:Security.AccessControl.AceEnumerator" /> that this method returns.</returns>
		// Token: 0x06002243 RID: 8771 RVA: 0x0008E30C File Offset: 0x0008C50C
		public AceEnumerator GetEnumerator()
		{
			return new AceEnumerator(this);
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Security.AccessControl.AceEnumerator" /> class cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.AceEnumerator" /> object, cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		// Token: 0x06002244 RID: 8772 RVA: 0x0008E314 File Offset: 0x0008C514
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are not associated with Directory Services objects.</summary>
		// Token: 0x04001099 RID: 4249
		public static readonly byte AclRevision = 2;

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are associated with Directory Services objects.</summary>
		// Token: 0x0400109A RID: 4250
		public static readonly byte AclRevisionDS = 4;

		/// <summary>The maximum allowed binary length of a <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		// Token: 0x0400109B RID: 4251
		public static readonly int MaxBinaryLength = 65536;
	}
}

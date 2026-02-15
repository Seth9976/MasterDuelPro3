using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.IPAddress" /> types.</summary>
	// Token: 0x02000456 RID: 1110
	[DefaultMember("Item")]
	public class IPAddressCollection : ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> class.</summary>
		// Token: 0x06001C26 RID: 7206 RVA: 0x0007BE2A File Offset: 0x0007A02A
		protected internal IPAddressCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.IPAddress" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001C27 RID: 7207 RVA: 0x0007BE3D File Offset: 0x0007A03D
		public virtual void CopyTo(IPAddress[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.IPAddress" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.IPAddress" /> types in this collection.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0007BE4C File Offset: 0x0007A04C
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00003BCC File Offset: 0x00001DCC
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06001C2A RID: 7210 RVA: 0x0007BE59 File Offset: 0x0007A059
		public virtual void Add(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0007BE6A File Offset: 0x0007A06A
		internal void InternalAdd(IPAddress address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.IPAddress" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.IPAddress" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> object to be searched in the collection.</param>
		// Token: 0x06001C2C RID: 7212 RVA: 0x0007BE78 File Offset: 0x0007A078
		public virtual bool Contains(IPAddress address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> types in this collection.</returns>
		// Token: 0x06001C2D RID: 7213 RVA: 0x0007BE86 File Offset: 0x0007A086
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> types in this collection.</returns>
		// Token: 0x06001C2E RID: 7214 RVA: 0x0007BE8E File Offset: 0x0007A08E
		public virtual IEnumerator<IPAddress> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x06001C2F RID: 7215 RVA: 0x0007BE59 File Offset: 0x0007A059
		public virtual bool Remove(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x06001C30 RID: 7216 RVA: 0x0007BE59 File Offset: 0x0007A059
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x040012C9 RID: 4809
		private Collection<IPAddress> addresses = new Collection<IPAddress>();
	}
}

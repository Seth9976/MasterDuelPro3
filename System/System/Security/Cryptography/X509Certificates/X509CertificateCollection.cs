using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a collection that stores <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class X509CertificateCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class.</summary>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00037014 File Offset: 0x00035214
		public X509CertificateCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from another <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> with which to initialize the new object. </param>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0003701C File Offset: 0x0003521C
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the entry at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170001F5 RID: 501
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> with the specified value to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>The index into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at which the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003703E File Offset: 0x0003523E
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003705C File Offset: 0x0003525C
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> you can use to iterate through the collection.</returns>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000370A0 File Offset: 0x000352A0
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		/// <summary>Builds a hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>A hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000370A8 File Offset: 0x000352A8
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		/// <summary>Enumerates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		// Token: 0x020001C3 RID: 451
		public class X509CertificateEnumerator : IEnumerator
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection.X509CertificateEnumerator" /> class for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <param name="mappings">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to enumerate. </param>
			// Token: 0x06000ACA RID: 2762 RVA: 0x000370B5 File Offset: 0x000352B5
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			/// <summary>Gets the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <returns>The current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000370C9 File Offset: 0x000352C9
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>The current X.509 certificate object in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000370DB File Offset: 0x000352DB
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x06000ACD RID: 2765 RVA: 0x000370E8 File Offset: 0x000352E8
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x06000ACE RID: 2766 RVA: 0x000370F5 File Offset: 0x000352F5
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x06000ACF RID: 2767 RVA: 0x000370E8 File Offset: 0x000352E8
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x0400082E RID: 2094
			private IEnumerator enumerator;
		}
	}
}

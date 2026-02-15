using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />. This class cannot be inherited.</summary>
	// Token: 0x020001D0 RID: 464
	public sealed class X509ExtensionEnumerator : IEnumerator
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x000391CA File Offset: 0x000373CA
		internal X509ExtensionEnumerator(ArrayList list)
		{
			this.enumerator = list.GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x000391DE File Offset: 0x000373DE
		public X509Extension Current
		{
			get
			{
				return (X509Extension)this.enumerator.Current;
			}
		}

		/// <summary>Gets an object from a collection.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x000391F0 File Offset: 0x000373F0
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06000B57 RID: 2903 RVA: 0x000391FD File Offset: 0x000373FD
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06000B58 RID: 2904 RVA: 0x0003920A File Offset: 0x0003740A
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x04000857 RID: 2135
		private IEnumerator enumerator;
	}
}

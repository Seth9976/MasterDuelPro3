using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x020001C6 RID: 454
	public class X509ChainElement
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00037368 File Offset: 0x00035568
		internal X509ChainElement(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			this.info = string.Empty;
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00037382 File Offset: 0x00035582
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0003738A File Offset: 0x0003558A
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00037392 File Offset: 0x00035592
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0003739A File Offset: 0x0003559A
		internal X509ChainStatusFlags StatusFlags
		{
			get
			{
				return this.compressed_status_flags;
			}
			set
			{
				this.compressed_status_flags = value;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000373A4 File Offset: 0x000355A4
		private int Count(X509ChainStatusFlags flags)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			while (num2++ < 32)
			{
				if ((flags & (X509ChainStatusFlags)num3) == (X509ChainStatusFlags)num3)
				{
					num++;
				}
				num3 <<= 1;
			}
			return num;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000373D3 File Offset: 0x000355D3
		private void Set(X509ChainStatus[] status, ref int position, X509ChainStatusFlags flags, X509ChainStatusFlags mask)
		{
			if ((flags & mask) != X509ChainStatusFlags.NoError)
			{
				status[position].Status = mask;
				status[position].StatusInformation = X509ChainStatus.GetInformation(mask);
				position++;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00037404 File Offset: 0x00035604
		internal void UncompressFlags()
		{
			if (this.compressed_status_flags == X509ChainStatusFlags.NoError)
			{
				this.status = new X509ChainStatus[0];
				return;
			}
			int num = this.Count(this.compressed_status_flags);
			this.status = new X509ChainStatus[num];
			int num2 = 0;
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.UntrustedRoot);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeNested);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Revoked);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.RevocationStatusUnknown);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Cyclic);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidExtension);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidPolicyConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidBasicConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidNameConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotSupportedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotDefinedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotPermittedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasExcludedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.PartialChain);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.OfflineRevocation);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NoIssuanceChainPolicy);
		}

		// Token: 0x04000831 RID: 2097
		private X509Certificate2 certificate;

		// Token: 0x04000832 RID: 2098
		private X509ChainStatus[] status;

		// Token: 0x04000833 RID: 2099
		private string info;

		// Token: 0x04000834 RID: 2100
		private X509ChainStatusFlags compressed_status_flags;
	}
}

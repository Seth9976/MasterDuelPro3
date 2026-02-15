using System;
using System.Collections;
using System.Text;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001CA RID: 458
	internal class X509ChainImplMono : X509ChainImpl
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x000377B8 File Offset: 0x000359B8
		public X509ChainImplMono(bool useMachineContext)
		{
			this.location = (useMachineContext ? StoreLocation.LocalMachine : StoreLocation.CurrentUser);
			this.elements = new X509ChainElementCollection();
			this.policy = new X509ChainPolicy();
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x000377E3 File Offset: 0x000359E3
		public override X509ChainElementCollection ChainElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000377EB File Offset: 0x000359EB
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void AddStatus(X509ChainStatusFlags error)
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000377F4 File Offset: 0x000359F4
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public override bool Build(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentException("certificate");
			}
			this.Reset();
			X509ChainStatusFlags x509ChainStatusFlags;
			try
			{
				x509ChainStatusFlags = this.BuildChainFrom(certificate);
				this.ValidateChain(x509ChainStatusFlags);
			}
			catch (CryptographicException ex)
			{
				throw new ArgumentException("certificate", ex);
			}
			X509ChainStatusFlags x509ChainStatusFlags2 = X509ChainStatusFlags.NoError;
			ArrayList arrayList = new ArrayList();
			foreach (X509ChainElement x509ChainElement in this.elements)
			{
				foreach (X509ChainStatus x509ChainStatus in x509ChainElement.ChainElementStatus)
				{
					if ((x509ChainStatusFlags2 & x509ChainStatus.Status) != x509ChainStatus.Status)
					{
						arrayList.Add(x509ChainStatus);
						x509ChainStatusFlags2 |= x509ChainStatus.Status;
					}
				}
			}
			if (x509ChainStatusFlags != X509ChainStatusFlags.NoError)
			{
				arrayList.Insert(0, new X509ChainStatus(x509ChainStatusFlags));
			}
			this.status = (X509ChainStatus[])arrayList.ToArray(typeof(X509ChainStatus));
			if (this.status.Length == 0 || this.ChainPolicy.VerificationFlags == X509VerificationFlags.AllFlags)
			{
				return true;
			}
			bool flag = true;
			X509ChainStatus[] chainElementStatus = this.status;
			int i = 0;
			while (i < chainElementStatus.Length)
			{
				X509ChainStatus x509ChainStatus2 = chainElementStatus[i];
				X509ChainStatusFlags x509ChainStatusFlags3 = x509ChainStatus2.Status;
				if (x509ChainStatusFlags3 <= X509ChainStatusFlags.InvalidNameConstraints)
				{
					if (x509ChainStatusFlags3 <= X509ChainStatusFlags.UntrustedRoot)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.NotTimeValid)
						{
							if (x509ChainStatusFlags3 != X509ChainStatusFlags.NotTimeNested)
							{
								if (x509ChainStatusFlags3 != X509ChainStatusFlags.UntrustedRoot)
								{
									goto IL_02E4;
								}
								goto IL_0216;
							}
							else
							{
								flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeNested) > X509VerificationFlags.NoFlag;
							}
						}
						else
						{
							flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeValid) > X509VerificationFlags.NoFlag;
						}
					}
					else if (x509ChainStatusFlags3 <= X509ChainStatusFlags.InvalidPolicyConstraints)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidExtension)
						{
							if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidPolicyConstraints)
							{
								goto IL_02E4;
							}
							goto IL_0274;
						}
						else
						{
							flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) > X509VerificationFlags.NoFlag;
						}
					}
					else if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidBasicConstraints)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidNameConstraints)
						{
							goto IL_02E4;
						}
						goto IL_028D;
					}
					else
					{
						flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidBasicConstraints) > X509VerificationFlags.NoFlag;
					}
				}
				else if (x509ChainStatusFlags3 <= X509ChainStatusFlags.PartialChain)
				{
					if (x509ChainStatusFlags3 <= X509ChainStatusFlags.HasNotPermittedNameConstraint)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.HasNotSupportedNameConstraint && x509ChainStatusFlags3 != X509ChainStatusFlags.HasNotPermittedNameConstraint)
						{
							goto IL_02E4;
						}
						goto IL_028D;
					}
					else
					{
						if (x509ChainStatusFlags3 == X509ChainStatusFlags.HasExcludedNameConstraint)
						{
							goto IL_028D;
						}
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.PartialChain)
						{
							goto IL_02E4;
						}
						goto IL_0216;
					}
				}
				else if (x509ChainStatusFlags3 <= X509ChainStatusFlags.CtlNotSignatureValid)
				{
					if (x509ChainStatusFlags3 != X509ChainStatusFlags.CtlNotTimeValid)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.CtlNotSignatureValid)
						{
							goto IL_02E4;
						}
					}
					else
					{
						flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreCtlNotTimeValid) > X509VerificationFlags.NoFlag;
					}
				}
				else if (x509ChainStatusFlags3 != X509ChainStatusFlags.CtlNotValidForUsage)
				{
					if (x509ChainStatusFlags3 != X509ChainStatusFlags.NoIssuanceChainPolicy)
					{
						goto IL_02E4;
					}
					goto IL_0274;
				}
				else
				{
					flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) > X509VerificationFlags.NoFlag;
				}
				IL_02E6:
				if (!flag)
				{
					return false;
				}
				i++;
				continue;
				IL_0216:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.AllowUnknownCertificateAuthority) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_0274:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidPolicy) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_028D:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidName) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_02E4:
				flag = false;
				goto IL_02E6;
			}
			return true;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00037B10 File Offset: 0x00035D10
		public override void Reset()
		{
			if (this.status != null && this.status.Length != 0)
			{
				this.status = null;
			}
			if (this.elements.Count > 0)
			{
				this.elements.Clear();
			}
			if (this.user_root_store != null)
			{
				this.user_root_store.Close();
				this.user_root_store = null;
			}
			if (this.root_store != null)
			{
				this.root_store.Close();
				this.root_store = null;
			}
			if (this.user_ca_store != null)
			{
				this.user_ca_store.Close();
				this.user_ca_store = null;
			}
			if (this.ca_store != null)
			{
				this.ca_store.Close();
				this.ca_store = null;
			}
			this.roots = null;
			this.cas = null;
			this.collection = null;
			this.bce_restriction = null;
			this.working_public_key = null;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00037BDC File Offset: 0x00035DDC
		private X509Certificate2Collection Roots
		{
			get
			{
				if (this.roots == null)
				{
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					X509Store lmrootStore = this.LMRootStore;
					if (this.location == StoreLocation.CurrentUser)
					{
						x509Certificate2Collection.AddRange(this.UserRootStore.Certificates);
					}
					x509Certificate2Collection.AddRange(lmrootStore.Certificates);
					this.roots = x509Certificate2Collection;
				}
				return this.roots;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00037C34 File Offset: 0x00035E34
		private X509Certificate2Collection CertificateAuthorities
		{
			get
			{
				if (this.cas == null)
				{
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					X509Store lmcastore = this.LMCAStore;
					if (this.location == StoreLocation.CurrentUser)
					{
						x509Certificate2Collection.AddRange(this.UserCAStore.Certificates);
					}
					x509Certificate2Collection.AddRange(lmcastore.Certificates);
					this.cas = x509Certificate2Collection;
				}
				return this.cas;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00037C8C File Offset: 0x00035E8C
		private X509Store LMRootStore
		{
			get
			{
				if (this.root_store == null)
				{
					this.root_store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
					try
					{
						this.root_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.root_store;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00037CD8 File Offset: 0x00035ED8
		private X509Store UserRootStore
		{
			get
			{
				if (this.user_root_store == null)
				{
					this.user_root_store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
					try
					{
						this.user_root_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.user_root_store;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00037D24 File Offset: 0x00035F24
		private X509Store LMCAStore
		{
			get
			{
				if (this.ca_store == null)
				{
					this.ca_store = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
					try
					{
						this.ca_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.ca_store;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00037D70 File Offset: 0x00035F70
		private X509Store UserCAStore
		{
			get
			{
				if (this.user_ca_store == null)
				{
					this.user_ca_store = new X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser);
					try
					{
						this.user_ca_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.user_ca_store;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00037DBC File Offset: 0x00035FBC
		private X509Certificate2Collection CertificateCollection
		{
			get
			{
				if (this.collection == null)
				{
					this.collection = new X509Certificate2Collection(this.ChainPolicy.ExtraStore);
					this.collection.AddRange(this.Roots);
					this.collection.AddRange(this.CertificateAuthorities);
				}
				return this.collection;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00037E10 File Offset: 0x00036010
		private X509ChainStatusFlags BuildChainFrom(X509Certificate2 certificate)
		{
			this.elements.Add(certificate);
			while (!this.IsChainComplete(certificate))
			{
				certificate = this.FindParent(certificate);
				if (certificate == null)
				{
					return X509ChainStatusFlags.PartialChain;
				}
				if (this.elements.Contains(certificate))
				{
					return X509ChainStatusFlags.Cyclic;
				}
				this.elements.Add(certificate);
			}
			if (!this.Roots.Contains(certificate))
			{
				this.elements[this.elements.Count - 1].StatusFlags |= X509ChainStatusFlags.UntrustedRoot;
			}
			return X509ChainStatusFlags.NoError;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00037E9C File Offset: 0x0003609C
		private X509Certificate2 SelectBestFromCollection(X509Certificate2 child, X509Certificate2Collection c)
		{
			int count = c.Count;
			if (count == 0)
			{
				return null;
			}
			if (count == 1)
			{
				return c[0];
			}
			X509Certificate2Collection x509Certificate2Collection = c.Find(X509FindType.FindByTimeValid, this.ChainPolicy.VerificationTime, false);
			int count2 = x509Certificate2Collection.Count;
			if (count2 != 0)
			{
				if (count2 == 1)
				{
					return x509Certificate2Collection[0];
				}
			}
			else
			{
				x509Certificate2Collection = c;
			}
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(child);
			if (string.IsNullOrEmpty(authorityKeyIdentifier))
			{
				return x509Certificate2Collection[0];
			}
			foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
			{
				string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(x509Certificate);
				if (authorityKeyIdentifier == subjectKeyIdentifier)
				{
					return x509Certificate;
				}
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00037F4C File Offset: 0x0003614C
		private X509Certificate2 FindParent(X509Certificate2 certificate)
		{
			X509Certificate2Collection x509Certificate2Collection = this.CertificateCollection.Find(X509FindType.FindBySubjectDistinguishedName, certificate.Issuer, false);
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(certificate);
			if (authorityKeyIdentifier != null && authorityKeyIdentifier.Length > 0)
			{
				x509Certificate2Collection.AddRange(this.CertificateCollection.Find(X509FindType.FindBySubjectKeyIdentifier, authorityKeyIdentifier, false));
			}
			X509Certificate2 x509Certificate = this.SelectBestFromCollection(certificate, x509Certificate2Collection);
			if (!certificate.Equals(x509Certificate))
			{
				return x509Certificate;
			}
			return null;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00037FAC File Offset: 0x000361AC
		private bool IsChainComplete(X509Certificate2 certificate)
		{
			if (!this.IsSelfIssued(certificate))
			{
				return false;
			}
			if (certificate.Version < 3)
			{
				return true;
			}
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(certificate);
			if (string.IsNullOrEmpty(subjectKeyIdentifier))
			{
				return true;
			}
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(certificate);
			return string.IsNullOrEmpty(authorityKeyIdentifier) || authorityKeyIdentifier == subjectKeyIdentifier;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00037FF9 File Offset: 0x000361F9
		private bool IsSelfIssued(X509Certificate2 certificate)
		{
			return certificate.Issuer == certificate.Subject;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0003800C File Offset: 0x0003620C
		private void ValidateChain(X509ChainStatusFlags flag)
		{
			int num = this.elements.Count - 1;
			X509Certificate2 certificate = this.elements[num].Certificate;
			if ((flag & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.NoError)
			{
				this.Process(num);
				if (num == 0)
				{
					this.elements[0].UncompressFlags();
					return;
				}
				num--;
			}
			this.working_public_key = certificate.PublicKey.Key;
			this.working_issuer_name = certificate.IssuerName;
			this.max_path_length = num;
			for (int i = num; i > 0; i--)
			{
				this.Process(i);
				this.PrepareForNextCertificate(i);
			}
			this.Process(0);
			this.CheckRevocationOnChain(flag);
			this.WrapUp();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000380B4 File Offset: 0x000362B4
		private void Process(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			X509Certificate monoCertificate = X509Helper2.GetMonoCertificate(certificate);
			if (n != this.elements.Count - 1 && monoCertificate.KeyAlgorithm == "1.2.840.10040.4.1" && monoCertificate.KeyAlgorithmParameters == null)
			{
				X509Certificate monoCertificate2 = X509Helper2.GetMonoCertificate(this.elements[n + 1].Certificate);
				monoCertificate.KeyAlgorithmParameters = monoCertificate2.KeyAlgorithmParameters;
			}
			bool flag = this.working_public_key == null;
			if (!this.IsSignedWith(certificate, flag ? certificate.PublicKey.Key : this.working_public_key) && (flag || n != this.elements.Count - 1 || this.IsSelfIssued(certificate)))
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotSignatureValid;
			}
			if (this.ChainPolicy.VerificationTime < certificate.NotBefore || this.ChainPolicy.VerificationTime > certificate.NotAfter)
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotTimeValid;
			}
			if (flag)
			{
				return;
			}
			if (!X500DistinguishedName.AreEqual(certificate.IssuerName, this.working_issuer_name))
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidNameConstraints;
			}
			if (!this.IsSelfIssued(certificate))
			{
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000381F4 File Offset: 0x000363F4
		private void PrepareForNextCertificate(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			this.working_issuer_name = certificate.SubjectName;
			this.working_public_key = certificate.PublicKey.Key;
			X509BasicConstraintsExtension x509BasicConstraintsExtension = certificate.Extensions["2.5.29.19"] as X509BasicConstraintsExtension;
			if (x509BasicConstraintsExtension != null)
			{
				if (!x509BasicConstraintsExtension.CertificateAuthority)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			else if (certificate.Version >= 3)
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
			}
			if (!this.IsSelfIssued(certificate))
			{
				if (this.max_path_length > 0)
				{
					this.max_path_length--;
				}
				else if (this.bce_restriction != null)
				{
					this.bce_restriction.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (x509BasicConstraintsExtension != null && x509BasicConstraintsExtension.HasPathLengthConstraint && x509BasicConstraintsExtension.PathLengthConstraint < this.max_path_length)
			{
				this.max_path_length = x509BasicConstraintsExtension.PathLengthConstraint;
				this.bce_restriction = x509ChainElement;
			}
			X509KeyUsageExtension x509KeyUsageExtension = certificate.Extensions["2.5.29.15"] as X509KeyUsageExtension;
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.KeyCertSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotValidForUsage;
				}
			}
			this.ProcessCertificateExtensions(x509ChainElement);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00038330 File Offset: 0x00036530
		private void WrapUp()
		{
			X509ChainElement x509ChainElement = this.elements[0];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			this.IsSelfIssued(certificate);
			this.ProcessCertificateExtensions(x509ChainElement);
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				this.elements[i].UncompressFlags();
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0003838C File Offset: 0x0003658C
		private void ProcessCertificateExtensions(X509ChainElement element)
		{
			foreach (X509Extension x509Extension in element.Certificate.Extensions)
			{
				if (x509Extension.Critical)
				{
					string value = x509Extension.Oid.Value;
					if (!(value == "2.5.29.15") && !(value == "2.5.29.19"))
					{
						element.StatusFlags |= X509ChainStatusFlags.InvalidExtension;
					}
				}
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000383FB File Offset: 0x000365FB
		private bool IsSignedWith(X509Certificate2 signed, AsymmetricAlgorithm pubkey)
		{
			return pubkey != null && X509Helper2.GetMonoCertificate(signed).VerifySignature(pubkey);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00038410 File Offset: 0x00036610
		private string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension != null)
			{
				return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
			}
			return string.Empty;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00038442 File Offset: 0x00036642
		private static string GetAuthorityKeyIdentifier(X509Certificate2 certificate)
		{
			return X509ChainImplMono.GetAuthorityKeyIdentifier(X509Helper2.GetMonoCertificate(certificate).Extensions["2.5.29.35"]);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0003845E File Offset: 0x0003665E
		private static string GetAuthorityKeyIdentifier(X509Crl crl)
		{
			return X509ChainImplMono.GetAuthorityKeyIdentifier(crl.Extensions["2.5.29.35"]);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00038478 File Offset: 0x00036678
		private static string GetAuthorityKeyIdentifier(X509Extension ext)
		{
			if (ext == null)
			{
				return string.Empty;
			}
			byte[] identifier = new AuthorityKeyIdentifierExtension(ext).Identifier;
			if (identifier == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in identifier)
			{
				stringBuilder.Append(b.ToString("X02"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000384D8 File Offset: 0x000366D8
		private void CheckRevocationOnChain(X509ChainStatusFlags flag)
		{
			bool flag2 = (flag & X509ChainStatusFlags.PartialChain) > X509ChainStatusFlags.NoError;
			bool flag3;
			switch (this.ChainPolicy.RevocationMode)
			{
			case X509RevocationMode.NoCheck:
				return;
			case X509RevocationMode.Online:
				flag3 = true;
				break;
			case X509RevocationMode.Offline:
				flag3 = false;
				break;
			default:
				throw new InvalidOperationException(global::Locale.GetText("Invalid revocation mode."));
			}
			bool flag4 = flag2;
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				bool flag5 = true;
				switch (this.ChainPolicy.RevocationFlag)
				{
				case X509RevocationFlag.EndCertificateOnly:
					flag5 = i == 0;
					break;
				case X509RevocationFlag.EntireChain:
					flag5 = true;
					break;
				case X509RevocationFlag.ExcludeRoot:
					flag5 = i != this.elements.Count - 1;
					break;
				}
				X509ChainElement x509ChainElement = this.elements[i];
				if (!flag4)
				{
					flag4 |= (x509ChainElement.StatusFlags & X509ChainStatusFlags.NotSignatureValid) > X509ChainStatusFlags.NoError;
				}
				if (flag4)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.RevocationStatusUnknown;
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.OfflineRevocation;
				}
				else if (flag5 && !flag2 && !this.IsSelfIssued(x509ChainElement.Certificate))
				{
					x509ChainElement.StatusFlags |= this.CheckRevocation(x509ChainElement.Certificate, i + 1, flag3);
					flag4 |= (x509ChainElement.StatusFlags & X509ChainStatusFlags.Revoked) > X509ChainStatusFlags.NoError;
				}
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00038624 File Offset: 0x00036824
		private X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, int ca, bool online)
		{
			X509ChainStatusFlags x509ChainStatusFlags = X509ChainStatusFlags.RevocationStatusUnknown;
			X509Certificate2 x509Certificate = this.elements[ca].Certificate;
			while (this.IsSelfIssued(x509Certificate) && ca < this.elements.Count - 1)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, x509Certificate, online);
				if (x509ChainStatusFlags != X509ChainStatusFlags.RevocationStatusUnknown)
				{
					break;
				}
				ca++;
				x509Certificate = this.elements[ca].Certificate;
			}
			if (x509ChainStatusFlags == X509ChainStatusFlags.RevocationStatusUnknown)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, x509Certificate, online);
			}
			return x509ChainStatusFlags;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00038698 File Offset: 0x00036898
		private X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, X509Certificate2 ca_cert, bool online)
		{
			X509KeyUsageExtension x509KeyUsageExtension = ca_cert.Extensions["2.5.29.15"] as X509KeyUsageExtension;
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.CrlSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					return X509ChainStatusFlags.RevocationStatusUnknown;
				}
			}
			X509Crl x509Crl = this.FindCrl(ca_cert);
			bool flag = x509Crl == null && online;
			if (x509Crl == null)
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			if (!x509Crl.VerifySignature(ca_cert.PublicKey.Key))
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			X509Certificate monoCertificate = X509Helper2.GetMonoCertificate(certificate);
			X509Crl.X509CrlEntry crlEntry = x509Crl.GetCrlEntry(monoCertificate);
			if (crlEntry != null)
			{
				if (!this.ProcessCrlEntryExtensions(crlEntry))
				{
					return X509ChainStatusFlags.Revoked;
				}
				if (crlEntry.RevocationDate <= this.ChainPolicy.VerificationTime)
				{
					return X509ChainStatusFlags.Revoked;
				}
			}
			if (x509Crl.NextUpdate < this.ChainPolicy.VerificationTime)
			{
				return X509ChainStatusFlags.RevocationStatusUnknown | X509ChainStatusFlags.OfflineRevocation;
			}
			if (!this.ProcessCrlExtensions(x509Crl))
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			return X509ChainStatusFlags.NoError;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00038760 File Offset: 0x00036960
		private static X509Crl CheckCrls(string subject, string ski, X509Store store)
		{
			if (store == null)
			{
				return null;
			}
			foreach (object obj in store.Crls)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (x509Crl.IssuerName == subject && (ski.Length == 0 || ski == X509ChainImplMono.GetAuthorityKeyIdentifier(x509Crl)))
				{
					return x509Crl;
				}
			}
			return null;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000387E4 File Offset: 0x000369E4
		private X509Crl FindCrl(X509Certificate2 caCertificate)
		{
			string text = caCertificate.SubjectName.Decode(X500DistinguishedNameFlags.None);
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(caCertificate);
			X509Crl x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.LMCAStore.Store);
			if (x509Crl != null)
			{
				return x509Crl;
			}
			if (this.location == StoreLocation.CurrentUser)
			{
				x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.UserCAStore.Store);
				if (x509Crl != null)
				{
					return x509Crl;
				}
			}
			x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.LMRootStore.Store);
			if (x509Crl != null)
			{
				return x509Crl;
			}
			if (this.location == StoreLocation.CurrentUser)
			{
				x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.UserRootStore.Store);
				if (x509Crl != null)
				{
					return x509Crl;
				}
			}
			return null;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0003887C File Offset: 0x00036A7C
		private bool ProcessCrlExtensions(X509Crl crl)
		{
			foreach (object obj in crl.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				if (x509Extension.Critical)
				{
					string oid = x509Extension.Oid;
					if (!(oid == "2.5.29.20") && !(oid == "2.5.29.35"))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00038904 File Offset: 0x00036B04
		private bool ProcessCrlEntryExtensions(X509Crl.X509CrlEntry entry)
		{
			foreach (object obj in entry.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				if (x509Extension.Critical && !(x509Extension.Oid == "2.5.29.21"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000837 RID: 2103
		private StoreLocation location;

		// Token: 0x04000838 RID: 2104
		private X509ChainElementCollection elements;

		// Token: 0x04000839 RID: 2105
		private X509ChainPolicy policy;

		// Token: 0x0400083A RID: 2106
		private X509ChainStatus[] status;

		// Token: 0x0400083B RID: 2107
		private static X509ChainStatus[] Empty = new X509ChainStatus[0];

		// Token: 0x0400083C RID: 2108
		private int max_path_length;

		// Token: 0x0400083D RID: 2109
		private X500DistinguishedName working_issuer_name;

		// Token: 0x0400083E RID: 2110
		private AsymmetricAlgorithm working_public_key;

		// Token: 0x0400083F RID: 2111
		private X509ChainElement bce_restriction;

		// Token: 0x04000840 RID: 2112
		private X509Certificate2Collection roots;

		// Token: 0x04000841 RID: 2113
		private X509Certificate2Collection cas;

		// Token: 0x04000842 RID: 2114
		private X509Store root_store;

		// Token: 0x04000843 RID: 2115
		private X509Store ca_store;

		// Token: 0x04000844 RID: 2116
		private X509Store user_root_store;

		// Token: 0x04000845 RID: 2117
		private X509Store user_ca_store;

		// Token: 0x04000846 RID: 2118
		private X509Certificate2Collection collection;
	}
}

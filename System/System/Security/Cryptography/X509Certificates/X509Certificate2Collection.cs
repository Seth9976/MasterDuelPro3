using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001BD RID: 445
	public class X509Certificate2Collection : X509CertificateCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class without any <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> information.</summary>
		// Token: 0x06000A7D RID: 2685 RVA: 0x0003618F File Offset: 0x0003438F
		public X509Certificate2Collection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using the specified certificate collection.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		// Token: 0x06000A7E RID: 2686 RVA: 0x00036197 File Offset: 0x00034397
		public X509Certificate2Collection(X509Certificate2Collection certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		// Token: 0x170001D6 RID: 470
		public X509Certificate2 this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("negative index");
				}
				if (index >= base.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index >= Count");
				}
				return (X509Certificate2)base.InnerList[index];
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> index at which the <paramref name="certificate" /> has been added.</returns>
		/// <param name="certificate">An X.509 certificate represented as an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06000A80 RID: 2688 RVA: 0x000361E1 File Offset: 0x000343E1
		public int Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return base.InnerList.Add(certificate);
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object to another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		// Token: 0x06000A81 RID: 2689 RVA: 0x000361FD File Offset: 0x000343FD
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			base.InnerList.AddRange(certificates);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object contains a specific certificate.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> contains the specified <paramref name="certificate" />; otherwise, false.</returns>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to locate in the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06000A82 RID: 2690 RVA: 0x0003621C File Offset: 0x0003441C
		public bool Contains(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			using (IEnumerator enumerator = base.InnerList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((X509Certificate2)enumerator.Current).Equals(certificate))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0003628C File Offset: 0x0003448C
		private string GetKeyIdentifier(X509Certificate2 x)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = x.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension == null)
			{
				x509SubjectKeyIdentifierExtension = new X509SubjectKeyIdentifierExtension(x.PublicKey, X509SubjectKeyIdentifierHashAlgorithm.CapiSha1, false);
			}
			return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
		}

		/// <summary>Searches an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object using the search criteria specified by the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> enumeration and the <paramref name="findValue" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <param name="findType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" />  values. </param>
		/// <param name="findValue">The search criteria as an object. </param>
		/// <param name="validOnly">true to allow only valid certificates to be returned from the search; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="findType" /> is invalid. </exception>
		// Token: 0x06000A84 RID: 2692 RVA: 0x000362C8 File Offset: 0x000344C8
		[MonoTODO("Does not support X509FindType.FindByTemplateName, FindByApplicationPolicy and FindByCertificatePolicy")]
		public X509Certificate2Collection Find(X509FindType findType, object findValue, bool validOnly)
		{
			if (findValue == null)
			{
				throw new ArgumentNullException("findValue");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.None;
			DateTime dateTime = DateTime.MinValue;
			switch (findType)
			{
			case X509FindType.FindByThumbprint:
			case X509FindType.FindBySubjectName:
			case X509FindType.FindBySubjectDistinguishedName:
			case X509FindType.FindByIssuerName:
			case X509FindType.FindByIssuerDistinguishedName:
			case X509FindType.FindBySerialNumber:
			case X509FindType.FindByTemplateName:
			case X509FindType.FindBySubjectKeyIdentifier:
				try
				{
					text = (string)findValue;
					goto IL_0190;
				}
				catch (Exception ex)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
					{
						findValue.GetType(),
						"string"
					}), ex);
				}
				break;
			case X509FindType.FindByTimeValid:
			case X509FindType.FindByTimeNotYetValid:
			case X509FindType.FindByTimeExpired:
				goto IL_013C;
			case X509FindType.FindByApplicationPolicy:
			case X509FindType.FindByCertificatePolicy:
			case X509FindType.FindByExtension:
				break;
			case X509FindType.FindByKeyUsage:
				goto IL_0107;
			default:
				goto IL_0171;
			}
			try
			{
				text2 = (string)findValue;
			}
			catch (Exception ex2)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), ex2);
			}
			try
			{
				CryptoConfig.EncodeOID(text2);
				goto IL_0190;
			}
			catch (CryptographicUnexpectedOperationException)
			{
				string text3 = global::Locale.GetText("Invalid OID value '{0}'.", new object[] { text2 });
				throw new ArgumentException("findValue", text3);
			}
			IL_0107:
			try
			{
				x509KeyUsageFlags = (X509KeyUsageFlags)findValue;
				goto IL_0190;
			}
			catch (Exception ex3)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), ex3);
			}
			IL_013C:
			try
			{
				dateTime = (DateTime)findValue;
				goto IL_0190;
			}
			catch (Exception ex4)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509DateTime"
				}), ex4);
			}
			IL_0171:
			throw new CryptographicException(global::Locale.GetText("Invalid find type '{0}'.", new object[] { findType }));
			IL_0190:
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (object obj in base.InnerList)
			{
				X509Certificate2 x509Certificate = (X509Certificate2)obj;
				bool flag = false;
				switch (findType)
				{
				case X509FindType.FindByThumbprint:
					flag = string.Compare(text, x509Certificate.Thumbprint, true, invariantCulture) == 0 || string.Compare(text, x509Certificate.GetCertHashString(), true, invariantCulture) == 0;
					break;
				case X509FindType.FindBySubjectName:
					foreach (string text4 in x509Certificate.SubjectName.Format(true).Split(X509Certificate2Collection.newline_split, StringSplitOptions.RemoveEmptyEntries))
					{
						int num = text4.IndexOf('=');
						flag = text4.IndexOf(text, num, StringComparison.InvariantCultureIgnoreCase) >= 0;
						if (flag)
						{
							break;
						}
					}
					break;
				case X509FindType.FindBySubjectDistinguishedName:
					flag = string.Compare(text, x509Certificate.Subject, true, invariantCulture) == 0;
					break;
				case X509FindType.FindByIssuerName:
					flag = x509Certificate.GetNameInfo(X509NameType.SimpleName, true).IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0;
					break;
				case X509FindType.FindByIssuerDistinguishedName:
					flag = string.Compare(text, x509Certificate.Issuer, true, invariantCulture) == 0;
					break;
				case X509FindType.FindBySerialNumber:
					flag = string.Compare(text, x509Certificate.SerialNumber, true, invariantCulture) == 0;
					break;
				case X509FindType.FindByTimeValid:
					flag = dateTime >= x509Certificate.NotBefore && dateTime <= x509Certificate.NotAfter;
					break;
				case X509FindType.FindByTimeNotYetValid:
					flag = dateTime < x509Certificate.NotBefore;
					break;
				case X509FindType.FindByTimeExpired:
					flag = dateTime > x509Certificate.NotAfter;
					break;
				case X509FindType.FindByApplicationPolicy:
					flag = x509Certificate.Extensions.Count == 0;
					break;
				case X509FindType.FindByExtension:
					flag = x509Certificate.Extensions[text2] != null;
					break;
				case X509FindType.FindByKeyUsage:
				{
					X509KeyUsageExtension x509KeyUsageExtension = x509Certificate.Extensions["2.5.29.15"] as X509KeyUsageExtension;
					flag = x509KeyUsageExtension == null || (x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) == x509KeyUsageFlags;
					break;
				}
				case X509FindType.FindBySubjectKeyIdentifier:
					flag = string.Compare(text, this.GetKeyIdentifier(x509Certificate), true, invariantCulture) == 0;
					break;
				}
				if (flag)
				{
					if (validOnly)
					{
						try
						{
							if (x509Certificate.Verify())
							{
								x509Certificate2Collection.Add(x509Certificate);
							}
							continue;
						}
						catch
						{
							continue;
						}
					}
					x509Certificate2Collection.Add(x509Certificate);
				}
			}
			return x509Certificate2Collection;
		}

		/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator" /> object that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x06000A85 RID: 2693 RVA: 0x00036784 File Offset: 0x00034984
		public new X509Certificate2Enumerator GetEnumerator()
		{
			return new X509Certificate2Enumerator(this);
		}

		// Token: 0x04000826 RID: 2086
		private static string[] newline_split = new string[] { Environment.NewLine };
	}
}

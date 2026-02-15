using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides methods that help you use X.509 v.3 certificates.</summary>
	// Token: 0x020003CC RID: 972
	[Serializable]
	public class X509Certificate : IDisposable, IDeserializationCallback, ISerializable
	{
		/// <summary>Resets the state of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x060020FF RID: 8447 RVA: 0x00089BC8 File Offset: 0x00087DC8
		public virtual void Reset()
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
			this.lazyCertHash = null;
			this.lazyIssuer = null;
			this.lazySubject = null;
			this.lazySerialNumber = null;
			this.lazyKeyAlgorithm = null;
			this.lazyKeyAlgorithmParameters = null;
			this.lazyPublicKey = null;
			this.lazyNotBefore = DateTime.MinValue;
			this.lazyNotAfter = DateTime.MinValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class. </summary>
		// Token: 0x06002100 RID: 8448 RVA: 0x00089C44 File Offset: 0x00087E44
		public X509Certificate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class defined from a sequence of bytes representing an X.509v3 certificate.</summary>
		/// <param name="data">A byte array containing data from an X.509 certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x06002101 RID: 8449 RVA: 0x00089C62 File Offset: 0x00087E62
		public X509Certificate(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				this.impl = X509Helper.Import(data);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x06002102 RID: 8450 RVA: 0x00089C93 File Offset: 0x00087E93
		public X509Certificate(byte[] rawData, string password)
			: this(rawData, password, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x06002103 RID: 8451 RVA: 0x00089CA0 File Offset: 0x00087EA0
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("Array cannot be empty or null.", "rawData");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(rawData, safePasswordHandle, keyStorageFlags);
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00089D18 File Offset: 0x00087F18
		internal X509Certificate(X509CertificateImpl impl)
		{
			this.impl = X509Helper.InitFromCertificate(impl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x06002105 RID: 8453 RVA: 0x00089D42 File Offset: 0x00087F42
		public X509Certificate(string fileName)
			: this(fileName, null, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file, a password to access the certificate, and a key storage flag. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x06002106 RID: 8454 RVA: 0x00089D50 File Offset: 0x00087F50
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			byte[] array = File.ReadAllBytes(fileName);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(array, safePasswordHandle, keyStorageFlags);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class.</summary>
		/// <param name="cert">A <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class from which to initialize this class. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cert" /> parameter is null.</exception>
		// Token: 0x06002107 RID: 8455 RVA: 0x00089DC8 File Offset: 0x00087FC8
		public X509Certificate(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			this.impl = X509Helper.InitFromCertificate(cert);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and a <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes serialization information.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that describes how serialization should be performed.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06002108 RID: 8456 RVA: 0x00089E00 File Offset: 0x00088000
		public X509Certificate(SerializationInfo info, StreamingContext context)
			: this()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates an X.509v3 certificate from the specified signed file.</summary>
		/// <returns>The newly created X.509 certificate.</returns>
		/// <param name="filename">The path of the signed file from which to create the X.509 certificate. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06002109 RID: 8457 RVA: 0x00089E0D File Offset: 0x0008800D
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			return new X509Certificate(filename);
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x0600210A RID: 8458 RVA: 0x000145B3 File Offset: 0x000127B3
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.  </summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x0600210B RID: 8459 RVA: 0x000145B3 File Offset: 0x000127B3
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Gets the name of the certificate authority that issued the X.509v3 certificate.</summary>
		/// <returns>The name of the certificate authority that issued the X.509v3 certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00089E18 File Offset: 0x00088018
		public string Issuer
		{
			get
			{
				this.ThrowIfInvalid();
				string text = this.lazyIssuer;
				if (text == null)
				{
					text = (this.lazyIssuer = this.Impl.Issuer);
				}
				return text;
			}
		}

		/// <summary>Gets the subject distinguished name from the certificate.</summary>
		/// <returns>The subject distinguished name from the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00089E50 File Offset: 0x00088050
		public string Subject
		{
			get
			{
				this.ThrowIfInvalid();
				string text = this.lazySubject;
				if (text == null)
				{
					text = (this.lazySubject = this.Impl.Subject);
				}
				return text;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00089E87 File Offset: 0x00088087
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00089E90 File Offset: 0x00088090
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object. </param>
		// Token: 0x06002110 RID: 8464 RVA: 0x00089E9C File Offset: 0x0008809C
		public override bool Equals(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.Equals(x509Certificate);
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object.</param>
		// Token: 0x06002111 RID: 8465 RVA: 0x00089EBC File Offset: 0x000880BC
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Impl == null)
			{
				return other.Impl == null;
			}
			if (!this.Issuer.Equals(other.Issuer))
			{
				return false;
			}
			byte[] rawSerialNumber = this.GetRawSerialNumber();
			byte[] rawSerialNumber2 = other.GetRawSerialNumber();
			if (rawSerialNumber.Length != rawSerialNumber2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawSerialNumber.Length; i++)
			{
				if (rawSerialNumber[i] != rawSerialNumber2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array in a format described by one of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values, and using the specified password.</summary>
		/// <returns>An array of bytes that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
		/// </PermissionSet>
		// Token: 0x06002112 RID: 8466 RVA: 0x00089F28 File Offset: 0x00088128
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			this.VerifyContentType(contentType);
			if (this.Impl == null)
			{
				throw new CryptographicException(-2147467261);
			}
			byte[] array;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				array = this.Impl.Export(contentType, safePasswordHandle);
			}
			return array;
		}

		/// <summary>Returns the hash value for the X.509v3 certificate as an array of bytes.</summary>
		/// <returns>The hash value for the X.509 certificate.</returns>
		// Token: 0x06002113 RID: 8467 RVA: 0x00089F84 File Offset: 0x00088184
		public virtual byte[] GetCertHash()
		{
			this.ThrowIfInvalid();
			return this.GetRawCertHash().CloneByteArray();
		}

		/// <summary>Returns the SHA1 hash value for the X.509v3 certificate as a hexadecimal string.</summary>
		/// <returns>The hexadecimal string representation of the X.509 certificate hash value.</returns>
		// Token: 0x06002114 RID: 8468 RVA: 0x00089F97 File Offset: 0x00088197
		public virtual string GetCertHashString()
		{
			this.ThrowIfInvalid();
			return this.GetRawCertHash().ToHexStringUpper();
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00089FAC File Offset: 0x000881AC
		private byte[] GetRawCertHash()
		{
			byte[] array;
			if ((array = this.lazyCertHash) == null)
			{
				array = (this.lazyCertHash = this.Impl.Thumbprint);
			}
			return array;
		}

		/// <summary>Returns the raw data for the entire X.509v3 certificate.</summary>
		/// <returns>A byte array containing the X.509 certificate data.</returns>
		// Token: 0x06002116 RID: 8470 RVA: 0x00089FDB File Offset: 0x000881DB
		public virtual byte[] GetRawCertData()
		{
			this.ThrowIfInvalid();
			return this.Impl.RawData.CloneByteArray();
		}

		/// <summary>Returns the hash code for the X.509v3 certificate as an integer.</summary>
		/// <returns>The hash code for the X.509 certificate as an integer.</returns>
		// Token: 0x06002117 RID: 8471 RVA: 0x00089FF4 File Offset: 0x000881F4
		public override int GetHashCode()
		{
			if (this.Impl == null)
			{
				return 0;
			}
			byte[] rawCertHash = this.GetRawCertHash();
			int num = 0;
			int num2 = 0;
			while (num2 < rawCertHash.Length && num2 < 4)
			{
				num = (num << 8) | (int)rawCertHash[num2];
				num2++;
			}
			return num;
		}

		/// <summary>Returns the key algorithm information for this X.509v3 certificate.</summary>
		/// <returns>The key algorithm information for this X.509 certificate as a string.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x06002118 RID: 8472 RVA: 0x0008A030 File Offset: 0x00088230
		public virtual string GetKeyAlgorithm()
		{
			this.ThrowIfInvalid();
			string text = this.lazyKeyAlgorithm;
			if (text == null)
			{
				text = (this.lazyKeyAlgorithm = this.Impl.KeyAlgorithm);
			}
			return text;
		}

		/// <summary>Returns the key algorithm parameters for the X.509v3 certificate.</summary>
		/// <returns>The key algorithm parameters for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x06002119 RID: 8473 RVA: 0x0008A068 File Offset: 0x00088268
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			this.ThrowIfInvalid();
			byte[] array = this.lazyKeyAlgorithmParameters;
			if (array == null)
			{
				array = (this.lazyKeyAlgorithmParameters = this.Impl.KeyAlgorithmParameters);
			}
			return array.CloneByteArray();
		}

		/// <summary>Returns the public key for the X.509v3 certificate.</summary>
		/// <returns>The public key for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x0600211A RID: 8474 RVA: 0x0008A0A4 File Offset: 0x000882A4
		public virtual byte[] GetPublicKey()
		{
			this.ThrowIfInvalid();
			byte[] array = this.lazyPublicKey;
			if (array == null)
			{
				array = (this.lazyPublicKey = this.Impl.PublicKeyValue);
			}
			return array.CloneByteArray();
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x0600211B RID: 8475 RVA: 0x0008A0E0 File Offset: 0x000882E0
		public virtual byte[] GetSerialNumber()
		{
			this.ThrowIfInvalid();
			byte[] array = this.GetRawSerialNumber().CloneByteArray();
			Array.Reverse<byte>(array);
			return array;
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as a hexadecimal string.</returns>
		// Token: 0x0600211C RID: 8476 RVA: 0x0008A0F9 File Offset: 0x000882F9
		public virtual string GetSerialNumberString()
		{
			this.ThrowIfInvalid();
			return this.GetRawSerialNumber().ToHexStringUpper();
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0008A10C File Offset: 0x0008830C
		private byte[] GetRawSerialNumber()
		{
			byte[] array;
			if ((array = this.lazySerialNumber) == null)
			{
				array = (this.lazySerialNumber = this.Impl.SerialNumber);
			}
			return array;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		// Token: 0x0600211E RID: 8478 RVA: 0x0008A13B File Offset: 0x0008833B
		public override string ToString()
		{
			return this.ToString(false);
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object, with extra information, if specified.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="fVerbose">true to produce the verbose form of the string representation; otherwise, false. </param>
		// Token: 0x0600211F RID: 8479 RVA: 0x0008A144 File Offset: 0x00088344
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || !X509Helper.IsValid(this.impl))
			{
				return base.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Subject]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Subject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Issuer]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Issuer);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Serial Number]");
			stringBuilder.Append("  ");
			byte[] serialNumber = this.GetSerialNumber();
			Array.Reverse<byte>(serialNumber);
			stringBuilder.Append(serialNumber.ToHexArrayUpper());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not Before]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.GetNotBefore()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not After]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.GetNotAfter()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Thumbprint]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.GetRawCertHash().ToHexArrayUpper());
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0008A2A0 File Offset: 0x000884A0
		internal DateTime GetNotAfter()
		{
			this.ThrowIfInvalid();
			DateTime dateTime = this.lazyNotAfter;
			if (dateTime == DateTime.MinValue)
			{
				dateTime = (this.lazyNotAfter = this.impl.NotAfter);
			}
			return dateTime;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0008A2E0 File Offset: 0x000884E0
		internal DateTime GetNotBefore()
		{
			this.ThrowIfInvalid();
			DateTime dateTime = this.lazyNotBefore;
			if (dateTime == DateTime.MinValue)
			{
				dateTime = (this.lazyNotBefore = this.impl.NotBefore);
			}
			return dateTime;
		}

		/// <summary>Converts the specified date and time to a string.</summary>
		/// <returns>A string representation of the value of the <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="date">The date and time to convert.</param>
		// Token: 0x06002122 RID: 8482 RVA: 0x0008A320 File Offset: 0x00088520
		protected static string FormatDate(DateTime date)
		{
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;
			if (!cultureInfo.DateTimeFormat.Calendar.IsValidDay(date.Year, date.Month, date.Day, 0))
			{
				if (cultureInfo.DateTimeFormat.Calendar is UmAlQuraCalendar)
				{
					cultureInfo = cultureInfo.Clone() as CultureInfo;
					cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
				}
				else
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
			}
			return date.ToString(cultureInfo);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0008A39C File Offset: 0x0008859C
		internal static void ValidateKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
		{
			if ((keyStorageFlags & ~(X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != X509KeyStorageFlags.DefaultKeySet)
			{
				throw new ArgumentException("Value of flags is invalid.", "keyStorageFlags");
			}
			X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
			if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
			{
				throw new ArgumentException(SR.Format("The flags '{0}' may not be specified together.", x509KeyStorageFlags), "keyStorageFlags");
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0008A3E4 File Offset: 0x000885E4
		private void VerifyContentType(X509ContentType contentType)
		{
			if (contentType != X509ContentType.Cert && contentType != X509ContentType.SerializedCert && contentType != X509ContentType.Pfx)
			{
				throw new CryptographicException("Invalid content type.");
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0008A3FD File Offset: 0x000885FD
		internal void ImportHandle(X509CertificateImpl impl)
		{
			this.Reset();
			this.impl = impl;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0008A40C File Offset: 0x0008860C
		internal X509CertificateImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x0008A414 File Offset: 0x00088614
		internal bool IsValid
		{
			get
			{
				return X509Helper.IsValid(this.impl);
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0008A421 File Offset: 0x00088621
		internal void ThrowIfInvalid()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
		}

		// Token: 0x04000F89 RID: 3977
		private X509CertificateImpl impl;

		// Token: 0x04000F8A RID: 3978
		private volatile byte[] lazyCertHash;

		// Token: 0x04000F8B RID: 3979
		private volatile byte[] lazySerialNumber;

		// Token: 0x04000F8C RID: 3980
		private volatile string lazyIssuer;

		// Token: 0x04000F8D RID: 3981
		private volatile string lazySubject;

		// Token: 0x04000F8E RID: 3982
		private volatile string lazyKeyAlgorithm;

		// Token: 0x04000F8F RID: 3983
		private volatile byte[] lazyKeyAlgorithmParameters;

		// Token: 0x04000F90 RID: 3984
		private volatile byte[] lazyPublicKey;

		// Token: 0x04000F91 RID: 3985
		private DateTime lazyNotBefore = DateTime.MinValue;

		// Token: 0x04000F92 RID: 3986
		private DateTime lazyNotAfter = DateTime.MinValue;
	}
}

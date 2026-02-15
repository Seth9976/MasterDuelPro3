using System;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm inherit.</summary>
	// Token: 0x020003A9 RID: 937
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <returns>A new instance of the default implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002005 RID: 8197 RVA: 0x00081DBC File Offset: 0x0007FFBC
		public static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		/// <summary>Creates an instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <param name="algName">The name of the implementation of <see cref="T:System.Security.Cryptography.RSA" /> to use. </param>
		// Token: 0x06002006 RID: 8198 RVA: 0x00081DC8 File Offset: 0x0007FFC8
		public static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, decrypts the input data using the private key.</summary>
		/// <returns>The resulting decryption of the <paramref name="rgb" /> parameter in plain text.</returns>
		/// <param name="rgb">The cipher text to be decrypted. </param>
		// Token: 0x06002007 RID: 8199 RVA: 0x00081DD5 File Offset: 0x0007FFD5
		public virtual byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		/// <summary>When overridden in a derived class, encrypts the input data using the public key.</summary>
		/// <returns>The resulting encryption of the <paramref name="rgb" /> parameter as cipher text.</returns>
		/// <param name="rgb">The plain text to be encrypted. </param>
		// Token: 0x06002008 RID: 8200 RVA: 0x00081DD5 File Offset: 0x0007FFD5
		public virtual byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		/// <summary>Initializes an <see cref="T:System.Security.Cryptography.RSA" /> object from the key information from an XML string.</summary>
		/// <param name="xmlString">The XML string containing <see cref="T:System.Security.Cryptography.RSA" /> key information. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x06002009 RID: 8201 RVA: 0x00081DE8 File Offset: 0x0007FFE8
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("Modulus");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Modulus" }));
			}
			rsaparameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Exponent");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Exponent" }));
			}
			rsaparameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("P");
			if (text3 != null)
			{
				rsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			}
			string text4 = topElement.SearchForTextOfLocalName("Q");
			if (text4 != null)
			{
				rsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			}
			string text5 = topElement.SearchForTextOfLocalName("DP");
			if (text5 != null)
			{
				rsaparameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("DQ");
			if (text6 != null)
			{
				rsaparameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("InverseQ");
			if (text7 != null)
			{
				rsaparameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
			}
			string text8 = topElement.SearchForTextOfLocalName("D");
			if (text8 != null)
			{
				rsaparameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8));
			}
			this.ImportParameters(rsaparameters);
		}

		/// <summary>Creates and returns an XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</summary>
		/// <returns>An XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include a public and private RSA key; false to include only the public key. </param>
		// Token: 0x0600200A RID: 8202 RVA: 0x00081F84 File Offset: 0x00080184
		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaparameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaparameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(rsaparameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaparameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaparameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaparameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaparameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(rsaparameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x0600200B RID: 8203
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />. </param>
		// Token: 0x0600200C RID: 8204
		public abstract void ImportParameters(RSAParameters parameters);
	}
}

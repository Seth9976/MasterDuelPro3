using System;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) must inherit.</summary>
	// Token: 0x0200038D RID: 909
	[ComVisible(true)]
	public abstract class DSA : AsymmetricAlgorithm
	{
		/// <summary>Creates the default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F66 RID: 8038 RVA: 0x0007C753 File Offset: 0x0007A953
		public static DSA Create()
		{
			return DSA.Create("System.Security.Cryptography.DSA");
		}

		/// <summary>Creates the specified cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.DSA" /> to use. </param>
		// Token: 0x06001F67 RID: 8039 RVA: 0x0007C75F File Offset: 0x0007A95F
		public static DSA Create(string algName)
		{
			return (DSA)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x06001F68 RID: 8040
		public abstract byte[] CreateSignature(byte[] rgbHash);

		/// <summary>When overridden in a derived class, verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The hash of the data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />. </param>
		// Token: 0x06001F69 RID: 8041
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

		/// <summary>Reconstructs a <see cref="T:System.Security.Cryptography.DSA" /> object from an XML string.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.DSA" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x06001F6A RID: 8042 RVA: 0x0007C76C File Offset: 0x0007A96C
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DSAParameters dsaparameters = default(DSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("P");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "P" }));
			}
			dsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Q");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Q" }));
			}
			dsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("G");
			if (text3 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "G" }));
			}
			dsaparameters.G = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			string text4 = topElement.SearchForTextOfLocalName("Y");
			if (text4 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Y" }));
			}
			dsaparameters.Y = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			string text5 = topElement.SearchForTextOfLocalName("J");
			if (text5 != null)
			{
				dsaparameters.J = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("X");
			if (text6 != null)
			{
				dsaparameters.X = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("Seed");
			string text8 = topElement.SearchForTextOfLocalName("PgenCounter");
			if (text7 != null && text8 != null)
			{
				dsaparameters.Seed = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
				dsaparameters.Counter = Utils.ConvertByteArrayToInt(Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8)));
			}
			else if (text7 != null || text8 != null)
			{
				if (text7 == null)
				{
					throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "Seed" }));
				}
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "DSA", "PgenCounter" }));
			}
			this.ImportParameters(dsaparameters);
		}

		/// <summary>Creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06001F6B RID: 8043 RVA: 0x0007C9B4 File Offset: 0x0007ABB4
		public override string ToXmlString(bool includePrivateParameters)
		{
			DSAParameters dsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<DSAKeyValue>");
			stringBuilder.Append("<P>" + Convert.ToBase64String(dsaparameters.P) + "</P>");
			stringBuilder.Append("<Q>" + Convert.ToBase64String(dsaparameters.Q) + "</Q>");
			stringBuilder.Append("<G>" + Convert.ToBase64String(dsaparameters.G) + "</G>");
			stringBuilder.Append("<Y>" + Convert.ToBase64String(dsaparameters.Y) + "</Y>");
			if (dsaparameters.J != null)
			{
				stringBuilder.Append("<J>" + Convert.ToBase64String(dsaparameters.J) + "</J>");
			}
			if (dsaparameters.Seed != null)
			{
				stringBuilder.Append("<Seed>" + Convert.ToBase64String(dsaparameters.Seed) + "</Seed>");
				stringBuilder.Append("<PgenCounter>" + Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter)) + "</PgenCounter>");
			}
			if (includePrivateParameters)
			{
				stringBuilder.Append("<X>" + Convert.ToBase64String(dsaparameters.X) + "</X>");
			}
			stringBuilder.Append("</DSAKeyValue>");
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06001F6C RID: 8044
		public abstract DSAParameters ExportParameters(bool includePrivateParameters);

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		// Token: 0x06001F6D RID: 8045
		public abstract void ImportParameters(DSAParameters parameters);
	}
}

using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X509 extension.</summary>
	// Token: 0x020001CE RID: 462
	public class X509Extension : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		// Token: 0x06000B44 RID: 2884 RVA: 0x00038FD2 File Offset: 0x000371D2
		protected X509Extension()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">A string representing the object identifier.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		// Token: 0x06000B45 RID: 2885 RVA: 0x00038FDA File Offset: 0x000371DA
		public X509Extension(string oid, byte[] rawData, bool critical)
			: base(oid, rawData)
		{
			this._critical = critical;
		}

		/// <summary>Gets a Boolean value indicating whether the extension is critical.</summary>
		/// <returns>true if the extension is critical; otherwise, false.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00038FEB File Offset: 0x000371EB
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00038FF3 File Offset: 0x000371F3
		public bool Critical
		{
			get
			{
				return this._critical;
			}
			set
			{
				this._critical = value;
			}
		}

		/// <summary>Copies the extension properties of the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodedData" /> does not have a valid X.509 extension.</exception>
		// Token: 0x06000B48 RID: 2888 RVA: 0x00038FFC File Offset: 0x000371FC
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Expected a X509Extension instance."));
			}
			base.CopyFrom(asnEncodedData);
			this._critical = x509Extension.Critical;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00039044 File Offset: 0x00037244
		internal string FormatUnkownData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(data[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000854 RID: 2132
		private bool _critical;
	}
}

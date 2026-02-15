using System;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents a cryptographic object identifier. This class cannot be inherited.</summary>
	// Token: 0x020001A7 RID: 423
	public sealed class Oid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class.</summary>
		// Token: 0x06000A1B RID: 2587 RVA: 0x000026E5 File Offset: 0x000008E5
		public Oid()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using a string value of an <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An object identifier.</param>
		// Token: 0x06000A1C RID: 2588 RVA: 0x00034418 File Offset: 0x00032618
		public Oid(string oid)
		{
			string text = OidLookup.ToOid(oid, OidGroup.All, false);
			if (text == null)
			{
				text = oid;
			}
			this.Value = text;
			this._group = OidGroup.All;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified value and friendly name.</summary>
		/// <param name="value">The dotted number of the identifier.</param>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		// Token: 0x06000A1D RID: 2589 RVA: 0x00034447 File Offset: 0x00032647
		public Oid(string value, string friendlyName)
		{
			this._value = value;
			this._friendlyName = friendlyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The object identifier information to use to create the new object identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid " />is null.</exception>
		// Token: 0x06000A1E RID: 2590 RVA: 0x0003445D File Offset: 0x0003265D
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._value = oid._value;
			this._friendlyName = oid._friendlyName;
			this._group = oid._group;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object by using the specified OID value and group.</summary>
		/// <returns>A new instance of an <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oidValue">The OID value.</param>
		/// <param name="group">The group to search in.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oidValue" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The friendly name for the OID value was not found.</exception>
		// Token: 0x06000A1F RID: 2591 RVA: 0x00034498 File Offset: 0x00032698
		public static Oid FromOidValue(string oidValue, OidGroup group)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			string text = OidLookup.ToFriendlyName(oidValue, group, false);
			if (text == null)
			{
				throw new CryptographicException("The OID value is invalid.");
			}
			return new Oid(oidValue, text, group);
		}

		/// <summary>Gets or sets the dotted number of the identifier.</summary>
		/// <returns>The dotted number of the identifier.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000344D2 File Offset: 0x000326D2
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000344DA File Offset: 0x000326DA
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets or sets the friendly name of the identifier.</summary>
		/// <returns>The friendly name of the identifier.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000344E3 File Offset: 0x000326E3
		public string FriendlyName
		{
			get
			{
				if (this._friendlyName == null && this._value != null)
				{
					this._friendlyName = OidLookup.ToFriendlyName(this._value, this._group, true);
				}
				return this._friendlyName;
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00034513 File Offset: 0x00032713
		private Oid(string value, string friendlyName, OidGroup group)
		{
			this._value = value;
			this._friendlyName = friendlyName;
			this._group = group;
		}

		// Token: 0x04000782 RID: 1922
		private string _value;

		// Token: 0x04000783 RID: 1923
		private string _friendlyName;

		// Token: 0x04000784 RID: 1924
		private OidGroup _group;
	}
}

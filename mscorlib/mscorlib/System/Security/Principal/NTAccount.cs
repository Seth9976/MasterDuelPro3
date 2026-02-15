using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a user or group account.</summary>
	// Token: 0x020003D7 RID: 983
	[ComVisible(false)]
	public sealed class NTAccount : IdentityReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.NTAccount" /> class by using the specified name.</summary>
		/// <param name="name">The name used to create the <see cref="T:System.Security.Principal.NTAccount" /> object. This parameter cannot be null or an empty string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.-or-<paramref name="name" /> is too long.</exception>
		// Token: 0x0600215F RID: 8543 RVA: 0x0008A6C8 File Offset: 0x000888C8
		public NTAccount(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this._value = name;
		}

		/// <summary>Returns an uppercase string representation of this <see cref="T:System.Security.Principal.NTAccount" /> object.</summary>
		/// <returns>The uppercase string representation of this <see cref="T:System.Security.Principal.NTAccount" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0008A702 File Offset: 0x00088902
		public override string Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Security.Principal.NTAccount" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.NTAccount" /> object; otherwise, false.</returns>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.NTAccount" /> object, or null.</param>
		// Token: 0x06002161 RID: 8545 RVA: 0x0008A70C File Offset: 0x0008890C
		public override bool Equals(object o)
		{
			NTAccount ntaccount = o as NTAccount;
			return !(ntaccount == null) && ntaccount.Value == this.Value;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Security.Principal.NTAccount" /> object. The <see cref="M:System.Security.Principal.NTAccount.GetHashCode" /> method is suitable for hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash value for the current <see cref="T:System.Security.Principal.NTAccount" /> object.</returns>
		// Token: 0x06002162 RID: 8546 RVA: 0x0008A73C File Offset: 0x0008893C
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>Returns the account name, in Domain\Account format, for the account represented by the <see cref="T:System.Security.Principal.NTAccount" /> object.</summary>
		/// <returns>The account name, in Domain\Account format.</returns>
		// Token: 0x06002163 RID: 8547 RVA: 0x0008A749 File Offset: 0x00088949
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.NTAccount" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <returns>The converted identity.</returns>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.NTAccount" />. The target type must be a type that is considered valid by the <see cref="M:System.Security.Principal.NTAccount.IsValidTargetType(System.Type)" /> method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType " />is not an <see cref="T:System.Security.Principal.IdentityReference" />  type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">The source account name is too long.-or-A Win32 error code was returned.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002164 RID: 8548 RVA: 0x0008A754 File Offset: 0x00088954
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(NTAccount))
			{
				return this;
			}
			if (!(targetType == typeof(SecurityIdentifier)))
			{
				throw new ArgumentException("Unknown type", "targetType");
			}
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupByName(this.Value);
			if (wellKnownAccount == null || wellKnownAccount.Sid == null)
			{
				throw new IdentityNotMappedException("Cannot map account name: " + this.Value);
			}
			return new SecurityIdentifier(wellKnownAccount.Sid);
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.NTAccount" /> objects to determine whether they are equal. They are considered equal if they have the same canonical name representation as the one returned by the <see cref="P:System.Security.Principal.NTAccount.Value" /> property or if they are both null. </summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise false.</returns>
		/// <param name="left">The left operand to use for the equality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the equality comparison. This parameter can be null.</param>
		// Token: 0x06002165 RID: 8549 RVA: 0x0008A7CF File Offset: 0x000889CF
		public static bool operator ==(NTAccount left, NTAccount right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		// Token: 0x04000FA1 RID: 4001
		private string _value;
	}
}

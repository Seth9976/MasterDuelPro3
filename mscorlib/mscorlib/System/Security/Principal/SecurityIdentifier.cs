using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Principal
{
	/// <summary>Represents a security identifier (SID) and provides marshaling and comparison operations for SIDs.</summary>
	// Token: 0x020003D8 RID: 984
	[ComVisible(false)]
	public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using the specified security identifier (SID) in Security Descriptor Definition Language (SDDL) format.</summary>
		/// <param name="sddlForm">SDDL string for the SID used to create the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		// Token: 0x06002166 RID: 8550 RVA: 0x0008A7EF File Offset: 0x000889EF
		public SecurityIdentifier(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this.buffer = SecurityIdentifier.ParseSddlForm(sddlForm);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using a specified binary representation of a security identifier (SID).</summary>
		/// <param name="binaryForm">The byte array that represents the SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />. </param>
		// Token: 0x06002167 RID: 8551 RVA: 0x0008A814 File Offset: 0x00088A14
		public unsafe SecurityIdentifier(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 2)
			{
				throw new ArgumentException("offset");
			}
			fixed (byte[] array = binaryForm)
			{
				byte* ptr;
				if (binaryForm == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.CreateFromBinaryForm((IntPtr)((void*)(ptr + offset)), binaryForm.Length - offset);
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0008A87C File Offset: 0x00088A7C
		private void CreateFromBinaryForm(IntPtr binaryForm, int length)
		{
			int num = (int)Marshal.ReadByte(binaryForm, 0);
			int num2 = (int)Marshal.ReadByte(binaryForm, 1);
			if (num != 1 || num2 > 15)
			{
				throw new ArgumentException("Value was invalid.");
			}
			if (length < 8 + num2 * 4)
			{
				throw new ArgumentException("offset");
			}
			this.buffer = new byte[8 + num2 * 4];
			Marshal.Copy(binaryForm, this.buffer, 0, this.buffer.Length);
		}

		/// <summary>Returns the length, in bytes, of the security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The length, in bytes, of the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0008A8E3 File Offset: 0x00088AE3
		public int BinaryLength
		{
			get
			{
				return this.buffer.Length;
			}
		}

		/// <summary>Returns an uppercase Security Descriptor Definition Language (SDDL) string for the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>An uppercase SDDL string for the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0008A8F0 File Offset: 0x00088AF0
		public override string Value
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				ulong sidAuthority = this.GetSidAuthority();
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "S-1-{0}", sidAuthority);
				for (byte b = 0; b < this.GetSidSubAuthorityCount(); b += 1)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "-{0}", this.GetSidSubAuthority(b));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0008A958 File Offset: 0x00088B58
		private ulong GetSidAuthority()
		{
			return ((ulong)this.buffer[2] << 40) | ((ulong)this.buffer[3] << 32) | ((ulong)this.buffer[4] << 24) | ((ulong)this.buffer[5] << 16) | ((ulong)this.buffer[6] << 8) | (ulong)this.buffer[7];
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0008A9AE File Offset: 0x00088BAE
		private byte GetSidSubAuthorityCount()
		{
			return this.buffer[1];
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0008A9B8 File Offset: 0x00088BB8
		private uint GetSidSubAuthority(byte index)
		{
			int num = (int)(8 + index * 4);
			return (uint)((int)this.buffer[num] | ((int)this.buffer[num + 1] << 8) | ((int)this.buffer[num + 2] << 16) | ((int)this.buffer[num + 3] << 24));
		}

		/// <summary>Compares the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="sid" />.Return Value Description Less than zero This instance is less than <paramref name="sid" />. Zero This instance is equal to <paramref name="sid" />. Greater than zero This instance is greater than <paramref name="sid" />. </returns>
		/// <param name="sid">The object to compare with the current object.</param>
		// Token: 0x0600216E RID: 8558 RVA: 0x0008A9FC File Offset: 0x00088BFC
		public int CompareTo(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			int num;
			if ((num = this.GetSidAuthority().CompareTo(sid.GetSidAuthority())) != 0)
			{
				return num;
			}
			if ((num = this.GetSidSubAuthorityCount().CompareTo(sid.GetSidSubAuthorityCount())) != 0)
			{
				return num;
			}
			for (byte b = 0; b < this.GetSidSubAuthorityCount(); b += 1)
			{
				if ((num = this.GetSidSubAuthority(b).CompareTo(sid.GetSidSubAuthority(b))) != 0)
				{
					return num;
				}
			}
			return 0;
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object; otherwise, false.</returns>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object, or null.</param>
		// Token: 0x0600216F RID: 8559 RVA: 0x0008AA81 File Offset: 0x00088C81
		public override bool Equals(object o)
		{
			return this.Equals(o as SecurityIdentifier);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>true if the value of <paramref name="sid" /> is equal to the value of the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="sid">The object to compare with the current object.</param>
		// Token: 0x06002170 RID: 8560 RVA: 0x0008AA8F File Offset: 0x00088C8F
		public bool Equals(SecurityIdentifier sid)
		{
			return !(sid == null) && sid.Value == this.Value;
		}

		/// <summary>Copies the binary representation of the specified security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class to a byte array.</summary>
		/// <param name="binaryForm">The byte array to receive the copied SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />. </param>
		// Token: 0x06002171 RID: 8561 RVA: 0x0008AAB0 File Offset: 0x00088CB0
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - this.buffer.Length)
			{
				throw new ArgumentException("offset");
			}
			Array.Copy(this.buffer, 0, binaryForm, offset, this.buffer.Length);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. The <see cref="M:System.Security.Principal.SecurityIdentifier.GetHashCode" /> method is suitable for hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash value for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06002172 RID: 8562 RVA: 0x0008A73C File Offset: 0x0008893C
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>Returns the security identifier (SID), in Security Descriptor Definition Language (SDDL) format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. An example of the SDDL format is S-1-5-9. </summary>
		/// <returns>The SID, in SDDL format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06002173 RID: 8563 RVA: 0x0008A749 File Offset: 0x00088949
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <returns>The converted identity.</returns>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.SecurityIdentifier" />. The target type must be a type that is considered valid by the <see cref="M:System.Security.Principal.SecurityIdentifier.IsValidTargetType(System.Type)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType " />is not an <see cref="T:System.Security.Principal.IdentityReference" /> type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">A Win32 error code was returned.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002174 RID: 8564 RVA: 0x0008AB00 File Offset: 0x00088D00
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(SecurityIdentifier))
			{
				return this;
			}
			if (!(targetType == typeof(NTAccount)))
			{
				throw new ArgumentException("Unknown type.", "targetType");
			}
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupBySid(this.Value);
			if (wellKnownAccount == null || wellKnownAccount.Name == null)
			{
				throw new IdentityNotMappedException("Unable to map SID: " + this.Value);
			}
			return new NTAccount(wellKnownAccount.Name);
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are equal. They are considered equal if they have the same canonical representation as the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if they are both null. </summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The left operand to use for the equality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the equality comparison. This parameter can be null.</param>
		// Token: 0x06002175 RID: 8565 RVA: 0x0008A7CF File Offset: 0x000889CF
		public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are not equal. They are considered not equal if they have different canonical name representations than the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if one of the objects is null and the other is not.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The left operand to use for the inequality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the inequality comparison. This parameter can be null.</param>
		// Token: 0x06002176 RID: 8566 RVA: 0x0008AB7B File Offset: 0x00088D7B
		public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0008AB9C File Offset: 0x00088D9C
		private static byte[] ParseSddlForm(string sddlForm)
		{
			string text = sddlForm;
			if (sddlForm.Length == 2)
			{
				WellKnownAccount wellKnownAccount = WellKnownAccount.LookupBySddlForm(sddlForm);
				if (wellKnownAccount == null)
				{
					throw new ArgumentException("Invalid SDDL string - unrecognized account: " + sddlForm, "sddlForm");
				}
				if (!wellKnownAccount.IsAbsolute)
				{
					throw new NotImplementedException("Mono unable to convert account to SID: " + ((wellKnownAccount.Name != null) ? wellKnownAccount.Name : sddlForm));
				}
				text = wellKnownAccount.Sid;
			}
			string[] array = text.ToUpperInvariant().Split('-', StringSplitOptions.None);
			int num = array.Length - 3;
			if (array.Length < 3 || array[0] != "S" || num > 15)
			{
				throw new ArgumentException("Value was invalid.");
			}
			if (array[1] != "1")
			{
				throw new ArgumentException("Only SIDs with revision 1 are supported");
			}
			byte[] array2 = new byte[8 + num * 4];
			array2[0] = 1;
			array2[1] = (byte)num;
			ulong num2;
			if (!SecurityIdentifier.TryParseAuthority(array[2], out num2))
			{
				throw new ArgumentException("Value was invalid.");
			}
			array2[2] = (byte)((num2 >> 40) & 255UL);
			array2[3] = (byte)((num2 >> 32) & 255UL);
			array2[4] = (byte)((num2 >> 24) & 255UL);
			array2[5] = (byte)((num2 >> 16) & 255UL);
			array2[6] = (byte)((num2 >> 8) & 255UL);
			array2[7] = (byte)(num2 & 255UL);
			for (int i = 0; i < num; i++)
			{
				uint num3;
				if (!SecurityIdentifier.TryParseSubAuthority(array[i + 3], out num3))
				{
					throw new ArgumentException("Value was invalid.");
				}
				int num4 = 8 + i * 4;
				array2[num4] = (byte)num3;
				array2[num4 + 1] = (byte)(num3 >> 8);
				array2[num4 + 2] = (byte)(num3 >> 16);
				array2[num4 + 3] = (byte)(num3 >> 24);
			}
			return array2;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0008AD42 File Offset: 0x00088F42
		private static bool TryParseAuthority(string s, out ulong result)
		{
			if (s.StartsWith("0X"))
			{
				return ulong.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
			}
			return ulong.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0008AD76 File Offset: 0x00088F76
		private static bool TryParseSubAuthority(string s, out uint result)
		{
			if (s.StartsWith("0X"))
			{
				return uint.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
			}
			return uint.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x04000FA2 RID: 4002
		private byte[] buffer;

		/// <summary>Returns the maximum size, in bytes, of the binary representation of the security identifier.</summary>
		// Token: 0x04000FA3 RID: 4003
		public static readonly int MaxBinaryLength = 68;

		/// <summary>Returns the minimum size, in bytes, of the binary representation of the security identifier.</summary>
		// Token: 0x04000FA4 RID: 4004
		public static readonly int MinBinaryLength = 8;
	}
}

using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE), and is the base class for all other ACE classes.</summary>
	// Token: 0x020003F4 RID: 1012
	public abstract class GenericAce
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x0008DFAB File Offset: 0x0008C1AB
		internal GenericAce(AceType type, AceFlags flags)
		{
			if (type > AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.ace_type = type;
			this.ace_flags = flags;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0008DFD4 File Offset: 0x0008C1D4
		internal GenericAce(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 2)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			this.ace_type = (AceType)binaryForm[offset];
			this.ace_flags = (AceFlags)binaryForm[offset + 1];
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0008E02B File Offset: 0x0008C22B
		public AceFlags AceFlags
		{
			get
			{
				return this.ace_flags;
			}
		}

		/// <summary>Gets the type of this Access Control Entry (ACE).</summary>
		/// <returns>The type of this ACE.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0008E033 File Offset: 0x0008C233
		public AceType AceType
		{
			get
			{
				return this.ace_type;
			}
		}

		/// <summary>Gets the audit information associated with this Access Control Entry (ACE).</summary>
		/// <returns>The audit information associated with this Access Control Entry (ACE).</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x0008E03C File Offset: 0x0008C23C
		public AuditFlags AuditFlags
		{
			get
			{
				AuditFlags auditFlags = AuditFlags.None;
				if ((this.ace_flags & AceFlags.SuccessfulAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Success;
				}
				if ((this.ace_flags & AceFlags.FailedAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Failure;
				}
				return auditFlags;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600222A RID: 8746
		public abstract int BinaryLength { get; }

		/// <summary>Gets a Boolean value that specifies whether this Access Control Entry (ACE) is inherited or is set explicitly.</summary>
		/// <returns>true if this ACE is inherited; otherwise, false.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x0008E06D File Offset: 0x0008C26D
		public bool IsInherited
		{
			get
			{
				return (this.ace_flags & AceFlags.Inherited) > AceFlags.None;
			}
		}

		/// <summary>Creates a <see cref="T:System.Security.AccessControl.GenericAce" /> object from the specified binary data.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> object this method creates.</returns>
		/// <param name="binaryForm">The binary data from which to create the new <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		/// <param name="offset">The offset at which to begin unmarshaling.</param>
		// Token: 0x0600222C RID: 8748 RVA: 0x0008E07C File Offset: 0x0008C27C
		public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 1)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (GenericAce.IsObjectType((AceType)binaryForm[offset]))
			{
				return new ObjectAce(binaryForm, offset);
			}
			return new CommonAce(binaryForm, offset);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		// Token: 0x0600222D RID: 8749 RVA: 0x0008E0D2 File Offset: 0x0008C2D2
		public sealed override bool Equals(object o)
		{
			return this == o as GenericAce;
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x0600222E RID: 8750
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Serves as a hash function for the <see cref="T:System.Security.AccessControl.GenericAce" /> class. The  <see cref="M:System.Security.AccessControl.GenericAce.GetHashCode" /> method is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x0600222F RID: 8751 RVA: 0x0008E0E0 File Offset: 0x0008C2E0
		public sealed override int GetHashCode()
		{
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num = (num << 3) | ((num >> 29) & 7);
				num ^= (int)(array[i] & byte.MaxValue);
			}
			return num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered equal.</summary>
		/// <returns>true if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are equal; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		// Token: 0x06002230 RID: 8752 RVA: 0x0008E12C File Offset: 0x0008C32C
		public static bool operator ==(GenericAce left, GenericAce right)
		{
			if (left == null)
			{
				return right == null;
			}
			if (right == null)
			{
				return false;
			}
			int binaryLength = left.BinaryLength;
			int binaryLength2 = right.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return false;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			left.GetBinaryForm(array, 0);
			right.GetBinaryForm(array2, 0);
			for (int i = 0; i < binaryLength; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered unequal.</summary>
		/// <returns>true if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are unequal; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		// Token: 0x06002231 RID: 8753 RVA: 0x0008E198 File Offset: 0x0008C398
		public static bool operator !=(GenericAce left, GenericAce right)
		{
			if (left == null)
			{
				return right != null;
			}
			if (right == null)
			{
				return true;
			}
			int binaryLength = left.BinaryLength;
			int binaryLength2 = right.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return true;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			left.GetBinaryForm(array, 0);
			right.GetBinaryForm(array2, 0);
			for (int i = 0; i < binaryLength; i++)
			{
				if (array[i] != array2[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0008E201 File Offset: 0x0008C401
		private static bool IsObjectType(AceType type)
		{
			return type == AceType.AccessAllowedCallbackObject || type == AceType.AccessAllowedObject || type == AceType.AccessDeniedCallbackObject || type == AceType.AccessDeniedObject || type == AceType.SystemAlarmCallbackObject || type == AceType.SystemAlarmObject || type == AceType.SystemAuditCallbackObject || type == AceType.SystemAuditObject;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0008E229 File Offset: 0x0008C429
		internal static ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0008E237 File Offset: 0x0008C437
		internal static int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0008E256 File Offset: 0x0008C456
		internal static void WriteInt(int val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0008E27A File Offset: 0x0008C47A
		internal static void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x04001097 RID: 4247
		private AceFlags ace_flags;

		// Token: 0x04001098 RID: 4248
		private AceType ace_type;
	}
}

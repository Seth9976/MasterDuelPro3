using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to Directory Services objects. This class represents an Access Control Entry (ACE) associated with a directory object.</summary>
	// Token: 0x02000401 RID: 1025
	public sealed class ObjectAce : QualifiedAce
	{
		/// <summary>Initiates a new instance of the <see cref="T:System.Security.AccessControl.ObjectAce" /> class.</summary>
		/// <param name="aceFlags">The inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="qualifier">The use of the new ACE.</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		/// <param name="flags">Whether the <paramref name="type" /> and <paramref name="inheritedType" /> parameters contain valid object GUIDs.</param>
		/// <param name="type">A GUID that identifies the object type to which the new ACE applies.</param>
		/// <param name="inheritedType">A GUID that identifies the object type that can inherit the new ACE.</param>
		/// <param name="isCallback">true if the new ACE is a callback type ACE.</param>
		/// <param name="opaque">Opaque data associated with the new ACE. This is allowed only for callback ACE types. The length of this array must not be greater than the return value of the <see cref="M:System.Security.AccessControl.ObjectAceMaxOpaqueLength" /> method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The qualifier parameter contains an invalid value or the length of the value of the opaque parameter is greater than the return value of the <see cref="M:System.Security.AccessControl.ObjectAceMaxOpaqueLength" /> method.</exception>
		// Token: 0x0600226F RID: 8815 RVA: 0x0008E7E6 File Offset: 0x0008C9E6
		public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque)
			: base(ObjectAce.ConvertType(qualifier, isCallback), aceFlags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
			this.ObjectAceFlags = flags;
			this.ObjectAceType = type;
			this.InheritedObjectAceType = inheritedType;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0008E820 File Offset: 0x0008CA20
		internal ObjectAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
			int num = (int)GenericAce.ReadUShort(binaryForm, offset + 2);
			int num2 = 12 + SecurityIdentifier.MinBinaryLength;
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACE - truncated", "binaryForm");
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			base.AccessMask = GenericAce.ReadInt(binaryForm, offset + 4);
			this.ObjectAceFlags = (ObjectAceFlags)GenericAce.ReadInt(binaryForm, offset + 8);
			if (this.ObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			int num3 = 12;
			if (this.ObjectAceTypePresent)
			{
				this.ObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				this.InheritedObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			base.SecurityIdentifier = new SecurityIdentifier(binaryForm, offset + num3);
			num3 += base.SecurityIdentifier.BinaryLength;
			int num4 = num - num3;
			if (num4 > 0)
			{
				byte[] array = new byte[num4];
				Array.Copy(binaryForm, offset + num3, array, 0, num4);
				base.SetOpaque(array);
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.ObjectAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.ObjectAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0008E940 File Offset: 0x0008CB40
		public override int BinaryLength
		{
			get
			{
				int num = 12 + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
				if (this.ObjectAceTypePresent)
				{
					num += 16;
				}
				if (this.InheritedObjectAceTypePresent)
				{
					num += 16;
				}
				return num;
			}
		}

		/// <summary>Gets or sets the GUID of the object type that can inherit the Access Control Entry (ACE) that this <see cref="T:System.Security.AccessControl.ObjectAce" /> object represents.</summary>
		/// <returns>The GUID of the object type that can inherit the Access Control Entry (ACE) that this <see cref="T:System.Security.AccessControl.ObjectAce" /> object represents.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0008E97E File Offset: 0x0008CB7E
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x0008E986 File Offset: 0x0008CB86
		public Guid InheritedObjectAceType
		{
			get
			{
				return this.inherited_object_type;
			}
			set
			{
				this.inherited_object_type = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0008E98F File Offset: 0x0008CB8F
		private bool InheritedObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		/// <summary>Gets or sets flags that specify whether the <see cref="P:System.Security.AccessControl.ObjectAce.ObjectAceType" /> and <see cref="P:System.Security.AccessControl.ObjectAce.InheritedObjectAceType" /> properties contain values that identify valid object types.</summary>
		/// <returns>On or more members of the <see cref="T:System.Security.AccessControl.ObjectAceFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0008E99C File Offset: 0x0008CB9C
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x0008E9A4 File Offset: 0x0008CBA4
		public ObjectAceFlags ObjectAceFlags
		{
			get
			{
				return this.object_ace_flags;
			}
			set
			{
				this.object_ace_flags = value;
			}
		}

		/// <summary>Gets or sets the GUID of the object type associated with this <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</summary>
		/// <returns>The GUID of the object type associated with this <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0008E9AD File Offset: 0x0008CBAD
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x0008E9B5 File Offset: 0x0008CBB5
		public Guid ObjectAceType
		{
			get
			{
				return this.object_ace_type;
			}
			set
			{
				this.object_ace_type = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0008E9BE File Offset: 0x0008CBBE
		private bool ObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.ObjectAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.ObjectAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.ObjectAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x0600227A RID: 8826 RVA: 0x0008E9CC File Offset: 0x0008CBCC
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			ushort binaryLength = (ushort)this.BinaryLength;
			binaryForm[offset++] = (byte)base.AceType;
			binaryForm[offset++] = (byte)base.AceFlags;
			GenericAce.WriteUShort(binaryLength, binaryForm, offset);
			offset += 2;
			GenericAce.WriteInt(base.AccessMask, binaryForm, offset);
			offset += 4;
			GenericAce.WriteInt((int)this.ObjectAceFlags, binaryForm, offset);
			offset += 4;
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.ObjectAceType, binaryForm, offset);
				offset += 16;
			}
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.InheritedObjectAceType, binaryForm, offset);
				offset += 16;
			}
			base.SecurityIdentifier.GetBinaryForm(binaryForm, offset);
			offset += base.SecurityIdentifier.BinaryLength;
			byte[] opaque = base.GetOpaque();
			if (opaque != null)
			{
				Array.Copy(opaque, 0, binaryForm, offset, opaque.Length);
				offset += opaque.Length;
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0008EAA4 File Offset: 0x0008CCA4
		private static AceType ConvertType(AceQualifier qualifier, bool isCallback)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (isCallback)
				{
					return AceType.AccessAllowedCallbackObject;
				}
				return AceType.AccessAllowedObject;
			case AceQualifier.AccessDenied:
				if (isCallback)
				{
					return AceType.AccessDeniedCallbackObject;
				}
				return AceType.AccessDeniedObject;
			case AceQualifier.SystemAudit:
				if (isCallback)
				{
					return AceType.SystemAuditCallbackObject;
				}
				return AceType.SystemAuditObject;
			case AceQualifier.SystemAlarm:
				if (isCallback)
				{
					return AceType.SystemAlarmCallbackObject;
				}
				return AceType.SystemAlarmObject;
			default:
				throw new ArgumentException("Unrecognized ACE qualifier: " + qualifier.ToString(), "qualifier");
			}
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0008EB0A File Offset: 0x0008CD0A
		private void WriteGuid(Guid val, byte[] buffer, int offset)
		{
			Array.Copy(val.ToByteArray(), 0, buffer, offset, 16);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0008EB20 File Offset: 0x0008CD20
		private Guid ReadGuid(byte[] buffer, int offset)
		{
			byte[] array = new byte[16];
			Array.Copy(buffer, offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x040010B0 RID: 4272
		private Guid object_ace_type;

		// Token: 0x040010B1 RID: 4273
		private Guid inherited_object_type;

		// Token: 0x040010B2 RID: 4274
		private ObjectAceFlags object_ace_flags;
	}
}

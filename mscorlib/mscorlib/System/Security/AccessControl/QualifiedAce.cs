using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that contains a qualifier. The qualifier, represented by an <see cref="T:System.Security.AccessControl.AceQualifier" /> object, specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms. The <see cref="T:System.Security.AccessControl.QualifiedAce" /> class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonAce" /> and <see cref="T:System.Security.AccessControl.ObjectAce" /> classes.</summary>
	// Token: 0x02000405 RID: 1029
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x06002290 RID: 8848 RVA: 0x0008EE2F File Offset: 0x0008D02F
		internal QualifiedAce(AceType type, AceFlags flags, byte[] opaque)
			: base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0008EE40 File Offset: 0x0008D040
		internal QualifiedAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		/// <summary>Gets a value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</summary>
		/// <returns>A value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x0008EE4C File Offset: 0x0008D04C
		public AceQualifier AceQualifier
		{
			get
			{
				switch (base.AceType)
				{
				case AceType.AccessAllowed:
				case AceType.AccessAllowedCompound:
				case AceType.AccessAllowedObject:
				case AceType.AccessAllowedCallback:
				case AceType.AccessAllowedCallbackObject:
					return AceQualifier.AccessAllowed;
				case AceType.AccessDenied:
				case AceType.AccessDeniedObject:
				case AceType.AccessDeniedCallback:
				case AceType.AccessDeniedCallbackObject:
					return AceQualifier.AccessDenied;
				case AceType.SystemAudit:
				case AceType.SystemAuditObject:
				case AceType.SystemAuditCallback:
				case AceType.SystemAuditCallbackObject:
					return AceQualifier.SystemAudit;
				case AceType.SystemAlarm:
				case AceType.SystemAlarmObject:
				case AceType.SystemAlarmCallback:
				case AceType.SystemAlarmCallbackObject:
					return AceQualifier.SystemAlarm;
				default:
					throw new ArgumentException("Unrecognised ACE type: " + base.AceType.ToString());
				}
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data.</summary>
		/// <returns>true if this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data; otherwise, false.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		public bool IsCallback
		{
			get
			{
				return base.AceType == AceType.AccessAllowedCallback || base.AceType == AceType.AccessAllowedCallbackObject || base.AceType == AceType.AccessDeniedCallback || base.AceType == AceType.AccessDeniedCallbackObject || base.AceType == AceType.SystemAlarmCallback || base.AceType == AceType.SystemAlarmCallbackObject || base.AceType == AceType.SystemAuditCallback || base.AceType == AceType.SystemAuditCallbackObject;
			}
		}

		/// <summary>Gets the length of the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. This property is valid only for callback Access Control Entries (ACEs).</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0008EF37 File Offset: 0x0008D137
		public int OpaqueLength
		{
			get
			{
				if (this.opaque == null)
				{
					return 0;
				}
				return this.opaque.Length;
			}
		}

		/// <summary>Returns the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. </summary>
		/// <returns>An array of byte values that represents the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</returns>
		// Token: 0x06002295 RID: 8853 RVA: 0x0008EF4B File Offset: 0x0008D14B
		public byte[] GetOpaque()
		{
			if (this.opaque == null)
			{
				return null;
			}
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</param>
		// Token: 0x06002296 RID: 8854 RVA: 0x0008EF67 File Offset: 0x0008D167
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x040010BE RID: 4286
		private byte[] opaque;
	}
}

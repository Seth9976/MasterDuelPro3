using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents a System Access Control List (SACL).</summary>
	// Token: 0x0200040A RID: 1034
	public sealed class SystemAcl : CommonAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values from the specified <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Specify null to create an empty ACL.</param>
		// Token: 0x060022AD RID: 8877 RVA: 0x0008DEDC File Offset: 0x0008C0DC
		public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: base(isContainer, isDS, rawAcl)
		{
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0008F36C File Offset: 0x0008D56C
		internal override void ApplyCanonicalSortToExplicitAces()
		{
			int canonicalExplicitAceCount = base.GetCanonicalExplicitAceCount();
			base.ApplyCanonicalSortToExplicitAces(0, canonicalExplicitAceCount);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00033991 File Offset: 0x00031B91
		internal override int GetAceInsertPosition(AceQualifier aceQualifier)
		{
			return 0;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0008F388 File Offset: 0x0008D588
		internal override bool IsAceMeaningless(GenericAce ace)
		{
			if (base.IsAceMeaningless(ace))
			{
				return true;
			}
			if (!SystemAcl.IsValidAuditFlags(ace.AuditFlags))
			{
				return true;
			}
			QualifiedAce qualifiedAce = ace as QualifiedAce;
			return null != qualifiedAce && AceQualifier.SystemAudit != qualifiedAce.AceQualifier && AceQualifier.SystemAlarm != qualifiedAce.AceQualifier;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0008F3D4 File Offset: 0x0008D5D4
		private static bool IsValidAuditFlags(AuditFlags auditFlags)
		{
			return auditFlags != AuditFlags.None && auditFlags == ((AuditFlags.Success | AuditFlags.Failure) & auditFlags);
		}
	}
}

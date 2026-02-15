using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Encapsulates all Access Control Entry (ACE) types currently defined by Microsoft Corporation. All <see cref="T:System.Security.AccessControl.KnownAce" /> objects contain a 32-bit access mask and a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
	// Token: 0x020003F8 RID: 1016
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x0008E31C File Offset: 0x0008C51C
		internal KnownAce(AceType type, AceFlags flags)
			: base(type, flags)
		{
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0008E326 File Offset: 0x0008C526
		internal KnownAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		/// <summary>Gets or sets the access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0008E330 File Offset: 0x0008C530
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x0008E338 File Offset: 0x0008C538
		public int AccessMask
		{
			get
			{
				return this.access_mask;
			}
			set
			{
				this.access_mask = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0008E341 File Offset: 0x0008C541
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x0008E349 File Offset: 0x0008C549
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x040010A0 RID: 4256
		private int access_mask;

		// Token: 0x040010A1 RID: 4257
		private SecurityIdentifier identifier;
	}
}

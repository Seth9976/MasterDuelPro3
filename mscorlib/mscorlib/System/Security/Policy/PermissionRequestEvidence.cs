using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines evidence that represents permission requests. This class cannot be inherited.</summary>
	// Token: 0x02000346 RID: 838
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionRequestEvidence : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> class with the permission request of a code assembly.</summary>
		/// <param name="request">The minimum permissions the code requires to run. </param>
		/// <param name="optional">The permissions the code can use if they are granted, but that are not required. </param>
		/// <param name="denied">The permissions the code explicitly asks not to be granted. </param>
		// Token: 0x06001D8A RID: 7562 RVA: 0x000741C6 File Offset: 0x000723C6
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request != null)
			{
				this.requested = new PermissionSet(request);
			}
			if (optional != null)
			{
				this.optional = new PermissionSet(optional);
			}
			if (denied != null)
			{
				this.denied = new PermissionSet(denied);
			}
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001D8B RID: 7563 RVA: 0x000741FC File Offset: 0x000723FC
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.requested != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.requested.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.optional != null)
			{
				SecurityElement securityElement3 = new SecurityElement("Optional");
				securityElement3.AddChild(this.optional.ToXml());
				securityElement.AddChild(securityElement3);
			}
			if (this.denied != null)
			{
				SecurityElement securityElement4 = new SecurityElement("Denied");
				securityElement4.AddChild(this.denied.ToXml());
				securityElement.AddChild(securityElement4);
			}
			return securityElement.ToString();
		}

		// Token: 0x04000DA6 RID: 3494
		private PermissionSet requested;

		// Token: 0x04000DA7 RID: 3495
		private PermissionSet optional;

		// Token: 0x04000DA8 RID: 3496
		private PermissionSet denied;
	}
}

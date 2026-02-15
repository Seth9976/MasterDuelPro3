using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000189 RID: 393
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x06000961 RID: 2401 RVA: 0x0003181D File Offset: 0x0002FA1D
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06000962 RID: 2402 RVA: 0x0003182B File Offset: 0x0002FA2B
		public EventLogPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003183A File Offset: 0x0002FA3A
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine" };
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}
	}
}

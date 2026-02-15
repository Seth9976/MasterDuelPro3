using System;

namespace System.Drawing
{
	/// <summary>Specifies the fonts used to display text in Windows display elements.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005B RID: 91
	public sealed class SystemFonts
	{
		/// <summary>Gets the default font that applications can use for dialog boxes and forms.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the system. The value returned will vary depending on the user's operating system and the local culture setting of their system.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		public static Font DefaultFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 8.25f, "DefaultFont");
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Font" /> that is used for icon titles.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that is used for icon titles.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		public static Font IconTitleFont
		{
			get
			{
				return new Font("Microsoft Sans Serif", 11f, "IconTitleFont");
			}
		}
	}
}

using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemIcons" /> class is an <see cref="T:System.Drawing.Icon" /> object for Windows system-wide icons. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005C RID: 92
	public sealed class SystemIcons
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		static SystemIcons()
		{
			SystemIcons.icons[0] = new Icon("Mono.ico", true);
			SystemIcons.icons[1] = new Icon("Information.ico", true);
			SystemIcons.icons[2] = new Icon("Error.ico", true);
			SystemIcons.icons[3] = new Icon("Warning.ico", true);
			SystemIcons.icons[4] = new Icon("Question.ico", true);
			SystemIcons.icons[5] = new Icon("Shield.ico", true);
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system error icon (WIN32: IDI_ERROR).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system error icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000C65C File Offset: 0x0000A85C
		public static Icon Error
		{
			get
			{
				return SystemIcons.icons[2];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system information icon (WIN32: IDI_INFORMATION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system information icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C665 File Offset: 0x0000A865
		public static Icon Information
		{
			get
			{
				return SystemIcons.icons[1];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system question icon (WIN32: IDI_QUESTION).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system question icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000C66E File Offset: 0x0000A86E
		public static Icon Question
		{
			get
			{
				return SystemIcons.icons[4];
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon (WIN32: IDI_WARNING).</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> object that contains the system warning icon.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C677 File Offset: 0x0000A877
		public static Icon Warning
		{
			get
			{
				return SystemIcons.icons[3];
			}
		}

		// Token: 0x04000191 RID: 401
		private static Icon[] icons = new Icon[6];
	}
}

using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemPens" /> class is a <see cref="T:System.Drawing.Pen" /> that is the color of a Windows display element and that has a width of 1 pixel.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005D RID: 93
	public sealed class SystemPens
	{
		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000C680 File Offset: 0x0000A880
		public static Pen Control
		{
			get
			{
				if (SystemPens.control == null)
				{
					SystemPens.control = new Pen(SystemColors.Control);
					SystemPens.control.isModifiable = false;
				}
				return SystemPens.control;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public static Pen ControlDark
		{
			get
			{
				if (SystemPens.control_dark == null)
				{
					SystemPens.control_dark = new Pen(SystemColors.ControlDark);
					SystemPens.control_dark.isModifiable = false;
				}
				return SystemPens.control_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public static Pen ControlDarkDark
		{
			get
			{
				if (SystemPens.control_dark_dark == null)
				{
					SystemPens.control_dark_dark = new Pen(SystemColors.ControlDarkDark);
					SystemPens.control_dark_dark.isModifiable = false;
				}
				return SystemPens.control_dark_dark;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public static Pen ControlLight
		{
			get
			{
				if (SystemPens.control_light == null)
				{
					SystemPens.control_light = new Pen(SystemColors.ControlLight);
					SystemPens.control_light.isModifiable = false;
				}
				return SystemPens.control_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C720 File Offset: 0x0000A920
		public static Pen ControlLightLight
		{
			get
			{
				if (SystemPens.control_light_light == null)
				{
					SystemPens.control_light_light = new Pen(SystemColors.ControlLightLight);
					SystemPens.control_light_light.isModifiable = false;
				}
				return SystemPens.control_light_light;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000C748 File Offset: 0x0000A948
		public static Pen ControlText
		{
			get
			{
				if (SystemPens.control_text == null)
				{
					SystemPens.control_text = new Pen(SystemColors.ControlText);
					SystemPens.control_text.isModifiable = false;
				}
				return SystemPens.control_text;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> that is the color of a window frame.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C770 File Offset: 0x0000A970
		public static Pen WindowFrame
		{
			get
			{
				if (SystemPens.window_frame == null)
				{
					SystemPens.window_frame = new Pen(SystemColors.WindowFrame);
					SystemPens.window_frame.isModifiable = false;
				}
				return SystemPens.window_frame;
			}
		}

		// Token: 0x04000192 RID: 402
		private static Pen control;

		// Token: 0x04000193 RID: 403
		private static Pen control_dark;

		// Token: 0x04000194 RID: 404
		private static Pen control_dark_dark;

		// Token: 0x04000195 RID: 405
		private static Pen control_light;

		// Token: 0x04000196 RID: 406
		private static Pen control_light_light;

		// Token: 0x04000197 RID: 407
		private static Pen control_text;

		// Token: 0x04000198 RID: 408
		private static Pen window_frame;
	}
}

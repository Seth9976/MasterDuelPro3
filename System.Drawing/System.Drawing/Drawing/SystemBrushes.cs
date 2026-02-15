using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemBrushes" /> class is a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a Windows display element.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001B RID: 27
	public static class SystemBrushes
	{
		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004658 File Offset: 0x00002858
		public static Brush Control
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004664 File Offset: 0x00002864
		public static Brush ControlLightLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004670 File Offset: 0x00002870
		public static Brush ControlLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000467C File Offset: 0x0000287C
		public static Brush ControlDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004688 File Offset: 0x00002888
		public static Brush ControlDarkDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004694 File Offset: 0x00002894
		public static Brush ControlText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000046A0 File Offset: 0x000028A0
		public static Brush Highlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items. </summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000046AC File Offset: 0x000028AC
		public static Brush HighlightText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000046B8 File Offset: 0x000028B8
		public static Brush Menu
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000046C4 File Offset: 0x000028C4
		public static Brush Window
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Window);
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Brush" /> from the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The <see cref="T:System.Drawing.Brush" /> this method creates.</returns>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> structure from which to create the <see cref="T:System.Drawing.Brush" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000064 RID: 100 RVA: 0x000046D0 File Offset: 0x000028D0
		public static Brush FromSystemColor(Color c)
		{
			if (!c.IsSystemColor())
			{
				throw new ArgumentException(SR.Format("The color {0} is not a system color.", new object[] { c.ToString() }));
			}
			Brush[] array = (Brush[])SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey];
			if (array == null)
			{
				array = new Brush[33];
				SafeNativeMethods.Gdip.ThreadData[SystemBrushes.s_systemBrushesKey] = array;
			}
			int num = (int)c.ToKnownColor();
			if (num > 167)
			{
				num -= 141;
			}
			num--;
			if (array[num] == null)
			{
				array[num] = new SolidBrush(c, true);
			}
			return array[num];
		}

		// Token: 0x040000EE RID: 238
		private static readonly object s_systemBrushesKey = new object();
	}
}

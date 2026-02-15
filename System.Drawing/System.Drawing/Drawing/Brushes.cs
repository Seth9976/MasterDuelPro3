using System;

namespace System.Drawing
{
	/// <summary>Brushes for all the standard colors. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000026 RID: 38
	public sealed class Brushes
	{
		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000632B File Offset: 0x0000452B
		public static Brush Black
		{
			get
			{
				if (Brushes.black == null)
				{
					Brushes.black = new SolidBrush(Color.Black);
				}
				return Brushes.black;
			}
		}

		/// <summary>Gets a system-defined <see cref="T:System.Drawing.Brush" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006348 File Offset: 0x00004548
		public static Brush White
		{
			get
			{
				if (Brushes.white == null)
				{
					Brushes.white = new SolidBrush(Color.White);
				}
				return Brushes.white;
			}
		}

		// Token: 0x04000108 RID: 264
		private static SolidBrush black;

		// Token: 0x04000109 RID: 265
		private static SolidBrush white;
	}
}

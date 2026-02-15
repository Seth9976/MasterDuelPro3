using System;

namespace System.Drawing
{
	/// <summary>Pens for all the standard colors. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000057 RID: 87
	public sealed class Pens
	{
		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public static Pen Black
		{
			get
			{
				if (Pens.black == null)
				{
					Pens.black = new Pen(Color.Black);
					Pens.black.isModifiable = false;
				}
				return Pens.black;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public static Pen Gray
		{
			get
			{
				if (Pens.gray == null)
				{
					Pens.gray = new Pen(Color.Gray);
					Pens.gray.isModifiable = false;
				}
				return Pens.gray;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000BE14 File Offset: 0x0000A014
		public static Pen White
		{
			get
			{
				if (Pens.white == null)
				{
					Pens.white = new Pen(Color.White);
					Pens.white.isModifiable = false;
				}
				return Pens.white;
			}
		}

		// Token: 0x0400018C RID: 396
		private static Pen black;

		// Token: 0x0400018D RID: 397
		private static Pen gray;

		// Token: 0x0400018E RID: 398
		private static Pen white;
	}
}

using System;

namespace System.Drawing
{
	/// <summary>Translates colors to and from GDI+ <see cref="T:System.Drawing.Color" /> structures. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000027 RID: 39
	public sealed class ColorTranslator
	{
		/// <summary>Translates the specified <see cref="T:System.Drawing.Color" /> structure to a Windows color.</summary>
		/// <returns>The Windows color value.</returns>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> structure to translate. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001BB RID: 443 RVA: 0x00006365 File Offset: 0x00004565
		public static int ToWin32(Color c)
		{
			return ((int)c.B << 16) | ((int)c.G << 8) | (int)c.R;
		}
	}
}

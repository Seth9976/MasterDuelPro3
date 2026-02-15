using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides information about the current visual style of the operating system.</summary>
	// Token: 0x02000369 RID: 873
	public static class VisualStyleInformation
	{
		/// <summary>Gets the color scheme of the current visual style.</summary>
		/// <returns>A string that specifies the color scheme of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00086E73 File Offset: 0x00085073
		public static string ColorScheme
		{
			get
			{
				if (!VisualStyleRenderer.IsSupported)
				{
					return string.Empty;
				}
				return VisualStyleInformation.VisualStyles.VisualStyleInformationColorScheme;
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled visual styles in the operating system.</summary>
		/// <returns>true if the user has enabled visual styles in an operating system that supports them; otherwise, false.</returns>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00086E8C File Offset: 0x0008508C
		public static bool IsEnabledByUser
		{
			get
			{
				return VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.VisualStyles.UxThemeIsAppThemed() && VisualStyleInformation.VisualStyles.UxThemeIsThemeActive();
			}
		}

		/// <summary>Gets a value indicating whether the operating system supports visual styles.</summary>
		/// <returns>true if the operating system supports visual styles; otherwise, false.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00086EAF File Offset: 0x000850AF
		public static bool IsSupportedByOS
		{
			get
			{
				return VisualStyleInformation.VisualStyles.VisualStyleInformationIsSupportedByOS;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00086EBB File Offset: 0x000850BB
		private static IVisualStyles VisualStyles
		{
			get
			{
				return VisualStylesEngine.Instance;
			}
		}
	}
}

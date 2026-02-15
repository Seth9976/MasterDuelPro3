using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent categories of user preferences.</summary>
	// Token: 0x020000DD RID: 221
	public enum UserPreferenceCategory
	{
		/// <summary>Indicates user preferences associated with accessibility features of the system for users with disabilities.</summary>
		// Token: 0x04000366 RID: 870
		Accessibility = 1,
		/// <summary>Indicates user preferences associated with system colors. This category includes such as the default color of windows or menus.</summary>
		// Token: 0x04000367 RID: 871
		Color,
		/// <summary>Indicates user preferences associated with the system desktop. This category includes the background image or background image layout of the desktop.</summary>
		// Token: 0x04000368 RID: 872
		Desktop,
		/// <summary>Indicates user preferences that are not associated with any other category.</summary>
		// Token: 0x04000369 RID: 873
		General,
		/// <summary>Indicates user preferences for icon settings, including icon height and spacing.</summary>
		// Token: 0x0400036A RID: 874
		Icon,
		/// <summary>Indicates user preferences for keyboard settings, such as the key down repeat rate and delay.</summary>
		// Token: 0x0400036B RID: 875
		Keyboard,
		/// <summary>Indicates user preferences for menu settings, such as menu delays and text alignment.</summary>
		// Token: 0x0400036C RID: 876
		Menu,
		/// <summary>Indicates user preferences for mouse settings, such as double-click time and mouse sensitivity.</summary>
		// Token: 0x0400036D RID: 877
		Mouse,
		/// <summary>Indicates user preferences for policy settings, such as user rights and access levels.</summary>
		// Token: 0x0400036E RID: 878
		Policy,
		/// <summary>Indicates the user preferences for system power settings. This category includes power feature settings, such as the idle time before the system automatically enters low power mode.</summary>
		// Token: 0x0400036F RID: 879
		Power,
		/// <summary>Indicates user preferences associated with the screensaver.</summary>
		// Token: 0x04000370 RID: 880
		Screensaver,
		/// <summary>Indicates user preferences associated with the dimensions and characteristics of windows on the system.</summary>
		// Token: 0x04000371 RID: 881
		Window,
		/// <summary>Indicates changes in user preferences for regional settings, such as the character encoding and culture strings.</summary>
		// Token: 0x04000372 RID: 882
		Locale,
		/// <summary>Indicates user preferences associated with visual styles, such as enabling or disabling visual styles and switching from one visual style to another.</summary>
		// Token: 0x04000373 RID: 883
		VisualStyle
	}
}

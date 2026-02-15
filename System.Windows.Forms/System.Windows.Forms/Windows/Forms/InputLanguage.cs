using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides methods and fields to manage the input language. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DC RID: 220
	public sealed class InputLanguage
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00002A07 File Offset: 0x00000C07
		[MonoInternalNote("Pull Microsofts InputLanguages and enter them here")]
		internal InputLanguage()
		{
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00022F14 File Offset: 0x00021114
		internal InputLanguage(IntPtr handle, CultureInfo culture, string layout_name)
			: this()
		{
			this.handle = handle;
			this.culture = culture;
			this.layout_name = layout_name;
		}

		/// <summary>Gets or sets the input language for the current thread.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the input language for the current thread.</returns>
		/// <exception cref="T:System.ArgumentException">The input language is not recognized by the system.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00022F31 File Offset: 0x00021131
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				if (InputLanguage.current_input == null)
				{
					InputLanguage.current_input = InputLanguage.FromCulture(CultureInfo.CurrentUICulture);
				}
				return InputLanguage.current_input;
			}
		}

		/// <summary>Gets a list of all installed input languages.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.InputLanguage" /> objects that represent the input languages installed on the computer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00022F4E File Offset: 0x0002114E
		public static InputLanguageCollection InstalledInputLanguages
		{
			get
			{
				if (InputLanguage.all == null)
				{
					InputLanguage.all = new InputLanguageCollection(new InputLanguage[]
					{
						new InputLanguage(IntPtr.Zero, new CultureInfo(string.Empty), "US")
					});
				}
				return InputLanguage.all;
			}
		}

		/// <summary>Returns the input language associated with the specified culture.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the previously selected input language.</returns>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000815 RID: 2069 RVA: 0x00022F88 File Offset: 0x00021188
		public static InputLanguage FromCulture(CultureInfo culture)
		{
			foreach (object obj in InputLanguage.InstalledInputLanguages)
			{
				InputLanguage inputLanguage = (InputLanguage)obj;
				if (culture.EnglishName == inputLanguage.culture.EnglishName)
				{
					return new InputLanguage(inputLanguage.handle, inputLanguage.culture, inputLanguage.layout_name);
				}
			}
			return new InputLanguage(InputLanguage.InstalledInputLanguages[0].handle, InputLanguage.InstalledInputLanguages[0].culture, InputLanguage.InstalledInputLanguages[0].layout_name);
		}

		/// <summary>Specifies whether two input languages are equal.</summary>
		/// <returns>true if the two languages are equal; otherwise, false.</returns>
		/// <param name="value">The language to test for equality. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000816 RID: 2070 RVA: 0x00023044 File Offset: 0x00021244
		public override bool Equals(object value)
		{
			return value is InputLanguage && ((InputLanguage)value).culture == this.culture && ((InputLanguage)value).handle == this.handle && ((InputLanguage)value).layout_name == this.layout_name;
		}

		/// <summary>Returns the hash code for this input language.</summary>
		/// <returns>The hash code for this input language.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000817 RID: 2071 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400052B RID: 1323
		private static InputLanguageCollection all;

		// Token: 0x0400052C RID: 1324
		private IntPtr handle;

		// Token: 0x0400052D RID: 1325
		private CultureInfo culture;

		// Token: 0x0400052E RID: 1326
		private string layout_name;

		// Token: 0x0400052F RID: 1327
		private static InputLanguage current_input;
	}
}

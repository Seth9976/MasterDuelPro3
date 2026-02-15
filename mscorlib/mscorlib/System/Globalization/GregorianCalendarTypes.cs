using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the different language versions of the Gregorian calendar.</summary>
	// Token: 0x020006B6 RID: 1718
	[ComVisible(true)]
	[Serializable]
	public enum GregorianCalendarTypes
	{
		/// <summary>Refers to the localized version of the Gregorian calendar, based on the language of the <see cref="T:System.Globalization.CultureInfo" /> that uses the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		// Token: 0x04001D00 RID: 7424
		Localized = 1,
		/// <summary>Refers to the U.S. English version of the Gregorian calendar.</summary>
		// Token: 0x04001D01 RID: 7425
		USEnglish,
		/// <summary>Refers to the Middle East French version of the Gregorian calendar.</summary>
		// Token: 0x04001D02 RID: 7426
		MiddleEastFrench = 9,
		/// <summary>Refers to the Arabic version of the Gregorian calendar.</summary>
		// Token: 0x04001D03 RID: 7427
		Arabic,
		/// <summary>Refers to the transliterated English version of the Gregorian calendar.</summary>
		// Token: 0x04001D04 RID: 7428
		TransliteratedEnglish,
		/// <summary>Refers to the transliterated French version of the Gregorian calendar.</summary>
		// Token: 0x04001D05 RID: 7429
		TransliteratedFrench
	}
}

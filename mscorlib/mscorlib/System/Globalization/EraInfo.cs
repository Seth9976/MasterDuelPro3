using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020006B4 RID: 1716
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x000CFA48 File Offset: 0x000CDC48
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000CFA94 File Offset: 0x000CDC94
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x04001CEF RID: 7407
		internal int era;

		// Token: 0x04001CF0 RID: 7408
		internal long ticks;

		// Token: 0x04001CF1 RID: 7409
		internal int yearOffset;

		// Token: 0x04001CF2 RID: 7410
		internal int minEraYear;

		// Token: 0x04001CF3 RID: 7411
		internal int maxEraYear;

		// Token: 0x04001CF4 RID: 7412
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x04001CF5 RID: 7413
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x04001CF6 RID: 7414
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}

using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005BF RID: 1471
	internal struct MatchResult
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000A48C0 File Offset: 0x000A2AC0
		public bool success
		{
			get
			{
				return this.errorCode == MatchResultErrorCode.None;
			}
		}

		// Token: 0x04001518 RID: 5400
		public MatchResultErrorCode errorCode;

		// Token: 0x04001519 RID: 5401
		public string errorValue;
	}
}

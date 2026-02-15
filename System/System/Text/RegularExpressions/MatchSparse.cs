using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200012C RID: 300
	internal class MatchSparse : Match
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001D0A2 File Offset: 0x0001B2A2
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos)
			: base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001D0BB File Offset: 0x0001B2BB
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x040004D8 RID: 1240
		internal new readonly Hashtable _caps;
	}
}

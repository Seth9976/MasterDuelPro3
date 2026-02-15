using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group. </summary>
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0001C856 File Offset: 0x0001AA56
		internal Group(string text, int[] caps, int capcount, string name)
			: base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this.<Name>k__BackingField = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>true if the match is successful; otherwise, false.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001C88F File Offset: 0x0001AA8F
		public bool Success
		{
			get
			{
				return this._capcount != 0;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		internal Group()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004C4 RID: 1220
		internal static readonly Group s_emptyGroup = new Group(string.Empty, Array.Empty<int>(), 0, string.Empty);

		// Token: 0x040004C5 RID: 1221
		internal readonly int[] _caps;

		// Token: 0x040004C6 RID: 1222
		internal int _capcount;

		// Token: 0x040004C7 RID: 1223
		internal CaptureCollection _capcoll;
	}
}

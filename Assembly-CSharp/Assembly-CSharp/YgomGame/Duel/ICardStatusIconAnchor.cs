using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EA0 RID: 3744
	public interface ICardStatusIconAnchor
	{
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06006D2C RID: 27948
		GameObject effectAnchor { get; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06006D2D RID: 27949
		Quaternion localRot { get; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06006D2E RID: 27950
		Vector3 centerOfs { get; }

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06006D2F RID: 27951
		Vector3 atkDefOfs { get; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06006D30 RID: 27952
		Vector3 levelOfs { get; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06006D31 RID: 27953
		Vector3 attrOfs { get; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06006D32 RID: 27954
		Vector3 typeOfs { get; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06006D33 RID: 27955
		Vector3 counterOfs { get; }

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06006D34 RID: 27956
		Vector3 turnsOfs { get; }
	}
}

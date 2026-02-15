using System;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000B4 RID: 180
	internal class TweenLink
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00011498 File Offset: 0x0000F698
		public TweenLink(GameObject target, LinkBehaviour behaviour)
		{
			this.target = target;
			this.behaviour = behaviour;
			this.lastSeenActive = target.activeInHierarchy;
		}

		// Token: 0x0400021D RID: 541
		public readonly GameObject target;

		// Token: 0x0400021E RID: 542
		public readonly LinkBehaviour behaviour;

		// Token: 0x0400021F RID: 543
		public bool lastSeenActive;
	}
}

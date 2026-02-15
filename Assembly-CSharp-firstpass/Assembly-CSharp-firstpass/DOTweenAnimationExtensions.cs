using System;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000062 RID: 98
	public static class DOTweenAnimationExtensions
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00006B07 File Offset: 0x00004D07
		public static bool IsSameOrSubclassOf<T>(this Component t)
		{
			return t is T;
		}
	}
}

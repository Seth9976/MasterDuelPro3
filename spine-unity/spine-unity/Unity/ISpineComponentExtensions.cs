using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000056 RID: 86
	public static class ISpineComponentExtensions
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000F54C File Offset: 0x0000D74C
		public static bool IsNullOrDestroyed(this ISpineComponent component)
		{
			return component == null || (global::UnityEngine.Object)component == null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.Effect
{
	// Token: 0x0200078B RID: 1931
	public class SpriteScalerManager : MonoBehaviour
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool reqUpdate
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Add(SpriteScaler screen)
		{
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Remove(SpriteScaler screen)
		{
		}

		// Token: 0x040034DA RID: 13530
		private Vector2 currentScreenSize;

		// Token: 0x040034DB RID: 13531
		private static List<SpriteScaler> scalerList;
	}
}

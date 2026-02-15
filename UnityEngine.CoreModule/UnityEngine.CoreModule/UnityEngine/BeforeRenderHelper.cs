using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x020000CE RID: 206
	internal static class BeforeRenderHelper
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag2 = callback != null;
					if (flag2)
					{
						callback();
					}
				}
			}
		}

		// Token: 0x04000276 RID: 630
		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		// Token: 0x020000CF RID: 207
		private struct OrderBlock
		{
			// Token: 0x04000277 RID: 631
			internal int order;

			// Token: 0x04000278 RID: 632
			internal UnityAction callback;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005B7 RID: 1463
	public class RenderTextureTargetManager : MonoBehaviour
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x0000216A File Offset: 0x0000036A
		public static RenderTextureTargetManager Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationQuit()
		{
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reboot()
		{
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateManager()
		{
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Create(Action<RenderTextureTarget> onFinish)
		{
			return 0;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DestroyTarget(int id)
		{
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x0000216A File Offset: 0x0000036A
		public static Camera GetTargetCamera(int id)
		{
			return null;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetUnusedId()
		{
			return 0;
		}

		// Token: 0x04002BD3 RID: 11219
		private int targetCount;

		// Token: 0x04002BD4 RID: 11220
		private GameObject lightObject;

		// Token: 0x04002BD5 RID: 11221
		private List<int> useIdxList;

		// Token: 0x04002BD6 RID: 11222
		private List<int> removeIdxList;

		// Token: 0x04002BD7 RID: 11223
		private List<RenderTextureTarget> renderTextureTargetList;

		// Token: 0x04002BD8 RID: 11224
		private static RenderTextureTargetManager instance;

		// Token: 0x04002BD9 RID: 11225
		private static bool isQuitting;
	}
}

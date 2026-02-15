using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YgomGame.Card
{
	// Token: 0x02001129 RID: 4393
	public static class ProtectorUtility
	{
		// Token: 0x060082DB RID: 33499 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x060082DC RID: 33500 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reset()
		{
		}

		// Token: 0x060082DD RID: 33501 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetProtectorAsync(this RawImage rawImage, int protectorId, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082DE RID: 33502 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetProtectorAsync(this Image image, int protectorId, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082DF RID: 33503 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetProtectorAsync(this MeshRenderer meshRenderer, int matId, int protectorId, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082E0 RID: 33504 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetProtectorAsync(int protectorId, UnityAction<Material, int> onFinish = null)
		{
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetProtectorAsyncUI(int protectorId, UnityAction<Material, int> onFinish = null)
		{
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetInsight(this RawImage rawImage, bool enable)
		{
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetInsight(this MeshRenderer meshRenderer, int matId, bool enable)
		{
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetProtectorForRawImageImpl(RawImage rawImage, Material mat, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetProtectorForImageImpl(Image image, Material mat, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetProtectorForMeshRendererImpl(MeshRenderer meshRenderer, int matId, Material mat, UnityAction onFinish = null)
		{
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetName(int sid)
		{
			return null;
		}

		// Token: 0x060082E8 RID: 33512 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetShortName(int sid)
		{
			return null;
		}

		// Token: 0x0400BDF4 RID: 48628
		public const int DEFAULTPROTECTORID = 1;

		// Token: 0x0400BDF5 RID: 48629
		private const string LABEL_SD_SHOWCARD = "SHOWCARD_ON";

		// Token: 0x0400BDF6 RID: 48630
		private const string LABEL_SD_CARDPICTURE = "_CardPicture";

		// Token: 0x0400BDF7 RID: 48631
		private const string LABEL_SD_ZWRITE = "_ZWrite";

		// Token: 0x0400BDF8 RID: 48632
		private static ProtectorManager protectorManager3D;

		// Token: 0x0400BDF9 RID: 48633
		private static ProtectorManager protectorManagerUI;
	}
}

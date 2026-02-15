using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YgomGame.Card
{
	// Token: 0x02001115 RID: 4373
	public static class CardTextureUtility
	{
		// Token: 0x06008221 RID: 33313 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardQuality(CardQuality quality)
		{
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x0000216D File Offset: 0x0000036D
		public static void IgnoreCardIllust(bool enable)
		{
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardIllustAsync(this RawImage rawImage, int cardid, UnityAction onFinish = null, bool isAutoRelease = false, bool immediateOnReuse = false)
		{
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardMaterialAsync(this RawImage rawImage, int cardid, int finishid = 1, UnityAction onFinish = null, bool isResetParent = true)
		{
		}

		// Token: 0x06008226 RID: 33318 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardPictureAsync(this GameObject gob, List<int> cardidList, List<UnityAction<Texture2D>> onFinishList = null, UnityAction onFinishAll = null)
		{
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardPictureAsync(this GameObject gob, int cardid, UnityAction<Texture2D> onFinish = null)
		{
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardMaterialAsync(this MeshRenderer meshRenderer, int matid, int cardid, int finishid = 1, UnityAction onFinish = null, bool isResetParent = true)
		{
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearAllCache()
		{
		}

		// Token: 0x0600822A RID: 33322 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetAll()
		{
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCacheActive(bool enable)
		{
		}

		// Token: 0x0600822C RID: 33324 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetCardIllustAspectRatio(this AspectRatioFitter aspectRatioFitter, int cardid)
		{
		}

		// Token: 0x0600822D RID: 33325 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PreLoadCardPictureAsync(int cardid, bool force = false)
		{
			return false;
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReleaseCardMaterial(Material mat)
		{
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardCreating()
		{
			return false;
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x0000216D File Offset: 0x0000036D
		private static void AddTweenShaderFloat(GameObject gob)
		{
		}

		// Token: 0x06008231 RID: 33329 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTextureInfo()
		{
			return null;
		}

		// Token: 0x0400BA80 RID: 47744
		public const int DUMMYCARDID = 0;

		// Token: 0x0400BA81 RID: 47745
		private const float FADETIME = 0.1f;

		// Token: 0x0400BA82 RID: 47746
		private static CardIllustManager cardIllustManager;

		// Token: 0x0400BA83 RID: 47747
		private static CardMaterialManager cardMaterialManager;

		// Token: 0x0400BA84 RID: 47748
		private static CardPictureManager cardPictureManager;

		// Token: 0x0400BA85 RID: 47749
		private static CardMaskManager cardMaskManager;

		// Token: 0x0400BA86 RID: 47750
		private static bool initialized;
	}
}

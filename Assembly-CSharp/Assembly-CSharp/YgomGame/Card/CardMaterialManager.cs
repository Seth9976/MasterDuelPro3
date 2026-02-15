using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YgomGame.Card
{
	// Token: 0x02001106 RID: 4358
	public class CardMaterialManager
	{
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06008186 RID: 33158 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<CardFinishSetting.FinishType, int> matcountStyleidTable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardMaterialManager Create(CardPictureManager cardPictureManager, CardMaskManager cardMaskManager)
		{
			return null;
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetCardMaterialForMeshRendererAsync(MeshRenderer meshRenderer, int cardId, int styleId, UnityAction onFinished)
		{
		}

		// Token: 0x06008189 RID: 33161 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetCardMaterialForRawImageAsync(RawImage rawImage, int cardId, int styleId, UnityAction onFinished)
		{
		}

		// Token: 0x0600818A RID: 33162 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Release(Material mat)
		{
			return 0;
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool SetMaskTex(int cardid, int styleId, Material mat)
		{
			return false;
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFrameMask(int cardid, ref Material mat)
		{
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardNameMask(int cardid, Material mat)
		{
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetParameterMask(int cardid, ref Material mat)
		{
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetStarColorMask(int cardid, int styleid, ref Material mat)
		{
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardNameColorMask(int cardid, int styleid, ref Material mat)
		{
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReturnCardMaterial(Material mat)
		{
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x0000216A File Offset: 0x0000036A
		private Material GetCardMaterial(int cardid, int finishid, bool isforui)
		{
			return null;
		}

		// Token: 0x0400BA06 RID: 47622
		private const int MAXMATPOOLCOUNT = 64;

		// Token: 0x0400BA07 RID: 47623
		private const int MAXRARENUM = 4;

		// Token: 0x0400BA08 RID: 47624
		private const string MASKTEX = "MaskTex";

		// Token: 0x0400BA09 RID: 47625
		private CardPictureManager m_CardPictureManager;

		// Token: 0x0400BA0A RID: 47626
		private CardMaskManager m_CardMaskManager;

		// Token: 0x0400BA0B RID: 47627
		private Dictionary<Material, int> m_MatInstanceTable;

		// Token: 0x0400BA0C RID: 47628
		private Dictionary<Material, int> m_CardPictureTaskTable;

		// Token: 0x0400BA0D RID: 47629
		private Dictionary<Material, int> m_CardMaskTaskTable;

		// Token: 0x0400BA0E RID: 47630
		private Dictionary<CardFinishSetting.FinishType, int> m_MatcountStyleidTable;

		// Token: 0x0400BA0F RID: 47631
		private Dictionary<CardFinishSetting.FinishType, Stack<Material>> m_CardMaterialStack;

		// Token: 0x0400BA10 RID: 47632
		private Dictionary<string, CardFinishSetting.FinishType> m_FinishTypeNameTable;
	}
}

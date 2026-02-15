using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Duel;

namespace YgomGame
{
	// Token: 0x020007B4 RID: 1972
	public class CharacterSoundDataManager
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x0000216A File Offset: 0x0000036A
		public static AvatarModelSetting ModelSetting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoaded(int id)
		{
			return false;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reset()
		{
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadAudioClip(int modelId)
		{
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator LoadAudioClipCoroutine(int id)
		{
			return null;
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadAudioClip()
		{
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator UnloadAudioClipCroutine()
		{
			return null;
		}

		// Token: 0x040035BC RID: 13756
		private static int characterSoundLoadCount;

		// Token: 0x040035BD RID: 13757
		private static AvatarModelSetting modelSetting;

		// Token: 0x040035BE RID: 13758
		private static Dictionary<int, int> loadIdDic;

		// Token: 0x040035BF RID: 13759
		private static List<int> loadedId;
	}
}

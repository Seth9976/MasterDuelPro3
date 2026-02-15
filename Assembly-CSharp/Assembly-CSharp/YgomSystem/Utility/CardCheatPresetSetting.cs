using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;

namespace YgomSystem.Utility
{
	// Token: 0x02000509 RID: 1289
	public class CardCheatPresetSetting : ScriptableObject
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardCheatPresetSetting instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0000216A File Offset: 0x0000036A
		public CardCheatPresetSetting.CheatPreset GetAt(int index)
		{
			return null;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0000216A File Offset: 0x0000036A
		public CardCheatPresetSetting.CheatPreset Get(int presetID)
		{
			return null;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetIndex(int presetID)
		{
			return 0;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNum()
		{
			return 0;
		}

		// Token: 0x04002915 RID: 10517
		public List<CardCheatPresetSetting.CheatPreset> presetList;

		// Token: 0x04002916 RID: 10518
		private const string path = "Debug/ScriptableObjects/CardCheatPresetSetting";

		// Token: 0x04002917 RID: 10519
		private static CardCheatPresetSetting _instance;

		// Token: 0x0200050A RID: 1290
		public enum PositionType
		{
			// Token: 0x04002919 RID: 10521
			Field,
			// Token: 0x0400291A RID: 10522
			Hand,
			// Token: 0x0400291B RID: 10523
			Deck,
			// Token: 0x0400291C RID: 10524
			Grave,
			// Token: 0x0400291D RID: 10525
			Exclude
		}

		// Token: 0x0200050B RID: 1291
		[Serializable]
		public class CheatInfo
		{
			// Token: 0x06002867 RID: 10343 RVA: 0x0000216A File Offset: 0x0000036A
			public CardCheatPresetSetting.CheatInfo Copy()
			{
				return null;
			}

			// Token: 0x0400291E RID: 10526
			public SharedDefinition.Location location;

			// Token: 0x0400291F RID: 10527
			public CardCheatPresetSetting.PositionType position;

			// Token: 0x04002920 RID: 10528
			public int cardID;

			// Token: 0x04002921 RID: 10529
			public int createNum;

			// Token: 0x04002922 RID: 10530
			public string note;
		}

		// Token: 0x0200050C RID: 1292
		[Serializable]
		public class CheatPreset
		{
			// Token: 0x06002869 RID: 10345 RVA: 0x0000216A File Offset: 0x0000036A
			public CardCheatPresetSetting.CheatPreset Copy()
			{
				return null;
			}

			// Token: 0x04002923 RID: 10531
			public int presetID;

			// Token: 0x04002924 RID: 10532
			public List<CardCheatPresetSetting.CheatInfo> infoList;

			// Token: 0x04002925 RID: 10533
			public string note;
		}
	}
}

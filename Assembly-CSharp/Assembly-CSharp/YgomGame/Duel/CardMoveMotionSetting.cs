using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CE2 RID: 3298
	public class CardMoveMotionSetting : ScriptableObject
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005E5A RID: 24154 RVA: 0x0000216D File Offset: 0x0000036D
		public static CardMoveMotionSetting instance
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x0000216A File Offset: 0x0000036A
		public CardMoveMotionSetting.MotionInfo GetInfo(SharedDefinition.Location fromLocation, int fromPosition, int fromIndex, SharedDefinition.Location toLocation, int toPosition, int toIndex, CardMoveMotionSetting.CardType cardType, int cardPower, CardMoveMotionSetting.FaceType faceType)
		{
			return null;
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x000029CC File Offset: 0x00000BCC
		private CardMoveMotionSetting.PositionType PositionToPositionType(int position, int index)
		{
			return CardMoveMotionSetting.PositionType.Unknown;
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0000216A File Offset: 0x0000036A
		public CardMoveMotionSetting.MotionInfo GetInfo(SharedDefinition.Location fromLocation, CardMoveMotionSetting.PositionType fromPosition, SharedDefinition.Location toLocation, CardMoveMotionSetting.PositionType toPosition, CardMoveMotionSetting.CardType cardType, int cardPower, CardMoveMotionSetting.FaceType faceType)
		{
			return null;
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0000216A File Offset: 0x0000036A
		public CardMoveMotionSetting.MotionInfo GetInfo(SharedDefinition.Location fromLocation, int fromPosition, int fromIndex, SharedDefinition.Location toLocation, int toPosition, int toIndex, bool toFace, CardMoveMotionSetting.CardType cardType, int cardPower = 0)
		{
			return null;
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x0000216A File Offset: 0x0000036A
		public BezierMotionSetting[] GetMotion(SharedDefinition.Location fromLocation, int fromPosition, int fromIndex, SharedDefinition.Location toLocation, int toPosition, int toIndex, bool toFace, CardMoveMotionSetting.CardType cardType, int cardPower = 0)
		{
			return null;
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardMoveMotionSetting.MotionInfo> GetInfo(CardMoveMotionSetting.PositionType fromPosition, CardMoveMotionSetting.PositionType toPosition)
		{
			return null;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetInfoIndex(CardMoveMotionSetting.MotionInfo info)
		{
			return 0;
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetMoveStartSE(string seCode)
		{
			return null;
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load(Action onFinished)
		{
		}

		// Token: 0x040099C6 RID: 39366
		public List<CardMoveMotionSetting.MotionInfo> infoList;

		// Token: 0x040099C7 RID: 39367
		private const string motionSettingPath = "Duel/ScriptableObject/BezierMotion/CardMoveMotion/CardMoveMotionSetting";

		// Token: 0x02000CE3 RID: 3299
		[Serializable]
		public class MotionInfo
		{
			// Token: 0x06005E65 RID: 24165 RVA: 0x0000216A File Offset: 0x0000036A
			public CardMoveMotionSetting.MotionInfo Copy()
			{
				return null;
			}

			// Token: 0x040099C8 RID: 39368
			public CardMoveMotionSetting.TeamType teamType;

			// Token: 0x040099C9 RID: 39369
			public CardMoveMotionSetting.PositionType fromPositon;

			// Token: 0x040099CA RID: 39370
			public CardMoveMotionSetting.PositionType toPosition;

			// Token: 0x040099CB RID: 39371
			public CardMoveMotionSetting.CardType cardType;

			// Token: 0x040099CC RID: 39372
			public int cardPower;

			// Token: 0x040099CD RID: 39373
			public CardMoveMotionSetting.FaceType faceType;

			// Token: 0x040099CE RID: 39374
			public List<BezierMotionSetting> motionList;

			// Token: 0x040099CF RID: 39375
			public string seCode;
		}

		// Token: 0x02000CE4 RID: 3300
		public enum TeamType
		{
			// Token: 0x040099D1 RID: 39377
			Both,
			// Token: 0x040099D2 RID: 39378
			Myself,
			// Token: 0x040099D3 RID: 39379
			Rival
		}

		// Token: 0x02000CE5 RID: 3301
		public enum PositionType
		{
			// Token: 0x040099D5 RID: 39381
			Unknown,
			// Token: 0x040099D6 RID: 39382
			Deck,
			// Token: 0x040099D7 RID: 39383
			ExtraDeck,
			// Token: 0x040099D8 RID: 39384
			Hand,
			// Token: 0x040099D9 RID: 39385
			MonsterZone,
			// Token: 0x040099DA RID: 39386
			MagicZone,
			// Token: 0x040099DB RID: 39387
			Grave,
			// Token: 0x040099DC RID: 39388
			Exclude,
			// Token: 0x040099DD RID: 39389
			XyzMaterial
		}

		// Token: 0x02000CE6 RID: 3302
		public enum CardType
		{
			// Token: 0x040099DF RID: 39391
			Normal,
			// Token: 0x040099E0 RID: 39392
			Soul,
			// Token: 0x040099E1 RID: 39393
			Both
		}

		// Token: 0x02000CE7 RID: 3303
		public enum FaceType
		{
			// Token: 0x040099E3 RID: 39395
			Face,
			// Token: 0x040099E4 RID: 39396
			Back,
			// Token: 0x040099E5 RID: 39397
			Both
		}
	}
}

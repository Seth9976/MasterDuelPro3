using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CFF RID: 3327
	public class CardRunEffectSetting : ScriptableObject
	{
		// Token: 0x06005FC6 RID: 24518 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRunEffectSetting.CardRunEffectInfo Get(int mrk, CardRunEffectSetting.Player player)
		{
			return null;
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x0000216A File Offset: 0x0000036A
		public CardRunEffectSetting.CardRunEffectInfo Get(int mrk)
		{
			return null;
		}

		// Token: 0x04009A85 RID: 39557
		public List<CardRunEffectSetting.CardRunEffectInfo> infoList;

		// Token: 0x02000D00 RID: 3328
		public enum PlayType
		{
			// Token: 0x04009A87 RID: 39559
			Timeline3D,
			// Token: 0x04009A88 RID: 39560
			TimelineHUD,
			// Token: 0x04009A89 RID: 39561
			SimpleEffect
		}

		// Token: 0x02000D01 RID: 3329
		public enum RunTiming
		{
			// Token: 0x04009A8B RID: 39563
			ChainRun,
			// Token: 0x04009A8C RID: 39564
			CardBreak,
			// Token: 0x04009A8D RID: 39565
			CardMove,
			// Token: 0x04009A8E RID: 39566
			SpecialFx,
			// Token: 0x04009A8F RID: 39567
			CardDisable,
			// Token: 0x04009A90 RID: 39568
			Unknown = 65535
		}

		// Token: 0x02000D02 RID: 3330
		public enum Player
		{
			// Token: 0x04009A92 RID: 39570
			Myself,
			// Token: 0x04009A93 RID: 39571
			Rival,
			// Token: 0x04009A94 RID: 39572
			Any
		}

		// Token: 0x02000D03 RID: 3331
		public enum RotationType
		{
			// Token: 0x04009A96 RID: 39574
			None,
			// Token: 0x04009A97 RID: 39575
			PivotToTarget
		}

		// Token: 0x02000D04 RID: 3332
		public enum ExtraSetting
		{
			// Token: 0x04009A99 RID: 39577
			None,
			// Token: 0x04009A9A RID: 39578
			PositionActivation,
			// Token: 0x04009A9B RID: 39579
			CardAttackPosition,
			// Token: 0x04009A9C RID: 39580
			ChangeLayerMagic,
			// Token: 0x04009A9D RID: 39581
			ChangeLayerOver3D
		}

		// Token: 0x02000D05 RID: 3333
		[Serializable]
		public class CardRunEffectInfo
		{
			// Token: 0x06005FC9 RID: 24521 RVA: 0x0000216A File Offset: 0x0000036A
			public CardRunEffectSetting.CardRunEffectInfo Copy()
			{
				return null;
			}

			// Token: 0x04009A9E RID: 39582
			public bool enable;

			// Token: 0x04009A9F RID: 39583
			public int mrk;

			// Token: 0x04009AA0 RID: 39584
			public CardRunEffectSetting.Player player;

			// Token: 0x04009AA1 RID: 39585
			public CardRunEffectSetting.RunTiming runTiming;

			// Token: 0x04009AA2 RID: 39586
			public bool useTargetEffect;

			// Token: 0x04009AA3 RID: 39587
			public CardRunEffectSetting.PlayType playType;

			// Token: 0x04009AA4 RID: 39588
			public int effectType;

			// Token: 0x04009AA5 RID: 39589
			public string effectPath;

			// Token: 0x04009AA6 RID: 39590
			public CardRunEffectSetting.RotationType rotationType;

			// Token: 0x04009AA7 RID: 39591
			public Vector3 pivot;

			// Token: 0x04009AA8 RID: 39592
			public float delay;

			// Token: 0x04009AA9 RID: 39593
			public CardRunEffectSetting.ExtraSetting extraSettings;

			// Token: 0x04009AAA RID: 39594
			public bool useCenterEffect;

			// Token: 0x04009AAB RID: 39595
			public CardRunEffectSetting.PlayType centerPlayType;

			// Token: 0x04009AAC RID: 39596
			public int centerEffectType;

			// Token: 0x04009AAD RID: 39597
			public string centerEffectPath;

			// Token: 0x04009AAE RID: 39598
			public float centerDelay;

			// Token: 0x04009AAF RID: 39599
			public CardRunEffectSetting.ExtraSetting centerExtraSettings;

			// Token: 0x04009AB0 RID: 39600
			public string seLabel;

			// Token: 0x04009AB1 RID: 39601
			public bool is3Dse;

			// Token: 0x04009AB2 RID: 39602
			public bool waitFinish;

			// Token: 0x04009AB3 RID: 39603
			public float finishSecond;

			// Token: 0x04009AB4 RID: 39604
			public string cameraShakeType;

			// Token: 0x04009AB5 RID: 39605
			public bool essential;
		}
	}
}

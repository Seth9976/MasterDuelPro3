using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D63 RID: 3427
	public class DuelEffectPool : MonoBehaviour
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060063CB RID: 25547 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060063CC RID: 25548 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060063CD RID: 25549 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060063CE RID: 25550 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isTerminated
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060063CF RID: 25551 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060063D0 RID: 25552 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
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

		// Token: 0x060063D1 RID: 25553 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelEffectPool Create(DuelGameObjectManager goManager, GameObject root, string name)
		{
			return null;
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadEffects(Dictionary<DuelEffectPool.Type, object[]> paths)
		{
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x0000216D File Offset: 0x0000036D
		public void Inactivate()
		{
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x000F5B4C File Offset: 0x000F3D4C
		public T Use<T>(DuelEffectPool.Type type, GameObject target, bool autoQuit, bool enableRestock = true, Action onFinished = null) where T : DuelEffectHandle
		{
			return default(T);
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x0000216A File Offset: 0x0000036A
		public SimpleEffect UseSimple(DuelEffectPool.Type type, GameObject target, bool autoQuit, bool enableRestock = true, Action onFinished = null)
		{
			return null;
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0000216D File Offset: 0x0000036D
		public void Unuse(DuelEffectPool.Type type, DuelEffectHandle eff)
		{
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializingStep()
		{
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x0000216D File Offset: 0x0000036D
		private void TerminatingStep()
		{
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0000216D File Offset: 0x0000036D
		private void Create(DuelEffectPool.Type type, int numCreates)
		{
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x0000216D File Offset: 0x0000036D
		private void Enqueue(DuelEffectPool.Type type, GameObject go)
		{
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject Dequeue(DuelEffectPool.Type type, bool enableRestock)
		{
			return null;
		}

		// Token: 0x04009DDC RID: 40412
		private DuelEffectPool.Step step;

		// Token: 0x04009DDD RID: 40413
		private Dictionary<DuelEffectPool.Type, Queue<GameObject>> effects;

		// Token: 0x04009DDE RID: 40414
		private Dictionary<DuelEffectPool.Type, List<DuelEffectHandle>> usingEffects;

		// Token: 0x04009DDF RID: 40415
		private List<DuelEffectPool.Type> remainTypes;

		// Token: 0x04009DE0 RID: 40416
		private Dictionary<DuelEffectPool.Type, global::UnityEngine.Object> effSrcs;

		// Token: 0x04009DE1 RID: 40417
		private List<GameObject> allEffects;

		// Token: 0x04009DE2 RID: 40418
		private List<GameObject> usedEffects;

		// Token: 0x04009DE3 RID: 40419
		private const string parentDir = "Duel/Effects";

		// Token: 0x04009DE4 RID: 40420
		private static readonly Dictionary<DuelEffectPool.Type, object[]> srcPaths;

		// Token: 0x02000D64 RID: 3428
		private enum Step
		{
			// Token: 0x04009DE6 RID: 40422
			Initializing,
			// Token: 0x04009DE7 RID: 40423
			Idle,
			// Token: 0x04009DE8 RID: 40424
			Terminating
		}

		// Token: 0x02000D65 RID: 3429
		public enum Type
		{
			// Token: 0x04009DEA RID: 40426
			AffectRelative = 2,
			// Token: 0x04009DEB RID: 40427
			AttackDirect00 = 6,
			// Token: 0x04009DEC RID: 40428
			AttackDirect01,
			// Token: 0x04009DED RID: 40429
			AttackGuard00,
			// Token: 0x04009DEE RID: 40430
			TargetingAiming = 10,
			// Token: 0x04009DEF RID: 40431
			CardActivate = 13,
			// Token: 0x04009DF0 RID: 40432
			CardExplosion = 17,
			// Token: 0x04009DF1 RID: 40433
			CardBreak,
			// Token: 0x04009DF2 RID: 40434
			CardDecide2 = 20,
			// Token: 0x04009DF3 RID: 40435
			CardHappen,
			// Token: 0x04009DF4 RID: 40436
			CardDisable,
			// Token: 0x04009DF5 RID: 40437
			CardLockon00,
			// Token: 0x04009DF6 RID: 40438
			ActiveCardAuraMiddle00,
			// Token: 0x04009DF7 RID: 40439
			ActiveCardAuraMiddle01,
			// Token: 0x04009DF8 RID: 40440
			CardMoveTrail01 = 27,
			// Token: 0x04009DF9 RID: 40441
			DeckHighlight00 = 29,
			// Token: 0x04009DFA RID: 40442
			DrawArrow00,
			// Token: 0x04009DFB RID: 40443
			FieldIcon00 = 32,
			// Token: 0x04009DFC RID: 40444
			Invalid00 = 34,
			// Token: 0x04009DFD RID: 40445
			MaterialMonster,
			// Token: 0x04009DFE RID: 40446
			SacrificeRun = 41,
			// Token: 0x04009DFF RID: 40447
			SacrificeTarget,
			// Token: 0x04009E00 RID: 40448
			SummonPlane = 45,
			// Token: 0x04009E01 RID: 40449
			TapMonster = 49,
			// Token: 0x04009E02 RID: 40450
			HitNullSmall,
			// Token: 0x04009E03 RID: 40451
			HitNullLargest = 53,
			// Token: 0x04009E04 RID: 40452
			HitLightSmall,
			// Token: 0x04009E05 RID: 40453
			HitLightLargest = 57,
			// Token: 0x04009E06 RID: 40454
			HitDarkSmall,
			// Token: 0x04009E07 RID: 40455
			HitDarkLargest = 61,
			// Token: 0x04009E08 RID: 40456
			HitWaterSmall,
			// Token: 0x04009E09 RID: 40457
			HitWaterLargest = 65,
			// Token: 0x04009E0A RID: 40458
			HitFireSmall,
			// Token: 0x04009E0B RID: 40459
			HitFireLargest = 69,
			// Token: 0x04009E0C RID: 40460
			HitEarthSmall,
			// Token: 0x04009E0D RID: 40461
			HitEarthLargest = 73,
			// Token: 0x04009E0E RID: 40462
			HitWindSmall,
			// Token: 0x04009E0F RID: 40463
			HitWindLargest = 77,
			// Token: 0x04009E10 RID: 40464
			CardDecide00 = 85,
			// Token: 0x04009E11 RID: 40465
			CardCrack00,
			// Token: 0x04009E12 RID: 40466
			AttackTrailNullLow,
			// Token: 0x04009E13 RID: 40467
			AttackTrailNullHigh,
			// Token: 0x04009E14 RID: 40468
			AttackTrailLightLow,
			// Token: 0x04009E15 RID: 40469
			AttackTrailLightHigh,
			// Token: 0x04009E16 RID: 40470
			AttackTrailDarkLow,
			// Token: 0x04009E17 RID: 40471
			AttackTrailDarkHigh,
			// Token: 0x04009E18 RID: 40472
			AttackTrailWaterLow,
			// Token: 0x04009E19 RID: 40473
			AttackTrailWaterHigh,
			// Token: 0x04009E1A RID: 40474
			AttackTrailFireLow,
			// Token: 0x04009E1B RID: 40475
			AttackTrailFireHigh,
			// Token: 0x04009E1C RID: 40476
			AttackTrailEarthLow,
			// Token: 0x04009E1D RID: 40477
			AttackTrailEarthHigh,
			// Token: 0x04009E1E RID: 40478
			AttackTrailWindLow,
			// Token: 0x04009E1F RID: 40479
			AttackTrailWindHigh,
			// Token: 0x04009E20 RID: 40480
			AttackTrailGodLow,
			// Token: 0x04009E21 RID: 40481
			AttackTrailGodHigh,
			// Token: 0x04009E22 RID: 40482
			AttackNullLow,
			// Token: 0x04009E23 RID: 40483
			AttackNullHigh,
			// Token: 0x04009E24 RID: 40484
			AttackLightLow,
			// Token: 0x04009E25 RID: 40485
			AttackLightHigh,
			// Token: 0x04009E26 RID: 40486
			AttackDarkLow,
			// Token: 0x04009E27 RID: 40487
			AttackDarkHigh,
			// Token: 0x04009E28 RID: 40488
			AttackWaterLow,
			// Token: 0x04009E29 RID: 40489
			AttackWaterHigh,
			// Token: 0x04009E2A RID: 40490
			AttackFireLow,
			// Token: 0x04009E2B RID: 40491
			AttackFireHigh,
			// Token: 0x04009E2C RID: 40492
			AttackEarthLow,
			// Token: 0x04009E2D RID: 40493
			AttackEarthHigh,
			// Token: 0x04009E2E RID: 40494
			AttackWindLow,
			// Token: 0x04009E2F RID: 40495
			AttackWindHigh,
			// Token: 0x04009E30 RID: 40496
			AttackGodLow,
			// Token: 0x04009E31 RID: 40497
			AttackGodHigh,
			// Token: 0x04009E32 RID: 40498
			DrawDrag = 122,
			// Token: 0x04009E33 RID: 40499
			DrawImpact,
			// Token: 0x04009E34 RID: 40500
			ActiveCardAuraLow00,
			// Token: 0x04009E35 RID: 40501
			ActiveCardAuraLow01,
			// Token: 0x04009E36 RID: 40502
			ActiveCardAuraHigh00,
			// Token: 0x04009E37 RID: 40503
			ActiveCardAuraHigh01,
			// Token: 0x04009E38 RID: 40504
			LinkTargetCard = 131,
			// Token: 0x04009E39 RID: 40505
			SelectingCursorDeck,
			// Token: 0x04009E3A RID: 40506
			SelectingCursorMonster,
			// Token: 0x04009E3B RID: 40507
			SelectingCursorMagic,
			// Token: 0x04009E3C RID: 40508
			SelectingCursorField,
			// Token: 0x04009E3D RID: 40509
			ActiveMagicCardAuraHigh00,
			// Token: 0x04009E3E RID: 40510
			ActiveMagicCardAuraHigh01,
			// Token: 0x04009E3F RID: 40511
			CardExclude,
			// Token: 0x04009E40 RID: 40512
			CardExcludeTrail,
			// Token: 0x04009E41 RID: 40513
			CardBreakExcludeTrail,
			// Token: 0x04009E42 RID: 40514
			CardReleaseTrail,
			// Token: 0x04009E43 RID: 40515
			CardReleaseExcludeTrail,
			// Token: 0x04009E44 RID: 40516
			FusionTargetCard,
			// Token: 0x04009E45 RID: 40517
			SynchroTargetCard,
			// Token: 0x04009E46 RID: 40518
			RitualTargetCard,
			// Token: 0x04009E47 RID: 40519
			LandingLinkMiddle0,
			// Token: 0x04009E48 RID: 40520
			LandingLinkMiddle1,
			// Token: 0x04009E49 RID: 40521
			LandingLinkHigh0,
			// Token: 0x04009E4A RID: 40522
			LandingLinkHigh1,
			// Token: 0x04009E4B RID: 40523
			LandingFusionMiddle0,
			// Token: 0x04009E4C RID: 40524
			LandingFusionMiddle1,
			// Token: 0x04009E4D RID: 40525
			LandingFusionHigh0,
			// Token: 0x04009E4E RID: 40526
			LandingFusionHigh1,
			// Token: 0x04009E4F RID: 40527
			LandingPendulumMiddle0,
			// Token: 0x04009E50 RID: 40528
			LandingPendulumMiddle1,
			// Token: 0x04009E51 RID: 40529
			LandingPendulumHigh0,
			// Token: 0x04009E52 RID: 40530
			LandingPendulumHigh1,
			// Token: 0x04009E53 RID: 40531
			LandingAdvanceMiddle0,
			// Token: 0x04009E54 RID: 40532
			LandingAdvanceMiddle1,
			// Token: 0x04009E55 RID: 40533
			LandingAdvanceHigh0,
			// Token: 0x04009E56 RID: 40534
			LandingAdvanceHigh1,
			// Token: 0x04009E57 RID: 40535
			SelectTargetMonsterZone,
			// Token: 0x04009E58 RID: 40536
			DecideTargetMonsterZone,
			// Token: 0x04009E59 RID: 40537
			SelectTargetMagicZone,
			// Token: 0x04009E5A RID: 40538
			DecideTargetMagicZone,
			// Token: 0x04009E5B RID: 40539
			SelectTargetCard,
			// Token: 0x04009E5C RID: 40540
			DecideTargetCard,
			// Token: 0x04009E5D RID: 40541
			AttackTargetLine,
			// Token: 0x04009E5E RID: 40542
			LandingSpSummonMiddle0,
			// Token: 0x04009E5F RID: 40543
			LandingSpSummonMiddle1,
			// Token: 0x04009E60 RID: 40544
			LandingSpSummonHigh0,
			// Token: 0x04009E61 RID: 40545
			LandingSpSummonHigh1,
			// Token: 0x04009E62 RID: 40546
			LandingMagic,
			// Token: 0x04009E63 RID: 40547
			LandingSummonSmall,
			// Token: 0x04009E64 RID: 40548
			EquipTargetLine,
			// Token: 0x04009E65 RID: 40549
			LandingXyzMiddle0,
			// Token: 0x04009E66 RID: 40550
			LandingXyzMiddle1,
			// Token: 0x04009E67 RID: 40551
			LandingXyzHigh0,
			// Token: 0x04009E68 RID: 40552
			LandingXyzHigh1,
			// Token: 0x04009E69 RID: 40553
			LandingRitualMiddle0,
			// Token: 0x04009E6A RID: 40554
			LandingRitualMiddle1,
			// Token: 0x04009E6B RID: 40555
			LandingRitualHigh0,
			// Token: 0x04009E6C RID: 40556
			LandingRitualHigh1,
			// Token: 0x04009E6D RID: 40557
			LandingSynchroMiddle0,
			// Token: 0x04009E6E RID: 40558
			LandingSynchroMiddle1,
			// Token: 0x04009E6F RID: 40559
			LandingSynchroHigh0,
			// Token: 0x04009E70 RID: 40560
			LandingSynchroHigh1,
			// Token: 0x04009E71 RID: 40561
			DrawLimitedCardMove,
			// Token: 0x04009E72 RID: 40562
			DrawLimitedCardImpact,
			// Token: 0x04009E73 RID: 40563
			XyzMaterialIn,
			// Token: 0x04009E74 RID: 40564
			XyzMaterialOut,
			// Token: 0x04009E75 RID: 40565
			XyzMaterialTrail,
			// Token: 0x04009E76 RID: 40566
			ActivateEffectExDeck,
			// Token: 0x04009E77 RID: 40567
			ActivateEffectGrave,
			// Token: 0x04009E78 RID: 40568
			ActivateEffectExclude,
			// Token: 0x04009E79 RID: 40569
			SelectTargetDirectAttack,
			// Token: 0x04009E7A RID: 40570
			DecideTargetDirectAttack,
			// Token: 0x04009E7B RID: 40571
			HitGodSmall,
			// Token: 0x04009E7C RID: 40572
			HitGodLargest,
			// Token: 0x04009E7D RID: 40573
			CardVanish,
			// Token: 0x04009E7E RID: 40574
			XyzMaterialAppear,
			// Token: 0x04009E7F RID: 40575
			TutorialHighlight,
			// Token: 0x04009E80 RID: 40576
			TutorialArrow,
			// Token: 0x04009E81 RID: 40577
			TutorialUIHighlight,
			// Token: 0x04009E82 RID: 40578
			LethalEffectNear,
			// Token: 0x04009E83 RID: 40579
			LethalEffectFar,
			// Token: 0x04009E84 RID: 40580
			HandDragLine,
			// Token: 0x04009E85 RID: 40581
			CardRunEffectMugenHouyou,
			// Token: 0x04009E86 RID: 40582
			CardRunEffectSkillDrain,
			// Token: 0x04009E87 RID: 40583
			CardRunEffectSkillDrainCenter,
			// Token: 0x04009E88 RID: 40584
			CardRunEffectLightningStorm,
			// Token: 0x04009E89 RID: 40585
			CardRunEffectMugenHouyouCenter,
			// Token: 0x04009E8A RID: 40586
			SpSummonEffect,
			// Token: 0x04009E8B RID: 40587
			CardRunEffectHaruUrara,
			// Token: 0x04009E8C RID: 40588
			LethalEffectDarkMagicianNear,
			// Token: 0x04009E8D RID: 40589
			LethalEffectDarkMagicianFar,
			// Token: 0x04009E8E RID: 40590
			CardRunEffectEffectVeiler,
			// Token: 0x04009E8F RID: 40591
			LethalEffectBlueEyesNear,
			// Token: 0x04009E90 RID: 40592
			LethalEffectBlueEyesFar,
			// Token: 0x04009E91 RID: 40593
			LethalEffectRedEyesNear,
			// Token: 0x04009E92 RID: 40594
			LethalEffectRedEyesFar,
			// Token: 0x04009E93 RID: 40595
			AttackShootRedEyes,
			// Token: 0x04009E94 RID: 40596
			CardApply,
			// Token: 0x04009E95 RID: 40597
			ResidualEffectMugenHouyou,
			// Token: 0x04009E96 RID: 40598
			Noop = 65535
		}
	}
}

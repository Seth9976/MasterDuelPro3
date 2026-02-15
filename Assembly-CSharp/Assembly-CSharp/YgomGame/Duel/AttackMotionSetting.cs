using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C7D RID: 3197
	public class AttackMotionSetting : ScriptableObject
	{
		// Token: 0x06005BD8 RID: 23512 RVA: 0x0000216A File Offset: 0x0000036A
		public AttackMotionSetting.MotionInfo Get(string label)
		{
			return null;
		}

		// Token: 0x040096E3 RID: 38627
		public List<AttackMotionSetting.MotionInfo> infoList;

		// Token: 0x02000C7E RID: 3198
		public enum EffectTiming
		{
			// Token: 0x040096E5 RID: 38629
			OnStart,
			// Token: 0x040096E6 RID: 38630
			OnFinish
		}

		// Token: 0x02000C7F RID: 3199
		[Serializable]
		public class MotionInfo
		{
			// Token: 0x06005BDA RID: 23514 RVA: 0x0000216A File Offset: 0x0000036A
			public AttackMotionSetting.MotionInfo Copy()
			{
				return null;
			}

			// Token: 0x06005BDB RID: 23515 RVA: 0x000029C5 File Offset: 0x00000BC5
			private float GetStartTime(AttackMotionSetting.EffectTiming timing, float offset, int motionIndex)
			{
				return 0f;
			}

			// Token: 0x06005BDC RID: 23516 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetTrailStartTime(int motionIndex)
			{
				return 0f;
			}

			// Token: 0x06005BDD RID: 23517 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetAttackStartTime(int motionIndex)
			{
				return 0f;
			}

			// Token: 0x06005BDE RID: 23518 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetHitStartTime(int motionIndex)
			{
				return 0f;
			}

			// Token: 0x06005BDF RID: 23519 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetShootStartTime(int motionIndex)
			{
				return 0f;
			}

			// Token: 0x06005BE0 RID: 23520 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float GetTimelineStartTime(int motionIndex, int timelineIndex)
			{
				return 0f;
			}

			// Token: 0x040096E7 RID: 38631
			public string label;

			// Token: 0x040096E8 RID: 38632
			public List<AttackMotionSetting.MotionList> motionList;

			// Token: 0x040096E9 RID: 38633
			public string startSoundLabel;

			// Token: 0x040096EA RID: 38634
			public bool useTrailEffect;

			// Token: 0x040096EB RID: 38635
			public DuelEffectPool.Type trailEffect;

			// Token: 0x040096EC RID: 38636
			public AttackMotionSetting.EffectTiming trailTiming;

			// Token: 0x040096ED RID: 38637
			public float trailTimingOffset;

			// Token: 0x040096EE RID: 38638
			public bool useAttackEffect;

			// Token: 0x040096EF RID: 38639
			public DuelEffectPool.Type attackEffect;

			// Token: 0x040096F0 RID: 38640
			public string attackSoundLabel;

			// Token: 0x040096F1 RID: 38641
			public AttackMotionSetting.EffectTiming attackTiming;

			// Token: 0x040096F2 RID: 38642
			public float attackTimingOffset;

			// Token: 0x040096F3 RID: 38643
			public bool useHitEffect;

			// Token: 0x040096F4 RID: 38644
			public DuelEffectPool.Type hitEffect;

			// Token: 0x040096F5 RID: 38645
			public string hitSoundLabel;

			// Token: 0x040096F6 RID: 38646
			public AttackMotionSetting.EffectTiming hitTiming;

			// Token: 0x040096F7 RID: 38647
			public float hitTimingOffset;

			// Token: 0x040096F8 RID: 38648
			public bool forceHitEffect;

			// Token: 0x040096F9 RID: 38649
			public bool useShootEffect;

			// Token: 0x040096FA RID: 38650
			public DuelEffectPool.Type shootEffect;

			// Token: 0x040096FB RID: 38651
			public string shootSoundLabel;

			// Token: 0x040096FC RID: 38652
			public AttackMotionSetting.EffectTiming shootTiming;

			// Token: 0x040096FD RID: 38653
			public float shootTimingOffset;

			// Token: 0x040096FE RID: 38654
			public BezierMotionContainer shootMotionSetting;

			// Token: 0x040096FF RID: 38655
			public List<AttackMotionSetting.TimelineInfo> timelineInfoList;

			// Token: 0x04009700 RID: 38656
			public bool forLethalAttack;

			// Token: 0x04009701 RID: 38657
			public bool changeCardLayerToOver3D;
		}

		// Token: 0x02000C80 RID: 3200
		[Serializable]
		public class TimelineInfo
		{
			// Token: 0x06005BE2 RID: 23522 RVA: 0x0000216A File Offset: 0x0000036A
			public AttackMotionSetting.TimelineInfo Clone()
			{
				return null;
			}

			// Token: 0x04009702 RID: 38658
			public AttackMotionSetting.TimelineInfo.PlayTarget playTarget;

			// Token: 0x04009703 RID: 38659
			public bool line;

			// Token: 0x04009704 RID: 38660
			public bool trace;

			// Token: 0x04009705 RID: 38661
			public AttackMotionSetting.TimelineInfo.RotationMode rotationMode;

			// Token: 0x04009706 RID: 38662
			public bool onlyRotationY;

			// Token: 0x04009707 RID: 38663
			public string path;

			// Token: 0x04009708 RID: 38664
			public AttackMotionSetting.EffectTiming timing;

			// Token: 0x04009709 RID: 38665
			public float timingOffset;

			// Token: 0x0400970A RID: 38666
			public string soundLabel;

			// Token: 0x0400970B RID: 38667
			public bool changeLayerToOver3D;

			// Token: 0x02000C81 RID: 3201
			public enum PlayTarget
			{
				// Token: 0x0400970D RID: 38669
				Attacker,
				// Token: 0x0400970E RID: 38670
				Deffender,
				// Token: 0x0400970F RID: 38671
				Origin
			}

			// Token: 0x02000C82 RID: 3202
			public enum RotationMode
			{
				// Token: 0x04009711 RID: 38673
				None,
				// Token: 0x04009712 RID: 38674
				LookTarget
			}
		}

		// Token: 0x02000C83 RID: 3203
		[Serializable]
		public class MotionList
		{
			// Token: 0x170009C0 RID: 2496
			// (get) Token: 0x06005BE4 RID: 23524 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float animationTime
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x04009713 RID: 38675
			public List<BezierMotionSetting> settingList;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000DB1 RID: 3505
	public class EffectTaskBattleRun : EffectTask
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060066DD RID: 26333 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float attackExecPostTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskBattleRun(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadMotion()
		{
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetMotionLabel(EffectTaskBattleRun.AttackType attack_type, Content.Attribute attribute, Util.AttackLevel attackLevel, LethalEffect.EffectType effectType, bool isDirect)
		{
			return null;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0000216A File Offset: 0x0000036A
		public static AttackMotionSetting.MotionInfo GetMotionInfo(AttackMotionSetting setting, EffectTaskBattleRun.AttackType attack_type, Content.Attribute attribute, Util.AttackLevel attackLevel, LethalEffect.EffectType effectType, bool isDirect)
		{
			return null;
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadSEStep()
		{
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x0000216D File Offset: 0x0000036D
		private void AttackInitStep()
		{
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x0000216D File Offset: 0x0000036D
		private void AttackMoveStep()
		{
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x000F5CEC File Offset: 0x000F3EEC
		public static EffectTaskBattleRun.AttackEffectInfo AttackMove(CardRoot cardRoot, ChainedBezierMotion runMotion, AttackMotionSetting.MotionInfo motionInfo, int motionIndex, float time, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, DuelEffectPool effectPool)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x000F5D04 File Offset: 0x000F3F04
		private static EffectTaskBattleRun.AttackEffectInfo PlayTrailEffect(CardRoot cardRoot, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x000F5D1C File Offset: 0x000F3F1C
		private static EffectTaskBattleRun.AttackEffectInfo PlayAttackEffect(CardRoot cardRoot, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo, DuelEffectPool effectPool)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x000F5D34 File Offset: 0x000F3F34
		private static EffectTaskBattleRun.AttackEffectInfo PlayHitEffect(CardRoot cardRoot, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo, DuelEffectPool effectPool)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x000F5D4C File Offset: 0x000F3F4C
		private static EffectTaskBattleRun.AttackEffectInfo PlayShootEffect(CardRoot cardRoot, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo, DuelEffectPool effectPool)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x000F5D64 File Offset: 0x000F3F64
		public static EffectTaskBattleRun.AttackEffectInfo PlayTimeline(Transform card, Transform defaultParent, bool isMyself, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo, int timelineIndex)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x000F5D7C File Offset: 0x000F3F7C
		public static EffectTaskBattleRun.AttackEffectInfo UpdateTimeline(Transform card, EffectTaskBattleRun.AttackEffectInfo attackEffectInfo, AttackMotionSetting.MotionInfo motionInfo, int timelineIndex)
		{
			return default(EffectTaskBattleRun.AttackEffectInfo);
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetAttackLevel(CardRoot cardRoot, bool includeNoDamage, bool isBreak, bool isDamage, int targetPlayer)
		{
			return 0;
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x000029CC File Offset: 0x00000BCC
		private Content.Attribute GetAttackAttribute(short element)
		{
			return Content.Attribute.Null;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowDamage()
		{
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowDamageImpl(int team, int position, int damage)
		{
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardLayer(int layer)
		{
		}

		// Token: 0x0400A11A RID: 41242
		private bool hitEffect;

		// Token: 0x0400A11B RID: 41243
		private EffectTaskBattleRun.Step step;

		// Token: 0x0400A11C RID: 41244
		private float time;

		// Token: 0x0400A11D RID: 41245
		private CardRoot cardRoot;

		// Token: 0x0400A11E RID: 41246
		private int[] damages;

		// Token: 0x0400A11F RID: 41247
		private int prop;

		// Token: 0x0400A120 RID: 41248
		private int srcDamage;

		// Token: 0x0400A121 RID: 41249
		private int dstDamage;

		// Token: 0x0400A122 RID: 41250
		private bool isSrcBreak;

		// Token: 0x0400A123 RID: 41251
		private bool isDstBreak;

		// Token: 0x0400A124 RID: 41252
		private bool isSrcDamage;

		// Token: 0x0400A125 RID: 41253
		private bool isDstDamage;

		// Token: 0x0400A126 RID: 41254
		private EffectTaskBattleRun.AttackEffectInfo attackEffectInfo;

		// Token: 0x0400A127 RID: 41255
		private Content.Attribute attribute;

		// Token: 0x0400A128 RID: 41256
		public const float hitEffectOfsH = 3f;

		// Token: 0x0400A129 RID: 41257
		public static readonly Vector3 hitEffectOffset;

		// Token: 0x0400A12A RID: 41258
		private ChainedBezierMotion runMotion;

		// Token: 0x0400A12B RID: 41259
		private AttackMotionSetting.MotionInfo motionInfo;

		// Token: 0x0400A12C RID: 41260
		private int motionIndex;

		// Token: 0x0400A12D RID: 41261
		private const string BattleRunAttackSettingPath = "Duel/ScriptableObject/BattleRunAttackSetting";

		// Token: 0x0400A12E RID: 41262
		private const string AttackMotionSettingPath = "Duel/ScriptableObject/AttackMotionSetting";

		// Token: 0x0400A12F RID: 41263
		private int loadCounter;

		// Token: 0x0400A130 RID: 41264
		private BattleRunAttackSetting attackSetting;

		// Token: 0x0400A131 RID: 41265
		private LethalEffect.EffectType lethalEffectType;

		// Token: 0x02000DB2 RID: 3506
		private enum Step
		{
			// Token: 0x0400A133 RID: 41267
			LoadMotion,
			// Token: 0x0400A134 RID: 41268
			WaitCardMove,
			// Token: 0x0400A135 RID: 41269
			LoadSE,
			// Token: 0x0400A136 RID: 41270
			AttackInit,
			// Token: 0x0400A137 RID: 41271
			AttackMove,
			// Token: 0x0400A138 RID: 41272
			AttackBack,
			// Token: 0x0400A139 RID: 41273
			Lethal,
			// Token: 0x0400A13A RID: 41274
			Finish
		}

		// Token: 0x02000DB3 RID: 3507
		public struct AttackEffectInfo
		{
			// Token: 0x17000BB9 RID: 3001
			// (get) Token: 0x060066F5 RID: 26357 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isLethal
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400A13B RID: 41275
			public bool isFinished;

			// Token: 0x0400A13C RID: 41276
			public int srcTeam;

			// Token: 0x0400A13D RID: 41277
			public int srcPosition;

			// Token: 0x0400A13E RID: 41278
			public int dstTeam;

			// Token: 0x0400A13F RID: 41279
			public int dstPosition;

			// Token: 0x0400A140 RID: 41280
			public int attackLevel;

			// Token: 0x0400A141 RID: 41281
			public bool isDirectAttack;

			// Token: 0x0400A142 RID: 41282
			public bool isSrcLethal;

			// Token: 0x0400A143 RID: 41283
			public bool isDstLethal;

			// Token: 0x0400A144 RID: 41284
			public Vector3 attackTargetPos;

			// Token: 0x0400A145 RID: 41285
			public Vector3 attackDefaultPos;

			// Token: 0x0400A146 RID: 41286
			public Quaternion attackDefaultRot;

			// Token: 0x0400A147 RID: 41287
			public Vector3 attackTargetHandPos;

			// Token: 0x0400A148 RID: 41288
			public Vector3 attackSrcHandPos;

			// Token: 0x0400A149 RID: 41289
			public bool isStart;

			// Token: 0x0400A14A RID: 41290
			public bool isTrail;

			// Token: 0x0400A14B RID: 41291
			public bool isAttack;

			// Token: 0x0400A14C RID: 41292
			public bool isHit;

			// Token: 0x0400A14D RID: 41293
			public bool isShoot;

			// Token: 0x0400A14E RID: 41294
			public bool[] timelinePlayed;

			// Token: 0x0400A14F RID: 41295
			public PlayableDirector[] playingTimeline;

			// Token: 0x0400A150 RID: 41296
			public SimpleEffect shootEffect;

			// Token: 0x0400A151 RID: 41297
			public Vector3 shootOriginPosition;

			// Token: 0x0400A152 RID: 41298
			public Vector3 shootTargetPosition;

			// Token: 0x0400A153 RID: 41299
			public Quaternion shootOriginRotation;

			// Token: 0x0400A154 RID: 41300
			public Quaternion shootTargetRotation;

			// Token: 0x0400A155 RID: 41301
			public ChainedBezierMotion shootMotion;
		}

		// Token: 0x02000DB4 RID: 3508
		public enum AttackType
		{
			// Token: 0x0400A157 RID: 41303
			Strike,
			// Token: 0x0400A158 RID: 41304
			Flash,
			// Token: 0x0400A159 RID: 41305
			Shoot,
			// Token: 0x0400A15A RID: 41306
			Unknown
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Card;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Test
{
	// Token: 0x020008B3 RID: 2227
	public class DuelBattleTest : ViewController
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x0000216D File Offset: 0x0000036D
		public List<GameObject> cards
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

		// Token: 0x06004118 RID: 16664 RVA: 0x0000216D File Offset: 0x0000036D
		public void Start()
		{
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupObjects()
		{
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateCard(Transform parent)
		{
			return null;
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearObjects()
		{
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartMotionTest()
		{
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetZone(SharedDefinition.Location location, DuelBattleTest.ZoneType zone_type, int position)
		{
			return null;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetHand(SharedDefinition.Location location)
		{
			return null;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMotionTest()
		{
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTrailEffect()
		{
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayAttackEffect()
		{
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayHitEffect()
		{
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayShootEffect()
		{
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardLayer(int layer)
		{
		}

		// Token: 0x04007F49 RID: 32585
		[SerializeField]
		private CameraViewSetting cameraViewSetting;

		// Token: 0x04007F4A RID: 32586
		[SerializeField]
		private AttackMotionSetting battleRunMotionSetting;

		// Token: 0x04007F4B RID: 32587
		[SerializeField]
		private Transform objectsParent;

		// Token: 0x04007F4C RID: 32588
		[SerializeField]
		private Transform duelObjectsParent;

		// Token: 0x04007F4D RID: 32589
		[SerializeField]
		private GameObject cardPrefab;

		// Token: 0x04007F4E RID: 32590
		[SerializeField]
		private GameObject nearFieldPrefab;

		// Token: 0x04007F4F RID: 32591
		[SerializeField]
		private GameObject farFieldPrefab;

		// Token: 0x04007F50 RID: 32592
		[SerializeField]
		private float matOffset;

		// Token: 0x04007F51 RID: 32593
		[SerializeField]
		private ElementObjectManager ui;

		// Token: 0x04007F52 RID: 32594
		private GameObject nearMat;

		// Token: 0x04007F53 RID: 32595
		private GameObject farMat;

		// Token: 0x04007F54 RID: 32596
		private DuelEffectPool effectPool;

		// Token: 0x04007F55 RID: 32597
		private SharedDefinition.Location fromLocation;

		// Token: 0x04007F56 RID: 32598
		private DuelBattleTest.ZoneType fromZone;

		// Token: 0x04007F57 RID: 32599
		private int fromPosition;

		// Token: 0x04007F58 RID: 32600
		private SharedDefinition.Location toLocation;

		// Token: 0x04007F59 RID: 32601
		private DuelBattleTest.ZoneType toZone;

		// Token: 0x04007F5A RID: 32602
		private int toPosition;

		// Token: 0x04007F5B RID: 32603
		private GameObject motionCard;

		// Token: 0x04007F5C RID: 32604
		private GameObject targetCard;

		// Token: 0x04007F5D RID: 32605
		private EffectTaskBattleRun.AttackType attackType;

		// Token: 0x04007F5E RID: 32606
		private Content.Attribute attribute;

		// Token: 0x04007F5F RID: 32607
		private Util.AttackLevel attackLevel;

		// Token: 0x04007F60 RID: 32608
		private int motionIndex;

		// Token: 0x04007F61 RID: 32609
		private bool guard;

		// Token: 0x04007F62 RID: 32610
		private bool directAttack;

		// Token: 0x04007F63 RID: 32611
		private bool isLethal;

		// Token: 0x04007F64 RID: 32612
		private LethalEffect.EffectType effectType;

		// Token: 0x04007F65 RID: 32613
		private bool reqStart;

		// Token: 0x04007F66 RID: 32614
		private AttackMotionSetting.MotionInfo motionInfo;

		// Token: 0x04007F67 RID: 32615
		private bool isPlaying;

		// Token: 0x04007F68 RID: 32616
		private float currentTime;

		// Token: 0x04007F69 RID: 32617
		private int playMotionIndex;

		// Token: 0x04007F6A RID: 32618
		private Vector3 originPosition;

		// Token: 0x04007F6B RID: 32619
		private Vector3 targetPosition;

		// Token: 0x04007F6C RID: 32620
		private Quaternion originRotation;

		// Token: 0x04007F6D RID: 32621
		private Quaternion targetRotation;

		// Token: 0x04007F6E RID: 32622
		private SimpleEffect moveTrail;

		// Token: 0x04007F6F RID: 32623
		private bool isStart;

		// Token: 0x04007F70 RID: 32624
		private bool isTrail;

		// Token: 0x04007F71 RID: 32625
		private bool isAttack;

		// Token: 0x04007F72 RID: 32626
		private bool isHit;

		// Token: 0x04007F73 RID: 32627
		private bool isShoot;

		// Token: 0x04007F74 RID: 32628
		private ChainedBezierMotion chaindBezierMotion;

		// Token: 0x04007F75 RID: 32629
		private ChainedBezierMotion shootMotion;

		// Token: 0x04007F76 RID: 32630
		private SimpleEffect shootEffect;

		// Token: 0x04007F77 RID: 32631
		private Vector3 shootOriginPosition;

		// Token: 0x04007F78 RID: 32632
		private Vector3 shootTargetPosition;

		// Token: 0x04007F79 RID: 32633
		private Quaternion shootOriginRotation;

		// Token: 0x04007F7A RID: 32634
		private Quaternion shootTargetRotation;

		// Token: 0x04007F7B RID: 32635
		private EffectTaskBattleRun.AttackEffectInfo attackEffectInfo;

		// Token: 0x020008B4 RID: 2228
		private enum ZoneType
		{
			// Token: 0x04007F7D RID: 32637
			Main,
			// Token: 0x04007F7E RID: 32638
			Ex
		}
	}
}

using System;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Test
{
	// Token: 0x020008B1 RID: 2225
	public class CardMoveTest : ViewController
	{
		// Token: 0x060040FF RID: 16639 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFromPlayer(int v)
		{
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetToPlayer(int v)
		{
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFromPosition(int v)
		{
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetToPosition(int v)
		{
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLandingType(int v)
		{
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSummonEffectType(int v)
		{
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFromIsAttack(int v)
		{
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetToIsAttack(int v)
		{
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsStrongSummon(int v)
		{
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsSummonEffect(int v)
		{
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDestCardID(string v)
		{
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMatCardID(string v)
		{
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMatCardNum(string v)
		{
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTunerLevel(string v)
		{
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMaterialLevel(string v)
		{
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTunerNum(string v)
		{
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupMenu()
		{
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000F475C File Offset: 0x000F295C
		private ValueTuple<Vector3, Quaternion, Vector3> GetTrans(SharedDefinition.Location location, int position)
		{
			return default(ValueTuple<Vector3, Quaternion, Vector3>);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardTransform(float time)
		{
		}

		// Token: 0x04007F1E RID: 32542
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04007F1F RID: 32543
		[SerializeField]
		private CardMoveMotionSetting motionSetting;

		// Token: 0x04007F20 RID: 32544
		[SerializeField]
		private GameObject prefabCard;

		// Token: 0x04007F21 RID: 32545
		[SerializeField]
		private GameObject prefabFieldMatNear;

		// Token: 0x04007F22 RID: 32546
		[SerializeField]
		private GameObject prefabFieldMatFar;

		// Token: 0x04007F23 RID: 32547
		private ElementObjectManager ui;

		// Token: 0x04007F24 RID: 32548
		private SharedDefinition.Location fromPlayer;

		// Token: 0x04007F25 RID: 32549
		private int fromPos;

		// Token: 0x04007F26 RID: 32550
		private bool fromIsAttack;

		// Token: 0x04007F27 RID: 32551
		private SharedDefinition.Location toPlayer;

		// Token: 0x04007F28 RID: 32552
		private int toPos;

		// Token: 0x04007F29 RID: 32553
		private bool toIsAttack;

		// Token: 0x04007F2A RID: 32554
		private EffectTaskCardMove.LandingType landingType;

		// Token: 0x04007F2B RID: 32555
		private EffectTaskCardMove.SummonEffectType summonEffectType;

		// Token: 0x04007F2C RID: 32556
		private bool isStrong;

		// Token: 0x04007F2D RID: 32557
		private bool isSummonEffect;

		// Token: 0x04007F2E RID: 32558
		private int cardID;

		// Token: 0x04007F2F RID: 32559
		private int matCardID;

		// Token: 0x04007F30 RID: 32560
		private int matCardNum;

		// Token: 0x04007F31 RID: 32561
		private int tunerLevel;

		// Token: 0x04007F32 RID: 32562
		private int materialLevel;

		// Token: 0x04007F33 RID: 32563
		private int tunerNum;

		// Token: 0x04007F34 RID: 32564
		private DuelEffectPool effectPool;

		// Token: 0x04007F35 RID: 32565
		private ElementObjectManager matNear;

		// Token: 0x04007F36 RID: 32566
		private ElementObjectManager matFar;

		// Token: 0x04007F37 RID: 32567
		private GameObject root;

		// Token: 0x04007F38 RID: 32568
		private GameObject card;

		// Token: 0x04007F39 RID: 32569
		private ChainedBezierMotion motion;

		// Token: 0x04007F3A RID: 32570
		private Vector3 fromPosition;

		// Token: 0x04007F3B RID: 32571
		private Vector3 toPosition;

		// Token: 0x04007F3C RID: 32572
		private Quaternion fromRotation;

		// Token: 0x04007F3D RID: 32573
		private Quaternion toRotation;

		// Token: 0x04007F3E RID: 32574
		private Vector3 fromScale;

		// Token: 0x04007F3F RID: 32575
		private Vector3 toScale;

		// Token: 0x04007F40 RID: 32576
		private SummonEffectBase summonEffect;

		// Token: 0x04007F41 RID: 32577
		private CardMoveTest.PlayStatus playStatus;

		// Token: 0x04007F42 RID: 32578
		private float time;

		// Token: 0x020008B2 RID: 2226
		private enum PlayStatus
		{
			// Token: 0x04007F44 RID: 32580
			Idle,
			// Token: 0x04007F45 RID: 32581
			Load,
			// Token: 0x04007F46 RID: 32582
			SummonEffect,
			// Token: 0x04007F47 RID: 32583
			MonsterCutin,
			// Token: 0x04007F48 RID: 32584
			CardMove
		}
	}
}

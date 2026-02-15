using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Card;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000CEB RID: 3307
	public class CardPlane : MonoBehaviour
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EAA RID: 24234 RVA: 0x0000216D File Offset: 0x0000036D
		public Transform offset
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

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EAC RID: 24236 RVA: 0x0000216D File Offset: 0x0000036D
		public Transform turn
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

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x0000216D File Offset: 0x0000036D
		public bool useFloatingPivot
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x0000216D File Offset: 0x0000036D
		public bool floatingPivotPosition
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x0000216D File Offset: 0x0000036D
		public bool floatingPivotRotation
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x0000216A File Offset: 0x0000036A
		private BezierMotionSetting flipMotionCard
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x0000216A File Offset: 0x0000036A
		private BezierMotionSetting flipMotionPlateMonster
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x0000216A File Offset: 0x0000036A
		private BezierMotionSetting flipMotionPlateMagic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06005EB6 RID: 24246 RVA: 0x0000216A File Offset: 0x0000036A
		private BezierMotionSetting flipMotionDeckCard
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTerminated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06005EB8 RID: 24248 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EB9 RID: 24249 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot cardRoot
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

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005EBB RID: 24251 RVA: 0x0000216D File Offset: 0x0000036D
		public CardModel cardModel
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

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06005EBC RID: 24252 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005EBD RID: 24253 RVA: 0x0000216D File Offset: 0x0000036D
		public bool showing
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardRoot.ModelType modelType
		{
			get
			{
				return (CardRoot.ModelType)0;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoaded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPlane Create(CardRoot cardRoot, GameObject go)
		{
			return null;
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x0000216D File Offset: 0x0000036D
		public void FlipTurn(bool isFace, bool isAttack, bool immediate, bool deckFlip, Action onFinished)
		{
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x0000216D File Offset: 0x0000036D
		public void CrearContents()
		{
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeModel(CardRoot.ModelType modelType)
		{
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispModelCard(bool disp)
		{
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTextures(int cardId, int sleeveId, int uniqueId, int styleId, Action onLoaded = null)
		{
		}

		// Token: 0x06005ECA RID: 24266 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReloadTexture(int cardId, int uniqueId, Action onLoaded = null)
		{
		}

		// Token: 0x06005ECB RID: 24267 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqHighlight(bool enable, SharedDefinition.ActivateAura type, int order = 0)
		{
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetHighlightEffectOrderOffset(int offset)
		{
		}

		// Token: 0x06005ECD RID: 24269 RVA: 0x000029CC File Offset: 0x00000BCC
		public static DuelEffectPool.Type TrailTypeToEffectType(CardPlane.MoveTrailType trailType)
		{
			return (DuelEffectPool.Type)0;
		}

		// Token: 0x06005ECE RID: 24270 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqMoveEffect(DuelEffectPool.Type eff_type, bool perisitent_vision = false)
		{
		}

		// Token: 0x06005ECF RID: 24271 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqStopMoveEffect()
		{
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqAppealEffect(Action on_finished)
		{
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x000F521C File Offset: 0x000F341C
		public static ValueTuple<bool, DuelEffectPool.Type, string, string> GetBrokenEffectInfo(CardPlane.BrokenType brokenType)
		{
			return default(ValueTuple<bool, DuelEffectPool.Type, string, string>);
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupBackInsight(bool force = false)
		{
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x0000216D File Offset: 0x0000036D
		private void IdleStep()
		{
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x0000216D File Offset: 0x0000036D
		private void FlipTurnStep()
		{
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTransition(float clampedTime)
		{
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIdlingType(CardPlane.IdlingType idling_type, SharedDefinition.Location location)
		{
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayTween(string label, bool stop_all = true)
		{
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePivot(bool reset = false)
		{
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetPivotPosition()
		{
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCastShadow(bool isOn)
		{
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x0000216D File Offset: 0x0000036D
		public void Shake(Vector3 groundZero)
		{
		}

		// Token: 0x06005EDD RID: 24285 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopShake()
		{
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAttackReady()
		{
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x0000216D File Offset: 0x0000036D
		public void FinishAttackReady()
		{
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopTweens()
		{
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetPosition()
		{
		}

		// Token: 0x040099FE RID: 39422
		private CardPlane.Step step;

		// Token: 0x040099FF RID: 39423
		private float time;

		// Token: 0x04009A00 RID: 39424
		private Vector3 fromPosition;

		// Token: 0x04009A01 RID: 39425
		private Vector3 toPosition;

		// Token: 0x04009A02 RID: 39426
		private Quaternion fromRotationModel;

		// Token: 0x04009A03 RID: 39427
		private Quaternion toRotationModel;

		// Token: 0x04009A04 RID: 39428
		private Quaternion fromRotationTurn;

		// Token: 0x04009A05 RID: 39429
		private Quaternion toRotationTurn;

		// Token: 0x04009A06 RID: 39430
		private Action onFinished;

		// Token: 0x04009A07 RID: 39431
		private SimpleEffect highlightEff;

		// Token: 0x04009A08 RID: 39432
		private int highlightEffectOrder;

		// Token: 0x04009A09 RID: 39433
		private int highlightEffectOrderOffset;

		// Token: 0x04009A0A RID: 39434
		private SimpleEffect moveTrail;

		// Token: 0x04009A0B RID: 39435
		private Transform pivot;

		// Token: 0x04009A0C RID: 39436
		private DuelEffectPool.Type auraEffType;

		// Token: 0x04009A0D RID: 39437
		private Vector3 currentPivotPosition;

		// Token: 0x04009A0E RID: 39438
		private Quaternion currentPivotRotation;

		// Token: 0x04009A0F RID: 39439
		private bool insight;

		// Token: 0x04009A10 RID: 39440
		private CardPlane.IdlingType idlingType;

		// Token: 0x04009A11 RID: 39441
		private SharedDefinition.Location location;

		// Token: 0x04009A12 RID: 39442
		private BezierMotionSetting flipMotion;

		// Token: 0x04009A13 RID: 39443
		private BezierMotionSetting _flipMotionCard;

		// Token: 0x04009A14 RID: 39444
		private BezierMotionSetting _flipMotionPlateMonster;

		// Token: 0x04009A15 RID: 39445
		private BezierMotionSetting _flipMotionPlateMagic;

		// Token: 0x04009A16 RID: 39446
		private BezierMotionSetting _flipMotionDeckCard;

		// Token: 0x04009A17 RID: 39447
		private Tween[] tweens;

		// Token: 0x04009A18 RID: 39448
		private TweenPosition tweenShakePosition;

		// Token: 0x04009A19 RID: 39449
		private TweenRotation tweenShakeRotation;

		// Token: 0x04009A1A RID: 39450
		private TweenPositionTo tweenAttackReadyStart;

		// Token: 0x04009A1B RID: 39451
		private TweenPosition tweenAttackReadyIdle;

		// Token: 0x04009A1C RID: 39452
		private TweenPositionTo tweenAttackReadyFinish;

		// Token: 0x04009A1D RID: 39453
		private const string tweenLabelIdlingDefault = "default";

		// Token: 0x04009A1E RID: 39454
		private const string tweenLabelIdlingNearHandSelecting = "near_hand_selecting";

		// Token: 0x04009A1F RID: 39455
		private const string tweenLabelIdlingNearHandDeciding = "near_hand_deciding";

		// Token: 0x04009A20 RID: 39456
		private const string tweenLabelIdlingFarHandSelecting = "far_hand_selecting";

		// Token: 0x04009A21 RID: 39457
		private const string tweenLabelIdlingFarHandDeciding = "far_hand_deciding";

		// Token: 0x04009A22 RID: 39458
		private const string tweenLabelIdlingFieldSelecting = "field_selecting";

		// Token: 0x04009A23 RID: 39459
		private const string tweenLabelIdlingFieldDeciding = "field_deciding";

		// Token: 0x04009A24 RID: 39460
		private const string tweenLabelHandAppealEffect = "hand_appeal";

		// Token: 0x02000CEC RID: 3308
		private enum Step
		{
			// Token: 0x04009A26 RID: 39462
			Idle,
			// Token: 0x04009A27 RID: 39463
			FlipTurn
		}

		// Token: 0x02000CED RID: 3309
		public enum IdlingType
		{
			// Token: 0x04009A29 RID: 39465
			Default,
			// Token: 0x04009A2A RID: 39466
			HandSelecting,
			// Token: 0x04009A2B RID: 39467
			HandDeciding,
			// Token: 0x04009A2C RID: 39468
			FieldSelecting,
			// Token: 0x04009A2D RID: 39469
			FieldDeciding
		}

		// Token: 0x02000CEE RID: 3310
		public enum BrokenType
		{
			// Token: 0x04009A2F RID: 39471
			Break,
			// Token: 0x04009A30 RID: 39472
			Explosion
		}

		// Token: 0x02000CEF RID: 3311
		public enum MoveTrailType
		{
			// Token: 0x04009A32 RID: 39474
			None,
			// Token: 0x04009A33 RID: 39475
			Normal,
			// Token: 0x04009A34 RID: 39476
			Break,
			// Token: 0x04009A35 RID: 39477
			Fusion,
			// Token: 0x04009A36 RID: 39478
			Attack,
			// Token: 0x04009A37 RID: 39479
			Exclude,
			// Token: 0x04009A38 RID: 39480
			BreakExclude,
			// Token: 0x04009A39 RID: 39481
			Release,
			// Token: 0x04009A3A RID: 39482
			ReleaseExclude,
			// Token: 0x04009A3B RID: 39483
			XyzMaterial
		}
	}
}

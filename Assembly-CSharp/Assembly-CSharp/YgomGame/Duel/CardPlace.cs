using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CE8 RID: 3304
	public abstract class CardPlace
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool incoming
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool outgoing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E6A RID: 24170 RVA: 0x0000216D File Offset: 0x0000036D
		public bool turning
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

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool cardMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06005E6C RID: 24172 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E6D RID: 24173 RVA: 0x0000216D File Offset: 0x0000036D
		public int incomingFromPosition
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06005E6E RID: 24174 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual int loadStartIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06005E6F RID: 24175 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual int loadIndexIncValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06005E70 RID: 24176 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool loadIsOver
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06005E71 RID: 24177 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005E72 RID: 24178 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPrepared
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isStatusVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool UpdateInitialize()
		{
			return false;
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool UpdateTerminate()
		{
			return false;
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual CardLocator GetCardLocator(int index, bool create, bool insert)
		{
			return null;
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardLocator(int index, CardLocator cardLocator)
		{
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdateLocators()
		{
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void SetupCardLocator(CardLocator cardLocator)
		{
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual CardRoot GetCardRoot(int index)
		{
			return null;
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual CardLocator OnEnter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool OnLeave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return false;
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnUnregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x000F51BC File Offset: 0x000F33BC
		public virtual Vector3 GetScreenPos(int index, Vector2 ofsRate)
		{
			return default(Vector3);
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x00002739 File Offset: 0x00000939
		public CardPlace(DuelFieldBase duelField, int team, int position)
		{
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Terminate()
		{
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0000216A File Offset: 0x0000036A
		public CardLocator Enter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0000216D File Offset: 0x0000036D
		public void Leave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0000216D File Offset: 0x0000036D
		public void Register(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0000216D File Offset: 0x0000036D
		public void Unregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddOnFinishedCardMove(Action onFinished)
		{
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitLoadCardStep()
		{
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x0000216D File Offset: 0x0000036D
		public void PrepareToDuel()
		{
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnPrepareToDuel(bool startAtZero, Action onFinished)
		{
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowUp(bool playEffect, Action onFinished)
		{
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnShowUp(bool playEffect, Action onFinished)
		{
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x000F51D4 File Offset: 0x000F33D4
		public virtual Vector3 GetTypicalPos()
		{
			return default(Vector3);
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x000F51EC File Offset: 0x000F33EC
		public virtual Quaternion GetTypicalRot()
		{
			return default(Quaternion);
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x000F5204 File Offset: 0x000F3404
		public virtual Vector3 GetTypicalScale()
		{
			return default(Vector3);
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x0000216D File Offset: 0x0000036D
		public void Shuffle(Action onFinished)
		{
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ShuffleImpl(Action onFinished)
		{
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void HighlightIfAvailable(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqHighlight(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ReqHighlightImpl(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x000029CC File Offset: 0x00000BCC
		protected SharedDefinition.ActivateAura GetActivateAura(int index, uint activeCommandBit)
		{
			return SharedDefinition.ActivateAura.None;
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x0000216D File Offset: 0x0000036D
		public void FlipTurn(int index, bool isFace, bool isAttack, bool immediate, Action onFinished)
		{
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void FlipTurnImpl(int index, bool isFace, bool isAttack, bool immediate, Action onFinished)
		{
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void FlipTurnStartImpl(CardRoot cardRoot, bool isFace)
		{
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqDecideEffect(int index, Action onFinished)
		{
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ReqDecideEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndSacrificeTargetEffect(int index, Action onFinished)
		{
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void EndSacrificeTargetEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void StartAffectRelativeEffect(Engine.AffectType affectType, Action onFinished)
		{
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void StartAffectRelativeEffectImpl(Engine.AffectType affectType, Action onFinished)
		{
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void EndAffectEffect(Action onFinished)
		{
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void EndAffectEffectImpl(Action onFinished)
		{
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateState(Action onFinished)
		{
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdateStateImpl(Action onFinished)
		{
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnSelected()
		{
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnDeselected()
		{
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int GetIndexByViewIndex(int viewIndex)
		{
			return 0;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int GetViewIndex(int index)
		{
			return 0;
		}

		// Token: 0x040099E6 RID: 39398
		public DuelFieldBase duelField;

		// Token: 0x040099E7 RID: 39399
		public int team;

		// Token: 0x040099E8 RID: 39400
		public int position;

		// Token: 0x040099E9 RID: 39401
		protected Dictionary<int, CardLocator> cardLocators;

		// Token: 0x040099EA RID: 39402
		private CardPlace.Step step;

		// Token: 0x040099EB RID: 39403
		private SimpleEffect affectTargetEff;

		// Token: 0x040099EC RID: 39404
		private SimpleEffect affectRelativeEff;

		// Token: 0x040099ED RID: 39405
		protected int currentLoadIdx;

		// Token: 0x040099EE RID: 39406
		protected int numLoadCards;

		// Token: 0x040099EF RID: 39407
		private int numIncomings;

		// Token: 0x040099F0 RID: 39408
		private int numOutgoings;

		// Token: 0x040099F1 RID: 39409
		private Action onFinishedCardMove;

		// Token: 0x040099F2 RID: 39410
		private Action onFinishedPrepare;

		// Token: 0x040099F3 RID: 39411
		private Dictionary<Engine.CommandType, CardPlace.CommandPower> commandPowerList;

		// Token: 0x040099F4 RID: 39412
		protected CardLocator typicalLocator;

		// Token: 0x02000CE9 RID: 3305
		private enum Step
		{
			// Token: 0x040099F6 RID: 39414
			Idle,
			// Token: 0x040099F7 RID: 39415
			InitLoadCard,
			// Token: 0x040099F8 RID: 39416
			WaitLoadCard
		}

		// Token: 0x02000CEA RID: 3306
		private enum CommandPower
		{
			// Token: 0x040099FA RID: 39418
			None,
			// Token: 0x040099FB RID: 39419
			Low,
			// Token: 0x040099FC RID: 39420
			Middle,
			// Token: 0x040099FD RID: 39421
			High
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D27 RID: 3367
	public class DrawOperation
	{
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060061A6 RID: 24998 RVA: 0x0000216D File Offset: 0x0000036D
		public bool finished
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

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060061A7 RID: 24999 RVA: 0x000F5604 File Offset: 0x000F3804
		private Vector3 cardPosTo
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060061A8 RID: 25000 RVA: 0x000F561C File Offset: 0x000F381C
		private Quaternion cardRotTo
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060061A9 RID: 25001 RVA: 0x000F5634 File Offset: 0x000F3834
		private Vector3 deckPosTo
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x000F564C File Offset: 0x000F384C
		private Quaternion deckRotTo
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x000F5664 File Offset: 0x000F3864
		private Vector3 deckScaleTo
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x0000216A File Offset: 0x0000036A
		public static DrawOperation Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsBusyEffect(Engine.ViewType viewType)
		{
			return false;
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x0000216D File Offset: 0x0000036D
		public void WaitInput(int param1, int param2, int param3)
		{
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x0000216D File Offset: 0x0000036D
		private void DrawCommand()
		{
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0000216D File Offset: 0x0000036D
		public void CardMove(int param1, int param2, int param3)
		{
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitPlaceToReadyStep()
		{
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitStep()
		{
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveDeckStep()
		{
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStep()
		{
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepInit()
		{
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepNeutral()
		{
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepInitDrawCardCenterFirst()
		{
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepWaitDrawCardCenterFirst()
		{
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepWaitDrawCardCenterLatter()
		{
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepWaitDetail()
		{
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x0000216D File Offset: 0x0000036D
		private void TouchableStepFinish()
		{
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateDeckBack()
		{
			return false;
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitBackStep()
		{
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x0000216D File Offset: 0x0000036D
		public void Abort()
		{
		}

		// Token: 0x04009C8D RID: 40077
		private RunEffectWorker worker;

		// Token: 0x04009C8E RID: 40078
		private DrawOperation.Step step;

		// Token: 0x04009C8F RID: 40079
		private Engine.CardStatus fromStatus;

		// Token: 0x04009C90 RID: 40080
		private Engine.CardStatus toStatus;

		// Token: 0x04009C91 RID: 40081
		private CardPlace fromPlace;

		// Token: 0x04009C92 RID: 40082
		private CardPlace toPlace;

		// Token: 0x04009C93 RID: 40083
		private int uniqueID;

		// Token: 0x04009C94 RID: 40084
		private bool isFace;

		// Token: 0x04009C95 RID: 40085
		private int player;

		// Token: 0x04009C96 RID: 40086
		private int team;

		// Token: 0x04009C97 RID: 40087
		private DrawOperation.TouchPhase phase;

		// Token: 0x04009C98 RID: 40088
		private DeckCardPlace deckPlace;

		// Token: 0x04009C99 RID: 40089
		private CardRoot drawCard;

		// Token: 0x04009C9A RID: 40090
		private Vector3 defaultPos;

		// Token: 0x04009C9B RID: 40091
		private Quaternion defaultRot;

		// Token: 0x04009C9C RID: 40092
		private float camDist;

		// Token: 0x04009C9D RID: 40093
		private SimpleEffect drawArrowEff;

		// Token: 0x04009C9E RID: 40094
		private bool calledCardMove;

		// Token: 0x04009C9F RID: 40095
		private float frameOutTime;

		// Token: 0x04009CA0 RID: 40096
		private bool isScreenOperated;

		// Token: 0x04009CA1 RID: 40097
		private float time;

		// Token: 0x04009CA2 RID: 40098
		private ScreenSelector screenSelector;

		// Token: 0x04009CA3 RID: 40099
		private SelectionButton shortCutKey;

		// Token: 0x04009CA4 RID: 40100
		private ChainedBezierMotion deckCloseUpMotion;

		// Token: 0x04009CA5 RID: 40101
		private ChainedBezierMotion deckBackMotion;

		// Token: 0x04009CA6 RID: 40102
		private ChainedBezierMotion drawMotionFirst;

		// Token: 0x04009CA7 RID: 40103
		private ChainedBezierMotion drawMotionLatter;

		// Token: 0x04009CA8 RID: 40104
		private ChainedBezierMotion toHandMotion;

		// Token: 0x04009CA9 RID: 40105
		private Vector2 dragDirection;

		// Token: 0x04009CAA RID: 40106
		private const float neutralPhaseTimeLimit = 10f;

		// Token: 0x04009CAB RID: 40107
		private const float waitDetailPhaseTimeLimit = 10f;

		// Token: 0x04009CAC RID: 40108
		private SimpleEffect limitedEffectImpact;

		// Token: 0x02000D28 RID: 3368
		private enum Step
		{
			// Token: 0x04009CAE RID: 40110
			WaitPlaceToReady,
			// Token: 0x04009CAF RID: 40111
			WaitCardMove,
			// Token: 0x04009CB0 RID: 40112
			Init,
			// Token: 0x04009CB1 RID: 40113
			MoveDeck,
			// Token: 0x04009CB2 RID: 40114
			Touchable,
			// Token: 0x04009CB3 RID: 40115
			WaitBack,
			// Token: 0x04009CB4 RID: 40116
			Finish
		}

		// Token: 0x02000D29 RID: 3369
		private enum TouchPhase
		{
			// Token: 0x04009CB6 RID: 40118
			Init,
			// Token: 0x04009CB7 RID: 40119
			Neutral,
			// Token: 0x04009CB8 RID: 40120
			InitDrawCardCenterFirst,
			// Token: 0x04009CB9 RID: 40121
			WaitDrawCardCenterFirst,
			// Token: 0x04009CBA RID: 40122
			InitDrawCardCenterLatter,
			// Token: 0x04009CBB RID: 40123
			WaitDrawCardCenterLatter,
			// Token: 0x04009CBC RID: 40124
			WaitDetail,
			// Token: 0x04009CBD RID: 40125
			Finish
		}
	}
}

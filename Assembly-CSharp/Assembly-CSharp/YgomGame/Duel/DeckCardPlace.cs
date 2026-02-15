using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D22 RID: 3362
	public class DeckCardPlace : CardPlace
	{
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600614D RID: 24909 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600614E RID: 24910 RVA: 0x0000216D File Offset: 0x0000036D
		public int localTopCardIndex
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006150 RID: 24912 RVA: 0x0000216D File Offset: 0x0000036D
		public int localCardNum
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

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x000029CC File Offset: 0x00000BCC
		private int defaultSleeveID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06006152 RID: 24914 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006153 RID: 24915 RVA: 0x0000216D File Offset: 0x0000036D
		public SharedDefinition.Location location
		{
			[CompilerGenerated]
			get
			{
				return SharedDefinition.Location.Near;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06006154 RID: 24916 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006155 RID: 24917 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject anchor
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06006156 RID: 24918 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006157 RID: 24919 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject box
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06006158 RID: 24920 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006159 RID: 24921 RVA: 0x0000216D File Offset: 0x0000036D
		public MeshRenderer boxBeforeTopRenderer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600615A RID: 24922 RVA: 0x0000216A File Offset: 0x0000036A
		private MeshRenderer boxFirstTopRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600615B RID: 24923 RVA: 0x0000216A File Offset: 0x0000036A
		private MeshRenderer boxSecondTopRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x0000216A File Offset: 0x0000036A
		private MeshRenderer boxThirdTopRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x0000216A File Offset: 0x0000036A
		private MeshRenderer boxForthTopRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600615E RID: 24926 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600615F RID: 24927 RVA: 0x0000216D File Offset: 0x0000036D
		public MeshRenderer boxAfterTopRenderer
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06006160 RID: 24928 RVA: 0x0000216A File Offset: 0x0000036A
		public MeshRenderer boxTopRenderer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006162 RID: 24930 RVA: 0x0000216D File Offset: 0x0000036D
		public CardRoot top
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

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float deckHeightScaler
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float adjustedDeckHeightScaler
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x000029CC File Offset: 0x00000BCC
		private int indexToDownValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06006166 RID: 24934 RVA: 0x000F5544 File Offset: 0x000F3744
		// (set) Token: 0x06006167 RID: 24935 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 posOfNumCardsStatus
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06006168 RID: 24936 RVA: 0x000029CC File Offset: 0x00000BCC
		public int bottomIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x000F555C File Offset: 0x000F375C
		private Vector3 highLightEffLocalScale
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x000F4E3E File Offset: 0x000F303E
		public DeckCardPlace(DuelFieldBase duelField, int team, int position, GameObject anchor, string name, SharedDefinition.Location location)
			: base(null, 0, 0)
		{
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateInitialize()
		{
			return false;
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool UpdateTerminate()
		{
			return false;
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPrepareToDuel(bool startAtZero, Action onFinished)
		{
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnShowUp(bool playEffect, Action onFinished)
		{
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0000216A File Offset: 0x0000036A
		public override CardLocator GetCardLocator(int index, bool create, bool insert)
		{
			return null;
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShuffleImpl(Action onFinished)
		{
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRegister(CardRoot cardRoot, int index, bool withEffect)
		{
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUnregister(CardRoot cardRoot, int index)
		{
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnLeave(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return false;
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0000216A File Offset: 0x0000036A
		protected override CardLocator OnEnter(CardRoot cardRoot, int index, bool reqUpdateIndices)
		{
			return null;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePopUpText()
		{
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0000216A File Offset: 0x0000036A
		private CardRoot LoadCardRootToLocator(CardLocator cardLocator, int cardID, int uniqueID, bool isFace, int sleeveID = -1)
		{
			return null;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqHighlightImpl(bool available, uint cmdBit, Action onFinished)
		{
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispHighlightEffect(bool disp)
		{
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ReqDecideEffectImpl(int index, Action onFinished)
		{
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void FlipTurnStartImpl(CardRoot cardRoot, bool isFace)
		{
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnUpdate()
		{
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x000F5574 File Offset: 0x000F3774
		public override Vector3 GetScreenPos(int index, Vector2 ofsRate)
		{
			return default(Vector3);
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x000F558C File Offset: 0x000F378C
		public DeckCardPlace.Trans3D GetDefaultAnchorTransform()
		{
			return default(DeckCardPlace.Trans3D);
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetAnchorTransform()
		{
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBeforeTopTexture(Texture tex)
		{
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetSleeveID(int uniqueID)
		{
			return 0;
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLocatorIndices()
		{
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0000216D File Offset: 0x0000036D
		public void SyncToEngine(Dictionary<string, object> savedEngineParams, Action onFinished, int num = 0)
		{
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartReverseStep()
		{
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitReverseStep()
		{
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartLoadCardStep()
		{
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitLoad1stCardStep()
		{
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishSyncStep()
		{
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitShuffleDeckStep()
		{
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitShuffleBeforeReverseStep()
		{
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartShuffleStep()
		{
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitShuffleStep()
		{
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitEndShuffleStep()
		{
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitShuffleAfterReverseStep()
		{
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x0000216D File Offset: 0x0000036D
		private void TermShuffleStep()
		{
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateLocator()
		{
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateParts(bool reqReloadTex = true)
		{
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(Vector3 pos)
		{
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x000F55A4 File Offset: 0x000F37A4
		public Vector3 Hide(bool lethalEffect = false)
		{
			return default(Vector3);
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowStatusLabel(bool immediate, bool showDetail)
		{
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideStatusLabel(bool immediate, bool finishShowDetail = false)
		{
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInputEnabled(bool enabled)
		{
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBoxTopTexture()
		{
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject DuplicateShuffleDeck()
		{
			return null;
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x000F55BC File Offset: 0x000F37BC
		public override Vector3 GetTypicalPos()
		{
			return default(Vector3);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x000F55D4 File Offset: 0x000F37D4
		private Vector3 GetScaleY()
		{
			return default(Vector3);
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnSelected()
		{
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDeselected()
		{
		}

		// Token: 0x04009C58 RID: 40024
		protected CardLocator cardLocator1st;

		// Token: 0x04009C59 RID: 40025
		private DeckCardPlace.Step step;

		// Token: 0x04009C5A RID: 40026
		private DeckCardPlace.ShuffleStep shuffleStep;

		// Token: 0x04009C5B RID: 40027
		private string name;

		// Token: 0x04009C5C RID: 40028
		private global::UnityEngine.Object srcObject;

		// Token: 0x04009C5D RID: 40029
		private SimpleEffect highlightEff;

		// Token: 0x04009C5E RID: 40030
		private Action onFinishedSync;

		// Token: 0x04009C5F RID: 40031
		private DeckPlaceStatus deckStatus;

		// Token: 0x04009C60 RID: 40032
		private bool showDetailStatus;

		// Token: 0x04009C61 RID: 40033
		private List<MeshRenderer> boxRenderers;

		// Token: 0x04009C62 RID: 40034
		private DeckCardPlace.Trans3D defaultAnchorTrans;

		// Token: 0x04009C63 RID: 40035
		private Vector3 defaultAnchorPos;

		// Token: 0x04009C64 RID: 40036
		private Quaternion defaultAnchorRot;

		// Token: 0x04009C65 RID: 40037
		private GameObject shuffleDeck;

		// Token: 0x04009C66 RID: 40038
		private Transform root;

		// Token: 0x04009C67 RID: 40039
		private Action onFinishedShuffle;

		// Token: 0x04009C68 RID: 40040
		private Animator boxAnim;

		// Token: 0x04009C69 RID: 40041
		private Queue<float> seQueue;

		// Token: 0x04009C6A RID: 40042
		private float time;

		// Token: 0x04009C6B RID: 40043
		private bool isSync;

		// Token: 0x04009C6C RID: 40044
		private bool reverse;

		// Token: 0x04009C6D RID: 40045
		private int topSleeveID;

		// Token: 0x04009C6E RID: 40046
		private int topCardID;

		// Token: 0x04009C6F RID: 40047
		private int topUniqueID;

		// Token: 0x04009C70 RID: 40048
		private bool topFace;

		// Token: 0x04009C71 RID: 40049
		private ChainedBezierMotion reverseMotion;

		// Token: 0x04009C72 RID: 40050
		private float reverseTime;

		// Token: 0x04009C73 RID: 40051
		private const int invalidIndex = -1;

		// Token: 0x04009C74 RID: 40052
		private static readonly Vector3 cardOfsOfOne;

		// Token: 0x04009C75 RID: 40053
		private static readonly int anim_Shuffle;

		// Token: 0x04009C76 RID: 40054
		private static readonly int anim_Idle;

		// Token: 0x04009C77 RID: 40055
		private bool effectActivation;

		// Token: 0x04009C78 RID: 40056
		private bool lethalEffectPlayed;

		// Token: 0x04009C79 RID: 40057
		private const string deckModelResPath = "Duel/Models/DeckModelWrapper";

		// Token: 0x02000D23 RID: 3363
		private enum Step
		{
			// Token: 0x04009C7B RID: 40059
			Idle,
			// Token: 0x04009C7C RID: 40060
			WaitReverse,
			// Token: 0x04009C7D RID: 40061
			WaitLoad1stCard,
			// Token: 0x04009C7E RID: 40062
			FinishSync
		}

		// Token: 0x02000D24 RID: 3364
		private enum ShuffleStep
		{
			// Token: 0x04009C80 RID: 40064
			Idle,
			// Token: 0x04009C81 RID: 40065
			InitShuffleDeck,
			// Token: 0x04009C82 RID: 40066
			WaitShuffleBeforeReverse,
			// Token: 0x04009C83 RID: 40067
			StartShuffle,
			// Token: 0x04009C84 RID: 40068
			WaitShuffle,
			// Token: 0x04009C85 RID: 40069
			WaitEndShuffle,
			// Token: 0x04009C86 RID: 40070
			WaitShuffleAfterReverse,
			// Token: 0x04009C87 RID: 40071
			TermShuffle
		}

		// Token: 0x02000D25 RID: 3365
		public struct Trans3D
		{
			// Token: 0x04009C88 RID: 40072
			public Vector3 position;

			// Token: 0x04009C89 RID: 40073
			public Quaternion rotation;

			// Token: 0x04009C8A RID: 40074
			public Vector3 scale;
		}
	}
}

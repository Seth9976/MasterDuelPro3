using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EFB RID: 3835
	public class SelectStandOperation
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06007102 RID: 28930 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007103 RID: 28931 RVA: 0x0000216D File Offset: 0x0000036D
		public static SelectStandOperation instance
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

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06007104 RID: 28932 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007105 RID: 28933 RVA: 0x0000216D File Offset: 0x0000036D
		public int player
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

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06007106 RID: 28934 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007107 RID: 28935 RVA: 0x0000216D File Offset: 0x0000036D
		public int position
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

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06007108 RID: 28936 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007109 RID: 28937 RVA: 0x0000216D File Offset: 0x0000036D
		public int index
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

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600710A RID: 28938 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600710B RID: 28939 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600710C RID: 28940 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600710D RID: 28941 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600710E RID: 28942 RVA: 0x0000216D File Offset: 0x0000036D
		public bool activate
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

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600710F RID: 28943 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007110 RID: 28944 RVA: 0x0000216D File Offset: 0x0000036D
		public bool draggable
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

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06007111 RID: 28945 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007112 RID: 28946 RVA: 0x0000216D File Offset: 0x0000036D
		public static int targetPlayer
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

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06007113 RID: 28947 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007114 RID: 28948 RVA: 0x0000216D File Offset: 0x0000036D
		public static int targetPosition
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

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06007115 RID: 28949 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007116 RID: 28950 RVA: 0x0000216D File Offset: 0x0000036D
		public static int targetIndex
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

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06007117 RID: 28951 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007118 RID: 28952 RVA: 0x0000216D File Offset: 0x0000036D
		public static uint targetStand
		{
			[CompilerGenerated]
			get
			{
				return 0U;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06007119 RID: 28953 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600711A RID: 28954 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool decideCommand
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

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600711B RID: 28955 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600711C RID: 28956 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool decideLocation
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

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600711D RID: 28957 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600711E RID: 28958 RVA: 0x0000216D File Offset: 0x0000036D
		private static bool onTarget
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

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600711F RID: 28959 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCommandDisp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06007120 RID: 28960 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool readyToSpSummon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06007121 RID: 28961 RVA: 0x000F6218 File Offset: 0x000F4418
		private Vector2 neutralScreenPosition
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectStandOperation Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(RunEffectWorker worker)
		{
		}

		// Token: 0x06007124 RID: 28964 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginSpSummon(SelectStandOperation.StandType standType, int uniqueID, Action<int, int, int, uint> onExecuteSpSummon, Vector2 screenPoint, SelectStandOperation.Status status, SelectStandOperation.ZoneMode zoneMode)
		{
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginDrag(Vector2 screenPoint)
		{
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDrag(Vector2 screenPoint)
		{
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardPosition(Vector2 screenPoint, float height, bool immediate)
		{
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x0000216D File Offset: 0x0000036D
		public void End()
		{
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool EndDrag(Vector2 screenPoint, SelectStandOperation.Status noTargetStatus = SelectStandOperation.Status.Neutral)
		{
			return false;
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStatus(SelectStandOperation.Status status)
		{
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAvailableZone(int player, int position)
		{
			return false;
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetTargetLocation(int player, int position, bool playDecideEffect = true, bool force = false)
		{
			return false;
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetCardColor(Color color)
		{
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTargetStand(uint stand)
		{
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelCommand()
		{
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ExecuteTargetSpSummon(bool force = false)
		{
			return false;
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelected(int player, int position, bool selected)
		{
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCursor(int index)
		{
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reset()
		{
		}

		// Token: 0x0400AAB8 RID: 43704
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400AAB9 RID: 43705
		private const string prefabPath = "Prefabs/Duel/UI/CommandOperation";

		// Token: 0x0400AABA RID: 43706
		private ElementObjectManager ui;

		// Token: 0x0400AABB RID: 43707
		private GhostCard dragCard;

		// Token: 0x0400AABC RID: 43708
		private SelectionButton dragCardButton;

		// Token: 0x0400AABD RID: 43709
		private GameObject operationObject;

		// Token: 0x0400AABE RID: 43710
		private SelectionButton bgButton;

		// Token: 0x0400AABF RID: 43711
		private CardCommandEx cardCommandEx;

		// Token: 0x0400AAC0 RID: 43712
		private bool usingInfoDialog;

		// Token: 0x0400AAC1 RID: 43713
		private RunEffectWorker worker;

		// Token: 0x0400AAC2 RID: 43714
		private SelectStandOperation.Status status;

		// Token: 0x0400AAC3 RID: 43715
		private Action<int, int, int, uint> onExecuteSpSummon;

		// Token: 0x0400AAC4 RID: 43716
		public SelectStandOperation.ZoneMode zoneMode;

		// Token: 0x0400AAC5 RID: 43717
		private SelectStandOperation.StandType standType;

		// Token: 0x0400AAC6 RID: 43718
		private int loadCount;

		// Token: 0x0400AAC7 RID: 43719
		private bool dragging;

		// Token: 0x0400AAC8 RID: 43720
		private uint directDraggedStand;

		// Token: 0x0400AAC9 RID: 43721
		private int cardID;

		// Token: 0x0400AACA RID: 43722
		private int uniqueID;

		// Token: 0x0400AACB RID: 43723
		private CardRoot targetCard;

		// Token: 0x0400AACC RID: 43724
		private CommandZoneIconController zoneIcon;

		// Token: 0x0400AACD RID: 43725
		private const float dragCardHeight = 18f;

		// Token: 0x0400AACE RID: 43726
		private const float dragCardHeightOnTarget = 1f;

		// Token: 0x02000EFC RID: 3836
		public enum Status
		{
			// Token: 0x0400AAD0 RID: 43728
			None,
			// Token: 0x0400AAD1 RID: 43729
			Neutral,
			// Token: 0x0400AAD2 RID: 43730
			Drag,
			// Token: 0x0400AAD3 RID: 43731
			OnTarget
		}

		// Token: 0x02000EFD RID: 3837
		public enum StandType
		{
			// Token: 0x0400AAD5 RID: 43733
			FaceAttackFaceDefense,
			// Token: 0x0400AAD6 RID: 43734
			FaceAttackBackDefense,
			// Token: 0x0400AAD7 RID: 43735
			FaceDefenseBackDefense,
			// Token: 0x0400AAD8 RID: 43736
			All,
			// Token: 0x0400AAD9 RID: 43737
			None
		}

		// Token: 0x02000EFE RID: 3838
		public enum ZoneMode
		{
			// Token: 0x0400AADB RID: 43739
			SelectStand,
			// Token: 0x0400AADC RID: 43740
			DecidePosition
		}
	}
}

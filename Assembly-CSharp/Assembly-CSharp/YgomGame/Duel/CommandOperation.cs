using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D19 RID: 3353
	public class CommandOperation
	{
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060060D7 RID: 24791 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060060D8 RID: 24792 RVA: 0x0000216D File Offset: 0x0000036D
		public static CommandOperation instance
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

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060060D9 RID: 24793 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060DA RID: 24794 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060DC RID: 24796 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060060DD RID: 24797 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060DE RID: 24798 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060060DF RID: 24799 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060060E0 RID: 24800 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060E1 RID: 24801 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060E3 RID: 24803 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060E5 RID: 24805 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060E7 RID: 24807 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060E9 RID: 24809 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060EB RID: 24811 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060060EC RID: 24812 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060ED RID: 24813 RVA: 0x0000216D File Offset: 0x0000036D
		public static Engine.CommandType targetCommand
		{
			[CompilerGenerated]
			get
			{
				return Engine.CommandType.Attack;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060EF RID: 24815 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060060F0 RID: 24816 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060F1 RID: 24817 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060060F2 RID: 24818 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060F3 RID: 24819 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060060F4 RID: 24820 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060060F5 RID: 24821 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCommandDisp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060060F7 RID: 24823 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool readyToCommandExecution
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x000F552C File Offset: 0x000F372C
		private Vector2 neutralScreenPosition
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommandOperation Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(RunEffectWorker worker)
		{
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool BeginCommand(int player, int position, int index, Action<int, int, int, Engine.CommandType> onExecuteCommand, Vector2 screenPoint, CommandOperation.Status status)
		{
			return false;
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool BeginCommand(uint commandMask, int player, int position, int index, Action<int, int, int, Engine.CommandType> onExecuteCommand, Vector2 screenPoint, CommandOperation.Status status)
		{
			return false;
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0000216D File Offset: 0x0000036D
		private void CommandToList(uint commandMask)
		{
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0000216D File Offset: 0x0000036D
		public void BeginDrag(Vector2 screenPoint)
		{
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCommandInteractable(int targetPlayer, int targetPosition)
		{
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsDragCommandMask(uint commandMask, bool isMonster)
		{
			return false;
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDrag(Vector2 screenPoint)
		{
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardPosition(Vector2 screenPoint, float height, bool immediate)
		{
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0000216D File Offset: 0x0000036D
		public void End(bool selectOpenedItem = true, bool closeCommand = true)
		{
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndDrag(Vector2 screenPoint, CommandOperation.Status noTargetStatus = CommandOperation.Status.Neutral)
		{
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStatus(CommandOperation.Status status)
		{
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAvailableZone(int player, int position)
		{
			return false;
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetTargetLocation(int player, int position)
		{
			return false;
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetCardColor(Color color)
		{
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTargetCommand(Engine.CommandType command)
		{
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ExecuteTargetCommand(bool force = false)
		{
			return false;
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelected(int player, int position, bool selected)
		{
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCursor(int index)
		{
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReapplyCommandPosition()
		{
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reset()
		{
		}

		// Token: 0x04009C1D RID: 39965
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04009C1E RID: 39966
		private const string prefabPath = "Prefabs/Duel/UI/CommandOperation";

		// Token: 0x04009C1F RID: 39967
		private ElementObjectManager ui;

		// Token: 0x04009C20 RID: 39968
		private GhostCard dragCard;

		// Token: 0x04009C21 RID: 39969
		private SelectionButton dragCardButton;

		// Token: 0x04009C22 RID: 39970
		private TargetingLine targetLine;

		// Token: 0x04009C23 RID: 39971
		private GameObject operationObject;

		// Token: 0x04009C24 RID: 39972
		private SelectionButton bgButton;

		// Token: 0x04009C25 RID: 39973
		private CardCommand cardCommand;

		// Token: 0x04009C26 RID: 39974
		private bool usingInfoDialog;

		// Token: 0x04009C27 RID: 39975
		private RunEffectWorker worker;

		// Token: 0x04009C28 RID: 39976
		private uint commandMask;

		// Token: 0x04009C29 RID: 39977
		private uint commandMaskOrigin;

		// Token: 0x04009C2A RID: 39978
		private CommandOperation.Status status;

		// Token: 0x04009C2B RID: 39979
		private Action<int, int, int, Engine.CommandType> onExecuteCommand;

		// Token: 0x04009C2C RID: 39980
		private int loadCount;

		// Token: 0x04009C2D RID: 39981
		private bool dragging;

		// Token: 0x04009C2E RID: 39982
		private int cardID;

		// Token: 0x04009C2F RID: 39983
		private CardRoot targetCard;

		// Token: 0x04009C30 RID: 39984
		private bool isMonsterCard;

		// Token: 0x04009C31 RID: 39985
		private List<Engine.CommandType> commandList;

		// Token: 0x04009C32 RID: 39986
		private CommandZoneIconController zoneIcon;

		// Token: 0x04009C33 RID: 39987
		private bool directDragging;

		// Token: 0x04009C34 RID: 39988
		private Engine.CommandType directDraggedCommand;

		// Token: 0x04009C35 RID: 39989
		private const float dragCardHeight = 3f;

		// Token: 0x04009C36 RID: 39990
		private const float dragCardHeightOnTarget = 1f;

		// Token: 0x02000D1A RID: 3354
		public enum Status
		{
			// Token: 0x04009C38 RID: 39992
			None,
			// Token: 0x04009C39 RID: 39993
			Neutral,
			// Token: 0x04009C3A RID: 39994
			Drag,
			// Token: 0x04009C3B RID: 39995
			OnTarget
		}
	}
}

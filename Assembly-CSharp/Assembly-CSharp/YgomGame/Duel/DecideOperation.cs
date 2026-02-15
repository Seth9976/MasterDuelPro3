using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D21 RID: 3361
	public class DecideOperation
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006135 RID: 24885 RVA: 0x0000216D File Offset: 0x0000036D
		public static DecideOperation instance
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

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006137 RID: 24887 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06006138 RID: 24888 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006139 RID: 24889 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600613A RID: 24890 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600613B RID: 24891 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600613C RID: 24892 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600613D RID: 24893 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600613E RID: 24894 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600613F RID: 24895 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006140 RID: 24896 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06006141 RID: 24897 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCommandDisp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x0000216A File Offset: 0x0000036A
		public static DecideOperation Create(RunEffectWorker worker)
		{
			return null;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(RunEffectWorker worker)
		{
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool BeginCommand(int player, int position, int index, Action onExecuteCommand, Vector2 screenPoint)
		{
			return false;
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0000216D File Offset: 0x0000036D
		public void End(bool selectOpenedItem = true)
		{
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecuteCommand()
		{
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExecuteDecideCommand()
		{
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelected(int player, int position, bool selected)
		{
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCursor(int index)
		{
		}

		// Token: 0x04009C4B RID: 40011
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04009C4C RID: 40012
		private const string prefabPath = "Prefabs/Duel/UI/DecideOperation";

		// Token: 0x04009C4D RID: 40013
		private ElementObjectManager ui;

		// Token: 0x04009C4E RID: 40014
		private GameObject operationObject;

		// Token: 0x04009C4F RID: 40015
		private SelectionButton bgButton;

		// Token: 0x04009C50 RID: 40016
		private CardCommand cardCommand;

		// Token: 0x04009C51 RID: 40017
		private RunEffectWorker worker;

		// Token: 0x04009C52 RID: 40018
		private Action onExecuteCommand;

		// Token: 0x04009C53 RID: 40019
		private int loadCount;

		// Token: 0x04009C54 RID: 40020
		private CardRoot targetCard;

		// Token: 0x04009C55 RID: 40021
		private bool isCancelButtonActive;

		// Token: 0x04009C56 RID: 40022
		private bool isDecisionButtonActive;

		// Token: 0x04009C57 RID: 40023
		private CommandZoneIconController zoneIcon;
	}
}

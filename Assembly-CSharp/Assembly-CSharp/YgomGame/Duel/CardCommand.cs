using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000CB3 RID: 3251
	public class CardCommand : MonoBehaviour
	{
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005C8C RID: 23692 RVA: 0x0000216D File Offset: 0x0000036D
		public int cmdPlayer
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06005C8D RID: 23693 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005C8E RID: 23694 RVA: 0x0000216D File Offset: 0x0000036D
		public int cmdPosition
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005C90 RID: 23696 RVA: 0x0000216D File Offset: 0x0000036D
		public int cmdIndex
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

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06005C91 RID: 23697 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C92 RID: 23698 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<bool> OnClose
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06005C93 RID: 23699 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C94 RID: 23700 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<Engine.CommandType> onExecuteCommand
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06005C95 RID: 23701 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C96 RID: 23702 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<int, Vector2> onDragBegin
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C98 RID: 23704 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<int, Vector2> onDragging
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06005C99 RID: 23705 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005C9A RID: 23706 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<int, Vector2> onDragEnd
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005C9C RID: 23708 RVA: 0x0000216D File Offset: 0x0000036D
		public bool opening
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

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005C9E RID: 23710 RVA: 0x0000216D File Offset: 0x0000036D
		public int selectorPriority
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

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x000F4FDC File Offset: 0x000F31DC
		private Vector2 commandSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06005CA0 RID: 23712 RVA: 0x000F4FF4 File Offset: 0x000F31F4
		private Vector2 commandSizeRatio
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, DuelClient host, Action<CardCommand> onLoaded)
		{
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelClient host)
		{
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x0000216D File Offset: 0x0000036D
		public void Term()
		{
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Open(uint command, int player, int position, int index, Vector2 screenPoint, Action affectButtonCallback)
		{
			return false;
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCommand(uint commandMask)
		{
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDefaultPosition()
		{
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x0000216D File Offset: 0x0000036D
		public void FixedPositionMode()
		{
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCommand(int index)
		{
			return false;
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCommand(Engine.CommandType command)
		{
			return false;
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x0000216D File Offset: 0x0000036D
		public void AlphaChange(bool setAlpha)
		{
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetButtonInteractable(Engine.CommandType command, bool interactable)
		{
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetButtonInteractable(int index, bool interactable)
		{
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAllButtonInteractable(bool interactable)
		{
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x000F500C File Offset: 0x000F320C
		public Vector3 SetPosition(Vector2 screenPoint)
		{
			return default(Vector3);
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetComBaseLocalPosition(Vector3 localPosition)
		{
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetComGridLocalPosition(Vector3 localPosition)
		{
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x000F5024 File Offset: 0x000F3224
		public Vector3 ReapplyPosition()
		{
			return default(Vector3);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGridPositionHand()
		{
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGridPositionField()
		{
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveCommandBG(bool active)
		{
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCommand(int index)
		{
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCommand(Engine.CommandType command)
		{
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAffectButton()
		{
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x000029CC File Offset: 0x00000BCC
		public Engine.CommandType GetCurrentCommand()
		{
			return Engine.CommandType.Attack;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x0000216D File Offset: 0x0000036D
		private void ComDoCommand(Engine.CommandType command)
		{
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectorPriority(SharedDefinition.DuelSelectorPriority priority = SharedDefinition.DuelSelectorPriority.HUD, int priorityInCluster = 7)
		{
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x0000216A File Offset: 0x0000036A
		private CardCommand.CommandSetting GetSetting(Engine.CommandType commandType, bool isPendulum)
		{
			return null;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetCommandText(Engine.CommandType commandType, bool isPendulum)
		{
			return null;
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTween(string label, string stopLabel)
		{
		}

		// Token: 0x0400982D RID: 38957
		[SerializeField]
		private List<CardCommand.CommandSetting> commandSettings;

		// Token: 0x0400982E RID: 38958
		private CardCommand.CommandButton[] buttons;

		// Token: 0x0400982F RID: 38959
		private ElementObjectManager ui;

		// Token: 0x04009830 RID: 38960
		private Selector selector;

		// Token: 0x04009831 RID: 38961
		private SelectionButton closeButton;

		// Token: 0x04009832 RID: 38962
		private ElementObjectManager affectButtonRoot;

		// Token: 0x04009833 RID: 38963
		private SelectionButton affectButton;

		// Token: 0x04009834 RID: 38964
		private Action onAffectCallback;

		// Token: 0x04009835 RID: 38965
		private RectTransform commandBG;

		// Token: 0x04009836 RID: 38966
		private Image commandBGImage;

		// Token: 0x04009837 RID: 38967
		private RectTransform commBase;

		// Token: 0x04009838 RID: 38968
		private RectTransform commGrid;

		// Token: 0x04009839 RID: 38969
		private RectTransform commGridPosHand;

		// Token: 0x0400983A RID: 38970
		private RectTransform commGridPosField;

		// Token: 0x0400983B RID: 38971
		private Vector2 dispPositionOrg;

		// Token: 0x0400983C RID: 38972
		private bool isPendulum;

		// Token: 0x0400983D RID: 38973
		private int currentCommandIndex;

		// Token: 0x0400983E RID: 38974
		private bool docommand;

		// Token: 0x0400983F RID: 38975
		private static readonly string prefabPath;

		// Token: 0x04009840 RID: 38976
		public bool dontClose;

		// Token: 0x04009841 RID: 38977
		private Vector3 actPosBase;

		// Token: 0x04009842 RID: 38978
		private RectTransform fixedPosition;

		// Token: 0x04009843 RID: 38979
		private int cmdMax;

		// Token: 0x02000CB4 RID: 3252
		[Serializable]
		private class CommandSetting
		{
			// Token: 0x04009844 RID: 38980
			public Engine.CommandType type;

			// Token: 0x04009845 RID: 38981
			public Sprite icon;

			// Token: 0x04009846 RID: 38982
			public Sprite iconSelect;

			// Token: 0x04009847 RID: 38983
			public Sprite iconDown;

			// Token: 0x04009848 RID: 38984
			public Sprite iconInactive;

			// Token: 0x04009849 RID: 38985
			public string textID;

			// Token: 0x0400984A RID: 38986
			public bool isPendulum;
		}

		// Token: 0x02000CB5 RID: 3253
		public class CommandButton
		{
			// Token: 0x0400984B RID: 38987
			public ElementObjectManager root;

			// Token: 0x0400984C RID: 38988
			public SelectionButton button;

			// Token: 0x0400984D RID: 38989
			public ColorContainerImage icon;

			// Token: 0x0400984E RID: 38990
			public ExtendedTextMeshProUGUI text;

			// Token: 0x0400984F RID: 38991
			public Engine.CommandType command;
		}
	}
}

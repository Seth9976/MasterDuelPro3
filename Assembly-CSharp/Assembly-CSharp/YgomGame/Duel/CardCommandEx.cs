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
	// Token: 0x02000CB6 RID: 3254
	public class CardCommandEx : MonoBehaviour
	{
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CC4 RID: 23748 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CC6 RID: 23750 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<uint> OnCommandClick
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CC8 RID: 23752 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<uint, Vector2> onDragBegin
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CCA RID: 23754 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<uint, Vector2> onDragging
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

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005CCC RID: 23756 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<uint, Vector2> onDragEnd
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

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06005CCD RID: 23757 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005CCE RID: 23758 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06005CCF RID: 23759 RVA: 0x000F503C File Offset: 0x000F323C
		private Vector2 commandSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x000F5054 File Offset: 0x000F3254
		private Vector2 commandSizeRatio
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, DuelClient host, Action<CardCommandEx> onLoaded)
		{
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelClient host)
		{
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x0000216D File Offset: 0x0000036D
		public void Term()
		{
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(int cardID, int face, Vector3 screenPoint)
		{
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispButton(uint standType)
		{
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispAllButton(bool disp)
		{
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDefaultPosition()
		{
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0000216D File Offset: 0x0000036D
		public void FixedPositionMode()
		{
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCommand(int index)
		{
			return false;
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCommandByStandType(uint standType)
		{
			return false;
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0000216D File Offset: 0x0000036D
		public void AlphaChange(bool setAlpha)
		{
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x000F506C File Offset: 0x000F326C
		public Vector3 SetPosition(Vector2 screenPoint)
		{
			return default(Vector3);
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGridPositionHand()
		{
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetGridPositionField()
		{
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveCommandBG(bool active)
		{
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCommand(int index)
		{
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardCommandEx.StandType GetCurrentBattlePosition()
		{
			return CardCommandEx.StandType.FrontAtk;
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x000029CC File Offset: 0x00000BCC
		public uint GetCurrentResultParam()
		{
			return 0U;
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x000029CC File Offset: 0x00000BCC
		private uint GetCommandExToResultParam(CardCommandEx.StandType battlePosition)
		{
			return 0U;
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardCommandEx.StandType ResultParamToStandType(uint resultParam)
		{
			return CardCommandEx.StandType.FrontAtk;
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x0000216A File Offset: 0x0000036A
		private CardCommandEx.CommandSetting GetSetting(CardCommandEx.StandType commandType)
		{
			return null;
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetCommandText(CardCommandEx.StandType commandType)
		{
			return null;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTween(string label, string stopLabel)
		{
		}

		// Token: 0x04009850 RID: 38992
		[SerializeField]
		private List<CardCommandEx.CommandSetting> commandSettings;

		// Token: 0x04009851 RID: 38993
		private CardCommandEx.CommandButton[] buttons;

		// Token: 0x04009852 RID: 38994
		private RectTransform commandBG;

		// Token: 0x04009853 RID: 38995
		private Image commandBGImage;

		// Token: 0x04009854 RID: 38996
		private Selector selector;

		// Token: 0x04009855 RID: 38997
		private RectTransform commBase;

		// Token: 0x04009856 RID: 38998
		private RectTransform commGrid;

		// Token: 0x04009857 RID: 38999
		private RectTransform commGridPosHand;

		// Token: 0x04009858 RID: 39000
		private RectTransform commGridPosField;

		// Token: 0x04009859 RID: 39001
		private bool docommand;

		// Token: 0x0400985A RID: 39002
		private static readonly string prefabPath;

		// Token: 0x0400985B RID: 39003
		private Vector3 actPosBase;

		// Token: 0x0400985C RID: 39004
		private RectTransform fixedPosition;

		// Token: 0x0400985D RID: 39005
		private int bpFace;

		// Token: 0x0400985E RID: 39006
		private int cmdMax;

		// Token: 0x0400985F RID: 39007
		private int currentCommandIndex;

		// Token: 0x02000CB7 RID: 3255
		public enum StandType
		{
			// Token: 0x04009861 RID: 39009
			FrontAtk,
			// Token: 0x04009862 RID: 39010
			FrontDef,
			// Token: 0x04009863 RID: 39011
			BackDef,
			// Token: 0x04009864 RID: 39012
			NONE = -1
		}

		// Token: 0x02000CB8 RID: 3256
		[Serializable]
		private class CommandSetting
		{
			// Token: 0x04009865 RID: 39013
			public CardCommandEx.StandType type;

			// Token: 0x04009866 RID: 39014
			public Sprite icon;

			// Token: 0x04009867 RID: 39015
			public Sprite iconSelect;

			// Token: 0x04009868 RID: 39016
			public Sprite iconDown;

			// Token: 0x04009869 RID: 39017
			public Sprite iconInactive;

			// Token: 0x0400986A RID: 39018
			public string textID;
		}

		// Token: 0x02000CB9 RID: 3257
		public class CommandButton
		{
			// Token: 0x0400986B RID: 39019
			public ElementObjectManager root;

			// Token: 0x0400986C RID: 39020
			public SelectionButton button;

			// Token: 0x0400986D RID: 39021
			public ColorContainerImage icon;

			// Token: 0x0400986E RID: 39022
			public ExtendedTextMeshProUGUI text;

			// Token: 0x0400986F RID: 39023
			public CardCommandEx.StandType battlePosition;

			// Token: 0x04009870 RID: 39024
			public uint engineParam;
		}
	}
}

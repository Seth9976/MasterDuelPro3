using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D8E RID: 3470
	public class DuelLogScrollView : MonoBehaviour
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x000029CC File Offset: 0x00000BCC
		public int topitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x000029CC File Offset: 0x00000BCC
		public int bottomitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060065C8 RID: 26056 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060065C9 RID: 26057 RVA: 0x0000216A File Offset: 0x0000036A
		public ExtendedScrollRect scrollrect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060065CB RID: 26059 RVA: 0x0000216D File Offset: 0x0000036D
		public List<string> m_DataLabelList
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

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_Spacing
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_BottomitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionItem[] m_TopSelectionItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060065CF RID: 26063 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionItem[] m_BottomSelectionItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialzize(ref List<string> templateLabelList, DuelClient host)
		{
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTopItem(SelectionItem item)
		{
			return false;
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsBottomItem(SelectionItem item)
		{
			return false;
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddDataLabel(string label)
		{
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetTopItemRT()
		{
			return null;
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetBottomItemRT()
		{
			return null;
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetBottomSItem()
		{
			return null;
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetTopSItem()
		{
			return null;
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContent()
		{
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairRightTransition(SelectionItemPair sbtnpair)
		{
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairLeftTransition(SelectionItemPair sbtnpair)
		{
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairVerticalTransition(SelectionItemPair sbtnpair0, SelectionItemPair sbtnpair1)
		{
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x000F5C88 File Offset: 0x000F3E88
		protected ValueTuple<int, int, float> GetDataIndexRange()
		{
			return default(ValueTuple<int, int, float>);
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int GetDataIndexByPos(float pos)
		{
			return 0;
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideItem(GameObject obj)
		{
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowItem(GameObject obj, bool istop)
		{
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetFreeItemByLabel(string label)
		{
			return null;
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddItem(int index, bool top)
		{
			return false;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveItem(bool top)
		{
			return false;
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveAllItem()
		{
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeContentSize()
		{
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddTopItem()
		{
			return false;
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddBottomItem()
		{
			return false;
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveTopItem()
		{
			return false;
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveBottomItem()
		{
			return false;
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveUp()
		{
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveDown()
		{
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToLastLabel(string targetlabel)
		{
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToNextLabel(string targetlabel)
		{
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveToTargetData(int dataindex, bool ontop = true)
		{
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveToPosition(Vector3 targetpos)
		{
		}

		// Token: 0x0400A01B RID: 40987
		[HideInInspector]
		public Dictionary<string, GetInt> m_DataNumDict;

		// Token: 0x0400A01C RID: 40988
		public DuelLogScrollView.OnItemUpdate onItemUpdate;

		// Token: 0x0400A01D RID: 40989
		public UnityEvent onReady;

		// Token: 0x0400A01E RID: 40990
		public bool autoScroll;

		// Token: 0x0400A01F RID: 40991
		protected Dictionary<string, Stack<Transform>> m_ItemStackDict;

		// Token: 0x0400A020 RID: 40992
		protected Dictionary<string, GameObject> m_PrehabDict;

		// Token: 0x0400A021 RID: 40993
		protected List<string> m_TemplateLabelList;

		// Token: 0x0400A022 RID: 40994
		protected List<float> m_ItemBiaslList;

		// Token: 0x0400A023 RID: 40995
		protected List<GameObject> m_UsedItemQueue;

		// Token: 0x0400A024 RID: 40996
		protected List<SelectionItemPair> m_SbtnPairList;

		// Token: 0x0400A025 RID: 40997
		protected ElementObjectManager m_EOManager;

		// Token: 0x0400A026 RID: 40998
		protected RectTransform m_ContentRT;

		// Token: 0x0400A027 RID: 40999
		protected RectTransform m_ViewportRT;

		// Token: 0x0400A028 RID: 41000
		protected RectTransform m_ScrollViewRT;

		// Token: 0x0400A029 RID: 41001
		protected Selector m_Selector;

		// Token: 0x0400A02A RID: 41002
		protected ExtendedScrollRect m_ScrollRect;

		// Token: 0x0400A02B RID: 41003
		protected DuelClient m_Host;

		// Token: 0x0400A02C RID: 41004
		private const string LABEL_EO_CONTENT = "content";

		// Token: 0x0400A02D RID: 41005
		private const string LABEL_EO_VIEWPORT = "viewport";

		// Token: 0x0400A02E RID: 41006
		private const string LABEL_EO_HIDDENPORT = "hiddenport";

		// Token: 0x0400A02F RID: 41007
		private const string LABEL_EO_PANEL = "AutoScrollPanel";

		// Token: 0x0400A030 RID: 41008
		private const string LABEL_EO_STATUE = "AutoScrollStatue";

		// Token: 0x0400A031 RID: 41009
		private const string LABEL_EO_SCROLLBAR = "ScrollBar";

		// Token: 0x0400A032 RID: 41010
		private int m_InitialTopPadding;

		// Token: 0x0400A033 RID: 41011
		private int m_TopitemDataindex;

		// Token: 0x02000D8F RID: 3471
		public class OnItemUpdate : UnityEvent<GameObject, int>
		{
		}
	}
}

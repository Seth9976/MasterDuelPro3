using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomDebug
{
	// Token: 0x02001157 RID: 4439
	public class DuelLogScrollViewForAnalysis : MonoBehaviour
	{
		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06008418 RID: 33816 RVA: 0x000029CC File Offset: 0x00000BCC
		public int topitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06008419 RID: 33817 RVA: 0x000029CC File Offset: 0x00000BCC
		public int bottomitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x0600841A RID: 33818 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x0600841B RID: 33819 RVA: 0x0000216A File Offset: 0x0000036A
		public ExtendedScrollRect scrollrect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x0600841C RID: 33820 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_Spacing
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600841D RID: 33821 RVA: 0x000029CC File Offset: 0x00000BCC
		private int m_BottomitemDataindex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600841E RID: 33822 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionItem[] m_TopSelectionItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600841F RID: 33823 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionItem[] m_BottomSelectionItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008420 RID: 33824 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialzize(ref List<string> templateLabelList)
		{
		}

		// Token: 0x06008421 RID: 33825 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTopItem(SelectionItem item)
		{
			return false;
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsBottomItem(SelectionItem item)
		{
			return false;
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataLabelList(List<string> datalabellist)
		{
		}

		// Token: 0x06008424 RID: 33828 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddDataLabel(string label)
		{
		}

		// Token: 0x06008425 RID: 33829 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetTopItemRT()
		{
			return null;
		}

		// Token: 0x06008426 RID: 33830 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetBottomItemRT()
		{
			return null;
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetBottomSItem()
		{
			return null;
		}

		// Token: 0x06008428 RID: 33832 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetTopSItem()
		{
			return null;
		}

		// Token: 0x06008429 RID: 33833 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContent()
		{
		}

		// Token: 0x0600842A RID: 33834 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairRightTransition(SelectionItemPair sbtnpair)
		{
		}

		// Token: 0x0600842B RID: 33835 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairLeftTransition(SelectionItemPair sbtnpair)
		{
		}

		// Token: 0x0600842C RID: 33836 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSbtnPairVerticalTransition(SelectionItemPair sbtnpair0, SelectionItemPair sbtnpair1)
		{
		}

		// Token: 0x0600842D RID: 33837 RVA: 0x000F746C File Offset: 0x000F566C
		protected ValueTuple<int, int, float> GetDataIndexRange()
		{
			return default(ValueTuple<int, int, float>);
		}

		// Token: 0x0600842E RID: 33838 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int GetDataIndexByPos(float pos)
		{
			return 0;
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideItem(GameObject obj)
		{
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowItem(GameObject obj, bool istop)
		{
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetFreeItemByLabel(string label)
		{
			return null;
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddItem(int index, bool top)
		{
			return false;
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveItem(bool top)
		{
			return false;
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveAllItem()
		{
		}

		// Token: 0x06008435 RID: 33845 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeContentSize()
		{
		}

		// Token: 0x06008436 RID: 33846 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddTopItem()
		{
			return false;
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddBottomItem()
		{
			return false;
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveTopItem()
		{
			return false;
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveBottomItem()
		{
			return false;
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveUp()
		{
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveDown()
		{
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToLastLabel(string targetlabel)
		{
		}

		// Token: 0x0600843E RID: 33854 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveToNextLabel(string targetlabel)
		{
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveToTargetData(int dataindex, bool ontop = true)
		{
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x0000216D File Offset: 0x0000036D
		private void MoveToPosition(Vector3 targetpos)
		{
		}

		// Token: 0x0400BF72 RID: 49010
		[HideInInspector]
		public Dictionary<string, GetInt> m_DataNumDict;

		// Token: 0x0400BF73 RID: 49011
		public DuelLogScrollViewForAnalysis.OnItemUpdate onItemUpdate;

		// Token: 0x0400BF74 RID: 49012
		public UnityEvent onReady;

		// Token: 0x0400BF75 RID: 49013
		public bool autoScroll;

		// Token: 0x0400BF76 RID: 49014
		protected Dictionary<string, Stack<Transform>> m_ItemStackDict;

		// Token: 0x0400BF77 RID: 49015
		protected Dictionary<string, GameObject> m_PrehabDict;

		// Token: 0x0400BF78 RID: 49016
		protected List<string> m_TemplateLabelList;

		// Token: 0x0400BF79 RID: 49017
		protected List<string> m_DataLabelList;

		// Token: 0x0400BF7A RID: 49018
		protected List<float> m_ItemBiaslList;

		// Token: 0x0400BF7B RID: 49019
		protected List<GameObject> m_UsedItemQueue;

		// Token: 0x0400BF7C RID: 49020
		protected ElementObjectManager m_EOManager;

		// Token: 0x0400BF7D RID: 49021
		protected RectTransform m_ContentRT;

		// Token: 0x0400BF7E RID: 49022
		protected RectTransform m_ViewportRT;

		// Token: 0x0400BF7F RID: 49023
		protected RectTransform m_ScrollViewRT;

		// Token: 0x0400BF80 RID: 49024
		protected Selector m_Selector;

		// Token: 0x0400BF81 RID: 49025
		protected ExtendedScrollRect m_ScrollRect;

		// Token: 0x0400BF82 RID: 49026
		private const string LABEL_EO_CONTENT = "content";

		// Token: 0x0400BF83 RID: 49027
		private const string LABEL_EO_VIEWPORT = "viewport";

		// Token: 0x0400BF84 RID: 49028
		private const string LABEL_EO_HIDDENPORT = "hiddenport";

		// Token: 0x0400BF85 RID: 49029
		private const string LABEL_EO_PANEL = "AutoScrollPanel";

		// Token: 0x0400BF86 RID: 49030
		private const string LABEL_EO_STATUE = "AutoScrollStatue";

		// Token: 0x0400BF87 RID: 49031
		private const string LABEL_EO_SCROLLBAR = "ScrollBar";

		// Token: 0x0400BF88 RID: 49032
		private int m_InitialTopPadding;

		// Token: 0x0400BF89 RID: 49033
		private int m_TopitemDataindex;

		// Token: 0x02001158 RID: 4440
		public class OnItemUpdate : UnityEvent<GameObject, int>
		{
		}
	}
}

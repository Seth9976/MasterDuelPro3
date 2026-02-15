using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E2C RID: 3628
	public class EndlessScrollView : MonoBehaviour
	{
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06006897 RID: 26775 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_ContentRT
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06006898 RID: 26776 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_ItemNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06006899 RID: 26777 RVA: 0x0000216A File Offset: 0x0000036A
		protected GridLayoutGroup m_GridLayoutGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x000029CC File Offset: 0x00000BCC
		public int viewItemRow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600689B RID: 26779 RVA: 0x000029CC File Offset: 0x00000BCC
		public int viewItemColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600689C RID: 26780 RVA: 0x000F5DE8 File Offset: 0x000F3FE8
		// (set) Token: 0x0600689D RID: 26781 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector2 dataBias
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600689E RID: 26782 RVA: 0x000F5E00 File Offset: 0x000F4000
		// (set) Token: 0x0600689F RID: 26783 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector2 itemBias
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060068A0 RID: 26784 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInitialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060068A1 RID: 26785 RVA: 0x000F5E18 File Offset: 0x000F4018
		protected Vector2 m_BlockBias
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060068A2 RID: 26786 RVA: 0x000F5E30 File Offset: 0x000F4030
		public Vector2 unitSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060068A3 RID: 26787 RVA: 0x000F5E48 File Offset: 0x000F4048
		public Vector2 blockSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060068A4 RID: 26788 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060068A5 RID: 26789 RVA: 0x0000216D File Offset: 0x0000036D
		public ScrollMode endlessMode
		{
			get
			{
				return ScrollMode.Horizontal;
			}
			set
			{
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060068A6 RID: 26790 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOnTop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060068A7 RID: 26791 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOnBottom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060068A8 RID: 26792 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_DataNumChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060068A9 RID: 26793 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_DataBiasChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060068AA RID: 26794 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_ItemBiasChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060068AB RID: 26795 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_BlockBiasChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060068AC RID: 26796 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_ContentPosChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060068AD RID: 26797 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_PaddingChangedFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060068AE RID: 26798 RVA: 0x0000216A File Offset: 0x0000036A
		protected ScrollRect m_ScrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060068AF RID: 26799 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060068B0 RID: 26800 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataLineCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060068B1 RID: 26801 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_ItemNumPerLine
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060068B2 RID: 26802 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataBiasIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize(Func<int> getDataNum, Action<GameObject, int> onItemUpdate, Action<GameObject> onItemInitialize = null)
		{
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceUpdateContent()
		{
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Update()
		{
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Instantiation()
		{
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetViewSize()
		{
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AttachTween()
		{
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLayout()
		{
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateItemData()
		{
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetItemVisibility(int itemIndex, bool visibility)
		{
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataBias()
		{
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdatePadding()
		{
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveContent(Vector2 move)
		{
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x0000216D File Offset: 0x0000036D
		public void IncreaseLine(bool loop = false, float lineNum = 1f)
		{
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x0000216D File Offset: 0x0000036D
		public void DecreaseLine(bool loop = false, float lineNum = 1f)
		{
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetItemByDataIndex(int dataIndex)
		{
			return null;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x000F5E60 File Offset: 0x000F4060
		public Vector2 GetItemPosByDataIndex(int dataIndex, Vector2 pivot = default(Vector2))
		{
			return default(Vector2);
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetItemIdByDataIndex(int dataIndex)
		{
			return 0;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIdByItemIndex(int itemIndex)
		{
			return 0;
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckItemIndex(int itemIndex)
		{
			return false;
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndexCorrection(int dataIndex)
		{
			return 0;
		}

		// Token: 0x0400A392 RID: 41874
		[SerializeField]
		protected string templateLabel;

		// Token: 0x0400A393 RID: 41875
		[SerializeField]
		protected string contentLabel;

		// Token: 0x0400A394 RID: 41876
		[SerializeField]
		protected string viewportLabel;

		// Token: 0x0400A395 RID: 41877
		[SerializeField]
		protected ScrollMode m_ScrollMode;

		// Token: 0x0400A396 RID: 41878
		protected Rect m_ScrollViewRT;

		// Token: 0x0400A397 RID: 41879
		protected GridLayoutGroup m_GridLayoutGroup_org;

		// Token: 0x0400A398 RID: 41880
		[SerializeField]
		protected RectOffset m_Padding;

		// Token: 0x0400A399 RID: 41881
		[SerializeField]
		protected Vector2 m_CellSize;

		// Token: 0x0400A39A RID: 41882
		[SerializeField]
		protected Vector2 m_Spacing;

		// Token: 0x0400A39B RID: 41883
		protected TextAnchor m_ChildAlignment;

		// Token: 0x0400A39C RID: 41884
		protected UnityEvent onBlockBiasUpdate;

		// Token: 0x0400A39D RID: 41885
		protected UnityEvent onDataBiasUpdate;

		// Token: 0x0400A39E RID: 41886
		protected UnityEvent onDataNumChanged;

		// Token: 0x0400A39F RID: 41887
		protected UnityEvent onPaddingChanged;

		// Token: 0x0400A3A0 RID: 41888
		public Func<int> getDataNum;

		// Token: 0x0400A3A1 RID: 41889
		public Action<GameObject> onItemInitialize;

		// Token: 0x0400A3A2 RID: 41890
		public Action<GameObject, int> onItemUpdate;

		// Token: 0x0400A3A3 RID: 41891
		protected Vector2 m_ItemBias;

		// Token: 0x0400A3A4 RID: 41892
		protected Vector2 m_DataBias;

		// Token: 0x0400A3A5 RID: 41893
		protected Vector2 m_PosTrans;

		// Token: 0x0400A3A6 RID: 41894
		protected TweenPosition m_TweenMove;

		// Token: 0x0400A3A7 RID: 41895
		protected List<RectTransform> m_ItemRTList;

		// Token: 0x0400A3A8 RID: 41896
		protected int m_DataNum_Old;

		// Token: 0x0400A3A9 RID: 41897
		protected Vector2 m_ItemBias_Old;

		// Token: 0x0400A3AA RID: 41898
		protected Vector2 m_DataBias_Old;

		// Token: 0x0400A3AB RID: 41899
		protected Vector2 m_BlockBias_Old;

		// Token: 0x0400A3AC RID: 41900
		protected Vector2 m_ContentPos_Old;

		// Token: 0x0400A3AD RID: 41901
		protected RectOffset m_Padding_Old;

		// Token: 0x0400A3AE RID: 41902
		protected bool m_Isinitialized;
	}
}

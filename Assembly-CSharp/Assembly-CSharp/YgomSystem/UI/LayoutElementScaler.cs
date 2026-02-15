using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005A4 RID: 1444
	public class LayoutElementScaler : UIBehaviour, ILayoutElement
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x0000216A File Offset: 0x0000036A
		public ILayoutElement layoutElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x0000216D File Offset: 0x0000036D
		public int layoutPriority
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x0000216D File Offset: 0x0000036D
		public float widthScale
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x0000216D File Offset: 0x0000036D
		public float heightScale
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float preferredWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float flexibleWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float preferredHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float flexibleHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0000216D File Offset: 0x0000036D
		public void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0000216D File Offset: 0x0000036D
		public void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDirty()
		{
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayedSetDirty(RectTransform rectTransform)
		{
			return null;
		}

		// Token: 0x04002B80 RID: 11136
		[SerializeField]
		private int m_LayoutPriority;

		// Token: 0x04002B81 RID: 11137
		[SerializeField]
		private float m_WidthScale;

		// Token: 0x04002B82 RID: 11138
		[SerializeField]
		private float m_HeightScale;

		// Token: 0x04002B83 RID: 11139
		private ILayoutElement m_LayoutElementCache;
	}
}

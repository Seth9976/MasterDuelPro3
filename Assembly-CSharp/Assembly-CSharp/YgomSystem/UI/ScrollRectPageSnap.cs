using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005C2 RID: 1474
	public class ScrollRectPageSnap : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x0000216D File Offset: 0x0000036D
		public int horizontalPages
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x0000216D File Offset: 0x0000036D
		public int verticalPages
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0000216D File Offset: 0x0000036D
		public int hPage
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x0000216D File Offset: 0x0000036D
		public int vPage
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDragging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTweening
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRect target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnScroll(PointerEventData eventData)
		{
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000029CC File Offset: 0x00000BCC
		public int calcPageIndex(float norm, int pages, int startpage)
		{
			return 0;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseDrag(Vector2 delta)
		{
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStartBarDrag()
		{
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBarDragging()
		{
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEndBarDrag()
		{
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x0000216D File Offset: 0x0000036D
		private void CalcPositionToPage()
		{
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeNormalized(float ex, float ey)
		{
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetVPageEnable(bool enable, int page = -1)
		{
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetHPageEnable(bool enable, int page = -1)
		{
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceSnap()
		{
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoopHorizontal()
		{
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0000216D File Offset: 0x0000036D
		public void PseudoLoopHorizontalView()
		{
		}

		// Token: 0x04002C05 RID: 11269
		[SerializeField]
		public int m_HorizontalPages;

		// Token: 0x04002C06 RID: 11270
		[SerializeField]
		public int m_VerticalPages;

		// Token: 0x04002C07 RID: 11271
		public bool wheelScrollEnable;

		// Token: 0x04002C08 RID: 11272
		[SerializeField]
		public Tween.Easing easing;

		// Token: 0x04002C09 RID: 11273
		[SecField]
		[SerializeField]
		public float duration;

		// Token: 0x04002C0A RID: 11274
		[SerializeField]
		public float sensitivity;

		// Token: 0x04002C0B RID: 11275
		[SerializeField]
		public float backlash;

		// Token: 0x04002C0C RID: 11276
		[SerializeField]
		public UnityEvent onPageChanged;

		// Token: 0x04002C0D RID: 11277
		[SerializeField]
		public UnityEvent onPageTotalChanged;

		// Token: 0x04002C0E RID: 11278
		public Action<int> onReleaseDragCallback;

		// Token: 0x04002C0F RID: 11279
		private int draggingId;

		// Token: 0x04002C10 RID: 11280
		private ScrollRect scrollrect;

		// Token: 0x04002C11 RID: 11281
		private float tweenTime;

		// Token: 0x04002C12 RID: 11282
		private float tweenMaxTime;

		// Token: 0x04002C13 RID: 11283
		private Vector2 startNormalized;

		// Token: 0x04002C14 RID: 11284
		private Vector2 endNormalized;

		// Token: 0x04002C15 RID: 11285
		private int vpage;

		// Token: 0x04002C16 RID: 11286
		private int hpage;

		// Token: 0x04002C17 RID: 11287
		private int vspage;

		// Token: 0x04002C18 RID: 11288
		private int hspage;

		// Token: 0x04002C19 RID: 11289
		private List<bool> hpageEnableList;

		// Token: 0x04002C1A RID: 11290
		private List<bool> vpageEnableList;

		// Token: 0x04002C1B RID: 11291
		private bool isBarDragging;

		// Token: 0x04002C1C RID: 11292
		public bool dragScrollEnabled;
	}
}

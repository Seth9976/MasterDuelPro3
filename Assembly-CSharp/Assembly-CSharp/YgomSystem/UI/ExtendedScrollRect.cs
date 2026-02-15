using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x02000591 RID: 1425
	public class ExtendedScrollRect : ScrollRect, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IPointerDownHandler
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x0000216D File Offset: 0x0000036D
		public int dragBlockCounter
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDragBlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000F1F5C File Offset: 0x000F015C
		public Vector2 targetPos
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isAutoScroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIgnoreNotTagetAxis(bool isIgnore, float thres)
		{
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnInitializePotentialDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnScroll(PointerEventData data)
		{
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScrollByDelta(Vector2 delta)
		{
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScrollByTargetPos(Vector2 targetPos)
		{
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAutoScroll(bool isInvokeCallback = true)
		{
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerator MoveContentImpl()
		{
			return null;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActiveAnalogPadScroll(SelectorManager.AnalogType analogType)
		{
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetAlpha(GameObject target, float alpha)
		{
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowScrollBar()
		{
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideScrollBar()
		{
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StopMovement()
		{
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScrollByVerticalNormalizedPos(float dst, bool overrideTarget = true)
		{
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScrollByHorizontalNormalizedPos(float dst, bool overrideTarget = true)
		{
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScrollByNormalizedPos(Vector2 dst, bool overrideTarget = true)
		{
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDestroy()
		{
		}

		// Token: 0x04002B2D RID: 11053
		private const float WHEELSCROLL_MINMAGNITUDE = 0.0001f;

		// Token: 0x04002B2E RID: 11054
		private const float k_BaseDeltaSec = 0.01666f;

		// Token: 0x04002B2F RID: 11055
		private const float k_MinDeltaModify = 0.3f;

		// Token: 0x04002B30 RID: 11056
		private GameObject barObjHorizontal;

		// Token: 0x04002B31 RID: 11057
		private GameObject barObjVertical;

		// Token: 0x04002B32 RID: 11058
		private TweenContainer tweenContainerH;

		// Token: 0x04002B33 RID: 11059
		private TweenContainer tweenContainerV;

		// Token: 0x04002B34 RID: 11060
		private CanvasGroup barCanvasGrpH;

		// Token: 0x04002B35 RID: 11061
		private CanvasGroup barCanvasGrpV;

		// Token: 0x04002B36 RID: 11062
		private bool dragScrollEnabled;

		// Token: 0x04002B37 RID: 11063
		private bool fadeEnabled;

		// Token: 0x04002B38 RID: 11064
		private bool isHorizontalShowing;

		// Token: 0x04002B39 RID: 11065
		private bool isVerticalShowing;

		// Token: 0x04002B3A RID: 11066
		protected bool dragging;

		// Token: 0x04002B3B RID: 11067
		private Vector2 m_WheelScrollVelocity;

		// Token: 0x04002B3C RID: 11068
		private Vector2 m_TargetPos;

		// Token: 0x04002B3D RID: 11069
		private Vector2 m_AnchorPos;

		// Token: 0x04002B3E RID: 11070
		private IEnumerator m_yMoveContentImpl;

		// Token: 0x04002B3F RID: 11071
		public Action onStopScrollCallback;

		// Token: 0x04002B40 RID: 11072
		[SerializeField]
		private bool m_IsIgnoreNotTagetAxis;

		// Token: 0x04002B41 RID: 11073
		[SerializeField]
		private float ignoreRate;

		// Token: 0x04002B42 RID: 11074
		[SerializeField]
		private bool m_StopOnPointerDown;

		// Token: 0x04002B43 RID: 11075
		[SerializeField]
		private bool m_ApplyDeltaTime;
	}
}

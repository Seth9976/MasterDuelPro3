using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI.InfinityScroll
{
	// Token: 0x02000682 RID: 1666
	public class EntityPoolController
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x0000216A File Offset: 0x0000036A
		public EntityPoolController.WaitFocusData waitFocusData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExistsFocusWaitData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000029CC File Offset: 0x00000BCC
		public int dataCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_LastLineItemCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_LastLineItemCountInView
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_IsHorizontalScroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isHorizontalScroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_CurrentContentPos
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_ViewSizeAlongScrollDirection
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_SpacingAlongScrollDirection
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000029CC File Offset: 0x00000BCC
		public int beginDataIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataIndexOfListBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_VerticalOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_HorizontalOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000029CC File Offset: 0x00000BCC
		public int endDataIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataIndexOfListEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingBias
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCompletedReserve
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<GameObject> activeEntityList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000029CC File Offset: 0x00000BCC
		public int activeEntityLineCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndexOfItemBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndexOfItemEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x0000216D File Offset: 0x0000036D
		public int constraintCount
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000029CC File Offset: 0x00000BCC
		public int contentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700035C RID: 860
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject> onCreatedEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x1700035D RID: 861
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject> onActivateEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x1700035E RID: 862
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject, int> onUpdateEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x1700035F RID: 863
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject, int, bool, bool> onFocusEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x17000360 RID: 864
		// (set) Token: 0x060033B1 RID: 13233 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject> onDeactivateEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x17000361 RID: 865
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<GameObject, int, bool> onRemoveEntityCallback
		{
			set
			{
			}
		}

		// Token: 0x17000362 RID: 866
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<int, ValueTuple<bool, float>> customMoveContentToFitDataFunc
		{
			set
			{
			}
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_UnitSizeAlongScrollDirection(int templateIdx)
		{
			return 0f;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(EntityPoolSettings entityPoolSettings, InfinityScrollView owner, ExtendedScrollRect scrollRect, List<GameObject> templates, Action onCompleteCallback = null)
		{
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReserveTemplate(int templateIdx = 0, int asyncPerFrame = 0, Action onComplete = null)
		{
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x0000216D File Offset: 0x0000036D
		public void TerminateReserve()
		{
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yReserveTemplate(int templateIdx = 0, int asyncPerFrame = 0, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdatePaddingSize()
		{
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateViewportRectSize()
		{
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitContentRT(EntityPoolSettings entityPoolSettings)
		{
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerator ReadRectSize(EntityPoolSettings entityPoolSettings, List<GameObject> templates, Action onCompleteCallback = null)
		{
			return null;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollValueChanged(Vector2 bias)
		{
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InstantiateTemplate(GameObject template, int templateIdx = 0)
		{
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddTemplateInstantance(int templateIdx)
		{
			return false;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitLayout(EntityPoolSettings entityPoolSettings, object layout)
		{
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RemakeLayout(GridLayoutGroup gridLayoutGroup)
		{
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddTopItem()
		{
			return false;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddBottomItem()
		{
			return false;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveTopItem()
		{
			return false;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveBottomItem()
		{
			return false;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContentPos()
		{
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x0000216D File Offset: 0x0000036D
		public void CheckWaitFocusData()
		{
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddItem(int dataindex, int posInHierachy)
		{
			return false;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveItem(int itemindex)
		{
			return false;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RemoveAllItem()
		{
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ChangeContentSize()
		{
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool MoveContentToFitDataPos(int dataindex)
		{
			return false;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0000216D File Offset: 0x0000036D
		public void CancelMoving()
		{
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0000216D File Offset: 0x0000036D
		protected void MoveContent(float targetpos)
		{
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000029CC File Offset: 0x00000BCC
		private int ClampDataIndex(int index)
		{
			return 0;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool IsIndexInSameLine(int index0, int index1)
		{
			return false;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckItemIndexCorrect(int itemindex)
		{
			return false;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckDataIndexCorrect(int dataindex)
		{
			return false;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckItemInViewByDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float GetContentPosByDataLine(int lineindex)
		{
			return 0f;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int GetDataLineByContentPos(float pos)
		{
			return 0;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void UpdateDataCount(int dataCount, List<int> templateList = null)
		{
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void UpdateDataCountAsync(int dataCount, List<int> templateList = null, int updatePerFrame = 1, Action onComplete = null)
		{
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerUpdateDataCount(int dataCount, List<int> templateList = null)
		{
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInnerUpdateDataCountAsync(int dataCount, List<int> templateList = null, int updatePerFrame = 1, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataAsync(int updatePerFrame, Action onComplete)
		{
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yUpdateDataAsync(int updatePerFrame, Action onComplete)
		{
			return null;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusItemByDataIndex(int dataindex, bool selectItem = true, bool isInitializeSelect = false, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryFocusInnerViewport(bool selectItem, bool isInitializeSelect = false, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetContentPosition()
		{
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImmediateApplyMovement()
		{
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetTemplateSizeAlongScrollDirection(int templateIdx)
		{
			return 0f;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetItemByListPos(int x, int y)
		{
			return null;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000F2BA0 File Offset: 0x000F0DA0
		public ValueTuple<int, int> GetListPosByItemIndex(int itemIndex)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetItemIndexByListPos(int x, int y)
		{
			return 0;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByItemIndex(int itemIndex)
		{
			return 0;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByListPos(int x, int y)
		{
			return 0;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetEntityIndexByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLineByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetEntityByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000F2BB8 File Offset: 0x000F0DB8
		public T GetEntityByDataIndex<T>(int dataindex)
		{
			return default(T);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByEntity(GameObject entity)
		{
			return 0;
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTemplateIndexByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTemplateIndexByEntity(GameObject entity)
		{
			return 0;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTemplateLabelByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTemplateLabelByTemplateIndex(int templateindex)
		{
			return null;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndex(int baseIdx, PadInputDirection direction)
		{
			return 0;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndexUp(int baseIdx)
		{
			return 0;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndexDown(int baseIdx)
		{
			return 0;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndexLeft(int baseIdx)
		{
			return 0;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndexRight(int baseIdx)
		{
			return 0;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeByDataIndex(int dataindex, PadInputDirection checkDirection)
		{
			return false;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeLeftDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeRightDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeUpDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeDownDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeHeadItemIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeTailItemIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeStartSideItemIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeEndSideItemIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFractionItemIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x04002FAC RID: 12204
		private InfinityScrollView m_Owner;

		// Token: 0x04002FAD RID: 12205
		private GridLayoutGroup.Axis m_ScrollAxis;

		// Token: 0x04002FAE RID: 12206
		private RectOffset m_Padding;

		// Token: 0x04002FAF RID: 12207
		private Vector2 m_ViewportSize;

		// Token: 0x04002FB0 RID: 12208
		private ExtendedScrollRect m_ScrollRect;

		// Token: 0x04002FB1 RID: 12209
		private GridLayoutGroup m_LayoutGroup;

		// Token: 0x04002FB2 RID: 12210
		private HorizontalLayoutGroup m_HorizontalLayoutGroup;

		// Token: 0x04002FB3 RID: 12211
		private VerticalLayoutGroup m_VerticallLayoutGroup;

		// Token: 0x04002FB4 RID: 12212
		private EntityPoolController.LayoutType m_LayoutType;

		// Token: 0x04002FB5 RID: 12213
		private int m_ConstraintCount;

		// Token: 0x04002FB6 RID: 12214
		private Vector2 m_Spacing;

		// Token: 0x04002FB7 RID: 12215
		private List<GameObject> m_Templates;

		// Token: 0x04002FB8 RID: 12216
		private int m_NumTemplates;

		// Token: 0x04002FB9 RID: 12217
		private List<int> m_TemplateList;

		// Token: 0x04002FBA RID: 12218
		private List<int> m_PrevTemplateList;

		// Token: 0x04002FBB RID: 12219
		private List<Vector2> m_UnitSize;

		// Token: 0x04002FBC RID: 12220
		private Dictionary<GameObject, int> m_EntityDataIndexTable;

		// Token: 0x04002FBD RID: 12221
		private List<Stack<GameObject>> m_FreeEntityStack;

		// Token: 0x04002FBE RID: 12222
		private List<GameObject> m_ActiveEntityList;

		// Token: 0x04002FBF RID: 12223
		private int m_DataCount;

		// Token: 0x04002FC0 RID: 12224
		private bool m_IsReady;

		// Token: 0x04002FC1 RID: 12225
		private EntityPoolController.WaitFocusData m_WaitFocusData;

		// Token: 0x04002FC2 RID: 12226
		public bool useViewportSize;

		// Token: 0x04002FC3 RID: 12227
		private Action<GameObject> m_OnCreatedEntityCallback;

		// Token: 0x04002FC4 RID: 12228
		private Action<GameObject> m_OnActivateEntityCallback;

		// Token: 0x04002FC5 RID: 12229
		private Action<GameObject, int> m_OnUpdateEntityCallback;

		// Token: 0x04002FC6 RID: 12230
		private Action<GameObject, int, bool, bool> m_OnFocusEntityCallback;

		// Token: 0x04002FC7 RID: 12231
		private Action<GameObject> m_OnDeactivateEntityCallback;

		// Token: 0x04002FC8 RID: 12232
		private Action<GameObject, int, bool> m_OnRemoveEntityCallback;

		// Token: 0x04002FC9 RID: 12233
		private Func<int, ValueTuple<bool, float>> m_CustomMoveContentToFitDataFunc;

		// Token: 0x04002FCA RID: 12234
		private bool m_IsInstantiateAllTemplates;

		// Token: 0x04002FCB RID: 12235
		private List<int> itemCount;

		// Token: 0x04002FCC RID: 12236
		private List<int> itemLimmit;

		// Token: 0x04002FCD RID: 12237
		protected int m_LastLineItemCountInViewCache;

		// Token: 0x04002FCE RID: 12238
		private IEnumerator m_ReserveTemplateRoutine;

		// Token: 0x04002FCF RID: 12239
		private IEnumerator m_UpdateDataCountAsync;

		// Token: 0x04002FD0 RID: 12240
		private IEnumerator m_UpdateDataAsync;

		// Token: 0x02000683 RID: 1667
		public class WaitFocusData
		{
			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06003400 RID: 13312 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isExists
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06003401 RID: 13313 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003402 RID: 13314 RVA: 0x0000216D File Offset: 0x0000036D
			public int dataIndex
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

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06003403 RID: 13315 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003404 RID: 13316 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionItem currentItem
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

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06003405 RID: 13317 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003406 RID: 13318 RVA: 0x0000216D File Offset: 0x0000036D
			public bool isSelectItem
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

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06003407 RID: 13319 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003408 RID: 13320 RVA: 0x0000216D File Offset: 0x0000036D
			public bool isInitializeSelect
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

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06003409 RID: 13321 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600340A RID: 13322 RVA: 0x0000216D File Offset: 0x0000036D
			public Action onComplete
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

			// Token: 0x0600340B RID: 13323 RVA: 0x0000216A File Offset: 0x0000036A
			public EntityPoolController.WaitFocusData Clone()
			{
				return null;
			}

			// Token: 0x0600340C RID: 13324 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x0600340D RID: 13325 RVA: 0x0000216D File Offset: 0x0000036D
			public void Assign(int dataIndex, bool isSelectItem, bool isInitializeSelect, Action onComplete, SelectionItem currentItem)
			{
			}
		}

		// Token: 0x02000684 RID: 1668
		private enum LayoutType
		{
			// Token: 0x04002FD2 RID: 12242
			Grid,
			// Token: 0x04002FD3 RID: 12243
			Horizontal,
			// Token: 0x04002FD4 RID: 12244
			Vertical
		}
	}
}

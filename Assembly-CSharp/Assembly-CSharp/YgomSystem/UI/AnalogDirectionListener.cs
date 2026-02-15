using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000564 RID: 1380
	public class AnalogDirectionListener : MonoBehaviour
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem selectionItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000029CC File Offset: 0x00000BCC
		public PadInputDirection currentInputDir
		{
			get
			{
				return PadInputDirection.None;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.AnalogType currentAnalogType
		{
			get
			{
				return SelectorManager.AnalogType.None;
			}
		}

		// Token: 0x17000211 RID: 529
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<SelectorManager.AnalogType, PadInputDirection> onInputCallback
		{
			set
			{
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0000216A File Offset: 0x0000036A
		public static AnalogDirectionListener Assign(GameObject owner, SelectionItem selectionItem, bool onlySelected = false, Action<SelectorManager.AnalogType, PadInputDirection> onInputCallback = null, params SelectorManager.AnalogType[] analogTypes)
		{
			return null;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0000216D File Offset: 0x0000036D
		private void Clear()
		{
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetInterval()
		{
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float GetInputIntervalTime(int intervalStep)
		{
			return 0f;
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000029CC File Offset: 0x00000BCC
		private PadInputDirection VecToDirection(Vector2 vec)
		{
			return PadInputDirection.None;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnAnalogInput(Vector2 vec, SelectorManager.AnalogType analogType)
		{
			return false;
		}

		// Token: 0x04002A78 RID: 10872
		private readonly float k_Threshhold;

		// Token: 0x04002A79 RID: 10873
		private readonly int k_IntervalStepMax;

		// Token: 0x04002A7A RID: 10874
		[SerializeField]
		private SelectionItem m_SelectionItem;

		// Token: 0x04002A7B RID: 10875
		[SerializeField]
		private bool m_OnlySelected;

		// Token: 0x04002A7C RID: 10876
		[SerializeField]
		private SelectorManager.AnalogType[] m_AnalogTypes;

		// Token: 0x04002A7D RID: 10877
		private PadInputDirection m_CurrentInputDir;

		// Token: 0x04002A7E RID: 10878
		private SelectorManager.AnalogType m_CurrentAnalogType;

		// Token: 0x04002A7F RID: 10879
		private int m_IntervalStep;

		// Token: 0x04002A80 RID: 10880
		private float m_InputIntervalTime;

		// Token: 0x04002A81 RID: 10881
		private Action<SelectorManager.AnalogType, PadInputDirection> m_OnInputCallback;
	}
}

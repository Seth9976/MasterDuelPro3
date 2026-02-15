using System;
using System.Collections;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013F4 RID: 5108
	[RequireComponent(typeof(RectTransform))]
	public class ConditionalAlignmentController : MonoBehaviour
	{
		// Token: 0x06009425 RID: 37925 RVA: 0x00151CD5 File Offset: 0x0014FED5
		private void Awake()
		{
			this.Initialize();
		}

		// Token: 0x06009426 RID: 37926 RVA: 0x00151CDD File Offset: 0x0014FEDD
		private void OnEnable()
		{
			SystemEvent.OnResolutionChange += this.Initialize;
		}

		// Token: 0x06009427 RID: 37927 RVA: 0x00151CF0 File Offset: 0x0014FEF0
		private void OnDisable()
		{
			SystemEvent.OnResolutionChange -= this.Initialize;
		}

		// Token: 0x06009428 RID: 37928 RVA: 0x00151D03 File Offset: 0x0014FF03
		private void Initialize()
		{
			if (this._parentRect == null)
			{
				this._parentRect = base.GetComponent<RectTransform>();
			}
			base.StartCoroutine(this.UpdateChildAlignment());
		}

		// Token: 0x06009429 RID: 37929 RVA: 0x00151D2C File Offset: 0x0014FF2C
		private IEnumerator UpdateChildAlignment()
		{
			if (this.targetChild == null)
			{
				yield break;
			}
			while (this._parentRect.rect.width < 0f)
			{
				yield return null;
			}
			ConditionalAlignmentController.AlignmentPreset preset = ((this._parentRect.rect.width > this.widthThreshold) ? this.wideScreenPreset : this.narrowScreenPreset);
			this.targetChild.anchorMin = preset.anchorMin;
			this.targetChild.anchorMax = preset.anchorMax;
			this.targetChild.anchoredPosition = preset.anchoredPosition;
			yield break;
		}

		// Token: 0x0400D273 RID: 53875
		[Header("子节点配置")]
		[SerializeField]
		private RectTransform targetChild;

		// Token: 0x0400D274 RID: 53876
		[Header("布局参数")]
		[SerializeField]
		[Tooltip("触发布局变化的宽度阈值")]
		private float widthThreshold = 512f;

		// Token: 0x0400D275 RID: 53877
		[Header("宽屏模式布局")]
		[SerializeField]
		private ConditionalAlignmentController.AlignmentPreset wideScreenPreset = new ConditionalAlignmentController.AlignmentPreset
		{
			anchorMin = new Vector2(0.5f, 0.5f),
			anchorMax = new Vector2(0.5f, 0.5f),
			anchoredPosition = Vector2.zero
		};

		// Token: 0x0400D276 RID: 53878
		[Header("窄屏模式布局")]
		[SerializeField]
		private ConditionalAlignmentController.AlignmentPreset narrowScreenPreset = new ConditionalAlignmentController.AlignmentPreset
		{
			anchorMin = new Vector2(0f, 0.5f),
			anchorMax = new Vector2(0f, 0.5f),
			anchoredPosition = new Vector2(256f, 0f)
		};

		// Token: 0x0400D277 RID: 53879
		private RectTransform _parentRect;

		// Token: 0x020013F5 RID: 5109
		[Serializable]
		public struct AlignmentPreset
		{
			// Token: 0x0400D278 RID: 53880
			[Tooltip("锚点最小值（左下角）")]
			public Vector2 anchorMin;

			// Token: 0x0400D279 RID: 53881
			[Tooltip("锚点最大值（右上角）")]
			public Vector2 anchorMax;

			// Token: 0x0400D27A RID: 53882
			[Tooltip("相对锚点的偏移量")]
			public Vector2 anchoredPosition;
		}
	}
}

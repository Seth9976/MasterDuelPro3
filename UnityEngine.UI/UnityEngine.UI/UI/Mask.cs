using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000055 RID: 85
	[AddComponentMenu("UI/Mask", 13)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_RectTransform) == null)
				{
					rectTransform = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000FACA File Offset: 0x0000DCCA
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000FB00 File Offset: 0x0000DD00
		public Graphic graphic
		{
			get
			{
				Graphic graphic;
				if ((graphic = this.m_Graphic) == null)
				{
					graphic = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return graphic;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000FB26 File Offset: 0x0000DD26
		protected Mask()
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000FB35 File Offset: 0x0000DD35
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00002209 File Offset: 0x00000409
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000FB50 File Offset: 0x0000DD50
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = true;
				}
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = false;
				}
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000FC4A File Offset: 0x0000DE4A
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!this.MaskEnabled())
			{
				return baseMaterial;
			}
			Transform rootSortCanvas = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, rootSortCanvas);
			if (stencilDepth >= 8)
			{
				Debug.LogWarning("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int desiredStencilBit = 1 << stencilDepth;
			if (desiredStencilBit == 1)
			{
				Material maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0));
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				Material unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = unmaskMaterial;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material maskMaterial2 = StencilMaterial.Add(baseMaterial, desiredStencilBit | (desiredStencilBit - 1), StencilOp.Replace, CompareFunction.Equal, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0), desiredStencilBit - 1, desiredStencilBit | (desiredStencilBit - 1));
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = maskMaterial2;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material unmaskMaterial2 = StencilMaterial.Add(baseMaterial, desiredStencilBit - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, desiredStencilBit - 1, desiredStencilBit | (desiredStencilBit - 1));
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = unmaskMaterial2;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}

		// Token: 0x04000196 RID: 406
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x04000197 RID: 407
		[SerializeField]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x04000198 RID: 408
		[NonSerialized]
		private Graphic m_Graphic;

		// Token: 0x04000199 RID: 409
		[NonSerialized]
		private Material m_MaskMaterial;

		// Token: 0x0400019A RID: 410
		[NonSerialized]
		private Material m_UnmaskMaterial;
	}
}

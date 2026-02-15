using System;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000057 RID: 87
	public abstract class MaskableGraphic : Graphic, IClippable, IMaskable, IMaterialModifier
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00010196 File Offset: 0x0000E396
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0001019E File Offset: 0x0000E39E
		public MaskableGraphic.CullStateChangedEvent onCullStateChanged
		{
			get
			{
				return this.m_OnCullStateChanged;
			}
			set
			{
				this.m_OnCullStateChanged = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000101A7 File Offset: 0x0000E3A7
		// (set) Token: 0x0600034C RID: 844 RVA: 0x000101AF File Offset: 0x0000E3AF
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.m_ShouldRecalculateStencil = true;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000101CF File Offset: 0x0000E3CF
		// (set) Token: 0x0600034E RID: 846 RVA: 0x000101D7 File Offset: 0x0000E3D7
		public bool isMaskingGraphic
		{
			get
			{
				return this.m_IsMaskingGraphic;
			}
			set
			{
				if (value == this.m_IsMaskingGraphic)
				{
					return;
				}
				this.m_IsMaskingGraphic = value;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000101EC File Offset: 0x0000E3EC
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			Material toUse = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				if (this.maskable)
				{
					Transform rootCanvas = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
					this.m_StencilValue = MaskUtilities.GetStencilDepth(base.transform, rootCanvas);
				}
				else
				{
					this.m_StencilValue = 0;
				}
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0 && !this.isMaskingGraphic)
			{
				Material maskMat = StencilMaterial.Add(toUse, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMat;
				toUse = this.m_MaskMaterial;
			}
			return toUse;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001028C File Offset: 0x0000E48C
		public virtual void Cull(Rect clipRect, bool validRect)
		{
			bool cull = !validRect || !clipRect.Overlaps(this.rootCanvasRect, true);
			this.UpdateCull(cull);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private void UpdateCull(bool cull)
		{
			if (base.canvasRenderer.cull != cull)
			{
				base.canvasRenderer.cull = cull;
				UISystemProfilerApi.AddMarker("MaskableGraphic.cullingChanged", this);
				this.m_OnCullStateChanged.Invoke(cull);
				this.OnCullingChanged();
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000102F1 File Offset: 0x0000E4F1
		public virtual void SetClipRect(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.EnableRectClipping(clipRect);
				return;
			}
			base.canvasRenderer.DisableRectClipping();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001030E File Offset: 0x0000E50E
		public virtual void SetClipSoftness(Vector2 clipSoftness)
		{
			base.canvasRenderer.clippingSoftness = clipSoftness;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001031C File Offset: 0x0000E51C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
			if (this.isMaskingGraphic)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010345 File Offset: 0x0000E545
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
			this.UpdateClipParent();
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			if (this.isMaskingGraphic)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010380 File Offset: 0x0000E580
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00002209 File Offset: 0x00000409
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		public virtual void ParentMaskStateChanged()
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000103A4 File Offset: 0x0000E5A4
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000103C8 File Offset: 0x0000E5C8
		private Rect rootCanvasRect
		{
			get
			{
				base.rectTransform.GetWorldCorners(this.m_Corners);
				if (base.canvas)
				{
					Matrix4x4 mat = base.canvas.rootCanvas.transform.worldToLocalMatrix;
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = mat.MultiplyPoint(this.m_Corners[i]);
					}
				}
				Vector2 min = this.m_Corners[0];
				Vector2 max = this.m_Corners[0];
				for (int j = 1; j < 4; j++)
				{
					min.x = Mathf.Min(this.m_Corners[j].x, min.x);
					min.y = Mathf.Min(this.m_Corners[j].y, min.y);
					max.x = Mathf.Max(this.m_Corners[j].x, max.x);
					max.y = Mathf.Max(this.m_Corners[j].y, max.y);
				}
				return new Rect(min, max - min);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001050C File Offset: 0x0000E70C
		private void UpdateClipParent()
		{
			RectMask2D newParent = ((this.maskable && this.IsActive()) ? MaskUtilities.GetRectMaskForClippable(this) : null);
			if (this.m_ParentMask != null && (newParent != this.m_ParentMask || !newParent.IsActive()))
			{
				this.m_ParentMask.RemoveClippable(this);
				this.UpdateCull(false);
			}
			if (newParent != null && newParent.IsActive())
			{
				newParent.AddClippable(this);
			}
			this.m_ParentMask = newParent;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00010589 File Offset: 0x0000E789
		public virtual void RecalculateClipping()
		{
			this.UpdateClipParent();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010591 File Offset: 0x0000E791
		public virtual void RecalculateMasking()
		{
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000105E6 File Offset: 0x0000E7E6
		GameObject IClippable.get_gameObject()
		{
			return base.gameObject;
		}

		// Token: 0x0400019B RID: 411
		[NonSerialized]
		protected bool m_ShouldRecalculateStencil = true;

		// Token: 0x0400019C RID: 412
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x0400019D RID: 413
		[NonSerialized]
		private RectMask2D m_ParentMask;

		// Token: 0x0400019E RID: 414
		[SerializeField]
		private bool m_Maskable = true;

		// Token: 0x0400019F RID: 415
		private bool m_IsMaskingGraphic;

		// Token: 0x040001A0 RID: 416
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x040001A1 RID: 417
		[SerializeField]
		private MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged = new MaskableGraphic.CullStateChangedEvent();

		// Token: 0x040001A2 RID: 418
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore", true)]
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;

		// Token: 0x040001A3 RID: 419
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x040001A4 RID: 420
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x02000058 RID: 88
		[Serializable]
		public class CullStateChangedEvent : UnityEvent<bool>
		{
		}
	}
}

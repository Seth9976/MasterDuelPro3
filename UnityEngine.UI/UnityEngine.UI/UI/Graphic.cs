using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x0200001E RID: 30
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public abstract class Graphic : UIBehaviour, ICanvasElement
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000055F5 File Offset: 0x000037F5
		public static Material defaultGraphicMaterial
		{
			get
			{
				if (Graphic.s_DefaultUI == null)
				{
					Graphic.s_DefaultUI = Canvas.GetDefaultCanvasMaterial();
				}
				return Graphic.s_DefaultUI;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005613 File Offset: 0x00003813
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000561B File Offset: 0x0000381B
		public virtual Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_Color, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005631 File Offset: 0x00003831
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000563C File Offset: 0x0000383C
		public virtual bool raycastTarget
		{
			get
			{
				return this.m_RaycastTarget;
			}
			set
			{
				if (value != this.m_RaycastTarget)
				{
					if (this.m_RaycastTarget)
					{
						GraphicRegistry.UnregisterRaycastGraphicForCanvas(this.canvas, this);
					}
					this.m_RaycastTarget = value;
					if (this.m_RaycastTarget && base.isActiveAndEnabled)
					{
						GraphicRegistry.RegisterRaycastGraphicForCanvas(this.canvas, this);
					}
				}
				this.m_RaycastTargetCache = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005690 File Offset: 0x00003890
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005698 File Offset: 0x00003898
		public Vector4 raycastPadding
		{
			get
			{
				return this.m_RaycastPadding;
			}
			set
			{
				this.m_RaycastPadding = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000056A1 File Offset: 0x000038A1
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000056A9 File Offset: 0x000038A9
		protected bool useLegacyMeshGeneration { get; set; }

		// Token: 0x060000EC RID: 236 RVA: 0x000056B4 File Offset: 0x000038B4
		protected Graphic()
		{
			if (this.m_ColorTweenRunner == null)
			{
				this.m_ColorTweenRunner = new TweenRunner<ColorTween>();
			}
			this.m_ColorTweenRunner.Init(this);
			this.useLegacyMeshGeneration = true;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005706 File Offset: 0x00003906
		public virtual void SetAllDirty()
		{
			if (this.m_SkipLayoutUpdate)
			{
				this.m_SkipLayoutUpdate = false;
			}
			else
			{
				this.SetLayoutDirty();
			}
			if (this.m_SkipMaterialUpdate)
			{
				this.m_SkipMaterialUpdate = false;
			}
			else
			{
				this.SetMaterialDirty();
			}
			this.SetVerticesDirty();
			this.SetRaycastDirty();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005742 File Offset: 0x00003942
		public virtual void SetLayoutDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			if (this.m_OnDirtyLayoutCallback != null)
			{
				this.m_OnDirtyLayoutCallback();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000576B File Offset: 0x0000396B
		public virtual void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_VertsDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyVertsCallback != null)
			{
				this.m_OnDirtyVertsCallback();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005796 File Offset: 0x00003996
		public virtual void SetMaterialDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_MaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000057C4 File Offset: 0x000039C4
		public void SetRaycastDirty()
		{
			if (this.m_RaycastTargetCache != this.m_RaycastTarget)
			{
				if (this.m_RaycastTarget && base.isActiveAndEnabled)
				{
					GraphicRegistry.RegisterRaycastGraphicForCanvas(this.canvas, this);
				}
				else if (!this.m_RaycastTarget)
				{
					GraphicRegistry.UnregisterRaycastGraphicForCanvas(this.canvas, this);
				}
			}
			this.m_RaycastTargetCache = this.m_RaycastTarget;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000581D File Offset: 0x00003A1D
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					this.SetVerticesDirty();
					return;
				}
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005846 File Offset: 0x00003A46
		protected override void OnBeforeTransformParentChanged()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000585F File Offset: 0x00003A5F
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_Canvas = null;
			if (!this.IsActive())
			{
				return;
			}
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			this.SetAllDirty();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000588F File Offset: 0x00003A8F
		public int depth
		{
			get
			{
				return this.canvasRenderer.absoluteDepth;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000589C File Offset: 0x00003A9C
		public RectTransform rectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000058B8 File Offset: 0x00003AB8
		public Canvas canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					this.CacheCanvas();
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000058D4 File Offset: 0x00003AD4
		private void CacheCanvas()
		{
			List<Canvas> list = CollectionPool<List<Canvas>, Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].isActiveAndEnabled)
					{
						this.m_Canvas = list[i];
						break;
					}
					if (i == list.Count - 1)
					{
						this.m_Canvas = null;
					}
				}
			}
			else
			{
				this.m_Canvas = null;
			}
			CollectionPool<List<Canvas>, Canvas>.Release(list);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000594C File Offset: 0x00003B4C
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRenderer == null)
				{
					this.m_CanvasRenderer = base.GetComponent<CanvasRenderer>();
					if (this.m_CanvasRenderer == null)
					{
						this.m_CanvasRenderer = base.gameObject.AddComponent<CanvasRenderer>();
					}
				}
				return this.m_CanvasRenderer;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005981 File Offset: 0x00003B81
		public virtual Material defaultMaterial
		{
			get
			{
				return Graphic.defaultGraphicMaterial;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005988 File Offset: 0x00003B88
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000059A5 File Offset: 0x00003BA5
		public virtual Material material
		{
			get
			{
				if (!(this.m_Material != null))
				{
					return this.defaultMaterial;
				}
				return this.m_Material;
			}
			set
			{
				if (this.m_Material == value)
				{
					return;
				}
				this.m_Material = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000059C4 File Offset: 0x00003BC4
		public virtual Material materialForRendering
		{
			get
			{
				List<IMaterialModifier> components = CollectionPool<List<IMaterialModifier>, IMaterialModifier>.Get();
				base.GetComponents<IMaterialModifier>(components);
				Material currentMat = this.material;
				for (int i = 0; i < components.Count; i++)
				{
					currentMat = components[i].GetModifiedMaterial(currentMat);
				}
				CollectionPool<List<IMaterialModifier>, IMaterialModifier>.Release(components);
				return currentMat;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005A0B File Offset: 0x00003C0B
		public virtual Texture mainTexture
		{
			get
			{
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005A12 File Offset: 0x00003C12
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			if (Graphic.s_WhiteTexture == null)
			{
				Graphic.s_WhiteTexture = Texture2D.whiteTexture;
			}
			this.SetAllDirty();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005A49 File Offset: 0x00003C49
		protected override void OnDisable()
		{
			GraphicRegistry.DisableGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005A87 File Offset: 0x00003C87
		protected override void OnDestroy()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedMesh)
			{
				Object.Destroy(this.m_CachedMesh);
			}
			this.m_CachedMesh = null;
			base.OnDestroy();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005AC0 File Offset: 0x00003CC0
		protected override void OnCanvasHierarchyChanged()
		{
			Canvas currentCanvas = this.m_Canvas;
			this.m_Canvas = null;
			if (!this.IsActive())
			{
				GraphicRegistry.UnregisterGraphicForCanvas(currentCanvas, this);
				return;
			}
			this.CacheCanvas();
			if (currentCanvas != this.m_Canvas)
			{
				GraphicRegistry.UnregisterGraphicForCanvas(currentCanvas, this);
				if (this.IsActive())
				{
					GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B1A File Offset: 0x00003D1A
		public virtual void OnCullingChanged()
		{
			if (!this.canvasRenderer.cull && (this.m_VertsDirty || this.m_MaterialDirty))
			{
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005B40 File Offset: 0x00003D40
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (this.canvasRenderer == null || this.canvasRenderer.cull)
			{
				return;
			}
			if (update == CanvasUpdate.PreRender)
			{
				if (this.m_VertsDirty)
				{
					this.UpdateGeometry();
					this.m_VertsDirty = false;
				}
				if (this.m_MaterialDirty)
				{
					this.UpdateMaterial();
					this.m_MaterialDirty = false;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005B97 File Offset: 0x00003D97
		protected virtual void UpdateMaterial()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(this.materialForRendering, 0);
			this.canvasRenderer.SetTexture(this.mainTexture);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005BD1 File Offset: 0x00003DD1
		protected virtual void UpdateGeometry()
		{
			if (this.useLegacyMeshGeneration)
			{
				this.DoLegacyMeshGeneration();
				return;
			}
			this.DoMeshGeneration();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005BE8 File Offset: 0x00003DE8
		private void DoMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.s_VertexHelper);
			}
			else
			{
				Graphic.s_VertexHelper.Clear();
			}
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			base.GetComponents(typeof(IMeshModifier), components);
			for (int i = 0; i < components.Count; i++)
			{
				((IMeshModifier)components[i]).ModifyMesh(Graphic.s_VertexHelper);
			}
			CollectionPool<List<Component>, Component>.Release(components);
			Graphic.s_VertexHelper.FillMesh(Graphic.workerMesh);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005CB4 File Offset: 0x00003EB4
		private void DoLegacyMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.workerMesh);
			}
			else
			{
				Graphic.workerMesh.Clear();
			}
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			base.GetComponents(typeof(IMeshModifier), components);
			for (int i = 0; i < components.Count; i++)
			{
				((IMeshModifier)components[i]).ModifyMesh(Graphic.workerMesh);
			}
			CollectionPool<List<Component>, Component>.Release(components);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005D6E File Offset: 0x00003F6E
		protected static Mesh workerMesh
		{
			get
			{
				if (Graphic.s_Mesh == null)
				{
					Graphic.s_Mesh = new Mesh();
					Graphic.s_Mesh.name = "Shared UI Mesh";
				}
				return Graphic.s_Mesh;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002209 File Offset: 0x00000409
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use OnPopulateMesh instead.", true)]
		protected virtual void OnFillVBO(List<UIVertex> vbo)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005D9B File Offset: 0x00003F9B
		[Obsolete("Use OnPopulateMesh(VertexHelper vh) instead.", false)]
		protected virtual void OnPopulateMesh(Mesh m)
		{
			this.OnPopulateMesh(Graphic.s_VertexHelper);
			Graphic.s_VertexHelper.FillMesh(m);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005DB4 File Offset: 0x00003FB4
		protected virtual void OnPopulateMesh(VertexHelper vh)
		{
			Rect r = this.GetPixelAdjustedRect();
			Vector4 v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
			Color32 color32 = this.color;
			vh.Clear();
			vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1f, 1f));
			vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005ECF File Offset: 0x000040CF
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetAllDirty();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void SetNativeSize()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005ED8 File Offset: 0x000040D8
		public virtual bool Raycast(Vector2 sp, Camera eventCamera)
		{
			if (!base.isActiveAndEnabled)
			{
				return false;
			}
			Transform t = base.transform;
			List<Component> components = CollectionPool<List<Component>, Component>.Get();
			bool ignoreParentGroups = false;
			bool continueTraversal = true;
			while (t != null)
			{
				t.GetComponents<Component>(components);
				for (int i = 0; i < components.Count; i++)
				{
					Canvas canvas = components[i] as Canvas;
					if (canvas != null && canvas.overrideSorting)
					{
						continueTraversal = false;
					}
					ICanvasRaycastFilter filter = components[i] as ICanvasRaycastFilter;
					if (filter != null)
					{
						bool raycastValid = true;
						CanvasGroup group = components[i] as CanvasGroup;
						if (group != null)
						{
							if (!group.enabled)
							{
								goto IL_00CD;
							}
							if (!ignoreParentGroups && group.ignoreParentGroups)
							{
								ignoreParentGroups = true;
								raycastValid = filter.IsRaycastLocationValid(sp, eventCamera);
							}
							else if (!ignoreParentGroups)
							{
								raycastValid = filter.IsRaycastLocationValid(sp, eventCamera);
							}
						}
						else
						{
							raycastValid = filter.IsRaycastLocationValid(sp, eventCamera);
						}
						if (!raycastValid)
						{
							CollectionPool<List<Component>, Component>.Release(components);
							return false;
						}
					}
					IL_00CD:;
				}
				t = (continueTraversal ? t.parent : null);
			}
			CollectionPool<List<Component>, Component>.Release(components);
			return true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005FE8 File Offset: 0x000041E8
		public Vector2 PixelAdjustPoint(Vector2 point)
		{
			if (!this.canvas || this.canvas.renderMode == RenderMode.WorldSpace || this.canvas.scaleFactor == 0f || !this.canvas.pixelPerfect)
			{
				return point;
			}
			return RectTransformUtility.PixelAdjustPoint(point, base.transform, this.canvas);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006044 File Offset: 0x00004244
		public Rect GetPixelAdjustedRect()
		{
			if (!this.canvas || this.canvas.renderMode == RenderMode.WorldSpace || this.canvas.scaleFactor == 0f || !this.canvas.pixelPerfect)
			{
				return this.rectTransform.rect;
			}
			return RectTransformUtility.PixelAdjustRect(this.rectTransform, this.canvas);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000060A8 File Offset: 0x000042A8
		public virtual void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			this.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, true);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000060B8 File Offset: 0x000042B8
		public virtual void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (this.canvasRenderer == null || (!useRGB && !useAlpha))
			{
				return;
			}
			if (this.canvasRenderer.GetColor().Equals(targetColor))
			{
				this.m_ColorTweenRunner.StopTween();
				return;
			}
			ColorTween.ColorTweenMode mode = ((useRGB && useAlpha) ? ColorTween.ColorTweenMode.All : (useRGB ? ColorTween.ColorTweenMode.RGB : ColorTween.ColorTweenMode.Alpha));
			ColorTween colorTween = new ColorTween
			{
				duration = duration,
				startColor = this.canvasRenderer.GetColor(),
				targetColor = targetColor
			};
			colorTween.AddOnChangedCallback(new UnityAction<Color>(this.canvasRenderer.SetColor));
			colorTween.ignoreTimeScale = ignoreTimeScale;
			colorTween.tweenMode = mode;
			this.m_ColorTweenRunner.StartTween(colorTween);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006174 File Offset: 0x00004374
		private static Color CreateColorFromAlpha(float alpha)
		{
			Color alphaColor = Color.black;
			alphaColor.a = alpha;
			return alphaColor;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006190 File Offset: 0x00004390
		public virtual void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this.CrossFadeColor(Graphic.CreateColorFromAlpha(alpha), duration, ignoreTimeScale, true, false);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000061A2 File Offset: 0x000043A2
		public void RegisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000061BB File Offset: 0x000043BB
		public void UnregisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000061D4 File Offset: 0x000043D4
		public void RegisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000061ED File Offset: 0x000043ED
		public void UnregisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006206 File Offset: 0x00004406
		public void RegisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000621F File Offset: 0x0000441F
		public void UnregisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400006D RID: 109
		protected static Material s_DefaultUI = null;

		// Token: 0x0400006E RID: 110
		protected static Texture2D s_WhiteTexture = null;

		// Token: 0x0400006F RID: 111
		[FormerlySerializedAs("m_Mat")]
		[SerializeField]
		protected Material m_Material;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x04000071 RID: 113
		[NonSerialized]
		protected bool m_SkipLayoutUpdate;

		// Token: 0x04000072 RID: 114
		[NonSerialized]
		protected bool m_SkipMaterialUpdate;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private bool m_RaycastTarget = true;

		// Token: 0x04000074 RID: 116
		private bool m_RaycastTargetCache = true;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		private Vector4 m_RaycastPadding;

		// Token: 0x04000076 RID: 118
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x04000077 RID: 119
		[NonSerialized]
		private CanvasRenderer m_CanvasRenderer;

		// Token: 0x04000078 RID: 120
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x04000079 RID: 121
		[NonSerialized]
		private bool m_VertsDirty;

		// Token: 0x0400007A RID: 122
		[NonSerialized]
		private bool m_MaterialDirty;

		// Token: 0x0400007B RID: 123
		[NonSerialized]
		protected UnityAction m_OnDirtyLayoutCallback;

		// Token: 0x0400007C RID: 124
		[NonSerialized]
		protected UnityAction m_OnDirtyVertsCallback;

		// Token: 0x0400007D RID: 125
		[NonSerialized]
		protected UnityAction m_OnDirtyMaterialCallback;

		// Token: 0x0400007E RID: 126
		[NonSerialized]
		protected static Mesh s_Mesh;

		// Token: 0x0400007F RID: 127
		[NonSerialized]
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x04000080 RID: 128
		[NonSerialized]
		protected Mesh m_CachedMesh;

		// Token: 0x04000081 RID: 129
		[NonSerialized]
		protected Vector2[] m_CachedUvs;

		// Token: 0x04000082 RID: 130
		[NonSerialized]
		private readonly TweenRunner<ColorTween> m_ColorTweenRunner;
	}
}

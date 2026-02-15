using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	[RequireComponent(typeof(Canvas))]
	[ExecuteAlways]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[DisallowMultipleComponent]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000D744 File Offset: 0x0000B944
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000D74C File Offset: 0x0000B94C
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D755 File Offset: 0x0000B955
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000D75D File Offset: 0x0000B95D
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D766 File Offset: 0x0000B966
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000D76E File Offset: 0x0000B96E
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = Mathf.Max(0.01f, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000D781 File Offset: 0x0000B981
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000D78C File Offset: 0x0000B98C
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000D82A File Offset: 0x0000BA2A
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000D832 File Offset: 0x0000BA32
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000D83B File Offset: 0x0000BA3B
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000D843 File Offset: 0x0000BA43
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000D84C File Offset: 0x0000BA4C
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000D854 File Offset: 0x0000BA54
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D85D File Offset: 0x0000BA5D
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000D865 File Offset: 0x0000BA65
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000D86E File Offset: 0x0000BA6E
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000D876 File Offset: 0x0000BA76
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = Mathf.Max(1f, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000D889 File Offset: 0x0000BA89
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000D891 File Offset: 0x0000BA91
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D89C File Offset: 0x0000BA9C
		protected CanvasScaler()
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D918 File Offset: 0x0000BB18
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
			Canvas.preWillRenderCanvases += this.Canvas_preWillRenderCanvases;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D943 File Offset: 0x0000BB43
		private void Canvas_preWillRenderCanvases()
		{
			this.Handle();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D94B File Offset: 0x0000BB4B
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			Canvas.preWillRenderCanvases -= this.Canvas_preWillRenderCanvases;
			base.OnDisable();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D97C File Offset: 0x0000BB7C
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				return;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				return;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000DA02 File Offset: 0x0000BC02
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 screenSize = this.m_Canvas.renderingDisplaySize;
			int displayIndex = this.m_Canvas.targetDisplay;
			if (displayIndex > 0 && displayIndex < Display.displays.Length)
			{
				Display disp = Display.displays[displayIndex];
				screenSize = new Vector2((float)disp.renderingWidth, (float)disp.renderingHeight);
			}
			float scaleFactor = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float num = Mathf.Log(screenSize.x / this.m_ReferenceResolution.x, 2f);
				float logHeight = Mathf.Log(screenSize.y / this.m_ReferenceResolution.y, 2f);
				float logWeightedAverage = Mathf.Lerp(num, logHeight, this.m_MatchWidthOrHeight);
				scaleFactor = Mathf.Pow(2f, logWeightedAverage);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				scaleFactor = Mathf.Min(screenSize.x / this.m_ReferenceResolution.x, screenSize.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				scaleFactor = Mathf.Max(screenSize.x / this.m_ReferenceResolution.x, screenSize.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(scaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DB54 File Offset: 0x0000BD54
		protected virtual void HandleConstantPhysicalSize()
		{
			float currentDpi = Screen.dpi;
			float dpi = ((currentDpi == 0f) ? this.m_FallbackScreenDPI : currentDpi);
			float targetDPI = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				targetDPI = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				targetDPI = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				targetDPI = 1f;
				break;
			case CanvasScaler.Unit.Points:
				targetDPI = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				targetDPI = 6f;
				break;
			}
			this.SetScaleFactor(dpi / targetDPI);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * targetDPI / this.m_DefaultSpriteDPI);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DBE6 File Offset: 0x0000BDE6
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DC05 File Offset: 0x0000BE05
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x04000132 RID: 306
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x04000133 RID: 307
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x04000134 RID: 308
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x04000135 RID: 309
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x04000136 RID: 310
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x04000137 RID: 311
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x04000138 RID: 312
		private const float kLogBase = 2f;

		// Token: 0x04000139 RID: 313
		[Tooltip("The physical unit to specify positions and sizes in.")]
		[SerializeField]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x0400013A RID: 314
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x0400013B RID: 315
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x0400013C RID: 316
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x0400013D RID: 317
		private Canvas m_Canvas;

		// Token: 0x0400013E RID: 318
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x0400013F RID: 319
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x04000140 RID: 320
		[SerializeField]
		protected bool m_PresetInfoIsWorld;

		// Token: 0x0200003D RID: 61
		public enum ScaleMode
		{
			// Token: 0x04000142 RID: 322
			ConstantPixelSize,
			// Token: 0x04000143 RID: 323
			ScaleWithScreenSize,
			// Token: 0x04000144 RID: 324
			ConstantPhysicalSize
		}

		// Token: 0x0200003E RID: 62
		public enum ScreenMatchMode
		{
			// Token: 0x04000146 RID: 326
			MatchWidthOrHeight,
			// Token: 0x04000147 RID: 327
			Expand,
			// Token: 0x04000148 RID: 328
			Shrink
		}

		// Token: 0x0200003F RID: 63
		public enum Unit
		{
			// Token: 0x0400014A RID: 330
			Centimeters,
			// Token: 0x0400014B RID: 331
			Millimeters,
			// Token: 0x0400014C RID: 332
			Inches,
			// Token: 0x0400014D RID: 333
			Points,
			// Token: 0x0400014E RID: 334
			Picas
		}
	}
}

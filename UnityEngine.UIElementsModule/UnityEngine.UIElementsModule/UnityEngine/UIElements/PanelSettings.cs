using System;
using System.Diagnostics;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000254 RID: 596
	[HelpURL("UIE-Runtime-Panel-Settings")]
	public class PanelSettings : ScriptableObject
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00044F00 File Offset: 0x00043100
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x00044F18 File Offset: 0x00043118
		public ThemeStyleSheet themeStyleSheet
		{
			get
			{
				return this.themeUss;
			}
			set
			{
				this.themeUss = value;
				this.ApplyThemeStyleSheet(null);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00044F2A File Offset: 0x0004312A
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x00044F32 File Offset: 0x00043132
		public RenderTexture targetTexture
		{
			get
			{
				return this.m_TargetTexture;
			}
			set
			{
				this.m_TargetTexture = value;
				this.m_PanelAccess.SetTargetTexture();
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00044F48 File Offset: 0x00043148
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00044F50 File Offset: 0x00043150
		internal PanelRenderMode renderMode
		{
			get
			{
				return this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00044F59 File Offset: 0x00043159
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00044F61 File Offset: 0x00043161
		internal int worldSpaceLayer
		{
			get
			{
				return this.m_WorldSpaceLayer;
			}
			set
			{
				this.m_WorldSpaceLayer = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00044F6A File Offset: 0x0004316A
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x00044F72 File Offset: 0x00043172
		public PanelScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleMode = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00044F7C File Offset: 0x0004317C
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x00044F94 File Offset: 0x00043194
		public float referenceSpritePixelsPerUnit
		{
			get
			{
				return this.m_ReferenceSpritePixelsPerUnit;
			}
			set
			{
				this.m_ReferenceSpritePixelsPerUnit = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00044FA0 File Offset: 0x000431A0
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x00044FB8 File Offset: 0x000431B8
		internal float pixelsPerUnit
		{
			get
			{
				return this.m_PixelsPerUnit;
			}
			set
			{
				this.m_PixelsPerUnit = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00044FC2 File Offset: 0x000431C2
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x00044FCA File Offset: 0x000431CA
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00044FD3 File Offset: 0x000431D3
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00044FDB File Offset: 0x000431DB
		public float referenceDpi
		{
			get
			{
				return this.m_ReferenceDpi;
			}
			set
			{
				this.m_ReferenceDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00044FF3 File Offset: 0x000431F3
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x00044FFB File Offset: 0x000431FB
		public float fallbackDpi
		{
			get
			{
				return this.m_FallbackDpi;
			}
			set
			{
				this.m_FallbackDpi = ((value >= 1f) ? value : 96f);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00045013 File Offset: 0x00043213
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0004501B File Offset: 0x0004321B
		public Vector2Int referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00045024 File Offset: 0x00043224
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0004502C File Offset: 0x0004322C
		public PanelScreenMatchMode screenMatchMode
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00045035 File Offset: 0x00043235
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0004503D File Offset: 0x0004323D
		public float match
		{
			get
			{
				return this.m_Match;
			}
			set
			{
				this.m_Match = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00045046 File Offset: 0x00043246
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0004504E File Offset: 0x0004324E
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				this.m_SortingOrder = value;
				this.ApplySortingOrder();
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004505F File Offset: 0x0004325F
		internal void ApplySortingOrder()
		{
			this.m_PanelAccess.SetSortingPriority();
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0004506E File Offset: 0x0004326E
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x00045076 File Offset: 0x00043276
		public int targetDisplay
		{
			get
			{
				return this.m_TargetDisplay;
			}
			set
			{
				this.m_TargetDisplay = value;
				this.m_PanelAccess.SetTargetDisplay();
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0004508C File Offset: 0x0004328C
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00045094 File Offset: 0x00043294
		public BindingLogLevel bindingLogLevel
		{
			get
			{
				return this.m_BindingLogLevel;
			}
			set
			{
				bool flag = this.m_BindingLogLevel == value;
				if (!flag)
				{
					this.m_BindingLogLevel = value;
					Binding.SetPanelLogLevel(this.panel, value);
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x000450C5 File Offset: 0x000432C5
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x000450CD File Offset: 0x000432CD
		public bool clearDepthStencil
		{
			get
			{
				return this.m_ClearDepthStencil;
			}
			set
			{
				this.m_ClearDepthStencil = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x000450D6 File Offset: 0x000432D6
		public float depthClearValue
		{
			get
			{
				return 0.99f;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000450DD File Offset: 0x000432DD
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x000450E5 File Offset: 0x000432E5
		public bool clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000450EE File Offset: 0x000432EE
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x000450F6 File Offset: 0x000432F6
		public Color colorClearValue
		{
			get
			{
				return this.m_ColorClearValue;
			}
			set
			{
				this.m_ColorClearValue = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000450FF File Offset: 0x000432FF
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00045107 File Offset: 0x00043307
		public uint vertexBudget
		{
			get
			{
				return this.m_VertexBudget;
			}
			set
			{
				this.m_VertexBudget = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00045110 File Offset: 0x00043310
		internal BaseRuntimePanel panel
		{
			get
			{
				return this.m_PanelAccess.panel;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0004511D File Offset: 0x0004331D
		internal bool isInitialized
		{
			get
			{
				PanelSettings.RuntimePanelAccess panelAccess = this.m_PanelAccess;
				return panelAccess != null && panelAccess.isInitialized;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00045131 File Offset: 0x00043331
		internal VisualElement visualTree
		{
			get
			{
				return this.m_PanelAccess.panel.visualTree;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00045143 File Offset: 0x00043343
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0004514B File Offset: 0x0004334B
		public DynamicAtlasSettings dynamicAtlasSettings
		{
			get
			{
				return this.m_DynamicAtlasSettings;
			}
			set
			{
				this.m_DynamicAtlasSettings = value;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00045154 File Offset: 0x00043354
		private PanelSettings()
		{
			this.m_PanelAccess = new PanelSettings.RuntimePanelAccess(this);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000020EA File Offset: 0x000002EA
		private void Reset()
		{
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004522C File Offset: 0x0004342C
		private void OnEnable()
		{
			bool flag = !this.m_DisableNoThemeWarning && this.themeUss == null;
			if (flag)
			{
				Debug.LogWarning("No Theme Style Sheet set to PanelSettings " + base.name + ", UI will not render properly", this);
			}
			this.UpdateScreenDPI();
			this.InitializeShaders();
			this.AssignICUData();
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004528A File Offset: 0x0004348A
		private void OnDisable()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0004528A File Offset: 0x0004348A
		internal void DisposePanel()
		{
			this.m_PanelAccess.DisposePanel();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00045299 File Offset: 0x00043499
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x000452A1 File Offset: 0x000434A1
		private float ScreenDPI { get; set; }

		// Token: 0x0600103A RID: 4154 RVA: 0x000452AA File Offset: 0x000434AA
		[Conditional("ENABLE_PROFILER")]
		public void SetPanelChangeReceiver(IDebugPanelChangeReceiver value)
		{
			this.m_PanelChangeReceiver = value;
			this.m_PanelAccess.SetPanelChangeReceiver();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000452C0 File Offset: 0x000434C0
		internal IDebugPanelChangeReceiver GetPanelChangeReceiver()
		{
			return this.m_PanelChangeReceiver;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000452D8 File Offset: 0x000434D8
		internal void UpdateScreenDPI()
		{
			this.ScreenDPI = Screen.dpi;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000452E8 File Offset: 0x000434E8
		private void ApplyThemeStyleSheet(VisualElement root = null)
		{
			bool flag = !this.m_PanelAccess.isInitialized;
			if (!flag)
			{
				bool flag2 = root == null;
				if (flag2)
				{
					root = this.visualTree;
				}
				bool flag3 = this.m_OldThemeUss != this.themeUss && this.m_OldThemeUss != null;
				if (flag3)
				{
					if (root != null)
					{
						root.styleSheets.Remove(this.m_OldThemeUss);
					}
				}
				bool flag4 = this.themeUss != null;
				if (flag4)
				{
					this.themeUss.isDefaultStyleSheet = true;
					if (root != null)
					{
						root.styleSheets.Add(this.themeUss);
					}
				}
				this.m_OldThemeUss = this.themeUss;
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000453AC File Offset: 0x000435AC
		private bool AssignICUData()
		{
			return false;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000453C0 File Offset: 0x000435C0
		private void InitializeShaders()
		{
			bool flag = this.m_AtlasBlitShader == null;
			if (flag)
			{
				this.m_AtlasBlitShader = Shader.Find(Shaders.k_AtlasBlit);
			}
			bool flag2 = this.m_RuntimeShader == null;
			if (flag2)
			{
				this.m_RuntimeShader = Shader.Find(Shaders.k_Runtime);
			}
			bool flag3 = this.m_RuntimeWorldShader == null;
			if (flag3)
			{
				this.m_RuntimeWorldShader = Shader.Find(Shaders.k_RuntimeWorld);
			}
			this.m_PanelAccess.SetTargetTexture();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00045440 File Offset: 0x00043640
		internal void ApplyPanelSettings()
		{
			Rect oldTargetRect = this.m_TargetRect;
			float oldResolvedScaling = this.m_ResolvedScale;
			this.UpdateScreenDPI();
			this.m_TargetRect = this.GetDisplayRect();
			bool flag = this.renderMode == PanelRenderMode.WorldSpace;
			if (flag)
			{
				this.m_ResolvedScale = 1f;
			}
			else
			{
				this.m_ResolvedScale = this.ResolveScale(this.m_TargetRect, this.ScreenDPI);
			}
			BaseRuntimePanel p = this.panel;
			bool flag2 = this.renderMode != PanelRenderMode.WorldSpace;
			if (flag2)
			{
				bool flag3 = this.visualTree.style.width.value == 0f || this.m_ResolvedScale != oldResolvedScaling || this.m_TargetRect.width != oldTargetRect.width || this.m_TargetRect.height != oldTargetRect.height;
				if (flag3)
				{
					p.scale = ((this.m_ResolvedScale == 0f) ? 0f : (1f / this.m_ResolvedScale));
					this.visualTree.style.left = 0f;
					this.visualTree.style.top = 0f;
					this.visualTree.style.width = this.m_TargetRect.width * this.m_ResolvedScale;
					this.visualTree.style.height = this.m_TargetRect.height * this.m_ResolvedScale;
				}
				p.panelRenderer.forceGammaRendering = this.targetTexture != null && this.forceGammaRendering;
			}
			p.targetTexture = this.targetTexture;
			p.targetDisplay = this.targetDisplay;
			p.drawsInCameras = this.renderMode == PanelRenderMode.WorldSpace;
			p.pixelsPerUnit = this.pixelsPerUnit;
			p.isFlat = this.renderMode != PanelRenderMode.WorldSpace;
			p.worldSpaceLayer = this.worldSpaceLayer;
			p.clearSettings = new PanelClearSettings
			{
				clearColor = this.m_ClearColor,
				clearDepthStencil = this.m_ClearDepthStencil,
				color = this.m_ColorClearValue
			};
			p.referenceSpritePixelsPerUnit = this.referenceSpritePixelsPerUnit;
			p.panelRenderer.vertexBudget = this.m_VertexBudget;
			p.dataBindingManager.logLevel = this.m_BindingLogLevel;
			DynamicAtlas atlas = p.atlas as DynamicAtlas;
			bool flag4 = atlas != null;
			if (flag4)
			{
				atlas.minAtlasSize = this.dynamicAtlasSettings.minAtlasSize;
				atlas.maxAtlasSize = this.dynamicAtlasSettings.maxAtlasSize;
				atlas.maxSubTextureSize = this.dynamicAtlasSettings.maxSubTextureSize;
				atlas.activeFilters = this.dynamicAtlasSettings.activeFilters;
				atlas.customFilter = this.dynamicAtlasSettings.customFilter;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004572D File Offset: 0x0004392D
		public void SetScreenToPanelSpaceFunction(Func<Vector2, Vector2> screentoPanelSpaceFunction)
		{
			this.m_AssignedScreenToPanel = screentoPanelSpaceFunction;
			this.panel.screenToPanelSpace = this.m_AssignedScreenToPanel;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004574C File Offset: 0x0004394C
		internal float ResolveScale(Rect targetRect, float screenDpi)
		{
			float resolvedScale = 1f;
			switch (this.scaleMode)
			{
			case PanelScaleMode.ConstantPhysicalSize:
			{
				float dpi = ((screenDpi == 0f) ? this.fallbackDpi : screenDpi);
				bool flag = dpi != 0f;
				if (flag)
				{
					resolvedScale = this.referenceDpi / dpi;
				}
				break;
			}
			case PanelScaleMode.ScaleWithScreenSize:
			{
				bool flag2 = this.referenceResolution.x * this.referenceResolution.y != 0;
				if (flag2)
				{
					Vector2 refSize = this.referenceResolution;
					Vector2 sizeRatio = new Vector2(targetRect.width / refSize.x, targetRect.height / refSize.y);
					PanelScreenMatchMode screenMatchMode = this.screenMatchMode;
					PanelScreenMatchMode panelScreenMatchMode = screenMatchMode;
					float denominator;
					if (panelScreenMatchMode != PanelScreenMatchMode.Shrink)
					{
						if (panelScreenMatchMode != PanelScreenMatchMode.Expand)
						{
							float widthHeightRatio = Mathf.Clamp01(this.match);
							denominator = Mathf.Lerp(sizeRatio.x, sizeRatio.y, widthHeightRatio);
						}
						else
						{
							denominator = Mathf.Min(sizeRatio.x, sizeRatio.y);
						}
					}
					else
					{
						denominator = Mathf.Max(sizeRatio.x, sizeRatio.y);
					}
					bool flag3 = denominator != 0f;
					if (flag3)
					{
						resolvedScale = 1f / denominator;
					}
				}
				break;
			}
			}
			bool flag4 = this.scale > 0f;
			if (flag4)
			{
				resolvedScale /= this.scale;
			}
			else
			{
				resolvedScale = 0f;
			}
			return resolvedScale;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000458D8 File Offset: 0x00043AD8
		internal Rect GetDisplayRect()
		{
			bool flag = this.m_TargetTexture != null;
			Rect rect;
			if (flag)
			{
				rect = new Rect(0f, 0f, (float)this.m_TargetTexture.width, (float)this.m_TargetTexture.height);
			}
			else
			{
				rect = new Rect(0f, 0f, (float)BaseRuntimePanel.getScreenRenderingWidth(this.targetDisplay), (float)BaseRuntimePanel.getScreenRenderingHeight(this.targetDisplay));
			}
			return rect;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004594C File Offset: 0x00043B4C
		internal void AttachAndInsertUIDocumentToVisualTree(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (flag)
			{
				this.m_AttachedUIDocumentsList = new UIDocumentList();
			}
			else
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
			}
			this.m_AttachedUIDocumentsList.AddToListAndToVisualTree(uiDocument, this.visualTree, 0);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004599C File Offset: 0x00043B9C
		internal void DetachUIDocument(UIDocument uiDocument)
		{
			bool flag = this.m_AttachedUIDocumentsList == null;
			if (!flag)
			{
				this.m_AttachedUIDocumentsList.RemoveFromListAndFromVisualTree(uiDocument);
				bool flag2 = this.m_AttachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag2)
				{
					this.m_PanelAccess.MarkPotentiallyEmpty();
				}
			}
		}

		// Token: 0x04000905 RID: 2309
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x04000906 RID: 2310
		private const float k_DefaultScaleValue = 1f;

		// Token: 0x04000907 RID: 2311
		internal const string k_DefaultStyleSheetPath = "Packages/com.unity.ui/PackageResources/StyleSheets/Generated/Default.tss.asset";

		// Token: 0x04000908 RID: 2312
		[SerializeField]
		private ThemeStyleSheet themeUss;

		// Token: 0x04000909 RID: 2313
		[SerializeField]
		private bool m_DisableNoThemeWarning = false;

		// Token: 0x0400090A RID: 2314
		[SerializeField]
		private RenderTexture m_TargetTexture;

		// Token: 0x0400090B RID: 2315
		[SerializeField]
		private PanelRenderMode m_RenderMode = PanelRenderMode.ScreenSpaceOverlay;

		// Token: 0x0400090C RID: 2316
		[SerializeField]
		private int m_WorldSpaceLayer = 0;

		// Token: 0x0400090D RID: 2317
		[SerializeField]
		private PanelScaleMode m_ScaleMode = PanelScaleMode.ConstantPhysicalSize;

		// Token: 0x0400090E RID: 2318
		[SerializeField]
		private float m_ReferenceSpritePixelsPerUnit = 100f;

		// Token: 0x0400090F RID: 2319
		[SerializeField]
		private float m_PixelsPerUnit = 100f;

		// Token: 0x04000910 RID: 2320
		[SerializeField]
		private float m_Scale = 1f;

		// Token: 0x04000911 RID: 2321
		private const float DefaultDpi = 96f;

		// Token: 0x04000912 RID: 2322
		[SerializeField]
		private float m_ReferenceDpi = 96f;

		// Token: 0x04000913 RID: 2323
		[SerializeField]
		private float m_FallbackDpi = 96f;

		// Token: 0x04000914 RID: 2324
		[SerializeField]
		private Vector2Int m_ReferenceResolution = new Vector2Int(1200, 800);

		// Token: 0x04000915 RID: 2325
		[SerializeField]
		private PanelScreenMatchMode m_ScreenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;

		// Token: 0x04000916 RID: 2326
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Match = 0f;

		// Token: 0x04000917 RID: 2327
		[SerializeField]
		private float m_SortingOrder = 0f;

		// Token: 0x04000918 RID: 2328
		[SerializeField]
		private int m_TargetDisplay = 0;

		// Token: 0x04000919 RID: 2329
		[SerializeField]
		private BindingLogLevel m_BindingLogLevel;

		// Token: 0x0400091A RID: 2330
		[SerializeField]
		private bool m_ClearDepthStencil = true;

		// Token: 0x0400091B RID: 2331
		[SerializeField]
		private bool m_ClearColor;

		// Token: 0x0400091C RID: 2332
		[SerializeField]
		private Color m_ColorClearValue = Color.clear;

		// Token: 0x0400091D RID: 2333
		[SerializeField]
		private uint m_VertexBudget = 0U;

		// Token: 0x0400091E RID: 2334
		private PanelSettings.RuntimePanelAccess m_PanelAccess;

		// Token: 0x0400091F RID: 2335
		internal UIDocumentList m_AttachedUIDocumentsList;

		// Token: 0x04000920 RID: 2336
		[HideInInspector]
		[SerializeField]
		private DynamicAtlasSettings m_DynamicAtlasSettings = DynamicAtlasSettings.defaults;

		// Token: 0x04000921 RID: 2337
		[SerializeField]
		[HideInInspector]
		private Shader m_AtlasBlitShader;

		// Token: 0x04000922 RID: 2338
		[HideInInspector]
		[SerializeField]
		private Shader m_RuntimeShader;

		// Token: 0x04000923 RID: 2339
		[SerializeField]
		[HideInInspector]
		private Shader m_RuntimeWorldShader;

		// Token: 0x04000924 RID: 2340
		[SerializeField]
		[HideInInspector]
		internal TextAsset m_ICUDataAsset;

		// Token: 0x04000925 RID: 2341
		[SerializeField]
		public bool forceGammaRendering;

		// Token: 0x04000926 RID: 2342
		[SerializeField]
		public PanelTextSettings textSettings;

		// Token: 0x04000927 RID: 2343
		private Rect m_TargetRect;

		// Token: 0x04000928 RID: 2344
		private float m_ResolvedScale;

		// Token: 0x04000929 RID: 2345
		private StyleSheet m_OldThemeUss;

		// Token: 0x0400092B RID: 2347
		private IDebugPanelChangeReceiver m_PanelChangeReceiver = null;

		// Token: 0x0400092C RID: 2348
		private Func<Vector2, Vector2> m_AssignedScreenToPanel;

		// Token: 0x02000255 RID: 597
		private class RuntimePanelAccess
		{
			// Token: 0x06001046 RID: 4166 RVA: 0x000459EA File Offset: 0x00043BEA
			internal RuntimePanelAccess(PanelSettings settings)
			{
				this.m_Settings = settings;
			}

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06001047 RID: 4167 RVA: 0x000459FB File Offset: 0x00043BFB
			internal bool isInitialized
			{
				get
				{
					return this.m_RuntimePanel != null;
				}
			}

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06001048 RID: 4168 RVA: 0x00045A08 File Offset: 0x00043C08
			internal BaseRuntimePanel panel
			{
				get
				{
					bool flag = this.m_RuntimePanel == null;
					if (flag)
					{
						this.m_RuntimePanel = this.CreateRelatedRuntimePanel();
						this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
						this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
						this.m_RuntimePanel.panelChangeReceiver = this.m_Settings.GetPanelChangeReceiver();
						VisualElement root = this.m_RuntimePanel.visualTree;
						root.name = this.m_Settings.name;
						this.m_Settings.ApplyPanelSettings();
						this.m_Settings.ApplyThemeStyleSheet(root);
						bool flag2 = this.m_Settings.m_TargetTexture != null;
						if (flag2)
						{
							this.m_RuntimePanel.targetTexture = this.m_Settings.m_TargetTexture;
						}
						bool flag3 = this.m_Settings.m_AssignedScreenToPanel != null;
						if (flag3)
						{
							this.m_Settings.SetScreenToPanelSpaceFunction(this.m_Settings.m_AssignedScreenToPanel);
						}
					}
					return this.m_RuntimePanel;
				}
			}

			// Token: 0x06001049 RID: 4169 RVA: 0x00045B14 File Offset: 0x00043D14
			internal void DisposePanel()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.DisposeRelatedPanel();
					this.m_RuntimePanel = null;
				}
			}

			// Token: 0x0600104A RID: 4170 RVA: 0x00045B40 File Offset: 0x00043D40
			internal void SetTargetTexture()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetTexture = this.m_Settings.targetTexture;
				}
			}

			// Token: 0x0600104B RID: 4171 RVA: 0x00045B74 File Offset: 0x00043D74
			internal void SetSortingPriority()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.sortingPriority = this.m_Settings.m_SortingOrder;
				}
			}

			// Token: 0x0600104C RID: 4172 RVA: 0x00045BA8 File Offset: 0x00043DA8
			internal void SetTargetDisplay()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.targetDisplay = this.m_Settings.m_TargetDisplay;
				}
			}

			// Token: 0x0600104D RID: 4173 RVA: 0x00045BDC File Offset: 0x00043DDC
			internal void SetPanelChangeReceiver()
			{
				bool flag = this.m_RuntimePanel != null;
				if (flag)
				{
					this.m_RuntimePanel.panelChangeReceiver = this.m_Settings.m_PanelChangeReceiver;
				}
			}

			// Token: 0x0600104E RID: 4174 RVA: 0x00045C10 File Offset: 0x00043E10
			private BaseRuntimePanel CreateRelatedRuntimePanel()
			{
				return (RuntimePanel)UIElementsRuntimeUtility.FindOrCreateRuntimePanel(this.m_Settings, new UIElementsRuntimeUtility.CreateRuntimePanelDelegate(RuntimePanel.Create));
			}

			// Token: 0x0600104F RID: 4175 RVA: 0x00045C40 File Offset: 0x00043E40
			private void DisposeRelatedPanel()
			{
				UIElementsRuntimeUtility.DisposeRuntimePanel(this.m_Settings);
			}

			// Token: 0x06001050 RID: 4176 RVA: 0x00045C4F File Offset: 0x00043E4F
			internal void MarkPotentiallyEmpty()
			{
				UIElementsRuntimeUtility.MarkPotentiallyEmpty(this.m_Settings);
			}

			// Token: 0x0400092D RID: 2349
			private readonly PanelSettings m_Settings;

			// Token: 0x0400092E RID: 2350
			private BaseRuntimePanel m_RuntimePanel;
		}
	}
}

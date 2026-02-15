using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025B RID: 603
	[ExecuteAlways]
	[AddComponentMenu("UI Toolkit/UI Document")]
	[HelpURL("UIE-get-started-with-runtime-ui")]
	[DisallowMultipleComponent]
	[DefaultExecutionOrder(-100)]
	public sealed class UIDocument : MonoBehaviour
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00045F84 File Offset: 0x00044184
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x00045F9C File Offset: 0x0004419C
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
			set
			{
				bool flag = this.parentUI == null;
				if (flag)
				{
					bool flag2 = this.m_PanelSettings == value;
					if (flag2)
					{
						this.m_PreviousPanelSettings = this.m_PanelSettings;
						return;
					}
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.DetachUIDocument(this);
					}
					this.m_PanelSettings = value;
					bool flag4 = this.m_PanelSettings != null;
					if (flag4)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
				else
				{
					Assert.AreEqual<PanelSettings>(this.parentUI.m_PanelSettings, value);
					this.m_PanelSettings = this.parentUI.m_PanelSettings;
				}
				bool flag5 = this.m_ChildrenContent != null;
				if (flag5)
				{
					foreach (UIDocument child in this.m_ChildrenContent.m_AttachedUIDocuments)
					{
						child.panelSettings = this.m_PanelSettings;
					}
				}
				this.m_PreviousPanelSettings = this.m_PanelSettings;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x000460C0 File Offset: 0x000442C0
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x000460C8 File Offset: 0x000442C8
		public UIDocument parentUI
		{
			get
			{
				return this.m_ParentUI;
			}
			private set
			{
				this.m_ParentUI = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000460D4 File Offset: 0x000442D4
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x000460EC File Offset: 0x000442EC
		public VisualTreeAsset visualTreeAsset
		{
			get
			{
				return this.sourceAsset;
			}
			set
			{
				this.sourceAsset = value;
				this.RecreateUI();
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00046100 File Offset: 0x00044300
		public VisualElement rootVisualElement
		{
			get
			{
				return this.m_RootVisualElement;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00046118 File Offset: 0x00044318
		internal int firstChildInserIndex
		{
			get
			{
				return this.m_FirstChildInsertIndex;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00046120 File Offset: 0x00044320
		internal UIDocument.WorldSpaceSizeMode worldSpaceSizeMode
		{
			get
			{
				return this.m_WorldSpaceSizeMode;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00046128 File Offset: 0x00044328
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00046130 File Offset: 0x00044330
		public float sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				bool flag = this.m_SortingOrder == value;
				if (!flag)
				{
					this.m_SortingOrder = value;
					this.ApplySortingOrder();
				}
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004615C File Offset: 0x0004435C
		internal void ApplySortingOrder()
		{
			this.AddRootVisualElementToTree();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00046168 File Offset: 0x00044368
		private UIDocument()
		{
			this.m_UIDocumentCreationIndex = UIDocument.s_CurrentUIDocumentCounter++;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000461CD File Offset: 0x000443CD
		private void Awake()
		{
			this.SetupFromHierarchy();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000461D8 File Offset: 0x000443D8
		private void OnEnable()
		{
			bool flag = this.parentUI != null && this.m_PanelSettings == null;
			if (flag)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
			}
			bool flag2 = this.m_RootVisualElement == null;
			if (flag2)
			{
				this.RecreateUI();
			}
			else
			{
				this.AddRootVisualElementToTree();
			}
			this.ResolveRuntimePanel();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00046244 File Offset: 0x00044444
		private void ResolveRuntimePanel()
		{
			bool flag = this.m_RuntimePanel == null;
			if (flag)
			{
				VisualElement rootVisualElement = this.rootVisualElement;
				this.m_RuntimePanel = ((rootVisualElement != null) ? rootVisualElement.panel : null) as RuntimePanel;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0004627C File Offset: 0x0004447C
		public IRuntimePanel runtimePanel
		{
			get
			{
				this.ResolveRuntimePanel();
				return this.m_RuntimePanel;
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004629C File Offset: 0x0004449C
		private void LateUpdate()
		{
			bool flag = this.m_RootVisualElement == null || this.panelSettings == null || this.panelSettings.panel == null;
			if (!flag)
			{
				this.AddOrRemoveRendererComponent();
				bool flag2 = !this.panelSettings.panel.isFlat;
				if (flag2)
				{
					this.ResolveRuntimePanel();
					this.SetTransform();
					this.UpdateRenderer();
				}
				else
				{
					bool rootHasWorldTransform = this.m_RootHasWorldTransform;
					if (rootHasWorldTransform)
					{
						this.ClearTransform();
					}
				}
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00046320 File Offset: 0x00044520
		private void UpdateRenderer()
		{
			UIRenderer renderer;
			bool flag = !base.TryGetComponent<UIRenderer>(out renderer);
			if (flag)
			{
				this.rootVisualElement.uiRenderer = null;
				this.UpdateCutRenderChainFlag();
			}
			else
			{
				this.rootVisualElement.uiRenderer = renderer;
				renderer.skipRendering = this.parentUI != null || this.pixelsPerUnit < Mathf.Epsilon;
				BaseRuntimePanel rtp = (BaseRuntimePanel)this.m_RootVisualElement.panel;
				bool flag2 = rtp == null;
				if (!flag2)
				{
					Debug.Assert(rtp.drawsInCameras);
					float ppu = ((this.m_RuntimePanel == null) ? 1f : this.m_RuntimePanel.pixelsPerUnit);
					bool flag3 = ppu < 1E-30f;
					if (flag3)
					{
						ppu = 100f;
					}
					float ppuScale = 1f / ppu;
					Rect rect = this.rootVisualElement.boundingBox;
					Vector2 center = rect.center;
					center.y = -center.y;
					renderer.localBounds = new Bounds(center * ppuScale, new Vector3(rect.width * ppuScale, rect.height * ppuScale, 0f));
					this.UpdateCutRenderChainFlag();
				}
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00046450 File Offset: 0x00044650
		private void AddOrRemoveRendererComponent()
		{
			UIRenderer renderer;
			base.TryGetComponent<UIRenderer>(out renderer);
			bool flag;
			if (this.m_PanelSettings != null)
			{
				BaseRuntimePanel panel = this.m_PanelSettings.panel;
				flag = panel != null && panel.drawsInCameras;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = renderer == null;
				if (flag3)
				{
					base.gameObject.AddComponent<UIRenderer>();
				}
			}
			else
			{
				UIRUtility.Destroy(renderer);
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000464B8 File Offset: 0x000446B8
		private void UpdateCutRenderChainFlag()
		{
			bool shouldCutRenderChain = this.parentUI == null;
			bool flag = this.rootVisualElement.shouldCutRenderChain != shouldCutRenderChain;
			if (flag)
			{
				this.rootVisualElement.shouldCutRenderChain = shouldCutRenderChain;
				this.rootVisualElement.MarkDirtyRepaint();
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00046504 File Offset: 0x00044704
		private void SetTransform()
		{
			Matrix4x4 matrix;
			this.ComputeTransform(base.transform, out matrix);
			this.m_RootVisualElement.style.transformOrigin = new TransformOrigin(Vector3.zero);
			this.m_RootVisualElement.style.translate = new Translate(matrix.GetPosition());
			this.m_RootVisualElement.style.rotate = new Rotate(matrix.rotation);
			this.m_RootVisualElement.style.scale = new Scale(matrix.lossyScale);
			this.m_RootHasWorldTransform = true;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000465B0 File Offset: 0x000447B0
		private void ClearTransform()
		{
			this.m_RootVisualElement.style.transformOrigin = StyleKeyword.Null;
			this.m_RootVisualElement.style.translate = StyleKeyword.Null;
			this.m_RootVisualElement.style.rotate = StyleKeyword.Null;
			this.m_RootVisualElement.style.scale = StyleKeyword.Null;
			this.m_RootHasWorldTransform = false;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00046621 File Offset: 0x00044821
		private float pixelsPerUnit
		{
			get
			{
				return (this.m_RuntimePanel == null) ? 1f : this.m_RuntimePanel.pixelsPerUnit;
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00046640 File Offset: 0x00044840
		private void ComputeTransform(Transform transform, out Matrix4x4 matrix)
		{
			float ppu = this.pixelsPerUnit;
			bool flag = ppu < Mathf.Epsilon;
			if (flag)
			{
				matrix = Matrix4x4.identity;
			}
			else
			{
				float ppuScale = 1f / ppu;
				Vector3 scale = Vector3.one * ppuScale;
				Quaternion flipRotation = Quaternion.AngleAxis(180f, Vector3.right);
				bool flag2 = this.parentUI == null;
				if (flag2)
				{
					matrix = Matrix4x4.TRS(Vector3.zero, flipRotation, scale);
				}
				else
				{
					Matrix4x4 ui2Go = Matrix4x4.TRS(Vector3.zero, flipRotation, scale);
					Matrix4x4 go2Ui = ui2Go.inverse;
					Matrix4x4 childGoToWorld = transform.localToWorldMatrix;
					Matrix4x4 worldToParentGo = this.parentUI.transform.worldToLocalMatrix;
					matrix = go2Ui * worldToParentGo * childGoToWorld * ui2Go;
				}
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00046710 File Offset: 0x00044910
		private static void SetNoTransform(VisualElement visualElement)
		{
			visualElement.style.translate = Translate.None();
			visualElement.style.rotate = Rotate.None();
			visualElement.style.scale = Scale.None();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00046760 File Offset: 0x00044960
		private void SetupFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			this.parentUI = this.FindUIDocumentParent();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0004679C File Offset: 0x0004499C
		private UIDocument FindUIDocumentParent()
		{
			Transform t = base.transform;
			Transform parentTransform = t.parent;
			bool flag = parentTransform != null;
			if (flag)
			{
				UIDocument[] potentialParents = parentTransform.GetComponentsInParent<UIDocument>(true);
				bool flag2 = potentialParents != null && potentialParents.Length != 0;
				if (flag2)
				{
					return potentialParents[0];
				}
			}
			return null;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000467F0 File Offset: 0x000449F0
		internal void Reset()
		{
			bool flag = this.parentUI == null;
			if (flag)
			{
				PanelSettings previousPanelSettings = this.m_PreviousPanelSettings;
				if (previousPanelSettings != null)
				{
					previousPanelSettings.DetachUIDocument(this);
				}
				this.panelSettings = null;
			}
			this.SetupFromHierarchy();
			bool flag2 = this.parentUI != null;
			if (flag2)
			{
				this.m_PanelSettings = this.parentUI.m_PanelSettings;
				this.AddRootVisualElementToTree();
			}
			else
			{
				bool flag3 = this.m_PanelSettings != null;
				if (flag3)
				{
					this.AddRootVisualElementToTree();
				}
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00046878 File Offset: 0x00044A78
		private void AddChildAndInsertContentToVisualTree(UIDocument child)
		{
			bool flag = this.m_ChildrenContent == null;
			if (flag)
			{
				this.m_ChildrenContent = new UIDocumentList();
			}
			else
			{
				this.m_ChildrenContent.RemoveFromListAndFromVisualTree(child);
			}
			this.m_ChildrenContent.AddToListAndToVisualTree(child, this.m_RootVisualElement, this.m_FirstChildInsertIndex);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000468CA File Offset: 0x00044ACA
		private void RemoveChild(UIDocument child)
		{
			UIDocumentList childrenContent = this.m_ChildrenContent;
			if (childrenContent != null)
			{
				childrenContent.RemoveFromListAndFromVisualTree(child);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000468E0 File Offset: 0x00044AE0
		private void RecreateUI()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
			bool flag2 = this.sourceAsset != null;
			if (flag2)
			{
				this.m_RootVisualElement = this.sourceAsset.Instantiate();
				bool flag3 = this.m_RootVisualElement == null;
				if (flag3)
				{
					Debug.LogError("The UXML file set for the UIDocument could not be cloned.");
				}
			}
			bool flag4 = this.m_RootVisualElement == null;
			if (flag4)
			{
				this.m_RootVisualElement = new TemplateContainer
				{
					name = base.gameObject.name + "-container"
				};
			}
			else
			{
				this.m_RootVisualElement.name = base.gameObject.name + "-container";
			}
			this.m_RootVisualElement.pickingMode = PickingMode.Ignore;
			bool isActiveAndEnabled = base.isActiveAndEnabled;
			if (isActiveAndEnabled)
			{
				this.AddRootVisualElementToTree();
			}
			this.m_FirstChildInsertIndex = this.m_RootVisualElement.childCount;
			bool flag5 = this.m_ChildrenContent != null;
			if (flag5)
			{
				bool flag6 = this.m_ChildrenContentCopy == null;
				if (flag6)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument child in this.m_ChildrenContentCopy)
				{
					bool isActiveAndEnabled2 = child.isActiveAndEnabled;
					if (isActiveAndEnabled2)
					{
						bool flag7 = child.m_RootVisualElement == null;
						if (flag7)
						{
							child.RecreateUI();
						}
						else
						{
							this.AddChildAndInsertContentToVisualTree(child);
						}
					}
				}
				this.m_ChildrenContentCopy.Clear();
			}
			this.SetupRootClassList();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00046ABC File Offset: 0x00044CBC
		private void SetupRootClassList()
		{
			bool flag = this.m_RootVisualElement == null;
			if (!flag)
			{
				bool flag2 = this.panelSettings == null || this.panelSettings.renderMode != PanelRenderMode.WorldSpace;
				if (flag2)
				{
					this.m_RootVisualElement.EnableInClassList("unity-ui-document__root", this.parentUI == null);
				}
				else
				{
					this.UpdateWorldSpaceSize();
				}
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00046B2C File Offset: 0x00044D2C
		private void UpdateWorldSpaceSize()
		{
			bool flag = this.m_RootVisualElement == null;
			if (!flag)
			{
				bool flag2 = this.m_WorldSpaceSizeMode == UIDocument.WorldSpaceSizeMode.Fixed;
				if (flag2)
				{
					this.m_RootVisualElement.style.position = Position.Absolute;
					this.m_RootVisualElement.style.width = this.m_WorldSpaceWidth;
					this.m_RootVisualElement.style.height = this.m_WorldSpaceHeight;
				}
				else
				{
					this.m_RootVisualElement.style.position = Position.Relative;
					this.m_RootVisualElement.style.width = StyleKeyword.Null;
					this.m_RootVisualElement.style.height = StyleKeyword.Null;
				}
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00046BF4 File Offset: 0x00044DF4
		private void AddRootVisualElementToTree()
		{
			bool flag = !base.enabled;
			if (!flag)
			{
				bool flag2 = this.parentUI != null;
				if (flag2)
				{
					this.parentUI.AddChildAndInsertContentToVisualTree(this);
				}
				else
				{
					bool flag3 = this.m_PanelSettings != null;
					if (flag3)
					{
						this.m_PanelSettings.AttachAndInsertUIDocumentToVisualTree(this);
					}
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00046C54 File Offset: 0x00044E54
		private void RemoveFromHierarchy()
		{
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.parentUI.RemoveChild(this);
			}
			else
			{
				bool flag2 = this.m_PanelSettings != null;
				if (flag2)
				{
					this.m_PanelSettings.DetachUIDocument(this);
				}
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00046CA4 File Offset: 0x00044EA4
		private void OnDisable()
		{
			bool flag = this.m_RootVisualElement != null;
			if (flag)
			{
				this.RemoveFromHierarchy();
				this.m_RootVisualElement = null;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00046CD0 File Offset: 0x00044ED0
		private void OnTransformChildrenChanged()
		{
			bool flag = this.m_ChildrenContent != null;
			if (flag)
			{
				bool flag2 = this.m_ChildrenContentCopy == null;
				if (flag2)
				{
					this.m_ChildrenContentCopy = new List<UIDocument>(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				else
				{
					this.m_ChildrenContentCopy.AddRange(this.m_ChildrenContent.m_AttachedUIDocuments);
				}
				foreach (UIDocument child in this.m_ChildrenContentCopy)
				{
					child.ReactToHierarchyChanged();
				}
				this.m_ChildrenContentCopy.Clear();
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00046D88 File Offset: 0x00044F88
		private void OnTransformParentChanged()
		{
			this.ReactToHierarchyChanged();
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00046D94 File Offset: 0x00044F94
		internal void ReactToHierarchyChanged()
		{
			this.SetupFromHierarchy();
			bool flag = this.parentUI != null;
			if (flag)
			{
				this.panelSettings = this.parentUI.m_PanelSettings;
			}
			VisualElement rootVisualElement = this.m_RootVisualElement;
			if (rootVisualElement != null)
			{
				rootVisualElement.RemoveFromHierarchy();
			}
			this.AddRootVisualElementToTree();
			this.SetupRootClassList();
		}

		// Token: 0x04000934 RID: 2356
		internal const string k_RootStyleClassName = "unity-ui-document__root";

		// Token: 0x04000935 RID: 2357
		internal const string k_VisualElementNameSuffix = "-container";

		// Token: 0x04000936 RID: 2358
		private const int k_DefaultSortingOrder = 0;

		// Token: 0x04000937 RID: 2359
		private static int s_CurrentUIDocumentCounter;

		// Token: 0x04000938 RID: 2360
		internal readonly int m_UIDocumentCreationIndex;

		// Token: 0x04000939 RID: 2361
		[SerializeField]
		private PanelSettings m_PanelSettings;

		// Token: 0x0400093A RID: 2362
		private PanelSettings m_PreviousPanelSettings = null;

		// Token: 0x0400093B RID: 2363
		[SerializeField]
		private UIDocument m_ParentUI;

		// Token: 0x0400093C RID: 2364
		private UIDocumentList m_ChildrenContent = null;

		// Token: 0x0400093D RID: 2365
		private List<UIDocument> m_ChildrenContentCopy = null;

		// Token: 0x0400093E RID: 2366
		[SerializeField]
		private VisualTreeAsset sourceAsset;

		// Token: 0x0400093F RID: 2367
		private VisualElement m_RootVisualElement;

		// Token: 0x04000940 RID: 2368
		private RuntimePanel m_RuntimePanel;

		// Token: 0x04000941 RID: 2369
		private int m_FirstChildInsertIndex;

		// Token: 0x04000942 RID: 2370
		[SerializeField]
		private float m_SortingOrder = 0f;

		// Token: 0x04000943 RID: 2371
		[SerializeField]
		private UIDocument.WorldSpaceSizeMode m_WorldSpaceSizeMode = UIDocument.WorldSpaceSizeMode.Fixed;

		// Token: 0x04000944 RID: 2372
		[SerializeField]
		private float m_WorldSpaceWidth = 1920f;

		// Token: 0x04000945 RID: 2373
		[SerializeField]
		private float m_WorldSpaceHeight = 1080f;

		// Token: 0x04000946 RID: 2374
		private bool m_RootHasWorldTransform;

		// Token: 0x0200025C RID: 604
		internal enum WorldSpaceSizeMode
		{
			// Token: 0x04000948 RID: 2376
			Dynamic,
			// Token: 0x04000949 RID: 2377
			Fixed
		}
	}
}

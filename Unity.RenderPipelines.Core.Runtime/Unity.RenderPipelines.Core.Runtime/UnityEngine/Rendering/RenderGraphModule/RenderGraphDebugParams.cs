using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000221 RID: 545
	internal class RenderGraphDebugParams : IDebugDisplaySettingsQuery
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00035235 File Offset: 0x00033435
		internal void Reset()
		{
			this.clearRenderTargetsAtCreation = false;
			this.clearRenderTargetsAtRelease = false;
			this.disablePassCulling = false;
			this.immediateMode = false;
			this.enableLogging = false;
			this.logFrameInformation = false;
			this.logResources = false;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00035268 File Offset: 0x00033468
		internal List<DebugUI.Widget> GetWidgetList(string name)
		{
			return new List<DebugUI.Widget>
			{
				new DebugUI.Container
				{
					displayName = name + " Render Graph",
					children = 
					{
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.ClearRenderTargetsAtCreation,
							getter = () => this.clearRenderTargetsAtCreation,
							setter = delegate(bool value)
							{
								this.clearRenderTargetsAtCreation = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.ClearRenderTargetsAtFree,
							getter = () => this.clearRenderTargetsAtRelease,
							setter = delegate(bool value)
							{
								this.clearRenderTargetsAtRelease = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.DisablePassCulling,
							getter = () => this.disablePassCulling,
							setter = delegate(bool value)
							{
								this.disablePassCulling = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.ImmediateMode,
							getter = () => this.immediateMode,
							setter = delegate(bool value)
							{
								this.immediateMode = value;
							},
							isHiddenCallback = () => !this.IsImmediateModeSupported()
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.EnableLogging,
							getter = () => this.enableLogging,
							setter = delegate(bool value)
							{
								this.enableLogging = value;
							}
						},
						new DebugUI.Button
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.LogFrameInformation,
							action = delegate
							{
								if (!this.enableLogging)
								{
									Debug.Log("You must first enable logging before logging frame information.");
								}
								this.logFrameInformation = true;
							}
						},
						new DebugUI.Button
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.LogResources,
							action = delegate
							{
								if (!this.enableLogging)
								{
									Debug.Log("You must first enable logging before logging resources.");
								}
								this.logResources = true;
							}
						}
					}
				}
			};
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00035440 File Offset: 0x00033640
		private bool IsImmediateModeSupported()
		{
			IRenderGraphEnabledRenderPipeline rgPipeline = GraphicsSettings.currentRenderPipeline as IRenderGraphEnabledRenderPipeline;
			return rgPipeline != null && rgPipeline.isImmediateModeSupported;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00035464 File Offset: 0x00033664
		public void RegisterDebug(string name, DebugUI.Panel debugPanel = null)
		{
			List<DebugUI.Widget> list = this.GetWidgetList(name);
			this.m_DebugItems = list.ToArray();
			this.m_DebugPanel = ((debugPanel != null) ? debugPanel : DebugManager.instance.GetPanel((name.Length == 0) ? "Render Graph" : name, true, 0, false));
			this.m_DebugPanel.children.Add(this.m_DebugItems);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000354C4 File Offset: 0x000336C4
		public void UnRegisterDebug(string name)
		{
			if (this.m_DebugPanel != null)
			{
				this.m_DebugPanel.children.Remove(this.m_DebugItems);
			}
			this.m_DebugPanel = null;
			this.m_DebugItems = null;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000354F3 File Offset: 0x000336F3
		public bool AreAnySettingsActive
		{
			get
			{
				return this.clearRenderTargetsAtCreation || this.clearRenderTargetsAtRelease || this.disablePassCulling || this.immediateMode || this.enableLogging;
			}
		}

		// Token: 0x04000979 RID: 2425
		private DebugUI.Widget[] m_DebugItems;

		// Token: 0x0400097A RID: 2426
		private DebugUI.Panel m_DebugPanel;

		// Token: 0x0400097B RID: 2427
		public bool clearRenderTargetsAtCreation;

		// Token: 0x0400097C RID: 2428
		public bool clearRenderTargetsAtRelease;

		// Token: 0x0400097D RID: 2429
		public bool disablePassCulling;

		// Token: 0x0400097E RID: 2430
		public bool immediateMode;

		// Token: 0x0400097F RID: 2431
		public bool enableLogging;

		// Token: 0x04000980 RID: 2432
		public bool logFrameInformation;

		// Token: 0x04000981 RID: 2433
		public bool logResources;

		// Token: 0x02000222 RID: 546
		private static class Strings
		{
			// Token: 0x04000982 RID: 2434
			public static readonly DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtCreation = new DebugUI.Widget.NameAndTooltip
			{
				name = "Clear Render Targets At Creation",
				tooltip = "Enable to clear all render textures before any rendergraph passes to check if some clears are missing."
			};

			// Token: 0x04000983 RID: 2435
			public static readonly DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtFree = new DebugUI.Widget.NameAndTooltip
			{
				name = "Clear Render Targets When Freed",
				tooltip = "Enable to clear all render textures when textures are freed by the graph to detect use after free of textures."
			};

			// Token: 0x04000984 RID: 2436
			public static readonly DebugUI.Widget.NameAndTooltip DisablePassCulling = new DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Pass Culling",
				tooltip = "Enable to temporarily disable culling to assess if a pass is culled."
			};

			// Token: 0x04000985 RID: 2437
			public static readonly DebugUI.Widget.NameAndTooltip ImmediateMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Immediate Mode",
				tooltip = "Enable to force render graph to execute all passes in the order you registered them."
			};

			// Token: 0x04000986 RID: 2438
			public static readonly DebugUI.Widget.NameAndTooltip EnableLogging = new DebugUI.Widget.NameAndTooltip
			{
				name = "Enable Logging",
				tooltip = "Enable to allow HDRP to capture information in the log."
			};

			// Token: 0x04000987 RID: 2439
			public static readonly DebugUI.Widget.NameAndTooltip LogFrameInformation = new DebugUI.Widget.NameAndTooltip
			{
				name = "Log Frame Information",
				tooltip = "Enable to log information output from each frame."
			};

			// Token: 0x04000988 RID: 2440
			public static readonly DebugUI.Widget.NameAndTooltip LogResources = new DebugUI.Widget.NameAndTooltip
			{
				name = "Log Resources",
				tooltip = "Enable to log the current render graph's global resource usage."
			};

			// Token: 0x04000989 RID: 2441
			public static readonly DebugUI.Widget.NameAndTooltip EnableNativeCompiler = new DebugUI.Widget.NameAndTooltip
			{
				name = "Enable Native Pass Compiler",
				tooltip = "Enable the new native pass compiler."
			};
		}
	}
}

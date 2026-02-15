using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000545 RID: 1349
	internal class RenderChain : IDisposable
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x00090E5F File Offset: 0x0008F05F
		// (set) Token: 0x06002525 RID: 9509 RVA: 0x00090E67 File Offset: 0x0008F067
		public OpacityIdAccelerator opacityIdAccelerator { get; private set; }

		// Token: 0x06002526 RID: 9510 RVA: 0x00090E70 File Offset: 0x0008F070
		public RenderChain(BaseVisualElementPanel panel)
			: this(panel, new UIRenderDevice(panel.panelRenderer.vertexBudget, 0U), panel.atlas, new VectorImageManager(panel.atlas))
		{
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00090EA0 File Offset: 0x0008F0A0
		protected RenderChain(BaseVisualElementPanel panel, UIRenderDevice device, AtlasBase atlas, VectorImageManager vectorImageManager)
		{
			this.m_DirtyTracker.heads = new List<VisualElement>(8);
			this.m_DirtyTracker.tails = new List<VisualElement>(8);
			this.m_DirtyTracker.minDepths = new int[5];
			this.m_DirtyTracker.maxDepths = new int[5];
			this.m_DirtyTracker.Reset();
			this.panel = panel;
			this.device = device;
			this.atlas = atlas;
			this.vectorImageManager = vectorImageManager;
			this.tempMeshAllocator = new TempMeshAllocatorImpl();
			this.jobManager = new JobManager();
			this.opacityIdAccelerator = new OpacityIdAccelerator();
			this.meshGenerationNodeManager = new MeshGenerationNodeManager(this.entryRecorder);
			this.m_VisualChangesProcessor = new RenderChain.VisualChangesProcessor(this);
			ColorSpace activeColorSpace = QualitySettings.activeColorSpace;
			bool flag = panel.contextType == ContextType.Player;
			if (flag)
			{
				BaseRuntimePanel runtimePanel = (BaseRuntimePanel)panel;
				device.drawsInCameras = (this.drawInCameras = runtimePanel.drawsInCameras);
				bool drawInCameras = this.drawInCameras;
				if (drawInCameras)
				{
					this.m_DefaultMat = Shaders.runtimeWorldMaterial;
				}
				else
				{
					this.m_DefaultMat = Shaders.runtimeMaterial;
					bool flag2 = activeColorSpace == ColorSpace.Linear;
					if (flag2)
					{
						this.forceGammaRendering = panel.panelRenderer.forceGammaRendering;
					}
				}
			}
			else
			{
				bool flag3 = activeColorSpace == ColorSpace.Linear;
				if (flag3)
				{
					this.forceGammaRendering = true;
				}
				this.m_DefaultMat = Shaders.editorMaterial;
			}
			Shaders.Acquire();
			this.shaderInfoAllocator = new UIRVEShaderInfoAllocator(this.forceGammaRendering ? ColorSpace.Gamma : activeColorSpace);
			device.isFlat = (this.isFlat = panel.isFlat);
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000910E0 File Offset: 0x0008F2E0
		// (set) Token: 0x06002529 RID: 9513 RVA: 0x000910E8 File Offset: 0x0008F2E8
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600252A RID: 9514 RVA: 0x000910F1 File Offset: 0x0008F2F1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00091104 File Offset: 0x0008F304
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Shaders.Release();
					RenderChainCommand firstCommand = this.m_FirstCommand;
					for (VisualElement ve = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); ve != null; ve = ve.renderChainData.next)
					{
						this.ResetTextures(ve);
					}
					this.tempMeshAllocator.Dispose();
					this.tempMeshAllocator = null;
					this.jobManager.Dispose();
					this.jobManager = null;
					VectorImageManager vectorImageManager = this.vectorImageManager;
					if (vectorImageManager != null)
					{
						vectorImageManager.Dispose();
					}
					this.vectorImageManager = null;
					this.shaderInfoAllocator.Dispose();
					this.shaderInfoAllocator = null;
					UIRenderDevice device = this.device;
					if (device != null)
					{
						device.Dispose();
					}
					this.device = null;
					OpacityIdAccelerator opacityIdAccelerator = this.opacityIdAccelerator;
					if (opacityIdAccelerator != null)
					{
						opacityIdAccelerator.Dispose();
					}
					this.opacityIdAccelerator = null;
					RenderChain.VisualChangesProcessor visualChangesProcessor = this.m_VisualChangesProcessor;
					if (visualChangesProcessor != null)
					{
						visualChangesProcessor.Dispose();
					}
					this.m_VisualChangesProcessor = null;
					MeshGenerationDeferrer meshGenerationDeferrer = this.m_MeshGenerationDeferrer;
					if (meshGenerationDeferrer != null)
					{
						meshGenerationDeferrer.Dispose();
					}
					this.m_MeshGenerationDeferrer = null;
					this.meshGenerationNodeManager.Dispose();
					this.meshGenerationNodeManager = null;
					this.atlas = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0009124A File Offset: 0x0008F44A
		internal ref ChainBuilderStats statsByRef
		{
			get
			{
				return ref this.m_Stats;
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00091254 File Offset: 0x0008F454
		public void ProcessChanges()
		{
			this.m_Stats = default(ChainBuilderStats);
			this.m_Stats.elementsAdded = this.m_Stats.elementsAdded + this.m_StatsElementsAdded;
			this.m_Stats.elementsRemoved = this.m_Stats.elementsRemoved + this.m_StatsElementsRemoved;
			this.m_StatsElementsAdded = (this.m_StatsElementsRemoved = 0U);
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			int dirtyClass = 0;
			RenderDataDirtyTypes dirtyFlags = RenderDataDirtyTypes.Clipping | RenderDataDirtyTypes.ClippingHierarchy;
			RenderDataDirtyTypes clearDirty = ~dirtyFlags;
			for (int depth = this.m_DirtyTracker.minDepths[dirtyClass]; depth <= this.m_DirtyTracker.maxDepths[dirtyClass]; depth++)
			{
				VisualElement ve = this.m_DirtyTracker.heads[depth];
				while (ve != null)
				{
					VisualElement veNext = ve.renderChainData.nextDirty;
					bool flag = (ve.renderChainData.dirtiedValues & dirtyFlags) > RenderDataDirtyTypes.None;
					if (flag)
					{
						bool flag2 = ve.renderChainData.isInChain && ve.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag2)
						{
							RenderEvents.ProcessOnClippingChanged(this, ve, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(ve, clearDirty);
					}
					ve = veNext;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			dirtyClass = 1;
			dirtyFlags = RenderDataDirtyTypes.Opacity | RenderDataDirtyTypes.OpacityHierarchy;
			clearDirty = ~dirtyFlags;
			for (int depth2 = this.m_DirtyTracker.minDepths[dirtyClass]; depth2 <= this.m_DirtyTracker.maxDepths[dirtyClass]; depth2++)
			{
				VisualElement ve2 = this.m_DirtyTracker.heads[depth2];
				while (ve2 != null)
				{
					VisualElement veNext2 = ve2.renderChainData.nextDirty;
					bool flag3 = (ve2.renderChainData.dirtiedValues & dirtyFlags) > RenderDataDirtyTypes.None;
					if (flag3)
					{
						bool flag4 = ve2.renderChainData.isInChain && ve2.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag4)
						{
							RenderEvents.ProcessOnOpacityChanged(this, ve2, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(ve2, clearDirty);
					}
					ve2 = veNext2;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			dirtyClass = 2;
			dirtyFlags = RenderDataDirtyTypes.Color;
			clearDirty = ~dirtyFlags;
			for (int depth3 = this.m_DirtyTracker.minDepths[dirtyClass]; depth3 <= this.m_DirtyTracker.maxDepths[dirtyClass]; depth3++)
			{
				VisualElement ve3 = this.m_DirtyTracker.heads[depth3];
				while (ve3 != null)
				{
					VisualElement veNext3 = ve3.renderChainData.nextDirty;
					bool flag5 = (ve3.renderChainData.dirtiedValues & dirtyFlags) > RenderDataDirtyTypes.None;
					if (flag5)
					{
						bool flag6 = ve3.renderChainData.isInChain && ve3.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag6)
						{
							RenderEvents.ProcessOnColorChanged(this, ve3, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(ve3, clearDirty);
					}
					ve3 = veNext3;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			dirtyClass = 3;
			dirtyFlags = RenderDataDirtyTypes.Transform | RenderDataDirtyTypes.ClipRectSize;
			clearDirty = ~dirtyFlags;
			for (int depth4 = this.m_DirtyTracker.minDepths[dirtyClass]; depth4 <= this.m_DirtyTracker.maxDepths[dirtyClass]; depth4++)
			{
				VisualElement ve4 = this.m_DirtyTracker.heads[depth4];
				while (ve4 != null)
				{
					VisualElement veNext4 = ve4.renderChainData.nextDirty;
					bool flag7 = (ve4.renderChainData.dirtiedValues & dirtyFlags) > RenderDataDirtyTypes.None;
					if (flag7)
					{
						bool flag8 = ve4.renderChainData.isInChain && ve4.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag8)
						{
							RenderEvents.ProcessOnTransformOrSizeChanged(this, ve4, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(ve4, clearDirty);
					}
					ve4 = veNext4;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.jobManager.CompleteNudgeJobs();
			this.m_BlockDirtyRegistration = true;
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			dirtyClass = 4;
			dirtyFlags = RenderDataDirtyTypes.AllVisuals;
			clearDirty = ~dirtyFlags;
			for (int depth5 = this.m_DirtyTracker.minDepths[dirtyClass]; depth5 <= this.m_DirtyTracker.maxDepths[dirtyClass]; depth5++)
			{
				VisualElement ve5 = this.m_DirtyTracker.heads[depth5];
				while (ve5 != null)
				{
					VisualElement veNext5 = ve5.renderChainData.nextDirty;
					bool flag9 = (ve5.renderChainData.dirtiedValues & dirtyFlags) > RenderDataDirtyTypes.None;
					if (flag9)
					{
						bool flag10 = ve5.renderChainData.isInChain && ve5.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag10)
						{
							this.m_VisualChangesProcessor.ProcessOnVisualsChanged(ve5, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(ve5, clearDirty);
					}
					ve5 = veNext5;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			this.m_MeshGenerationDeferrer.ProcessDeferredWork(this.m_VisualChangesProcessor.meshGenerationContext);
			this.m_VisualChangesProcessor.ScheduleMeshGenerationJobs();
			this.m_MeshGenerationDeferrer.ProcessDeferredWork(this.m_VisualChangesProcessor.meshGenerationContext);
			this.m_VisualChangesProcessor.ConvertEntriesToCommands(ref this.m_Stats);
			this.jobManager.CompleteConvertMeshJobs();
			this.jobManager.CompleteCopyMeshJobs();
			this.opacityIdAccelerator.CompleteJobs();
			this.m_BlockDirtyRegistration = false;
			this.meshGenerationNodeManager.ResetAll();
			this.tempMeshAllocator.Clear();
			this.meshWriteDataPool.ReturnAll();
			this.entryPool.ReturnAll();
			this.m_DirtyTracker.Reset();
			AtlasBase atlas = this.atlas;
			if (atlas != null)
			{
				atlas.InvokeUpdateDynamicTextures(this.panel);
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			if (vectorImageManager != null)
			{
				vectorImageManager.Commit();
			}
			this.shaderInfoAllocator.IssuePendingStorageChanges();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.OnFrameRenderingBegin();
			}
			bool drawInCameras = this.drawInCameras;
			if (drawInCameras)
			{
				this.SerializeCommandsForCameras();
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00091928 File Offset: 0x0008FB28
		private void SerializeCommandsForCameras()
		{
			using (RenderChain.k_MarkerSerialize.Auto())
			{
				bool flag = this.m_FirstCommand == null;
				if (!flag)
				{
					Exception immediateException = null;
					UIRenderDevice device = this.device;
					RenderChainCommand firstCommand = this.m_FirstCommand;
					Material defaultMat = this.m_DefaultMat;
					Material defaultMat2 = this.m_DefaultMat;
					VectorImageManager vectorImageManager = this.vectorImageManager;
					device.EvaluateChain(firstCommand, defaultMat, defaultMat2, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, this.panel.scaledPixelsPerPoint, ref immediateException);
					UIRenderDevice device2 = this.device;
					List<CommandList> frameCommandLists = ((device2 != null) ? device2.currentFrameCommandLists : null);
					bool flag2 = this.drawInCameras && frameCommandLists != null;
					if (flag2)
					{
						for (int cmdListIndex = 0; cmdListIndex < this.device.currentFrameCommandListCount; cmdListIndex++)
						{
							CommandList cmdList = frameCommandLists[cmdListIndex];
							VisualElement owner = cmdList.m_Owner;
							UIRenderer renderer = ((owner != null) ? owner.uiRenderer : null);
							bool flag3 = renderer != null;
							if (flag3)
							{
								List<CommandList>[] commandLists = this.device.commandLists;
								renderer.commandLists = commandLists;
								int safeFrameIndex = (int)(this.device.frameIndex % (uint)commandLists.Length);
								renderer.SetNativeData(safeFrameIndex, cmdListIndex, this.m_DefaultMat);
							}
						}
					}
					Debug.Assert(immediateException == null);
				}
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00091A98 File Offset: 0x0008FC98
		public void Render()
		{
			Debug.Assert(!this.drawInCameras);
			PanelClearSettings clearSettings = this.panel.clearSettings;
			bool flag = clearSettings.clearColor || clearSettings.clearDepthStencil;
			if (flag)
			{
				Color clearColor = clearSettings.color;
				clearColor = clearColor.RGBMultiplied(clearColor.a);
				GL.Clear(clearSettings.clearDepthStencil, clearSettings.clearColor, clearColor, 0.99f);
			}
			Exception immediateException = null;
			bool flag2 = this.m_FirstCommand != null;
			if (flag2)
			{
				Rect viewport = this.panel.visualTree.layout;
				bool forceGammaRendering = this.forceGammaRendering;
				if (forceGammaRendering)
				{
					this.m_DefaultMat.EnableKeyword(Shaders.k_ForceGammaKeyword);
				}
				else
				{
					this.m_DefaultMat.DisableKeyword(Shaders.k_ForceGammaKeyword);
				}
				this.m_DefaultMat.SetPass(0);
				Matrix4x4 projection = ProjectionUtils.Ortho(viewport.xMin, viewport.xMax, viewport.yMax, viewport.yMin, -0.001f, 1.001f);
				GL.LoadProjectionMatrix(projection);
				GL.modelview = Matrix4x4.identity;
				UIRenderDevice device = this.device;
				RenderChainCommand firstCommand = this.m_FirstCommand;
				Material defaultMat = this.m_DefaultMat;
				Material defaultMat2 = this.m_DefaultMat;
				VectorImageManager vectorImageManager = this.vectorImageManager;
				device.EvaluateChain(firstCommand, defaultMat, defaultMat2, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, this.panel.scaledPixelsPerPoint, ref immediateException);
			}
			bool flag3 = immediateException != null;
			if (!flag3)
			{
				bool drawStats = this.drawStats;
				if (drawStats)
				{
					this.DrawStats();
				}
				return;
			}
			bool flag4 = GUIUtility.IsExitGUIException(immediateException);
			if (flag4)
			{
				throw immediateException;
			}
			throw new ImmediateModeException(immediateException);
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00091C28 File Offset: 0x0008FE28
		public void UIEOnChildAdded(VisualElement ve)
		{
			VisualElement parent = ve.hierarchy.parent;
			int index = ((parent != null) ? parent.hierarchy.IndexOf(ve) : 0);
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be added to an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			bool flag = parent != null && !parent.renderChainData.isInChain;
			if (!flag)
			{
				uint addedCount = RenderEvents.DepthFirstOnChildAdded(this, parent, ve, index, true);
				Debug.Assert(ve.renderChainData.isInChain);
				Debug.Assert(ve.panel == this.panel);
				this.UIEOnClippingChanged(ve, true);
				this.UIEOnOpacityChanged(ve, false);
				this.UIEOnVisualsChanged(ve, true);
				this.m_StatsElementsAdded += addedCount;
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00091CE8 File Offset: 0x0008FEE8
		public void UIEOnChildrenReordered(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be moved under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			int childrenCount = ve.hierarchy.childCount;
			for (int i = 0; i < childrenCount; i++)
			{
				RenderEvents.DepthFirstOnChildRemoving(this, ve.hierarchy[i]);
			}
			for (int j = 0; j < childrenCount; j++)
			{
				RenderEvents.DepthFirstOnChildAdded(this, ve, ve.hierarchy[j], j, false);
			}
			this.UIEOnClippingChanged(ve, true);
			this.UIEOnOpacityChanged(ve, true);
			this.UIEOnVisualsChanged(ve, true);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00091D90 File Offset: 0x0008FF90
		public void UIEOnChildRemoving(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be removed from an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			this.m_StatsElementsRemoved += RenderEvents.DepthFirstOnChildRemoving(this, ve);
			Debug.Assert(!ve.renderChainData.isInChain);
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00091DDC File Offset: 0x0008FFDC
		public void UIEOnRenderHintsChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("Render Hints cannot change under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				bool onlyDynamicColorIsDirty = (ve.renderHints & RenderHints.DirtyAll) == RenderHints.DirtyDynamicColor;
				bool flag = onlyDynamicColorIsDirty;
				if (flag)
				{
					this.UIEOnVisualsChanged(ve, false);
				}
				else
				{
					this.UIEOnChildRemoving(ve);
					this.UIEOnChildAdded(ve);
				}
				ve.MarkRenderHintsClean();
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00091E50 File Offset: 0x00090050
		public void UIEOnClippingChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change clipping state under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Clipping | (hierarchical ? RenderDataDirtyTypes.ClippingHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Clipping);
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00091E9C File Offset: 0x0009009C
		public void UIEOnOpacityChanged(VisualElement ve, bool hierarchical = false)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change opacity under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Opacity | (hierarchical ? RenderDataDirtyTypes.OpacityHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Opacity);
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00091EF0 File Offset: 0x000900F0
		public void UIEOnColorChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change background color under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Color, RenderDataDirtyTypeClasses.Color);
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00091F38 File Offset: 0x00090138
		public void UIEOnTransformOrSizeChanged(VisualElement ve, bool transformChanged, bool clipRectSizeChanged)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change size or transform under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				RenderDataDirtyTypes flags = (transformChanged ? RenderDataDirtyTypes.Transform : RenderDataDirtyTypes.None) | (clipRectSizeChanged ? RenderDataDirtyTypes.ClipRectSize : RenderDataDirtyTypes.None);
				this.m_DirtyTracker.RegisterDirty(ve, flags, RenderDataDirtyTypeClasses.TransformSize);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00091F8C File Offset: 0x0009018C
		public void UIEOnVisualsChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot be marked for dirty repaint under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Visuals | (hierarchical ? RenderDataDirtyTypes.VisualsHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Visuals);
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00091FDC File Offset: 0x000901DC
		public void UIEOnOpacityIdChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot for opacity id change under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.VisualsOpacityId, RenderDataDirtyTypeClasses.Visuals);
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00092020 File Offset: 0x00090220
		public void UIEOnDisableRenderingChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change their display style during generateVisualContent callback execution nor during visual tree rendering");
				}
				CommandManipulator.DisableElementRendering(this, ve, ve.disableRendering);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00092062 File Offset: 0x00090262
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x0009206A File Offset: 0x0009026A
		internal BaseVisualElementPanel panel { get; private set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00092073 File Offset: 0x00090273
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x0009207B File Offset: 0x0009027B
		internal UIRenderDevice device { get; private set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00092084 File Offset: 0x00090284
		public BaseElementBuilder elementBuilder
		{
			get
			{
				return this.m_VisualChangesProcessor.elementBuilder;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00092091 File Offset: 0x00090291
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x00092099 File Offset: 0x00090299
		internal AtlasBase atlas { get; private set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000920A2 File Offset: 0x000902A2
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x000920AA File Offset: 0x000902AA
		internal VectorImageManager vectorImageManager { get; private set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000920B3 File Offset: 0x000902B3
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x000920BB File Offset: 0x000902BB
		internal TempMeshAllocatorImpl tempMeshAllocator { get; private set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000920C4 File Offset: 0x000902C4
		internal MeshWriteDataPool meshWriteDataPool { get; } = new MeshWriteDataPool();

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000920CC File Offset: 0x000902CC
		internal EntryPool entryPool
		{
			get
			{
				return RenderChain.s_SharedEntryPool;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000920D3 File Offset: 0x000902D3
		public MeshGenerationDeferrer meshGenerationDeferrer
		{
			get
			{
				return this.m_MeshGenerationDeferrer;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000920DB File Offset: 0x000902DB
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000920E3 File Offset: 0x000902E3
		public MeshGenerationNodeManager meshGenerationNodeManager { get; private set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000920EC File Offset: 0x000902EC
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x000920F4 File Offset: 0x000902F4
		internal JobManager jobManager { get; private set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000920FD File Offset: 0x000902FD
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x00092105 File Offset: 0x00090305
		internal bool drawStats { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0009210E File Offset: 0x0009030E
		internal bool drawInCameras { get; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x00092116 File Offset: 0x00090316
		internal bool isFlat { get; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0009211E File Offset: 0x0009031E
		public bool forceGammaRendering { get; }

		// Token: 0x06002552 RID: 9554 RVA: 0x00092126 File Offset: 0x00090326
		internal void EnsureFitsDepth(int depth)
		{
			this.m_DirtyTracker.EnsureFits(depth);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00092138 File Offset: 0x00090338
		internal void ChildWillBeRemoved(VisualElement ve)
		{
			bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
			if (flag)
			{
				this.m_DirtyTracker.ClearDirty(ve, ~ve.renderChainData.dirtiedValues);
			}
			Debug.Assert(ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None);
			Debug.Assert(ve.renderChainData.prevDirty == null);
			Debug.Assert(ve.renderChainData.nextDirty == null);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000921B0 File Offset: 0x000903B0
		internal RenderChainCommand AllocCommand()
		{
			RenderChainCommand cmd = this.m_CommandPool.Get();
			cmd.Reset();
			return cmd;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000921D6 File Offset: 0x000903D6
		internal void FreeCommand(RenderChainCommand cmd)
		{
			cmd.Reset();
			this.m_CommandPool.Return(cmd);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000921F0 File Offset: 0x000903F0
		internal void OnRenderCommandAdded(RenderChainCommand command)
		{
			bool flag = command.prev == null;
			if (flag)
			{
				this.m_FirstCommand = command;
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00092214 File Offset: 0x00090414
		internal void OnRenderCommandsRemoved(RenderChainCommand firstCommand, RenderChainCommand lastCommand)
		{
			bool flag = firstCommand.prev == null;
			if (flag)
			{
				this.m_FirstCommand = lastCommand.next;
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0009223C File Offset: 0x0009043C
		internal void RepaintTexturedElements()
		{
			RenderChainCommand firstCommand = this.m_FirstCommand;
			for (VisualElement ve = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); ve != null; ve = ve.renderChainData.next)
			{
				bool flag = ve.renderChainData.textures != null;
				if (flag)
				{
					this.UIEOnVisualsChanged(ve, false);
				}
			}
			this.UIEOnOpacityChanged(this.panel.visualTree, false);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000922A8 File Offset: 0x000904A8
		public ExtraRenderChainVEData GetOrAddExtraData(VisualElement ve)
		{
			ExtraRenderChainVEData extraData;
			bool flag = !this.m_ExtraData.TryGetValue(ve, out extraData);
			if (flag)
			{
				extraData = this.m_ExtraDataPool.Get();
				this.m_ExtraData.Add(ve, extraData);
				ve.renderChainData.flags = ve.renderChainData.flags | RenderDataFlags.HasExtraData;
			}
			return extraData;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000922FC File Offset: 0x000904FC
		public void FreeExtraData(VisualElement ve)
		{
			Debug.Assert(ve.renderChainData.hasExtraData);
			Debug.Assert(!ve.renderChainData.hasExtraMeshes);
			ExtraRenderChainVEData extraData;
			this.m_ExtraData.Remove(ve, out extraData);
			this.m_ExtraDataPool.Return(extraData);
			ve.renderChainData.flags = ve.renderChainData.flags & ~RenderDataFlags.HasExtraData;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0009235C File Offset: 0x0009055C
		public void InsertExtraMesh(VisualElement ve, MeshHandle mesh)
		{
			ExtraRenderChainVEData extraData = this.GetOrAddExtraData(ve);
			BasicNode<MeshHandle> newNode = this.m_MeshHandleNodePool.Get();
			newNode.data = mesh;
			newNode.InsertFirst(ref extraData.extraMesh);
			ve.renderChainData.flags = ve.renderChainData.flags | RenderDataFlags.HasExtraMeshes;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000923A4 File Offset: 0x000905A4
		public void FreeExtraMeshes(VisualElement ve)
		{
			bool flag = !ve.renderChainData.hasExtraMeshes;
			if (!flag)
			{
				ExtraRenderChainVEData extraData = this.m_ExtraData[ve];
				BasicNode<MeshHandle> mesh = extraData.extraMesh;
				extraData.extraMesh = null;
				while (mesh != null)
				{
					this.device.Free(mesh.data);
					BasicNode<MeshHandle> next = mesh.next;
					mesh.data = null;
					mesh.next = null;
					this.m_MeshHandleNodePool.Return(mesh);
					mesh = next;
				}
				ve.renderChainData.flags = ve.renderChainData.flags & ~RenderDataFlags.HasExtraMeshes;
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00092438 File Offset: 0x00090638
		public void InsertTexture(VisualElement ve, Texture src, TextureId id, bool isAtlas)
		{
			BasicNode<TextureEntry> node = this.m_TexturePool.Get();
			node.data.source = src;
			node.data.actual = id;
			node.data.replaced = isAtlas;
			node.InsertFirst(ref ve.renderChainData.textures);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0009248C File Offset: 0x0009068C
		public void ResetTextures(VisualElement ve)
		{
			AtlasBase atlas = this.atlas;
			TextureRegistry registry = this.m_TextureRegistry;
			BasicNodePool<TextureEntry> pool = this.m_TexturePool;
			BasicNode<TextureEntry> current = ve.renderChainData.textures;
			ve.renderChainData.textures = null;
			while (current != null)
			{
				BasicNode<TextureEntry> next = current.next;
				bool replaced = current.data.replaced;
				if (replaced)
				{
					atlas.ReturnAtlas(ve, current.data.source as Texture2D, current.data.actual);
				}
				else
				{
					registry.Release(current.data.actual);
				}
				pool.Return(current);
				current = next;
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00092530 File Offset: 0x00090730
		private void DrawStats()
		{
			bool realDevice = this.device != null;
			float y_off = 12f;
			Rect rc = new Rect(30f, 60f, 1000f, 100f);
			GUI.Box(new Rect(20f, 40f, 200f, (float)(realDevice ? 380 : 256)), "UI Toolkit Draw Stats");
			GUI.Label(rc, "Elements added\t: " + this.m_Stats.elementsAdded.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Elements removed\t: " + this.m_Stats.elementsRemoved.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Mesh allocs allocated\t: " + this.m_Stats.newMeshAllocations.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Mesh allocs updated\t: " + this.m_Stats.updatedMeshAllocations.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Clip update roots\t: " + this.m_Stats.recursiveClipUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Clip update total\t: " + this.m_Stats.recursiveClipUpdatesExpanded.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Opacity update roots\t: " + this.m_Stats.recursiveOpacityUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Opacity update total\t: " + this.m_Stats.recursiveOpacityUpdatesExpanded.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Opacity ID update\t: " + this.m_Stats.opacityIdUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xform update roots\t: " + this.m_Stats.recursiveTransformUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xform update total\t: " + this.m_Stats.recursiveTransformUpdatesExpanded.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xformed by bone\t: " + this.m_Stats.boneTransformed.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xformed by skipping\t: " + this.m_Stats.skipTransformed.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xformed by nudging\t: " + this.m_Stats.nudgeTransformed.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Xformed by repaint\t: " + this.m_Stats.visualUpdateTransformed.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Visual update roots\t: " + this.m_Stats.recursiveVisualUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Visual update total\t: " + this.m_Stats.recursiveVisualUpdatesExpanded.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Visual update flats\t: " + this.m_Stats.nonRecursiveVisualUpdates.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Dirty processed\t: " + this.m_Stats.dirtyProcessed.ToString());
			rc.y += y_off;
			GUI.Label(rc, "Group-xform updates\t: " + this.m_Stats.groupTransformElementsChanged.ToString());
			rc.y += y_off;
			bool flag = !realDevice;
			if (!flag)
			{
				rc.y += y_off;
				UIRenderDevice.DrawStatistics drawStats = this.device.GatherDrawStatistics();
				GUI.Label(rc, "Frame index\t: " + drawStats.currentFrameIndex.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Command count\t: " + drawStats.commandCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Skip cmd counts\t: " + drawStats.skippedCommandCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Draw commands\t: " + drawStats.drawCommandCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Disable commands\t: " + drawStats.disableCommandCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Draw ranges\t: " + drawStats.drawRangeCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Draw range calls\t: " + drawStats.drawRangeCallCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Material sets\t: " + drawStats.materialSetCount.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Stencil changes\t: " + drawStats.stencilRefChanges.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Immediate draws\t: " + drawStats.immediateDraws.ToString());
				rc.y += y_off;
				GUI.Label(rc, "Total triangles\t: " + (drawStats.totalIndices / 3U).ToString());
				rc.y += y_off;
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00092B8C File Offset: 0x00090D8C
		private static VisualElement GetFirstElementInPanel(VisualElement ve)
		{
			for (;;)
			{
				bool flag;
				if (ve != null)
				{
					VisualElement prev = ve.renderChainData.prev;
					flag = prev != null && prev.renderChainData.isInChain;
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					break;
				}
				ve = ve.renderChainData.prev;
			}
			return ve;
		}

		// Token: 0x04001245 RID: 4677
		private RenderChainCommand m_FirstCommand;

		// Token: 0x04001246 RID: 4678
		private RenderChain.DepthOrderedDirtyTracking m_DirtyTracker;

		// Token: 0x04001247 RID: 4679
		private RenderChain.VisualChangesProcessor m_VisualChangesProcessor;

		// Token: 0x04001248 RID: 4680
		private LinkedPool<RenderChainCommand> m_CommandPool = new LinkedPool<RenderChainCommand>(() => new RenderChainCommand(), null, 10000);

		// Token: 0x04001249 RID: 4681
		private LinkedPool<ExtraRenderChainVEData> m_ExtraDataPool = new LinkedPool<ExtraRenderChainVEData>(() => new ExtraRenderChainVEData(), null, 10000);

		// Token: 0x0400124A RID: 4682
		private BasicNodePool<MeshHandle> m_MeshHandleNodePool = new BasicNodePool<MeshHandle>();

		// Token: 0x0400124B RID: 4683
		private BasicNodePool<TextureEntry> m_TexturePool = new BasicNodePool<TextureEntry>();

		// Token: 0x0400124C RID: 4684
		private Dictionary<VisualElement, ExtraRenderChainVEData> m_ExtraData = new Dictionary<VisualElement, ExtraRenderChainVEData>();

		// Token: 0x0400124D RID: 4685
		private MeshGenerationDeferrer m_MeshGenerationDeferrer = new MeshGenerationDeferrer();

		// Token: 0x0400124E RID: 4686
		private Material m_DefaultMat;

		// Token: 0x0400124F RID: 4687
		private bool m_BlockDirtyRegistration;

		// Token: 0x04001250 RID: 4688
		private ChainBuilderStats m_Stats;

		// Token: 0x04001251 RID: 4689
		private uint m_StatsElementsAdded;

		// Token: 0x04001252 RID: 4690
		private uint m_StatsElementsRemoved;

		// Token: 0x04001253 RID: 4691
		private TextureRegistry m_TextureRegistry = TextureRegistry.instance;

		// Token: 0x04001255 RID: 4693
		private static EntryPool s_SharedEntryPool = new EntryPool(10000);

		// Token: 0x04001256 RID: 4694
		private static readonly ProfilerMarker k_MarkerProcess = new ProfilerMarker("RenderChain.Process");

		// Token: 0x04001257 RID: 4695
		private static readonly ProfilerMarker k_MarkerClipProcessing = new ProfilerMarker("RenderChain.UpdateClips");

		// Token: 0x04001258 RID: 4696
		private static readonly ProfilerMarker k_MarkerOpacityProcessing = new ProfilerMarker("RenderChain.UpdateOpacity");

		// Token: 0x04001259 RID: 4697
		private static readonly ProfilerMarker k_MarkerColorsProcessing = new ProfilerMarker("RenderChain.UpdateColors");

		// Token: 0x0400125A RID: 4698
		private static readonly ProfilerMarker k_MarkerTransformProcessing = new ProfilerMarker("RenderChain.UpdateTransforms");

		// Token: 0x0400125B RID: 4699
		private static readonly ProfilerMarker k_MarkerVisualsProcessing = new ProfilerMarker("RenderChain.UpdateVisuals");

		// Token: 0x0400125C RID: 4700
		private static readonly ProfilerMarker k_MarkerSerialize = new ProfilerMarker("RenderChain.Serialize");

		// Token: 0x04001264 RID: 4708
		public EntryRecorder entryRecorder = new EntryRecorder(RenderChain.s_SharedEntryPool);

		// Token: 0x04001267 RID: 4711
		internal UIRVEShaderInfoAllocator shaderInfoAllocator;

		// Token: 0x02000546 RID: 1350
		private struct DepthOrderedDirtyTracking
		{
			// Token: 0x06002562 RID: 9570 RVA: 0x00092C5C File Offset: 0x00090E5C
			public void EnsureFits(int maxDepth)
			{
				while (this.heads.Count <= maxDepth)
				{
					this.heads.Add(null);
					this.tails.Add(null);
				}
			}

			// Token: 0x06002563 RID: 9571 RVA: 0x00092CA0 File Offset: 0x00090EA0
			public void RegisterDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypes, RenderDataDirtyTypeClasses dirtyTypeClass)
			{
				Debug.Assert(dirtyTypes > RenderDataDirtyTypes.None);
				int depth = ve.renderChainData.hierarchyDepth;
				this.minDepths[(int)dirtyTypeClass] = ((depth < this.minDepths[(int)dirtyTypeClass]) ? depth : this.minDepths[(int)dirtyTypeClass]);
				this.maxDepths[(int)dirtyTypeClass] = ((depth > this.maxDepths[(int)dirtyTypeClass]) ? depth : this.maxDepths[(int)dirtyTypeClass]);
				bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
				if (flag)
				{
					ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues | dirtyTypes;
				}
				else
				{
					ve.renderChainData.dirtiedValues = dirtyTypes;
					bool flag2 = this.tails[depth] != null;
					if (flag2)
					{
						this.tails[depth].renderChainData.nextDirty = ve;
						ve.renderChainData.prevDirty = this.tails[depth];
						this.tails[depth] = ve;
					}
					else
					{
						List<VisualElement> list = this.heads;
						int num = depth;
						this.tails[depth] = ve;
						list[num] = ve;
					}
				}
			}

			// Token: 0x06002564 RID: 9572 RVA: 0x00092DA8 File Offset: 0x00090FA8
			public void ClearDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypesInverse)
			{
				Debug.Assert(ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None);
				ve.renderChainData.dirtiedValues = ve.renderChainData.dirtiedValues & dirtyTypesInverse;
				bool flag = ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None;
				if (flag)
				{
					bool flag2 = ve.renderChainData.prevDirty != null;
					if (flag2)
					{
						ve.renderChainData.prevDirty.renderChainData.nextDirty = ve.renderChainData.nextDirty;
					}
					bool flag3 = ve.renderChainData.nextDirty != null;
					if (flag3)
					{
						ve.renderChainData.nextDirty.renderChainData.prevDirty = ve.renderChainData.prevDirty;
					}
					bool flag4 = this.tails[ve.renderChainData.hierarchyDepth] == ve;
					if (flag4)
					{
						Debug.Assert(ve.renderChainData.nextDirty == null);
						this.tails[ve.renderChainData.hierarchyDepth] = ve.renderChainData.prevDirty;
					}
					bool flag5 = this.heads[ve.renderChainData.hierarchyDepth] == ve;
					if (flag5)
					{
						Debug.Assert(ve.renderChainData.prevDirty == null);
						this.heads[ve.renderChainData.hierarchyDepth] = ve.renderChainData.nextDirty;
					}
					ve.renderChainData.prevDirty = (ve.renderChainData.nextDirty = null);
				}
			}

			// Token: 0x06002565 RID: 9573 RVA: 0x00092F20 File Offset: 0x00091120
			public void Reset()
			{
				for (int i = 0; i < this.minDepths.Length; i++)
				{
					this.minDepths[i] = int.MaxValue;
					this.maxDepths[i] = int.MinValue;
				}
			}

			// Token: 0x0400126C RID: 4716
			public List<VisualElement> heads;

			// Token: 0x0400126D RID: 4717
			public List<VisualElement> tails;

			// Token: 0x0400126E RID: 4718
			public int[] minDepths;

			// Token: 0x0400126F RID: 4719
			public int[] maxDepths;

			// Token: 0x04001270 RID: 4720
			public uint dirtyID;
		}

		// Token: 0x02000547 RID: 1351
		private class VisualChangesProcessor : IDisposable
		{
			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x06002566 RID: 9574 RVA: 0x00092F61 File Offset: 0x00091161
			public BaseElementBuilder elementBuilder
			{
				get
				{
					return this.m_ElementBuilder;
				}
			}

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x06002567 RID: 9575 RVA: 0x00092F69 File Offset: 0x00091169
			public MeshGenerationContext meshGenerationContext
			{
				get
				{
					return this.m_MeshGenerationContext;
				}
			}

			// Token: 0x06002568 RID: 9576 RVA: 0x00092F74 File Offset: 0x00091174
			public VisualChangesProcessor(RenderChain renderChain)
			{
				this.m_RenderChain = renderChain;
				this.m_MeshGenerationContext = new MeshGenerationContext(this.m_RenderChain.meshWriteDataPool, this.m_RenderChain.entryRecorder, this.m_RenderChain.tempMeshAllocator, this.m_RenderChain.meshGenerationDeferrer, this.m_RenderChain.meshGenerationNodeManager);
				this.m_ElementBuilder = new DefaultElementBuilder(this.m_RenderChain);
				this.m_EntryProcessingList = new List<RenderChain.VisualChangesProcessor.EntryProcessingInfo>();
				this.m_Processors = new List<EntryProcessor>(4);
			}

			// Token: 0x06002569 RID: 9577 RVA: 0x00092FFA File Offset: 0x000911FA
			public void ScheduleMeshGenerationJobs()
			{
				this.m_ElementBuilder.ScheduleMeshGenerationJobs(this.m_MeshGenerationContext);
			}

			// Token: 0x0600256A RID: 9578 RVA: 0x00093010 File Offset: 0x00091210
			public void ProcessOnVisualsChanged(VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
			{
				bool hierarchical = ve.renderChainData.pendingHierarchicalRepaint || (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None;
				bool flag = hierarchical;
				if (flag)
				{
					stats.recursiveVisualUpdates += 1U;
				}
				else
				{
					stats.nonRecursiveVisualUpdates += 1U;
				}
				this.DepthFirstOnVisualsChanged(ve, dirtyID, hierarchical, ref stats);
			}

			// Token: 0x0600256B RID: 9579 RVA: 0x00093068 File Offset: 0x00091268
			private void DepthFirstOnVisualsChanged(VisualElement ve, uint dirtyID, bool hierarchical, ref ChainBuilderStats stats)
			{
				bool flag = dirtyID == ve.renderChainData.dirtyID;
				if (!flag)
				{
					ve.renderChainData.dirtyID = dirtyID;
					if (hierarchical)
					{
						stats.recursiveVisualUpdatesExpanded += 1U;
					}
					bool flag2 = !ve.areAncestorsAndSelfDisplayed;
					if (flag2)
					{
						if (hierarchical)
						{
							ve.renderChainData.pendingHierarchicalRepaint = true;
						}
						else
						{
							ve.renderChainData.pendingRepaint = true;
						}
					}
					else
					{
						ve.renderChainData.pendingHierarchicalRepaint = false;
						ve.renderChainData.pendingRepaint = false;
						bool flag3 = !hierarchical && (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.AllVisuals) == RenderDataDirtyTypes.VisualsOpacityId;
						if (flag3)
						{
							stats.opacityIdUpdates += 1U;
							RenderChain.VisualChangesProcessor.UpdateOpacityId(ve, this.m_RenderChain);
						}
						else
						{
							RenderChain.VisualChangesProcessor.UpdateWorldFlipsWinding(ve);
							Debug.Assert(ve.renderChainData.clipMethod > ClipMethod.Undetermined);
							Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.hierarchy.parent == null || ve.renderChainData.transformID.Equals(ve.hierarchy.parent.renderChainData.transformID) || ve.renderChainData.isGroupTransform);
							bool flag4 = ve is TextElement;
							if (flag4)
							{
								RenderEvents.UpdateTextCoreSettings(this.m_RenderChain, ve);
							}
							bool flag5 = (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor;
							if (flag5)
							{
								RenderEvents.SetColorValues(this.m_RenderChain, ve);
							}
							Entry rootEntry = this.m_RenderChain.entryPool.Get();
							rootEntry.type = EntryType.DedicatedPlaceholder;
							this.m_EntryProcessingList.Add(new RenderChain.VisualChangesProcessor.EntryProcessingInfo
							{
								type = RenderChain.VisualChangesProcessor.VisualsProcessingType.Head,
								visualElement = ve,
								rootEntry = rootEntry
							});
							this.m_MeshGenerationContext.Begin(rootEntry, ve);
							this.m_ElementBuilder.Build(this.m_MeshGenerationContext);
							this.m_MeshGenerationContext.End();
							if (hierarchical)
							{
								int childrenCount = ve.hierarchy.childCount;
								for (int i = 0; i < childrenCount; i++)
								{
									this.DepthFirstOnVisualsChanged(ve.hierarchy[i], dirtyID, true, ref stats);
								}
							}
							this.m_EntryProcessingList.Add(new RenderChain.VisualChangesProcessor.EntryProcessingInfo
							{
								type = RenderChain.VisualChangesProcessor.VisualsProcessingType.Tail,
								visualElement = ve,
								rootEntry = rootEntry
							});
						}
					}
				}
			}

			// Token: 0x0600256C RID: 9580 RVA: 0x000932DC File Offset: 0x000914DC
			private static void UpdateWorldFlipsWinding(VisualElement ve)
			{
				bool flipsWinding = ve.renderChainData.localFlipsWinding;
				bool parentFlipsWinding = false;
				VisualElement parent = ve.hierarchy.parent;
				bool flag = parent != null;
				if (flag)
				{
					parentFlipsWinding = parent.renderChainData.worldFlipsWinding;
				}
				ve.renderChainData.worldFlipsWinding = parentFlipsWinding ^ flipsWinding;
			}

			// Token: 0x0600256D RID: 9581 RVA: 0x0009332C File Offset: 0x0009152C
			public void ConvertEntriesToCommands(ref ChainBuilderStats stats)
			{
				int depth = 0;
				for (int i = 0; i < this.m_EntryProcessingList.Count; i++)
				{
					RenderChain.VisualChangesProcessor.EntryProcessingInfo processingInfo = this.m_EntryProcessingList[i];
					bool flag = processingInfo.type == RenderChain.VisualChangesProcessor.VisualsProcessingType.Head;
					if (flag)
					{
						bool flag2 = depth < this.m_Processors.Count;
						EntryProcessor processor;
						if (flag2)
						{
							processor = this.m_Processors[depth];
						}
						else
						{
							processor = new EntryProcessor();
							this.m_Processors.Add(processor);
						}
						depth++;
						processor.Init(processingInfo.rootEntry, this.m_RenderChain, processingInfo.visualElement);
						processor.ProcessHead();
					}
					else
					{
						depth--;
						EntryProcessor processor2 = this.m_Processors[depth];
						processor2.ProcessTail();
						bool hasCommands = processor2.firstHeadCommand != null || processor2.firstTailCommand != null;
						bool flag3 = hasCommands;
						if (flag3)
						{
						}
						CommandManipulator.ReplaceCommands(this.m_RenderChain, processingInfo.visualElement, processor2);
					}
				}
				this.m_EntryProcessingList.Clear();
			}

			// Token: 0x0600256E RID: 9582 RVA: 0x00093440 File Offset: 0x00091640
			public static void UpdateOpacityId(VisualElement ve, RenderChain renderChain)
			{
				bool flag = ve.renderChainData.headMesh != null;
				if (flag)
				{
					RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, ve.renderChainData.headMesh);
				}
				bool flag2 = ve.renderChainData.tailMesh != null;
				if (flag2)
				{
					RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, ve.renderChainData.tailMesh);
				}
				bool hasExtraMeshes = ve.renderChainData.hasExtraMeshes;
				if (hasExtraMeshes)
				{
					ExtraRenderChainVEData extraData = renderChain.GetOrAddExtraData(ve);
					for (BasicNode<MeshHandle> extraMesh = extraData.extraMesh; extraMesh != null; extraMesh = extraMesh.next)
					{
						RenderChain.VisualChangesProcessor.DoUpdateOpacityId(ve, renderChain, extraMesh.data);
					}
				}
			}

			// Token: 0x0600256F RID: 9583 RVA: 0x000934E0 File Offset: 0x000916E0
			private static void DoUpdateOpacityId(VisualElement ve, RenderChain renderChain, MeshHandle mesh)
			{
				int vertCount = (int)mesh.allocVerts.size;
				NativeSlice<Vertex> oldVerts = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, vertCount);
				NativeSlice<Vertex> newVerts;
				renderChain.device.Update(mesh, (uint)vertCount, out newVerts);
				Color32 opacityData = renderChain.shaderInfoAllocator.OpacityAllocToVertexData(ve.renderChainData.opacityID);
				renderChain.opacityIdAccelerator.CreateJob(oldVerts, newVerts, opacityData, vertCount);
			}

			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x06002570 RID: 9584 RVA: 0x00093553 File Offset: 0x00091753
			// (set) Token: 0x06002571 RID: 9585 RVA: 0x0009355B File Offset: 0x0009175B
			private protected bool disposed { protected get; private set; }

			// Token: 0x06002572 RID: 9586 RVA: 0x00093564 File Offset: 0x00091764
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06002573 RID: 9587 RVA: 0x00093578 File Offset: 0x00091778
			protected void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						this.m_MeshGenerationContext.Dispose();
						this.m_MeshGenerationContext = null;
					}
					this.disposed = true;
				}
			}

			// Token: 0x04001271 RID: 4721
			private static readonly ProfilerMarker k_GenerateEntriesMarker = new ProfilerMarker("UIR.GenerateEntries");

			// Token: 0x04001272 RID: 4722
			private static readonly ProfilerMarker k_ConvertEntriesToCommandsMarker = new ProfilerMarker("UIR.ConvertEntriesToCommands");

			// Token: 0x04001273 RID: 4723
			private static readonly ProfilerMarker k_UpdateOpacityIdMarker = new ProfilerMarker("UIR.UpdateOpacityId");

			// Token: 0x04001274 RID: 4724
			private RenderChain m_RenderChain;

			// Token: 0x04001275 RID: 4725
			private MeshGenerationContext m_MeshGenerationContext;

			// Token: 0x04001276 RID: 4726
			private BaseElementBuilder m_ElementBuilder;

			// Token: 0x04001277 RID: 4727
			private List<RenderChain.VisualChangesProcessor.EntryProcessingInfo> m_EntryProcessingList;

			// Token: 0x04001278 RID: 4728
			private List<EntryProcessor> m_Processors;

			// Token: 0x02000548 RID: 1352
			private enum VisualsProcessingType
			{
				// Token: 0x0400127B RID: 4731
				Head,
				// Token: 0x0400127C RID: 4732
				Tail
			}

			// Token: 0x02000549 RID: 1353
			private struct EntryProcessingInfo
			{
				// Token: 0x0400127D RID: 4733
				public VisualElement visualElement;

				// Token: 0x0400127E RID: 4734
				public RenderChain.VisualChangesProcessor.VisualsProcessingType type;

				// Token: 0x0400127F RID: 4735
				public Entry rootEntry;
			}
		}
	}
}

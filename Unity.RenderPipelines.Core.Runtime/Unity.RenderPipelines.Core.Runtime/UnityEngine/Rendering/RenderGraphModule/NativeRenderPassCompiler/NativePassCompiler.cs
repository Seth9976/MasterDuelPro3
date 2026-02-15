using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000288 RID: 648
	internal class NativePassCompiler : IDisposable
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x0003F8A0 File Offset: 0x0003DAA0
		private unsafe static RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo MakeAttachmentInfo(CompilerContextData ctx, in NativePassData nativePass, int attachmentIndex)
		{
			FixedAttachmentArray<NativePassAttachment> attachments = nativePass.attachments;
			NativePassAttachment attachment = *attachments[attachmentIndex];
			ResourceUnversionedData pointTo = *ctx.UnversionedResourceData(attachment.handle);
			FixedAttachmentArray<LoadAudit> loadAudit2 = nativePass.loadAudit;
			LoadAudit loadAudit = *loadAudit2[attachmentIndex];
			string loadReason = LoadAudit.LoadReasonMessages[(int)loadAudit.reason];
			if (loadAudit.passId >= 0)
			{
				loadReason = loadReason.Replace("{pass}", "<b>" + ctx.passNames[loadAudit.passId].name + "</b>");
			}
			FixedAttachmentArray<StoreAudit> storeAudit2 = nativePass.storeAudit;
			StoreAudit storeAudit = *storeAudit2[attachmentIndex];
			string storeReason = StoreAudit.StoreReasonMessages[(int)storeAudit.reason];
			if (storeAudit.passId >= 0)
			{
				storeReason = storeReason.Replace("{pass}", "<b>" + ctx.passNames[storeAudit.passId].name + "</b>");
			}
			string storeMsaaReason = string.Empty;
			if (storeAudit.msaaReason != StoreReason.InvalidReason && storeAudit.msaaReason != StoreReason.NoMSAABuffer)
			{
				storeMsaaReason = StoreAudit.StoreReasonMessages[(int)storeAudit.msaaReason];
				if (storeAudit.msaaPassId >= 0)
				{
					storeMsaaReason = storeMsaaReason.Replace("{pass}", "<b>" + ctx.passNames[storeAudit.msaaPassId].name + "</b>");
				}
			}
			return new RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo
			{
				resourceName = pointTo.GetName(ctx, attachment.handle),
				attachmentIndex = attachmentIndex,
				loadAction = attachment.loadAction.ToString(),
				loadReason = loadReason,
				storeAction = attachment.storeAction.ToString(),
				storeReason = storeReason,
				storeMsaaReason = storeMsaaReason
			};
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003FA64 File Offset: 0x0003DC64
		internal static string MakePassBreakInfoMessage(CompilerContextData ctx, in NativePassData nativePass)
		{
			string msg = "";
			if (nativePass.breakAudit.breakPass >= 0)
			{
				msg = msg + "Failed to merge " + ctx.passNames[nativePass.breakAudit.breakPass].name + " into this native pass.\n";
			}
			return msg + PassBreakAudit.BreakReasonMessages[(int)nativePass.breakAudit.reason];
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0003FACC File Offset: 0x0003DCCC
		internal static string MakePassMergeMessage(CompilerContextData ctx, in PassData pass, in PassData prevPass, PassBreakAudit mergeResult)
		{
			string message = ((mergeResult.reason == PassBreakReason.Merged) ? "The passes are <b>compatible</b> to be merged.\n\n" : "The passes are <b>incompatible</b> to be merged.\n\n");
			PassData passData = pass;
			string passName = NativePassCompiler.InjectSpaces(passData.GetName(ctx).name);
			passData = prevPass;
			string prevPassName = NativePassCompiler.InjectSpaces(passData.GetName(ctx).name);
			switch (mergeResult.reason)
			{
			case PassBreakReason.TargetSizeMismatch:
				return message + "The fragment attachments of the passes have different sizes or sample counts.\n" + string.Format("- {0}: {1}x{2}, {3} sample(s).\n", new object[] { prevPassName, prevPass.fragmentInfoWidth, prevPass.fragmentInfoHeight, prevPass.fragmentInfoSamples }) + string.Format("- {0}: {1}x{2}, {3} sample(s).", new object[] { passName, pass.fragmentInfoWidth, pass.fragmentInfoHeight, pass.fragmentInfoSamples });
			case PassBreakReason.NextPassReadsTexture:
				return message + "The next pass reads one of the outputs as a regular texture, the pass needs to break.";
			case PassBreakReason.NonRasterPass:
				return message + string.Format("{0} is type {1}. Only Raster passes can be merged.", prevPassName, prevPass.type);
			case PassBreakReason.DifferentDepthTextures:
				return string.Concat(new string[] { message, prevPassName, " uses a different depth buffer than ", passName, "." });
			case PassBreakReason.AttachmentLimitReached:
				return message + string.Format("Merging the passes would use more than {0} attachments.", 8);
			case PassBreakReason.SubPassLimitReached:
				return message + string.Format("Merging the passes would use more than {0} native subpasses.", 8);
			case PassBreakReason.EndOfGraph:
				return message + "The pass is the last pass in the graph.";
			case PassBreakReason.Merged:
				if (pass.nativePassIndex == prevPass.nativePassIndex && pass.mergeState != PassMergeState.None)
				{
					return message + "Passes are merged.";
				}
				return message + "Passes can be merged but are not recorded consecutively.";
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0003FCC8 File Offset: 0x0003DEC8
		private static string InjectSpaces(string camelCaseString)
		{
			StringBuilder bld = new StringBuilder();
			for (int i = 0; i < camelCaseString.Length; i++)
			{
				if (char.IsUpper(camelCaseString[i]) && i != 0 && char.IsLower(camelCaseString[i - 1]))
				{
					bld.Append(" ");
				}
				bld.Append(camelCaseString[i]);
			}
			return bld.ToString();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003FD30 File Offset: 0x0003DF30
		internal unsafe void GenerateNativeCompilerDebugData(ref RenderGraph.DebugData debugData)
		{
			ref CompilerContextData ctx = ref this.contextData;
			debugData.isNRPCompiler = true;
			Dictionary<ValueTuple<RenderGraphResourceType, int>, List<int>> resourceReadLists = new Dictionary<ValueTuple<RenderGraphResourceType, int>, List<int>>();
			Dictionary<ValueTuple<RenderGraphResourceType, int>, List<int>> resourceWriteLists = new Dictionary<ValueTuple<RenderGraphResourceType, int>, List<int>>();
			foreach (RenderGraphPass renderGraphPass in this.graph.m_RenderPasses)
			{
				for (int type = 0; type < 3; type++)
				{
					int numResources = ctx.resources.unversionedData[type].Length;
					for (int resIndex = 0; resIndex < numResources; resIndex++)
					{
						foreach (ResourceHandle read in renderGraphPass.resourceReadLists[type])
						{
							if (!renderGraphPass.implicitReadsList.Contains(read) && read.type == (RenderGraphResourceType)type && read.index == resIndex)
							{
								ValueTuple<RenderGraphResourceType, int> pair = new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)type, resIndex);
								if (!resourceReadLists.ContainsKey(pair))
								{
									resourceReadLists[pair] = new List<int>();
								}
								resourceReadLists[pair].Add(renderGraphPass.index);
							}
						}
						foreach (ResourceHandle read2 in renderGraphPass.resourceWriteLists[type])
						{
							if (read2.type == (RenderGraphResourceType)type && read2.index == resIndex)
							{
								ValueTuple<RenderGraphResourceType, int> pair2 = new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)type, resIndex);
								if (!resourceWriteLists.ContainsKey(pair2))
								{
									resourceWriteLists[pair2] = new List<int>();
								}
								resourceWriteLists[pair2].Add(renderGraphPass.index);
							}
						}
					}
				}
			}
			for (int t = 0; t < 3; t++)
			{
				int numResources2 = ctx.resources.unversionedData[t].Length;
				for (int i = 0; i < numResources2; i++)
				{
					ref ResourceUnversionedData resourceUnversioned = ref ctx.resources.unversionedData[t].ElementAt(i);
					RenderGraph.DebugData.ResourceData debugResource = default(RenderGraph.DebugData.ResourceData);
					RenderGraphResourceType type2 = (RenderGraphResourceType)t;
					bool isNullResource = i == 0;
					if (!isNullResource)
					{
						string resourceName = ctx.resources.resourceNames[t][i].name;
						debugResource.name = ((!string.IsNullOrEmpty(resourceName)) ? resourceName : "(unnamed)");
						debugResource.imported = resourceUnversioned.isImported;
					}
					else
					{
						debugResource.name = "<null>";
						debugResource.imported = true;
					}
					RenderTargetInfo info = default(RenderTargetInfo);
					if (type2 == RenderGraphResourceType.Texture && !isNullResource)
					{
						ResourceHandle handle = new ResourceHandle(i, type2, false);
						try
						{
							this.graph.m_ResourcesForDebugOnly.GetRenderTargetInfo(in handle, out info);
						}
						catch (Exception)
						{
						}
					}
					debugResource.creationPassIndex = resourceUnversioned.firstUsePassID;
					debugResource.releasePassIndex = resourceUnversioned.lastUsePassID;
					debugResource.textureData = new RenderGraph.DebugData.TextureResourceData();
					debugResource.textureData.width = resourceUnversioned.width;
					debugResource.textureData.height = resourceUnversioned.height;
					debugResource.textureData.depth = resourceUnversioned.volumeDepth;
					debugResource.textureData.samples = resourceUnversioned.msaaSamples;
					debugResource.textureData.format = info.format;
					debugResource.memoryless = resourceUnversioned.memoryLess;
					debugResource.consumerList = new List<int>();
					debugResource.producerList = new List<int>();
					if (resourceReadLists.ContainsKey(new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)t, i)))
					{
						debugResource.consumerList = resourceReadLists[new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)t, i)];
					}
					if (resourceWriteLists.ContainsKey(new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)t, i)))
					{
						debugResource.producerList = resourceWriteLists[new ValueTuple<RenderGraphResourceType, int>((RenderGraphResourceType)t, i)];
					}
					debugData.resourceLists[t].Add(debugResource);
				}
			}
			for (int passId = 0; passId < ctx.passData.Length; passId++)
			{
				RenderGraphPass graphPass = this.graph.m_RenderPasses[passId];
				ref PassData passData = ref ctx.passData.ElementAt(passId);
				string passName = passData.GetName(ctx).name;
				string passDisplayName = NativePassCompiler.InjectSpaces(passName);
				RenderGraph.DebugData.PassData debugPass = default(RenderGraph.DebugData.PassData);
				debugPass.name = passDisplayName;
				debugPass.type = passData.type;
				debugPass.culled = passData.culled;
				debugPass.async = passData.asyncCompute;
				debugPass.nativeSubPassIndex = passData.nativeSubPassIndex;
				debugPass.generateDebugData = graphPass.generateDebugData;
				debugPass.resourceReadLists = new List<int>[3];
				debugPass.resourceWriteLists = new List<int>[3];
				RenderGraph.DebugData.s_PassScriptMetadata.TryGetValue(passName, out debugPass.scriptInfo);
				debugPass.syncFromPassIndex = -1;
				debugPass.syncToPassIndex = -1;
				debugPass.nrpInfo = new RenderGraph.DebugData.PassData.NRPInfo();
				debugPass.nrpInfo.width = passData.fragmentInfoWidth;
				debugPass.nrpInfo.height = passData.fragmentInfoHeight;
				debugPass.nrpInfo.volumeDepth = passData.fragmentInfoVolumeDepth;
				debugPass.nrpInfo.samples = passData.fragmentInfoSamples;
				debugPass.nrpInfo.hasDepth = passData.fragmentInfoHasDepth;
				foreach (ValueTuple<TextureHandle, int> setGlobal in graphPass.setGlobalsList)
				{
					List<int> setGlobals = debugPass.nrpInfo.setGlobals;
					ResourceHandle resourceHandle = setGlobal.Item1.handle;
					setGlobals.Add(resourceHandle.index);
				}
				for (int type3 = 0; type3 < 3; type3++)
				{
					debugPass.resourceReadLists[type3] = new List<int>();
					debugPass.resourceWriteLists[type3] = new List<int>();
					foreach (ResourceHandle resRead in graphPass.resourceReadLists[type3])
					{
						if (!graphPass.implicitReadsList.Contains(resRead))
						{
							debugPass.resourceReadLists[type3].Add(resRead.index);
						}
					}
					foreach (ResourceHandle resWrite in graphPass.resourceWriteLists[type3])
					{
						debugPass.resourceWriteLists[type3].Add(resWrite.index);
					}
				}
				ReadOnlySpan<PassFragmentData> readOnlySpan = passData.FragmentInputs(ctx);
				for (int l = 0; l < readOnlySpan.Length; l++)
				{
					PassFragmentData fragmentInput = *readOnlySpan[l];
					List<int> textureFBFetchList = debugPass.nrpInfo.textureFBFetchList;
					ResourceHandle resourceHandle = fragmentInput.resource;
					textureFBFetchList.Add(resourceHandle.index);
				}
				debugData.passList.Add(debugPass);
			}
			foreach (ref NativePassData nativePassData in ctx.NativePasses)
			{
				List<int> mergedPassIds = new List<int>();
				for (int graphPassId = nativePassData.firstGraphPass; graphPassId < nativePassData.lastGraphPass + 1; graphPassId++)
				{
					mergedPassIds.Add(graphPassId);
				}
				if (nativePassData.numGraphPasses > 0)
				{
					RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo nativePassInfo = new RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo();
					nativePassInfo.passBreakReasoning = NativePassCompiler.MakePassBreakInfoMessage(ctx, in nativePassData);
					nativePassInfo.attachmentInfos = new List<RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo>();
					int a = 0;
					for (;;)
					{
						int num = a;
						FixedAttachmentArray<NativePassAttachment> attachments = nativePassData.attachments;
						if (num >= attachments.size)
						{
							break;
						}
						nativePassInfo.attachmentInfos.Add(NativePassCompiler.MakeAttachmentInfo(ctx, in nativePassData, a));
						a++;
					}
					nativePassInfo.passCompatibility = new Dictionary<int, RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo>();
					nativePassInfo.mergedPassIds = mergedPassIds;
					for (int j = 0; j < mergedPassIds.Count; j++)
					{
						int mergedPassId = mergedPassIds[j];
						RenderGraph.DebugData.PassData debugPass2 = debugData.passList[mergedPassId];
						debugPass2.nrpInfo.nativePassInfo = nativePassInfo;
						debugData.passList[mergedPassId] = debugPass2;
					}
				}
			}
			for (int passIndex = 0; passIndex < ctx.passData.Length; passIndex++)
			{
				ref PassData pass = ref ctx.passData.ElementAt(passIndex);
				RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo nativePassInfo2 = debugData.passList[pass.passId].nrpInfo.nativePassInfo;
				if (nativePassInfo2 != null)
				{
					ReadOnlySpan<PassInputData> readOnlySpan2 = pass.Inputs(ctx);
					for (int l = 0; l < readOnlySpan2.Length; l++)
					{
						readonly ref PassInputData input = ref readOnlySpan2[l];
						ref ResourceVersionedData inputDataVersioned = ref ctx.VersionedResourceData(input.resource);
						if (inputDataVersioned.written)
						{
							PassData inputDependencyPass = ctx.passData[inputDataVersioned.writePassId];
							PassBreakAudit mergeResult = ((inputDependencyPass.nativePassIndex >= 0) ? NativePassData.CanMerge(ctx, inputDependencyPass.nativePassIndex, pass.passId) : new PassBreakAudit(PassBreakReason.NonRasterPass, pass.passId));
							string mergeMessage = "This pass writes to a resource that is read by the currently selected pass.\n\n" + NativePassCompiler.MakePassMergeMessage(ctx, in pass, in inputDependencyPass, mergeResult);
							nativePassInfo2.passCompatibility.TryAdd(inputDependencyPass.passId, new RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo
							{
								message = mergeMessage,
								isCompatible = (mergeResult.reason == PassBreakReason.Merged)
							});
						}
					}
					if (pass.nativePassIndex >= 0)
					{
						ReadOnlySpan<PassOutputData> readOnlySpan3 = pass.Outputs(ctx);
						for (int l = 0; l < readOnlySpan3.Length; l++)
						{
							readonly ref PassOutputData output = ref readOnlySpan3[l];
							if (ctx.UnversionedResourceData(output.resource).lastUsePassID != pass.passId)
							{
								int numReaders = ctx.VersionedResourceData(output.resource).numReaders;
								for (int k = 0; k < numReaders; k++)
								{
									int depIdx = ResourcesData.IndexReader(output.resource, k);
									NativeList<ResourceReaderData>[] readerData = ctx.resources.readerData;
									ResourceHandle resourceHandle = output.resource;
									ref ResourceReaderData dep = ref readerData[resourceHandle.iType].ElementAt(depIdx);
									PassData outputDependencyPass = ctx.passData[dep.passId];
									PassBreakAudit mergeResult2 = NativePassData.CanMerge(ctx, pass.nativePassIndex, outputDependencyPass.passId);
									string mergeMessage2 = "This pass reads a resource that is written to by the currently selected pass.\n\n" + NativePassCompiler.MakePassMergeMessage(ctx, in outputDependencyPass, in pass, mergeResult2);
									nativePassInfo2.passCompatibility.TryAdd(outputDependencyPass.passId, new RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo
									{
										message = mergeMessage2,
										isCompatible = (mergeResult2.reason == PassBreakReason.Merged)
									});
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00040840 File Offset: 0x0003EA40
		public NativePassCompiler(RenderGraphCompilationCache cache)
		{
			this.m_CompilationCache = cache;
			this.defaultContextData = new CompilerContextData(100);
			this.toVisitPassIds = new Stack<int>(100);
			this.m_BeginRenderPassAttachments = new NativeList<AttachmentDescriptor>(8, Allocator.Persistent);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00040894 File Offset: 0x0003EA94
		~NativePassCompiler()
		{
			this.Cleanup();
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000408C0 File Offset: 0x0003EAC0
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000408CE File Offset: 0x0003EACE
		private void Cleanup()
		{
			if (!this.m_Disposed)
			{
				this.m_BeginRenderPassAttachments.Dispose();
				this.m_Disposed = true;
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000408EC File Offset: 0x0003EAEC
		public bool Initialize(RenderGraphResourceRegistry resources, List<RenderGraphPass> renderPasses, bool disableCulling, string debugName, bool useCompilationCaching, int graphHash, int frameIndex)
		{
			bool cached = false;
			if (!useCompilationCaching)
			{
				this.contextData = this.defaultContextData;
			}
			else
			{
				cached = this.m_CompilationCache.GetCompilationCache(graphHash, frameIndex, out this.contextData);
			}
			this.graph.m_ResourcesForDebugOnly = resources;
			this.graph.m_RenderPasses = renderPasses;
			this.graph.disableCulling = disableCulling;
			this.graph.debugName = debugName;
			this.Clear(!useCompilationCaching);
			return cached;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00040960 File Offset: 0x0003EB60
		public void Compile(RenderGraphResourceRegistry resources)
		{
			this.SetupContextData(resources);
			this.BuildGraph();
			this.CullUnusedRenderPasses();
			this.TryMergeNativePasses();
			this.FindResourceUsageRanges();
			this.DetectMemoryLessResources();
			this.PrepareNativeRenderPasses();
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004098D File Offset: 0x0003EB8D
		public void Clear(bool clearContextData)
		{
			if (clearContextData)
			{
				this.contextData.Clear();
			}
			this.toVisitPassIds.Clear();
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000409A8 File Offset: 0x0003EBA8
		private void SetPassStatesForNativePass(int nativePassId)
		{
			NativePassData.SetPassStatesForNativePass(this.contextData, nativePassId);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000409B8 File Offset: 0x0003EBB8
		private void SetupContextData(RenderGraphResourceRegistry resources)
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_SetupContextData)))
			{
				this.contextData.Initialize(resources);
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00040A00 File Offset: 0x0003EC00
		private void BuildGraph()
		{
			CompilerContextData ctx = this.contextData;
			List<RenderGraphPass> passes = this.graph.m_RenderPasses;
			ctx.passData.ResizeUninitialized(passes.Count);
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_BuildGraph)))
			{
				for (int passId = 0; passId < passes.Count; passId++)
				{
					RenderGraphPass inputPass = passes[passId];
					ref PassData ctxPass = ref ctx.passData.ElementAt(passId);
					ctxPass.ResetAndInitialize(in inputPass, passId);
					DynamicArray<Name> passNames = ctx.passNames;
					Name name = new Name(inputPass.name, true);
					passNames.Add(in name);
					if (ctxPass.hasSideEffects)
					{
						this.toVisitPassIds.Push(passId);
					}
					if (ctxPass.type == RenderGraphPassType.Raster)
					{
						ctxPass.firstFragment = ctx.fragmentData.Length;
						if (inputPass.depthAccess.textureHandle.handle.IsValid())
						{
							ctxPass.fragmentInfoHasDepth = true;
							if (ctx.AddToFragmentList(inputPass.depthAccess, ctxPass.firstFragment, ctxPass.numFragments))
							{
								ctxPass.AddFragment(inputPass.depthAccess.textureHandle.handle, ctx);
							}
						}
						for (int ci = 0; ci < inputPass.colorBufferMaxIndex + 1; ci++)
						{
							if (inputPass.colorBufferAccess[ci].textureHandle.handle.IsValid() && ctx.AddToFragmentList(inputPass.colorBufferAccess[ci], ctxPass.firstFragment, ctxPass.numFragments))
							{
								ctxPass.AddFragment(inputPass.colorBufferAccess[ci].textureHandle.handle, ctx);
							}
						}
						ctxPass.firstFragmentInput = ctx.fragmentData.Length;
						for (int ci2 = 0; ci2 < inputPass.fragmentInputMaxIndex + 1; ci2++)
						{
							if (inputPass.fragmentInputAccess[ci2].textureHandle.IsValid())
							{
								TextureAccess[] fragmentInputAccess = inputPass.fragmentInputAccess;
								if (ctx.AddToFragmentList(inputPass.fragmentInputAccess[ci2], ctxPass.firstFragmentInput, ctxPass.numFragmentInputs))
								{
									ctxPass.AddFragmentInput(inputPass.fragmentInputAccess[ci2].textureHandle.handle, ctx);
								}
							}
						}
						ctxPass.firstRandomAccessResource = ctx.randomAccessResourceData.Length;
						for (int ci3 = 0; ci3 < passes[passId].randomAccessResourceMaxIndex + 1; ci3++)
						{
							ref RenderGraphPass.RandomWriteResourceInfo uav = ref passes[passId].randomAccessResource[ci3];
							if (uav.h.IsValid() && ctx.AddToRandomAccessResourceList(uav.h, ci3, uav.preserveCounterValue, ctxPass.firstRandomAccessResource, ctxPass.numRandomAccessResources))
							{
								ctxPass.AddRandomAccessResource();
							}
						}
						int numFragments = ctxPass.numFragments;
					}
					ctxPass.firstInput = ctx.inputData.Length;
					ctxPass.firstOutput = ctx.outputData.Length;
					for (int type = 0; type < 3; type++)
					{
						List<ResourceHandle> resourceWrite = inputPass.resourceWriteLists[type];
						int resourceWriteCount = resourceWrite.Count;
						for (int i = 0; i < resourceWriteCount; i++)
						{
							ResourceHandle resource = resourceWrite[i];
							if (ctx.UnversionedResourceData(resource).isImported && !ctxPass.hasSideEffects)
							{
								ctxPass.hasSideEffects = true;
								this.toVisitPassIds.Push(passId);
							}
							ctx.resources[resource].SetWritingPass(ctx, resource, passId);
							CompilerContextData compilerContextData = ctx;
							PassOutputData passOutputData = default(PassOutputData);
							passOutputData.resource = resource;
							compilerContextData.outputData.Add(in passOutputData);
							ctxPass.numOutputs++;
						}
						List<ResourceHandle> resourceRead = inputPass.resourceReadLists[type];
						int resourceReadCount = resourceRead.Count;
						for (int j = 0; j < resourceReadCount; j++)
						{
							ResourceHandle resource2 = resourceRead[j];
							ctx.resources[resource2].RegisterReadingPass(ctx, resource2, passId, ctxPass.numInputs);
							CompilerContextData compilerContextData2 = ctx;
							PassInputData passInputData = default(PassInputData);
							passInputData.resource = resource2;
							compilerContextData2.inputData.Add(in passInputData);
							ctxPass.numInputs++;
						}
						List<ResourceHandle> resourceTrans = inputPass.transientResourceList[type];
						int resourceTransCount = resourceTrans.Count;
						for (int k = 0; k < resourceTransCount; k++)
						{
							ResourceHandle resource3 = resourceTrans[k];
							ctx.resources[resource3].RegisterReadingPass(ctx, resource3, passId, ctxPass.numInputs);
							CompilerContextData compilerContextData3 = ctx;
							PassInputData passInputData = default(PassInputData);
							passInputData.resource = resource3;
							compilerContextData3.inputData.Add(in passInputData);
							ctxPass.numInputs++;
							ctx.resources[resource3].SetWritingPass(ctx, resource3, passId);
							CompilerContextData compilerContextData4 = ctx;
							PassOutputData passOutputData = default(PassOutputData);
							passOutputData.resource = resource3;
							compilerContextData4.outputData.Add(in passOutputData);
							ctxPass.numOutputs++;
						}
					}
				}
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		private void CullUnusedRenderPasses()
		{
			CompilerContextData ctx = this.contextData;
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_CullNodes)))
			{
				if (!this.graph.disableCulling)
				{
					ctx.CullAllPasses(true);
					while (this.toVisitPassIds.Count != 0)
					{
						int passId = this.toVisitPassIds.Pop();
						ref PassData passData = ref ctx.passData.ElementAt(passId);
						if (passData.culled)
						{
							ReadOnlySpan<PassInputData> readOnlySpan = passData.Inputs(ctx);
							for (int i = 0; i < readOnlySpan.Length; i++)
							{
								readonly ref PassInputData input = ref readOnlySpan[i];
								int inputPassIndex = ctx.resources[input.resource].writePassId;
								this.toVisitPassIds.Push(inputPassIndex);
							}
							passData.culled = false;
						}
					}
					int numPasses = ctx.passData.Length;
					for (int passIndex = 0; passIndex < numPasses; passIndex++)
					{
						ref PassData pass = ref ctx.passData.ElementAt(passIndex);
						if (pass.culled)
						{
							ReadOnlySpan<PassInputData> readOnlySpan = pass.Inputs(ctx);
							for (int i = 0; i < readOnlySpan.Length; i++)
							{
								ResourceHandle inputResource = readOnlySpan[i].resource;
								ctx.resources[inputResource].RemoveReadingPass(ctx, inputResource, pass.passId);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004106C File Offset: 0x0003F26C
		private void TryMergeNativePasses()
		{
			CompilerContextData ctx = this.contextData;
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_TryMergeNativePasses)))
			{
				int activeNativePassId = -1;
				for (int passIdx = 0; passIdx < ctx.passData.Length; passIdx++)
				{
					ref PassData passToAdd = ref ctx.passData.ElementAt(passIdx);
					if (!passToAdd.culled)
					{
						if (activeNativePassId == -1)
						{
							if (passToAdd.type == RenderGraphPassType.Raster)
							{
								CompilerContextData compilerContextData = ctx;
								NativePassData nativePassData = new NativePassData(ref passToAdd, ctx);
								compilerContextData.nativePassData.Add(in nativePassData);
								passToAdd.nativePassIndex = (ref ctx.nativePassData).LastIndex<NativePassData>();
								activeNativePassId = passToAdd.nativePassIndex;
							}
						}
						else
						{
							PassBreakAudit mergeTestResult = NativePassData.TryMerge(this.contextData, activeNativePassId, passIdx);
							if (mergeTestResult.reason != PassBreakReason.Merged)
							{
								this.SetPassStatesForNativePass(activeNativePassId);
								if (mergeTestResult.reason == PassBreakReason.NonRasterPass)
								{
									activeNativePassId = -1;
								}
								else
								{
									CompilerContextData compilerContextData2 = ctx;
									NativePassData nativePassData = new NativePassData(ref passToAdd, ctx);
									compilerContextData2.nativePassData.Add(in nativePassData);
									passToAdd.nativePassIndex = (ref ctx.nativePassData).LastIndex<NativePassData>();
									activeNativePassId = passToAdd.nativePassIndex;
								}
							}
						}
					}
				}
				if (activeNativePassId >= 0)
				{
					this.SetPassStatesForNativePass(activeNativePassId);
				}
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00041194 File Offset: 0x0003F394
		private void FindResourceUsageRanges()
		{
			CompilerContextData ctx = this.contextData;
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_FindResourceUsageRanges)))
			{
				for (int passIndex = 0; passIndex < ctx.passData.Length; passIndex++)
				{
					ref PassData pass = ref ctx.passData.ElementAt(passIndex);
					if (!pass.culled)
					{
						ReadOnlySpan<PassInputData> readOnlySpan = pass.Inputs(ctx);
						for (int j = 0; j < readOnlySpan.Length; j++)
						{
							ResourceHandle inputResource = readOnlySpan[j].resource;
							ref ResourceUnversionedData pointTo = ref ctx.UnversionedResourceData(inputResource);
							pointTo.lastUsePassID = -1;
							if (inputResource.version == 0 && pointTo.firstUsePassID < 0)
							{
								pointTo.firstUsePassID = pass.passId;
								pass.AddFirstUse(inputResource, ctx);
							}
							if (pointTo.latestVersionNumber == inputResource.version)
							{
								pointTo.tag++;
							}
						}
						ReadOnlySpan<PassOutputData> readOnlySpan2 = pass.Outputs(ctx);
						for (int j = 0; j < readOnlySpan2.Length; j++)
						{
							ResourceHandle outputResource = readOnlySpan2[j].resource;
							ref ResourceUnversionedData pointTo2 = ref ctx.UnversionedResourceData(outputResource);
							if (outputResource.version == 1 && pointTo2.firstUsePassID < 0)
							{
								pointTo2.firstUsePassID = pass.passId;
								pass.AddFirstUse(outputResource, ctx);
							}
							if (pointTo2.latestVersionNumber == outputResource.version)
							{
								pointTo2.lastWritePassID = pass.passId;
							}
						}
					}
				}
				for (int passIndex2 = 0; passIndex2 < ctx.passData.Length; passIndex2++)
				{
					ref PassData pass2 = ref ctx.passData.ElementAt(passIndex2);
					if (!pass2.culled)
					{
						pass2.waitOnGraphicsFencePassId = -1;
						pass2.insertGraphicsFence = false;
						ReadOnlySpan<PassInputData> readOnlySpan = pass2.Inputs(ctx);
						for (int j = 0; j < readOnlySpan.Length; j++)
						{
							ResourceHandle inputResource2 = readOnlySpan[j].resource;
							ref ResourceUnversionedData pointTo3 = ref ctx.UnversionedResourceData(inputResource2);
							if (pointTo3.latestVersionNumber == inputResource2.version)
							{
								int refC = pointTo3.tag - 1;
								if (refC == 0)
								{
									pointTo3.lastUsePassID = pass2.passId;
									pass2.AddLastUse(inputResource2, ctx);
								}
								pointTo3.tag = refC;
							}
							if (pass2.waitOnGraphicsFencePassId == -1)
							{
								ref ResourceVersionedData pointToVer = ref ctx.VersionedResourceData(inputResource2);
								ref PassData wPass = ref ctx.passData.ElementAt(pointToVer.writePassId);
								if (wPass.asyncCompute != pass2.asyncCompute)
								{
									pass2.waitOnGraphicsFencePassId = wPass.passId;
								}
							}
						}
						ReadOnlySpan<PassOutputData> readOnlySpan2 = pass2.Outputs(ctx);
						for (int j = 0; j < readOnlySpan2.Length; j++)
						{
							ResourceHandle outputResource2 = readOnlySpan2[j].resource;
							ref ResourceUnversionedData pointTo4 = ref ctx.UnversionedResourceData(outputResource2);
							ref ResourceVersionedData pointToVer2 = ref ctx.VersionedResourceData(outputResource2);
							if (pointTo4.latestVersionNumber == outputResource2.version && pointToVer2.numReaders == 0)
							{
								pointTo4.lastUsePassID = pass2.passId;
								pass2.AddLastUse(outputResource2, ctx);
							}
							int numReaders = pointToVer2.numReaders;
							for (int i = 0; i < numReaders; i++)
							{
								int depIdx = ResourcesData.IndexReader(outputResource2, i);
								ref ResourceReaderData dep = ref ctx.resources.readerData[outputResource2.iType].ElementAt(depIdx);
								ref PassData depPass = ref ctx.passData.ElementAt(dep.passId);
								if (pass2.asyncCompute != depPass.asyncCompute)
								{
									pass2.insertGraphicsFence = true;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004151C File Offset: 0x0003F71C
		private void PrepareNativeRenderPasses()
		{
			for (int passIdx = 0; passIdx < this.contextData.nativePassData.Length; passIdx++)
			{
				ref NativePassData nativePassData = ref this.contextData.nativePassData.ElementAt(passIdx);
				this.DetermineLoadStoreActions(ref nativePassData);
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00041560 File Offset: 0x0003F760
		private static bool IsGlobalTextureInPass(RenderGraphPass pass, ResourceHandle handle)
		{
			foreach (ValueTuple<TextureHandle, int> g in pass.setGlobalsList)
			{
				ResourceHandle handle2 = g.Item1.handle;
				if (handle2.index == handle.index)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000415D0 File Offset: 0x0003F7D0
		private void DetectMemoryLessResources()
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_DetectMemorylessResources)))
			{
				foreach (ref NativePassData nativePass in this.contextData.NativePasses)
				{
					ReadOnlySpan<PassData> graphPasses = nativePass.GraphPasses(this.contextData);
					ReadOnlySpan<PassData> readOnlySpan = graphPasses;
					for (int i = 0; i < readOnlySpan.Length; i++)
					{
						readonly ref PassData subPass = ref readOnlySpan[i];
						ReadOnlySpan<ResourceHandle> readOnlySpan2 = subPass.FirstUsedResources(this.contextData);
						for (int j = 0; j < readOnlySpan2.Length; j++)
						{
							readonly ref ResourceHandle createdRes = ref readOnlySpan2[j];
							ref ResourceUnversionedData createInfo = ref this.contextData.UnversionedResourceData(createdRes);
							if (createdRes.type == RenderGraphResourceType.Texture && !createInfo.isImported)
							{
								bool isGlobal = NativePassCompiler.IsGlobalTextureInPass(this.graph.m_RenderPasses[subPass.passId], createdRes);
								ReadOnlySpan<PassData> readOnlySpan3 = graphPasses;
								for (int k = 0; k < readOnlySpan3.Length; k++)
								{
									readonly ref PassData subPass2 = ref readOnlySpan3[k];
									ReadOnlySpan<ResourceHandle> readOnlySpan4 = subPass2.LastUsedResources(this.contextData);
									for (int l = 0; l < readOnlySpan4.Length; l++)
									{
										readonly ref ResourceHandle destroyedRes = ref readOnlySpan4[l];
										ref ResourceUnversionedData destInfo = ref this.contextData.UnversionedResourceData(destroyedRes);
										if (destroyedRes.type == RenderGraphResourceType.Texture && !destInfo.isImported)
										{
											ResourceHandle resourceHandle = createdRes;
											int index = resourceHandle.index;
											resourceHandle = destroyedRes;
											if (index == resourceHandle.index && !isGlobal && (nativePass.numNativeSubPasses > 1 || subPass2.IsUsedAsFragment(createdRes, this.contextData)))
											{
												createInfo.memoryLess = true;
												destInfo.memoryLess = true;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000417E0 File Offset: 0x0003F9E0
		internal static bool IsSameNativeSubPass(ref SubPassDescriptor a, ref SubPassDescriptor b)
		{
			if (a.flags != b.flags || a.colorOutputs.Length != b.colorOutputs.Length || a.inputs.Length != b.inputs.Length)
			{
				return false;
			}
			for (int i = 0; i < a.colorOutputs.Length; i++)
			{
				if (a.colorOutputs[i] != b.colorOutputs[i])
				{
					return false;
				}
			}
			for (int j = 0; j < a.inputs.Length; j++)
			{
				if (a.inputs[j] != b.inputs[j])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00041894 File Offset: 0x0003FA94
		private void ExecuteCreateRessource(InternalRenderGraphContext rgContext, RenderGraphResourceRegistry resources, in PassData pass)
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteCreateResources)))
			{
				resources.forceManualClearOfResource = true;
				if (pass.type == RenderGraphPassType.Raster && pass.nativePassIndex >= 0)
				{
					if (pass.mergeState == PassMergeState.Begin || pass.mergeState == PassMergeState.None)
					{
						ReadOnlySpan<PassData> readOnlySpan = this.contextData.nativePassData.ElementAt(pass.nativePassIndex).GraphPasses(this.contextData);
						for (int i = 0; i < readOnlySpan.Length; i++)
						{
							readonly ref PassData subPass = ref readOnlySpan[i];
							ReadOnlySpan<ResourceHandle> readOnlySpan2 = subPass.FirstUsedResources(this.contextData);
							for (int j = 0; j < readOnlySpan2.Length; j++)
							{
								readonly ref ResourceHandle res = ref readOnlySpan2[j];
								ref ResourceUnversionedData resInfo = ref this.contextData.UnversionedResourceData(res);
								if (!resInfo.isImported && !resInfo.memoryLess)
								{
									bool usedAsFragmentThisPass = subPass.IsUsedAsFragment(res, this.contextData);
									resources.forceManualClearOfResource = !usedAsFragmentThisPass;
									ResourceHandle resourceHandle = res;
									int iType = resourceHandle.iType;
									resourceHandle = res;
									resources.CreatePooledResource(rgContext, iType, resourceHandle.index);
								}
							}
						}
					}
				}
				else
				{
					ReadOnlySpan<ResourceHandle> readOnlySpan2 = pass.FirstUsedResources(this.contextData);
					for (int i = 0; i < readOnlySpan2.Length; i++)
					{
						readonly ref ResourceHandle create = ref readOnlySpan2[i];
						if (!this.contextData.UnversionedResourceData(create).isImported)
						{
							ResourceHandle resourceHandle = create;
							int iType2 = resourceHandle.iType;
							resourceHandle = create;
							resources.CreatePooledResource(rgContext, iType2, resourceHandle.index);
						}
					}
				}
				resources.forceManualClearOfResource = true;
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00041A64 File Offset: 0x0003FC64
		private void DetermineLoadStoreActions(ref NativePassData nativePass)
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_PrepareNativePass)))
			{
				this.contextData.passData.ElementAt(nativePass.firstGraphPass);
				this.contextData.passData.ElementAt(nativePass.lastGraphPass);
				if (nativePass.fragments.size > 0)
				{
					ref FixedAttachmentArray<PassFragmentData> fragmentList = ref nativePass.fragments;
					int fragmentId = 0;
					for (;;)
					{
						int num = fragmentId;
						FixedAttachmentArray<PassFragmentData> fixedAttachmentArray = fragmentList;
						if (num >= fixedAttachmentArray.size)
						{
							break;
						}
						fixedAttachmentArray = fragmentList;
						ref PassFragmentData fragment = ref fixedAttachmentArray[fragmentId];
						NativePassAttachment nativePassAttachment = default(NativePassAttachment);
						int idx = nativePass.attachments.Add(in nativePassAttachment);
						ref NativePassAttachment currAttachment = ref nativePass.attachments[idx];
						currAttachment.handle = fragment.resource;
						currAttachment.mipLevel = fragment.mipLevel;
						currAttachment.depthSlice = fragment.depthSlice;
						currAttachment.loadAction = RenderBufferLoadAction.DontCare;
						currAttachment.storeAction = RenderBufferStoreAction.DontCare;
						bool partialWrite = fragment.accessFlags.HasFlag(AccessFlags.Write) && !fragment.accessFlags.HasFlag(AccessFlags.Discard);
						ref ResourceUnversionedData resourceData = ref this.contextData.UnversionedResourceData(fragment.resource);
						bool isImported = resourceData.isImported;
						int destroyPassID = resourceData.lastUsePassID;
						bool usedAfterThisNativePass = destroyPassID >= nativePass.lastGraphPass + 1;
						if (fragment.accessFlags.HasFlag(AccessFlags.Read) || partialWrite)
						{
							if (resourceData.firstUsePassID < nativePass.firstGraphPass)
							{
								currAttachment.loadAction = RenderBufferLoadAction.Load;
								if (usedAfterThisNativePass)
								{
									currAttachment.storeAction = RenderBufferStoreAction.Store;
								}
							}
							else if (isImported)
							{
								if (resourceData.clear)
								{
									currAttachment.loadAction = RenderBufferLoadAction.Clear;
								}
								else
								{
									currAttachment.loadAction = RenderBufferLoadAction.Load;
								}
							}
							else
							{
								currAttachment.loadAction = RenderBufferLoadAction.Clear;
							}
						}
						if (fragment.accessFlags.HasFlag(AccessFlags.Write))
						{
							if (nativePass.samples <= 1)
							{
								if (usedAfterThisNativePass)
								{
									currAttachment.storeAction = RenderBufferStoreAction.Store;
								}
								else if (isImported)
								{
									if (resourceData.discard)
									{
										currAttachment.storeAction = RenderBufferStoreAction.DontCare;
									}
									else
									{
										currAttachment.storeAction = RenderBufferStoreAction.Store;
									}
								}
								else
								{
									currAttachment.storeAction = RenderBufferStoreAction.DontCare;
								}
							}
							else
							{
								currAttachment.storeAction = RenderBufferStoreAction.DontCare;
								int latestVersionNumber = resourceData.latestVersionNumber;
								ResourceHandle resource = fragment.resource;
								bool lastWriter = latestVersionNumber == resource.version;
								bool isImportedLastWriter = isImported && lastWriter;
								if (destroyPassID >= nativePass.firstGraphPass + nativePass.numGraphPasses)
								{
									bool needsMSAASamples = isImportedLastWriter && !resourceData.discard;
									bool needsResolvedData = isImportedLastWriter && !resourceData.bindMS;
									ReadOnlySpan<ResourceReaderData> readOnlySpan = this.contextData.Readers(fragment.resource);
									for (int i = 0; i < readOnlySpan.Length; i++)
									{
										readonly ref ResourceReaderData reader = ref readOnlySpan[i];
										ref PassData ptr = ref this.contextData.passData.ElementAt(reader.passId);
										bool isFragment = ptr.IsUsedAsFragment(fragment.resource, this.contextData);
										if (ptr.type == RenderGraphPassType.Unsafe)
										{
											needsMSAASamples = true;
											needsResolvedData = !resourceData.bindMS;
											break;
										}
										if (isFragment)
										{
											needsMSAASamples = true;
										}
										else if (resourceData.bindMS)
										{
											needsMSAASamples = true;
										}
										else
										{
											needsResolvedData = true;
										}
									}
									if (needsMSAASamples && needsResolvedData)
									{
										currAttachment.storeAction = RenderBufferStoreAction.StoreAndResolve;
									}
									else if (needsResolvedData)
									{
										currAttachment.storeAction = RenderBufferStoreAction.Resolve;
									}
									else if (needsMSAASamples)
									{
										currAttachment.storeAction = RenderBufferStoreAction.Store;
									}
								}
								else if (isImportedLastWriter)
								{
									if (resourceData.bindMS)
									{
										if (resourceData.discard)
										{
											currAttachment.storeAction = RenderBufferStoreAction.DontCare;
										}
										else
										{
											currAttachment.storeAction = RenderBufferStoreAction.Store;
										}
									}
									else if (resourceData.discard)
									{
										bool isDepthAttachment = nativePass.hasDepth && idx == 0;
										currAttachment.storeAction = (isDepthAttachment ? RenderBufferStoreAction.DontCare : RenderBufferStoreAction.Resolve);
									}
									else
									{
										currAttachment.storeAction = RenderBufferStoreAction.StoreAndResolve;
									}
								}
							}
						}
						if (resourceData.memoryLess)
						{
							currAttachment.memoryless = true;
						}
						fragmentId++;
					}
				}
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00041E50 File Offset: 0x00040050
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateNativePass(in NativePassData nativePass, int width, int height, int depth, int samples, int attachmentCount)
		{
			if (RenderGraph.enableValidityChecks)
			{
				FixedAttachmentArray<NativePassAttachment> attachments = nativePass.attachments;
				if (attachments.size == 0 || nativePass.numNativeSubPasses == 0)
				{
					throw new Exception("Empty render pass");
				}
				if (width == 0 || height == 0 || depth == 0 || samples == 0 || nativePass.numNativeSubPasses == 0 || attachmentCount == 0)
				{
					throw new Exception("Invalid render pass properties. One or more properties are zero.");
				}
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00041EAC File Offset: 0x000400AC
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void ValidateAttachmentRenderTarget(in RenderTargetInfo attRenderTargetInfo, RenderGraphResourceRegistry resources, int nativePassWidth, int nativePassHeight, int nativePassMSAASamples)
		{
			if (RenderGraph.enableValidityChecks && (attRenderTargetInfo.width != nativePassWidth || attRenderTargetInfo.height != nativePassHeight || attRenderTargetInfo.msaaSamples != nativePassMSAASamples))
			{
				throw new Exception("Low level rendergraph error: Attachments in renderpass do not match!");
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00041EE0 File Offset: 0x000400E0
		internal unsafe void ExecuteBeginRenderPass(InternalRenderGraphContext rgContext, RenderGraphResourceRegistry resources, ref NativePassData nativePass)
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteBeginRenderpassCommand)))
			{
				ref FixedAttachmentArray<NativePassAttachment> attachments = ref nativePass.attachments;
				int attachmentCount = attachments.size;
				ref PassData ptr = ref this.contextData.passData.ElementAt(nativePass.firstGraphPass);
				int w = ptr.fragmentInfoWidth;
				int h = ptr.fragmentInfoHeight;
				int d = ptr.fragmentInfoVolumeDepth;
				int s = ptr.fragmentInfoSamples;
				NativeArray<SubPassDescriptor> nativeSubPassArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<SubPassDescriptor>((void*)(this.contextData.nativeSubPassData.GetUnsafeReadOnlyPtr<SubPassDescriptor>() + nativePass.firstNativeSubPass), nativePass.numNativeSubPasses, Allocator.None);
				if (nativePass.hasFoveatedRasterization)
				{
					rgContext.cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
				}
				this.m_BeginRenderPassAttachments.Resize(attachmentCount, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < attachmentCount; i++)
				{
					ref ResourceHandle currAttachmentHandle = ref attachments[i].handle;
					RenderTargetInfo renderTargetInfo;
					resources.GetRenderTargetInfo(in currAttachmentHandle, out renderTargetInfo);
					ref AttachmentDescriptor currBeginAttachment = ref this.m_BeginRenderPassAttachments.ElementAt(i);
					currBeginAttachment = new AttachmentDescriptor(renderTargetInfo.format);
					if (!attachments[i].memoryless)
					{
						RTHandle rtHandle = resources.GetTexture(currAttachmentHandle.index);
						RenderTargetIdentifier rtidAllSlices = rtHandle;
						currBeginAttachment.loadStoreTarget = new RenderTargetIdentifier(rtidAllSlices, attachments[i].mipLevel, CubemapFace.Unknown, attachments[i].depthSlice);
						if (attachments[i].storeAction == RenderBufferStoreAction.Resolve || attachments[i].storeAction == RenderBufferStoreAction.StoreAndResolve)
						{
							currBeginAttachment.resolveTarget = rtHandle;
						}
					}
					currBeginAttachment.loadAction = attachments[i].loadAction;
					currBeginAttachment.storeAction = attachments[i].storeAction;
					if (attachments[i].loadAction == RenderBufferLoadAction.Clear)
					{
						currBeginAttachment.clearColor = Color.red;
						currBeginAttachment.clearDepth = 1f;
						currBeginAttachment.clearStencil = 0U;
						TextureDesc desc = resources.GetTextureResourceDesc(in currAttachmentHandle, true);
						if (i == 0 && nativePass.hasDepth)
						{
							currBeginAttachment.clearDepth = 1f;
						}
						else
						{
							currBeginAttachment.clearColor = desc.clearColor;
						}
					}
				}
				NativeArray<AttachmentDescriptor> attachmentDescArray = this.m_BeginRenderPassAttachments.AsArray();
				int depthAttachmentIndex = (nativePass.hasDepth ? 0 : (-1));
				ReadOnlySpan<byte> graphPassNamesForDebugSpan = ReadOnlySpan<byte>.Empty;
				rgContext.cmd.BeginRenderPass(w, h, d, s, attachmentDescArray, depthAttachmentIndex, nativeSubPassArray, graphPassNamesForDebugSpan);
				CommandBuffer.ThrowOnSetRenderTarget = true;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00042158 File Offset: 0x00040358
		private void ExecuteDestroyResource(InternalRenderGraphContext rgContext, RenderGraphResourceRegistry resources, ref PassData pass)
		{
			using (new ProfilingScope(ProfilingSampler.Get<NativePassCompiler.NativeCompilerProfileId>(NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteDestroyResources)))
			{
				if (pass.type == RenderGraphPassType.Raster && pass.nativePassIndex >= 0)
				{
					if (pass.mergeState == PassMergeState.End || pass.mergeState == PassMergeState.None)
					{
						ReadOnlySpan<PassData> readOnlySpan = this.contextData.nativePassData.ElementAt(pass.nativePassIndex).GraphPasses(this.contextData);
						for (int i = 0; i < readOnlySpan.Length; i++)
						{
							ReadOnlySpan<ResourceHandle> readOnlySpan2 = readOnlySpan[i].LastUsedResources(this.contextData);
							for (int j = 0; j < readOnlySpan2.Length; j++)
							{
								readonly ref ResourceHandle res = ref readOnlySpan2[j];
								ref ResourceUnversionedData resInfo = ref this.contextData.UnversionedResourceData(res);
								if (!resInfo.isImported && !resInfo.memoryLess)
								{
									ResourceHandle resourceHandle = res;
									int iType = resourceHandle.iType;
									resourceHandle = res;
									resources.ReleasePooledResource(rgContext, iType, resourceHandle.index);
								}
							}
						}
					}
				}
				else
				{
					ReadOnlySpan<ResourceHandle> readOnlySpan2 = pass.LastUsedResources(this.contextData);
					for (int i = 0; i < readOnlySpan2.Length; i++)
					{
						readonly ref ResourceHandle destroy = ref readOnlySpan2[i];
						if (!this.contextData.UnversionedResourceData(destroy).isImported)
						{
							ResourceHandle resourceHandle = destroy;
							int iType2 = resourceHandle.iType;
							resourceHandle = destroy;
							resources.ReleasePooledResource(rgContext, iType2, resourceHandle.index);
						}
					}
				}
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000422F4 File Offset: 0x000404F4
		internal void SetRandomWriteTarget(in CommandBuffer cmd, RenderGraphResourceRegistry resources, int index, ResourceHandle resource, bool preserveCounterValue = true)
		{
			if (resource.type == RenderGraphResourceType.Texture)
			{
				RTHandle tex = resources.GetTexture(resource.index);
				cmd.SetRandomWriteTarget(index, tex);
				return;
			}
			if (resource.type != RenderGraphResourceType.Buffer)
			{
				throw new Exception(string.Format("Invalid resource type {0}, expected texture or buffer", resource.type));
			}
			GraphicsBuffer buff = resources.GetBuffer(resource.index);
			if (preserveCounterValue)
			{
				cmd.SetRandomWriteTarget(index, buff);
				return;
			}
			cmd.SetRandomWriteTarget(index, buff, false);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00042374 File Offset: 0x00040574
		internal void ExecuteGraphNode(ref InternalRenderGraphContext rgContext, RenderGraphResourceRegistry resources, RenderGraphPass pass)
		{
			rgContext.executingPass = pass;
			if (!pass.HasRenderFunc())
			{
				throw new InvalidOperationException(string.Format("RenderPass {0} was not provided with an execute function.", pass.name));
			}
			using (new ProfilingScope(rgContext.cmd, pass.customSampler))
			{
				pass.Execute(rgContext);
				foreach (ValueTuple<TextureHandle, int> tex in pass.setGlobalsList)
				{
					rgContext.cmd.SetGlobalTexture(tex.Item2, tex.Item1);
				}
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004243C File Offset: 0x0004063C
		public unsafe void ExecuteGraph(InternalRenderGraphContext rgContext, RenderGraphResourceRegistry resources, in List<RenderGraphPass> passes)
		{
			bool inRenderPass = false;
			this.previousCommandBuffer = rgContext.cmd;
			rgContext.cmd.ClearRandomWriteTargets();
			for (int passIndex = 0; passIndex < this.contextData.passData.Length; passIndex++)
			{
				ref PassData pass = ref this.contextData.passData.ElementAt(passIndex);
				if (!pass.culled)
				{
					bool isRaster = pass.type == RenderGraphPassType.Raster;
					this.ExecuteCreateRessource(rgContext, resources, in pass);
					bool isAsyncCompute = pass.type == RenderGraphPassType.Compute && pass.asyncCompute;
					if (isAsyncCompute)
					{
						if (!rgContext.contextlessTesting)
						{
							rgContext.renderContext.ExecuteCommandBuffer(rgContext.cmd);
						}
						rgContext.cmd.Clear();
						CommandBuffer asyncCmd = CommandBufferPool.Get("async cmd");
						asyncCmd.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
						rgContext.cmd = asyncCmd;
					}
					if (pass.waitOnGraphicsFencePassId != -1)
					{
						GraphicsFence fence = this.contextData.fences[pass.waitOnGraphicsFencePassId];
						rgContext.cmd.WaitOnAsyncGraphicsFence(fence);
					}
					bool nrpBegan = false;
					if (isRaster && pass.mergeState <= PassMergeState.Begin && pass.nativePassIndex >= 0)
					{
						ref NativePassData nativePass = ref this.contextData.nativePassData.ElementAt(pass.nativePassIndex);
						if (nativePass.fragments.size > 0)
						{
							this.ExecuteBeginRenderPass(rgContext, resources, ref nativePass);
							nrpBegan = true;
							inRenderPass = true;
						}
					}
					if (pass.mergeState >= PassMergeState.SubPass && pass.beginNativeSubpass)
					{
						if (!inRenderPass)
						{
							throw new Exception("Compiler error: Pass is marked as beginning a native sub pass but no pass is currently active.");
						}
						rgContext.cmd.NextSubPass();
					}
					if (pass.numRandomAccessResources > 0)
					{
						ReadOnlySpan<PassRandomWriteData> readOnlySpan = pass.RandomWriteTextures(this.contextData);
						for (int i = 0; i < readOnlySpan.Length; i++)
						{
							PassRandomWriteData randomWriteAttachment = *readOnlySpan[i];
							this.SetRandomWriteTarget(in rgContext.cmd, resources, randomWriteAttachment.index, randomWriteAttachment.resource, true);
						}
					}
					this.ExecuteGraphNode(ref rgContext, resources, passes[pass.passId]);
					if (pass.numRandomAccessResources > 0)
					{
						rgContext.cmd.ClearRandomWriteTargets();
					}
					if (pass.insertGraphicsFence)
					{
						GraphicsFence fence2 = rgContext.cmd.CreateAsyncGraphicsFence();
						this.contextData.fences[pass.passId] = fence2;
					}
					if (isRaster)
					{
						if (((pass.mergeState == PassMergeState.None && nrpBegan) || pass.mergeState == PassMergeState.End) && pass.nativePassIndex >= 0)
						{
							ref NativePassData nativePass2 = ref this.contextData.nativePassData.ElementAt(pass.nativePassIndex);
							if (nativePass2.fragments.size > 0)
							{
								if (!inRenderPass)
								{
									throw new Exception("Compiler error: Generated a subpass pass but no pass is currently active.");
								}
								if (nativePass2.hasFoveatedRasterization)
								{
									rgContext.cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
								}
								rgContext.cmd.EndRenderPass();
								CommandBuffer.ThrowOnSetRenderTarget = false;
								inRenderPass = false;
							}
						}
					}
					else if (isAsyncCompute)
					{
						rgContext.renderContext.ExecuteCommandBufferAsync(rgContext.cmd, ComputeQueueType.Background);
						CommandBufferPool.Release(rgContext.cmd);
						rgContext.cmd = this.previousCommandBuffer;
					}
					this.ExecuteDestroyResource(rgContext, resources, ref pass);
				}
			}
		}

		// Token: 0x04000B59 RID: 2905
		internal NativePassCompiler.RenderGraphInputInfo graph;

		// Token: 0x04000B5A RID: 2906
		internal CompilerContextData contextData;

		// Token: 0x04000B5B RID: 2907
		internal CompilerContextData defaultContextData;

		// Token: 0x04000B5C RID: 2908
		internal CommandBuffer previousCommandBuffer;

		// Token: 0x04000B5D RID: 2909
		private Stack<int> toVisitPassIds;

		// Token: 0x04000B5E RID: 2910
		private RenderGraphCompilationCache m_CompilationCache;

		// Token: 0x04000B5F RID: 2911
		internal const int k_EstimatedPassCount = 100;

		// Token: 0x04000B60 RID: 2912
		internal const int k_MaxSubpass = 8;

		// Token: 0x04000B61 RID: 2913
		private NativeList<AttachmentDescriptor> m_BeginRenderPassAttachments;

		// Token: 0x04000B62 RID: 2914
		private bool m_Disposed;

		// Token: 0x04000B63 RID: 2915
		private const int ArbitraryMaxNbMergedPasses = 16;

		// Token: 0x04000B64 RID: 2916
		private DynamicArray<Name> graphPassNamesForDebug = new DynamicArray<Name>(16);

		// Token: 0x02000289 RID: 649
		internal struct RenderGraphInputInfo
		{
			// Token: 0x04000B65 RID: 2917
			public RenderGraphResourceRegistry m_ResourcesForDebugOnly;

			// Token: 0x04000B66 RID: 2918
			public List<RenderGraphPass> m_RenderPasses;

			// Token: 0x04000B67 RID: 2919
			public string debugName;

			// Token: 0x04000B68 RID: 2920
			public bool disableCulling;
		}

		// Token: 0x0200028A RID: 650
		internal enum NativeCompilerProfileId
		{
			// Token: 0x04000B6A RID: 2922
			NRPRGComp_PrepareNativePass,
			// Token: 0x04000B6B RID: 2923
			NRPRGComp_SetupContextData,
			// Token: 0x04000B6C RID: 2924
			NRPRGComp_BuildGraph,
			// Token: 0x04000B6D RID: 2925
			NRPRGComp_CullNodes,
			// Token: 0x04000B6E RID: 2926
			NRPRGComp_TryMergeNativePasses,
			// Token: 0x04000B6F RID: 2927
			NRPRGComp_FindResourceUsageRanges,
			// Token: 0x04000B70 RID: 2928
			NRPRGComp_DetectMemorylessResources,
			// Token: 0x04000B71 RID: 2929
			NRPRGComp_ExecuteCreateResources,
			// Token: 0x04000B72 RID: 2930
			NRPRGComp_ExecuteBeginRenderpassCommand,
			// Token: 0x04000B73 RID: 2931
			NRPRGComp_ExecuteDestroyResources
		}
	}
}

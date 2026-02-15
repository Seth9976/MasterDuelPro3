using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.VFX;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A1 RID: 161
	public abstract class ScriptableRenderer : IDisposable
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		[Obsolete("cameraDepth has been renamed to cameraDepthTarget. (UnityUpgradable) -> cameraDepthTarget", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RenderTargetIdentifier cameraDepth
		{
			get
			{
				return this.m_CameraDepthTarget.nameID;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DB58 File Offset: 0x0000BD58
		internal void ResetNativeRenderPassFrameData()
		{
			if (this.m_MergeableRenderPassesMapArrays == null)
			{
				this.m_MergeableRenderPassesMapArrays = new int[10][];
			}
			for (int i = 0; i < 10; i++)
			{
				if (this.m_MergeableRenderPassesMapArrays[i] == null)
				{
					this.m_MergeableRenderPassesMapArrays[i] = new int[20];
				}
				for (int j = 0; j < 20; j++)
				{
					this.m_MergeableRenderPassesMapArrays[i][j] = -1;
				}
			}
			this.m_firstPassIndexOfLastMergeableGroup = 0;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal void SetupNativeRenderPassFrameData(UniversalCameraData cameraData, bool isRenderPassEnabled)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.setupFrameData))
			{
				int count = this.m_ActiveRenderPassQueue.Count;
				this.m_MergeableRenderPassesMap.Clear();
				this.m_RenderPassesAttachmentCount.Clear();
				uint currentHashIndex = 0U;
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					ScriptableRenderPass renderPass = this.m_ActiveRenderPassQueue[i];
					if (this.IsRenderPassEnabled(renderPass))
					{
						if (i >= 20)
						{
							Debug.LogError(string.Format("Exceeded the maximum number of Render Passes (${0}). Please consider using Render Graph to support a higher number of render passes with Native RenderPass, note support will be enabled by default.", 20));
							return;
						}
						renderPass.renderPassQueueIndex = i;
						ScriptableRenderer.RenderPassDescriptor rpDesc = this.InitializeRenderPassDescriptor(cameraData, renderPass);
						Hash128 hash = ScriptableRenderer.CreateRenderPassHash(rpDesc, currentHashIndex);
						this.m_PassIndexToPassHash[i] = hash;
						if (!this.m_MergeableRenderPassesMap.ContainsKey(hash))
						{
							this.m_MergeableRenderPassesMap.Add(hash, this.m_MergeableRenderPassesMapArrays[this.m_MergeableRenderPassesMap.Count]);
							this.m_RenderPassesAttachmentCount.Add(hash, 0);
							this.m_firstPassIndexOfLastMergeableGroup = i;
						}
						else if (this.m_MergeableRenderPassesMap[hash][ScriptableRenderer.GetValidPassIndexCount(this.m_MergeableRenderPassesMap[hash]) - 1] != i - 1)
						{
							currentHashIndex += 1U;
							hash = ScriptableRenderer.CreateRenderPassHash(rpDesc, currentHashIndex);
							this.m_PassIndexToPassHash[i] = hash;
							this.m_MergeableRenderPassesMap.Add(hash, this.m_MergeableRenderPassesMapArrays[this.m_MergeableRenderPassesMap.Count]);
							this.m_RenderPassesAttachmentCount.Add(hash, 0);
							this.m_firstPassIndexOfLastMergeableGroup = i;
						}
						this.m_MergeableRenderPassesMap[hash][ScriptableRenderer.GetValidPassIndexCount(this.m_MergeableRenderPassesMap[hash])] = i;
					}
				}
				for (int j = 0; j < this.m_ActiveRenderPassQueue.Count; j++)
				{
					this.m_ActiveRenderPassQueue[j].m_ColorAttachmentIndices = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
					this.m_ActiveRenderPassQueue[j].m_InputAttachmentIndices = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		internal void UpdateFinalStoreActions(int[] currentMergeablePasses, UniversalCameraData cameraData, bool isLastMergeableGroup)
		{
			for (int i = 0; i < this.m_FinalColorStoreAction.Length; i++)
			{
				this.m_FinalColorStoreAction[i] = RenderBufferStoreAction.Store;
			}
			this.m_FinalDepthStoreAction = RenderBufferStoreAction.Store;
			foreach (int passIdx in currentMergeablePasses)
			{
				if (!ScriptableRenderer.m_UseOptimizedStoreActions || passIdx == -1)
				{
					break;
				}
				ScriptableRenderPass pass = this.m_ActiveRenderPassQueue[passIdx];
				int samples = (pass.overrideCameraTarget ? ScriptableRenderer.GetFirstAllocatedRTHandle(pass).rt.descriptor.msaaSamples : ((cameraData.targetTexture != null) ? cameraData.targetTexture.descriptor.msaaSamples : cameraData.cameraTargetDescriptor.msaaSamples));
				bool rendererSupportsMSAA = cameraData.renderer != null && cameraData.renderer.supportedRenderingFeatures.msaa;
				if (!cameraData.camera.allowMSAA || !rendererSupportsMSAA)
				{
					samples = 1;
				}
				for (int j = 0; j < this.m_FinalColorStoreAction.Length; j++)
				{
					if (this.m_FinalColorStoreAction[j] == RenderBufferStoreAction.Store || this.m_FinalColorStoreAction[j] == RenderBufferStoreAction.StoreAndResolve || pass.overriddenColorStoreActions[j])
					{
						this.m_FinalColorStoreAction[j] = pass.colorStoreActions[j];
					}
					if (samples > 1)
					{
						if (this.m_FinalColorStoreAction[j] == RenderBufferStoreAction.Store)
						{
							this.m_FinalColorStoreAction[j] = RenderBufferStoreAction.StoreAndResolve;
						}
						else if (this.m_FinalColorStoreAction[j] == RenderBufferStoreAction.DontCare)
						{
							this.m_FinalColorStoreAction[j] = RenderBufferStoreAction.Resolve;
						}
						else if (isLastMergeableGroup && this.m_FinalColorStoreAction[j] == RenderBufferStoreAction.Resolve)
						{
							this.m_FinalColorStoreAction[j] = RenderBufferStoreAction.StoreAndResolve;
						}
					}
				}
				if (this.m_FinalDepthStoreAction == RenderBufferStoreAction.Store || (this.m_FinalDepthStoreAction == RenderBufferStoreAction.StoreAndResolve && pass.depthStoreAction == RenderBufferStoreAction.Resolve) || pass.overriddenDepthStoreAction)
				{
					this.m_FinalDepthStoreAction = pass.depthStoreAction;
				}
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000DF98 File Offset: 0x0000C198
		internal void SetNativeRenderPassMRTAttachmentList(ScriptableRenderPass renderPass, UniversalCameraData cameraData, bool needCustomCameraColorClear, ClearFlag cameraClearFlag)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.setMRTAttachmentsList))
			{
				int currentPassIndex = renderPass.renderPassQueueIndex;
				Hash128 currentPassHash = this.m_PassIndexToPassHash[currentPassIndex];
				int[] currentMergeablePasses = this.m_MergeableRenderPassesMap[currentPassHash];
				if (currentMergeablePasses.First<int>() == currentPassIndex)
				{
					this.m_RenderPassesAttachmentCount[currentPassHash] = 0;
					this.UpdateFinalStoreActions(currentMergeablePasses, cameraData, currentPassIndex == this.m_firstPassIndexOfLastMergeableGroup);
					int currentAttachmentIdx = 0;
					bool hasInput = false;
					foreach (int passIdx in currentMergeablePasses)
					{
						if (passIdx == -1)
						{
							break;
						}
						ScriptableRenderPass pass = this.m_ActiveRenderPassQueue[passIdx];
						for (int i = 0; i < pass.m_ColorAttachmentIndices.Length; i++)
						{
							pass.m_ColorAttachmentIndices[i] = -1;
						}
						for (int j = 0; j < pass.m_InputAttachmentIndices.Length; j++)
						{
							pass.m_InputAttachmentIndices[j] = -1;
						}
						uint validColorBuffersCount = RenderingUtils.GetValidColorBufferCount(pass.colorAttachmentHandles);
						int k = 0;
						while ((long)k < (long)((ulong)validColorBuffersCount))
						{
							AttachmentDescriptor currentAttachmentDescriptor = new AttachmentDescriptor((pass.renderTargetFormat[k] != GraphicsFormat.None) ? pass.renderTargetFormat[k] : UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(cameraData.isHdrEnabled, cameraData.hdrColorBufferPrecision, Graphics.preserveFramebufferAlpha));
							RTHandle colorHandle = (pass.overrideCameraTarget ? pass.colorAttachmentHandles[k] : this.m_CameraColorTarget);
							int existingAttachmentIndex = ScriptableRenderer.FindAttachmentDescriptorIndexInList(colorHandle.nameID, this.m_ActiveColorAttachmentDescriptors);
							if (ScriptableRenderer.m_UseOptimizedStoreActions)
							{
								currentAttachmentDescriptor.storeAction = this.m_FinalColorStoreAction[k];
							}
							if (existingAttachmentIndex == -1)
							{
								this.m_ActiveColorAttachmentDescriptors[currentAttachmentIdx] = currentAttachmentDescriptor;
								bool passHasClearColor = (pass.clearFlag & ClearFlag.Color) > ClearFlag.None;
								this.m_ActiveColorAttachmentDescriptors[currentAttachmentIdx].ConfigureTarget(colorHandle.nameID, !passHasClearColor, true);
								if (pass.colorAttachmentHandles[k].nameID == this.m_CameraColorTarget.nameID && needCustomCameraColorClear && (cameraClearFlag & ClearFlag.Color) != ClearFlag.None)
								{
									this.m_ActiveColorAttachmentDescriptors[currentAttachmentIdx].ConfigureClear(cameraData.backgroundColor, 1f, 0U);
								}
								else if (passHasClearColor)
								{
									this.m_ActiveColorAttachmentDescriptors[currentAttachmentIdx].ConfigureClear(CoreUtils.ConvertSRGBToActiveColorSpace(pass.clearColor), 1f, 0U);
								}
								pass.m_ColorAttachmentIndices[k] = currentAttachmentIdx;
								currentAttachmentIdx++;
								Dictionary<Hash128, int> renderPassesAttachmentCount = this.m_RenderPassesAttachmentCount;
								Hash128 hash = currentPassHash;
								int num = renderPassesAttachmentCount[hash];
								renderPassesAttachmentCount[hash] = num + 1;
							}
							else
							{
								pass.m_ColorAttachmentIndices[k] = existingAttachmentIndex;
							}
							k++;
						}
						if (ScriptableRenderer.PassHasInputAttachments(pass))
						{
							hasInput = true;
							this.SetupInputAttachmentIndices(pass);
						}
						this.m_ActiveDepthAttachmentDescriptor = new AttachmentDescriptor(SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil));
						bool passHasClearDepth = (cameraClearFlag & ClearFlag.DepthStencil) > ClearFlag.None;
						this.m_ActiveDepthAttachmentDescriptor.ConfigureTarget(pass.overrideCameraTarget ? pass.depthAttachmentHandle.nameID : this.m_CameraDepthTarget.nameID, !passHasClearDepth, true);
						if (passHasClearDepth)
						{
							this.m_ActiveDepthAttachmentDescriptor.ConfigureClear(Color.black, 1f, 0U);
						}
						if (ScriptableRenderer.m_UseOptimizedStoreActions)
						{
							this.m_ActiveDepthAttachmentDescriptor.storeAction = this.m_FinalDepthStoreAction;
						}
					}
					if (hasInput)
					{
						this.SetupTransientInputAttachments(this.m_RenderPassesAttachmentCount[currentPassHash]);
					}
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E308 File Offset: 0x0000C508
		private bool IsDepthOnlyRenderTexture(RenderTexture t)
		{
			return t.graphicsFormat == GraphicsFormat.None;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E318 File Offset: 0x0000C518
		internal void SetNativeRenderPassAttachmentList(ScriptableRenderPass renderPass, UniversalCameraData cameraData, RTHandle passColorAttachment, RTHandle passDepthAttachment, ClearFlag finalClearFlag, Color finalClearColor)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.setAttachmentList))
			{
				int currentPassIndex = renderPass.renderPassQueueIndex;
				Hash128 currentPassHash = this.m_PassIndexToPassHash[currentPassIndex];
				int[] currentMergeablePasses = this.m_MergeableRenderPassesMap[currentPassHash];
				if (currentMergeablePasses.First<int>() == currentPassIndex)
				{
					this.m_RenderPassesAttachmentCount[currentPassHash] = 0;
					this.UpdateFinalStoreActions(currentMergeablePasses, cameraData, currentPassIndex == this.m_firstPassIndexOfLastMergeableGroup);
					int currentAttachmentIdx = 0;
					foreach (int passIdx in currentMergeablePasses)
					{
						if (passIdx == -1)
						{
							break;
						}
						ScriptableRenderPass pass = this.m_ActiveRenderPassQueue[passIdx];
						for (int i = 0; i < pass.m_ColorAttachmentIndices.Length; i++)
						{
							pass.m_ColorAttachmentIndices[i] = -1;
						}
						bool usesTargetTexture = cameraData.targetTexture != null;
						bool depthOnly = (pass.colorAttachmentHandle.rt != null && this.IsDepthOnlyRenderTexture(pass.colorAttachmentHandle.rt)) || (usesTargetTexture && this.IsDepthOnlyRenderTexture(cameraData.targetTexture));
						AttachmentDescriptor currentAttachmentDescriptor;
						int samples;
						RenderTargetIdentifier colorAttachmentTarget;
						if (new RenderTargetIdentifier(passColorAttachment.nameID, 0, CubemapFace.Unknown, 0) != BuiltinRenderTextureType.CameraTarget)
						{
							currentAttachmentDescriptor = new AttachmentDescriptor(depthOnly ? passColorAttachment.rt.descriptor.depthStencilFormat : passColorAttachment.rt.descriptor.graphicsFormat);
							samples = passColorAttachment.rt.descriptor.msaaSamples;
							colorAttachmentTarget = passColorAttachment.nameID;
						}
						else
						{
							currentAttachmentDescriptor = new AttachmentDescriptor((pass.renderTargetFormat[0] != GraphicsFormat.None) ? pass.renderTargetFormat[0] : UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(cameraData.isHdrEnabled, cameraData.hdrColorBufferPrecision, Graphics.preserveFramebufferAlpha));
							samples = cameraData.cameraTargetDescriptor.msaaSamples;
							colorAttachmentTarget = (usesTargetTexture ? new RenderTargetIdentifier(cameraData.targetTexture) : BuiltinRenderTextureType.CameraTarget);
						}
						currentAttachmentDescriptor.ConfigureTarget(colorAttachmentTarget, (finalClearFlag & ClearFlag.Color) == ClearFlag.None, true);
						if (ScriptableRenderer.PassHasInputAttachments(pass))
						{
							this.SetupInputAttachmentIndices(pass);
						}
						this.m_ActiveDepthAttachmentDescriptor = new AttachmentDescriptor(SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil));
						this.m_ActiveDepthAttachmentDescriptor.ConfigureTarget((passDepthAttachment.nameID != BuiltinRenderTextureType.CameraTarget) ? passDepthAttachment.nameID : (usesTargetTexture ? new RenderTargetIdentifier(cameraData.targetTexture.depthBuffer, 0, CubemapFace.Unknown, 0) : BuiltinRenderTextureType.Depth), (finalClearFlag & ClearFlag.Depth) == ClearFlag.None, true);
						if (finalClearFlag != ClearFlag.None)
						{
							if (cameraData.renderType != CameraRenderType.Overlay || (depthOnly && (finalClearFlag & ClearFlag.Color) != ClearFlag.None))
							{
								currentAttachmentDescriptor.ConfigureClear(finalClearColor, 1f, 0U);
							}
							if ((finalClearFlag & ClearFlag.Depth) != ClearFlag.None)
							{
								this.m_ActiveDepthAttachmentDescriptor.ConfigureClear(Color.black, 1f, 0U);
							}
						}
						if (samples > 1)
						{
							currentAttachmentDescriptor.ConfigureResolveTarget(colorAttachmentTarget);
							if (RenderingUtils.MultisampleDepthResolveSupported())
							{
								this.m_ActiveDepthAttachmentDescriptor.ConfigureResolveTarget(this.m_ActiveDepthAttachmentDescriptor.loadStoreTarget);
							}
						}
						if (ScriptableRenderer.m_UseOptimizedStoreActions)
						{
							currentAttachmentDescriptor.storeAction = this.m_FinalColorStoreAction[0];
							this.m_ActiveDepthAttachmentDescriptor.storeAction = this.m_FinalDepthStoreAction;
						}
						int existingAttachmentIndex = ScriptableRenderer.FindAttachmentDescriptorIndexInList(currentAttachmentIdx, currentAttachmentDescriptor, this.m_ActiveColorAttachmentDescriptors);
						if (existingAttachmentIndex == -1)
						{
							pass.m_ColorAttachmentIndices[0] = currentAttachmentIdx;
							this.m_ActiveColorAttachmentDescriptors[currentAttachmentIdx] = currentAttachmentDescriptor;
							currentAttachmentIdx++;
							Dictionary<Hash128, int> renderPassesAttachmentCount = this.m_RenderPassesAttachmentCount;
							Hash128 hash = currentPassHash;
							int num = renderPassesAttachmentCount[hash];
							renderPassesAttachmentCount[hash] = num + 1;
						}
						else
						{
							pass.m_ColorAttachmentIndices[0] = existingAttachmentIndex;
						}
					}
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		internal unsafe void ExecuteNativeRenderPass(ScriptableRenderContext context, ScriptableRenderPass renderPass, UniversalCameraData cameraData, ref RenderingData renderingData)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.execute))
			{
				int currentPassIndex = renderPass.renderPassQueueIndex;
				Hash128 currentPassHash = this.m_PassIndexToPassHash[currentPassIndex];
				int[] currentMergeablePasses = this.m_MergeableRenderPassesMap[currentPassHash];
				int validColorBuffersCount = this.m_RenderPassesAttachmentCount[currentPassHash];
				bool depthOnly = (renderPass.colorAttachmentHandle.rt != null && this.IsDepthOnlyRenderTexture(renderPass.colorAttachmentHandle.rt)) || (cameraData.targetTexture != null && this.IsDepthOnlyRenderTexture(cameraData.targetTexture));
				bool useDepth = depthOnly || !renderPass.overrideCameraTarget || (renderPass.overrideCameraTarget && renderPass.depthAttachmentHandle.nameID != BuiltinRenderTextureType.CameraTarget);
				NativeArray<AttachmentDescriptor> attachments = new NativeArray<AttachmentDescriptor>((useDepth && !depthOnly) ? (validColorBuffersCount + 1) : 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < validColorBuffersCount; i++)
				{
					attachments[i] = this.m_ActiveColorAttachmentDescriptors[i];
				}
				if (useDepth && !depthOnly)
				{
					attachments[validColorBuffersCount] = this.m_ActiveDepthAttachmentDescriptor;
				}
				ScriptableRenderer.RenderPassDescriptor rpDesc = this.InitializeRenderPassDescriptor(cameraData, renderPass);
				int validPassCount = ScriptableRenderer.GetValidPassIndexCount(currentMergeablePasses);
				uint attachmentIndicesCount = ScriptableRenderer.GetSubPassAttachmentIndicesCount(renderPass);
				NativeArray<int> attachmentIndices = new NativeArray<int>((int)((!depthOnly) ? attachmentIndicesCount : 0U), Allocator.Temp, NativeArrayOptions.ClearMemory);
				if (!depthOnly)
				{
					int j = 0;
					while ((long)j < (long)((ulong)attachmentIndicesCount))
					{
						attachmentIndices[j] = renderPass.m_ColorAttachmentIndices[j];
						j++;
					}
				}
				if (validPassCount == 1 || currentMergeablePasses[0] == currentPassIndex)
				{
					if (ScriptableRenderer.PassHasInputAttachments(renderPass))
					{
						Debug.LogWarning("First pass in a RenderPass should not have input attachments.");
					}
					context.BeginRenderPass(rpDesc.w, rpDesc.h, Math.Max(rpDesc.samples, 1), attachments, useDepth ? ((!depthOnly) ? validColorBuffersCount : 0) : (-1));
					attachments.Dispose();
					context.BeginSubPass(attachmentIndices, false);
					this.m_LastBeginSubpassPassIndex = currentPassIndex;
				}
				else if (!ScriptableRenderer.AreAttachmentIndicesCompatible(this.m_ActiveRenderPassQueue[this.m_LastBeginSubpassPassIndex], this.m_ActiveRenderPassQueue[currentPassIndex]))
				{
					context.EndSubPass();
					if (ScriptableRenderer.PassHasInputAttachments(this.m_ActiveRenderPassQueue[currentPassIndex]))
					{
						context.BeginSubPass(attachmentIndices, this.m_ActiveRenderPassQueue[currentPassIndex].m_InputAttachmentIndices, false);
					}
					else
					{
						context.BeginSubPass(attachmentIndices, false);
					}
					this.m_LastBeginSubpassPassIndex = currentPassIndex;
				}
				else if (ScriptableRenderer.PassHasInputAttachments(this.m_ActiveRenderPassQueue[currentPassIndex]))
				{
					context.EndSubPass();
					context.BeginSubPass(attachmentIndices, this.m_ActiveRenderPassQueue[currentPassIndex].m_InputAttachmentIndices, false);
					this.m_LastBeginSubpassPassIndex = currentPassIndex;
				}
				attachmentIndices.Dispose();
				renderPass.Execute(context, ref renderingData);
				context.ExecuteCommandBuffer(*renderingData.commandBuffer);
				renderingData.commandBuffer->Clear();
				if (validPassCount == 1 || currentMergeablePasses[validPassCount - 1] == currentPassIndex)
				{
					context.EndSubPass();
					context.EndRenderPass();
					this.m_LastBeginSubpassPassIndex = 0;
				}
				for (int k = 0; k < this.m_ActiveColorAttachmentDescriptors.Length; k++)
				{
					this.m_ActiveColorAttachmentDescriptors[k] = RenderingUtils.emptyAttachment;
					this.m_IsActiveColorAttachmentTransient[k] = false;
				}
				this.m_ActiveDepthAttachmentDescriptor = RenderingUtils.emptyAttachment;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		internal void SetupInputAttachmentIndices(ScriptableRenderPass pass)
		{
			int validInputBufferCount = ScriptableRenderer.GetValidInputAttachmentCount(pass);
			pass.m_InputAttachmentIndices = new NativeArray<int>(validInputBufferCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < validInputBufferCount; i++)
			{
				pass.m_InputAttachmentIndices[i] = ScriptableRenderer.FindAttachmentDescriptorIndexInList(pass.m_InputAttachments[i], this.m_ActiveColorAttachmentDescriptors);
				if (pass.m_InputAttachmentIndices[i] == -1)
				{
					Debug.LogWarning("RenderPass Input attachment not found in the current RenderPass");
				}
				else if (!this.m_IsActiveColorAttachmentTransient[pass.m_InputAttachmentIndices[i]])
				{
					this.m_IsActiveColorAttachmentTransient[pass.m_InputAttachmentIndices[i]] = pass.IsInputAttachmentTransient(i);
				}
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000EA90 File Offset: 0x0000CC90
		internal void SetupTransientInputAttachments(int attachmentCount)
		{
			for (int i = 0; i < attachmentCount; i++)
			{
				if (this.m_IsActiveColorAttachmentTransient[i])
				{
					this.m_ActiveColorAttachmentDescriptors[i].loadAction = RenderBufferLoadAction.DontCare;
					this.m_ActiveColorAttachmentDescriptors[i].storeAction = RenderBufferStoreAction.DontCare;
					this.m_ActiveColorAttachmentDescriptors[i].loadStoreTarget = BuiltinRenderTextureType.None;
				}
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		internal static uint GetSubPassAttachmentIndicesCount(ScriptableRenderPass pass)
		{
			uint numValidAttachments = 0U;
			using (NativeArray<int>.Enumerator enumerator = pass.m_ColorAttachmentIndices.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current >= 0)
					{
						numValidAttachments += 1U;
					}
				}
			}
			return numValidAttachments;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000EB48 File Offset: 0x0000CD48
		internal static bool AreAttachmentIndicesCompatible(ScriptableRenderPass lastSubPass, ScriptableRenderPass currentSubPass)
		{
			uint lastSubPassAttCount = ScriptableRenderer.GetSubPassAttachmentIndicesCount(lastSubPass);
			uint currentSubPassAttCount = ScriptableRenderer.GetSubPassAttachmentIndicesCount(currentSubPass);
			if (currentSubPassAttCount != lastSubPassAttCount)
			{
				return false;
			}
			uint numEqualAttachments = 0U;
			int currPassIdx = 0;
			while ((long)currPassIdx < (long)((ulong)currentSubPassAttCount))
			{
				int lastPassIdx = 0;
				while ((long)lastPassIdx < (long)((ulong)lastSubPassAttCount))
				{
					if (currentSubPass.m_ColorAttachmentIndices[currPassIdx] == lastSubPass.m_ColorAttachmentIndices[lastPassIdx])
					{
						numEqualAttachments += 1U;
					}
					lastPassIdx++;
				}
				currPassIdx++;
			}
			return numEqualAttachments == currentSubPassAttCount;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		internal static uint GetValidColorAttachmentCount(AttachmentDescriptor[] colorAttachments)
		{
			uint nonNullColorBuffers = 0U;
			if (colorAttachments != null)
			{
				for (int i = 0; i < colorAttachments.Length; i++)
				{
					if (colorAttachments[i] != RenderingUtils.emptyAttachment)
					{
						nonNullColorBuffers += 1U;
					}
				}
			}
			return nonNullColorBuffers;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		internal static int GetValidInputAttachmentCount(ScriptableRenderPass renderPass)
		{
			int length = renderPass.m_InputAttachments.Length;
			if (length != 8)
			{
				return length;
			}
			for (int i = 0; i < length; i++)
			{
				if (renderPass.m_InputAttachments[i] == null)
				{
					return i;
				}
			}
			return length;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EC24 File Offset: 0x0000CE24
		internal static int FindAttachmentDescriptorIndexInList(int attachmentIdx, AttachmentDescriptor attachmentDescriptor, AttachmentDescriptor[] attachmentDescriptors)
		{
			int existingAttachmentIndex = -1;
			for (int i = 0; i <= attachmentIdx; i++)
			{
				AttachmentDescriptor att = attachmentDescriptors[i];
				if (att.loadStoreTarget == attachmentDescriptor.loadStoreTarget && att.graphicsFormat == attachmentDescriptor.graphicsFormat)
				{
					existingAttachmentIndex = i;
					break;
				}
			}
			return existingAttachmentIndex;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000EC74 File Offset: 0x0000CE74
		internal static int FindAttachmentDescriptorIndexInList(RenderTargetIdentifier target, AttachmentDescriptor[] attachmentDescriptors)
		{
			for (int i = 0; i < attachmentDescriptors.Length; i++)
			{
				AttachmentDescriptor att = attachmentDescriptors[i];
				if (att.loadStoreTarget == target)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		internal static int GetValidPassIndexCount(int[] array)
		{
			if (array == null)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == -1)
				{
					return i;
				}
			}
			return array.Length - 1;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		internal static RTHandle GetFirstAllocatedRTHandle(ScriptableRenderPass pass)
		{
			for (int i = 0; i < pass.colorAttachmentHandles.Length; i++)
			{
				if (pass.colorAttachmentHandles[i].rt != null)
				{
					return pass.colorAttachmentHandles[i];
				}
			}
			return pass.colorAttachmentHandles[0];
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000ED22 File Offset: 0x0000CF22
		internal static bool PassHasInputAttachments(ScriptableRenderPass renderPass)
		{
			return renderPass.m_InputAttachments.Length != 8 || renderPass.m_InputAttachments[0] != null;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		internal static Hash128 CreateRenderPassHash(int width, int height, int depthID, int sample, uint hashIndex)
		{
			return new Hash128((uint)((width << 4) + height), (uint)depthID, (uint)sample, hashIndex);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		internal static Hash128 CreateRenderPassHash(ScriptableRenderer.RenderPassDescriptor desc, uint hashIndex)
		{
			return ScriptableRenderer.CreateRenderPassHash(desc.w, desc.h, desc.depthID, desc.samples, hashIndex);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		internal static void GetRenderTextureDescriptor(UniversalCameraData cameraData, ScriptableRenderPass renderPass, out RenderTextureDescriptor targetRT)
		{
			if (!renderPass.overrideCameraTarget || (renderPass.colorAttachmentHandle.rt == null && renderPass.depthAttachmentHandle.rt == null))
			{
				targetRT = cameraData.cameraTargetDescriptor;
				if (cameraData.targetTexture != null)
				{
					targetRT.width = cameraData.scaledWidth;
					targetRT.height = cameraData.scaledHeight;
					return;
				}
			}
			else
			{
				RTHandle handle = ScriptableRenderer.GetFirstAllocatedRTHandle(renderPass);
				targetRT = ((handle.rt != null) ? handle.rt.descriptor : renderPass.depthAttachmentHandle.rt.descriptor);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000EE14 File Offset: 0x0000D014
		private ScriptableRenderer.RenderPassDescriptor InitializeRenderPassDescriptor(UniversalCameraData cameraData, ScriptableRenderPass renderPass)
		{
			RenderTextureDescriptor targetRT;
			ScriptableRenderer.GetRenderTextureDescriptor(cameraData, renderPass, out targetRT);
			RTHandle depthTarget = (renderPass.overrideCameraTarget ? renderPass.depthAttachmentHandle : this.cameraDepthTargetHandle);
			int depthID = ((targetRT.graphicsFormat == GraphicsFormat.None && targetRT.depthStencilFormat != GraphicsFormat.None) ? renderPass.colorAttachmentHandle.GetHashCode() : depthTarget.GetHashCode());
			return new ScriptableRenderer.RenderPassDescriptor(targetRT.width, targetRT.height, targetRT.msaaSamples, depthID);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00002886 File Offset: 0x00000A86
		public virtual int SupportedCameraStackingTypes()
		{
			return 0;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000EE82 File Offset: 0x0000D082
		public bool SupportsCameraStackingType(CameraRenderType cameraRenderType)
		{
			return (this.SupportedCameraStackingTypes() & (1 << (int)cameraRenderType)) != 0;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00002886 File Offset: 0x00000A86
		protected internal virtual bool SupportsMotionVectors()
		{
			return false;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000EE94 File Offset: 0x0000D094
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000EE9C File Offset: 0x0000D09C
		protected ProfilingSampler profilingExecute { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
		internal DebugHandler DebugHandler { get; }

		// Token: 0x060003DD RID: 989 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		public static void SetCameraMatrices(CommandBuffer cmd, ref CameraData cameraData, bool setInverseMatrices)
		{
			ScriptableRenderer.SetCameraMatrices(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData.universalCameraData, setInverseMatrices, cameraData.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		public static void SetCameraMatrices(CommandBuffer cmd, UniversalCameraData cameraData, bool setInverseMatrices)
		{
			ScriptableRenderer.SetCameraMatrices(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData, setInverseMatrices, cameraData.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		internal static void SetCameraMatrices(RasterCommandBuffer cmd, UniversalCameraData cameraData, bool setInverseMatrices, bool isTargetFlipped)
		{
			if (cameraData.xr.enabled)
			{
				cameraData.PushBuiltinShaderConstantsXR(cmd, isTargetFlipped);
				XRSystemUniversal.MarkShaderProperties(cmd, cameraData.xrUniversal, isTargetFlipped);
				return;
			}
			Matrix4x4 viewMatrix = cameraData.GetViewMatrix(0);
			Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(0);
			cmd.SetViewProjectionMatrices(viewMatrix, projectionMatrix);
			if (setInverseMatrices)
			{
				Matrix4x4 gpuprojectionMatrix = cameraData.GetGPUProjectionMatrix(isTargetFlipped, 0);
				Matrix4x4 inverseViewMatrix = Matrix4x4.Inverse(viewMatrix);
				Matrix4x4 inverseProjectionMatrix = Matrix4x4.Inverse(gpuprojectionMatrix);
				Matrix4x4 inverseViewProjection = inverseViewMatrix * inverseProjectionMatrix;
				Matrix4x4 worldToCameraMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * viewMatrix;
				Matrix4x4 cameraToWorldMatrix = worldToCameraMatrix.inverse;
				cmd.SetGlobalMatrix(ShaderPropertyId.worldToCameraMatrix, worldToCameraMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.cameraToWorldMatrix, cameraToWorldMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewMatrix, inverseViewMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseProjectionMatrix, inverseProjectionMatrix);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewAndProjectionMatrix, inverseViewProjection);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		private void SetPerCameraShaderVariables(RasterCommandBuffer cmd, UniversalCameraData cameraData)
		{
			this.SetPerCameraShaderVariables(cmd, cameraData, new Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height), cameraData.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		private void SetPerCameraShaderVariables(RasterCommandBuffer cmd, UniversalCameraData cameraData, Vector2Int cameraTargetSizeCopy, bool isTargetFlipped)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.setPerCameraShaderVariables))
			{
				Camera camera = cameraData.camera;
				float scaledCameraTargetWidth = (float)cameraTargetSizeCopy.x;
				float scaledCameraTargetHeight = (float)cameraTargetSizeCopy.y;
				float cameraWidth = (float)camera.pixelWidth;
				float cameraHeight = (float)camera.pixelHeight;
				if (cameraData.renderType == CameraRenderType.Overlay)
				{
					cameraWidth = (float)cameraData.pixelWidth;
					cameraHeight = (float)cameraData.pixelHeight;
				}
				if (cameraData.xr.enabled)
				{
					cameraWidth = (float)cameraTargetSizeCopy.x;
					cameraHeight = (float)cameraTargetSizeCopy.y;
					this.useRenderPassEnabled = false;
				}
				if (camera.allowDynamicResolution)
				{
					scaledCameraTargetWidth *= ScalableBufferManager.widthScaleFactor;
					scaledCameraTargetHeight *= ScalableBufferManager.heightScaleFactor;
				}
				float near = camera.nearClipPlane;
				float far = camera.farClipPlane;
				float invNear = (Mathf.Approximately(near, 0f) ? 0f : (1f / near));
				float invFar = (Mathf.Approximately(far, 0f) ? 0f : (1f / far));
				float isOrthographic = (camera.orthographic ? 1f : 0f);
				float zc0 = 1f - far * invNear;
				float zc = far * invNear;
				Vector4 zBufferParams = new Vector4(zc0, zc, zc0 * invFar, zc * invFar);
				if (SystemInfo.usesReversedZBuffer)
				{
					zBufferParams.y += zBufferParams.x;
					zBufferParams.x = -zBufferParams.x;
					zBufferParams.w += zBufferParams.z;
					zBufferParams.z = -zBufferParams.z;
				}
				float projectionFlipSign = (isTargetFlipped ? (-1f) : 1f);
				Vector4 projectionParams = new Vector4(projectionFlipSign, near, far, 1f * invFar);
				cmd.SetGlobalVector(ShaderPropertyId.projectionParams, projectionParams);
				Vector4 orthoParams = new Vector4(camera.orthographicSize * cameraData.aspectRatio, camera.orthographicSize, 0f, isOrthographic);
				cmd.SetGlobalVector(ShaderPropertyId.worldSpaceCameraPos, cameraData.worldSpaceCameraPos);
				cmd.SetGlobalVector(ShaderPropertyId.screenParams, new Vector4(cameraWidth, cameraHeight, 1f + 1f / cameraWidth, 1f + 1f / cameraHeight));
				cmd.SetGlobalVector(ShaderPropertyId.scaledScreenParams, new Vector4(scaledCameraTargetWidth, scaledCameraTargetHeight, 1f + 1f / scaledCameraTargetWidth, 1f + 1f / scaledCameraTargetHeight));
				cmd.SetGlobalVector(ShaderPropertyId.zBufferParams, zBufferParams);
				cmd.SetGlobalVector(ShaderPropertyId.orthoParams, orthoParams);
				cmd.SetGlobalVector(ShaderPropertyId.screenSize, new Vector4(scaledCameraTargetWidth, scaledCameraTargetHeight, 1f / scaledCameraTargetWidth, 1f / scaledCameraTargetHeight));
				cmd.SetKeyword(in ShaderGlobalKeywords.SCREEN_COORD_OVERRIDE, cameraData.useScreenCoordOverride);
				cmd.SetGlobalVector(ShaderPropertyId.screenSizeOverride, cameraData.screenSizeOverride);
				cmd.SetGlobalVector(ShaderPropertyId.screenCoordScaleBias, cameraData.screenCoordScaleBias);
				cmd.SetGlobalVector(ShaderPropertyId.rtHandleScale, Vector4.one);
				float mipBias = Math.Min((float)(-(float)Math.Log((double)(cameraWidth / scaledCameraTargetWidth), 2.0)), 0f);
				float taaMipBias = Math.Min(cameraData.taaSettings.mipBias, 0f);
				mipBias = Math.Min(mipBias, taaMipBias);
				cmd.SetGlobalVector(ShaderPropertyId.globalMipBias, new Vector2(mipBias, Mathf.Pow(2f, mipBias)));
				ScriptableRenderer.SetCameraMatrices(cmd, cameraData, true, isTargetFlipped);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000F334 File Offset: 0x0000D534
		private void SetPerCameraBillboardProperties(RasterCommandBuffer cmd, UniversalCameraData cameraData)
		{
			Matrix4x4 worldToCameraMatrix = cameraData.GetViewMatrix(0);
			Vector3 cameraPos = cameraData.worldSpaceCameraPos;
			cmd.SetKeyword(in ShaderGlobalKeywords.BillboardFaceCameraPos, QualitySettings.billboardsFaceCameraPosition);
			Vector3 billboardTangent;
			Vector3 billboardNormal;
			float cameraXZAngle;
			ScriptableRenderer.CalculateBillboardProperties(in worldToCameraMatrix, out billboardTangent, out billboardNormal, out cameraXZAngle);
			cmd.SetGlobalVector(ShaderPropertyId.billboardNormal, new Vector4(billboardNormal.x, billboardNormal.y, billboardNormal.z, 0f));
			cmd.SetGlobalVector(ShaderPropertyId.billboardTangent, new Vector4(billboardTangent.x, billboardTangent.y, billboardTangent.z, 0f));
			cmd.SetGlobalVector(ShaderPropertyId.billboardCameraParams, new Vector4(cameraPos.x, cameraPos.y, cameraPos.z, cameraXZAngle));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		private static void CalculateBillboardProperties(in Matrix4x4 worldToCameraMatrix, out Vector3 billboardTangent, out Vector3 billboardNormal, out float cameraXZAngle)
		{
			Matrix4x4 cameraToWorldMatrix = worldToCameraMatrix;
			cameraToWorldMatrix = cameraToWorldMatrix.transpose;
			Vector3 cameraToWorldMatrixAxisX = new Vector3(cameraToWorldMatrix.m00, cameraToWorldMatrix.m10, cameraToWorldMatrix.m20);
			Vector3 cameraToWorldMatrixAxisY = new Vector3(cameraToWorldMatrix.m01, cameraToWorldMatrix.m11, cameraToWorldMatrix.m21);
			Vector3 vector = new Vector3(cameraToWorldMatrix.m02, cameraToWorldMatrix.m12, cameraToWorldMatrix.m22);
			Vector3 worldUp = Vector3.up;
			Vector3 cross = Vector3.Cross(vector, worldUp);
			billboardTangent = ((!Mathf.Approximately(cross.sqrMagnitude, 0f)) ? cross.normalized : cameraToWorldMatrixAxisX);
			billboardNormal = Vector3.Cross(worldUp, billboardTangent);
			billboardNormal = ((!Mathf.Approximately(billboardNormal.sqrMagnitude, 0f)) ? billboardNormal.normalized : cameraToWorldMatrixAxisY);
			Vector3 worldRight = new Vector3(0f, 0f, 1f);
			float s = worldRight.x * billboardTangent.z - worldRight.z * billboardTangent.x;
			float c = worldRight.x * billboardTangent.x + worldRight.z * billboardTangent.z;
			cameraXZAngle = Mathf.Atan2(s, c);
			if (cameraXZAngle < 0f)
			{
				cameraXZAngle += 6.2831855f;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F51B File Offset: 0x0000D71B
		private void SetPerCameraClippingPlaneProperties(RasterCommandBuffer cmd, UniversalCameraData cameraData)
		{
			this.SetPerCameraClippingPlaneProperties(cmd, in cameraData, cameraData.IsCameraProjectionMatrixFlipped());
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F52C File Offset: 0x0000D72C
		private void SetPerCameraClippingPlaneProperties(RasterCommandBuffer cmd, in UniversalCameraData cameraData, bool isTargetFlipped)
		{
			Matrix4x4 gpuprojectionMatrix = cameraData.GetGPUProjectionMatrix(isTargetFlipped, 0);
			Matrix4x4 viewMatrix = cameraData.GetViewMatrix(0);
			Matrix4x4 matrix4x = CoreMatrixUtils.MultiplyProjectionMatrix(gpuprojectionMatrix, viewMatrix, cameraData.camera.orthographic);
			Plane[] planes = ScriptableRenderer.s_Planes;
			GeometryUtility.CalculateFrustumPlanes(matrix4x, planes);
			Vector4[] cameraWorldClipPlanes = ScriptableRenderer.s_VectorPlanes;
			for (int i = 0; i < planes.Length; i++)
			{
				cameraWorldClipPlanes[i] = new Vector4(planes[i].normal.x, planes[i].normal.y, planes[i].normal.z, planes[i].distance);
			}
			cmd.SetGlobalVectorArray(ShaderPropertyId.cameraWorldClipPlanes, cameraWorldClipPlanes);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		private static void SetShaderTimeValues(IBaseCommandBuffer cmd, float time, float deltaTime, float smoothDeltaTime)
		{
			float timeEights = time / 8f;
			float timeFourth = time / 4f;
			float timeHalf = time / 2f;
			float lastTime = time - ShaderUtils.PersistentDeltaTime;
			Vector4 timeVector = time * new Vector4(0.05f, 1f, 2f, 3f);
			Vector4 sinTimeVector = new Vector4(Mathf.Sin(timeEights), Mathf.Sin(timeFourth), Mathf.Sin(timeHalf), Mathf.Sin(time));
			Vector4 cosTimeVector = new Vector4(Mathf.Cos(timeEights), Mathf.Cos(timeFourth), Mathf.Cos(timeHalf), Mathf.Cos(time));
			Vector4 deltaTimeVector = new Vector4(deltaTime, 1f / deltaTime, smoothDeltaTime, 1f / smoothDeltaTime);
			Vector4 timeParametersVector = new Vector4(time, Mathf.Sin(time), Mathf.Cos(time), 0f);
			Vector4 lastTimeParametersVector = new Vector4(lastTime, Mathf.Sin(lastTime), Mathf.Cos(lastTime), 0f);
			cmd.SetGlobalVector(ShaderPropertyId.time, timeVector);
			cmd.SetGlobalVector(ShaderPropertyId.sinTime, sinTimeVector);
			cmd.SetGlobalVector(ShaderPropertyId.cosTime, cosTimeVector);
			cmd.SetGlobalVector(ShaderPropertyId.deltaTime, deltaTimeVector);
			cmd.SetGlobalVector(ShaderPropertyId.timeParameters, timeParametersVector);
			cmd.SetGlobalVector(ShaderPropertyId.lastTimeParameters, lastTimeParametersVector);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		[Obsolete("Use cameraColorTargetHandle", true)]
		public RenderTargetIdentifier cameraColorTarget
		{
			get
			{
				throw new NotSupportedException("cameraColorTarget has been deprecated. Use cameraColorTargetHandle instead");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000F70F File Offset: 0x0000D90F
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public RTHandle cameraColorTargetHandle
		{
			get
			{
				if (!this.m_IsPipelineExecuting)
				{
					Debug.LogError("You can only call cameraColorTargetHandle inside the scope of a ScriptableRenderPass. Otherwise the pipeline camera target texture might have not been created or might have already been disposed.");
					return null;
				}
				return this.m_CameraColorTarget;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000278C File Offset: 0x0000098C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal virtual RTHandle GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			return null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000278C File Offset: 0x0000098C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal virtual RTHandle GetCameraColorBackBuffer(CommandBuffer cmd)
		{
			return null;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000F72C File Offset: 0x0000D92C
		[Obsolete("Use cameraDepthTargetHandle", true)]
		public RenderTargetIdentifier cameraDepthTarget
		{
			get
			{
				throw new NotSupportedException("cameraDepthTarget has been deprecated. Use cameraDepthTargetHandle instead");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000F743 File Offset: 0x0000D943
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public RTHandle cameraDepthTargetHandle
		{
			get
			{
				if (!this.m_IsPipelineExecuting)
				{
					Debug.LogError("You can only call cameraDepthTargetHandle inside the scope of a ScriptableRenderPass. Otherwise the pipeline camera target texture might have not been created or might have already been disposed.");
					return null;
				}
				return this.m_CameraDepthTarget;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F75F File Offset: 0x0000D95F
		protected List<ScriptableRendererFeature> rendererFeatures
		{
			get
			{
				return this.m_RendererFeatures;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000F767 File Offset: 0x0000D967
		protected List<ScriptableRenderPass> activeRenderPassQueue
		{
			get
			{
				return this.m_ActiveRenderPassQueue;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F76F File Offset: 0x0000D96F
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000F777 File Offset: 0x0000D977
		public ScriptableRenderer.RenderingFeatures supportedRenderingFeatures { get; set; } = new ScriptableRenderer.RenderingFeatures();

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000F780 File Offset: 0x0000D980
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000F788 File Offset: 0x0000D988
		public GraphicsDeviceType[] unsupportedGraphicsDeviceTypes { get; set; } = new GraphicsDeviceType[0];

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000F791 File Offset: 0x0000D991
		internal ContextContainer frameData
		{
			get
			{
				return this.m_frameData;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000F799 File Offset: 0x0000D999
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000F7A1 File Offset: 0x0000D9A1
		internal bool useDepthPriming { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000F7AA File Offset: 0x0000D9AA
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000F7B2 File Offset: 0x0000D9B2
		internal bool stripShadowsOffVariants { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000F7BB File Offset: 0x0000D9BB
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000F7C3 File Offset: 0x0000D9C3
		internal bool stripAdditionalLightOffVariants { get; set; }

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public ScriptableRenderer(ScriptableRendererData data)
		{
			this.profilingExecute = new ProfilingSampler("ScriptableRenderer.Execute: " + data.name);
			foreach (ScriptableRendererFeature feature in data.rendererFeatures)
			{
				if (!(feature == null))
				{
					feature.Create();
					this.m_RendererFeatures.Add(feature);
				}
			}
			this.ResetNativeRenderPassFrameData();
			this.useRenderPassEnabled = data.useNativeRenderPass;
			this.Clear(CameraRenderType.Base);
			this.m_ActiveRenderPassQueue.Clear();
			if (UniversalRenderPipeline.asset)
			{
				this.m_StoreActionsOptimizationSetting = UniversalRenderPipeline.asset.storeActionsOptimization;
			}
			ScriptableRenderer.m_UseOptimizedStoreActions = this.m_StoreActionsOptimizationSetting != StoreActionsOptimization.Store;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public void Dispose()
		{
			for (int i = 0; i < this.m_RendererFeatures.Count; i++)
			{
				if (!(this.rendererFeatures[i] == null))
				{
					this.rendererFeatures[i].Dispose();
				}
			}
			this.Dispose(true);
			this.hasReleasedRTs = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000FA00 File Offset: 0x0000DC00
		protected virtual void Dispose(bool disposing)
		{
			DebugHandler debugHandler = this.DebugHandler;
			if (debugHandler == null)
			{
				return;
			}
			debugHandler.Dispose();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000217F File Offset: 0x0000037F
		internal virtual void ReleaseRenderTargets()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000FA12 File Offset: 0x0000DC12
		[Obsolete("Use RTHandles for colorTarget and depthTarget", true)]
		public void ConfigureCameraTarget(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget)
		{
			throw new NotSupportedException("ConfigureCameraTarget with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead");
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000FA1E File Offset: 0x0000DC1E
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public void ConfigureCameraTarget(RTHandle colorTarget, RTHandle depthTarget)
		{
			this.m_CameraColorTarget = colorTarget;
			this.m_CameraDepthTarget = depthTarget;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000FA2E File Offset: 0x0000DC2E
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureCameraTarget(RTHandle colorTarget, RTHandle depthTarget, RTHandle resolveTarget)
		{
			this.m_CameraColorTarget = colorTarget;
			this.m_CameraDepthTarget = depthTarget;
			this.m_CameraResolveTarget = resolveTarget;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FA45 File Offset: 0x0000DC45
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal void ConfigureCameraColorTarget(RTHandle colorTarget)
		{
			this.m_CameraColorTarget = colorTarget;
		}

		// Token: 0x06000402 RID: 1026
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public abstract void Setup(ScriptableRenderContext context, ref RenderingData renderingData);

		// Token: 0x06000403 RID: 1027 RVA: 0x0000217F File Offset: 0x0000037F
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public virtual void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void FinishRendering(CommandBuffer cmd)
		{
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void OnBeginRenderGraphFrame()
		{
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000217F File Offset: 0x0000037F
		internal virtual void OnRecordRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context)
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void OnEndRenderGraphFrame()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FA50 File Offset: 0x0000DC50
		private void InitRenderGraphFrame(RenderGraph renderGraph)
		{
			ScriptableRenderer.PassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<ScriptableRenderer.PassData>(ScriptableRenderer.Profiling.initRenderGraphFrame.name, out passData, ScriptableRenderer.Profiling.initRenderGraphFrame, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 846))
			{
				passData.renderer = this;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ScriptableRenderer.PassData>(delegate(ScriptableRenderer.PassData data, UnsafeGraphContext rgContext)
				{
					UnsafeCommandBuffer cmd = rgContext.cmd;
					float time = Time.time;
					float deltaTime = Time.deltaTime;
					float smoothDeltaTime = Time.smoothDeltaTime;
					ScriptableRenderer.ClearRenderingState(cmd);
					ScriptableRenderer.SetShaderTimeValues(cmd, time, deltaTime, smoothDeltaTime);
				});
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		internal void ProcessVFXCameraCommand(RenderGraph renderGraph)
		{
			UniversalRenderingData renderingData = this.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = this.frameData.Get<UniversalCameraData>();
			XRPass xr = cameraData.xr;
			ScriptableRenderer.VFXProcessCameraPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<ScriptableRenderer.VFXProcessCameraPassData>("ProcessVFXCameraCommand", out passData, ScriptableRenderer.Profiling.vfxProcessCamera, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 884))
			{
				passData.camera = cameraData.camera;
				passData.renderingData = renderingData;
				passData.cameraXRSettings.viewTotal = (xr.enabled ? 2U : 1U);
				passData.cameraXRSettings.viewCount = (uint)(xr.enabled ? xr.viewCount : 1);
				passData.cameraXRSettings.viewOffset = (uint)xr.multipassId;
				passData.xrPass = (xr.enabled ? xr : null);
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ScriptableRenderer.VFXProcessCameraPassData>(delegate(ScriptableRenderer.VFXProcessCameraPassData data, UnsafeGraphContext context)
				{
					if (data.xrPass != null)
					{
						data.xrPass.StartSinglePass(context.cmd);
					}
					CommandBufferHelpers.VFXManager_ProcessCameraCommand(data.camera, context.cmd, data.cameraXRSettings, data.renderingData.cullResults);
					if (data.xrPass != null)
					{
						data.xrPass.StopSinglePass(context.cmd);
					}
				});
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		internal void SetupRenderGraphCameraProperties(RenderGraph renderGraph, bool isTargetBackbuffer)
		{
			ScriptableRenderer.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ScriptableRenderer.PassData>(ScriptableRenderer.Profiling.setupCamera.name, out passData, ScriptableRenderer.Profiling.setupCamera, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 913))
			{
				passData.renderer = this;
				passData.cameraData = this.frameData.Get<UniversalCameraData>();
				passData.cameraTargetSizeCopy = new Vector2Int(passData.cameraData.cameraTargetDescriptor.width, passData.cameraData.cameraTargetDescriptor.height);
				passData.isTargetBackbuffer = isTargetBackbuffer;
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<ScriptableRenderer.PassData>(delegate(ScriptableRenderer.PassData data, RasterGraphContext context)
				{
					bool yFlip = !SystemInfo.graphicsUVStartsAtTop || data.isTargetBackbuffer;
					if (data.cameraData.renderType == CameraRenderType.Base)
					{
						context.cmd.SetupCameraProperties(data.cameraData.camera);
						data.renderer.SetPerCameraShaderVariables(context.cmd, data.cameraData, data.cameraTargetSizeCopy, !yFlip);
					}
					else
					{
						data.renderer.SetPerCameraShaderVariables(context.cmd, data.cameraData, data.cameraTargetSizeCopy, !yFlip);
						data.renderer.SetPerCameraClippingPlaneProperties(context.cmd, in data.cameraData, !yFlip);
						data.renderer.SetPerCameraBillboardProperties(context.cmd, data.cameraData);
					}
					float time = Time.time;
					float deltaTime = Time.deltaTime;
					float smoothDeltaTime = Time.smoothDeltaTime;
					ScriptableRenderer.SetShaderTimeValues(context.cmd, time, deltaTime, smoothDeltaTime);
				});
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000217F File Offset: 0x0000037F
		internal void DrawRenderGraphGizmos(RenderGraph renderGraph, ContextContainer frameData, TextureHandle color, TextureHandle depth, GizmoSubset gizmoSubset)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000217F File Offset: 0x0000037F
		internal void DrawRenderGraphWireOverlay(RenderGraph renderGraph, ContextContainer frameData, TextureHandle color)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FC98 File Offset: 0x0000DE98
		internal void BeginRenderGraphXRRendering(RenderGraph renderGraph)
		{
			UniversalCameraData cameraData = this.frameData.Get<UniversalCameraData>();
			if (!cameraData.xr.enabled)
			{
				return;
			}
			bool isDefaultXRViewport = XRSystem.GetRenderViewportScale() == 1f;
			cameraData.xrUniversal.canFoveateIntermediatePasses = !PlatformAutoDetect.isXRMobile || isDefaultXRViewport;
			ScriptableRenderer.BeginXRPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ScriptableRenderer.BeginXRPassData>("BeginXRRendering", out passData, ScriptableRenderer.Profiling.beginXRRendering, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 1055))
			{
				passData.cameraData = cameraData;
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<ScriptableRenderer.BeginXRPassData>(delegate(ScriptableRenderer.BeginXRPassData data, RasterGraphContext context)
				{
					if (data.cameraData.xr.enabled)
					{
						if (data.cameraData.xrUniversal.isLateLatchEnabled)
						{
							data.cameraData.xrUniversal.canMarkLateLatch = true;
						}
						data.cameraData.xr.StartSinglePass(context.cmd);
						if (data.cameraData.xr.supportsFoveatedRendering)
						{
							context.cmd.ConfigureFoveatedRendering(data.cameraData.xr.foveatedRenderingInfo);
							if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster))
							{
								context.cmd.SetKeyword(in ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, true);
							}
						}
					}
				});
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000FD54 File Offset: 0x0000DF54
		internal void EndRenderGraphXRRendering(RenderGraph renderGraph)
		{
			UniversalCameraData cameraData = this.frameData.Get<UniversalCameraData>();
			if (!cameraData.xr.enabled)
			{
				return;
			}
			ScriptableRenderer.EndXRPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ScriptableRenderer.EndXRPassData>("EndXRRendering", out passData, ScriptableRenderer.Profiling.endXRRendering, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 1096))
			{
				passData.cameraData = cameraData;
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<ScriptableRenderer.EndXRPassData>(delegate(ScriptableRenderer.EndXRPassData data, RasterGraphContext context)
				{
					if (data.cameraData.xr.enabled)
					{
						data.cameraData.xr.StopSinglePass(context.cmd);
					}
					if (XRSystem.foveatedRenderingCaps != FoveatedRenderingCaps.None)
					{
						if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster))
						{
							context.cmd.SetKeyword(in ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, false);
						}
						context.cmd.ConfigureFoveatedRendering(IntPtr.Zero);
					}
				});
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		private void SetEditorTarget(RenderGraph renderGraph)
		{
			ScriptableRenderer.DummyData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<ScriptableRenderer.DummyData>("SetEditorTarget", out passData, ScriptableRenderer.Profiling.setEditorTarget, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ScriptableRenderer.cs", 1129))
			{
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ScriptableRenderer.DummyData>(delegate(ScriptableRenderer.DummyData data, UnsafeGraphContext context)
				{
					context.cmd.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.DontCare);
				});
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FE64 File Offset: 0x0000E064
		internal void RecordRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context)
		{
			using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RecordRenderGraph)))
			{
				this.OnBeginRenderGraphFrame();
				using (new ProfilingScope(ScriptableRenderer.Profiling.sortRenderPasses))
				{
					ScriptableRenderer.SortStable(this.m_ActiveRenderPassQueue);
				}
				this.InitRenderGraphFrame(renderGraph);
				using (new ProfilingScope(ScriptableRenderer.Profiling.recordRenderGraph))
				{
					this.OnRecordRenderGraph(renderGraph, context);
				}
				this.OnEndRenderGraphFrame();
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FF14 File Offset: 0x0000E114
		internal void FinishRenderGraphRendering(CommandBuffer cmd)
		{
			UniversalCameraData cameraData = this.frameData.Get<UniversalCameraData>();
			this.OnFinishRenderGraphRendering(cmd);
			this.InternalFinishRenderingCommon(cmd, cameraData.resolveFinalTarget);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000217F File Offset: 0x0000037F
		internal virtual void OnFinishRenderGraphRendering(CommandBuffer cmd)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FF44 File Offset: 0x0000E144
		internal void RecordCustomRenderGraphPassesInEventRange(RenderGraph renderGraph, RenderPassEvent eventStart, RenderPassEvent eventEnd)
		{
			if (eventStart != eventEnd)
			{
				foreach (ScriptableRenderPass pass in this.m_ActiveRenderPassQueue)
				{
					if (pass.renderPassEvent >= eventStart && pass.renderPassEvent < eventEnd)
					{
						pass.RecordRenderGraph(renderGraph, this.m_frameData);
					}
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		internal void CalculateSplitEventRange(RenderPassEvent startInjectionPoint, RenderPassEvent targetEvent, out RenderPassEvent startEvent, out RenderPassEvent splitEvent, out RenderPassEvent endEvent)
		{
			int range = ScriptableRenderPass.GetRenderPassEventRange(startInjectionPoint);
			startEvent = startInjectionPoint;
			endEvent = startEvent + range;
			splitEvent = (RenderPassEvent)Math.Clamp((int)targetEvent, (int)startEvent, (int)endEvent);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		internal void RecordCustomRenderGraphPasses(RenderGraph renderGraph, RenderPassEvent startInjectionPoint, RenderPassEvent endInjectionPoint)
		{
			int range = ScriptableRenderPass.GetRenderPassEventRange(endInjectionPoint);
			this.RecordCustomRenderGraphPassesInEventRange(renderGraph, startInjectionPoint, endInjectionPoint + range);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FFFF File Offset: 0x0000E1FF
		internal void RecordCustomRenderGraphPasses(RenderGraph renderGraph, RenderPassEvent injectionPoint)
		{
			this.RecordCustomRenderGraphPasses(renderGraph, injectionPoint, injectionPoint);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001000C File Offset: 0x0000E20C
		internal bool InterruptFramebufferFetch(FramebufferFetchEvent fetchEvent, RenderPassEvent startInjectionPoint, RenderPassEvent endInjectionPoint)
		{
			int range = ScriptableRenderPass.GetRenderPassEventRange(endInjectionPoint);
			int nextValue = (int)(endInjectionPoint + range);
			foreach (ScriptableRenderPass pass in this.m_ActiveRenderPassQueue)
			{
				if (pass.renderPassEvent >= startInjectionPoint && pass.renderPassEvent < (RenderPassEvent)nextValue && fetchEvent == FramebufferFetchEvent.FetchGbufferInDeferred && pass.breakGBufferAndDeferredRenderPass)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001008C File Offset: 0x0000E28C
		internal void SetPerCameraProperties(ScriptableRenderContext context, UniversalCameraData cameraData, Camera camera, CommandBuffer cmd)
		{
			if (cameraData.renderType == CameraRenderType.Base)
			{
				context.SetupCameraProperties(camera, false);
				this.SetPerCameraShaderVariables(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData);
				return;
			}
			this.SetPerCameraShaderVariables(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData);
			this.SetPerCameraClippingPlaneProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData);
			this.SetPerCameraBillboardProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000100E4 File Offset: 0x0000E2E4
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			bool drawGizmos = DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.sceneOverrideMode == DebugSceneOverrideMode.None;
			this.hasReleasedRTs = false;
			this.m_IsPipelineExecuting = true;
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			Camera camera = cameraData.camera;
			if (this.rendererFeatures.Count != 0 && !renderingData.cameraData.isPreviewCamera)
			{
				this.SetupRenderPasses(in renderingData);
			}
			CommandBuffer cmd = *renderingData.commandBuffer;
			CommandBuffer cmdScope = (renderingData.cameraData.xr.enabled ? null : cmd);
			using (new ProfilingScope(cmdScope, this.profilingExecute))
			{
				this.InternalStartRendering(context, ref renderingData);
				float time = Time.time;
				float deltaTime = Time.deltaTime;
				float smoothDeltaTime = Time.smoothDeltaTime;
				ScriptableRenderer.ClearRenderingState(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
				ScriptableRenderer.SetShaderTimeValues(CommandBufferHelpers.GetRasterCommandBuffer(cmd), time, deltaTime, smoothDeltaTime);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				using (new ProfilingScope(ScriptableRenderer.Profiling.sortRenderPasses))
				{
					ScriptableRenderer.SortStable(this.m_ActiveRenderPassQueue);
				}
				using (new ProfilingScope(ScriptableRenderer.Profiling.RenderPass.configure))
				{
					foreach (ScriptableRenderPass scriptableRenderPass in this.activeRenderPassQueue)
					{
						scriptableRenderPass.Configure(cmd, cameraData.cameraTargetDescriptor);
					}
					context.ExecuteCommandBuffer(cmd);
					cmd.Clear();
				}
				this.SetupNativeRenderPassFrameData(cameraData, this.useRenderPassEnabled);
				ScriptableRenderer.RenderBlocks renderBlocks = new ScriptableRenderer.RenderBlocks(this.m_ActiveRenderPassQueue);
				try
				{
					using (new ProfilingScope(ScriptableRenderer.Profiling.setupLights))
					{
						this.SetupLights(context, ref renderingData);
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.BeforeRendering) > 0)
					{
						using (new ProfilingScope(ScriptableRenderer.Profiling.RenderBlock.beforeRendering))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.BeforeRendering, in renderBlocks, context, ref renderingData, false);
						}
					}
					using (new ProfilingScope(ScriptableRenderer.Profiling.setupCamera))
					{
						this.SetPerCameraProperties(context, cameraData, camera, cmd);
						ScriptableRenderer.SetShaderTimeValues(CommandBufferHelpers.GetRasterCommandBuffer(cmd), time, deltaTime, smoothDeltaTime);
					}
					context.ExecuteCommandBuffer(cmd);
					cmd.Clear();
					this.BeginXRRendering(cmd, context, ref renderingData.cameraData);
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.MainRenderingOpaque) > 0)
					{
						using (new ProfilingScope(ScriptableRenderer.Profiling.RenderBlock.mainRenderingOpaque))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.MainRenderingOpaque, in renderBlocks, context, ref renderingData, false);
						}
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.MainRenderingTransparent) > 0)
					{
						using (new ProfilingScope(ScriptableRenderer.Profiling.RenderBlock.mainRenderingTransparent))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.MainRenderingTransparent, in renderBlocks, context, ref renderingData, false);
						}
					}
					if (cameraData.xr.enabled)
					{
						cameraData.xrUniversal.canMarkLateLatch = false;
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.AfterRendering) > 0)
					{
						using (new ProfilingScope(ScriptableRenderer.Profiling.RenderBlock.afterRendering))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.AfterRendering, in renderBlocks, context, ref renderingData, false);
						}
					}
					this.EndXRRendering(cmd, context, ref renderingData.cameraData);
					this.InternalFinishRenderingExecute(context, cmd, cameraData.resolveFinalTarget);
					for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
					{
						this.m_ActiveRenderPassQueue[i].m_ColorAttachmentIndices.Dispose();
						this.m_ActiveRenderPassQueue[i].m_InputAttachmentIndices.Dispose();
					}
				}
				finally
				{
					((IDisposable)renderBlocks).Dispose();
				}
			}
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010580 File Offset: 0x0000E780
		public void EnqueuePass(ScriptableRenderPass pass)
		{
			this.m_ActiveRenderPassQueue.Add(pass);
			if (this.disableNativeRenderPassInFeatures)
			{
				pass.useNativeRenderPass = false;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001059D File Offset: 0x0000E79D
		protected static ClearFlag GetCameraClearFlag(ref CameraData cameraData)
		{
			return ScriptableRenderer.GetCameraClearFlag(cameraData.universalCameraData);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000105AC File Offset: 0x0000E7AC
		protected static ClearFlag GetCameraClearFlag(UniversalCameraData cameraData)
		{
			CameraClearFlags cameraClearFlags = cameraData.camera.clearFlags;
			if (cameraData.renderType == CameraRenderType.Overlay)
			{
				if (!cameraData.clearDepth)
				{
					return ClearFlag.None;
				}
				return ClearFlag.DepthStencil;
			}
			else
			{
				DebugHandler debugHandler = cameraData.renderer.DebugHandler;
				if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && debugHandler.IsScreenClearNeeded)
				{
					return ClearFlag.All;
				}
				if (cameraClearFlags == CameraClearFlags.Skybox && RenderSettings.skybox != null && cameraData.postProcessEnabled && cameraData.xr.enabled)
				{
					return ClearFlag.All;
				}
				if ((cameraClearFlags != CameraClearFlags.Skybox || !(RenderSettings.skybox != null)) && cameraClearFlags != CameraClearFlags.Nothing)
				{
					return ClearFlag.All;
				}
				if (cameraData.cameraTargetDescriptor.msaaSamples > 1)
				{
					cameraData.camera.backgroundColor = Color.black;
					return ClearFlag.All;
				}
				return ClearFlag.DepthStencil;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00010664 File Offset: 0x0000E864
		internal void OnPreCullRenderPasses(in CameraData cameraData)
		{
			for (int i = 0; i < this.rendererFeatures.Count; i++)
			{
				if (this.rendererFeatures[i].isActive)
				{
					this.rendererFeatures[i].OnCameraPreCull(this, in cameraData);
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000106B0 File Offset: 0x0000E8B0
		internal void AddRenderPasses(ref RenderingData renderingData)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.addRenderPasses))
			{
				for (int i = 0; i < this.rendererFeatures.Count; i++)
				{
					if (this.rendererFeatures[i].isActive)
					{
						if (!this.rendererFeatures[i].SupportsNativeRenderPass())
						{
							this.disableNativeRenderPassInFeatures = true;
						}
						this.rendererFeatures[i].AddRenderPasses(this, ref renderingData);
						this.disableNativeRenderPassInFeatures = false;
					}
				}
				int count = this.activeRenderPassQueue.Count;
				for (int j = count - 1; j >= 0; j--)
				{
					if (this.activeRenderPassQueue[j] == null)
					{
						this.activeRenderPassQueue.RemoveAt(j);
					}
				}
				if (count > 0 && this.m_StoreActionsOptimizationSetting == StoreActionsOptimization.Auto)
				{
					ScriptableRenderer.m_UseOptimizedStoreActions = false;
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001078C File Offset: 0x0000E98C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		protected void SetupRenderPasses(in RenderingData renderingData)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.setupRenderPasses))
			{
				for (int i = 0; i < this.rendererFeatures.Count; i++)
				{
					if (this.rendererFeatures[i].isActive)
					{
						this.rendererFeatures[i].SetupRenderPasses(this, in renderingData);
					}
				}
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00010804 File Offset: 0x0000EA04
		private static void ClearRenderingState(IBaseCommandBuffer cmd)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.clearRenderingState))
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadows, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowCascades, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightsVertex, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightsPixel, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.ForwardPlus, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightShadows, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.ReflectionProbeBlending, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.ReflectionProbeBoxProjection, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsLow, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsMedium, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsHigh, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.MixedLightingSubtractive, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.LightmapShadowMixing, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.ShadowsShadowMask, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.LinearToSRGBConversion, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.LightLayers, false);
				cmd.SetGlobalVector(ScreenSpaceAmbientOcclusionPass.s_AmbientOcclusionParamID, Vector4.zero);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001091C File Offset: 0x0000EB1C
		internal void Clear(CameraRenderType cameraType)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = ScriptableRenderer.k_CameraTarget;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = null;
			}
			for (int j = 0; j < ScriptableRenderer.m_ActiveColorAttachments.Length; j++)
			{
				RenderTargetIdentifier[] activeColorAttachmentIDs = ScriptableRenderer.m_ActiveColorAttachmentIDs;
				int num = j;
				RTHandle rthandle = ScriptableRenderer.m_ActiveColorAttachments[j];
				activeColorAttachmentIDs[num] = ((rthandle != null) ? rthandle.nameID : 0);
			}
			ScriptableRenderer.m_ActiveDepthAttachment = ScriptableRenderer.k_CameraTarget;
			this.m_FirstTimeCameraColorTargetIsBound = cameraType == CameraRenderType.Base;
			this.m_FirstTimeCameraDepthTargetIsBound = true;
			this.m_CameraColorTarget = null;
			this.m_CameraDepthTarget = null;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000109B0 File Offset: 0x0000EBB0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private void ExecuteBlock(int blockIndex, in ScriptableRenderer.RenderBlocks renderBlocks, ScriptableRenderContext context, ref RenderingData renderingData, bool submit = false)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			ScriptableRenderer.RenderBlocks renderBlocks2 = renderBlocks;
			foreach (int currIndex in renderBlocks2.GetRange(blockIndex))
			{
				ScriptableRenderPass renderPass = this.m_ActiveRenderPassQueue[currIndex];
				this.ExecuteRenderPass(context, renderPass, cameraData, ref renderingData);
			}
			if (submit)
			{
				context.Submit();
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00010A3C File Offset: 0x0000EC3C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private bool IsRenderPassEnabled(ScriptableRenderPass renderPass)
		{
			return renderPass.useNativeRenderPass && this.useRenderPassEnabled;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00010A50 File Offset: 0x0000EC50
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private unsafe void ExecuteRenderPass(ScriptableRenderContext context, ScriptableRenderPass renderPass, UniversalCameraData cameraData, ref RenderingData renderingData)
		{
			using (new ProfilingScope(renderPass.profilingSampler))
			{
				CommandBuffer cmd = *renderingData.commandBuffer;
				if (cameraData.xr.supportsFoveatedRendering && ((renderPass.renderPassEvent >= RenderPassEvent.BeforeRenderingPrePasses && renderPass.renderPassEvent < RenderPassEvent.BeforeRenderingPostProcessing) || (renderPass.renderPassEvent > RenderPassEvent.AfterRendering && XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.FoveationImage))))
				{
					cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
				}
				using (new ProfilingScope(ScriptableRenderer.Profiling.RenderPass.setRenderPassAttachments))
				{
					this.SetRenderPassAttachments(cmd, renderPass, cameraData);
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				if (this.IsRenderPassEnabled(renderPass) && cameraData.isRenderPassSupportedCamera)
				{
					this.ExecuteNativeRenderPass(context, renderPass, cameraData, ref renderingData);
				}
				else
				{
					renderPass.Execute(context, ref renderingData);
					context.ExecuteCommandBuffer(cmd);
					cmd.Clear();
				}
				if (cameraData.xr.enabled)
				{
					if (cameraData.xr.supportsFoveatedRendering)
					{
						cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
					}
					XRSystemUniversal.UnmarkShaderProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData.xrUniversal);
					context.ExecuteCommandBuffer(cmd);
					cmd.Clear();
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00002886 File Offset: 0x00000A86
		internal bool IsSceneFilteringEnabled(Camera camera)
		{
			return false;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private void SetRenderPassAttachments(CommandBuffer cmd, ScriptableRenderPass renderPass, UniversalCameraData cameraData)
		{
			Camera camera = cameraData.camera;
			ClearFlag cameraClearFlag = ScriptableRenderer.GetCameraClearFlag(cameraData);
			if (RenderingUtils.GetValidColorBufferCount(renderPass.colorAttachmentHandles) == 0U)
			{
				return;
			}
			if (RenderingUtils.IsMRT(renderPass.colorAttachmentHandles))
			{
				bool needCustomCameraColorClear = false;
				bool needCustomCameraDepthClear = false;
				int cameraColorTargetIndex = RenderingUtils.IndexOf(renderPass.colorAttachmentHandles, this.m_CameraColorTarget);
				if (cameraColorTargetIndex != -1 && this.m_FirstTimeCameraColorTargetIsBound)
				{
					this.m_FirstTimeCameraColorTargetIsBound = false;
					needCustomCameraColorClear = (cameraClearFlag & ClearFlag.Color) != (renderPass.clearFlag & ClearFlag.Color) || cameraData.backgroundColor != renderPass.clearColor;
				}
				RenderTargetIdentifier depthTargetID = this.m_CameraDepthTarget.nameID;
				if (cameraData.xr.enabled)
				{
					depthTargetID = new RenderTargetIdentifier(depthTargetID, 0, CubemapFace.Unknown, -1);
				}
				if (new RenderTargetIdentifier(renderPass.depthAttachmentHandle.nameID, 0, CubemapFace.Unknown, 0) == new RenderTargetIdentifier(depthTargetID, 0, CubemapFace.Unknown, 0) && this.m_FirstTimeCameraDepthTargetIsBound)
				{
					this.m_FirstTimeCameraDepthTargetIsBound = false;
					needCustomCameraDepthClear = (cameraClearFlag & ClearFlag.DepthStencil) != (renderPass.clearFlag & ClearFlag.DepthStencil);
				}
				if (needCustomCameraColorClear)
				{
					if ((cameraClearFlag & ClearFlag.Color) != ClearFlag.None && (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera))
					{
						ScriptableRenderer.SetRenderTarget(cmd, renderPass.colorAttachmentHandles[cameraColorTargetIndex], renderPass.depthAttachmentHandle, ClearFlag.Color, cameraData.backgroundColor);
					}
					if ((renderPass.clearFlag & ClearFlag.Color) != ClearFlag.None)
					{
						uint otherTargetsCount = RenderingUtils.CountDistinct(renderPass.colorAttachmentHandles, this.m_CameraColorTarget);
						RTHandle[] nonCameraAttachments = ScriptableRenderer.m_TrimmedColorAttachmentCopies[(int)otherTargetsCount];
						int writeIndex = 0;
						for (int readIndex = 0; readIndex < renderPass.colorAttachmentHandles.Length; readIndex++)
						{
							if (renderPass.colorAttachmentHandles[readIndex] != null && renderPass.colorAttachmentHandles[readIndex].nameID != 0 && renderPass.colorAttachmentHandles[readIndex].nameID != this.m_CameraColorTarget.nameID)
							{
								nonCameraAttachments[writeIndex] = renderPass.colorAttachmentHandles[readIndex];
								writeIndex++;
							}
						}
						RenderTargetIdentifier[] nonCameraAttachmentIDs = ScriptableRenderer.m_TrimmedColorAttachmentCopyIDs[(int)otherTargetsCount];
						int i = 0;
						while ((long)i < (long)((ulong)otherTargetsCount))
						{
							nonCameraAttachmentIDs[i] = nonCameraAttachments[i].nameID;
							i++;
						}
						if ((long)writeIndex != (long)((ulong)otherTargetsCount))
						{
							Debug.LogError("writeIndex and otherTargetsCount values differed. writeIndex:" + writeIndex.ToString() + " otherTargetsCount:" + otherTargetsCount.ToString());
						}
						if (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera)
						{
							ScriptableRenderer.SetRenderTarget(cmd, nonCameraAttachments, nonCameraAttachmentIDs, this.m_CameraDepthTarget, ClearFlag.Color, renderPass.clearColor);
						}
					}
				}
				ClearFlag finalClearFlag = ClearFlag.None;
				finalClearFlag |= (needCustomCameraDepthClear ? (cameraClearFlag & ClearFlag.DepthStencil) : (renderPass.clearFlag & ClearFlag.DepthStencil));
				finalClearFlag |= (needCustomCameraColorClear ? (this.IsRenderPassEnabled(renderPass) ? (cameraClearFlag & ClearFlag.Color) : ClearFlag.None) : (renderPass.clearFlag & ClearFlag.Color));
				if (this.IsRenderPassEnabled(renderPass) && cameraData.isRenderPassSupportedCamera)
				{
					this.SetNativeRenderPassMRTAttachmentList(renderPass, cameraData, needCustomCameraColorClear, finalClearFlag);
				}
				if (!RenderingUtils.SequenceEqual(renderPass.colorAttachmentHandles, ScriptableRenderer.m_ActiveColorAttachments) || renderPass.depthAttachmentHandle.nameID != ScriptableRenderer.m_ActiveDepthAttachment || finalClearFlag != ClearFlag.None)
				{
					int lastValidRTindex = RenderingUtils.LastValid(renderPass.colorAttachmentHandles);
					if (lastValidRTindex >= 0)
					{
						int rtCount = lastValidRTindex + 1;
						RTHandle[] trimmedAttachments = ScriptableRenderer.m_TrimmedColorAttachmentCopies[rtCount];
						for (int j = 0; j < rtCount; j++)
						{
							trimmedAttachments[j] = renderPass.colorAttachmentHandles[j];
						}
						RenderTargetIdentifier[] trimmedAttachmentIDs = ScriptableRenderer.m_TrimmedColorAttachmentCopyIDs[rtCount];
						for (int k = 0; k < rtCount; k++)
						{
							trimmedAttachmentIDs[k] = trimmedAttachments[k].nameID;
						}
						if (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera)
						{
							RTHandle depthAttachment = this.m_CameraDepthTarget;
							if (renderPass.overrideCameraTarget)
							{
								depthAttachment = renderPass.depthAttachmentHandle;
							}
							else
							{
								this.m_FirstTimeCameraDepthTargetIsBound = false;
							}
							ScriptableRenderer.SetRenderTarget(cmd, trimmedAttachments, trimmedAttachmentIDs, depthAttachment, finalClearFlag, renderPass.clearColor);
						}
						if (cameraData.xr.enabled)
						{
							bool renderIntoTexture = RenderingUtils.IndexOf(renderPass.colorAttachmentHandles, cameraData.xr.renderTarget) == -1;
							cameraData.PushBuiltinShaderConstantsXR(CommandBufferHelpers.GetRasterCommandBuffer(cmd), renderIntoTexture);
							XRSystemUniversal.MarkShaderProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData.xrUniversal, renderIntoTexture);
							return;
						}
					}
				}
			}
			else
			{
				RTHandle passColorAttachment = renderPass.colorAttachmentHandle;
				RTHandle passDepthAttachment = renderPass.depthAttachmentHandle;
				if (!renderPass.overrideCameraTarget)
				{
					if (renderPass.renderPassEvent < RenderPassEvent.BeforeRenderingPrePasses)
					{
						return;
					}
					passColorAttachment = this.m_CameraColorTarget;
					passDepthAttachment = this.m_CameraDepthTarget;
				}
				ClearFlag finalClearFlag2 = ClearFlag.None;
				Color finalClearColor;
				if (passColorAttachment.nameID == this.m_CameraColorTarget.nameID && this.m_FirstTimeCameraColorTargetIsBound)
				{
					this.m_FirstTimeCameraColorTargetIsBound = false;
					finalClearFlag2 |= cameraClearFlag & ClearFlag.Color;
					if (SystemInfo.usesLoadStoreActions && new RenderTargetIdentifier(passColorAttachment.nameID, 0, CubemapFace.Unknown, 0) != BuiltinRenderTextureType.CameraTarget)
					{
						finalClearFlag2 |= renderPass.clearFlag;
					}
					finalClearColor = cameraData.backgroundColor;
					if (this.m_FirstTimeCameraDepthTargetIsBound)
					{
						this.m_FirstTimeCameraDepthTargetIsBound = false;
						finalClearFlag2 |= cameraClearFlag & ClearFlag.DepthStencil;
					}
				}
				else
				{
					finalClearFlag2 |= renderPass.clearFlag & ClearFlag.Color;
					finalClearColor = renderPass.clearColor;
				}
				if (new RenderTargetIdentifier(this.m_CameraDepthTarget.nameID, 0, CubemapFace.Unknown, 0) != BuiltinRenderTextureType.CameraTarget && (passDepthAttachment.nameID == this.m_CameraDepthTarget.nameID || passColorAttachment.nameID == this.m_CameraDepthTarget.nameID) && this.m_FirstTimeCameraDepthTargetIsBound)
				{
					this.m_FirstTimeCameraDepthTargetIsBound = false;
					finalClearFlag2 |= cameraClearFlag & ClearFlag.DepthStencil;
				}
				else
				{
					finalClearFlag2 |= renderPass.clearFlag & ClearFlag.DepthStencil;
				}
				if (this.IsSceneFilteringEnabled(camera))
				{
					finalClearColor.a = 0f;
					finalClearFlag2 &= ~ClearFlag.Depth;
				}
				if (this.DebugHandler != null && this.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
				{
					this.DebugHandler.TryGetScreenClearColor(ref finalClearColor);
				}
				if (this.IsRenderPassEnabled(renderPass) && cameraData.isRenderPassSupportedCamera)
				{
					this.SetNativeRenderPassAttachmentList(renderPass, cameraData, passColorAttachment, passDepthAttachment, finalClearFlag2, finalClearColor);
					return;
				}
				bool colorAttachmentChanged = false;
				if (passColorAttachment.nameID != ScriptableRenderer.m_ActiveColorAttachments[0])
				{
					colorAttachmentChanged = true;
				}
				for (int l = 1; l < ScriptableRenderer.m_ActiveColorAttachments.Length; l++)
				{
					if (renderPass.colorAttachmentHandles[l] != ScriptableRenderer.m_ActiveColorAttachments[l])
					{
						colorAttachmentChanged = true;
						break;
					}
				}
				if (colorAttachmentChanged || passDepthAttachment.nameID != ScriptableRenderer.m_ActiveDepthAttachment || finalClearFlag2 != ClearFlag.None || renderPass.colorStoreActions[0] != ScriptableRenderer.m_ActiveColorStoreActions[0] || renderPass.depthStoreAction != ScriptableRenderer.m_ActiveDepthStoreAction)
				{
					ScriptableRenderer.SetRenderTarget(cmd, passColorAttachment, passDepthAttachment, finalClearFlag2, finalClearColor, renderPass.colorStoreActions[0], renderPass.depthStoreAction);
					if (cameraData.xr.enabled)
					{
						bool renderIntoTexture2 = passColorAttachment.nameID != cameraData.xr.renderTarget;
						cameraData.PushBuiltinShaderConstantsXR(CommandBufferHelpers.GetRasterCommandBuffer(cmd), renderIntoTexture2);
						XRSystemUniversal.MarkShaderProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData.xrUniversal, renderIntoTexture2);
					}
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011224 File Offset: 0x0000F424
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private void BeginXRRendering(CommandBuffer cmd, ScriptableRenderContext context, ref CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				if (cameraData.xrUniversal.isLateLatchEnabled)
				{
					cameraData.xrUniversal.canMarkLateLatch = true;
				}
				cameraData.xr.StartSinglePass(cmd);
				if (cameraData.xr.supportsFoveatedRendering)
				{
					cmd.ConfigureFoveatedRendering(cameraData.xr.foveatedRenderingInfo);
					if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster))
					{
						cmd.SetKeyword(in ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, true);
					}
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000112B4 File Offset: 0x0000F4B4
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private void EndXRRendering(CommandBuffer cmd, ScriptableRenderContext context, ref CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				cameraData.xr.StopSinglePass(cmd);
				if (XRSystem.foveatedRenderingCaps != FoveatedRenderingCaps.None)
				{
					if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster))
					{
						cmd.SetKeyword(in ShaderGlobalKeywords.FoveatedRenderingNonUniformRaster, false);
					}
					cmd.ConfigureFoveatedRendering(IntPtr.Zero);
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011320 File Offset: 0x0000F520
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal static void SetRenderTarget(CommandBuffer cmd, RTHandle colorAttachment, RTHandle depthAttachment, ClearFlag clearFlag, Color clearColor)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = colorAttachment;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = null;
			}
			for (int j = 0; j < ScriptableRenderer.m_ActiveColorAttachments.Length; j++)
			{
				RenderTargetIdentifier[] activeColorAttachmentIDs = ScriptableRenderer.m_ActiveColorAttachmentIDs;
				int num = j;
				RTHandle rthandle = ScriptableRenderer.m_ActiveColorAttachments[j];
				activeColorAttachmentIDs[num] = ((rthandle != null) ? rthandle.nameID : 0);
			}
			ScriptableRenderer.m_ActiveColorStoreActions[0] = RenderBufferStoreAction.Store;
			ScriptableRenderer.m_ActiveDepthStoreAction = RenderBufferStoreAction.Store;
			for (int k = 1; k < ScriptableRenderer.m_ActiveColorStoreActions.Length; k++)
			{
				ScriptableRenderer.m_ActiveColorStoreActions[k] = RenderBufferStoreAction.Store;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			RenderBufferLoadAction colorLoadAction = (((clearFlag & ClearFlag.Color) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			RenderBufferLoadAction depthLoadAction = (((clearFlag & ClearFlag.Depth) != ClearFlag.None || (clearFlag & ClearFlag.Stencil) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			if (colorAttachment.rt == null && depthAttachment.rt == null && depthAttachment.nameID == ScriptableRenderer.k_CameraTarget.nameID)
			{
				ScriptableRenderer.SetRenderTarget(cmd, colorAttachment, colorLoadAction, RenderBufferStoreAction.Store, colorAttachment, depthLoadAction, RenderBufferStoreAction.Store, clearFlag, clearColor);
				return;
			}
			ScriptableRenderer.SetRenderTarget(cmd, colorAttachment, colorLoadAction, RenderBufferStoreAction.Store, depthAttachment, depthLoadAction, RenderBufferStoreAction.Store, clearFlag, clearColor);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011424 File Offset: 0x0000F624
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal static void SetRenderTarget(CommandBuffer cmd, RTHandle colorAttachment, RTHandle depthAttachment, ClearFlag clearFlag, Color clearColor, RenderBufferStoreAction colorStoreAction, RenderBufferStoreAction depthStoreAction)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = colorAttachment;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = null;
			}
			for (int j = 0; j < ScriptableRenderer.m_ActiveColorAttachments.Length; j++)
			{
				RenderTargetIdentifier[] activeColorAttachmentIDs = ScriptableRenderer.m_ActiveColorAttachmentIDs;
				int num = j;
				RTHandle rthandle = ScriptableRenderer.m_ActiveColorAttachments[j];
				activeColorAttachmentIDs[num] = ((rthandle != null) ? rthandle.nameID : 0);
			}
			ScriptableRenderer.m_ActiveColorStoreActions[0] = colorStoreAction;
			ScriptableRenderer.m_ActiveDepthStoreAction = depthStoreAction;
			for (int k = 1; k < ScriptableRenderer.m_ActiveColorStoreActions.Length; k++)
			{
				ScriptableRenderer.m_ActiveColorStoreActions[k] = RenderBufferStoreAction.Store;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			RenderBufferLoadAction colorLoadAction = (((clearFlag & ClearFlag.Color) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			RenderBufferLoadAction depthLoadAction = (((clearFlag & ClearFlag.Depth) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			if (!ScriptableRenderer.m_UseOptimizedStoreActions)
			{
				if (colorStoreAction != RenderBufferStoreAction.StoreAndResolve)
				{
					colorStoreAction = RenderBufferStoreAction.Store;
				}
				if (depthStoreAction != RenderBufferStoreAction.StoreAndResolve)
				{
					depthStoreAction = RenderBufferStoreAction.Store;
				}
			}
			ScriptableRenderer.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, depthAttachment, depthLoadAction, depthStoreAction, clearFlag, clearColor);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000114FC File Offset: 0x0000F6FC
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private static void SetRenderTarget(CommandBuffer cmd, RTHandle colorAttachment, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RTHandle depthAttachment, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlags, Color clearColor)
		{
			if (depthAttachment.nameID == BuiltinRenderTextureType.CameraTarget)
			{
				CoreUtils.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, colorAttachment, depthLoadAction, depthStoreAction, clearFlags, clearColor, 0, CubemapFace.Unknown, -1);
				return;
			}
			CoreUtils.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, depthAttachment, depthLoadAction, depthStoreAction, clearFlags, clearColor, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00011549 File Offset: 0x0000F749
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		private static void SetRenderTarget(CommandBuffer cmd, RTHandle[] colorAttachments, RenderTargetIdentifier[] colorAttachmentIDs, RTHandle depthAttachment, ClearFlag clearFlag, Color clearColor)
		{
			ScriptableRenderer.m_ActiveColorAttachments = colorAttachments;
			ScriptableRenderer.m_ActiveColorAttachmentIDs = colorAttachmentIDs;
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			CoreUtils.SetRenderTarget(cmd, ScriptableRenderer.m_ActiveColorAttachmentIDs, depthAttachment, clearFlag, clearColor);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000217F File Offset: 0x0000037F
		internal virtual void SwapColorBuffer(CommandBuffer cmd)
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000217F File Offset: 0x0000037F
		internal virtual void EnableSwapBufferMSAA(bool enable)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000217F File Offset: 0x0000037F
		[Conditional("UNITY_EDITOR")]
		private void DrawGizmos(ScriptableRenderContext context, Camera camera, GizmoSubset gizmoSubset, ref RenderingData renderingData)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001156D File Offset: 0x0000F76D
		[Conditional("UNITY_EDITOR")]
		private void DrawWireOverlay(ScriptableRenderContext context, Camera camera)
		{
			context.DrawWireOverlay(camera);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011578 File Offset: 0x0000F778
		private unsafe void InternalStartRendering(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.internalStartRendering))
			{
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					this.m_ActiveRenderPassQueue[i].OnCameraSetup(*renderingData.commandBuffer, ref renderingData);
				}
			}
			context.ExecuteCommandBuffer(*renderingData.commandBuffer);
			renderingData.commandBuffer->Clear();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000115FC File Offset: 0x0000F7FC
		private void InternalFinishRenderingCommon(CommandBuffer cmd, bool resolveFinalTarget)
		{
			using (new ProfilingScope(ScriptableRenderer.Profiling.internalFinishRenderingCommon))
			{
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					this.m_ActiveRenderPassQueue[i].FrameCleanup(cmd);
				}
				if (resolveFinalTarget)
				{
					for (int j = 0; j < this.m_ActiveRenderPassQueue.Count; j++)
					{
						this.m_ActiveRenderPassQueue[j].OnFinishCameraStackRendering(cmd);
					}
					this.FinishRendering(cmd);
					this.m_IsPipelineExecuting = false;
				}
				this.m_ActiveRenderPassQueue.Clear();
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000116A4 File Offset: 0x0000F8A4
		private void InternalFinishRenderingExecute(ScriptableRenderContext context, CommandBuffer cmd, bool resolveFinalTarget)
		{
			this.InternalFinishRenderingCommon(cmd, resolveFinalTarget);
			this.ResetNativeRenderPassFrameData();
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000116C4 File Offset: 0x0000F8C4
		private protected int AdjustAndGetScreenMSAASamples(RenderGraph renderGraph, bool useIntermediateColorTarget)
		{
			if (UniversalRenderPipeline.canOptimizeScreenMSAASamples && useIntermediateColorTarget && renderGraph.nativeRenderPassesEnabled && Screen.msaaSamples > 1)
			{
				Screen.SetMSAASamples(1);
			}
			if (Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.IPhonePlayer)
			{
				return Mathf.Max(Screen.msaaSamples, 1);
			}
			return Mathf.Max(UniversalRenderPipeline.startFrameScreenMSAASamples, 1);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011724 File Offset: 0x0000F924
		internal static void SortStable(List<ScriptableRenderPass> list)
		{
			for (int i = 1; i < list.Count; i++)
			{
				ScriptableRenderPass curr = list[i];
				int j = i - 1;
				while (j >= 0 && curr < list[j])
				{
					list[j + 1] = list[j];
					j--;
				}
				list[j + 1] = curr;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00002886 File Offset: 0x00000A86
		internal virtual bool supportsNativeRenderPassRendergraphCompiler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00002886 File Offset: 0x00000A86
		public virtual bool supportsGPUOcclusion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400031A RID: 794
		internal const int kRenderPassMapSize = 10;

		// Token: 0x0400031B RID: 795
		internal const int kRenderPassMaxCount = 20;

		// Token: 0x0400031C RID: 796
		private int m_LastBeginSubpassPassIndex;

		// Token: 0x0400031D RID: 797
		private Dictionary<Hash128, int[]> m_MergeableRenderPassesMap = new Dictionary<Hash128, int[]>(10);

		// Token: 0x0400031E RID: 798
		private int[][] m_MergeableRenderPassesMapArrays;

		// Token: 0x0400031F RID: 799
		private Hash128[] m_PassIndexToPassHash = new Hash128[20];

		// Token: 0x04000320 RID: 800
		private Dictionary<Hash128, int> m_RenderPassesAttachmentCount = new Dictionary<Hash128, int>(10);

		// Token: 0x04000321 RID: 801
		private int m_firstPassIndexOfLastMergeableGroup;

		// Token: 0x04000322 RID: 802
		private AttachmentDescriptor[] m_ActiveColorAttachmentDescriptors = new AttachmentDescriptor[]
		{
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment
		};

		// Token: 0x04000323 RID: 803
		private AttachmentDescriptor m_ActiveDepthAttachmentDescriptor;

		// Token: 0x04000324 RID: 804
		private bool[] m_IsActiveColorAttachmentTransient = new bool[8];

		// Token: 0x04000325 RID: 805
		internal RenderBufferStoreAction[] m_FinalColorStoreAction = new RenderBufferStoreAction[8];

		// Token: 0x04000326 RID: 806
		internal RenderBufferStoreAction m_FinalDepthStoreAction;

		// Token: 0x04000328 RID: 808
		internal bool hasReleasedRTs = true;

		// Token: 0x0400032A RID: 810
		internal static ScriptableRenderer current = null;

		// Token: 0x0400032D RID: 813
		private StoreActionsOptimization m_StoreActionsOptimizationSetting;

		// Token: 0x0400032E RID: 814
		private static bool m_UseOptimizedStoreActions = false;

		// Token: 0x0400032F RID: 815
		private const int k_RenderPassBlockCount = 4;

		// Token: 0x04000330 RID: 816
		protected static readonly RTHandle k_CameraTarget = RTHandles.Alloc(BuiltinRenderTextureType.CameraTarget);

		// Token: 0x04000331 RID: 817
		private List<ScriptableRenderPass> m_ActiveRenderPassQueue = new List<ScriptableRenderPass>(32);

		// Token: 0x04000332 RID: 818
		private List<ScriptableRendererFeature> m_RendererFeatures = new List<ScriptableRendererFeature>(10);

		// Token: 0x04000333 RID: 819
		private RTHandle m_CameraColorTarget;

		// Token: 0x04000334 RID: 820
		private RTHandle m_CameraDepthTarget;

		// Token: 0x04000335 RID: 821
		private RTHandle m_CameraResolveTarget;

		// Token: 0x04000336 RID: 822
		private bool m_FirstTimeCameraColorTargetIsBound = true;

		// Token: 0x04000337 RID: 823
		private bool m_FirstTimeCameraDepthTargetIsBound = true;

		// Token: 0x04000338 RID: 824
		private bool m_IsPipelineExecuting;

		// Token: 0x04000339 RID: 825
		internal bool disableNativeRenderPassInFeatures;

		// Token: 0x0400033A RID: 826
		internal bool useRenderPassEnabled;

		// Token: 0x0400033B RID: 827
		private static RenderTargetIdentifier[] m_ActiveColorAttachmentIDs = new RenderTargetIdentifier[8];

		// Token: 0x0400033C RID: 828
		private static RTHandle[] m_ActiveColorAttachments = new RTHandle[8];

		// Token: 0x0400033D RID: 829
		private static RTHandle m_ActiveDepthAttachment;

		// Token: 0x0400033E RID: 830
		private ContextContainer m_frameData = new ContextContainer();

		// Token: 0x0400033F RID: 831
		private static RenderBufferStoreAction[] m_ActiveColorStoreActions = new RenderBufferStoreAction[8];

		// Token: 0x04000340 RID: 832
		private static RenderBufferStoreAction m_ActiveDepthStoreAction = RenderBufferStoreAction.Store;

		// Token: 0x04000341 RID: 833
		private static RenderTargetIdentifier[][] m_TrimmedColorAttachmentCopyIDs = new RenderTargetIdentifier[][]
		{
			Array.Empty<RenderTargetIdentifier>(),
			new RenderTargetIdentifier[1],
			new RenderTargetIdentifier[2],
			new RenderTargetIdentifier[3],
			new RenderTargetIdentifier[4],
			new RenderTargetIdentifier[5],
			new RenderTargetIdentifier[6],
			new RenderTargetIdentifier[7],
			new RenderTargetIdentifier[8]
		};

		// Token: 0x04000342 RID: 834
		private static RTHandle[][] m_TrimmedColorAttachmentCopies = new RTHandle[][]
		{
			Array.Empty<RTHandle>(),
			new RTHandle[1],
			new RTHandle[2],
			new RTHandle[3],
			new RTHandle[4],
			new RTHandle[5],
			new RTHandle[6],
			new RTHandle[7],
			new RTHandle[8]
		};

		// Token: 0x04000343 RID: 835
		private static Plane[] s_Planes = new Plane[6];

		// Token: 0x04000344 RID: 836
		private static Vector4[] s_VectorPlanes = new Vector4[6];

		// Token: 0x020000A2 RID: 162
		private static class Profiling
		{
			// Token: 0x04000348 RID: 840
			public static readonly ProfilingSampler setMRTAttachmentsList = new ProfilingSampler("NativeRenderPass SetNativeRenderPassMRTAttachmentList");

			// Token: 0x04000349 RID: 841
			public static readonly ProfilingSampler setAttachmentList = new ProfilingSampler("NativeRenderPass SetNativeRenderPassAttachmentList");

			// Token: 0x0400034A RID: 842
			public static readonly ProfilingSampler execute = new ProfilingSampler("NativeRenderPass ExecuteNativeRenderPass");

			// Token: 0x0400034B RID: 843
			public static readonly ProfilingSampler setupFrameData = new ProfilingSampler("NativeRenderPass SetupNativeRenderPassFrameData");

			// Token: 0x0400034C RID: 844
			private const string k_Name = "ScriptableRenderer";

			// Token: 0x0400034D RID: 845
			public static readonly ProfilingSampler setPerCameraShaderVariables = new ProfilingSampler("ScriptableRenderer.SetPerCameraShaderVariables");

			// Token: 0x0400034E RID: 846
			public static readonly ProfilingSampler sortRenderPasses = new ProfilingSampler("Sort Render Passes");

			// Token: 0x0400034F RID: 847
			public static readonly ProfilingSampler recordRenderGraph = new ProfilingSampler("On Record Render Graph");

			// Token: 0x04000350 RID: 848
			public static readonly ProfilingSampler setupLights = new ProfilingSampler("ScriptableRenderer.SetupLights");

			// Token: 0x04000351 RID: 849
			public static readonly ProfilingSampler setupCamera = new ProfilingSampler("Setup Camera Properties");

			// Token: 0x04000352 RID: 850
			public static readonly ProfilingSampler vfxProcessCamera = new ProfilingSampler("VFX Process Camera");

			// Token: 0x04000353 RID: 851
			public static readonly ProfilingSampler addRenderPasses = new ProfilingSampler("ScriptableRenderer.AddRenderPasses");

			// Token: 0x04000354 RID: 852
			public static readonly ProfilingSampler setupRenderPasses = new ProfilingSampler("ScriptableRenderer.SetupRenderPasses");

			// Token: 0x04000355 RID: 853
			public static readonly ProfilingSampler clearRenderingState = new ProfilingSampler("ScriptableRenderer.ClearRenderingState");

			// Token: 0x04000356 RID: 854
			public static readonly ProfilingSampler internalStartRendering = new ProfilingSampler("ScriptableRenderer.InternalStartRendering");

			// Token: 0x04000357 RID: 855
			public static readonly ProfilingSampler internalFinishRenderingCommon = new ProfilingSampler("ScriptableRenderer.InternalFinishRenderingCommon");

			// Token: 0x04000358 RID: 856
			public static readonly ProfilingSampler drawGizmos = new ProfilingSampler("DrawGizmos");

			// Token: 0x04000359 RID: 857
			public static readonly ProfilingSampler drawWireOverlay = new ProfilingSampler("DrawWireOverlay");

			// Token: 0x0400035A RID: 858
			internal static readonly ProfilingSampler beginXRRendering = new ProfilingSampler("Begin XR Rendering");

			// Token: 0x0400035B RID: 859
			internal static readonly ProfilingSampler endXRRendering = new ProfilingSampler("End XR Rendering");

			// Token: 0x0400035C RID: 860
			internal static readonly ProfilingSampler initRenderGraphFrame = new ProfilingSampler("Initialize Frame");

			// Token: 0x0400035D RID: 861
			internal static readonly ProfilingSampler setEditorTarget = new ProfilingSampler("Set Editor Target");

			// Token: 0x020000A3 RID: 163
			public static class RenderBlock
			{
				// Token: 0x0400035E RID: 862
				private const string k_Name = "RenderPassBlock";

				// Token: 0x0400035F RID: 863
				public static readonly ProfilingSampler beforeRendering = new ProfilingSampler("RenderPassBlock.BeforeRendering");

				// Token: 0x04000360 RID: 864
				public static readonly ProfilingSampler mainRenderingOpaque = new ProfilingSampler("RenderPassBlock.MainRenderingOpaque");

				// Token: 0x04000361 RID: 865
				public static readonly ProfilingSampler mainRenderingTransparent = new ProfilingSampler("RenderPassBlock.MainRenderingTransparent");

				// Token: 0x04000362 RID: 866
				public static readonly ProfilingSampler afterRendering = new ProfilingSampler("RenderPassBlock.AfterRendering");
			}

			// Token: 0x020000A4 RID: 164
			public static class RenderPass
			{
				// Token: 0x04000363 RID: 867
				private const string k_Name = "ScriptableRenderPass";

				// Token: 0x04000364 RID: 868
				public static readonly ProfilingSampler configure = new ProfilingSampler("ScriptableRenderPass.Configure");

				// Token: 0x04000365 RID: 869
				public static readonly ProfilingSampler setRenderPassAttachments = new ProfilingSampler("ScriptableRenderPass.SetRenderPassAttachments");
			}
		}

		// Token: 0x020000A5 RID: 165
		internal struct RenderPassDescriptor
		{
			// Token: 0x0600043D RID: 1085 RVA: 0x00011A4A File Offset: 0x0000FC4A
			internal RenderPassDescriptor(int width, int height, int sampleCount, int rtID)
			{
				this.w = width;
				this.h = height;
				this.samples = sampleCount;
				this.depthID = rtID;
			}

			// Token: 0x04000366 RID: 870
			internal int w;

			// Token: 0x04000367 RID: 871
			internal int h;

			// Token: 0x04000368 RID: 872
			internal int samples;

			// Token: 0x04000369 RID: 873
			internal int depthID;
		}

		// Token: 0x020000A6 RID: 166
		public class RenderingFeatures
		{
			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x00011A69 File Offset: 0x0000FC69
			// (set) Token: 0x0600043F RID: 1087 RVA: 0x00011A71 File Offset: 0x0000FC71
			[Obsolete("cameraStacking has been deprecated use SupportedCameraRenderTypes() in ScriptableRenderer instead.", true)]
			public bool cameraStacking { get; set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x00011A7A File Offset: 0x0000FC7A
			// (set) Token: 0x06000441 RID: 1089 RVA: 0x00011A82 File Offset: 0x0000FC82
			public bool msaa { get; set; } = true;
		}

		// Token: 0x020000A7 RID: 167
		private static class RenderPassBlock
		{
			// Token: 0x0400036C RID: 876
			public static readonly int BeforeRendering = 0;

			// Token: 0x0400036D RID: 877
			public static readonly int MainRenderingOpaque = 1;

			// Token: 0x0400036E RID: 878
			public static readonly int MainRenderingTransparent = 2;

			// Token: 0x0400036F RID: 879
			public static readonly int AfterRendering = 3;
		}

		// Token: 0x020000A8 RID: 168
		private class VFXProcessCameraPassData
		{
			// Token: 0x04000370 RID: 880
			internal UniversalRenderingData renderingData;

			// Token: 0x04000371 RID: 881
			internal Camera camera;

			// Token: 0x04000372 RID: 882
			internal VFXCameraXRSettings cameraXRSettings;

			// Token: 0x04000373 RID: 883
			internal XRPass xrPass;
		}

		// Token: 0x020000A9 RID: 169
		private class DrawGizmosPassData
		{
			// Token: 0x04000374 RID: 884
			public RendererListHandle gizmoRenderList;
		}

		// Token: 0x020000AA RID: 170
		private class DrawWireOverlayPassData
		{
			// Token: 0x04000375 RID: 885
			public RendererListHandle wireOverlayList;
		}

		// Token: 0x020000AB RID: 171
		private class BeginXRPassData
		{
			// Token: 0x04000376 RID: 886
			internal UniversalCameraData cameraData;
		}

		// Token: 0x020000AC RID: 172
		private class EndXRPassData
		{
			// Token: 0x04000377 RID: 887
			public UniversalCameraData cameraData;
		}

		// Token: 0x020000AD RID: 173
		private class DummyData
		{
		}

		// Token: 0x020000AE RID: 174
		private class PassData
		{
			// Token: 0x04000378 RID: 888
			internal ScriptableRenderer renderer;

			// Token: 0x04000379 RID: 889
			internal UniversalCameraData cameraData;

			// Token: 0x0400037A RID: 890
			internal bool isTargetBackbuffer;

			// Token: 0x0400037B RID: 891
			internal Vector2Int cameraTargetSizeCopy;
		}

		// Token: 0x020000AF RID: 175
		internal struct RenderBlocks : IDisposable
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x00011AB4 File Offset: 0x0000FCB4
			public RenderBlocks(List<ScriptableRenderPass> activeRenderPassQueue)
			{
				this.m_BlockEventLimits = new NativeArray<RenderPassEvent>(4, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockRanges = new NativeArray<int>(this.m_BlockEventLimits.Length + 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockRangeLengths = new NativeArray<int>(this.m_BlockRanges.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.BeforeRendering] = RenderPassEvent.BeforeRenderingPrePasses;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.MainRenderingOpaque] = RenderPassEvent.AfterRenderingOpaques;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.MainRenderingTransparent] = RenderPassEvent.AfterRenderingPostProcessing;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.AfterRendering] = (RenderPassEvent)2147483647;
				this.FillBlockRanges(activeRenderPassQueue);
				this.m_BlockEventLimits.Dispose();
				for (int i = 0; i < this.m_BlockRanges.Length - 1; i++)
				{
					this.m_BlockRangeLengths[i] = this.m_BlockRanges[i + 1] - this.m_BlockRanges[i];
				}
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00011BA6 File Offset: 0x0000FDA6
			public void Dispose()
			{
				this.m_BlockRangeLengths.Dispose();
				this.m_BlockRanges.Dispose();
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00011BC0 File Offset: 0x0000FDC0
			private void FillBlockRanges(List<ScriptableRenderPass> activeRenderPassQueue)
			{
				int currRangeIndex = 0;
				int currRenderPass = 0;
				this.m_BlockRanges[currRangeIndex++] = 0;
				for (int i = 0; i < this.m_BlockEventLimits.Length - 1; i++)
				{
					while (currRenderPass < activeRenderPassQueue.Count && activeRenderPassQueue[currRenderPass].renderPassEvent < this.m_BlockEventLimits[i])
					{
						currRenderPass++;
					}
					this.m_BlockRanges[currRangeIndex++] = currRenderPass;
				}
				this.m_BlockRanges[currRangeIndex] = activeRenderPassQueue.Count;
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x00011C44 File Offset: 0x0000FE44
			public int GetLength(int index)
			{
				return this.m_BlockRangeLengths[index];
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x00011C52 File Offset: 0x0000FE52
			public ScriptableRenderer.RenderBlocks.BlockRange GetRange(int index)
			{
				return new ScriptableRenderer.RenderBlocks.BlockRange(this.m_BlockRanges[index], this.m_BlockRanges[index + 1]);
			}

			// Token: 0x0400037C RID: 892
			private NativeArray<RenderPassEvent> m_BlockEventLimits;

			// Token: 0x0400037D RID: 893
			private NativeArray<int> m_BlockRanges;

			// Token: 0x0400037E RID: 894
			private NativeArray<int> m_BlockRangeLengths;

			// Token: 0x020000B0 RID: 176
			public struct BlockRange : IDisposable
			{
				// Token: 0x06000450 RID: 1104 RVA: 0x00011C73 File Offset: 0x0000FE73
				public BlockRange(int begin, int end)
				{
					this.m_Current = ((begin < end) ? begin : end);
					this.m_End = ((end >= begin) ? end : begin);
					this.m_Current--;
				}

				// Token: 0x06000451 RID: 1105 RVA: 0x00011C9F File Offset: 0x0000FE9F
				public ScriptableRenderer.RenderBlocks.BlockRange GetEnumerator()
				{
					return this;
				}

				// Token: 0x06000452 RID: 1106 RVA: 0x00011CA8 File Offset: 0x0000FEA8
				public bool MoveNext()
				{
					int num = this.m_Current + 1;
					this.m_Current = num;
					return num < this.m_End;
				}

				// Token: 0x17000100 RID: 256
				// (get) Token: 0x06000453 RID: 1107 RVA: 0x00011CCE File Offset: 0x0000FECE
				public int Current
				{
					get
					{
						return this.m_Current;
					}
				}

				// Token: 0x06000454 RID: 1108 RVA: 0x0000217F File Offset: 0x0000037F
				public void Dispose()
				{
				}

				// Token: 0x0400037F RID: 895
				private int m_Current;

				// Token: 0x04000380 RID: 896
				private int m_End;
			}
		}
	}
}

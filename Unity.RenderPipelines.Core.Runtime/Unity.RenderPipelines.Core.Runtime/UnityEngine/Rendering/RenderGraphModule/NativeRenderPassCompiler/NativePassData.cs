using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000298 RID: 664
	internal struct NativePassData
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x00042E64 File Offset: 0x00041064
		public NativePassData(ref PassData pass, CompilerContextData ctx)
		{
			this.firstGraphPass = pass.passId;
			this.lastGraphPass = pass.passId;
			this.numGraphPasses = 1;
			this.firstNativeSubPass = -1;
			this.numNativeSubPasses = 0;
			this.fragments = default(FixedAttachmentArray<PassFragmentData>);
			this.attachments = default(FixedAttachmentArray<NativePassAttachment>);
			this.width = pass.fragmentInfoWidth;
			this.height = pass.fragmentInfoHeight;
			this.volumeDepth = pass.fragmentInfoVolumeDepth;
			this.samples = pass.fragmentInfoSamples;
			this.hasDepth = pass.fragmentInfoHasDepth;
			this.hasFoveatedRasterization = pass.hasFoveatedRasterization;
			this.loadAudit = default(FixedAttachmentArray<LoadAudit>);
			this.storeAudit = default(FixedAttachmentArray<StoreAudit>);
			this.breakAudit = new PassBreakAudit(PassBreakReason.NotOptimized, -1);
			ReadOnlySpan<PassFragmentData> readOnlySpan = pass.Fragments(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref PassFragmentData fragment = ref readOnlySpan[i];
				this.fragments.Add(in fragment);
			}
			readOnlySpan = pass.FragmentInputs(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref PassFragmentData fragment2 = ref readOnlySpan[i];
				this.fragments.Add(in fragment2);
			}
			NativePassData.TryMergeNativeSubPass(ctx, ref this, ref pass);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00042F8B File Offset: 0x0004118B
		public void Clear()
		{
			this.firstGraphPass = 0;
			this.numGraphPasses = 0;
			this.attachments.Clear();
			this.fragments.Clear();
			this.loadAudit.Clear();
			this.storeAudit.Clear();
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00042FC7 File Offset: 0x000411C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsValid()
		{
			return this.numGraphPasses > 0;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00042FD4 File Offset: 0x000411D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<PassData> GraphPasses(CompilerContextData ctx)
		{
			if (this.lastGraphPass - this.firstGraphPass + 1 == this.numGraphPasses)
			{
				return (ref ctx.passData).MakeReadOnlySpan(this.firstGraphPass, this.numGraphPasses);
			}
			PassData[] actualPasses = new PassData[this.numGraphPasses];
			int i = this.firstGraphPass;
			int index = 0;
			while (i < this.lastGraphPass + 1)
			{
				PassData pass = ctx.passData[i];
				if (!pass.culled)
				{
					actualPasses[index++] = pass;
				}
				i++;
			}
			return actualPasses;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00043060 File Offset: 0x00041260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void GetGraphPassNames(CompilerContextData ctx, DynamicArray<Name> dest)
		{
			ReadOnlySpan<PassData> readOnlySpan = this.GraphPasses(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref PassData pass = ref readOnlySpan[i];
				PassData passData = pass;
				Name name = passData.GetName(ctx);
				dest.Add(in name);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000430AC File Offset: 0x000412AC
		public static PassBreakAudit CanMerge(CompilerContextData contextData, int activeNativePassId, int passIdToMerge)
		{
			ref PassData passToMerge = ref contextData.passData.ElementAt(passIdToMerge);
			if (passToMerge.type != RenderGraphPassType.Raster)
			{
				return new PassBreakAudit(PassBreakReason.NonRasterPass, passIdToMerge);
			}
			ref NativePassData nativePass = ref contextData.nativePassData.ElementAt(activeNativePassId);
			FixedAttachmentArray<PassFragmentData> fixedAttachmentArray;
			if (passToMerge.numFragments > 0 || passToMerge.numFragmentInputs > 0)
			{
				if (nativePass.width != passToMerge.fragmentInfoWidth || nativePass.height != passToMerge.fragmentInfoHeight || nativePass.volumeDepth != passToMerge.fragmentInfoVolumeDepth || nativePass.samples != passToMerge.fragmentInfoSamples)
				{
					return new PassBreakAudit(PassBreakReason.TargetSizeMismatch, passIdToMerge);
				}
				if (nativePass.hasDepth && passToMerge.fragmentInfoHasDepth)
				{
					ref PassFragmentData firstFragment = ref contextData.fragmentData.ElementAt(passToMerge.firstFragment);
					fixedAttachmentArray = nativePass.fragments;
					int index = fixedAttachmentArray[0].resource.index;
					ResourceHandle resourceHandle = firstFragment.resource;
					if (index != resourceHandle.index)
					{
						return new PassBreakAudit(PassBreakReason.DifferentDepthTextures, passIdToMerge);
					}
				}
				if (nativePass.hasFoveatedRasterization != passToMerge.hasFoveatedRasterization)
				{
					return new PassBreakAudit(PassBreakReason.FRStateMismatch, passIdToMerge);
				}
			}
			ReadOnlySpan<PassInputData> readOnlySpan = passToMerge.Inputs(contextData);
			int k;
			for (k = 0; k < readOnlySpan.Length; k++)
			{
				ResourceHandle inputResource = readOnlySpan[k].resource;
				int writingPassId = contextData.resources[inputResource].writePassId;
				if (writingPassId >= nativePass.firstGraphPass && writingPassId < nativePass.lastGraphPass + 1 && !passToMerge.IsUsedAsFragment(inputResource, contextData))
				{
					return new PassBreakAudit(PassBreakReason.NextPassReadsTexture, passIdToMerge);
				}
			}
			FixedAttachmentArray<PassFragmentData> attachmentsToTryAdding = default(FixedAttachmentArray<PassFragmentData>);
			int num = 8;
			fixedAttachmentArray = nativePass.fragments;
			int currAvailableAttachmentSlots = num - fixedAttachmentArray.size;
			ReadOnlySpan<PassFragmentData> readOnlySpan2 = passToMerge.Fragments(contextData);
			k = 0;
			while (k < readOnlySpan2.Length)
			{
				readonly ref PassFragmentData fragment = ref readOnlySpan2[k];
				bool alreadyAttached = false;
				int i = 0;
				for (;;)
				{
					int num2 = i;
					fixedAttachmentArray = nativePass.fragments;
					if (num2 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = nativePass.fragments;
					int index2 = fixedAttachmentArray[i].resource.index;
					ResourceHandle resourceHandle = fragment.resource;
					if (index2 == resourceHandle.index)
					{
						goto Block_15;
					}
					i++;
				}
				IL_01E7:
				if (!alreadyAttached)
				{
					if (currAvailableAttachmentSlots == 0)
					{
						return new PassBreakAudit(PassBreakReason.AttachmentLimitReached, passIdToMerge);
					}
					attachmentsToTryAdding.Add(in fragment);
					currAvailableAttachmentSlots--;
				}
				k++;
				continue;
				Block_15:
				alreadyAttached = true;
				goto IL_01E7;
			}
			readOnlySpan2 = passToMerge.FragmentInputs(contextData);
			k = 0;
			while (k < readOnlySpan2.Length)
			{
				readonly ref PassFragmentData fragmentInput = ref readOnlySpan2[k];
				bool alreadyAttached2 = false;
				int j = 0;
				for (;;)
				{
					int num3 = j;
					fixedAttachmentArray = nativePass.fragments;
					if (num3 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = nativePass.fragments;
					int index3 = fixedAttachmentArray[j].resource.index;
					ResourceHandle resourceHandle = fragmentInput.resource;
					if (index3 == resourceHandle.index)
					{
						goto Block_19;
					}
					j++;
				}
				IL_0287:
				if (!alreadyAttached2)
				{
					if (currAvailableAttachmentSlots == 0)
					{
						return new PassBreakAudit(PassBreakReason.AttachmentLimitReached, passIdToMerge);
					}
					attachmentsToTryAdding.Add(in fragmentInput);
					currAvailableAttachmentSlots--;
				}
				k++;
				continue;
				Block_19:
				alreadyAttached2 = true;
				goto IL_0287;
			}
			if (!NativePassData.CanMergeNativeSubPass(contextData, nativePass, passToMerge) && nativePass.numGraphPasses + 1 > 8)
			{
				return new PassBreakAudit(PassBreakReason.SubPassLimitReached, passIdToMerge);
			}
			return new PassBreakAudit(PassBreakReason.Merged, passIdToMerge);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000433A0 File Offset: 0x000415A0
		private static bool CanMergeNativeSubPass(CompilerContextData contextData, NativePassData nativePass, PassData passToMerge)
		{
			if (passToMerge.numFragments == 0 && passToMerge.numFragmentInputs == 0)
			{
				return true;
			}
			if (nativePass.numNativeSubPasses == 0)
			{
				return false;
			}
			ref FixedAttachmentArray<PassFragmentData> fragmentList = ref nativePass.fragments;
			ref SubPassDescriptor lastPass = ref contextData.nativeSubPassData.ElementAt(nativePass.firstNativeSubPass + nativePass.numNativeSubPasses - 1);
			SubPassFlags flags = SubPassFlags.None;
			if (!passToMerge.fragmentInfoHasDepth && nativePass.hasDepth)
			{
				flags = SubPassFlags.ReadOnlyDepth;
			}
			int fragmentIdx = 0;
			int colorOffset = (passToMerge.fragmentInfoHasDepth ? (-1) : 0);
			int num = passToMerge.numFragments + colorOffset;
			AttachmentIndexArray attachmentIndexArray = lastPass.colorOutputs;
			if (num != attachmentIndexArray.Length)
			{
				return false;
			}
			ReadOnlySpan<PassFragmentData> readOnlySpan = passToMerge.Fragments(contextData);
			int i;
			for (i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref PassFragmentData graphPassFragment = ref readOnlySpan[i];
				if (!passToMerge.fragmentInfoHasDepth || fragmentIdx != 0)
				{
					int colorAttachmentIdx = -1;
					int fragmentId = 0;
					for (;;)
					{
						int num2 = fragmentId;
						FixedAttachmentArray<PassFragmentData> fixedAttachmentArray = fragmentList;
						if (num2 >= fixedAttachmentArray.size)
						{
							break;
						}
						fixedAttachmentArray = fragmentList;
						int index = fixedAttachmentArray[fragmentId].resource.index;
						ResourceHandle resourceHandle = graphPassFragment.resource;
						if (index == resourceHandle.index)
						{
							goto Block_11;
						}
						fragmentId++;
					}
					IL_011B:
					if (colorAttachmentIdx >= 0)
					{
						int num3 = colorAttachmentIdx;
						attachmentIndexArray = lastPass.colorOutputs;
						if (num3 == attachmentIndexArray[fragmentIdx + colorOffset])
						{
							goto IL_0139;
						}
					}
					return false;
					Block_11:
					colorAttachmentIdx = fragmentId;
					goto IL_011B;
				}
				flags = (graphPassFragment.accessFlags.HasFlag(AccessFlags.Write) ? SubPassFlags.None : SubPassFlags.ReadOnlyDepth);
				IL_0139:
				fragmentIdx++;
			}
			int inputIndex = 0;
			int numFragmentInputs = passToMerge.numFragmentInputs;
			attachmentIndexArray = lastPass.inputs;
			if (numFragmentInputs != attachmentIndexArray.Length)
			{
				return false;
			}
			readOnlySpan = passToMerge.FragmentInputs(contextData);
			i = 0;
			while (i < readOnlySpan.Length)
			{
				readonly ref PassFragmentData graphFragmentInput = ref readOnlySpan[i];
				int inputAttachmentIdx = -1;
				int fragmentId2 = 0;
				for (;;)
				{
					int num4 = fragmentId2;
					FixedAttachmentArray<PassFragmentData> fixedAttachmentArray = fragmentList;
					if (num4 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = fragmentList;
					int index2 = fixedAttachmentArray[fragmentId2].resource.index;
					ResourceHandle resourceHandle = graphFragmentInput.resource;
					if (index2 == resourceHandle.index)
					{
						goto Block_15;
					}
					fragmentId2++;
				}
				IL_01DE:
				if (inputAttachmentIdx >= 0)
				{
					int num5 = inputAttachmentIdx;
					attachmentIndexArray = lastPass.inputs;
					if (num5 == attachmentIndexArray[inputIndex])
					{
						inputIndex++;
						i++;
						continue;
					}
				}
				return false;
				Block_15:
				inputAttachmentIdx = fragmentId2;
				goto IL_01DE;
			}
			return flags == lastPass.flags;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000435CC File Offset: 0x000417CC
		public static void TryMergeNativeSubPass(CompilerContextData contextData, ref NativePassData nativePass, ref PassData passToMerge)
		{
			ref FixedAttachmentArray<PassFragmentData> fragmentList = ref nativePass.fragments;
			NativePassData nativePassData = nativePass;
			if (nativePassData.numNativeSubPasses == 0)
			{
				FixedAttachmentArray<PassFragmentData> fixedAttachmentArray = nativePassData.fragments;
				if (fixedAttachmentArray.size > 0)
				{
					nativePass.firstNativeSubPass = contextData.nativeSubPassData.Length;
				}
			}
			SubPassDescriptor desc = default(SubPassDescriptor);
			if (passToMerge.numFragments == 0 && passToMerge.numFragmentInputs == 0)
			{
				passToMerge.nativeSubPassIndex = nativePass.numNativeSubPasses - 1;
				passToMerge.beginNativeSubpass = false;
				return;
			}
			if (!passToMerge.fragmentInfoHasDepth && nativePass.hasDepth)
			{
				desc.flags = SubPassFlags.ReadOnlyDepth;
			}
			int fragmentIdx = 0;
			int colorOffset = (passToMerge.fragmentInfoHasDepth ? (-1) : 0);
			desc.colorOutputs = new AttachmentIndexArray(passToMerge.numFragments + colorOffset);
			ReadOnlySpan<PassFragmentData> readOnlySpan = passToMerge.Fragments(contextData);
			int i;
			for (i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref PassFragmentData graphPassFragment = ref readOnlySpan[i];
				if (!passToMerge.fragmentInfoHasDepth || fragmentIdx != 0)
				{
					int colorAttachmentIdx = -1;
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
						int index = fixedAttachmentArray[fragmentId].resource.index;
						ResourceHandle resourceHandle = graphPassFragment.resource;
						if (index == resourceHandle.index)
						{
							goto Block_11;
						}
						fragmentId++;
					}
					IL_0144:
					desc.colorOutputs[fragmentIdx + colorOffset] = colorAttachmentIdx;
					goto IL_0157;
					Block_11:
					colorAttachmentIdx = fragmentId;
					goto IL_0144;
				}
				desc.flags = (graphPassFragment.accessFlags.HasFlag(AccessFlags.Write) ? SubPassFlags.None : SubPassFlags.ReadOnlyDepth);
				IL_0157:
				fragmentIdx++;
			}
			int inputIndex = 0;
			desc.inputs = new AttachmentIndexArray(passToMerge.numFragmentInputs);
			readOnlySpan = passToMerge.FragmentInputs(contextData);
			i = 0;
			while (i < readOnlySpan.Length)
			{
				readonly ref PassFragmentData fragmentInput = ref readOnlySpan[i];
				int inputAttachmentIdx = -1;
				int fragmentId2 = 0;
				for (;;)
				{
					int num2 = fragmentId2;
					FixedAttachmentArray<PassFragmentData> fixedAttachmentArray = fragmentList;
					if (num2 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = fragmentList;
					int index2 = fixedAttachmentArray[fragmentId2].resource.index;
					ResourceHandle resourceHandle = fragmentInput.resource;
					if (index2 == resourceHandle.index)
					{
						goto Block_13;
					}
					fragmentId2++;
				}
				IL_01F1:
				desc.inputs[inputIndex] = inputAttachmentIdx;
				inputIndex++;
				i++;
				continue;
				Block_13:
				inputAttachmentIdx = fragmentId2;
				goto IL_01F1;
			}
			if (nativePass.numNativeSubPasses == 0 || !NativePassCompiler.IsSameNativeSubPass(ref desc, contextData.nativeSubPassData.ElementAt(nativePass.firstNativeSubPass + nativePass.numNativeSubPasses - 1)))
			{
				contextData.nativeSubPassData.Add(in desc);
				(ref contextData.nativeSubPassData).LastIndex<SubPassDescriptor>();
				nativePass.numNativeSubPasses++;
				passToMerge.beginNativeSubpass = true;
			}
			else
			{
				passToMerge.beginNativeSubpass = false;
			}
			passToMerge.nativeSubPassIndex = nativePass.numNativeSubPasses - 1;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00043864 File Offset: 0x00041A64
		private static void UpdateNativeSubPassesAttachments(CompilerContextData contextData, ref NativePassData nativePass)
		{
			int lastVisitedNativeSubpassIdx = -1;
			ref FixedAttachmentArray<PassFragmentData> fragmentList = ref nativePass.fragments;
			int countPasses = nativePass.lastGraphPass - nativePass.firstGraphPass + 1;
			for (int graphPassIdx = 0; graphPassIdx < countPasses - 1; graphPassIdx++)
			{
				ref PassData currGraphPass = ref contextData.passData.ElementAt(nativePass.firstGraphPass + graphPassIdx);
				if (currGraphPass.nativeSubPassIndex + nativePass.firstNativeSubPass != lastVisitedNativeSubpassIdx && !currGraphPass.culled)
				{
					lastVisitedNativeSubpassIdx = currGraphPass.nativeSubPassIndex + nativePass.firstNativeSubPass;
					ref SubPassDescriptor nativeSubPassDescriptor = ref contextData.nativeSubPassData.ElementAt(lastVisitedNativeSubpassIdx);
					if (!currGraphPass.fragmentInfoHasDepth && nativePass.hasDepth)
					{
						nativeSubPassDescriptor.flags = SubPassFlags.ReadOnlyDepth;
					}
					int fragmentIdx = 0;
					int colorOffset = (currGraphPass.fragmentInfoHasDepth ? (-1) : 0);
					nativeSubPassDescriptor.colorOutputs = new AttachmentIndexArray(currGraphPass.numFragments + colorOffset);
					ReadOnlySpan<PassFragmentData> readOnlySpan = currGraphPass.Fragments(contextData);
					for (int i = 0; i < readOnlySpan.Length; i++)
					{
						readonly ref PassFragmentData graphPassFragment = ref readOnlySpan[i];
						if (!currGraphPass.fragmentInfoHasDepth || fragmentIdx != 0)
						{
							int colorAttachmentIdx = -1;
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
								int index = fixedAttachmentArray[fragmentId].resource.index;
								ResourceHandle resource = graphPassFragment.resource;
								if (index == resource.index)
								{
									goto Block_9;
								}
								fragmentId++;
							}
							IL_0157:
							nativeSubPassDescriptor.colorOutputs[fragmentIdx + colorOffset] = colorAttachmentIdx;
							goto IL_016A;
							Block_9:
							colorAttachmentIdx = fragmentId;
							goto IL_0157;
						}
						nativeSubPassDescriptor.flags = (graphPassFragment.accessFlags.HasFlag(AccessFlags.Write) ? SubPassFlags.None : SubPassFlags.ReadOnlyDepth);
						IL_016A:
						fragmentIdx++;
					}
				}
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00043A04 File Offset: 0x00041C04
		public unsafe static PassBreakAudit TryMerge(CompilerContextData contextData, int activeNativePassId, int passIdToMerge)
		{
			PassBreakAudit passBreakAudit = NativePassData.CanMerge(contextData, activeNativePassId, passIdToMerge);
			if (passBreakAudit.reason != PassBreakReason.Merged)
			{
				return passBreakAudit;
			}
			ref PassData passToMerge = ref contextData.passData.ElementAt(passIdToMerge);
			ref NativePassData nativePass = ref contextData.nativePassData.ElementAt(activeNativePassId);
			passToMerge.mergeState = PassMergeState.SubPass;
			if (passToMerge.nativePassIndex >= 0)
			{
				contextData.nativePassData.ElementAt(passToMerge.nativePassIndex).Clear();
			}
			passToMerge.nativePassIndex = activeNativePassId;
			nativePass.numGraphPasses++;
			nativePass.lastGraphPass = passIdToMerge;
			if (!nativePass.hasDepth && passToMerge.fragmentInfoHasDepth)
			{
				nativePass.hasDepth = true;
				ref NativePassData ptr = ref nativePass;
				PassFragmentData passFragmentData = contextData.fragmentData[passToMerge.firstFragment];
				ptr.fragments.Add(in passFragmentData);
				int size = nativePass.fragments.size;
				if (size > 1)
				{
					ref PassFragmentData ptr2 = ref nativePass.fragments[0];
					ref PassFragmentData ptr3 = ref nativePass.fragments[size - 1];
					passFragmentData = *nativePass.fragments[size - 1];
					PassFragmentData passFragmentData2 = *nativePass.fragments[0];
					ptr2 = passFragmentData;
					ptr3 = passFragmentData2;
				}
				NativePassData.UpdateNativeSubPassesAttachments(contextData, ref nativePass);
			}
			ReadOnlySpan<PassFragmentData> readOnlySpan = passToMerge.Fragments(contextData);
			for (int k = 0; k < readOnlySpan.Length; k++)
			{
				readonly ref PassFragmentData newAttach = ref readOnlySpan[k];
				bool alreadyAttached = false;
				for (int i = 0; i < nativePass.fragments.size; i++)
				{
					ref PassFragmentData existingAttach = ref nativePass.fragments[i];
					int index = existingAttach.resource.index;
					ResourceHandle resourceHandle = newAttach.resource;
					if (index == resourceHandle.index)
					{
						existingAttach.accessFlags |= newAttach.accessFlags;
						ref PassFragmentData ptr4 = ref existingAttach;
						resourceHandle = newAttach.resource;
						ptr4.resource.version = resourceHandle.version;
						alreadyAttached = true;
						break;
					}
				}
				if (!alreadyAttached)
				{
					nativePass.fragments.Add(in newAttach);
				}
			}
			readOnlySpan = passToMerge.FragmentInputs(contextData);
			for (int k = 0; k < readOnlySpan.Length; k++)
			{
				readonly ref PassFragmentData newAttach2 = ref readOnlySpan[k];
				bool alreadyAttached2 = false;
				for (int j = 0; j < nativePass.fragments.size; j++)
				{
					ref PassFragmentData existingAttach2 = ref nativePass.fragments[j];
					int index2 = existingAttach2.resource.index;
					ResourceHandle resourceHandle = newAttach2.resource;
					if (index2 == resourceHandle.index)
					{
						existingAttach2.accessFlags |= newAttach2.accessFlags;
						ref PassFragmentData ptr5 = ref existingAttach2;
						resourceHandle = newAttach2.resource;
						ptr5.resource.version = resourceHandle.version;
						alreadyAttached2 = true;
						break;
					}
				}
				if (!alreadyAttached2)
				{
					nativePass.fragments.Add(in newAttach2);
				}
			}
			NativePassData.TryMergeNativeSubPass(contextData, ref nativePass, ref passToMerge);
			NativePassData.SetPassStatesForNativePass(contextData, activeNativePassId);
			return passBreakAudit;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00043CB8 File Offset: 0x00041EB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPassStatesForNativePass(CompilerContextData contextData, int nativePassId)
		{
			ref NativePassData nativePass = ref contextData.nativePassData.ElementAt(nativePassId);
			if (nativePass.numGraphPasses > 1)
			{
				contextData.passData.ElementAt(nativePass.firstGraphPass).mergeState = PassMergeState.Begin;
				int countPasses = nativePass.lastGraphPass - nativePass.firstGraphPass + 1;
				for (int i = 1; i < countPasses; i++)
				{
					int indexPass = nativePass.firstGraphPass + i;
					if (contextData.passData.ElementAt(indexPass).culled)
					{
						contextData.passData.ElementAt(indexPass).mergeState = PassMergeState.None;
					}
					else
					{
						contextData.passData.ElementAt(nativePass.firstGraphPass + i).mergeState = PassMergeState.SubPass;
					}
				}
				contextData.passData.ElementAt(nativePass.lastGraphPass).mergeState = PassMergeState.End;
				return;
			}
			contextData.passData.ElementAt(nativePass.firstGraphPass).mergeState = PassMergeState.None;
		}

		// Token: 0x04000BD1 RID: 3025
		public FixedAttachmentArray<LoadAudit> loadAudit;

		// Token: 0x04000BD2 RID: 3026
		public FixedAttachmentArray<StoreAudit> storeAudit;

		// Token: 0x04000BD3 RID: 3027
		public PassBreakAudit breakAudit;

		// Token: 0x04000BD4 RID: 3028
		public FixedAttachmentArray<PassFragmentData> fragments;

		// Token: 0x04000BD5 RID: 3029
		public FixedAttachmentArray<NativePassAttachment> attachments;

		// Token: 0x04000BD6 RID: 3030
		public int firstGraphPass;

		// Token: 0x04000BD7 RID: 3031
		public int lastGraphPass;

		// Token: 0x04000BD8 RID: 3032
		public int numGraphPasses;

		// Token: 0x04000BD9 RID: 3033
		public int firstNativeSubPass;

		// Token: 0x04000BDA RID: 3034
		public int numNativeSubPasses;

		// Token: 0x04000BDB RID: 3035
		public int width;

		// Token: 0x04000BDC RID: 3036
		public int height;

		// Token: 0x04000BDD RID: 3037
		public int volumeDepth;

		// Token: 0x04000BDE RID: 3038
		public int samples;

		// Token: 0x04000BDF RID: 3039
		public bool hasDepth;

		// Token: 0x04000BE0 RID: 3040
		public bool hasFoveatedRasterization;
	}
}

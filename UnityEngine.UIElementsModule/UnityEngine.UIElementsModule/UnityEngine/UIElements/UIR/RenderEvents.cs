using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000552 RID: 1362
	internal static class RenderEvents
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x000936BC File Offset: 0x000918BC
		internal static void ProcessOnClippingChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool hierarchical = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.ClippingHierarchy) > RenderDataDirtyTypes.None;
			bool flag = hierarchical;
			if (flag)
			{
				stats.recursiveClipUpdates += 1U;
			}
			else
			{
				stats.nonRecursiveClipUpdates += 1U;
			}
			RenderEvents.DepthFirstOnClippingChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, hierarchical, true, false, false, false, renderChain.device, ref stats);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0009371C File Offset: 0x0009191C
		internal static void ProcessOnOpacityChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool hierarchical = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.OpacityHierarchy) > RenderDataDirtyTypes.None;
			stats.recursiveOpacityUpdates += 1U;
			RenderEvents.DepthFirstOnOpacityChanged(renderChain, (ve.hierarchy.parent != null) ? ve.hierarchy.parent.renderChainData.compositeOpacity : 1f, ve, dirtyID, hierarchical, ref stats, false);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00093785 File Offset: 0x00091985
		internal static void ProcessOnColorChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.colorUpdates += 1U;
			RenderEvents.OnColorChanged(renderChain, ve, dirtyID, ref stats);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000937A0 File Offset: 0x000919A0
		internal static void ProcessOnTransformOrSizeChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.recursiveTransformUpdates += 1U;
			RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, renderChain.device, false, false, ref stats);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000937DC File Offset: 0x000919DC
		private static Matrix4x4 GetTransformIDTransformInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.renderChainData.isGroupTransform);
			bool flag = ve.renderChainData.groupTransformAncestor != null;
			Matrix4x4 transform;
			if (flag)
			{
				VisualElement.MultiplyMatrix34(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
			}
			else
			{
				transform = ve.worldTransform;
			}
			transform.m22 = 1f;
			return transform;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0009385C File Offset: 0x00091A5C
		private static Vector4 GetClipRectIDClipInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
			bool flag = ve.renderChainData.groupTransformAncestor == null;
			Rect rect;
			if (flag)
			{
				rect = ve.worldClip;
			}
			else
			{
				rect = ve.worldClipMinusGroup;
				VisualElement.TransformAlignedRect(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ref rect);
			}
			Vector2 min = rect.min;
			Vector2 max = rect.max;
			Vector2 diff = max - min;
			Vector2 mul = new Vector2(1f / (diff.x + 0.0001f), 1f / (diff.y + 0.0001f));
			Vector2 a = 2f * mul;
			Vector2 b = -(min + max) * mul;
			return new Vector4(a.x, a.y, b.x, b.y);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0009394C File Offset: 0x00091B4C
		internal static uint DepthFirstOnChildAdded(RenderChain renderChain, VisualElement parent, VisualElement ve, int index, bool resetState)
		{
			Debug.Assert(ve.panel != null);
			bool isInChain = ve.renderChainData.isInChain;
			uint num;
			if (isInChain)
			{
				num = 0U;
			}
			else
			{
				if (resetState)
				{
					ve.renderChainData = default(RenderChainVEData);
				}
				ve.renderChainData.flags = RenderDataFlags.IsInChain;
				ve.renderChainData.verticesSpace = Matrix4x4.identity;
				ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				ve.renderChainData.colorID = BMPAlloc.Invalid;
				ve.renderChainData.backgroundColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderLeftColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderTopColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderRightColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderBottomColorID = BMPAlloc.Invalid;
				ve.renderChainData.tintColorID = BMPAlloc.Invalid;
				ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
				ve.renderChainData.compositeOpacity = float.MaxValue;
				RenderEvents.UpdateLocalFlipsWinding(ve);
				bool flag = (ve.renderHints & RenderHints.GroupTransform) != RenderHints.None && !renderChain.drawInCameras;
				if (flag)
				{
					ve.renderChainData.flags = ve.renderChainData.flags | RenderDataFlags.IsGroupTransform;
				}
				bool flag2 = parent != null;
				if (flag2)
				{
					bool isGroupTransform = parent.renderChainData.isGroupTransform;
					if (isGroupTransform)
					{
						ve.renderChainData.groupTransformAncestor = parent;
					}
					else
					{
						ve.renderChainData.groupTransformAncestor = parent.renderChainData.groupTransformAncestor;
					}
					ve.renderChainData.hierarchyDepth = parent.renderChainData.hierarchyDepth + 1;
				}
				else
				{
					ve.renderChainData.groupTransformAncestor = null;
					ve.renderChainData.hierarchyDepth = 0;
				}
				renderChain.EnsureFitsDepth(ve.renderChainData.hierarchyDepth);
				bool flag3 = index > 0;
				if (flag3)
				{
					Debug.Assert(parent != null);
					ve.renderChainData.prev = RenderEvents.GetLastDeepestChild(parent.hierarchy[index - 1]);
				}
				else
				{
					ve.renderChainData.prev = parent;
				}
				ve.renderChainData.next = ((ve.renderChainData.prev != null) ? ve.renderChainData.prev.renderChainData.next : null);
				bool flag4 = ve.renderChainData.prev != null;
				if (flag4)
				{
					ve.renderChainData.prev.renderChainData.next = ve;
				}
				bool flag5 = ve.renderChainData.next != null;
				if (flag5)
				{
					ve.renderChainData.next.renderChainData.prev = ve;
				}
				Debug.Assert(!RenderChainVEData.AllocatesID(ve.renderChainData.transformID));
				bool flag6 = RenderEvents.NeedsTransformID(ve);
				if (flag6)
				{
					ve.renderChainData.transformID = renderChain.shaderInfoAllocator.AllocTransform();
				}
				else
				{
					ve.renderChainData.transformID = BMPAlloc.Invalid;
				}
				ve.renderChainData.boneTransformAncestor = null;
				bool flag7 = RenderEvents.NeedsColorID(ve);
				if (flag7)
				{
					RenderEvents.InitColorIDs(renderChain, ve);
					RenderEvents.SetColorValues(renderChain, ve);
				}
				bool flag8 = !RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag8)
				{
					bool flag9 = parent != null && !ve.renderChainData.isGroupTransform;
					if (flag9)
					{
						bool flag10 = RenderChainVEData.AllocatesID(parent.renderChainData.transformID);
						if (flag10)
						{
							ve.renderChainData.boneTransformAncestor = parent;
						}
						else
						{
							ve.renderChainData.boneTransformAncestor = parent.renderChainData.boneTransformAncestor;
						}
						ve.renderChainData.transformID = parent.renderChainData.transformID;
						ve.renderChainData.transformID.ownedState = OwnedState.Inherited;
					}
					else
					{
						ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
					}
				}
				else
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
				}
				int childrenCount = ve.hierarchy.childCount;
				uint deepCount = 0U;
				for (int i = 0; i < childrenCount; i++)
				{
					deepCount += RenderEvents.DepthFirstOnChildAdded(renderChain, ve, ve.hierarchy[i], i, resetState);
				}
				num = 1U + deepCount;
			}
			return num;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00093D84 File Offset: 0x00091F84
		internal static uint DepthFirstOnChildRemoving(RenderChain renderChain, VisualElement ve)
		{
			int childrenCount = ve.hierarchy.childCount - 1;
			uint deepCount = 0U;
			while (childrenCount >= 0)
			{
				deepCount += RenderEvents.DepthFirstOnChildRemoving(renderChain, ve.hierarchy[childrenCount--]);
			}
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				renderChain.ChildWillBeRemoved(ve);
				CommandManipulator.ResetCommands(renderChain, ve);
				renderChain.ResetTextures(ve);
				bool hasExtraData = ve.renderChainData.hasExtraData;
				if (hasExtraData)
				{
					renderChain.FreeExtraMeshes(ve);
					renderChain.FreeExtraData(ve);
				}
				ve.renderChainData.flags = ve.renderChainData.flags & ~RenderDataFlags.IsInChain;
				ve.renderChainData.clipMethod = ClipMethod.Undetermined;
				bool flag = ve.renderChainData.next != null;
				if (flag)
				{
					ve.renderChainData.next.renderChainData.prev = ve.renderChainData.prev;
				}
				bool flag2 = ve.renderChainData.prev != null;
				if (flag2)
				{
					ve.renderChainData.prev.renderChainData.next = ve.renderChainData.next;
				}
				bool flag3 = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
				if (flag3)
				{
					renderChain.shaderInfoAllocator.FreeTextCoreSettings(ve.renderChainData.textCoreSettingsID);
					ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
				}
				bool flag4 = RenderChainVEData.AllocatesID(ve.renderChainData.opacityID);
				if (flag4)
				{
					renderChain.shaderInfoAllocator.FreeOpacity(ve.renderChainData.opacityID);
					ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				}
				bool flag5 = RenderChainVEData.AllocatesID(ve.renderChainData.colorID);
				if (flag5)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.colorID);
					ve.renderChainData.colorID = BMPAlloc.Invalid;
				}
				bool flag6 = RenderChainVEData.AllocatesID(ve.renderChainData.backgroundColorID);
				if (flag6)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.backgroundColorID);
					ve.renderChainData.backgroundColorID = BMPAlloc.Invalid;
				}
				bool flag7 = RenderChainVEData.AllocatesID(ve.renderChainData.borderLeftColorID);
				if (flag7)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderLeftColorID);
					ve.renderChainData.borderLeftColorID = BMPAlloc.Invalid;
				}
				bool flag8 = RenderChainVEData.AllocatesID(ve.renderChainData.borderTopColorID);
				if (flag8)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderTopColorID);
					ve.renderChainData.borderTopColorID = BMPAlloc.Invalid;
				}
				bool flag9 = RenderChainVEData.AllocatesID(ve.renderChainData.borderRightColorID);
				if (flag9)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderRightColorID);
					ve.renderChainData.borderRightColorID = BMPAlloc.Invalid;
				}
				bool flag10 = RenderChainVEData.AllocatesID(ve.renderChainData.borderBottomColorID);
				if (flag10)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderBottomColorID);
					ve.renderChainData.borderBottomColorID = BMPAlloc.Invalid;
				}
				bool flag11 = RenderChainVEData.AllocatesID(ve.renderChainData.tintColorID);
				if (flag11)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.tintColorID);
					ve.renderChainData.tintColorID = BMPAlloc.Invalid;
				}
				bool flag12 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag12)
				{
					renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
					ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				}
				bool flag13 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag13)
				{
					renderChain.shaderInfoAllocator.FreeTransform(ve.renderChainData.transformID);
					ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				}
				ve.renderChainData.boneTransformAncestor = (ve.renderChainData.groupTransformAncestor = null);
				bool flag14 = ve.renderChainData.tailMesh != null;
				if (flag14)
				{
					renderChain.device.Free(ve.renderChainData.tailMesh);
					ve.renderChainData.tailMesh = null;
				}
				bool flag15 = ve.renderChainData.headMesh != null;
				if (flag15)
				{
					renderChain.device.Free(ve.renderChainData.headMesh);
					ve.renderChainData.headMesh = null;
				}
			}
			ve.renderChainData.prev = null;
			ve.renderChainData.next = null;
			return deepCount + 1U;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00094208 File Offset: 0x00092408
		private static void DepthFirstOnClippingChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, bool hierarchical, bool isRootOfChange, bool isPendingHierarchicalRepaint, bool inheritedClipRectIDChanged, bool inheritedMaskingChanged, UIRenderDevice device, ref ChainBuilderStats stats)
		{
			bool upToDate = dirtyID == ve.renderChainData.dirtyID;
			bool flag = upToDate && !inheritedClipRectIDChanged && !inheritedMaskingChanged;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				bool flag2 = !isRootOfChange;
				if (flag2)
				{
					stats.recursiveClipUpdatesExpanded += 1U;
				}
				isPendingHierarchicalRepaint |= (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None;
				bool mustUpdateClipRectID = hierarchical || isRootOfChange || inheritedClipRectIDChanged;
				bool mustUpdateClippingMethod = hierarchical || isRootOfChange;
				bool mustUpdateChildrenMasking = hierarchical || isRootOfChange || inheritedMaskingChanged;
				bool mustRepaintThis = false;
				bool mustRepaintHierarchy = false;
				bool mustProcessSizeChange = false;
				bool mustRecurse = hierarchical;
				ClipMethod oldClippingMethod = ve.renderChainData.clipMethod;
				ClipMethod newClippingMethod = (mustUpdateClippingMethod ? RenderEvents.DetermineSelfClipMethod(renderChain, ve) : oldClippingMethod);
				bool clipRectIDChanged = false;
				bool flag3 = mustUpdateClipRectID;
				if (flag3)
				{
					BMPAlloc newClipRectID = ve.renderChainData.clipRectID;
					bool flag4 = newClippingMethod == ClipMethod.ShaderDiscard;
					if (flag4)
					{
						bool flag5 = !RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag5)
						{
							newClipRectID = renderChain.shaderInfoAllocator.AllocClipRect();
							bool flag6 = !newClipRectID.IsValid();
							if (flag6)
							{
								newClippingMethod = ClipMethod.Scissor;
								newClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
							}
						}
					}
					else
					{
						bool flag7 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag7)
						{
							renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
						}
						bool flag8 = !ve.renderChainData.isGroupTransform;
						if (flag8)
						{
							newClipRectID = ((newClippingMethod != ClipMethod.Scissor && parent != null) ? parent.renderChainData.clipRectID : UIRVEShaderInfoAllocator.infiniteClipRect);
							newClipRectID.ownedState = OwnedState.Inherited;
						}
					}
					clipRectIDChanged = !ve.renderChainData.clipRectID.Equals(newClipRectID);
					Debug.Assert(!ve.renderChainData.isGroupTransform || !clipRectIDChanged);
					ve.renderChainData.clipRectID = newClipRectID;
				}
				bool maskingChanged = false;
				bool flag9 = oldClippingMethod != newClippingMethod;
				if (flag9)
				{
					ve.renderChainData.clipMethod = newClippingMethod;
					bool flag10 = oldClippingMethod == ClipMethod.Stencil || newClippingMethod == ClipMethod.Stencil;
					if (flag10)
					{
						maskingChanged = true;
						mustUpdateChildrenMasking = true;
					}
					bool flag11 = oldClippingMethod == ClipMethod.Scissor || newClippingMethod == ClipMethod.Scissor;
					if (flag11)
					{
						mustRepaintThis = true;
					}
					bool flag12 = newClippingMethod == ClipMethod.ShaderDiscard || (oldClippingMethod == ClipMethod.ShaderDiscard && RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
					if (flag12)
					{
						mustProcessSizeChange = true;
					}
				}
				bool flag13 = clipRectIDChanged;
				if (flag13)
				{
					mustRecurse = true;
					mustRepaintHierarchy = true;
				}
				bool flag14 = mustUpdateChildrenMasking;
				if (flag14)
				{
					int newChildrenMaskDepth = 0;
					int newChildrenStencilRef = 0;
					bool flag15 = parent != null;
					if (flag15)
					{
						newChildrenMaskDepth = parent.renderChainData.childrenMaskDepth;
						newChildrenStencilRef = parent.renderChainData.childrenStencilRef;
						bool flag16 = newClippingMethod == ClipMethod.Stencil;
						if (flag16)
						{
							bool flag17 = newChildrenMaskDepth > newChildrenStencilRef;
							if (flag17)
							{
								newChildrenStencilRef++;
							}
							newChildrenMaskDepth++;
						}
						bool flag18 = (ve.renderHints & RenderHints.MaskContainer) == RenderHints.MaskContainer && newChildrenMaskDepth < 7;
						if (flag18)
						{
							newChildrenStencilRef = newChildrenMaskDepth;
						}
					}
					bool flag19 = ve.renderChainData.childrenMaskDepth != newChildrenMaskDepth || ve.renderChainData.childrenStencilRef != newChildrenStencilRef;
					if (flag19)
					{
						maskingChanged = true;
					}
					ve.renderChainData.childrenMaskDepth = newChildrenMaskDepth;
					ve.renderChainData.childrenStencilRef = newChildrenStencilRef;
				}
				bool flag20 = maskingChanged;
				if (flag20)
				{
					mustRecurse = true;
					mustRepaintHierarchy = true;
				}
				bool flag21 = (mustRepaintThis || mustRepaintHierarchy) && !isPendingHierarchicalRepaint;
				if (flag21)
				{
					renderChain.UIEOnVisualsChanged(ve, mustRepaintHierarchy);
					isPendingHierarchicalRepaint = true;
				}
				bool flag22 = mustProcessSizeChange;
				if (flag22)
				{
					renderChain.UIEOnTransformOrSizeChanged(ve, false, true);
				}
				bool flag23 = mustRecurse;
				if (flag23)
				{
					int childrenCount = ve.hierarchy.childCount;
					for (int i = 0; i < childrenCount; i++)
					{
						RenderEvents.DepthFirstOnClippingChanged(renderChain, ve, ve.hierarchy[i], dirtyID, hierarchical, false, isPendingHierarchicalRepaint, clipRectIDChanged, maskingChanged, device, ref stats);
					}
				}
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000945BC File Offset: 0x000927BC
		private static void DepthFirstOnOpacityChanged(RenderChain renderChain, float parentCompositeOpacity, VisualElement ve, uint dirtyID, bool hierarchical, ref ChainBuilderStats stats, bool isDoingFullVertexRegeneration = false)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				stats.recursiveOpacityUpdatesExpanded += 1U;
				float oldOpacity = ve.renderChainData.compositeOpacity;
				float newOpacity = ve.resolvedStyle.opacity * parentCompositeOpacity;
				bool visiblityTresholdPassed = (oldOpacity < RenderEvents.VisibilityTreshold) ^ (newOpacity < RenderEvents.VisibilityTreshold);
				bool compositeOpacityChanged = Mathf.Abs(oldOpacity - newOpacity) > 0.0001f || visiblityTresholdPassed;
				bool flag2 = compositeOpacityChanged;
				if (flag2)
				{
					ve.renderChainData.compositeOpacity = newOpacity;
				}
				bool changedOpacityID = false;
				bool hasDistinctOpacity = newOpacity < parentCompositeOpacity - 0.0001f;
				bool flag3 = hasDistinctOpacity;
				if (flag3)
				{
					bool flag4 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag4)
					{
						changedOpacityID = true;
						ve.renderChainData.opacityID = renderChain.shaderInfoAllocator.AllocOpacity();
					}
					bool flag5 = (changedOpacityID || compositeOpacityChanged) && ve.renderChainData.opacityID.IsValid();
					if (flag5)
					{
						renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, newOpacity);
					}
				}
				else
				{
					bool flag6 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag6)
					{
						bool flag7 = ve.hierarchy.parent != null && !ve.renderChainData.opacityID.Equals(ve.hierarchy.parent.renderChainData.opacityID);
						if (flag7)
						{
							changedOpacityID = true;
							ve.renderChainData.opacityID = ve.hierarchy.parent.renderChainData.opacityID;
							ve.renderChainData.opacityID.ownedState = OwnedState.Inherited;
						}
					}
					else
					{
						bool flag8 = compositeOpacityChanged && ve.renderChainData.opacityID.IsValid();
						if (flag8)
						{
							renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, newOpacity);
						}
					}
				}
				if (!isDoingFullVertexRegeneration)
				{
					bool flag9 = changedOpacityID && (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Visuals) == RenderDataDirtyTypes.None && (ve.renderChainData.headMesh != null || ve.renderChainData.tailMesh != null);
					if (flag9)
					{
						renderChain.UIEOnOpacityIdChanged(ve);
					}
				}
				bool flag10 = compositeOpacityChanged || changedOpacityID || hierarchical;
				if (flag10)
				{
					int childrenCount = ve.hierarchy.childCount;
					for (int i = 0; i < childrenCount; i++)
					{
						RenderEvents.DepthFirstOnOpacityChanged(renderChain, newOpacity, ve.hierarchy[i], dirtyID, hierarchical, ref stats, isDoingFullVertexRegeneration);
					}
				}
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00094858 File Offset: 0x00092A58
		private static void OnColorChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				stats.colorUpdatesExpanded += 1U;
				Color newColor = ve.resolvedStyle.backgroundColor;
				bool flag2 = ve.renderChainData.backgroundAlpha == 0f && newColor.a > 0f;
				if (flag2)
				{
					renderChain.UIEOnVisualsChanged(ve, false);
				}
				ve.renderChainData.backgroundAlpha = newColor.a;
				bool shouldUpdateVisuals = false;
				bool flag3 = (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor && !ve.renderChainData.isIgnoringDynamicColorHint;
				if (flag3)
				{
					bool flag4 = RenderEvents.InitColorIDs(renderChain, ve);
					if (flag4)
					{
						shouldUpdateVisuals = true;
					}
					RenderEvents.SetColorValues(renderChain, ve);
					bool flag5 = ve is TextElement && !RenderEvents.UpdateTextCoreSettings(renderChain, ve);
					if (flag5)
					{
						shouldUpdateVisuals = true;
					}
				}
				else
				{
					shouldUpdateVisuals = true;
				}
				bool flag6 = shouldUpdateVisuals;
				if (flag6)
				{
					renderChain.UIEOnVisualsChanged(ve, false);
				}
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00094950 File Offset: 0x00092B50
		private static void DepthFirstOnTransformOrSizeChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, UIRenderDevice device, bool isAncestorOfChangeSkinned, bool transformChanged, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				stats.recursiveTransformUpdatesExpanded += 1U;
				transformChanged |= (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Transform) > RenderDataDirtyTypes.None;
				bool flag2 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag2)
				{
					renderChain.shaderInfoAllocator.SetClipRectValue(ve.renderChainData.clipRectID, RenderEvents.GetClipRectIDClipInfo(ve));
				}
				bool flag3 = transformChanged && RenderEvents.UpdateLocalFlipsWinding(ve);
				if (flag3)
				{
					renderChain.UIEOnVisualsChanged(ve, true);
				}
				bool flag4 = transformChanged;
				if (flag4)
				{
					RenderEvents.UpdateZeroScaling(ve);
				}
				bool dirtyHasBeenResolved = true;
				bool flag5 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag5)
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
					isAncestorOfChangeSkinned = true;
					stats.boneTransformed += 1U;
				}
				else
				{
					bool flag6 = !transformChanged;
					if (!flag6)
					{
						bool isGroupTransform = ve.renderChainData.isGroupTransform;
						if (isGroupTransform)
						{
							stats.groupTransformElementsChanged += 1U;
						}
						else
						{
							bool flag7 = isAncestorOfChangeSkinned;
							if (flag7)
							{
								Debug.Assert(RenderChainVEData.InheritsID(ve.renderChainData.transformID));
								dirtyHasBeenResolved = false;
								stats.skipTransformed += 1U;
							}
							else
							{
								bool flag8 = (ve.renderChainData.dirtiedValues & (RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy)) == RenderDataDirtyTypes.None && (ve.renderChainData.headMesh != null || ve.renderChainData.tailMesh != null);
								if (flag8)
								{
									bool flag9 = RenderEvents.NudgeVerticesToNewSpace(ve, renderChain, device);
									if (flag9)
									{
										stats.nudgeTransformed += 1U;
									}
									else
									{
										renderChain.UIEOnVisualsChanged(ve, false);
										stats.visualUpdateTransformed += 1U;
									}
								}
							}
						}
					}
				}
				bool flag10 = dirtyHasBeenResolved;
				if (flag10)
				{
					ve.renderChainData.dirtyID = dirtyID;
				}
				bool drawInCameras = renderChain.drawInCameras;
				if (drawInCameras)
				{
					ve.EnsureWorldTransformAndClipUpToDate();
				}
				bool flag11 = !ve.renderChainData.isGroupTransform;
				if (flag11)
				{
					int childrenCount = ve.hierarchy.childCount;
					for (int i = 0; i < childrenCount; i++)
					{
						RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve, ve.hierarchy[i], dirtyID, device, isAncestorOfChangeSkinned, transformChanged, ref stats);
					}
				}
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00094B98 File Offset: 0x00092D98
		public static bool UpdateTextCoreSettings(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve == null || !TextUtilities.IsFontAssigned(ve);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool allocatesID = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
				TextCoreSettings settings = TextUtilities.GetTextCoreSettingsForElement(ve, false);
				bool useDefaultColor = !RenderEvents.NeedsColorID(ve);
				bool flag3 = useDefaultColor && !RenderEvents.NeedsTextCoreSettings(ve) && !allocatesID;
				if (flag3)
				{
					ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
					flag2 = true;
				}
				else
				{
					bool flag4 = !allocatesID;
					if (flag4)
					{
						ve.renderChainData.textCoreSettingsID = renderChain.shaderInfoAllocator.AllocTextCoreSettings(settings);
					}
					bool flag5 = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
					if (flag5)
					{
						bool flag6 = ve.panel.contextType == ContextType.Editor;
						if (flag6)
						{
							Color playModeTintColor = ve.playModeTintColor;
							settings.faceColor *= playModeTintColor;
							settings.outlineColor *= playModeTintColor;
							settings.underlayColor *= playModeTintColor;
						}
						renderChain.shaderInfoAllocator.SetTextCoreSettingValue(ve.renderChainData.textCoreSettingsID, settings);
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00094CDC File Offset: 0x00092EDC
		private static bool NudgeVerticesToNewSpace(VisualElement ve, RenderChain renderChain, UIRenderDevice device)
		{
			Matrix4x4 newTransform;
			UIRUtility.GetVerticesTransformInfo(ve, out newTransform);
			Matrix4x4 nudgeTransform = newTransform * ve.renderChainData.verticesSpace.inverse;
			Matrix4x4 reconstructedNewTransform = nudgeTransform * ve.renderChainData.verticesSpace;
			float error = Mathf.Abs(newTransform.m00 - reconstructedNewTransform.m00);
			error += Mathf.Abs(newTransform.m01 - reconstructedNewTransform.m01);
			error += Mathf.Abs(newTransform.m02 - reconstructedNewTransform.m02);
			error += Mathf.Abs(newTransform.m03 - reconstructedNewTransform.m03);
			error += Mathf.Abs(newTransform.m10 - reconstructedNewTransform.m10);
			error += Mathf.Abs(newTransform.m11 - reconstructedNewTransform.m11);
			error += Mathf.Abs(newTransform.m12 - reconstructedNewTransform.m12);
			error += Mathf.Abs(newTransform.m13 - reconstructedNewTransform.m13);
			error += Mathf.Abs(newTransform.m20 - reconstructedNewTransform.m20);
			error += Mathf.Abs(newTransform.m21 - reconstructedNewTransform.m21);
			error += Mathf.Abs(newTransform.m22 - reconstructedNewTransform.m22);
			error += Mathf.Abs(newTransform.m23 - reconstructedNewTransform.m23);
			bool flag = error > 0.0001f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				ve.renderChainData.verticesSpace = newTransform;
				NudgeJobData job = new NudgeJobData
				{
					transform = nudgeTransform
				};
				bool flag3 = ve.renderChainData.headMesh != null;
				if (flag3)
				{
					RenderEvents.PrepareNudgeVertices(ve, device, ve.renderChainData.headMesh, out job.headSrc, out job.headDst, out job.headCount);
				}
				bool flag4 = ve.renderChainData.tailMesh != null;
				if (flag4)
				{
					RenderEvents.PrepareNudgeVertices(ve, device, ve.renderChainData.tailMesh, out job.tailSrc, out job.tailDst, out job.tailCount);
				}
				renderChain.jobManager.Add(ref job);
				bool hasExtraMeshes = ve.renderChainData.hasExtraMeshes;
				if (hasExtraMeshes)
				{
					ExtraRenderChainVEData extraData = renderChain.GetOrAddExtraData(ve);
					for (BasicNode<MeshHandle> extraMesh = extraData.extraMesh; extraMesh != null; extraMesh = extraMesh.next)
					{
						NudgeJobData extraJob = new NudgeJobData
						{
							transform = job.transform
						};
						RenderEvents.PrepareNudgeVertices(ve, device, extraMesh.data, out extraJob.headSrc, out extraJob.headDst, out extraJob.headCount);
						renderChain.jobManager.Add(ref extraJob);
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00094F64 File Offset: 0x00093164
		private static void PrepareNudgeVertices(VisualElement ve, UIRenderDevice device, MeshHandle mesh, out IntPtr src, out IntPtr dst, out int count)
		{
			int vertCount = (int)mesh.allocVerts.size;
			NativeSlice<Vertex> oldVerts = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, vertCount);
			NativeSlice<Vertex> newVerts;
			device.Update(mesh, (uint)vertCount, out newVerts);
			src = (IntPtr)oldVerts.GetUnsafePtr<Vertex>();
			dst = (IntPtr)newVerts.GetUnsafePtr<Vertex>();
			count = vertCount;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00094FCC File Offset: 0x000931CC
		private static VisualElement GetLastDeepestChild(VisualElement ve)
		{
			for (int childCount = ve.hierarchy.childCount; childCount > 0; childCount = ve.hierarchy.childCount)
			{
				ve = ve.hierarchy[childCount - 1];
			}
			return ve;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0009501C File Offset: 0x0009321C
		private static ClipMethod DetermineSelfClipMethod(RenderChain renderChain, VisualElement ve)
		{
			bool flag = !renderChain.isFlat;
			ClipMethod clipMethod;
			if (flag)
			{
				clipMethod = ClipMethod.NotClipped;
			}
			else
			{
				bool flag2 = !ve.ShouldClip();
				if (flag2)
				{
					clipMethod = ClipMethod.NotClipped;
				}
				else
				{
					ClipMethod rectClipMethod = ((ve.renderChainData.isGroupTransform || (ve.renderHints & RenderHints.ClipWithScissors) > RenderHints.None) ? ClipMethod.Scissor : ClipMethod.ShaderDiscard);
					bool flag3 = !renderChain.elementBuilder.RequiresStencilMask(ve);
					if (flag3)
					{
						clipMethod = rectClipMethod;
					}
					else
					{
						int inheritedMaskDepth = 0;
						VisualElement parent = ve.hierarchy.parent;
						bool flag4 = parent != null;
						if (flag4)
						{
							inheritedMaskDepth = parent.renderChainData.childrenMaskDepth;
						}
						bool flag5 = inheritedMaskDepth == 7;
						if (flag5)
						{
							clipMethod = rectClipMethod;
						}
						else
						{
							clipMethod = ClipMethod.Stencil;
						}
					}
				}
			}
			return clipMethod;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000950D4 File Offset: 0x000932D4
		private static bool UpdateLocalFlipsWinding(VisualElement ve)
		{
			bool flag = !ve.elementPanel.isFlat;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool oldFlipsWinding = ve.renderChainData.localFlipsWinding;
				Vector3 scale = ve.transform.scale;
				float winding = scale.x * scale.y;
				bool flag3 = Math.Abs(winding) < 0.001f;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool newFlipsWinding = winding < 0f;
					bool flag4 = oldFlipsWinding != newFlipsWinding;
					if (flag4)
					{
						ve.renderChainData.localFlipsWinding = newFlipsWinding;
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0009516C File Offset: 0x0009336C
		private static void UpdateZeroScaling(VisualElement ve)
		{
			bool transformScaleZero = Math.Abs(ve.transform.scale.x * ve.transform.scale.y) < 0.001f;
			ve.renderChainData.localTransformScaleZero = transformScaleZero;
			bool parentTransformScaleZero = false;
			VisualElement parent = ve.hierarchy.parent;
			bool flag = parent != null;
			if (flag)
			{
				parentTransformScaleZero = parent.renderChainData.worldTransformScaleZero;
			}
			ve.renderChainData.worldTransformScaleZero = parentTransformScaleZero || transformScaleZero;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000951EC File Offset: 0x000933EC
		private static bool NeedsTransformID(VisualElement ve)
		{
			return !ve.renderChainData.isGroupTransform && (ve.renderHints & RenderHints.BoneTransform) > RenderHints.None;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0009521C File Offset: 0x0009341C
		internal static bool NeedsColorID(VisualElement ve)
		{
			return (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0009523C File Offset: 0x0009343C
		internal static bool NeedsTextCoreSettings(VisualElement ve)
		{
			TextCoreSettings settings = TextUtilities.GetTextCoreSettingsForElement(ve, true);
			return settings.outlineWidth != 0f || settings.underlayOffset != Vector2.zero || settings.underlaySoftness != 0f;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00095294 File Offset: 0x00093494
		private static bool InitColorIDs(RenderChain renderChain, VisualElement ve)
		{
			IResolvedStyle style = ve.resolvedStyle;
			bool hasAllocated = false;
			bool flag = !ve.renderChainData.colorID.IsValid() && style.color != Color.white;
			if (flag)
			{
				ve.renderChainData.colorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag2 = !ve.renderChainData.backgroundColorID.IsValid() && style.backgroundColor != Color.clear;
			if (flag2)
			{
				ve.renderChainData.backgroundColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag3 = !ve.renderChainData.borderLeftColorID.IsValid() && style.borderLeftWidth > 0f;
			if (flag3)
			{
				ve.renderChainData.borderLeftColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag4 = !ve.renderChainData.borderTopColorID.IsValid() && style.borderTopWidth > 0f;
			if (flag4)
			{
				ve.renderChainData.borderTopColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag5 = !ve.renderChainData.borderRightColorID.IsValid() && style.borderRightWidth > 0f;
			if (flag5)
			{
				ve.renderChainData.borderRightColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag6 = !ve.renderChainData.borderBottomColorID.IsValid() && style.borderBottomWidth > 0f;
			if (flag6)
			{
				ve.renderChainData.borderBottomColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			bool flag7 = !ve.renderChainData.tintColorID.IsValid() && style.unityBackgroundImageTintColor != Color.white;
			if (flag7)
			{
				ve.renderChainData.tintColorID = renderChain.shaderInfoAllocator.AllocColor();
				hasAllocated = true;
			}
			return hasAllocated;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x00095488 File Offset: 0x00093688
		public static void SetColorValues(RenderChain renderChain, VisualElement ve)
		{
			IResolvedStyle style = ve.resolvedStyle;
			bool flag = ve.renderChainData.colorID.IsValid();
			if (flag)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.colorID, style.color);
			}
			bool flag2 = ve.renderChainData.backgroundColorID.IsValid();
			if (flag2)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.backgroundColorID, style.backgroundColor);
			}
			bool flag3 = ve.renderChainData.borderLeftColorID.IsValid();
			if (flag3)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderLeftColorID, style.borderLeftColor);
			}
			bool flag4 = ve.renderChainData.borderTopColorID.IsValid();
			if (flag4)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderTopColorID, style.borderTopColor);
			}
			bool flag5 = ve.renderChainData.borderRightColorID.IsValid();
			if (flag5)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderRightColorID, style.borderRightColor);
			}
			bool flag6 = ve.renderChainData.borderBottomColorID.IsValid();
			if (flag6)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderBottomColorID, style.borderBottomColor);
			}
			bool flag7 = ve.renderChainData.tintColorID.IsValid();
			if (flag7)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.tintColorID, style.unityBackgroundImageTintColor);
			}
		}

		// Token: 0x040012CF RID: 4815
		private static readonly ProfilerMarker k_NudgeVerticesMarker = new ProfilerMarker("UIR.NudgeVertices");

		// Token: 0x040012D0 RID: 4816
		private static readonly float VisibilityTreshold = 1E-30f;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000ED RID: 237
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Renderer.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public class Renderer : Component
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		public Bounds bounds
		{
			[FreeFunction(Name = "RendererScripting::GetWorldBounds", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Renderer.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000129 RID: 297
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0000D410 File Offset: 0x0000B610
		public Bounds localBounds
		{
			[NativeName("SetLocalAABB")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_localBounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000D434 File Offset: 0x0000B634
		[FreeFunction(Name = "RendererScripting::GetMaterial", HasExplicitThis = true)]
		private Material GetMaterial()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Material>(Renderer.GetMaterial_Injected(intPtr));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000D45C File Offset: 0x0000B65C
		[FreeFunction(Name = "RendererScripting::GetSharedMaterial", HasExplicitThis = true)]
		private Material GetSharedMaterial()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Material>(Renderer.GetSharedMaterial_Injected(intPtr));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000D484 File Offset: 0x0000B684
		[FreeFunction(Name = "RendererScripting::SetMaterial", HasExplicitThis = true)]
		private void SetMaterial(Material m)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Renderer.SetMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(m));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		[FreeFunction(Name = "RendererScripting::GetMaterialArray", HasExplicitThis = true)]
		private Material[] GetMaterialArray()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Renderer.GetMaterialArray_Injected(intPtr);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		[FreeFunction(Name = "RendererScripting::SetMaterialArray", HasExplicitThis = true)]
		private void SetMaterialArray([NotNull] Material[] m, int length)
		{
			if (m == null)
			{
				ThrowHelper.ThrowArgumentNullException(m, "m");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Renderer.SetMaterialArray_Injected(intPtr, m, length);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000D503 File Offset: 0x0000B703
		private void SetMaterialArray(Material[] m)
		{
			this.SetMaterialArray(m, (m != null) ? m.Length : 0);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000D518 File Offset: 0x0000B718
		[FreeFunction(Name = "RendererScripting::SetPropertyBlock", HasExplicitThis = true)]
		internal void Internal_SetPropertyBlock(MaterialPropertyBlock properties)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Renderer.Internal_SetPropertyBlock_Injected(intPtr, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000D54C File Offset: 0x0000B74C
		[FreeFunction(Name = "RendererScripting::GetPropertyBlock", HasExplicitThis = true)]
		internal void Internal_GetPropertyBlock([NotNull] MaterialPropertyBlock dest)
		{
			if (dest == null)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(dest);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			Renderer.Internal_GetPropertyBlock_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000D594 File Offset: 0x0000B794
		[FreeFunction(Name = "RendererScripting::SetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		internal void Internal_SetPropertyBlockMaterialIndex(MaterialPropertyBlock properties, int materialIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Renderer.Internal_SetPropertyBlockMaterialIndex_Injected(intPtr, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties), materialIndex);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		[FreeFunction(Name = "RendererScripting::GetPropertyBlockMaterialIndex", HasExplicitThis = true)]
		internal void Internal_GetPropertyBlockMaterialIndex([NotNull] MaterialPropertyBlock dest, int materialIndex)
		{
			if (dest == null)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(dest);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(dest, "dest");
			}
			Renderer.Internal_GetPropertyBlockMaterialIndex_Injected(intPtr, intPtr2, materialIndex);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000D610 File Offset: 0x0000B810
		[FreeFunction(Name = "RendererScripting::HasPropertyBlock", HasExplicitThis = true)]
		public bool HasPropertyBlock()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Renderer.HasPropertyBlock_Injected(intPtr);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000D632 File Offset: 0x0000B832
		public void SetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_SetPropertyBlock(properties);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000D63D File Offset: 0x0000B83D
		public void SetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_SetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000D649 File Offset: 0x0000B849
		public void GetPropertyBlock(MaterialPropertyBlock properties)
		{
			this.Internal_GetPropertyBlock(properties);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000D654 File Offset: 0x0000B854
		public void GetPropertyBlock(MaterialPropertyBlock properties, int materialIndex)
		{
			this.Internal_GetPropertyBlockMaterialIndex(properties, materialIndex);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000D660 File Offset: 0x0000B860
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0000D684 File Offset: 0x0000B884
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public bool isVisible
		{
			[NativeName("IsVisibleInScene")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_isVisible_Injected(intPtr);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public ShadowCastingMode shadowCastingMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_shadowCastingMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_shadowCastingMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000D714 File Offset: 0x0000B914
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000D738 File Offset: 0x0000B938
		public bool receiveShadows
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_receiveShadows_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_receiveShadows_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0000D75C File Offset: 0x0000B95C
		internal bool allowGPUDrivenRendering
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_allowGPUDrivenRendering_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000D780 File Offset: 0x0000B980
		internal bool smallMeshCulling
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_smallMeshCulling_Injected(intPtr, value);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public MotionVectorGenerationMode motionVectorGenerationMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_motionVectorGenerationMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_motionVectorGenerationMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000D810 File Offset: 0x0000BA10
		public LightProbeUsage lightProbeUsage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_lightProbeUsage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_lightProbeUsage_Injected(intPtr, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000D834 File Offset: 0x0000BA34
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000D858 File Offset: 0x0000BA58
		public ReflectionProbeUsage reflectionProbeUsage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_reflectionProbeUsage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_reflectionProbeUsage_Injected(intPtr, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public unsafe string sortingLayerName
		{
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Renderer.set_sortingLayerName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0000D904 File Offset: 0x0000BB04
		public int sortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_sortingLayerID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_sortingLayerID_Injected(intPtr, value);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000D928 File Offset: 0x0000BB28
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public int sortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_sortingOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_sortingOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000D970 File Offset: 0x0000BB70
		internal int sortingGroupID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_sortingGroupID_Injected(intPtr);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000D994 File Offset: 0x0000BB94
		internal int sortingGroupOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Renderer.get_sortingGroupOrder_Injected(intPtr);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public Transform probeAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(Renderer.get_probeAnchor_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Renderer.set_probeAnchor_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000DA08 File Offset: 0x0000BC08
		[NativeName("GetMaterialArray")]
		private Material[] GetSharedMaterialArray()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Renderer.GetSharedMaterialArray_Injected(intPtr);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public Material[] materials
		{
			get
			{
				return this.GetMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000DA50 File Offset: 0x0000BC50
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public Material material
		{
			get
			{
				return this.GetMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000DA74 File Offset: 0x0000BC74
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public Material sharedMaterial
		{
			get
			{
				return this.GetSharedMaterial();
			}
			set
			{
				this.SetMaterial(value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public Material[] sharedMaterials
		{
			get
			{
				return this.GetSharedMaterialArray();
			}
			set
			{
				this.SetMaterialArray(value);
			}
		}

		// Token: 0x06000691 RID: 1681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000692 RID: 1682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localBounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x06000693 RID: 1683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMaterial_Injected(IntPtr _unity_self);

		// Token: 0x06000694 RID: 1684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSharedMaterial_Injected(IntPtr _unity_self);

		// Token: 0x06000695 RID: 1685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterial_Injected(IntPtr _unity_self, IntPtr m);

		// Token: 0x06000696 RID: 1686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Material[] GetMaterialArray_Injected(IntPtr _unity_self);

		// Token: 0x06000697 RID: 1687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialArray_Injected(IntPtr _unity_self, Material[] m, int length);

		// Token: 0x06000698 RID: 1688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetPropertyBlock_Injected(IntPtr _unity_self, IntPtr properties);

		// Token: 0x06000699 RID: 1689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetPropertyBlock_Injected(IntPtr _unity_self, IntPtr dest);

		// Token: 0x0600069A RID: 1690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetPropertyBlockMaterialIndex_Injected(IntPtr _unity_self, IntPtr properties, int materialIndex);

		// Token: 0x0600069B RID: 1691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetPropertyBlockMaterialIndex_Injected(IntPtr _unity_self, IntPtr dest, int materialIndex);

		// Token: 0x0600069C RID: 1692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasPropertyBlock_Injected(IntPtr _unity_self);

		// Token: 0x0600069D RID: 1693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x0600069E RID: 1694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600069F RID: 1695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isVisible_Injected(IntPtr _unity_self);

		// Token: 0x060006A0 RID: 1696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShadowCastingMode get_shadowCastingMode_Injected(IntPtr _unity_self);

		// Token: 0x060006A1 RID: 1697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCastingMode_Injected(IntPtr _unity_self, ShadowCastingMode value);

		// Token: 0x060006A2 RID: 1698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_receiveShadows_Injected(IntPtr _unity_self);

		// Token: 0x060006A3 RID: 1699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_receiveShadows_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006A4 RID: 1700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowGPUDrivenRendering_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006A5 RID: 1701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_smallMeshCulling_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006A6 RID: 1702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MotionVectorGenerationMode get_motionVectorGenerationMode_Injected(IntPtr _unity_self);

		// Token: 0x060006A7 RID: 1703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_motionVectorGenerationMode_Injected(IntPtr _unity_self, MotionVectorGenerationMode value);

		// Token: 0x060006A8 RID: 1704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LightProbeUsage get_lightProbeUsage_Injected(IntPtr _unity_self);

		// Token: 0x060006A9 RID: 1705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightProbeUsage_Injected(IntPtr _unity_self, LightProbeUsage value);

		// Token: 0x060006AA RID: 1706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeUsage get_reflectionProbeUsage_Injected(IntPtr _unity_self);

		// Token: 0x060006AB RID: 1707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflectionProbeUsage_Injected(IntPtr _unity_self, ReflectionProbeUsage value);

		// Token: 0x060006AC RID: 1708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060006AD RID: 1709
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x060006AE RID: 1710
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerID_Injected(IntPtr _unity_self, int value);

		// Token: 0x060006AF RID: 1711
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingOrder_Injected(IntPtr _unity_self);

		// Token: 0x060006B0 RID: 1712
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x060006B1 RID: 1713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingGroupID_Injected(IntPtr _unity_self);

		// Token: 0x060006B2 RID: 1714
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingGroupOrder_Injected(IntPtr _unity_self);

		// Token: 0x060006B3 RID: 1715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_probeAnchor_Injected(IntPtr _unity_self);

		// Token: 0x060006B4 RID: 1716
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_probeAnchor_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060006B5 RID: 1717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Material[] GetSharedMaterialArray_Injected(IntPtr _unity_self);
	}
}

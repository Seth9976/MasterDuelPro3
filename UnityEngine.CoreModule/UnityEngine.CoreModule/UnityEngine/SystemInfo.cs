using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001DB RID: 475
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	[NativeHeader("Runtime/Misc/SystemInfoMemory.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h")]
	public sealed class SystemInfo
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000268E0 File Offset: 0x00024AE0
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.GetOperatingSystem();
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000268F8 File Offset: 0x00024AF8
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.GetOperatingSystemFamily();
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00026910 File Offset: 0x00024B10
		public static string processorType
		{
			get
			{
				return SystemInfo.GetProcessorType();
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00026928 File Offset: 0x00024B28
		public static string deviceName
		{
			get
			{
				return SystemInfo.GetDeviceName();
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00026940 File Offset: 0x00024B40
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.GetDeviceType();
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00026958 File Offset: 0x00024B58
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceName();
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00026970 File Offset: 0x00024B70
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendor();
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00026988 File Offset: 0x00024B88
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendorID();
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000269A0 File Offset: 0x00024BA0
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceType();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000269B8 File Offset: 0x00024BB8
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.GetGraphicsUVStartsAtTop();
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x000269D0 File Offset: 0x00024BD0
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.GetGraphicsShaderLevel();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x000269E8 File Offset: 0x00024BE8
		public static FoveatedRenderingCaps foveatedRenderingCaps
		{
			get
			{
				return SystemInfo.GetFoveatedRenderingCaps();
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00026A00 File Offset: 0x00024C00
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.HasHiddenSurfaceRemovalOnGPU();
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00026A18 File Offset: 0x00024C18
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.SupportsShadows();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00026A30 File Offset: 0x00024C30
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.GetCopyTextureSupport();
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00026A48 File Offset: 0x00024C48
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.SupportsComputeShaders();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00026A60 File Offset: 0x00024C60
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.SupportsRenderTargetArrayIndexFromVertexShader();
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00026A78 File Offset: 0x00024C78
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.SupportsInstancing();
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00026A90 File Offset: 0x00024C90
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.SupportedRenderTargetCount();
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00026AA8 File Offset: 0x00024CA8
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampledTextures();
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00026AC0 File Offset: 0x00024CC0
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.SupportsMultisampleAutoResolve();
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00026AD8 File Offset: 0x00024CD8
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.UsesReversedZBuffer();
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00026AF0 File Offset: 0x00024CF0
		private static bool IsValidEnumValue(Enum value)
		{
			bool flag = !Enum.IsDefined(value.GetType(), value);
			return !flag;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00026B1C File Offset: 0x00024D1C
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.HasRenderTextureNative(format);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00026B54 File Offset: 0x00024D54
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return SystemInfo.SupportsTextureFormatNative(format);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00026B8C File Offset: 0x00024D8C
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.GetMaxTextureSize();
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00026BA4 File Offset: 0x00024DA4
		internal static int maxRenderTextureSize
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				return SystemInfo.GetMaxRenderTextureSize();
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00026BBC File Offset: 0x00024DBC
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.SupportsGPUFence();
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00026BD4 File Offset: 0x00024DD4
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.MaxGraphicsBufferSize();
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00026BEC File Offset: 0x00024DEC
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.UsesLoadStoreActions();
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00026C04 File Offset: 0x00024E04
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.GetHDRDisplaySupportFlags();
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00026C1C File Offset: 0x00024E1C
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.SupportsMultiview();
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00026C34 File Offset: 0x00024E34
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.SupportsStoreAndResolveAction();
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00026C4C File Offset: 0x00024E4C
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.SupportsMultisampleResolveDepth();
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00026C64 File Offset: 0x00024E64
		public static bool supportsMultisampleResolveStencil
		{
			get
			{
				return SystemInfo.SupportsMultisampleResolveStencil();
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00026C7C File Offset: 0x00024E7C
		public static bool supportsIndirectArgumentsBuffer
		{
			get
			{
				return SystemInfo.SupportsIndirectArgumentsBuffer();
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00026C94 File Offset: 0x00024E94
		[FreeFunction("systeminfo::GetOperatingSystem")]
		private static string GetOperatingSystem()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetOperatingSystem_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001226 RID: 4646
		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		// Token: 0x06001227 RID: 4647 RVA: 0x00026CC4 File Offset: 0x00024EC4
		[FreeFunction("systeminfo::GetProcessorType")]
		private static string GetProcessorType()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetProcessorType_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00026CF4 File Offset: 0x00024EF4
		[FreeFunction("systeminfo::GetDeviceName")]
		private static string GetDeviceName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetDeviceName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001229 RID: 4649
		[FreeFunction("systeminfo::GetDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DeviceType GetDeviceType();

		// Token: 0x0600122A RID: 4650 RVA: 0x00026D24 File Offset: 0x00024F24
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		private static string GetGraphicsDeviceName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetGraphicsDeviceName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00026D54 File Offset: 0x00024F54
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		private static string GetGraphicsDeviceVendor()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetGraphicsDeviceVendor_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600122C RID: 4652
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsDeviceVendorID();

		// Token: 0x0600122D RID: 4653
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		// Token: 0x0600122E RID: 4654
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGraphicsUVStartsAtTop();

		// Token: 0x0600122F RID: 4655
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsShaderLevel();

		// Token: 0x06001230 RID: 4656
		[FreeFunction("ScriptingGraphicsCaps::GetFoveatedRenderingCaps")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FoveatedRenderingCaps GetFoveatedRenderingCaps();

		// Token: 0x06001231 RID: 4657
		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		// Token: 0x06001232 RID: 4658
		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsShadows();

		// Token: 0x06001233 RID: 4659
		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		// Token: 0x06001234 RID: 4660
		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsComputeShaders();

		// Token: 0x06001235 RID: 4661
		[FreeFunction("ScriptingGraphicsCaps::SupportsRenderTargetArrayIndexFromVertexShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRenderTargetArrayIndexFromVertexShader();

		// Token: 0x06001236 RID: 4662
		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsInstancing();

		// Token: 0x06001237 RID: 4663
		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportedRenderTargetCount();

		// Token: 0x06001238 RID: 4664
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportsMultisampledTextures();

		// Token: 0x06001239 RID: 4665
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleAutoResolve();

		// Token: 0x0600123A RID: 4666
		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesReversedZBuffer();

		// Token: 0x0600123B RID: 4667
		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		// Token: 0x0600123C RID: 4668
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		// Token: 0x0600123D RID: 4669
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxTextureSize();

		// Token: 0x0600123E RID: 4670
		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxRenderTextureSize();

		// Token: 0x0600123F RID: 4671
		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGPUFence();

		// Token: 0x06001240 RID: 4672
		[FreeFunction("ScriptingGraphicsCaps::MaxGraphicsBufferSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long MaxGraphicsBufferSize();

		// Token: 0x06001241 RID: 4673
		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFormatSupported(GraphicsFormat format, GraphicsFormatUsage usage);

		// Token: 0x06001242 RID: 4674
		[FreeFunction("ScriptingGraphicsCaps::GetCompatibleFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetCompatibleFormat(GraphicsFormat format, GraphicsFormatUsage usage);

		// Token: 0x06001243 RID: 4675
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetGraphicsFormat(DefaultFormat format);

		// Token: 0x06001244 RID: 4676 RVA: 0x00026D84 File Offset: 0x00024F84
		[FreeFunction("ScriptingGraphicsCaps::GetRenderTextureSupportedMSAASampleCount")]
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount_Injected(ref desc);
		}

		// Token: 0x06001245 RID: 4677
		[FreeFunction("ScriptingGraphicsCaps::UsesLoadStoreActions")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesLoadStoreActions();

		// Token: 0x06001246 RID: 4678
		[FreeFunction("ScriptingGraphicsCaps::GetHDRDisplaySupportFlags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HDRDisplaySupportFlags GetHDRDisplaySupportFlags();

		// Token: 0x06001247 RID: 4679
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultiview")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultiview();

		// Token: 0x06001248 RID: 4680
		[FreeFunction("ScriptingGraphicsCaps::SupportsStoreAndResolveAction")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsStoreAndResolveAction();

		// Token: 0x06001249 RID: 4681
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleResolveDepth")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleResolveDepth();

		// Token: 0x0600124A RID: 4682
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleResolveStencil")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleResolveStencil();

		// Token: 0x0600124B RID: 4683
		[FreeFunction("ScriptingGraphicsCaps::SupportsIndirectArgumentsBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsIndirectArgumentsBuffer();

		// Token: 0x0600124C RID: 4684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOperatingSystem_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600124D RID: 4685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetProcessorType_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600124E RID: 4686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600124F RID: 4687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsDeviceName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06001250 RID: 4688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsDeviceVendor_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06001251 RID: 4689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRenderTextureSupportedMSAASampleCount_Injected([In] ref RenderTextureDescriptor desc);
	}
}

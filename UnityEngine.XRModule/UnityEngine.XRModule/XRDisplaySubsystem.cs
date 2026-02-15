using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h")]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002580 File Offset: 0x00000780
		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			bool flag = this.displayFocusChanged != null;
			if (flag)
			{
				this.displayFocusChanged(focus);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025A8 File Offset: 0x000007A8
		public float scaleOfAllViewports
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_scaleOfAllViewports_Injected(intPtr);
			}
		}

		// Token: 0x1700000E RID: 14
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000025CC File Offset: 0x000007CC
		public float scaleOfAllRenderTargets
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_scaleOfAllRenderTargets_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025F0 File Offset: 0x000007F0
		public float zNear
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_zNear_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002614 File Offset: 0x00000814
		public float zFar
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_zFar_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002638 File Offset: 0x00000838
		public bool sRGB
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_sRGB_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000265C File Offset: 0x0000085C
		public XRDisplaySubsystem.TextureLayout textureLayout
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_textureLayout_Injected(intPtr, value);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002680 File Offset: 0x00000880
		public void SetMSAALevel(int level)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.SetMSAALevel_Injected(intPtr, level);
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026A4 File Offset: 0x000008A4
		public bool disableLegacyRenderer
		{
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_disableLegacyRenderer_Injected(intPtr, value);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026C8 File Offset: 0x000008C8
		public int GetRenderPassCount()
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetRenderPassCount_Injected(intPtr);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026EC File Offset: 0x000008EC
		public void GetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			bool flag = !this.Internal_TryGetRenderPass(renderPassIndex, out renderPass);
			if (flag)
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002718 File Offset: 0x00000918
		[NativeMethod("TryGetRenderPass")]
		private bool Internal_TryGetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryGetRenderPass_Injected(intPtr, renderPassIndex, out renderPass);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000273C File Offset: 0x0000093C
		public void EndRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryEndRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002774 File Offset: 0x00000974
		[NativeMethod("TryEndRecordingIfLateLatched")]
		private bool Internal_TryEndRecordingIfLateLatched(Camera camera)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryEndRecordingIfLateLatched_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000279C File Offset: 0x0000099C
		public void BeginRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryBeginRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027D4 File Offset: 0x000009D4
		[NativeMethod("TryBeginRecordingIfLateLatched")]
		private bool Internal_TryBeginRecordingIfLateLatched(Camera camera)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryBeginRecordingIfLateLatched_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027FC File Offset: 0x000009FC
		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			bool flag = !this.Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters);
			if (!flag)
			{
				return;
			}
			bool flag2 = camera == null;
			if (flag2)
			{
				throw new ArgumentNullException("camera");
			}
			throw new IndexOutOfRangeException("cullingPassIndex");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002840 File Offset: 0x00000A40
		[NativeMethod("TryGetCullingParams")]
		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
		private bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryGetCullingParams_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera), cullingPassIndex, out scriptableCullingParameters);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000286C File Offset: 0x00000A6C
		[NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		public int GetPreferredMirrorBlitMode()
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetPreferredMirrorBlitMode_Injected(intPtr);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002890 File Offset: 0x00000A90
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "SetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		public void SetPreferredMirrorBlitMode(int blitMode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.SetPreferredMirrorBlitMode_Injected(intPtr, blitMode);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028B4 File Offset: 0x00000AB4
		[NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetMirrorViewBlitDesc_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(mirrorRt), out outDesc, mode);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028E0 File Offset: 0x00000AE0
		[NativeConditional("ENABLE_XR")]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.AddGraphicsThreadMirrorViewBlit_Injected(intPtr, (cmd == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(cmd), allowGraphicsStateInvalidate, mode);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002914 File Offset: 0x00000B14
		public HDROutputSettings hdrOutputSettings
		{
			get
			{
				bool flag = this.m_HDROutputSettings == null;
				if (flag)
				{
					this.m_HDROutputSettings = new HDROutputSettings(-1);
				}
				return this.m_HDROutputSettings;
			}
		}

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_scaleOfAllViewports_Injected(IntPtr _unity_self);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleOfAllRenderTargets_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zNear_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zFar_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sRGB_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureLayout_Injected(IntPtr _unity_self, XRDisplaySubsystem.TextureLayout value);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMSAALevel_Injected(IntPtr _unity_self, int level);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_disableLegacyRenderer_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRenderPassCount_Injected(IntPtr _unity_self);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryGetRenderPass_Injected(IntPtr _unity_self, int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryEndRecordingIfLateLatched_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryBeginRecordingIfLateLatched_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryGetCullingParams_Injected(IntPtr _unity_self, IntPtr camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPreferredMirrorBlitMode_Injected(IntPtr _unity_self);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPreferredMirrorBlitMode_Injected(IntPtr _unity_self, int blitMode);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMirrorViewBlitDesc_Injected(IntPtr _unity_self, IntPtr mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddGraphicsThreadMirrorViewBlit_Injected(IntPtr _unity_self, IntPtr cmd, bool allowGraphicsStateInvalidate, int mode);

		// Token: 0x04000059 RID: 89
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<bool> displayFocusChanged;

		// Token: 0x0400005A RID: 90
		private HDROutputSettings m_HDROutputSettings;

		// Token: 0x02000012 RID: 18
		[Flags]
		public enum TextureLayout
		{
			// Token: 0x0400005C RID: 92
			Texture2DArray = 1,
			// Token: 0x0400005D RID: 93
			SingleTexture2D = 2,
			// Token: 0x0400005E RID: 94
			SeparateTexture2Ds = 4
		}

		// Token: 0x02000013 RID: 19
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			// Token: 0x0400005F RID: 95
			public Matrix4x4 view;

			// Token: 0x04000060 RID: 96
			public Matrix4x4 projection;

			// Token: 0x04000061 RID: 97
			public Rect viewport;

			// Token: 0x04000062 RID: 98
			public Mesh occlusionMesh;

			// Token: 0x04000063 RID: 99
			public int textureArraySlice;

			// Token: 0x04000064 RID: 100
			public Matrix4x4 previousView;

			// Token: 0x04000065 RID: 101
			public bool isPreviousViewValid;
		}

		// Token: 0x02000014 RID: 20
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h")]
		public struct XRRenderPass
		{
			// Token: 0x06000048 RID: 72 RVA: 0x00002950 File Offset: 0x00000B50
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter)
			{
				XRDisplaySubsystem.XRRenderPass.GetRenderParameter_Injected(ref this, Object.MarshalledUnityObject.Marshal<Camera>(camera), renderParameterIndex, out renderParameter);
			}

			// Token: 0x06000049 RID: 73
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern int GetRenderParameterCount();

			// Token: 0x0600004A RID: 74
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRenderParameter_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self, IntPtr camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter);

			// Token: 0x04000066 RID: 102
			private IntPtr displaySubsystemInstance;

			// Token: 0x04000067 RID: 103
			public int renderPassIndex;

			// Token: 0x04000068 RID: 104
			public RenderTargetIdentifier renderTarget;

			// Token: 0x04000069 RID: 105
			public RenderTextureDescriptor renderTargetDesc;

			// Token: 0x0400006A RID: 106
			public bool hasMotionVectorPass;

			// Token: 0x0400006B RID: 107
			public RenderTargetIdentifier motionVectorRenderTarget;

			// Token: 0x0400006C RID: 108
			public RenderTextureDescriptor motionVectorRenderTargetDesc;

			// Token: 0x0400006D RID: 109
			public bool shouldFillOutDepth;

			// Token: 0x0400006E RID: 110
			public int cullingPassIndex;

			// Token: 0x0400006F RID: 111
			public IntPtr foveatedRenderingInfo;
		}

		// Token: 0x02000015 RID: 21
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			// Token: 0x04000070 RID: 112
			public RenderTexture srcTex;

			// Token: 0x04000071 RID: 113
			public int srcTexArraySlice;

			// Token: 0x04000072 RID: 114
			public Rect srcRect;

			// Token: 0x04000073 RID: 115
			public Rect destRect;

			// Token: 0x04000074 RID: 116
			public IntPtr foveatedRenderingInfo;

			// Token: 0x04000075 RID: 117
			public bool srcHdrEncoded;

			// Token: 0x04000076 RID: 118
			public ColorGamut srcHdrColorGamut;

			// Token: 0x04000077 RID: 119
			public int srcHdrMaxLuminance;
		}

		// Token: 0x02000016 RID: 22
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			// Token: 0x0600004B RID: 75
			[NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void GetBlitParameter(int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter);

			// Token: 0x04000078 RID: 120
			private IntPtr displaySubsystemInstance;

			// Token: 0x04000079 RID: 121
			public bool nativeBlitAvailable;

			// Token: 0x0400007A RID: 122
			public bool nativeBlitInvalidStates;

			// Token: 0x0400007B RID: 123
			public int blitParamsCount;
		}

		// Token: 0x02000017 RID: 23
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600004C RID: 76 RVA: 0x0000296B File Offset: 0x00000B6B
			public static IntPtr ConvertToNative(XRDisplaySubsystem xrDisplaySubsystem)
			{
				return xrDisplaySubsystem.m_Ptr;
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[UsedByNativeCode]
	public class RenderTexture : Texture
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00016F24 File Offset: 0x00015124
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x00016F48 File Offset: 0x00015148
		public override int width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_width_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_width_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00016F6C File Offset: 0x0001516C
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x00016F90 File Offset: 0x00015190
		public override int height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00016FB4 File Offset: 0x000151B4
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00016FD8 File Offset: 0x000151D8
		public override TextureDimension dimension
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_dimension_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_dimension_Injected(intPtr, value);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00016FFC File Offset: 0x000151FC
		[NativeName("GetColorFormat")]
		private GraphicsFormat GetColorFormat(bool suppressWarnings)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetColorFormat_Injected(intPtr, suppressWarnings);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00017020 File Offset: 0x00015220
		[NativeName("SetColorFormat")]
		private void SetColorFormat(GraphicsFormat format)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetColorFormat_Injected(intPtr, format);
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00017044 File Offset: 0x00015244
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0001705D File Offset: 0x0001525D
		public new GraphicsFormat graphicsFormat
		{
			get
			{
				return this.GetColorFormat(true);
			}
			set
			{
				this.SetColorFormat(value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00017068 File Offset: 0x00015268
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0001708C File Offset: 0x0001528C
		[NativeProperty("MipMap")]
		public bool useMipMap
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useMipMap_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useMipMap_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000170B0 File Offset: 0x000152B0
		[NativeProperty("SRGBReadWrite")]
		public bool sRGB
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_sRGB_Injected(intPtr);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000170D4 File Offset: 0x000152D4
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000170F8 File Offset: 0x000152F8
		[NativeProperty("VRUsage")]
		public VRTextureUsage vrUsage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_vrUsage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_vrUsage_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001711C File Offset: 0x0001531C
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00017140 File Offset: 0x00015340
		[NativeProperty("Memoryless")]
		public RenderTextureMemoryless memorylessMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_memorylessMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_memorylessMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00017164 File Offset: 0x00015364
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x000171A8 File Offset: 0x000153A8
		public RenderTextureFormat format
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					renderTextureFormat = ((this.GetDescriptor().shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return renderTextureFormat;
			}
			set
			{
				bool flag = value == RenderTextureFormat.Depth || value == RenderTextureFormat.Shadowmap;
				if (flag)
				{
					bool flag2 = this.depthStencilFormat == GraphicsFormat.None;
					if (flag2)
					{
						RenderTexture.WarnAboutFallbackTo16BitsDepth(value);
						this.depthStencilFormat = GraphicsFormat.D16_UNorm;
					}
					bool flag3 = value == RenderTextureFormat.Shadowmap;
					if (flag3)
					{
						this.SetShadowSamplingMode(ShadowSamplingMode.CompareDepths);
					}
				}
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0001720C File Offset: 0x0001540C
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00017230 File Offset: 0x00015430
		public GraphicsFormat stencilFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_stencilFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_stencilFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00017254 File Offset: 0x00015454
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00017278 File Offset: 0x00015478
		public GraphicsFormat depthStencilFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_depthStencilFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_depthStencilFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001729C File Offset: 0x0001549C
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x000172C0 File Offset: 0x000154C0
		public bool autoGenerateMips
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_autoGenerateMips_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_autoGenerateMips_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000172E4 File Offset: 0x000154E4
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00017308 File Offset: 0x00015508
		public int volumeDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_volumeDepth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_volumeDepth_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001732C File Offset: 0x0001552C
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00017350 File Offset: 0x00015550
		public int antiAliasing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_antiAliasing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_antiAliasing_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00017374 File Offset: 0x00015574
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x00017398 File Offset: 0x00015598
		public bool bindTextureMS
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_bindTextureMS_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_bindTextureMS_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000173BC File Offset: 0x000155BC
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000173E0 File Offset: 0x000155E0
		public bool enableRandomWrite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_enableRandomWrite_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_enableRandomWrite_Injected(intPtr, value);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00017404 File Offset: 0x00015604
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00017428 File Offset: 0x00015628
		public bool useDynamicScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useDynamicScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useDynamicScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0001744C File Offset: 0x0001564C
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00017470 File Offset: 0x00015670
		public bool useDynamicScaleExplicit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useDynamicScaleExplicit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useDynamicScaleExplicit_Injected(intPtr, value);
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00017494 File Offset: 0x00015694
		public void ApplyDynamicScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ApplyDynamicScale_Injected(intPtr);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000174B8 File Offset: 0x000156B8
		private bool GetIsPowerOfTwo()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetIsPowerOfTwo_Injected(intPtr);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000174DC File Offset: 0x000156DC
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00003D56 File Offset: 0x00001F56
		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000174F4 File Offset: 0x000156F4
		[FreeFunction("RenderTexture::GetActiveAsRenderTexture")]
		private static RenderTexture GetActive()
		{
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(RenderTexture.GetActive_Injected());
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0001750C File Offset: 0x0001570C
		[FreeFunction("RenderTextureScripting::SetActive")]
		private static void SetActive(RenderTexture rt)
		{
			RenderTexture.SetActive_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00017524 File Offset: 0x00015724
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0001753B File Offset: 0x0001573B
		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00017548 File Offset: 0x00015748
		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderBuffer renderBuffer;
			RenderTexture.GetColorBuffer_Injected(intPtr, out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00017570 File Offset: 0x00015770
		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderBuffer renderBuffer;
			RenderTexture.GetDepthBuffer_Injected(intPtr, out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00017598 File Offset: 0x00015798
		private void SetMipMapCount(int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetMipMapCount_Injected(intPtr, count);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000175BC File Offset: 0x000157BC
		internal void SetShadowSamplingMode(ShadowSamplingMode samplingMode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetShadowSamplingMode_Injected(intPtr, samplingMode);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000175E0 File Offset: 0x000157E0
		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000175F8 File Offset: 0x000157F8
		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00017610 File Offset: 0x00015810
		public IntPtr GetNativeDepthBufferPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetNativeDepthBufferPtr_Injected(intPtr);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00017634 File Offset: 0x00015834
		public void DiscardContents(bool discardColor, bool discardDepth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.DiscardContents_Injected(intPtr, discardColor, discardDepth);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00017658 File Offset: 0x00015858
		[Obsolete("This function has no effect.", false)]
		public void MarkRestoreExpected()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.MarkRestoreExpected_Injected(intPtr);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001767A File Offset: 0x0001587A
		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00017688 File Offset: 0x00015888
		[NativeName("ResolveAntiAliasedSurface")]
		private void ResolveAA()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ResolveAA_Injected(intPtr);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000176AC File Offset: 0x000158AC
		[NativeName("ResolveAntiAliasedSurface")]
		private void ResolveAATo(RenderTexture rt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ResolveAATo_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000176D4 File Offset: 0x000158D4
		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000176DE File Offset: 0x000158DE
		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000176EC File Offset: 0x000158EC
		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		public unsafe void SetGlobalShaderProperty(string propertyName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				RenderTexture.SetGlobalShaderProperty_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00017750 File Offset: 0x00015950
		public bool Create()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.Create_Injected(intPtr);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00017774 File Offset: 0x00015974
		public void Release()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.Release_Injected(intPtr);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00017798 File Offset: 0x00015998
		public bool IsCreated()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.IsCreated_Injected(intPtr);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000177BC File Offset: 0x000159BC
		public void GenerateMips()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.GenerateMips_Injected(intPtr);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000177E0 File Offset: 0x000159E0
		[NativeThrows]
		public void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ConvertToEquirect_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(equirect), eye);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001780C File Offset: 0x00015A0C
		internal void SetSRGBReadWrite(bool srgb)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetSRGBReadWrite_Injected(intPtr, srgb);
		}

		// Token: 0x06000C61 RID: 3169
		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		// Token: 0x06000C62 RID: 3170 RVA: 0x00017830 File Offset: 0x00015A30
		[FreeFunction("RenderTextureSupportsStencil")]
		public static bool SupportsStencil(RenderTexture rt)
		{
			return RenderTexture.SupportsStencil_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00017848 File Offset: 0x00015A48
		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetRenderTextureDescriptor_Injected(intPtr, ref desc);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001786C File Offset: 0x00015A6C
		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTextureDescriptor renderTextureDescriptor;
			RenderTexture.GetDescriptor_Injected(intPtr, out renderTextureDescriptor);
			return renderTextureDescriptor;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00017894 File Offset: 0x00015A94
		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(RenderTexture.GetTemporary_Internal_Injected(ref desc));
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000178B0 File Offset: 0x00015AB0
		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		public static void ReleaseTemporary(RenderTexture temp)
		{
			RenderTexture.ReleaseTemporary_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(temp));
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000178C8 File Offset: 0x00015AC8
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000178EC File Offset: 0x00015AEC
		public int depth
		{
			[FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_depth_Injected(intPtr);
			}
			[FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_depth_Injected(intPtr, value);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001790F File Offset: 0x00015B0F
		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00017919 File Offset: 0x00015B19
		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(ref desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001793C File Offset: 0x00015B3C
		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTextureDescriptor desc = textureToCopy.descriptor;
			RenderTexture.ValidateRenderTextureDesc(ref desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00017988 File Offset: 0x00015B88
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, DefaultFormat format)
			: this(width, height, RenderTexture.GetDefaultColorFormat(format), RenderTexture.GetDefaultDepthStencilFormat(format, depth), Texture.GenerateAllMips)
		{
			bool flag = this != null;
			if (flag)
			{
				this.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(format));
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000179CC File Offset: 0x00015BCC
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000179E0 File Offset: 0x00015BE0
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = format != GraphicsFormat.None && !base.ValidateFormat(format, GraphicsFormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
				this.width = width;
				this.height = height;
				this.graphicsFormat = format;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00017A58 File Offset: 0x00015C58
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			bool flag = colorFormat != GraphicsFormat.None && !base.ValidateFormat(colorFormat, GraphicsFormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depthStencilFormat = depthStencilFormat;
				this.graphicsFormat = colorFormat;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(colorFormat));
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00017AC3 File Offset: 0x00015CC3
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
			: this(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00017AD7 File Offset: 0x00015CD7
		public RenderTexture(int width, int height, int depth, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
		{
			this.Initialize(width, height, depth, format, readWrite, Texture.GenerateAllMips);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00017AF4 File Offset: 0x00015CF4
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00017B08 File Offset: 0x00015D08
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth)
			: this(width, height, depth, RenderTextureFormat.Default)
		{
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00017B16 File Offset: 0x00015D16
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount)
		{
			this.Initialize(width, height, depth, format, RenderTextureReadWrite.Default, mipCount);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00017B30 File Offset: 0x00015D30
		private void Initialize(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite, int mipCount)
		{
			GraphicsFormat colorFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format, false);
			bool flag = colorFormat > GraphicsFormat.None;
			if (flag)
			{
				bool flag2 = !base.ValidateFormat(colorFormat, GraphicsFormatUsage.Render);
				if (flag2)
				{
					return;
				}
			}
			RenderTexture.Internal_Create(this);
			this.width = width;
			this.height = height;
			this.depthStencilFormat = depthStencilFormat;
			this.graphicsFormat = colorFormat;
			this.SetMipMapCount(mipCount);
			this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(colorFormat));
			this.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(format));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00017BBC File Offset: 0x00015DBC
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, GraphicsFormat colorFormat)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, false);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00017BD8 File Offset: 0x00015DD8
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, RenderTextureFormat format, bool disableFallback = false)
		{
			bool flag = !disableFallback && (format == RenderTextureFormat.Depth || format == RenderTextureFormat.Shadowmap) && depthBits < 16;
			if (flag)
			{
				RenderTexture.WarnAboutFallbackTo16BitsDepth(format);
				depthBits = 16;
			}
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == RenderTextureFormat.Shadowmap);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00017C18 File Offset: 0x00015E18
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, DefaultFormat format)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == DefaultFormat.Shadow);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00017C34 File Offset: 0x00015E34
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, ShadowSamplingMode shadowSamplingMode)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, shadowSamplingMode != ShadowSamplingMode.None);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00017C54 File Offset: 0x00015E54
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, bool requestedShadowMap)
		{
			GraphicsFormat format = (requestedShadowMap ? GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0) : GraphicsFormatUtility.GetDepthStencilFormat(depthBits));
			bool flag = depthBits > 16 && format == GraphicsFormat.None && requestedShadowMap;
			GraphicsFormat graphicsFormat;
			if (flag)
			{
				Debug.LogWarning(string.Format("No compatible shadow map depth format with {0} or more depth bits has been found. Changing to a 16 bit depth buffer.", depthBits));
				graphicsFormat = GraphicsFormat.D16_UNorm;
			}
			else
			{
				graphicsFormat = format;
			}
			return graphicsFormat;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00017CA8 File Offset: 0x00015EA8
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(ref value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00017CD4 File Offset: 0x00015ED4
		private static void ValidateRenderTextureDesc(ref RenderTextureDescriptor desc)
		{
			bool flag = desc.graphicsFormat == GraphicsFormat.None && desc.depthStencilFormat == GraphicsFormat.None;
			if (flag)
			{
				RenderTexture.WarnAboutFallbackTo16BitsDepth(desc.colorFormat);
				desc.depthStencilFormat = GraphicsFormat.D16_UNorm;
			}
			bool flag2 = desc.graphicsFormat != GraphicsFormat.None && !SystemInfo.IsFormatSupported(desc.graphicsFormat, GraphicsFormatUsage.Render);
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat.ToString() + " is not supported on this platform.", "desc.graphicsFormat");
			}
			bool flag3 = desc.depthStencilFormat != GraphicsFormat.None && !GraphicsFormatUtility.IsDepthStencilFormat(desc.depthStencilFormat);
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc depthStencilFormat must be a supported depth/stencil GraphicsFormat. " + desc.depthStencilFormat.ToString() + " is not supported on this platform.", "desc.depthStencilFormat");
			}
			bool flag4 = desc.width <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag5 = desc.height <= 0;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag6 = desc.volumeDepth <= 0;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag7 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag7)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag8 = desc.dimension == TextureDimension.CubeArray && desc.volumeDepth % 6 != 0;
			if (flag8)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be a multiple of 6 when dimension is CubeArray", "desc.volumeDepth");
			}
			bool flag9 = GraphicsFormatUtility.IsDepthStencilFormat(desc.graphicsFormat);
			if (flag9)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must not be a depth/stencil format. " + desc.graphicsFormat.ToString() + " is not supported.", "desc.graphicsFormat");
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00017EBC File Offset: 0x000160BC
		internal static GraphicsFormat GetDefaultColorFormat(DefaultFormat format)
		{
			GraphicsFormat graphicsFormat;
			if (format - DefaultFormat.DepthStencil > 1)
			{
				graphicsFormat = SystemInfo.GetGraphicsFormat(format);
			}
			else
			{
				graphicsFormat = GraphicsFormat.None;
			}
			return graphicsFormat;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00017EE4 File Offset: 0x000160E4
		internal static GraphicsFormat GetDefaultDepthStencilFormat(DefaultFormat format, int depth)
		{
			GraphicsFormat graphicsFormat;
			if (format - DefaultFormat.DepthStencil > 1)
			{
				graphicsFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
			}
			else
			{
				graphicsFormat = SystemInfo.GetGraphicsFormat(format);
			}
			return graphicsFormat;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00017F14 File Offset: 0x00016114
		internal static ShadowSamplingMode GetShadowSamplingModeForFormat(RenderTextureFormat format)
		{
			return (format == RenderTextureFormat.Shadowmap) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00017F30 File Offset: 0x00016130
		internal static ShadowSamplingMode GetShadowSamplingModeForFormat(DefaultFormat format)
		{
			return (format == DefaultFormat.Shadow) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00017F4A File Offset: 0x0001614A
		internal static void WarnAboutFallbackTo16BitsDepth(RenderTextureFormat format)
		{
			Debug.LogWarning(string.Format("{0} RenderTexture requested without a depth buffer. Changing to a 16 bit depth buffer. To resolve this warning, please specify the desired number of depth bits when creating the render texture.", format));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00017F64 File Offset: 0x00016164
		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat requestedFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(requestedFormat, GraphicsFormatUsage.Render);
			bool flag = requestedFormat == compatibleFormat;
			GraphicsFormat graphicsFormat;
			if (flag)
			{
				graphicsFormat = requestedFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", requestedFormat.ToString(), compatibleFormat.ToString()));
				graphicsFormat = compatibleFormat;
			}
			return graphicsFormat;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00017FC0 File Offset: 0x000161C0
		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(ref desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00017FEC File Offset: 0x000161EC
		private static RenderTexture GetTemporaryImpl(int width, int height, GraphicsFormat depthStencilFormat, GraphicsFormat colorFormat, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false, ShadowSamplingMode shadowSamplingMode = ShadowSamplingMode.None)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat)
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale,
				shadowSamplingMode = shadowSamplingMode
			});
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00018044 File Offset: 0x00016244
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			ShadowSamplingMode shadowSamplingMode = ShadowSamplingMode.None;
			return RenderTexture.GetTemporaryImpl(width, height, RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, shadowSamplingMode), format, antiAliasing, memorylessMode, vrUsage, useDynamicScale, ShadowSamplingMode.None);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00018070 File Offset: 0x00016270
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00018094 File Offset: 0x00016294
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000180B4 File Offset: 0x000162B4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000180D4 File Offset: 0x000162D4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, 1);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000180F0 File Offset: 0x000162F0
		public static RenderTexture GetTemporary(int width, int height, [UnityEngine.Internal.DefaultValue("0")] int depthBuffer, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			GraphicsFormat graphicsFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format, false);
			ShadowSamplingMode shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(format);
			return RenderTexture.GetTemporaryImpl(width, height, depthStencilFormat, graphicsFormat, antiAliasing, memorylessMode, vrUsage, useDynamicScale, shadowSamplingMode);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00018130 File Offset: 0x00016330
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00018154 File Offset: 0x00016354
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00018178 File Offset: 0x00016378
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00018198 File Offset: 0x00016398
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, 1);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000181B8 File Offset: 0x000163B8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, RenderTextureReadWrite.Default);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000181D4 File Offset: 0x000163D4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, RenderTextureFormat.Default);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000181F0 File Offset: 0x000163F0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporary(width, height, 0);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0001820C File Offset: 0x0001640C
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x00018227 File Offset: 0x00016427
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00018238 File Offset: 0x00016438
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x00018253 File Offset: 0x00016453
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00018264 File Offset: 0x00016464
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00003D56 File Offset: 0x00001F56
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00018278 File Offset: 0x00016478
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		// Token: 0x06000C9A RID: 3226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_width_Injected(IntPtr _unity_self);

		// Token: 0x06000C9B RID: 3227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_width_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000C9C RID: 3228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_height_Injected(IntPtr _unity_self);

		// Token: 0x06000C9D RID: 3229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000C9E RID: 3230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension get_dimension_Injected(IntPtr _unity_self);

		// Token: 0x06000C9F RID: 3231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dimension_Injected(IntPtr _unity_self, TextureDimension value);

		// Token: 0x06000CA0 RID: 3232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetColorFormat_Injected(IntPtr _unity_self, bool suppressWarnings);

		// Token: 0x06000CA1 RID: 3233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorFormat_Injected(IntPtr _unity_self, GraphicsFormat format);

		// Token: 0x06000CA2 RID: 3234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMipMap_Injected(IntPtr _unity_self);

		// Token: 0x06000CA3 RID: 3235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMipMap_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CA4 RID: 3236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sRGB_Injected(IntPtr _unity_self);

		// Token: 0x06000CA5 RID: 3237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VRTextureUsage get_vrUsage_Injected(IntPtr _unity_self);

		// Token: 0x06000CA6 RID: 3238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vrUsage_Injected(IntPtr _unity_self, VRTextureUsage value);

		// Token: 0x06000CA7 RID: 3239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTextureMemoryless get_memorylessMode_Injected(IntPtr _unity_self);

		// Token: 0x06000CA8 RID: 3240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_memorylessMode_Injected(IntPtr _unity_self, RenderTextureMemoryless value);

		// Token: 0x06000CA9 RID: 3241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat get_stencilFormat_Injected(IntPtr _unity_self);

		// Token: 0x06000CAA RID: 3242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stencilFormat_Injected(IntPtr _unity_self, GraphicsFormat value);

		// Token: 0x06000CAB RID: 3243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat get_depthStencilFormat_Injected(IntPtr _unity_self);

		// Token: 0x06000CAC RID: 3244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depthStencilFormat_Injected(IntPtr _unity_self, GraphicsFormat value);

		// Token: 0x06000CAD RID: 3245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoGenerateMips_Injected(IntPtr _unity_self);

		// Token: 0x06000CAE RID: 3246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoGenerateMips_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CAF RID: 3247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_volumeDepth_Injected(IntPtr _unity_self);

		// Token: 0x06000CB0 RID: 3248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volumeDepth_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000CB1 RID: 3249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_antiAliasing_Injected(IntPtr _unity_self);

		// Token: 0x06000CB2 RID: 3250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_antiAliasing_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000CB3 RID: 3251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bindTextureMS_Injected(IntPtr _unity_self);

		// Token: 0x06000CB4 RID: 3252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bindTextureMS_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CB5 RID: 3253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableRandomWrite_Injected(IntPtr _unity_self);

		// Token: 0x06000CB6 RID: 3254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableRandomWrite_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CB7 RID: 3255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDynamicScale_Injected(IntPtr _unity_self);

		// Token: 0x06000CB8 RID: 3256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDynamicScale_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CB9 RID: 3257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDynamicScaleExplicit_Injected(IntPtr _unity_self);

		// Token: 0x06000CBA RID: 3258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDynamicScaleExplicit_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000CBB RID: 3259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyDynamicScale_Injected(IntPtr _unity_self);

		// Token: 0x06000CBC RID: 3260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsPowerOfTwo_Injected(IntPtr _unity_self);

		// Token: 0x06000CBD RID: 3261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetActive_Injected();

		// Token: 0x06000CBE RID: 3262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive_Injected(IntPtr rt);

		// Token: 0x06000CBF RID: 3263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorBuffer_Injected(IntPtr _unity_self, out RenderBuffer ret);

		// Token: 0x06000CC0 RID: 3264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDepthBuffer_Injected(IntPtr _unity_self, out RenderBuffer ret);

		// Token: 0x06000CC1 RID: 3265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMipMapCount_Injected(IntPtr _unity_self, int count);

		// Token: 0x06000CC2 RID: 3266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShadowSamplingMode_Injected(IntPtr _unity_self, ShadowSamplingMode samplingMode);

		// Token: 0x06000CC3 RID: 3267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeDepthBufferPtr_Injected(IntPtr _unity_self);

		// Token: 0x06000CC4 RID: 3268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DiscardContents_Injected(IntPtr _unity_self, bool discardColor, bool discardDepth);

		// Token: 0x06000CC5 RID: 3269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkRestoreExpected_Injected(IntPtr _unity_self);

		// Token: 0x06000CC6 RID: 3270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveAA_Injected(IntPtr _unity_self);

		// Token: 0x06000CC7 RID: 3271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveAATo_Injected(IntPtr _unity_self, IntPtr rt);

		// Token: 0x06000CC8 RID: 3272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalShaderProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper propertyName);

		// Token: 0x06000CC9 RID: 3273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Create_Injected(IntPtr _unity_self);

		// Token: 0x06000CCA RID: 3274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Release_Injected(IntPtr _unity_self);

		// Token: 0x06000CCB RID: 3275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsCreated_Injected(IntPtr _unity_self);

		// Token: 0x06000CCC RID: 3276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateMips_Injected(IntPtr _unity_self);

		// Token: 0x06000CCD RID: 3277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConvertToEquirect_Injected(IntPtr _unity_self, IntPtr equirect, Camera.MonoOrStereoscopicEye eye);

		// Token: 0x06000CCE RID: 3278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSRGBReadWrite_Injected(IntPtr _unity_self, bool srgb);

		// Token: 0x06000CCF RID: 3279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsStencil_Injected(IntPtr rt);

		// Token: 0x06000CD0 RID: 3280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureDescriptor_Injected(IntPtr _unity_self, [In] ref RenderTextureDescriptor desc);

		// Token: 0x06000CD1 RID: 3281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptor_Injected(IntPtr _unity_self, out RenderTextureDescriptor ret);

		// Token: 0x06000CD2 RID: 3282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTemporary_Internal_Injected([In] ref RenderTextureDescriptor desc);

		// Token: 0x06000CD3 RID: 3283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseTemporary_Injected(IntPtr temp);

		// Token: 0x06000CD4 RID: 3284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_depth_Injected(IntPtr _unity_self);

		// Token: 0x06000CD5 RID: 3285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depth_Injected(IntPtr _unity_self, int value);
	}
}

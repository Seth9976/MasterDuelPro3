using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A5 RID: 165
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Misc/GameObjectUtility.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/RenderManager.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public sealed class Camera : Behaviour
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00006A80 File Offset: 0x00004C80
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00006AA4 File Offset: 0x00004CA4
		[NativeProperty("Near")]
		public float nearClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_nearClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_nearClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006AC8 File Offset: 0x00004CC8
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00006AEC File Offset: 0x00004CEC
		[NativeProperty("Far")]
		public float farClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_farClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_farClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006B10 File Offset: 0x00004D10
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00006B34 File Offset: 0x00004D34
		[NativeProperty("VerticalFieldOfView")]
		public float fieldOfView
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_fieldOfView_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_fieldOfView_Injected(intPtr, value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00006B58 File Offset: 0x00004D58
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00006B7C File Offset: 0x00004D7C
		public RenderingPath renderingPath
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_renderingPath_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_renderingPath_Injected(intPtr, value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public RenderingPath actualRenderingPath
		{
			[NativeName("CalculateRenderingPath")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_actualRenderingPath_Injected(intPtr);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.Reset_Injected(intPtr);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00006BE8 File Offset: 0x00004DE8
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00006C0C File Offset: 0x00004E0C
		public bool allowHDR
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowHDR_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowHDR_Injected(intPtr, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00006C30 File Offset: 0x00004E30
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00006C54 File Offset: 0x00004E54
		public bool allowMSAA
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowMSAA_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowMSAA_Injected(intPtr, value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00006C78 File Offset: 0x00004E78
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00006C9C File Offset: 0x00004E9C
		public bool allowDynamicResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowDynamicResolution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowDynamicResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00006CC0 File Offset: 0x00004EC0
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00006CE4 File Offset: 0x00004EE4
		[NativeProperty("ForceIntoRT")]
		public bool forceIntoRenderTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_forceIntoRenderTexture_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_forceIntoRenderTexture_Injected(intPtr, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00006D08 File Offset: 0x00004F08
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00006D2C File Offset: 0x00004F2C
		public float orthographicSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_orthographicSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_orthographicSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00006D50 File Offset: 0x00004F50
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00006D74 File Offset: 0x00004F74
		public bool orthographic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_orthographic_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_orthographic_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00006D98 File Offset: 0x00004F98
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00006DBC File Offset: 0x00004FBC
		public OpaqueSortMode opaqueSortMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_opaqueSortMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_opaqueSortMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006DE0 File Offset: 0x00004FE0
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00006E04 File Offset: 0x00005004
		public TransparencySortMode transparencySortMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_transparencySortMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_transparencySortMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006E28 File Offset: 0x00005028
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00006E50 File Offset: 0x00005050
		public Vector3 transparencySortAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Camera.get_transparencySortAxis_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_transparencySortAxis_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006E74 File Offset: 0x00005074
		public void ResetTransparencySortSettings()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetTransparencySortSettings_Injected(intPtr);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00006E98 File Offset: 0x00005098
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00006EBC File Offset: 0x000050BC
		public float depth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_depth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_depth_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00006EE0 File Offset: 0x000050E0
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00006F04 File Offset: 0x00005104
		public float aspect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_aspect_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_aspect_Injected(intPtr, value);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006F28 File Offset: 0x00005128
		public void ResetAspect()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetAspect_Injected(intPtr);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00006F4C File Offset: 0x0000514C
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Camera.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00006F74 File Offset: 0x00005174
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00006F98 File Offset: 0x00005198
		public int cullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_cullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00006FBC File Offset: 0x000051BC
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00006FE0 File Offset: 0x000051E0
		public int eventMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_eventMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_eventMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00007004 File Offset: 0x00005204
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000701C File Offset: 0x0000521C
		public bool layerCullSpherical
		{
			get
			{
				return this.layerCullSphericalInternal;
			}
			set
			{
				bool flag = GraphicsSettings.currentRenderPipeline != null;
				if (flag)
				{
					Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.layerCullSpherical only with the built-in renderer.");
				}
				this.layerCullSphericalInternal = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00007050 File Offset: 0x00005250
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00007074 File Offset: 0x00005274
		[NativeProperty("LayerCullSpherical")]
		internal bool layerCullSphericalInternal
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_layerCullSphericalInternal_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_layerCullSphericalInternal_Injected(intPtr, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00007098 File Offset: 0x00005298
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000070BC File Offset: 0x000052BC
		public CameraType cameraType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_cameraType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cameraType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000070E0 File Offset: 0x000052E0
		internal Material skyboxMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Camera.get_skyboxMaterial_Injected(intPtr));
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00007108 File Offset: 0x00005308
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000712C File Offset: 0x0000532C
		[NativeConditional("UNITY_EDITOR")]
		public ulong overrideSceneCullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_overrideSceneCullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_overrideSceneCullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00007150 File Offset: 0x00005350
		[NativeConditional("UNITY_EDITOR")]
		internal ulong sceneCullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_sceneCullingMask_Injected(intPtr);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00007174 File Offset: 0x00005374
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00007198 File Offset: 0x00005398
		[NativeConditional("UNITY_EDITOR")]
		internal bool useInteractiveLightBakingData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useInteractiveLightBakingData_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useInteractiveLightBakingData_Injected(intPtr, value);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000071BC File Offset: 0x000053BC
		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		private float[] GetLayerCullDistances()
		{
			float[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Camera.GetLayerCullDistances_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007200 File Offset: 0x00005400
		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		private unsafe void SetLayerCullDistances([NotNull] float[] d)
		{
			if (d == null)
			{
				ThrowHelper.ThrowArgumentNullException(d, "d");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(d);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Camera.SetLayerCullDistances_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00007258 File Offset: 0x00005458
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00007270 File Offset: 0x00005470
		public float[] layerCullDistances
		{
			get
			{
				return this.GetLayerCullDistances();
			}
			set
			{
				bool flag = value.Length != 32;
				if (flag)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				this.SetLayerCullDistances(value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000072A0 File Offset: 0x000054A0
		[Obsolete("PreviewCullingLayer is obsolete. Use scene culling masks instead.", false)]
		internal static int PreviewCullingLayer
		{
			get
			{
				return 31;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000072B4 File Offset: 0x000054B4
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x000072D8 File Offset: 0x000054D8
		public bool useOcclusionCulling
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useOcclusionCulling_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useOcclusionCulling_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000072FC File Offset: 0x000054FC
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00007324 File Offset: 0x00005524
		public Matrix4x4 cullingMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_cullingMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cullingMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00007348 File Offset: 0x00005548
		public void ResetCullingMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetCullingMatrix_Injected(intPtr);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000736C File Offset: 0x0000556C
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00007394 File Offset: 0x00005594
		public Color backgroundColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				Camera.get_backgroundColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_backgroundColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000073B8 File Offset: 0x000055B8
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000073DC File Offset: 0x000055DC
		public CameraClearFlags clearFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_clearFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_clearFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00007400 File Offset: 0x00005600
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00007424 File Offset: 0x00005624
		public DepthTextureMode depthTextureMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_depthTextureMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_depthTextureMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00007448 File Offset: 0x00005648
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000746C File Offset: 0x0000566C
		public bool clearStencilAfterLightingPass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_clearStencilAfterLightingPass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_clearStencilAfterLightingPass_Injected(intPtr, value);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00007490 File Offset: 0x00005690
		public unsafe void SetReplacementShader(Shader shader, string replacementTag)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(replacementTag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = replacementTag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Camera.SetReplacementShader_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000074FC File Offset: 0x000056FC
		public void ResetReplacementShader()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetReplacementShader_Injected(intPtr);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00007520 File Offset: 0x00005720
		internal Camera.ProjectionMatrixMode projectionMatrixMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_projectionMatrixMode_Injected(intPtr);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00007544 File Offset: 0x00005744
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00007568 File Offset: 0x00005768
		public bool usePhysicalProperties
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_usePhysicalProperties_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_usePhysicalProperties_Injected(intPtr, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000758C File Offset: 0x0000578C
		// (set) Token: 0x06000304 RID: 772 RVA: 0x000075B0 File Offset: 0x000057B0
		public int iso
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_iso_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_iso_Injected(intPtr, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000075D4 File Offset: 0x000057D4
		// (set) Token: 0x06000306 RID: 774 RVA: 0x000075F8 File Offset: 0x000057F8
		public float shutterSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_shutterSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_shutterSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000761C File Offset: 0x0000581C
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00007640 File Offset: 0x00005840
		public float aperture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_aperture_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_aperture_Injected(intPtr, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00007664 File Offset: 0x00005864
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00007688 File Offset: 0x00005888
		public float focusDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_focusDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_focusDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000076AC File Offset: 0x000058AC
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000076D0 File Offset: 0x000058D0
		public float focalLength
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_focalLength_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_focalLength_Injected(intPtr, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000076F4 File Offset: 0x000058F4
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00007718 File Offset: 0x00005918
		public int bladeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_bladeCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_bladeCount_Injected(intPtr, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000773C File Offset: 0x0000593C
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00007764 File Offset: 0x00005964
		public Vector2 curvature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_curvature_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_curvature_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007788 File Offset: 0x00005988
		// (set) Token: 0x06000312 RID: 786 RVA: 0x000077AC File Offset: 0x000059AC
		public float barrelClipping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_barrelClipping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_barrelClipping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000077D0 File Offset: 0x000059D0
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000077F4 File Offset: 0x000059F4
		public float anamorphism
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_anamorphism_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_anamorphism_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00007818 File Offset: 0x00005A18
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00007840 File Offset: 0x00005A40
		public Vector2 sensorSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_sensorSize_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_sensorSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000788C File Offset: 0x00005A8C
		public Vector2 lensShift
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_lensShift_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_lensShift_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000078B0 File Offset: 0x00005AB0
		// (set) Token: 0x0600031A RID: 794 RVA: 0x000078D4 File Offset: 0x00005AD4
		public Camera.GateFitMode gateFit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_gateFit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_gateFit_Injected(intPtr, value);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000078F8 File Offset: 0x00005AF8
		public float GetGateFittedFieldOfView()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetGateFittedFieldOfView_Injected(intPtr);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000791C File Offset: 0x00005B1C
		public Vector2 GetGateFittedLensShift()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Camera.GetGateFittedLensShift_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007944 File Offset: 0x00005B44
		internal Vector3 GetLocalSpaceAim()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.GetLocalSpaceAim_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000796C File Offset: 0x00005B6C
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00007994 File Offset: 0x00005B94
		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Camera.get_rect_Injected(intPtr, out rect);
				return rect;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_rect_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000079B8 File Offset: 0x00005BB8
		// (set) Token: 0x06000321 RID: 801 RVA: 0x000079E0 File Offset: 0x00005BE0
		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Camera.get_pixelRect_Injected(intPtr, out rect);
				return rect;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_pixelRect_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00007A04 File Offset: 0x00005C04
		public int pixelWidth
		{
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_pixelWidth_Injected(intPtr);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00007A28 File Offset: 0x00005C28
		public int pixelHeight
		{
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_pixelHeight_Injected(intPtr);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00007A4C File Offset: 0x00005C4C
		public int scaledPixelWidth
		{
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_scaledPixelWidth_Injected(intPtr);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00007A70 File Offset: 0x00005C70
		public int scaledPixelHeight
		{
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_scaledPixelHeight_Injected(intPtr);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00007A94 File Offset: 0x00005C94
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00007ABC File Offset: 0x00005CBC
		public RenderTexture targetTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(Camera.get_targetTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_targetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(value));
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public RenderTexture activeTexture
		{
			[NativeName("GetCurrentTargetTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(Camera.get_activeTexture_Injected(intPtr));
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00007B0C File Offset: 0x00005D0C
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00007B30 File Offset: 0x00005D30
		public int targetDisplay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_targetDisplay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_targetDisplay_Injected(intPtr, value);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007B54 File Offset: 0x00005D54
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetTargetBuffersImpl_Injected(intPtr, ref color, ref depth);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007B7A File Offset: 0x00005D7A
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007B88 File Offset: 0x00005D88
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private unsafe void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderBuffer> span = new Span<RenderBuffer>(color);
			fixed (RenderBuffer* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Camera.SetTargetBuffersMRTImpl_Injected(intPtr, ref managedSpanWrapper, ref depth);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007BD2 File Offset: 0x00005DD2
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal string[] GetCameraBufferWarnings()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetCameraBufferWarnings_Injected(intPtr);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00007C04 File Offset: 0x00005E04
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_cameraToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00007C2C File Offset: 0x00005E2C
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00007C54 File Offset: 0x00005E54
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_worldToCameraMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_worldToCameraMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00007C78 File Offset: 0x00005E78
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public Matrix4x4 projectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_projectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_projectionMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00007CC4 File Offset: 0x00005EC4
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00007CEC File Offset: 0x00005EEC
		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_nonJitteredProjectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_nonJitteredProjectionMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00007D10 File Offset: 0x00005F10
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00007D34 File Offset: 0x00005F34
		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public bool useJitteredProjectionMatrixForTransparentRendering
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useJitteredProjectionMatrixForTransparentRendering_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useJitteredProjectionMatrixForTransparentRendering_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00007D58 File Offset: 0x00005F58
		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_previousViewProjectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00007D80 File Offset: 0x00005F80
		public void ResetWorldToCameraMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetWorldToCameraMatrix_Injected(intPtr);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public void ResetProjectionMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetProjectionMatrix_Injected(intPtr);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00007DC8 File Offset: 0x00005FC8
		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.CalculateObliqueMatrix_Injected(intPtr, ref clipPlane, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public Vector3 WorldToScreenPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.WorldToScreenPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00007E18 File Offset: 0x00006018
		public Vector3 WorldToViewportPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.WorldToViewportPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00007E40 File Offset: 0x00006040
		public Vector3 ViewportToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ViewportToWorldPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00007E68 File Offset: 0x00006068
		public Vector3 ScreenToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ScreenToWorldPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00007E90 File Offset: 0x00006090
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return this.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00007EAC File Offset: 0x000060AC
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return this.WorldToViewportPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007EC8 File Offset: 0x000060C8
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return this.ViewportToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00007EE4 File Offset: 0x000060E4
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return this.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007F00 File Offset: 0x00006100
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ScreenToViewportPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007F28 File Offset: 0x00006128
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ViewportToScreenPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007F50 File Offset: 0x00006150
		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Camera.GetFrustumPlaneSizeAt_Injected(intPtr, distance, out vector);
			return vector;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00007F78 File Offset: 0x00006178
		private Ray ViewportPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Ray ray;
			Camera.ViewportPointToRay_Injected(intPtr, ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007FA0 File Offset: 0x000061A0
		public Ray ViewportPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ViewportPointToRay(pos, eye);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00007FC0 File Offset: 0x000061C0
		public Ray ViewportPointToRay(Vector3 pos)
		{
			return this.ViewportPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007FDC File Offset: 0x000061DC
		private Ray ScreenPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Ray ray;
			Camera.ScreenPointToRay_Injected(intPtr, ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008004 File Offset: 0x00006204
		public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ScreenPointToRay(pos, eye);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008024 File Offset: 0x00006224
		public Ray ScreenPointToRay(Vector3 pos)
		{
			return this.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008040 File Offset: 0x00006240
		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private unsafe void CalculateFrustumCornersInternal(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (outCorners != null)
				{
					fixed (Vector3[] array = outCorners)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Camera.CalculateFrustumCornersInternal_Injected(intPtr, ref viewport, z, eye, out blittableArrayWrapper);
			}
			finally
			{
				Vector3[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000080A4 File Offset: 0x000062A4
		public void CalculateFrustumCorners(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			bool flag = outCorners == null;
			if (flag)
			{
				throw new ArgumentNullException("outCorners");
			}
			bool flag2 = outCorners.Length < 4;
			if (flag2)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			this.CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000080F0 File Offset: 0x000062F0
		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode)
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008110 File Offset: 0x00006310
		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, Camera.GateFitParameters gateFitParameters = default(Camera.GateFitParameters))
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		// Token: 0x06000352 RID: 850
		[NativeName("FocalLengthToFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FocalLengthToFieldOfView(float focalLength, float sensorSize);

		// Token: 0x06000353 RID: 851
		[NativeName("FieldOfViewToFocalLength_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FieldOfViewToFocalLength(float fieldOfView, float sensorSize);

		// Token: 0x06000354 RID: 852
		[NativeName("HorizontalToVerticalFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio);

		// Token: 0x06000355 RID: 853
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio);

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00008130 File Offset: 0x00006330
		public static Camera main
		{
			[FreeFunction("FindMainCamera")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Camera>(Camera.get_main_Injected());
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00008148 File Offset: 0x00006348
		public static Camera current
		{
			get
			{
				return Camera.currentInternal;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008160 File Offset: 0x00006360
		private static Camera currentInternal
		{
			[FreeFunction("GetCurrentCameraPPtr")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Camera>(Camera.get_currentInternal_Injected());
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008178 File Offset: 0x00006378
		// (set) Token: 0x0600035A RID: 858 RVA: 0x000081A0 File Offset: 0x000063A0
		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Scene scene;
				Camera.get_scene_Injected(intPtr, out scene);
				return scene;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_scene_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000081C4 File Offset: 0x000063C4
		public bool stereoEnabled
		{
			[NativeMethod("GetStereoEnabledForBuiltInOrSRP")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoEnabled_Injected(intPtr);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000081E8 File Offset: 0x000063E8
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000820C File Offset: 0x0000640C
		public float stereoSeparation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoSeparation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoSeparation_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00008230 File Offset: 0x00006430
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00008254 File Offset: 0x00006454
		public float stereoConvergence
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoConvergence_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoConvergence_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00008278 File Offset: 0x00006478
		public bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_areVRStereoViewMatricesWithinSingleCullTolerance_Injected(intPtr);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000829C File Offset: 0x0000649C
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000082B4 File Offset: 0x000064B4
		public StereoTargetEyeMask stereoTargetEye
		{
			get
			{
				return this.stereoTargetEyeInternal;
			}
			set
			{
				bool flag = GraphicsSettings.currentRenderPipeline != null;
				if (flag)
				{
					Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.stereoTargetEye only with the built-in renderer.");
				}
				this.stereoTargetEyeInternal = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000082E8 File Offset: 0x000064E8
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000830C File Offset: 0x0000650C
		[NativeProperty("StereoTargetEye")]
		internal StereoTargetEyeMask stereoTargetEyeInternal
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoTargetEyeInternal_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoTargetEyeInternal_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00008330 File Offset: 0x00006530
		public Camera.MonoOrStereoscopicEye stereoActiveEye
		{
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoActiveEye_Injected(intPtr);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00008354 File Offset: 0x00006554
		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoNonJitteredProjectionMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000837C File Offset: 0x0000657C
		[FreeFunction("CameraScripting::GetStereoViewMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoViewMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoViewMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000083A4 File Offset: 0x000065A4
		public void CopyStereoDeviceProjectionMatrixToNonJittered(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.CopyStereoDeviceProjectionMatrixToNonJittered_Injected(intPtr, eye);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000083C8 File Offset: 0x000065C8
		[FreeFunction("CameraScripting::GetStereoProjectionMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoProjectionMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoProjectionMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000083F0 File Offset: 0x000065F0
		public void SetStereoProjectionMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetStereoProjectionMatrix_Injected(intPtr, eye, ref matrix);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008418 File Offset: 0x00006618
		public void ResetStereoProjectionMatrices()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetStereoProjectionMatrices_Injected(intPtr);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000843C File Offset: 0x0000663C
		public void SetStereoViewMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetStereoViewMatrix_Injected(intPtr, eye, ref matrix);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008464 File Offset: 0x00006664
		public void ResetStereoViewMatrices()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetStereoViewMatrices_Injected(intPtr);
		}

		// Token: 0x0600036E RID: 878
		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasCount();

		// Token: 0x0600036F RID: 879 RVA: 0x00008488 File Offset: 0x00006688
		[FreeFunction("CameraScripting::GetAllCameras")]
		private static int GetAllCamerasImpl([NotNull] [Out] Camera[] cam)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			return Camera.GetAllCamerasImpl_Injected(cam);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000084AC File Offset: 0x000066AC
		public static int allCamerasCount
		{
			get
			{
				return Camera.GetAllCamerasCount();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000084C4 File Offset: 0x000066C4
		public static Camera[] allCameras
		{
			get
			{
				Camera[] cam = new Camera[Camera.allCamerasCount];
				Camera.GetAllCamerasImpl(cam);
				return cam;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000084EC File Offset: 0x000066EC
		public static int GetAllCameras(Camera[] cameras)
		{
			bool flag = cameras == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = cameras.Length < Camera.allCamerasCount;
			if (flag2)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return Camera.GetAllCamerasImpl(cameras);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000852C File Offset: 0x0000672C
		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		private bool RenderToCubemapImpl(Texture tex, [DefaultValue("63")] int faceMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.RenderToCubemapImpl_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(tex), faceMask);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00008558 File Offset: 0x00006758
		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00008574 File Offset: 0x00006774
		public bool RenderToCubemap(Cubemap cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00008590 File Offset: 0x00006790
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000085AC File Offset: 0x000067AC
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000085C8 File Offset: 0x000067C8
		[NativeConditional("UNITY_EDITOR")]
		private int GetFilterMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetFilterMode_Injected(intPtr);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000085EC File Offset: 0x000067EC
		[NativeConditional("UNITY_EDITOR")]
		public Camera.SceneViewFilterMode sceneViewFilterMode
		{
			get
			{
				return (Camera.SceneViewFilterMode)this.GetFilterMode();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00008604 File Offset: 0x00006804
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00008628 File Offset: 0x00006828
		[NativeConditional("UNITY_EDITOR")]
		public bool renderCloudsInSceneView
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_renderCloudsInSceneView_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_renderCloudsInSceneView_Injected(intPtr, value);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000864C File Offset: 0x0000684C
		[NativeName("RenderToCubemap")]
		private bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.RenderToCubemapEyeImpl_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(cubemap), faceMask, stereoEye);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008678 File Offset: 0x00006878
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			return this.RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008694 File Offset: 0x00006894
		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		public void Render()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.Render_Injected(intPtr);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000086B8 File Offset: 0x000068B8
		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		public unsafe void RenderWithShader(Shader shader, string replacementTag)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(replacementTag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = replacementTag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Camera.RenderWithShader_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008724 File Offset: 0x00006924
		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		public void RenderDontRestore()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RenderDontRestore_Injected(intPtr);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00008748 File Offset: 0x00006948
		[Obsolete("SubmitRenderRequests is obsolete, use SubmitRenderRequest with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public void SubmitRenderRequests(List<Camera.RenderRequest> renderRequests)
		{
			bool flag = renderRequests == null || renderRequests.Count == 0;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests has been invoked with invalid renderRequests");
			}
			bool flag2 = GraphicsSettings.currentRenderPipeline == null;
			if (flag2)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequests' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequests);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000879C File Offset: 0x0000699C
		public void SubmitRenderRequest<RequestData>(RequestData renderRequest)
		{
			bool flag = renderRequest == null;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests is invoked with invalid renderRequests");
			}
			ObjectIdRequest objectIdRequest = renderRequest as ObjectIdRequest;
			bool flag2 = objectIdRequest != null;
			if (flag2)
			{
				bool flag3 = objectIdRequest.destination.depthStencilFormat == GraphicsFormat.None;
				if (flag3)
				{
					Debug.LogWarning("ObjectId Render Request submitted without a depth stencil, which can produce results that are not depth tested correctly");
				}
				bool flag4 = GraphicsSettings.currentRenderPipeline == null || !RenderPipelineManager.currentPipeline.IsRenderRequestSupported<ObjectIdRequest>(this, objectIdRequest);
				if (flag4)
				{
					throw new ArgumentException((GraphicsSettings.currentRenderPipeline == null) ? "The Built-In Render Pipeline does not support ObjectIdRequest outside of the editor." : "The current render pipeline does not support ObjectIdRequest, and the fallback implementation of the Built-In Render Pipeline is not available outside of the editor.");
				}
			}
			bool flag5 = GraphicsSettings.currentRenderPipeline == null;
			if (flag5)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequest' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequest);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00008868 File Offset: 0x00006A68
		[FreeFunction("CameraScripting::SubmitRenderRequests", HasExplicitThis = true)]
		private void SubmitRenderRequestsInternal(object requests)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SubmitRenderRequestsInternal_Injected(intPtr, requests);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000888C File Offset: 0x00006A8C
		[NativeConditional("UNITY_EDITOR")]
		[FreeFunction("CameraScripting::SubmitBuiltInObjectIDRenderRequest", HasExplicitThis = true)]
		[return: Unmarshalled]
		private Object[] SubmitBuiltInObjectIDRenderRequest(RenderTexture target, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.SubmitBuiltInObjectIDRenderRequest_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(target), mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000088B8 File Offset: 0x00006AB8
		[FreeFunction("CameraScripting::SetupCurrent")]
		public static void SetupCurrent(Camera cur)
		{
			Camera.SetupCurrent_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cur));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000088D0 File Offset: 0x00006AD0
		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		public void CopyFrom(Camera other)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.CopyFrom_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(other));
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000088F8 File Offset: 0x00006AF8
		public int commandBufferCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_commandBufferCount_Injected(intPtr);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000891C File Offset: 0x00006B1C
		[NativeName("RemoveCommandBuffers")]
		private void RemoveCommandBuffersImpl(CameraEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RemoveCommandBuffersImpl_Injected(intPtr, evt);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008940 File Offset: 0x00006B40
		[NativeName("RemoveAllCommandBuffers")]
		private void RemoveAllCommandBuffersImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RemoveAllCommandBuffersImpl_Injected(intPtr);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008964 File Offset: 0x00006B64
		public void RemoveCommandBuffers(CameraEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveCommandBuffers only with the built-in renderer.");
			}
			else
			{
				this.m_NonSerializedVersion += 1U;
				this.RemoveCommandBuffersImpl(evt);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000089A8 File Offset: 0x00006BA8
		public void RemoveAllCommandBuffers()
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveAllCommandBuffers only with the built-in renderer.");
			}
			else
			{
				this.m_NonSerializedVersion += 1U;
				this.RemoveAllCommandBuffersImpl();
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000089E8 File Offset: 0x00006BE8
		[NativeName("AddCommandBuffer")]
		private void AddCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.AddCommandBufferImpl_Injected(intPtr, evt, intPtr2);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00008A30 File Offset: 0x00006C30
		[NativeName("AddCommandBufferAsync")]
		private void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull] CommandBuffer buffer, ComputeQueueType queueType)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.AddCommandBufferAsyncImpl_Injected(intPtr, evt, intPtr2, queueType);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008A78 File Offset: 0x00006C78
		[NativeName("RemoveCommandBuffer")]
		private void RemoveCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.RemoveCommandBufferImpl_Injected(intPtr, evt, intPtr2);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.AddCommandBuffer only with the built-in renderer.");
			}
			else
			{
				this.AddCommandBufferImpl(evt, buffer);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00008B40 File Offset: 0x00006D40
		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.AddCommandBufferAsync only with the built-in renderer.");
			}
			else
			{
				this.AddCommandBufferAsyncImpl(evt, buffer, queueType);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveCommandBuffer only with the built-in renderer.");
			}
			else
			{
				this.RemoveCommandBufferImpl(evt, buffer);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008C40 File Offset: 0x00006E40
		public CommandBuffer[] GetCommandBuffers(CameraEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.GetCommandBuffers only with the built-in renderer.");
			}
			return this.GetCommandBuffersImpl(evt);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00008C74 File Offset: 0x00006E74
		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		internal CommandBuffer[] GetCommandBuffersImpl(CameraEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetCommandBuffersImpl_Injected(intPtr, evt);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00008C98 File Offset: 0x00006E98
		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			bool flag = Camera.onPreCull != null;
			if (flag)
			{
				Camera.onPreCull(cam);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00008CC0 File Offset: 0x00006EC0
		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			bool flag = Camera.onPreRender != null;
			if (flag)
			{
				Camera.onPreRender(cam);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00008CE8 File Offset: 0x00006EE8
		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			bool flag = Camera.onPostRender != null;
			if (flag)
			{
				Camera.onPostRender(cam);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00008D0E File Offset: 0x00006F0E
		[RequiredByNativeCode]
		private static void BumpNonSerializedVersion(Camera cam)
		{
			cam.m_NonSerializedVersion += 1U;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00003D56 File Offset: 0x00001F56
		internal void OnlyUsedForTesting1()
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00003D56 File Offset: 0x00001F56
		internal void OnlyUsedForTesting2()
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00008D20 File Offset: 0x00006F20
		public unsafe bool TryGetCullingParameters(out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00008D40 File Offset: 0x00006F40
		public unsafe bool TryGetCullingParameters(bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00008D60 File Offset: 0x00006F60
		[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetCullingParameters_Internal")]
		private static bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize)
		{
			return Camera.GetCullingParameters_Internal_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera), stereoAware, out cullingParameters, managedCullingParametersSize);
		}

		// Token: 0x0600039D RID: 925
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_nearClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x0600039E RID: 926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nearClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600039F RID: 927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_farClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x060003A0 RID: 928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_farClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003A1 RID: 929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fieldOfView_Injected(IntPtr _unity_self);

		// Token: 0x060003A2 RID: 930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fieldOfView_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003A3 RID: 931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingPath get_renderingPath_Injected(IntPtr _unity_self);

		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderingPath_Injected(IntPtr _unity_self, RenderingPath value);

		// Token: 0x060003A5 RID: 933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingPath get_actualRenderingPath_Injected(IntPtr _unity_self);

		// Token: 0x060003A6 RID: 934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x060003A7 RID: 935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowHDR_Injected(IntPtr _unity_self);

		// Token: 0x060003A8 RID: 936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowHDR_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowMSAA_Injected(IntPtr _unity_self);

		// Token: 0x060003AA RID: 938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowMSAA_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003AB RID: 939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowDynamicResolution_Injected(IntPtr _unity_self);

		// Token: 0x060003AC RID: 940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowDynamicResolution_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003AD RID: 941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_forceIntoRenderTexture_Injected(IntPtr _unity_self);

		// Token: 0x060003AE RID: 942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceIntoRenderTexture_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003AF RID: 943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_orthographicSize_Injected(IntPtr _unity_self);

		// Token: 0x060003B0 RID: 944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographicSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_orthographic_Injected(IntPtr _unity_self);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OpaqueSortMode get_opaqueSortMode_Injected(IntPtr _unity_self);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_opaqueSortMode_Injected(IntPtr _unity_self, OpaqueSortMode value);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TransparencySortMode get_transparencySortMode_Injected(IntPtr _unity_self);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortMode_Injected(IntPtr _unity_self, TransparencySortMode value);

		// Token: 0x060003B7 RID: 951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_transparencySortAxis_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003B8 RID: 952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortAxis_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003B9 RID: 953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTransparencySortSettings_Injected(IntPtr _unity_self);

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_depth_Injected(IntPtr _unity_self);

		// Token: 0x060003BB RID: 955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depth_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003BC RID: 956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_aspect_Injected(IntPtr _unity_self);

		// Token: 0x060003BD RID: 957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aspect_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003BE RID: 958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAspect_Injected(IntPtr _unity_self);

		// Token: 0x060003BF RID: 959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003C0 RID: 960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_eventMask_Injected(IntPtr _unity_self);

		// Token: 0x060003C3 RID: 963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_eventMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_layerCullSphericalInternal_Injected(IntPtr _unity_self);

		// Token: 0x060003C5 RID: 965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerCullSphericalInternal_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003C6 RID: 966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CameraType get_cameraType_Injected(IntPtr _unity_self);

		// Token: 0x060003C7 RID: 967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cameraType_Injected(IntPtr _unity_self, CameraType value);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_skyboxMaterial_Injected(IntPtr _unity_self);

		// Token: 0x060003C9 RID: 969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_overrideSceneCullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060003CA RID: 970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_overrideSceneCullingMask_Injected(IntPtr _unity_self, ulong value);

		// Token: 0x060003CB RID: 971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_sceneCullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060003CC RID: 972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useInteractiveLightBakingData_Injected(IntPtr _unity_self);

		// Token: 0x060003CD RID: 973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useInteractiveLightBakingData_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003CE RID: 974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLayerCullDistances_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060003CF RID: 975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerCullDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper d);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useOcclusionCulling_Injected(IntPtr _unity_self);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useOcclusionCulling_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cullingMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetCullingMatrix_Injected(IntPtr _unity_self);

		// Token: 0x060003D5 RID: 981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CameraClearFlags get_clearFlags_Injected(IntPtr _unity_self);

		// Token: 0x060003D8 RID: 984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clearFlags_Injected(IntPtr _unity_self, CameraClearFlags value);

		// Token: 0x060003D9 RID: 985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DepthTextureMode get_depthTextureMode_Injected(IntPtr _unity_self);

		// Token: 0x060003DA RID: 986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depthTextureMode_Injected(IntPtr _unity_self, DepthTextureMode value);

		// Token: 0x060003DB RID: 987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_clearStencilAfterLightingPass_Injected(IntPtr _unity_self);

		// Token: 0x060003DC RID: 988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clearStencilAfterLightingPass_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003DD RID: 989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReplacementShader_Injected(IntPtr _unity_self, IntPtr shader, ref ManagedSpanWrapper replacementTag);

		// Token: 0x060003DE RID: 990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetReplacementShader_Injected(IntPtr _unity_self);

		// Token: 0x060003DF RID: 991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.ProjectionMatrixMode get_projectionMatrixMode_Injected(IntPtr _unity_self);

		// Token: 0x060003E0 RID: 992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_usePhysicalProperties_Injected(IntPtr _unity_self);

		// Token: 0x060003E1 RID: 993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_usePhysicalProperties_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003E2 RID: 994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_iso_Injected(IntPtr _unity_self);

		// Token: 0x060003E3 RID: 995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_iso_Injected(IntPtr _unity_self, int value);

		// Token: 0x060003E4 RID: 996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shutterSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060003E5 RID: 997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shutterSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003E6 RID: 998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_aperture_Injected(IntPtr _unity_self);

		// Token: 0x060003E7 RID: 999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aperture_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003E8 RID: 1000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_focusDistance_Injected(IntPtr _unity_self);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_focusDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003EA RID: 1002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_focalLength_Injected(IntPtr _unity_self);

		// Token: 0x060003EB RID: 1003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_focalLength_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003EC RID: 1004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_bladeCount_Injected(IntPtr _unity_self);

		// Token: 0x060003ED RID: 1005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bladeCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x060003EE RID: 1006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_curvature_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003EF RID: 1007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_curvature_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003F0 RID: 1008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_barrelClipping_Injected(IntPtr _unity_self);

		// Token: 0x060003F1 RID: 1009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_barrelClipping_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_anamorphism_Injected(IntPtr _unity_self);

		// Token: 0x060003F3 RID: 1011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anamorphism_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003F4 RID: 1012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sensorSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sensorSize_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003F6 RID: 1014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lensShift_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003F7 RID: 1015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lensShift_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060003F8 RID: 1016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.GateFitMode get_gateFit_Injected(IntPtr _unity_self);

		// Token: 0x060003F9 RID: 1017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gateFit_Injected(IntPtr _unity_self, Camera.GateFitMode value);

		// Token: 0x060003FA RID: 1018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGateFittedFieldOfView_Injected(IntPtr _unity_self);

		// Token: 0x060003FB RID: 1019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGateFittedLensShift_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060003FC RID: 1020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalSpaceAim_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003FD RID: 1021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060003FE RID: 1022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rect_Injected(IntPtr _unity_self, [In] ref Rect value);

		// Token: 0x060003FF RID: 1023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pixelRect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x06000400 RID: 1024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pixelRect_Injected(IntPtr _unity_self, [In] ref Rect value);

		// Token: 0x06000401 RID: 1025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pixelWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000402 RID: 1026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pixelHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000403 RID: 1027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_scaledPixelWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000404 RID: 1028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_scaledPixelHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000405 RID: 1029
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000406 RID: 1030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000407 RID: 1031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_activeTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_targetDisplay_Injected(IntPtr _unity_self);

		// Token: 0x06000409 RID: 1033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetDisplay_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600040A RID: 1034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetBuffersImpl_Injected(IntPtr _unity_self, [In] ref RenderBuffer color, [In] ref RenderBuffer depth);

		// Token: 0x0600040B RID: 1035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetBuffersMRTImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper color, [In] ref RenderBuffer depth);

		// Token: 0x0600040C RID: 1036
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCameraBufferWarnings_Injected(IntPtr _unity_self);

		// Token: 0x0600040D RID: 1037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cameraToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x0600040E RID: 1038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldToCameraMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x0600040F RID: 1039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldToCameraMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x06000410 RID: 1040
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_projectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000411 RID: 1041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x06000412 RID: 1042
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_nonJitteredProjectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000413 RID: 1043
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nonJitteredProjectionMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x06000414 RID: 1044
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useJitteredProjectionMatrixForTransparentRendering_Injected(IntPtr _unity_self);

		// Token: 0x06000415 RID: 1045
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useJitteredProjectionMatrixForTransparentRendering_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000416 RID: 1046
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_previousViewProjectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000417 RID: 1047
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetWorldToCameraMatrix_Injected(IntPtr _unity_self);

		// Token: 0x06000418 RID: 1048
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetProjectionMatrix_Injected(IntPtr _unity_self);

		// Token: 0x06000419 RID: 1049
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateObliqueMatrix_Injected(IntPtr _unity_self, [In] ref Vector4 clipPlane, out Matrix4x4 ret);

		// Token: 0x0600041A RID: 1050
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToScreenPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600041B RID: 1051
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToViewportPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600041C RID: 1052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportToWorldPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600041D RID: 1053
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenToWorldPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x0600041E RID: 1054
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenToViewportPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x0600041F RID: 1055
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportToScreenPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x06000420 RID: 1056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFrustumPlaneSizeAt_Injected(IntPtr _unity_self, float distance, out Vector2 ret);

		// Token: 0x06000421 RID: 1057
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportPointToRay_Injected(IntPtr _unity_self, [In] ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x06000422 RID: 1058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenPointToRay_Injected(IntPtr _unity_self, [In] ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x06000423 RID: 1059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateFrustumCornersInternal_Injected(IntPtr _unity_self, [In] ref Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, out BlittableArrayWrapper outCorners);

		// Token: 0x06000424 RID: 1060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, [In] ref Vector2 sensorSize, [In] ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode);

		// Token: 0x06000425 RID: 1061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_main_Injected();

		// Token: 0x06000426 RID: 1062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_currentInternal_Injected();

		// Token: 0x06000427 RID: 1063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scene_Injected(IntPtr _unity_self, out Scene ret);

		// Token: 0x06000428 RID: 1064
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scene_Injected(IntPtr _unity_self, [In] ref Scene value);

		// Token: 0x06000429 RID: 1065
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stereoEnabled_Injected(IntPtr _unity_self);

		// Token: 0x0600042A RID: 1066
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stereoSeparation_Injected(IntPtr _unity_self);

		// Token: 0x0600042B RID: 1067
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoSeparation_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600042C RID: 1068
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stereoConvergence_Injected(IntPtr _unity_self);

		// Token: 0x0600042D RID: 1069
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoConvergence_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600042E RID: 1070
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_areVRStereoViewMatricesWithinSingleCullTolerance_Injected(IntPtr _unity_self);

		// Token: 0x0600042F RID: 1071
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StereoTargetEyeMask get_stereoTargetEyeInternal_Injected(IntPtr _unity_self);

		// Token: 0x06000430 RID: 1072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoTargetEyeInternal_Injected(IntPtr _unity_self, StereoTargetEyeMask value);

		// Token: 0x06000431 RID: 1073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.MonoOrStereoscopicEye get_stereoActiveEye_Injected(IntPtr _unity_self);

		// Token: 0x06000432 RID: 1074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoNonJitteredProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000433 RID: 1075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoViewMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000434 RID: 1076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyStereoDeviceProjectionMatrixToNonJittered_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye);

		// Token: 0x06000435 RID: 1077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x06000436 RID: 1078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, [In] ref Matrix4x4 matrix);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetStereoProjectionMatrices_Injected(IntPtr _unity_self);

		// Token: 0x06000438 RID: 1080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoViewMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, [In] ref Matrix4x4 matrix);

		// Token: 0x06000439 RID: 1081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetStereoViewMatrices_Injected(IntPtr _unity_self);

		// Token: 0x0600043A RID: 1082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasImpl_Injected([Out] Camera[] cam);

		// Token: 0x0600043B RID: 1083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RenderToCubemapImpl_Injected(IntPtr _unity_self, IntPtr tex, [DefaultValue("63")] int faceMask);

		// Token: 0x0600043C RID: 1084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFilterMode_Injected(IntPtr _unity_self);

		// Token: 0x0600043D RID: 1085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_renderCloudsInSceneView_Injected(IntPtr _unity_self);

		// Token: 0x0600043E RID: 1086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderCloudsInSceneView_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600043F RID: 1087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RenderToCubemapEyeImpl_Injected(IntPtr _unity_self, IntPtr cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye);

		// Token: 0x06000440 RID: 1088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Render_Injected(IntPtr _unity_self);

		// Token: 0x06000441 RID: 1089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderWithShader_Injected(IntPtr _unity_self, IntPtr shader, ref ManagedSpanWrapper replacementTag);

		// Token: 0x06000442 RID: 1090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderDontRestore_Injected(IntPtr _unity_self);

		// Token: 0x06000443 RID: 1091
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SubmitRenderRequestsInternal_Injected(IntPtr _unity_self, object requests);

		// Token: 0x06000444 RID: 1092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] SubmitBuiltInObjectIDRenderRequest_Injected(IntPtr _unity_self, IntPtr target, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06000445 RID: 1093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCurrent_Injected(IntPtr cur);

		// Token: 0x06000446 RID: 1094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyFrom_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x06000447 RID: 1095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_commandBufferCount_Injected(IntPtr _unity_self);

		// Token: 0x06000448 RID: 1096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBuffersImpl_Injected(IntPtr _unity_self, CameraEvent evt);

		// Token: 0x06000449 RID: 1097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAllCommandBuffersImpl_Injected(IntPtr _unity_self);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferAsyncImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer, ComputeQueueType queueType);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBufferImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer);

		// Token: 0x0600044D RID: 1101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CommandBuffer[] GetCommandBuffersImpl_Injected(IntPtr _unity_self, CameraEvent evt);

		// Token: 0x0600044E RID: 1102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetCullingParameters_Internal_Injected(IntPtr camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		// Token: 0x040001F3 RID: 499
		public const float kMinAperture = 0.7f;

		// Token: 0x040001F4 RID: 500
		public const float kMaxAperture = 32f;

		// Token: 0x040001F5 RID: 501
		public const int kMinBladeCount = 3;

		// Token: 0x040001F6 RID: 502
		public const int kMaxBladeCount = 11;

		// Token: 0x040001F7 RID: 503
		internal uint m_NonSerializedVersion;

		// Token: 0x040001F8 RID: 504
		public static Camera.CameraCallback onPreCull;

		// Token: 0x040001F9 RID: 505
		public static Camera.CameraCallback onPreRender;

		// Token: 0x040001FA RID: 506
		public static Camera.CameraCallback onPostRender;

		// Token: 0x020000A6 RID: 166
		internal enum ProjectionMatrixMode
		{
			// Token: 0x040001FC RID: 508
			Explicit,
			// Token: 0x040001FD RID: 509
			Implicit,
			// Token: 0x040001FE RID: 510
			PhysicalPropertiesBased
		}

		// Token: 0x020000A7 RID: 167
		public enum GateFitMode
		{
			// Token: 0x04000200 RID: 512
			Vertical = 1,
			// Token: 0x04000201 RID: 513
			Horizontal,
			// Token: 0x04000202 RID: 514
			Fill,
			// Token: 0x04000203 RID: 515
			Overscan,
			// Token: 0x04000204 RID: 516
			None = 0
		}

		// Token: 0x020000A8 RID: 168
		public struct GateFitParameters
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00008D7B File Offset: 0x00006F7B
			public readonly Camera.GateFitMode mode { get; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00008D83 File Offset: 0x00006F83
			public readonly float aspect { get; }
		}

		// Token: 0x020000A9 RID: 169
		public enum StereoscopicEye
		{
			// Token: 0x04000208 RID: 520
			Left,
			// Token: 0x04000209 RID: 521
			Right
		}

		// Token: 0x020000AA RID: 170
		public enum MonoOrStereoscopicEye
		{
			// Token: 0x0400020B RID: 523
			Left,
			// Token: 0x0400020C RID: 524
			Right,
			// Token: 0x0400020D RID: 525
			Mono
		}

		// Token: 0x020000AB RID: 171
		public enum SceneViewFilterMode
		{
			// Token: 0x0400020F RID: 527
			Off,
			// Token: 0x04000210 RID: 528
			ShowFiltered
		}

		// Token: 0x020000AC RID: 172
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public enum RenderRequestMode
		{
			// Token: 0x04000212 RID: 530
			None,
			// Token: 0x04000213 RID: 531
			ObjectId,
			// Token: 0x04000214 RID: 532
			Depth,
			// Token: 0x04000215 RID: 533
			VertexNormal,
			// Token: 0x04000216 RID: 534
			WorldPosition,
			// Token: 0x04000217 RID: 535
			EntityId,
			// Token: 0x04000218 RID: 536
			BaseColor,
			// Token: 0x04000219 RID: 537
			SpecularColor,
			// Token: 0x0400021A RID: 538
			Metallic,
			// Token: 0x0400021B RID: 539
			Emission,
			// Token: 0x0400021C RID: 540
			Normal,
			// Token: 0x0400021D RID: 541
			Smoothness,
			// Token: 0x0400021E RID: 542
			Occlusion,
			// Token: 0x0400021F RID: 543
			DiffuseColor
		}

		// Token: 0x020000AD RID: 173
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public enum RenderRequestOutputSpace
		{
			// Token: 0x04000221 RID: 545
			ScreenSpace = -1,
			// Token: 0x04000222 RID: 546
			UV0,
			// Token: 0x04000223 RID: 547
			UV1,
			// Token: 0x04000224 RID: 548
			UV2,
			// Token: 0x04000225 RID: 549
			UV3,
			// Token: 0x04000226 RID: 550
			UV4,
			// Token: 0x04000227 RID: 551
			UV5,
			// Token: 0x04000228 RID: 552
			UV6,
			// Token: 0x04000229 RID: 553
			UV7,
			// Token: 0x0400022A RID: 554
			UV8
		}

		// Token: 0x020000AE RID: 174
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public struct RenderRequest
		{
			// Token: 0x0400022B RID: 555
			private readonly Camera.RenderRequestMode m_CameraRenderMode;

			// Token: 0x0400022C RID: 556
			private readonly RenderTexture m_ResultRT;

			// Token: 0x0400022D RID: 557
			private readonly Camera.RenderRequestOutputSpace m_OutputSpace;
		}

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x06000452 RID: 1106
		public delegate void CameraCallback(Camera cam);
	}
}

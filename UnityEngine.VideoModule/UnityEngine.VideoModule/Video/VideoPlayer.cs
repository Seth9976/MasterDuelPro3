using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	[RequireComponent(typeof(Transform))]
	[RequiredByNativeCode]
	public sealed class VideoPlayer : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		public VideoSource source
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_source_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_source_Injected(intPtr, value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		public VideoTimeUpdateMode timeUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		[NativeName("VideoUrl")]
		public unsafe string url
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VideoPlayer.get_url_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
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
					VideoPlayer.set_url_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021E8 File Offset: 0x000003E8
		[NativeName("VideoClip")]
		public VideoClip clip
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<VideoClip>(VideoPlayer.get_clip_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_clip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<VideoClip>(value));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002234 File Offset: 0x00000434
		public VideoRenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_renderMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		public bool canSetTimeUpdateMode
		{
			[NativeName("CanSetTimeUpdateMode")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTimeUpdateMode_Injected(intPtr);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022A4 File Offset: 0x000004A4
		[NativeHeader("Runtime/Camera/Camera.h")]
		public Camera targetCamera
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Camera>(VideoPlayer.get_targetCamera_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCamera_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(value));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022F4 File Offset: 0x000004F4
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		public RenderTexture targetTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(VideoPlayer.get_targetTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(value));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002344 File Offset: 0x00000544
		[NativeHeader("Runtime/Graphics/Renderer.h")]
		public Renderer targetMaterialRenderer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Renderer>(VideoPlayer.get_targetMaterialRenderer_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetMaterialRenderer_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Renderer>(value));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000023AC File Offset: 0x000005AC
		public unsafe string targetMaterialProperty
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VideoPlayer.get_targetMaterialProperty_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
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
					VideoPlayer.set_targetMaterialProperty_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002434 File Offset: 0x00000634
		public VideoAspectRatio aspectRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_aspectRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_aspectRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000247C File Offset: 0x0000067C
		public float targetCameraAlpha
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_targetCameraAlpha_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCameraAlpha_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024A0 File Offset: 0x000006A0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000024C4 File Offset: 0x000006C4
		public Video3DLayout targetCamera3DLayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_targetCamera3DLayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCamera3DLayout_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024E8 File Offset: 0x000006E8
		[NativeHeader("Runtime/Graphics/Texture.h")]
		public Texture texture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(VideoPlayer.get_texture_Injected(intPtr));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002510 File Offset: 0x00000710
		public void Prepare()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Prepare_Injected(intPtr);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002534 File Offset: 0x00000734
		public bool isPrepared
		{
			[NativeName("IsPrepared")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPrepared_Injected(intPtr);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002558 File Offset: 0x00000758
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000257C File Offset: 0x0000077C
		public bool waitForFirstFrame
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_waitForFirstFrame_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_waitForFirstFrame_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025A0 File Offset: 0x000007A0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025C4 File Offset: 0x000007C4
		public bool playOnAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_playOnAwake_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_playOnAwake_Injected(intPtr, value);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025E8 File Offset: 0x000007E8
		public void Play()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Play_Injected(intPtr);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000260C File Offset: 0x0000080C
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Pause_Injected(intPtr);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002630 File Offset: 0x00000830
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Stop_Injected(intPtr);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002654 File Offset: 0x00000854
		public bool isPlaying
		{
			[NativeName("IsPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002678 File Offset: 0x00000878
		public bool isPaused
		{
			[NativeName("IsPaused")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPaused_Injected(intPtr);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000269C File Offset: 0x0000089C
		public bool canSetTime
		{
			[NativeName("CanSetTime")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTime_Injected(intPtr);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026C0 File Offset: 0x000008C0
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026E4 File Offset: 0x000008E4
		[NativeName("SecPosition")]
		public double time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002708 File Offset: 0x00000908
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000272C File Offset: 0x0000092C
		[NativeName("FramePosition")]
		public long frame
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frame_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_frame_Injected(intPtr, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002750 File Offset: 0x00000950
		public double clockTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_clockTime_Injected(intPtr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002774 File Offset: 0x00000974
		public bool canStep
		{
			[NativeName("CanStep")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canStep_Injected(intPtr);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002798 File Offset: 0x00000998
		public void StepForward()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.StepForward_Injected(intPtr);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027BC File Offset: 0x000009BC
		public bool canSetPlaybackSpeed
		{
			[NativeName("CanSetPlaybackSpeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetPlaybackSpeed_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002804 File Offset: 0x00000A04
		public float playbackSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_playbackSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_playbackSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000284C File Offset: 0x00000A4C
		[NativeName("Loop")]
		public bool isLooping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isLooping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_isLooping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002870 File Offset: 0x00000A70
		[Obsolete("VideoPlayer.canSetTimeSource is deprecated. Use canSetTimeUpdateMode instead. (UnityUpgradable) -> canSetTimeUpdateMode")]
		public bool canSetTimeSource
		{
			[NativeName("CanSetTimeSource")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTimeSource_Injected(intPtr);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000028B8 File Offset: 0x00000AB8
		[Obsolete("VideoPlayer.timeSource is deprecated. Use timeUpdateMode instead. (UnityUpgradable) -> timeUpdateMode")]
		public VideoTimeSource timeSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeSource_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028DC File Offset: 0x00000ADC
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002900 File Offset: 0x00000B00
		public VideoTimeReference timeReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeReference_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002924 File Offset: 0x00000B24
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002948 File Offset: 0x00000B48
		public double externalReferenceTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_externalReferenceTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_externalReferenceTime_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000296C File Offset: 0x00000B6C
		public bool canSetSkipOnDrop
		{
			[NativeName("CanSetSkipOnDrop")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetSkipOnDrop_Injected(intPtr);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002990 File Offset: 0x00000B90
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000029B4 File Offset: 0x00000BB4
		public bool skipOnDrop
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_skipOnDrop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_skipOnDrop_Injected(intPtr, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029D8 File Offset: 0x00000BD8
		public ulong frameCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frameCount_Injected(intPtr);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000029FC File Offset: 0x00000BFC
		public float frameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frameRate_Injected(intPtr);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A20 File Offset: 0x00000C20
		[NativeName("Duration")]
		public double length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A44 File Offset: 0x00000C44
		public uint width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_width_Injected(intPtr);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A68 File Offset: 0x00000C68
		public uint height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_height_Injected(intPtr);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A8C File Offset: 0x00000C8C
		public uint pixelAspectRatioNumerator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_pixelAspectRatioNumerator_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public uint pixelAspectRatioDenominator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_pixelAspectRatioDenominator_Injected(intPtr);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public ushort audioTrackCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_audioTrackCount_Injected(intPtr);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public string GetAudioLanguageCode(ushort trackIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VideoPlayer.GetAudioLanguageCode_Injected(intPtr, trackIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B38 File Offset: 0x00000D38
		public ushort GetAudioChannelCount(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetAudioChannelCount_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B5C File Offset: 0x00000D5C
		public uint GetAudioSampleRate(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetAudioSampleRate_Injected(intPtr, trackIndex);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004A RID: 74
		public static extern ushort controlledAudioTrackMaxCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002B98 File Offset: 0x00000D98
		public ushort controlledAudioTrackCount
		{
			get
			{
				return this.GetControlledAudioTrackCount();
			}
			set
			{
				int maxNumTracks = (int)VideoPlayer.controlledAudioTrackMaxCount;
				bool flag = (int)value > maxNumTracks;
				if (flag)
				{
					throw new ArgumentException(string.Format("Cannot control more than {0} tracks.", maxNumTracks), "value");
				}
				this.SetControlledAudioTrackCount(value);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private ushort GetControlledAudioTrackCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetControlledAudioTrackCount_Injected(intPtr);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BFC File Offset: 0x00000DFC
		private void SetControlledAudioTrackCount(ushort value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetControlledAudioTrackCount_Injected(intPtr, value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C20 File Offset: 0x00000E20
		public void EnableAudioTrack(ushort trackIndex, bool enabled)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.EnableAudioTrack_Injected(intPtr, trackIndex, enabled);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C44 File Offset: 0x00000E44
		public bool IsAudioTrackEnabled(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.IsAudioTrackEnabled_Injected(intPtr, trackIndex);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C68 File Offset: 0x00000E68
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002C8C File Offset: 0x00000E8C
		public VideoAudioOutputMode audioOutputMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_audioOutputMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_audioOutputMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public bool canSetDirectAudioVolume
		{
			[NativeName("CanSetDirectAudioVolume")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetDirectAudioVolume_Injected(intPtr);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public float GetDirectAudioVolume(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetDirectAudioVolume_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public void SetDirectAudioVolume(ushort trackIndex, float volume)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetDirectAudioVolume_Injected(intPtr, trackIndex, volume);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D1C File Offset: 0x00000F1C
		public bool GetDirectAudioMute(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetDirectAudioMute_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D40 File Offset: 0x00000F40
		public void SetDirectAudioMute(ushort trackIndex, bool mute)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetDirectAudioMute_Injected(intPtr, trackIndex, mute);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D64 File Offset: 0x00000F64
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		public AudioSource GetTargetAudioSource(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AudioSource>(VideoPlayer.GetTargetAudioSource_Injected(intPtr, trackIndex));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D8C File Offset: 0x00000F8C
		public void SetTargetAudioSource(ushort trackIndex, AudioSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetTargetAudioSource_Injected(intPtr, trackIndex, Object.MarshalledUnityObject.Marshal<AudioSource>(source));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005A RID: 90 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x00002DF0 File Offset: 0x00000FF0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler prepareCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00002E28 File Offset: 0x00001028
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x00002E60 File Offset: 0x00001060
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler loopPointReached;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005E RID: 94 RVA: 0x00002E98 File Offset: 0x00001098
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x00002ED0 File Offset: 0x000010D0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler started;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000060 RID: 96 RVA: 0x00002F08 File Offset: 0x00001108
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00002F40 File Offset: 0x00001140
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler frameDropped;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000062 RID: 98 RVA: 0x00002F78 File Offset: 0x00001178
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x00002FB0 File Offset: 0x000011B0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.ErrorEventHandler errorReceived;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000064 RID: 100 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x00003020 File Offset: 0x00001220
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler seekCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000066 RID: 102 RVA: 0x00003058 File Offset: 0x00001258
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x00003090 File Offset: 0x00001290
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.TimeEventHandler clockResyncOccurred;

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030C8 File Offset: 0x000012C8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000030EC File Offset: 0x000012EC
		public bool sendFrameReadyEvents
		{
			[NativeName("AreFrameReadyEventsEnabled")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_sendFrameReadyEvents_Injected(intPtr);
			}
			[NativeName("EnableFrameReadyEvents")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_sendFrameReadyEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600006A RID: 106 RVA: 0x00003110 File Offset: 0x00001310
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00003148 File Offset: 0x00001348
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.FrameReadyEventHandler frameReady;

		// Token: 0x0600006C RID: 108 RVA: 0x00003180 File Offset: 0x00001380
		[RequiredByNativeCode]
		private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.prepareCompleted != null;
			if (flag)
			{
				source.prepareCompleted(source);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031A8 File Offset: 0x000013A8
		[RequiredByNativeCode]
		private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
		{
			bool flag = source.frameReady != null;
			if (flag)
			{
				source.frameReady(source, frameIdx);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000031D4 File Offset: 0x000013D4
		[RequiredByNativeCode]
		private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.loopPointReached != null;
			if (flag)
			{
				source.loopPointReached(source);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000031FC File Offset: 0x000013FC
		[RequiredByNativeCode]
		private static void InvokeStartedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.started != null;
			if (flag)
			{
				source.started(source);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003224 File Offset: 0x00001424
		[RequiredByNativeCode]
		private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.frameDropped != null;
			if (flag)
			{
				source.frameDropped(source);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000324C File Offset: 0x0000144C
		[RequiredByNativeCode]
		private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
		{
			bool flag = source.errorReceived != null;
			if (flag)
			{
				source.errorReceived(source, errorStr);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003278 File Offset: 0x00001478
		[RequiredByNativeCode]
		private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.seekCompleted != null;
			if (flag)
			{
				source.seekCompleted(source);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032A0 File Offset: 0x000014A0
		[RequiredByNativeCode]
		private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
		{
			bool flag = source.clockResyncOccurred != null;
			if (flag)
			{
				source.clockResyncOccurred(source, seconds);
			}
		}

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoSource get_source_Injected(IntPtr _unity_self);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_source_Injected(IntPtr _unity_self, VideoSource value);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeUpdateMode get_timeUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeUpdateMode_Injected(IntPtr _unity_self, VideoTimeUpdateMode value);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_url_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_url_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_clip_Injected(IntPtr _unity_self);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clip_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoRenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, VideoRenderMode value);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTimeUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetCamera_Injected(IntPtr _unity_self);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetMaterialRenderer_Injected(IntPtr _unity_self);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetMaterialRenderer_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetMaterialProperty_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetMaterialProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoAspectRatio get_aspectRatio_Injected(IntPtr _unity_self);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aspectRatio_Injected(IntPtr _unity_self, VideoAspectRatio value);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_targetCameraAlpha_Injected(IntPtr _unity_self);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCameraAlpha_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Video3DLayout get_targetCamera3DLayout_Injected(IntPtr _unity_self);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera3DLayout_Injected(IntPtr _unity_self, Video3DLayout value);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_texture_Injected(IntPtr _unity_self);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Prepare_Injected(IntPtr _unity_self);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPrepared_Injected(IntPtr _unity_self);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_waitForFirstFrame_Injected(IntPtr _unity_self);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_waitForFirstFrame_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_playOnAwake_Injected(IntPtr _unity_self);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playOnAwake_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPaused_Injected(IntPtr _unity_self);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTime_Injected(IntPtr _unity_self);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_time_Injected(IntPtr _unity_self);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, double value);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long get_frame_Injected(IntPtr _unity_self);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frame_Injected(IntPtr _unity_self, long value);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_clockTime_Injected(IntPtr _unity_self);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canStep_Injected(IntPtr _unity_self);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StepForward_Injected(IntPtr _unity_self);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetPlaybackSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_playbackSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playbackSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isLooping_Injected(IntPtr _unity_self);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isLooping_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTimeSource_Injected(IntPtr _unity_self);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeSource get_timeSource_Injected(IntPtr _unity_self);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeSource_Injected(IntPtr _unity_self, VideoTimeSource value);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeReference get_timeReference_Injected(IntPtr _unity_self);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeReference_Injected(IntPtr _unity_self, VideoTimeReference value);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_externalReferenceTime_Injected(IntPtr _unity_self);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_externalReferenceTime_Injected(IntPtr _unity_self, double value);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetSkipOnDrop_Injected(IntPtr _unity_self);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_skipOnDrop_Injected(IntPtr _unity_self);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skipOnDrop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_frameCount_Injected(IntPtr _unity_self);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frameRate_Injected(IntPtr _unity_self);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_length_Injected(IntPtr _unity_self);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_width_Injected(IntPtr _unity_self);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_height_Injected(IntPtr _unity_self);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioNumerator_Injected(IntPtr _unity_self);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioDenominator_Injected(IntPtr _unity_self);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort get_audioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAudioLanguageCode_Injected(IntPtr _unity_self, ushort trackIndex, out ManagedSpanWrapper ret);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetAudioChannelCount_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetAudioSampleRate_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetControlledAudioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetControlledAudioTrackCount_Injected(IntPtr _unity_self, ushort value);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableAudioTrack_Injected(IntPtr _unity_self, ushort trackIndex, bool enabled);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAudioTrackEnabled_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoAudioOutputMode get_audioOutputMode_Injected(IntPtr _unity_self);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_audioOutputMode_Injected(IntPtr _unity_self, VideoAudioOutputMode value);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetDirectAudioVolume_Injected(IntPtr _unity_self);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDirectAudioVolume_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirectAudioVolume_Injected(IntPtr _unity_self, ushort trackIndex, float volume);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDirectAudioMute_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirectAudioMute_Injected(IntPtr _unity_self, ushort trackIndex, bool mute);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTargetAudioSource_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetAudioSource_Injected(IntPtr _unity_self, ushort trackIndex, IntPtr source);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sendFrameReadyEvents_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sendFrameReadyEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x060000CC RID: 204
		public delegate void EventHandler(VideoPlayer source);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x060000CE RID: 206
		public delegate void ErrorEventHandler(VideoPlayer source, string message);

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x060000D0 RID: 208
		public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x060000D2 RID: 210
		public delegate void TimeEventHandler(VideoPlayer source, double seconds);
	}
}

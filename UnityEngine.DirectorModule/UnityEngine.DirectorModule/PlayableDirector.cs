using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/Director/PlayableDirector.h")]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[RequiredByNativeCode]
	public class PlayableDirector : Behaviour, IExposedPropertyTable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F0 File Offset: 0x000002F0
		public PlayState state
		{
			get
			{
				return this.GetPlayState();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002108 File Offset: 0x00000308
		public DirectorWrapMode extrapolationMode
		{
			get
			{
				return this.GetWrapMode();
			}
			set
			{
				this.SetWrapMode(value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public PlayableAsset playableAsset
		{
			get
			{
				return this.Internal_GetPlayableAsset() as PlayableAsset;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000214C File Offset: 0x0000034C
		public PlayableGraph playableGraph
		{
			get
			{
				return this.GetGraphHandle();
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002164 File Offset: 0x00000364
		public bool playOnAwake
		{
			set
			{
				this.SetPlayOnAwake(value);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216F File Offset: 0x0000036F
		internal void Play(FrameRate frameRate)
		{
			this.PlayOnFrame(frameRate);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002179 File Offset: 0x00000379
		public void SetGenericBinding(Object key, Object value)
		{
			this.Internal_SetGenericBinding(key, value);
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002188 File Offset: 0x00000388
		public DirectorUpdateMode timeUpdateMode
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlayableDirector.set_timeUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021D0 File Offset: 0x000003D0
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021AC File Offset: 0x000003AC
		public double time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlayableDirector.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000003F4
		public double duration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_duration_Injected(intPtr);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002218 File Offset: 0x00000418
		[NativeThrows]
		public void Evaluate()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Evaluate_Injected(intPtr);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000223C File Offset: 0x0000043C
		[NativeThrows]
		private void PlayOnFrame(FrameRate frameRate)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.PlayOnFrame_Injected(intPtr, ref frameRate);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002260 File Offset: 0x00000460
		[NativeThrows]
		public void Play()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Play_Injected(intPtr);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002284 File Offset: 0x00000484
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Stop_Injected(intPtr);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022A8 File Offset: 0x000004A8
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Pause_Injected(intPtr);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CC File Offset: 0x000004CC
		public Object GetReferenceValue(PropertyName id, out bool idValid)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableDirector.GetReferenceValue_Injected(intPtr, ref id, out idValid));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F8 File Offset: 0x000004F8
		[NativeMethod("GetBindingFor")]
		public Object GetGenericBinding(Object key)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableDirector.GetGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key)));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002328 File Offset: 0x00000528
		private PlayState GetPlayState()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.GetPlayState_Injected(intPtr);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000234C File Offset: 0x0000054C
		private void SetWrapMode(DirectorWrapMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetWrapMode_Injected(intPtr, mode);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002370 File Offset: 0x00000570
		private DirectorWrapMode GetWrapMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.GetWrapMode_Injected(intPtr);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002394 File Offset: 0x00000594
		private PlayableGraph GetGraphHandle()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableGraph playableGraph;
			PlayableDirector.GetGraphHandle_Injected(intPtr, out playableGraph);
			return playableGraph;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023BC File Offset: 0x000005BC
		private void SetPlayOnAwake(bool on)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetPlayOnAwake_Injected(intPtr, on);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023E0 File Offset: 0x000005E0
		[NativeThrows]
		private void Internal_SetGenericBinding(Object key, Object value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Internal_SetGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key), Object.MarshalledUnityObject.Marshal<Object>(value));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002410 File Offset: 0x00000610
		private ScriptableObject Internal_GetPlayableAsset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(PlayableDirector.Internal_GetPlayableAsset_Injected(intPtr));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000020 RID: 32 RVA: 0x00002438 File Offset: 0x00000638
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002470 File Offset: 0x00000670
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PlayableDirector> stopped;

		// Token: 0x06000022 RID: 34 RVA: 0x000024A8 File Offset: 0x000006A8
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPlay()
		{
			bool flag = this.played != null;
			if (flag)
			{
				this.played(this);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024D0 File Offset: 0x000006D0
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPause()
		{
			bool flag = this.paused != null;
			if (flag)
			{
				this.paused(this);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024F8 File Offset: 0x000006F8
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorStop()
		{
			bool flag = this.stopped != null;
			if (flag)
			{
				this.stopped(this);
			}
		}

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeUpdateMode_Injected(IntPtr _unity_self, DirectorUpdateMode value);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, double value);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_time_Injected(IntPtr _unity_self);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_duration_Injected(IntPtr _unity_self);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Evaluate_Injected(IntPtr _unity_self);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOnFrame_Injected(IntPtr _unity_self, [In] ref FrameRate frameRate);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self);

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetReferenceValue_Injected(IntPtr _unity_self, [In] ref PropertyName id, out bool idValid);

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGenericBinding_Injected(IntPtr _unity_self, IntPtr key);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayState GetPlayState_Injected(IntPtr _unity_self);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWrapMode_Injected(IntPtr _unity_self, DirectorWrapMode mode);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorWrapMode GetWrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphHandle_Injected(IntPtr _unity_self, out PlayableGraph ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayOnAwake_Injected(IntPtr _unity_self, bool on);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetGenericBinding_Injected(IntPtr _unity_self, IntPtr key, IntPtr value);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetPlayableAsset_Injected(IntPtr _unity_self);

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<PlayableDirector> played;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<PlayableDirector> paused;
	}
}

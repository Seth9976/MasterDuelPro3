using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClip.bindings.h")]
	[NativeType("Modules/Animation/AnimationClip.h")]
	public sealed class AnimationClip : Motion
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000026CC File Offset: 0x000008CC
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x0600002A RID: 42
		[FreeFunction("AnimationClipBindings::Internal_CreateAnimationClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x0600002B RID: 43 RVA: 0x000026DD File Offset: 0x000008DD
		public void SampleAnimation(GameObject go, float time)
		{
			AnimationClip.SampleAnimation(go, this, time, this.wrapMode);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026F0 File Offset: 0x000008F0
		[FreeFunction]
		[NativeHeader("Modules/Animation/AnimationUtility.h")]
		internal static void SampleAnimation([NotNull] GameObject go, [NotNull] AnimationClip clip, float inTime, WrapMode wrapMode)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(clip);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			AnimationClip.SampleAnimation_Injected(intPtr, intPtr2, inTime, wrapMode);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000274C File Offset: 0x0000094C
		[NativeProperty("Length", false, TargetType.Function)]
		public float length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002770 File Offset: 0x00000970
		[NativeProperty("StartTime", false, TargetType.Function)]
		internal float startTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_startTime_Injected(intPtr);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002794 File Offset: 0x00000994
		[NativeProperty("StopTime", false, TargetType.Function)]
		internal float stopTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_stopTime_Injected(intPtr);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027B8 File Offset: 0x000009B8
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000027DC File Offset: 0x000009DC
		[NativeProperty("SampleRate", false, TargetType.Function)]
		public float frameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_frameRate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_frameRate_Injected(intPtr, value);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002800 File Offset: 0x00000A00
		[FreeFunction("AnimationClipBindings::Internal_SetCurve", HasExplicitThis = true)]
		public unsafe void SetCurve([NotNull] string relativePath, [NotNull] Type type, [NotNull] string propertyName, AnimationCurve curve)
		{
			if (relativePath == null)
			{
				ThrowHelper.ThrowArgumentNullException(relativePath, "relativePath");
			}
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException(propertyName, "propertyName");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(relativePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = relativePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = propertyName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				AnimationClip.SetCurve_Injected(intPtr, ref managedSpanWrapper, type, ref managedSpanWrapper2, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028D8 File Offset: 0x00000AD8
		public void EnsureQuaternionContinuity()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.EnsureQuaternionContinuity_Injected(intPtr);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028FC File Offset: 0x00000AFC
		public void ClearCurves()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.ClearCurves_Injected(intPtr);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002920 File Offset: 0x00000B20
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002944 File Offset: 0x00000B44
		[NativeProperty("WrapMode", false, TargetType.Function)]
		public WrapMode wrapMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002968 File Offset: 0x00000B68
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002990 File Offset: 0x00000B90
		[NativeProperty("Bounds", false, TargetType.Function)]
		public Bounds localBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				AnimationClip.get_localBounds_Injected(intPtr, out bounds);
				return bounds;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_localBounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029B4 File Offset: 0x00000BB4
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029D8 File Offset: 0x00000BD8
		public bool legacy
		{
			[NativeMethod("IsLegacy")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_legacy_Injected(intPtr);
			}
			[NativeMethod("SetLegacy")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationClip.set_legacy_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029FC File Offset: 0x00000BFC
		public bool humanMotion
		{
			[NativeMethod("IsHumanMotion")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_humanMotion_Injected(intPtr);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A20 File Offset: 0x00000C20
		public bool empty
		{
			[NativeMethod("IsEmpty")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_empty_Injected(intPtr);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A44 File Offset: 0x00000C44
		public bool hasGenericRootTransform
		{
			[NativeMethod("HasGenericRootTransform")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasGenericRootTransform_Injected(intPtr);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A68 File Offset: 0x00000C68
		public bool hasMotionFloatCurves
		{
			[NativeMethod("HasMotionFloatCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasMotionFloatCurves_Injected(intPtr);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A8C File Offset: 0x00000C8C
		public bool hasMotionCurves
		{
			[NativeMethod("HasMotionCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasMotionCurves_Injected(intPtr);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public bool hasRootCurves
		{
			[NativeMethod("HasRootCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasRootCurves_Injected(intPtr);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AD4 File Offset: 0x00000CD4
		internal bool hasRootMotion
		{
			[FreeFunction(Name = "AnimationClipBindings::Internal_GetHasRootMotion", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationClip.get_hasRootMotion_Injected(intPtr);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public void AddEvent(AnimationEvent evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			AnimationEventBlittable animationEventBlittable = AnimationEventBlittable.FromAnimationEvent(evt);
			this.AddEventInternal(animationEventBlittable);
			animationEventBlittable.Dispose();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B38 File Offset: 0x00000D38
		[FreeFunction(Name = "AnimationClipBindings::AddEventInternal", HasExplicitThis = true)]
		private void AddEventInternal(object evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.AddEventInternal_Injected(intPtr, evt);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B5C File Offset: 0x00000D5C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002B8C File Offset: 0x00000D8C
		public unsafe AnimationEvent[] events
		{
			get
			{
				IntPtr blittableEventsPointer;
				int numberOfEvents;
				this.GetEventsInternal(out blittableEventsPointer, out numberOfEvents);
				AnimationEvent[] animationEvents = AnimationEventBlittable.PointerToAnimationEvents(blittableEventsPointer, numberOfEvents);
				AnimationEventBlittable.DisposeEvents(blittableEventsPointer, numberOfEvents);
				return animationEvents;
			}
			set
			{
				using (NativeArray<AnimationEventBlittable> blittableEvents = new NativeArray<AnimationEventBlittable>(value.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory))
				{
					AnimationEventBlittable* pBlittableEvents = (AnimationEventBlittable*)blittableEvents.GetUnsafePtr<AnimationEventBlittable>();
					AnimationEventBlittable.FromAnimationEvents(value, pBlittableEvents);
					this.SetEventsInternal((void*)pBlittableEvents, blittableEvents.Length);
					for (int i = 0; i < value.Length; i++)
					{
						pBlittableEvents->Dispose();
						pBlittableEvents++;
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C0C File Offset: 0x00000E0C
		[FreeFunction(Name = "AnimationClipBindings::SetEventsInternal", HasExplicitThis = true)]
		private unsafe void SetEventsInternal(void* data, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.SetEventsInternal_Injected(intPtr, data, length);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C30 File Offset: 0x00000E30
		[FreeFunction(Name = "AnimationClipBindings::GetEventsInternal", HasExplicitThis = true)]
		private void GetEventsInternal(out IntPtr values, out int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AnimationClip.GetEventsInternal_Injected(intPtr, out values, out size);
		}

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SampleAnimation_Injected(IntPtr go, IntPtr clip, float inTime, WrapMode wrapMode);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startTime_Injected(IntPtr _unity_self);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stopTime_Injected(IntPtr _unity_self);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frameRate_Injected(IntPtr _unity_self);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frameRate_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCurve_Injected(IntPtr _unity_self, ref ManagedSpanWrapper relativePath, Type type, ref ManagedSpanWrapper propertyName, IntPtr curve);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnsureQuaternionContinuity_Injected(IntPtr _unity_self);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCurves_Injected(IntPtr _unity_self);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localBounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_legacy_Injected(IntPtr _unity_self);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_legacy_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_humanMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_empty_Injected(IntPtr _unity_self);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasGenericRootTransform_Injected(IntPtr _unity_self);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasMotionFloatCurves_Injected(IntPtr _unity_self);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasMotionCurves_Injected(IntPtr _unity_self);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootCurves_Injected(IntPtr _unity_self);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddEventInternal_Injected(IntPtr _unity_self, object evt);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetEventsInternal_Injected(IntPtr _unity_self, void* data, int length);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetEventsInternal_Injected(IntPtr _unity_self, out IntPtr values, out int size);
	}
}

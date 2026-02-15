using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[RequiredByNativeCode]
	[Serializable]
	internal struct AnimationEventBlittable : IDisposable
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000021C0 File Offset: 0x000003C0
		internal static AnimationEventBlittable FromAnimationEvent(AnimationEvent animationEvent)
		{
			bool flag = AnimationEventBlittable.s_handlePool == null;
			if (flag)
			{
				AnimationEventBlittable.s_handlePool = new GCHandlePool();
			}
			GCHandlePool handlePool = AnimationEventBlittable.s_handlePool;
			return new AnimationEventBlittable
			{
				m_Time = animationEvent.m_Time,
				m_FunctionName = handlePool.AllocHandleIfNotNull(animationEvent.m_FunctionName),
				m_StringParameter = handlePool.AllocHandleIfNotNull(animationEvent.m_StringParameter),
				m_ObjectReferenceParameter = handlePool.AllocHandleIfNotNull(animationEvent.m_ObjectReferenceParameter),
				m_FloatParameter = animationEvent.m_FloatParameter,
				m_IntParameter = animationEvent.m_IntParameter,
				m_MessageOptions = animationEvent.m_MessageOptions,
				m_Source = animationEvent.m_Source,
				m_StateSender = handlePool.AllocHandleIfNotNull(animationEvent.m_StateSender),
				m_AnimatorStateInfo = animationEvent.m_AnimatorStateInfo,
				m_AnimatorClipInfo = animationEvent.m_AnimatorClipInfo
			};
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022A4 File Offset: 0x000004A4
		internal unsafe static void FromAnimationEvents(AnimationEvent[] animationEvents, AnimationEventBlittable* animationEventBlittables)
		{
			bool flag = AnimationEventBlittable.s_handlePool == null;
			if (flag)
			{
				AnimationEventBlittable.s_handlePool = new GCHandlePool();
			}
			GCHandlePool handlePool = AnimationEventBlittable.s_handlePool;
			AnimationEventBlittable* animationEventBlittable = animationEventBlittables;
			foreach (AnimationEvent animationEvent in animationEvents)
			{
				animationEventBlittable->m_Time = animationEvent.m_Time;
				animationEventBlittable->m_FunctionName = handlePool.AllocHandleIfNotNull(animationEvent.m_FunctionName);
				animationEventBlittable->m_StringParameter = handlePool.AllocHandleIfNotNull(animationEvent.m_StringParameter);
				animationEventBlittable->m_ObjectReferenceParameter = handlePool.AllocHandleIfNotNull(animationEvent.m_ObjectReferenceParameter);
				animationEventBlittable->m_FloatParameter = animationEvent.m_FloatParameter;
				animationEventBlittable->m_IntParameter = animationEvent.m_IntParameter;
				animationEventBlittable->m_MessageOptions = animationEvent.m_MessageOptions;
				animationEventBlittable->m_Source = animationEvent.m_Source;
				animationEventBlittable->m_StateSender = handlePool.AllocHandleIfNotNull(animationEvent.m_StateSender);
				animationEventBlittable->m_AnimatorStateInfo = animationEvent.m_AnimatorStateInfo;
				animationEventBlittable->m_AnimatorClipInfo = animationEvent.m_AnimatorClipInfo;
				animationEventBlittable++;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023A4 File Offset: 0x000005A4
		[RequiredByNativeCode]
		internal unsafe static AnimationEvent PointerToAnimationEvent(IntPtr animationEventBlittable)
		{
			return AnimationEventBlittable.ToAnimationEvent(*(AnimationEventBlittable*)(void*)animationEventBlittable);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023C8 File Offset: 0x000005C8
		internal unsafe static AnimationEvent[] PointerToAnimationEvents(IntPtr animationEventBlittableArray, int size)
		{
			AnimationEvent[] animationEvents = new AnimationEvent[size];
			AnimationEventBlittable* animationEventsBlittable = (AnimationEventBlittable*)(void*)animationEventBlittableArray;
			for (int i = 0; i < size; i++)
			{
				animationEvents[i] = AnimationEventBlittable.PointerToAnimationEvent((IntPtr)((void*)(animationEventsBlittable + i)));
			}
			return animationEvents;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002418 File Offset: 0x00000618
		internal unsafe static void DisposeEvents(IntPtr animationEventBlittableArray, int size)
		{
			AnimationEventBlittable* animationEventsBlittable = (AnimationEventBlittable*)(void*)animationEventBlittableArray;
			for (int i = 0; i < size; i++)
			{
				animationEventsBlittable[i].Dispose();
			}
			AnimationEventBlittable.FreeEventsInternal(animationEventBlittableArray);
		}

		// Token: 0x06000025 RID: 37
		[FreeFunction(Name = "AnimationClipBindings::FreeEventsInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeEventsInternal(IntPtr value);

		// Token: 0x06000026 RID: 38 RVA: 0x00002458 File Offset: 0x00000658
		internal static AnimationEvent ToAnimationEvent(AnimationEventBlittable animationEventBlittable)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.m_Time = animationEventBlittable.m_Time;
			bool flag = animationEventBlittable.m_FunctionName != IntPtr.Zero;
			if (flag)
			{
				animationEvent.m_FunctionName = (string)UnsafeUtility.As<IntPtr, GCHandle>(ref animationEventBlittable.m_FunctionName).Target;
			}
			bool flag2 = animationEventBlittable.m_StringParameter != IntPtr.Zero;
			if (flag2)
			{
				animationEvent.m_StringParameter = (string)UnsafeUtility.As<IntPtr, GCHandle>(ref animationEventBlittable.m_StringParameter).Target;
			}
			bool flag3 = animationEventBlittable.m_ObjectReferenceParameter != IntPtr.Zero;
			if (flag3)
			{
				animationEvent.m_ObjectReferenceParameter = (Object)UnsafeUtility.As<IntPtr, GCHandle>(ref animationEventBlittable.m_ObjectReferenceParameter).Target;
			}
			animationEvent.m_FloatParameter = animationEventBlittable.m_FloatParameter;
			animationEvent.m_IntParameter = animationEventBlittable.m_IntParameter;
			animationEvent.m_MessageOptions = animationEventBlittable.m_MessageOptions;
			animationEvent.m_Source = animationEventBlittable.m_Source;
			bool flag4 = animationEventBlittable.m_StateSender != IntPtr.Zero;
			if (flag4)
			{
				animationEvent.m_StateSender = (AnimationState)UnsafeUtility.As<IntPtr, GCHandle>(ref animationEventBlittable.m_StateSender).Target;
			}
			animationEvent.m_AnimatorStateInfo = animationEventBlittable.m_AnimatorStateInfo;
			animationEvent.m_AnimatorClipInfo = animationEventBlittable.m_AnimatorClipInfo;
			return animationEvent;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000258C File Offset: 0x0000078C
		public unsafe void Dispose()
		{
			bool flag = AnimationEventBlittable.s_handlePool == null;
			if (flag)
			{
				AnimationEventBlittable.s_handlePool = new GCHandlePool();
			}
			GCHandlePool handlePool = AnimationEventBlittable.s_handlePool;
			bool flag2 = this.m_FunctionName != IntPtr.Zero;
			if (flag2)
			{
				handlePool.Free(*UnsafeUtility.As<IntPtr, GCHandle>(ref this.m_FunctionName));
			}
			bool flag3 = this.m_StringParameter != IntPtr.Zero;
			if (flag3)
			{
				handlePool.Free(*UnsafeUtility.As<IntPtr, GCHandle>(ref this.m_StringParameter));
			}
			bool flag4 = this.m_ObjectReferenceParameter != IntPtr.Zero;
			if (flag4)
			{
				handlePool.Free(*UnsafeUtility.As<IntPtr, GCHandle>(ref this.m_ObjectReferenceParameter));
			}
			bool flag5 = this.m_StateSender != IntPtr.Zero;
			if (flag5)
			{
				handlePool.Free(*UnsafeUtility.As<IntPtr, GCHandle>(ref this.m_StateSender));
			}
		}

		// Token: 0x0400000A RID: 10
		internal float m_Time;

		// Token: 0x0400000B RID: 11
		internal IntPtr m_FunctionName;

		// Token: 0x0400000C RID: 12
		internal IntPtr m_StringParameter;

		// Token: 0x0400000D RID: 13
		internal IntPtr m_ObjectReferenceParameter;

		// Token: 0x0400000E RID: 14
		internal float m_FloatParameter;

		// Token: 0x0400000F RID: 15
		internal int m_IntParameter;

		// Token: 0x04000010 RID: 16
		internal int m_MessageOptions;

		// Token: 0x04000011 RID: 17
		internal AnimationEventSource m_Source;

		// Token: 0x04000012 RID: 18
		internal IntPtr m_StateSender;

		// Token: 0x04000013 RID: 19
		internal AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x04000014 RID: 20
		internal AnimatorClipInfo m_AnimatorClipInfo;

		// Token: 0x04000015 RID: 21
		[ThreadStatic]
		private static GCHandlePool s_handlePool;
	}
}

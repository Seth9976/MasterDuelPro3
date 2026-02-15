using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/Animation/Animation.h")]
	[DefaultMember("Item")]
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000205C File Offset: 0x0000025C
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animation.Stop_Injected(intPtr);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002080 File Offset: 0x00000280
		[ExcludeFromDocs]
		public bool Play()
		{
			return this.Play(PlayMode.StopSameLayer);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000209C File Offset: 0x0000029C
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020B8 File Offset: 0x000002B8
		[NativeName("Play")]
		private bool PlayDefaultAnimation(PlayMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.PlayDefaultAnimation_Injected(intPtr, mode);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020DC File Offset: 0x000002DC
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020F4 File Offset: 0x000002F4
		[FreeFunction("AnimationBindings::GetStateAtIndex", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		internal AnimationState GetStateAtIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.GetStateAtIndex_Injected(intPtr, index);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002118 File Offset: 0x00000318
		[NativeName("GetAnimationStateCount")]
		internal int GetStateCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.GetStateCount_Injected(intPtr);
		}

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PlayDefaultAnimation_Injected(IntPtr _unity_self, PlayMode mode);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationState GetStateAtIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetStateCount_Injected(IntPtr _unity_self);

		// Token: 0x02000007 RID: 7
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x0600001B RID: 27 RVA: 0x0000213A File Offset: 0x0000033A
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002154 File Offset: 0x00000354
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x0600001D RID: 29 RVA: 0x00002178 File Offset: 0x00000378
			public bool MoveNext()
			{
				int childCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < childCount;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000021AD File Offset: 0x000003AD
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x04000008 RID: 8
			private Animation m_Outer;

			// Token: 0x04000009 RID: 9
			private int m_CurrentIndex = -1;
		}
	}
}

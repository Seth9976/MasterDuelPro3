using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000312 RID: 786
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Director/PlayableOutputHandle.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct PlayableOutputHandle : IEquatable<PlayableOutputHandle>
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0002DC2C File Offset: 0x0002BE2C
		public static PlayableOutputHandle Null
		{
			get
			{
				return PlayableOutputHandle.m_Null;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0002DC44 File Offset: 0x0002BE44
		[VisibleToOtherModules]
		internal bool IsPlayableOutputOfType<T>()
		{
			return this.GetPlayableOutputType() == typeof(T);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0002DC98 File Offset: 0x0002BE98
		public static bool operator ==(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		public override bool Equals(object p)
		{
			return p is PlayableOutputHandle && this.Equals((PlayableOutputHandle)p);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0002DCE0 File Offset: 0x0002BEE0
		public bool Equals(PlayableOutputHandle other)
		{
			return PlayableOutputHandle.CompareVersion(this, other);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0002DD00 File Offset: 0x0002BF00
		internal static bool CompareVersion(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x060015D9 RID: 5593
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsValid();

		// Token: 0x060015DA RID: 5594
		[FreeFunction("PlayableOutputHandleBindings::GetPlayableOutputType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetPlayableOutputType();

		// Token: 0x060015DB RID: 5595 RVA: 0x0002DD38 File Offset: 0x0002BF38
		[FreeFunction("PlayableOutputHandleBindings::SetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal void SetReferenceObject(Object target)
		{
			PlayableOutputHandle.SetReferenceObject_Injected(ref this, Object.MarshalledUnityObject.Marshal<Object>(target));
		}

		// Token: 0x060015DC RID: 5596
		[FreeFunction("PlayableOutputHandleBindings::SetUserData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetUserData([Writable] Object target);

		// Token: 0x060015DD RID: 5597 RVA: 0x0002DD54 File Offset: 0x0002BF54
		[FreeFunction("PlayableOutputHandleBindings::GetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetSourcePlayable()
		{
			PlayableHandle playableHandle;
			PlayableOutputHandle.GetSourcePlayable_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0002DD6C File Offset: 0x0002BF6C
		[FreeFunction("PlayableOutputHandleBindings::SetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSourcePlayable(PlayableHandle target, int port)
		{
			PlayableOutputHandle.SetSourcePlayable_Injected(ref this, ref target, port);
		}

		// Token: 0x060015DF RID: 5599
		[FreeFunction("PlayableOutputHandleBindings::GetSourceOutputPort", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetSourceOutputPort();

		// Token: 0x060015E0 RID: 5600
		[FreeFunction("PlayableOutputHandleBindings::SetWeight", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetWeight(float weight);

		// Token: 0x060015E1 RID: 5601 RVA: 0x0002DD84 File Offset: 0x0002BF84
		[FreeFunction("PlayableOutputHandleBindings::PushNotification", HasExplicitThis = true, ThrowsException = true)]
		internal void PushNotification(PlayableHandle origin, INotification notification, object context)
		{
			PlayableOutputHandle.PushNotification_Injected(ref this, ref origin, notification, context);
		}

		// Token: 0x060015E2 RID: 5602
		[FreeFunction("PlayableOutputHandleBindings::AddNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddNotificationReceiver(INotificationReceiver receiver);

		// Token: 0x060015E4 RID: 5604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReferenceObject_Injected(ref PlayableOutputHandle _unity_self, IntPtr target);

		// Token: 0x060015E5 RID: 5605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, out PlayableHandle ret);

		// Token: 0x060015E6 RID: 5606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, [In] ref PlayableHandle target, int port);

		// Token: 0x060015E7 RID: 5607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PushNotification_Injected(ref PlayableOutputHandle _unity_self, [In] ref PlayableHandle origin, INotification notification, object context);

		// Token: 0x0400082C RID: 2092
		internal IntPtr m_Handle;

		// Token: 0x0400082D RID: 2093
		internal uint m_Version;

		// Token: 0x0400082E RID: 2094
		private static readonly PlayableOutputHandle m_Null = default(PlayableOutputHandle);
	}
}

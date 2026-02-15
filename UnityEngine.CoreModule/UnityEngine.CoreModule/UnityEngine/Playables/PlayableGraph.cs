using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200030D RID: 781
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/PlayableGraph.bindings.h")]
	public struct PlayableGraph
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0002D634 File Offset: 0x0002B834
		public Playable GetRootPlayable(int index)
		{
			PlayableHandle handle = this.GetRootPlayableInternal(index);
			return new Playable(handle);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0002D654 File Offset: 0x0002B854
		public bool Connect<U, V>(U source, int sourceOutputPort, V destination, int destinationInputPort) where U : struct, IPlayable where V : struct, IPlayable
		{
			return this.ConnectInternal(source.GetHandle(), sourceOutputPort, destination.GetHandle(), destinationInputPort);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0002D689 File Offset: 0x0002B889
		public void Evaluate()
		{
			this.Evaluate(0f);
		}

		// Token: 0x06001585 RID: 5509
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsValid();

		// Token: 0x06001586 RID: 5510
		[FreeFunction("PlayableGraphBindings::IsPlaying", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying();

		// Token: 0x06001587 RID: 5511
		[FreeFunction("PlayableGraphBindings::Evaluate", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Evaluate([DefaultValue("0")] float deltaTime);

		// Token: 0x06001588 RID: 5512
		[FreeFunction("PlayableGraphBindings::GetResolver", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IExposedPropertyTable GetResolver();

		// Token: 0x06001589 RID: 5513
		[FreeFunction("PlayableGraphBindings::GetPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPlayableCount();

		// Token: 0x0600158A RID: 5514
		[FreeFunction("PlayableGraphBindings::GetRootPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetRootPlayableCount();

		// Token: 0x0600158B RID: 5515 RVA: 0x0002D698 File Offset: 0x0002B898
		[FreeFunction("PlayableGraphBindings::SynchronizeEvaluation", HasExplicitThis = true, ThrowsException = true)]
		internal void SynchronizeEvaluation(PlayableGraph playable)
		{
			PlayableGraph.SynchronizeEvaluation_Injected(ref this, ref playable);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		[FreeFunction("PlayableGraphBindings::CreatePlayableHandle", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle CreatePlayableHandle()
		{
			PlayableHandle playableHandle;
			PlayableGraph.CreatePlayableHandle_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		[FreeFunction("PlayableGraphBindings::CreateScriptOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal unsafe bool CreateScriptOutputInternal(string name, out PlayableOutputHandle handle)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = PlayableGraph.CreateScriptOutputInternal_Injected(ref this, ref managedSpanWrapper, out handle);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0002D720 File Offset: 0x0002B920
		[FreeFunction("PlayableGraphBindings::GetRootPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetRootPlayableInternal(int index)
		{
			PlayableHandle playableHandle;
			PlayableGraph.GetRootPlayableInternal_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x0600158F RID: 5519
		[FreeFunction("PlayableGraphBindings::IsMatchFrameRateEnabled", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsMatchFrameRateEnabled();

		// Token: 0x06001590 RID: 5520 RVA: 0x0002D738 File Offset: 0x0002B938
		[FreeFunction("PlayableGraphBindings::GetFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal FrameRate GetFrameRate()
		{
			FrameRate frameRate;
			PlayableGraph.GetFrameRate_Injected(ref this, out frameRate);
			return frameRate;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0002D750 File Offset: 0x0002B950
		[FreeFunction("PlayableGraphBindings::ConnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool ConnectInternal(PlayableHandle source, int sourceOutputPort, PlayableHandle destination, int destinationInputPort)
		{
			return PlayableGraph.ConnectInternal_Injected(ref this, ref source, sourceOutputPort, ref destination, destinationInputPort);
		}

		// Token: 0x06001592 RID: 5522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SynchronizeEvaluation_Injected(ref PlayableGraph _unity_self, [In] ref PlayableGraph playable);

		// Token: 0x06001593 RID: 5523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreatePlayableHandle_Injected(ref PlayableGraph _unity_self, out PlayableHandle ret);

		// Token: 0x06001594 RID: 5524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateScriptOutputInternal_Injected(ref PlayableGraph _unity_self, ref ManagedSpanWrapper name, out PlayableOutputHandle handle);

		// Token: 0x06001595 RID: 5525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootPlayableInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06001596 RID: 5526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFrameRate_Injected(ref PlayableGraph _unity_self, out FrameRate ret);

		// Token: 0x06001597 RID: 5527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConnectInternal_Injected(ref PlayableGraph _unity_self, [In] ref PlayableHandle source, int sourceOutputPort, [In] ref PlayableHandle destination, int destinationInputPort);

		// Token: 0x04000821 RID: 2081
		internal IntPtr m_Handle;

		// Token: 0x04000822 RID: 2082
		internal uint m_Version;
	}
}

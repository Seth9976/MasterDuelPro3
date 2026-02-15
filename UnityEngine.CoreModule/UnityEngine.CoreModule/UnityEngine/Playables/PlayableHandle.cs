using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200030F RID: 783
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		// Token: 0x06001598 RID: 5528 RVA: 0x0002D76C File Offset: 0x0002B96C
		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				object playable = this.GetScriptInstance();
				bool flag2 = playable == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)playable);
				}
			}
			return t;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0002D7BC File Offset: 0x0002B9BC
		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0002D81C File Offset: 0x0002BA1C
		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool flag2;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0002D848 File Offset: 0x0002BA48
		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float num;
			if (flag)
			{
				num = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0002D878 File Offset: 0x0002BA78
		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0002D894 File Offset: 0x0002BA94
		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0002D90C File Offset: 0x0002BB0C
		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0002D944 File Offset: 0x0002BB44
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0002D960 File Offset: 0x0002BB60
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 && acceptAny;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = inputIndex < 0;
				if (flag3)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag4 = this.GetInputCount() <= inputIndex;
				if (flag4)
				{
					throw new IndexOutOfRangeException(string.Concat(new string[]
					{
						"inputIndex ",
						inputIndex.ToString(),
						" is greater than the number of available inputs (",
						this.GetInputCount().ToString(),
						")."
					}));
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060015A5 RID: 5541
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsValid();

		// Token: 0x060015A6 RID: 5542
		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetPlayableType();

		// Token: 0x060015A7 RID: 5543
		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetScriptInstance(object scriptInstance);

		// Token: 0x060015A8 RID: 5544
		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern PlayState GetPlayState();

		// Token: 0x060015A9 RID: 5545
		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Play();

		// Token: 0x060015AA RID: 5546
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Pause();

		// Token: 0x060015AB RID: 5547
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetSpeed();

		// Token: 0x060015AC RID: 5548
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSpeed(double value);

		// Token: 0x060015AD RID: 5549
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetTime();

		// Token: 0x060015AE RID: 5550
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTime(double value);

		// Token: 0x060015AF RID: 5551
		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsDone();

		// Token: 0x060015B0 RID: 5552
		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDone(bool value);

		// Token: 0x060015B1 RID: 5553
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetDuration();

		// Token: 0x060015B2 RID: 5554
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDuration(double value);

		// Token: 0x060015B3 RID: 5555
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetPropagateSetTime(bool value);

		// Token: 0x060015B4 RID: 5556 RVA: 0x0002D9EC File Offset: 0x0002BBEC
		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph playableGraph;
			PlayableHandle.GetGraph_Injected(ref this, out playableGraph);
			return playableGraph;
		}

		// Token: 0x060015B5 RID: 5557
		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetInputCount();

		// Token: 0x060015B6 RID: 5558
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetInputCount(int value);

		// Token: 0x060015B7 RID: 5559 RVA: 0x0002DA04 File Offset: 0x0002BC04
		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		// Token: 0x060015B8 RID: 5560
		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetPreviousTime();

		// Token: 0x060015B9 RID: 5561
		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTraversalMode(PlayableTraversalMode mode);

		// Token: 0x060015BA RID: 5562
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern DirectorWrapMode GetTimeWrapMode();

		// Token: 0x060015BB RID: 5563
		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTimeWrapMode(DirectorWrapMode mode);

		// Token: 0x060015BC RID: 5564
		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetScriptInstance();

		// Token: 0x060015BD RID: 5565 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060015BE RID: 5566
		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetInputWeightFromIndex(int index, float weight);

		// Token: 0x060015BF RID: 5567
		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetInputWeightFromIndex(int index);

		// Token: 0x060015C1 RID: 5569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		// Token: 0x060015C2 RID: 5570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, [In] ref PlayableHandle input, float weight);

		// Token: 0x060015C3 RID: 5571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x04000827 RID: 2087
		internal IntPtr m_Handle;

		// Token: 0x04000828 RID: 2088
		internal uint m_Version;

		// Token: 0x04000829 RID: 2089
		private static readonly PlayableHandle m_Null = default(PlayableHandle);
	}
}

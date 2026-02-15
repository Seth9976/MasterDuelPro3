using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000005 RID: 5
	[StaticAccessor("PlayableSystemsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Director/ScriptBindings/PlayableSystems.bindings.h")]
	internal static class PlayableSystems
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000252C File Offset: 0x0000072C
		private static int CombineTypeAndIndex(int typeIndex, PlayableSystems.PlayableSystemStage stage)
		{
			return (typeIndex << 16) | (int)stage;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002544 File Offset: 0x00000744
		[RequiredByNativeCode]
		private unsafe static bool Internal_CallSystemDelegate(int systemIndex, PlayableSystems.PlayableSystemStage stage, IntPtr outputsPtr, int numOutputs)
		{
			PlayableOutputHandle* outputs = (PlayableOutputHandle*)(void*)outputsPtr;
			int combinedId = PlayableSystems.CombineTypeAndIndex(systemIndex, stage);
			bool systemFound = false;
			PlayableSystems.PlayableSystemDelegate systemDelegate = null;
			PlayableSystems.s_RWLock.EnterReadLock();
			Type systemType;
			bool typeFound = PlayableSystems.s_SystemTypes.TryGetValue(systemIndex, out systemType);
			bool flag = typeFound;
			if (flag)
			{
				systemFound = PlayableSystems.s_Delegates.TryGetValue(combinedId, out systemDelegate) && systemDelegate != null;
			}
			PlayableSystems.s_RWLock.ExitReadLock();
			bool flag2 = !typeFound || !systemFound;
			bool flag3;
			if (flag2)
			{
				flag3 = false;
			}
			else
			{
				PlayableSystems.DataPlayableOutputList outputsArgument = new PlayableSystems.DataPlayableOutputList(outputs, numOutputs);
				systemDelegate(outputsArgument);
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x04000005 RID: 5
		private static Dictionary<int, Type> s_SystemTypes = new Dictionary<int, Type>();

		// Token: 0x04000006 RID: 6
		private static Dictionary<int, PlayableSystems.PlayableSystemDelegate> s_Delegates = new Dictionary<int, PlayableSystems.PlayableSystemDelegate>();

		// Token: 0x04000007 RID: 7
		private static ReaderWriterLockSlim s_RWLock = new ReaderWriterLockSlim();

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x0600003C RID: 60
		public delegate void PlayableSystemDelegate(IReadOnlyList<DataPlayableOutput> outputs);

		// Token: 0x02000007 RID: 7
		public enum PlayableSystemStage : ushort
		{
			// Token: 0x04000009 RID: 9
			FixedUpdate,
			// Token: 0x0400000A RID: 10
			FixedUpdatePostPhysics,
			// Token: 0x0400000B RID: 11
			Update,
			// Token: 0x0400000C RID: 12
			AnimationBegin,
			// Token: 0x0400000D RID: 13
			AnimationEnd,
			// Token: 0x0400000E RID: 14
			LateUpdate,
			// Token: 0x0400000F RID: 15
			Render
		}

		// Token: 0x02000008 RID: 8
		private class DataPlayableOutputList : IReadOnlyList<DataPlayableOutput>, IEnumerable<DataPlayableOutput>, IEnumerable, IReadOnlyCollection<DataPlayableOutput>
		{
			// Token: 0x0600003D RID: 61 RVA: 0x000025FD File Offset: 0x000007FD
			public unsafe DataPlayableOutputList(PlayableOutputHandle* outputs, int count)
			{
				this.m_Outputs = outputs;
				this.m_Count = count;
			}

			// Token: 0x17000009 RID: 9
			public unsafe DataPlayableOutput this[int index]
			{
				get
				{
					bool flag = index >= this.m_Count;
					if (flag)
					{
						throw new IndexOutOfRangeException(string.Format("index {0} is greater than the number of items: {1}", index, this.m_Count));
					}
					bool flag2 = index < 0;
					if (flag2)
					{
						throw new IndexOutOfRangeException("index cannot be negative");
					}
					return new DataPlayableOutput(this.m_Outputs[index]);
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00002688 File Offset: 0x00000888
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002690 File Offset: 0x00000890
			public IEnumerator<DataPlayableOutput> GetEnumerator()
			{
				return new PlayableSystems.DataPlayableOutputList.DataPlayableOutputEnumerator(this);
			}

			// Token: 0x06000041 RID: 65 RVA: 0x000026A8 File Offset: 0x000008A8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000010 RID: 16
			private unsafe PlayableOutputHandle* m_Outputs;

			// Token: 0x04000011 RID: 17
			private int m_Count;

			// Token: 0x02000009 RID: 9
			private class DataPlayableOutputEnumerator : IEnumerator<DataPlayableOutput>, IEnumerator, IDisposable
			{
				// Token: 0x06000042 RID: 66 RVA: 0x000026C0 File Offset: 0x000008C0
				public DataPlayableOutputEnumerator(PlayableSystems.DataPlayableOutputList list)
				{
					this.m_List = list;
					this.m_Index = -1;
				}

				// Token: 0x1700000B RID: 11
				// (get) Token: 0x06000043 RID: 67 RVA: 0x000026D8 File Offset: 0x000008D8
				public DataPlayableOutput Current
				{
					get
					{
						DataPlayableOutput dataPlayableOutput;
						try
						{
							dataPlayableOutput = this.m_List[this.m_Index];
						}
						catch (IndexOutOfRangeException)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return dataPlayableOutput;
					}
				}

				// Token: 0x1700000C RID: 12
				// (get) Token: 0x06000044 RID: 68 RVA: 0x0000271C File Offset: 0x0000091C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06000045 RID: 69 RVA: 0x00002729 File Offset: 0x00000929
				public void Dispose()
				{
					this.m_List = null;
				}

				// Token: 0x06000046 RID: 70 RVA: 0x00002734 File Offset: 0x00000934
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Index < this.m_List.Count;
				}

				// Token: 0x06000047 RID: 71 RVA: 0x00002767 File Offset: 0x00000967
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x04000012 RID: 18
				private PlayableSystems.DataPlayableOutputList m_List;

				// Token: 0x04000013 RID: 19
				private int m_Index;
			}
		}
	}
}

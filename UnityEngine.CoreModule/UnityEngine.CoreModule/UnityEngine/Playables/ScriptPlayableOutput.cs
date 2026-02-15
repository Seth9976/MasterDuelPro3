using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000315 RID: 789
	[RequiredByNativeCode]
	public struct ScriptPlayableOutput : IPlayableOutput
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x0002E108 File Offset: 0x0002C308
		public static ScriptPlayableOutput Create(PlayableGraph graph, string name)
		{
			PlayableOutputHandle handle;
			bool flag = !graph.CreateScriptOutputInternal(name, out handle);
			ScriptPlayableOutput scriptPlayableOutput;
			if (flag)
			{
				scriptPlayableOutput = ScriptPlayableOutput.Null;
			}
			else
			{
				scriptPlayableOutput = new ScriptPlayableOutput(handle);
			}
			return scriptPlayableOutput;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0002E13C File Offset: 0x0002C33C
		internal ScriptPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<ScriptPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a ScriptPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0002E178 File Offset: 0x0002C378
		public static ScriptPlayableOutput Null
		{
			get
			{
				return new ScriptPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0002E194 File Offset: 0x0002C394
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0002E1AC File Offset: 0x0002C3AC
		public static implicit operator PlayableOutput(ScriptPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x04000831 RID: 2097
		private PlayableOutputHandle m_Handle;
	}
}

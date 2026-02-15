using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000314 RID: 788
	public static class ScriptPlayableBinding
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x0002E0C0 File Offset: 0x0002C2C0
		public static PlayableBinding Create(string name, Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}

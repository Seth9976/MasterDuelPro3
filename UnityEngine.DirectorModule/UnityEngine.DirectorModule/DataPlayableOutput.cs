using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayableOutput.bindings.h")]
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayableOutputExtensions.bindings.h")]
	[NativeHeader("Modules/Director/DataPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[RequiredByNativeCode]
	[StaticAccessor("DataPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	internal struct DataPlayableOutput : IPlayableOutput
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal DataPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<DataPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a DataPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		[RequiredByNativeCode]
		private static void Internal_CallOnPlayerChanged(PlayableOutputHandle handle, object previousPlayer, object currentPlayer)
		{
			DataPlayableOutput output = new DataPlayableOutput(handle);
			IDataPlayer previousDataPlayer = previousPlayer as IDataPlayer;
			bool flag = previousDataPlayer != null;
			if (flag)
			{
				previousDataPlayer.Release(output);
			}
			IDataPlayer currentDataPlayer = currentPlayer as IDataPlayer;
			bool flag2 = currentDataPlayer != null;
			if (flag2)
			{
				currentDataPlayer.Bind(output);
			}
		}

		// Token: 0x04000001 RID: 1
		private PlayableOutputHandle m_Handle;
	}
}

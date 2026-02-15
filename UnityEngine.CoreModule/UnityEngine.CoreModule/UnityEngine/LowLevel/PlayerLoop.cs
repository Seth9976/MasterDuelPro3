using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000258 RID: 600
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public class PlayerLoop
	{
		// Token: 0x060014D9 RID: 5337 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
		public static PlayerLoopSystem GetCurrentPlayerLoop()
		{
			PlayerLoopSystemInternal[] intSys = PlayerLoop.GetCurrentPlayerLoopInternal();
			int offset = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(intSys, ref offset);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		public static void SetPlayerLoop(PlayerLoopSystem loop)
		{
			List<PlayerLoopSystemInternal> intSys = new List<PlayerLoopSystemInternal>();
			PlayerLoop.PlayerLoopSystemToInternal(loop, ref intSys);
			PlayerLoop.SetPlayerLoopInternal(intSys.ToArray());
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		private static int PlayerLoopSystemToInternal(PlayerLoopSystem sys, ref List<PlayerLoopSystemInternal> internalSys)
		{
			int idx = internalSys.Count;
			PlayerLoopSystemInternal newSys = new PlayerLoopSystemInternal
			{
				type = sys.type,
				updateDelegate = sys.updateDelegate,
				updateFunction = sys.updateFunction,
				loopConditionFunction = sys.loopConditionFunction,
				numSubSystems = 0
			};
			internalSys.Add(newSys);
			bool flag = sys.subSystemList != null;
			if (flag)
			{
				for (int i = 0; i < sys.subSystemList.Length; i++)
				{
					newSys.numSubSystems += PlayerLoop.PlayerLoopSystemToInternal(sys.subSystemList[i], ref internalSys);
				}
			}
			internalSys[idx] = newSys;
			return newSys.numSubSystems + 1;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		private static PlayerLoopSystem InternalToPlayerLoopSystem(PlayerLoopSystemInternal[] internalSys, ref int offset)
		{
			PlayerLoopSystem sys = new PlayerLoopSystem
			{
				type = internalSys[offset].type,
				updateDelegate = internalSys[offset].updateDelegate,
				updateFunction = internalSys[offset].updateFunction,
				loopConditionFunction = internalSys[offset].loopConditionFunction,
				subSystemList = null
			};
			int num = offset;
			offset = num + 1;
			int idx = num;
			bool flag = internalSys[idx].numSubSystems > 0;
			if (flag)
			{
				List<PlayerLoopSystem> subsys = new List<PlayerLoopSystem>();
				while (offset <= idx + internalSys[idx].numSubSystems)
				{
					subsys.Add(PlayerLoop.InternalToPlayerLoopSystem(internalSys, ref offset));
				}
				sys.subSystemList = subsys.ToArray();
			}
			return sys;
		}

		// Token: 0x060014DD RID: 5341
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayerLoopSystemInternal[] GetCurrentPlayerLoopInternal();

		// Token: 0x060014DE RID: 5342
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayerLoopInternal(PlayerLoopSystemInternal[] loop);
	}
}

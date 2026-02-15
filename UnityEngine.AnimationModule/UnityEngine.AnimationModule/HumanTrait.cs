using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Animation/HumanTrait.h")]
	public class HumanTrait
	{
		// Token: 0x0600023B RID: 571
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexFromMono(int humanId);
	}
}

using System;

namespace Spine.Unity
{
	// Token: 0x0200005A RID: 90
	public interface IAnimationStateComponent : ISpineComponent
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002E9 RID: 745
		AnimationState AnimationState { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EA RID: 746
		// (set) Token: 0x060002EB RID: 747
		bool UnscaledTime { get; set; }
	}
}

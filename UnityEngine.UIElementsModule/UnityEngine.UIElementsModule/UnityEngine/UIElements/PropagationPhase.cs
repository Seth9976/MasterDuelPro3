using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D3 RID: 467
	public enum PropagationPhase
	{
		// Token: 0x04000833 RID: 2099
		None,
		// Token: 0x04000834 RID: 2100
		TrickleDown,
		// Token: 0x04000835 RID: 2101
		BubbleUp = 3,
		// Token: 0x04000836 RID: 2102
		[Obsolete("PropagationPhase.AtTarget has been removed as part of an event propagation simplification. Events now propagate through the TrickleDown phase followed immediately by the BubbleUp phase. Please use TrickleDown or BubbleUp. You can check if the event target is the current element by testing event.target == this in your local callback.", false)]
		AtTarget = 2,
		// Token: 0x04000837 RID: 2103
		[Obsolete("PropagationPhase.DefaultAction has been removed as part of an event propagation simplification. ExecuteDefaultAction now occurs as part of the BubbleUp phase. Please use BubbleUp.", false)]
		DefaultAction = 4,
		// Token: 0x04000838 RID: 2104
		[Obsolete("PropagationPhase.DefaultActionAtTarget has been removed as part of an event propagation simplification. ExecuteDefaultActionAtTarget now occurs as part of the BubbleUp phase. Please use BubbleUp", false)]
		DefaultActionAtTarget
	}
}

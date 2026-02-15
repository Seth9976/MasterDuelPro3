using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x02000233 RID: 563
	internal class PropagationPaths : IDisposable
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x00042F6A File Offset: 0x0004116A
		public PropagationPaths()
		{
			this.trickleDownPath = new List<VisualElement>(8);
			this.bubbleUpPath = new List<VisualElement>(8);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00042F8C File Offset: 0x0004118C
		[NotNull]
		public static PropagationPaths Build(VisualElement elem, EventBase evt, int eventCategories)
		{
			PropagationPaths paths = PropagationPaths.s_Pool.Get();
			bool flag = elem.HasTrickleDownEventInterests(eventCategories);
			if (flag)
			{
				paths.trickleDownPath.Add(elem);
			}
			bool flag2 = elem.HasBubbleUpEventInterests(eventCategories);
			if (flag2)
			{
				paths.bubbleUpPath.Add(elem);
			}
			for (VisualElement ve = elem.nextParentWithEventInterests; ve != null; ve = ve.nextParentWithEventInterests)
			{
				bool flag3 = !ve.HasParentEventInterests(eventCategories);
				if (flag3)
				{
					break;
				}
				bool flag4 = evt.tricklesDown && ve.HasTrickleDownEventInterests(eventCategories);
				if (flag4)
				{
					paths.trickleDownPath.Add(ve);
				}
				bool flag5 = evt.bubbles && ve.HasBubbleUpEventInterests(eventCategories);
				if (flag5)
				{
					paths.bubbleUpPath.Add(ve);
				}
			}
			return paths;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0004305C File Offset: 0x0004125C
		public void Dispose()
		{
			this.bubbleUpPath.Clear();
			this.trickleDownPath.Clear();
			PropagationPaths.s_Pool.Release(this);
		}

		// Token: 0x040008BD RID: 2237
		private static readonly ObjectPool<PropagationPaths> s_Pool = new ObjectPool<PropagationPaths>(() => new PropagationPaths(), 100);

		// Token: 0x040008BE RID: 2238
		public readonly List<VisualElement> trickleDownPath;

		// Token: 0x040008BF RID: 2239
		public readonly List<VisualElement> bubbleUpPath;
	}
}

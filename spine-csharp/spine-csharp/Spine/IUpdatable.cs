using System;

namespace Spine
{
	// Token: 0x02000061 RID: 97
	public interface IUpdatable
	{
		// Token: 0x06000327 RID: 807
		void Update(Skeleton.Physics physics);

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000328 RID: 808
		bool Active { get; }
	}
}

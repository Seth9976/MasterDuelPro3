using System;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000056 RID: 86
	public interface IResourceProvider
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FB RID: 507
		string ProviderId { get; }

		// Token: 0x060001FC RID: 508
		Type GetDefaultType(IResourceLocation location);

		// Token: 0x060001FD RID: 509
		bool CanProvide(Type type, IResourceLocation location);

		// Token: 0x060001FE RID: 510
		void Provide(ProvideHandle provideHandle);

		// Token: 0x060001FF RID: 511
		void Release(IResourceLocation location, object asset);

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000200 RID: 512
		ProviderBehaviourFlags BehaviourFlags { get; }
	}
}

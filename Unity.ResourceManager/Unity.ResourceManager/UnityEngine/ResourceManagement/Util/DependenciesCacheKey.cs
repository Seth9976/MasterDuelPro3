using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200002F RID: 47
	internal sealed class DependenciesCacheKey : IOperationCacheKey, IEquatable<IOperationCacheKey>
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000062EC File Offset: 0x000044EC
		public DependenciesCacheKey(IList<IResourceLocation> dependencies, int dependenciesHash)
		{
			this.m_Dependencies = dependencies;
			this.m_DependenciesHash = dependenciesHash;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006302 File Offset: 0x00004502
		public override int GetHashCode()
		{
			return this.m_DependenciesHash;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000630A File Offset: 0x0000450A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DependenciesCacheKey);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000630A File Offset: 0x0000450A
		public bool Equals(IOperationCacheKey other)
		{
			return this.Equals(other as DependenciesCacheKey);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006318 File Offset: 0x00004518
		private bool Equals(DependenciesCacheKey other)
		{
			return this == other || (other != null && LocationUtils.DependenciesEqual(this.m_Dependencies, other.m_Dependencies));
		}

		// Token: 0x04000082 RID: 130
		private readonly IList<IResourceLocation> m_Dependencies;

		// Token: 0x04000083 RID: 131
		private readonly int m_DependenciesHash;
	}
}

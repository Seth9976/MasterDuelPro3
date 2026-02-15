using System;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200002E RID: 46
	internal sealed class LocationCacheKey : IOperationCacheKey, IEquatable<IOperationCacheKey>
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00006260 File Offset: 0x00004460
		public LocationCacheKey(IResourceLocation location, Type desiredType)
		{
			if (location == null)
			{
				throw new NullReferenceException("Resource location cannot be null.");
			}
			if (desiredType == null)
			{
				throw new NullReferenceException("Desired type cannot be null.");
			}
			this.m_Location = location;
			this.m_DesiredType = desiredType;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006298 File Offset: 0x00004498
		public override int GetHashCode()
		{
			return this.m_Location.Hash(this.m_DesiredType);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000062AB File Offset: 0x000044AB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LocationCacheKey);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000062AB File Offset: 0x000044AB
		public bool Equals(IOperationCacheKey other)
		{
			return this.Equals(other as LocationCacheKey);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000062B9 File Offset: 0x000044B9
		private bool Equals(LocationCacheKey other)
		{
			return this == other || (other != null && LocationUtils.LocationEquals(this.m_Location, other.m_Location) && object.Equals(this.m_DesiredType, other.m_DesiredType));
		}

		// Token: 0x04000080 RID: 128
		private readonly IResourceLocation m_Location;

		// Token: 0x04000081 RID: 129
		private readonly Type m_DesiredType;
	}
}

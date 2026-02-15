using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200057B RID: 1403
	internal struct LayoutCacheData
	{
		// Token: 0x04001382 RID: 4994
		public static LayoutCacheData Default = new LayoutCacheData
		{
			NextCachedMeasurementsIndex = 0U,
			CachedLayout = LayoutCachedMeasurement.Default
		};

		// Token: 0x04001383 RID: 4995
		public uint NextCachedMeasurementsIndex;

		// Token: 0x04001384 RID: 4996
		public FixedBuffer16<LayoutCachedMeasurement> cachedMeasurements;

		// Token: 0x04001385 RID: 4997
		public LayoutCachedMeasurement CachedLayout;
	}
}

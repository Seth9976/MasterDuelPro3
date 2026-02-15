using System;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.Exceptions
{
	// Token: 0x02000017 RID: 23
	public class ProviderException : OperationException
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000441C File Offset: 0x0000261C
		public ProviderException(string message, IResourceLocation location = null, Exception innerException = null)
			: base(message, innerException)
		{
			this.Location = location;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000442D File Offset: 0x0000262D
		public IResourceLocation Location { get; }
	}
}

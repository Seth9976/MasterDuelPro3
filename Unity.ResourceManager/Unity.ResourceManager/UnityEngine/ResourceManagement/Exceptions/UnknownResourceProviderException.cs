using System;
using System.Runtime.Serialization;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.Exceptions
{
	// Token: 0x02000015 RID: 21
	public class UnknownResourceProviderException : ResourceManagerException
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004378 File Offset: 0x00002578
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004380 File Offset: 0x00002580
		public IResourceLocation Location { get; private set; }

		// Token: 0x060000AF RID: 175 RVA: 0x00004389 File Offset: 0x00002589
		public UnknownResourceProviderException(IResourceLocation location)
		{
			this.Location = location;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004398 File Offset: 0x00002598
		public UnknownResourceProviderException()
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043A0 File Offset: 0x000025A0
		public UnknownResourceProviderException(string message)
			: base(message)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000043A9 File Offset: 0x000025A9
		public UnknownResourceProviderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043B3 File Offset: 0x000025B3
		protected UnknownResourceProviderException(SerializationInfo message, StreamingContext context)
			: base(message, context)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000043C0 File Offset: 0x000025C0
		public override string Message
		{
			get
			{
				string[] array = new string[5];
				array[0] = base.Message;
				array[1] = ", ProviderId=";
				array[2] = this.Location.ProviderId;
				array[3] = ", Location=";
				int num = 4;
				IResourceLocation location = this.Location;
				array[num] = ((location != null) ? location.ToString() : null);
				return string.Concat(array);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004414 File Offset: 0x00002614
		public override string ToString()
		{
			return this.Message;
		}
	}
}

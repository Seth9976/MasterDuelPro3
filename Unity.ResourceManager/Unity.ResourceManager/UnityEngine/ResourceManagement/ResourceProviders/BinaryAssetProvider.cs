using System;
using System.ComponentModel;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200004D RID: 77
	[DisplayName("Binary Asset Provider")]
	internal class BinaryAssetProvider<TAdapter> : BinaryDataProvider where TAdapter : BinaryStorageBuffer.ISerializationAdapter, new()
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00008279 File Offset: 0x00006479
		public override object Convert(Type type, byte[] data)
		{
			return new BinaryStorageBuffer.Reader(data, 512, new BinaryStorageBuffer.ISerializationAdapter[]
			{
				new TAdapter()
			}).ReadObject(type, 0U, false);
		}
	}
}

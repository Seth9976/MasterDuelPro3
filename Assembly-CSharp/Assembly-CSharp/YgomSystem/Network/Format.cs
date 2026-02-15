using System;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x0200070E RID: 1806
	public abstract class Format
	{
		// Token: 0x060038AC RID: 14508 RVA: 0x0000216A File Offset: 0x0000036A
		public static Format GetInstance()
		{
			return null;
		}

		// Token: 0x060038AD RID: 14509
		public abstract byte[] Serialize(Dictionary<string, object> dict, byte[] token);

		// Token: 0x060038AE RID: 14510
		public abstract Dictionary<string, object> Deserialize(byte[] bin);

		// Token: 0x060038AF RID: 14511
		public abstract void DeserializeAsync(byte[] bin, Action<Dictionary<string, object>> onfinish);

		// Token: 0x0400324E RID: 12878
		protected static Format _singleInstance;
	}
}

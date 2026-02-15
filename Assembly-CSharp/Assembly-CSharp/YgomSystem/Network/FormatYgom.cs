using System;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x0200070F RID: 1807
	public class FormatYgom : Format
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x0000216A File Offset: 0x0000036A
		public override byte[] Serialize(Dictionary<string, object> dict, byte[] token)
		{
			return null;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x0000216A File Offset: 0x0000036A
		public override Dictionary<string, object> Deserialize(byte[] bin)
		{
			return null;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x0000216D File Offset: 0x0000036D
		public override void DeserializeAsync(byte[] bin, Action<Dictionary<string, object>> onfinish)
		{
		}
	}
}

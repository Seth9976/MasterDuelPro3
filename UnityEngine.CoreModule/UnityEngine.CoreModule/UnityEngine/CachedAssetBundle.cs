using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A1 RID: 161
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00006522 File Offset: 0x00004722
		public CachedAssetBundle(string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006534 File Offset: 0x00004734
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000654C File Offset: 0x0000474C
		public Hash128 hash
		{
			get
			{
				return this.m_Hash;
			}
		}

		// Token: 0x040001EF RID: 495
		private string m_Name;

		// Token: 0x040001F0 RID: 496
		private Hash128 m_Hash;
	}
}

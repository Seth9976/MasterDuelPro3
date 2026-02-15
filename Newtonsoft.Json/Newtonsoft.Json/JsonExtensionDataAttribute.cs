using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002FC3 File Offset: 0x000011C3
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002FCB File Offset: 0x000011CB
		public bool WriteData { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002FD4 File Offset: 0x000011D4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002FDC File Offset: 0x000011DC
		public bool ReadData { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00002FE5 File Offset: 0x000011E5
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}
	}
}

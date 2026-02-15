using System;
using System.IO;

namespace com.adjust.sdk
{
	// Token: 0x02000471 RID: 1137
	public class JSONData : JSONNode
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x0000216D File Offset: 0x0000036D
		public override string Value
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONData(string aData)
		{
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONData(float aData)
		{
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONData(double aData)
		{
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONData(bool aData)
		{
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000F166D File Offset: 0x000EF86D
		public JSONData(int aData)
		{
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString(string aPrefix)
		{
			return null;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x04002738 RID: 10040
		private string m_Data;
	}
}

using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000076 RID: 118
	[Obsolete("use AssetPPtr")]
	public class AssetID
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001809D File Offset: 0x0001629D
		public AssetID(string fileName, long pathID)
		{
			this.fileName = fileName;
			this.pathID = pathID;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000180B8 File Offset: 0x000162B8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is AssetID);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AssetID assetID = (AssetID)obj;
				flag2 = assetID.fileName == this.fileName && assetID.pathID == this.pathID;
			}
			return flag2;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001810C File Offset: 0x0001630C
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.fileName.GetHashCode();
			return num * 23 + this.pathID.GetHashCode();
		}

		// Token: 0x040003F0 RID: 1008
		public string fileName;

		// Token: 0x040003F1 RID: 1009
		public long pathID;
	}
}

using System;

namespace AssetsTools.NET
{
	// Token: 0x02000044 RID: 68
	public class AssetTypeReference
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00010A44 File Offset: 0x0000EC44
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public string ClassName { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00010A55 File Offset: 0x0000EC55
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00010A5D File Offset: 0x0000EC5D
		public string Namespace { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00010A66 File Offset: 0x0000EC66
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public string AsmName { get; set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x00010A77 File Offset: 0x0000EC77
		public AssetTypeReference()
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010A81 File Offset: 0x0000EC81
		public AssetTypeReference(string className, string nameSpace, string asmName)
		{
			this.ClassName = className;
			this.Namespace = nameSpace;
			this.AsmName = asmName;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010AA3 File Offset: 0x0000ECA3
		public void ReadMetadata(AssetsFileReader reader)
		{
			this.ClassName = reader.ReadNullTerminated();
			this.Namespace = reader.ReadNullTerminated();
			this.AsmName = reader.ReadNullTerminated();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010ACD File Offset: 0x0000ECCD
		public void ReadAsset(AssetsFileReader reader)
		{
			this.ClassName = reader.ReadCountStringInt32();
			reader.Align();
			this.Namespace = reader.ReadCountStringInt32();
			reader.Align();
			this.AsmName = reader.ReadCountStringInt32();
			reader.Align();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public void WriteMetadata(AssetsFileWriter writer)
		{
			writer.WriteNullTerminated(this.ClassName);
			writer.WriteNullTerminated(this.Namespace);
			writer.WriteNullTerminated(this.AsmName);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00010B36 File Offset: 0x0000ED36
		public void WriteAsset(AssetsFileWriter writer)
		{
			writer.WriteCountStringInt32(this.ClassName);
			writer.Align();
			writer.WriteCountStringInt32(this.Namespace);
			writer.Align();
			writer.WriteCountStringInt32(this.AsmName);
			writer.Align();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010B78 File Offset: 0x0000ED78
		public override bool Equals(object obj)
		{
			AssetTypeReference assetTypeReference = obj as AssetTypeReference;
			bool flag = assetTypeReference == null;
			return !flag && (this.ClassName == assetTypeReference.ClassName && this.Namespace == assetTypeReference.Namespace) && this.AsmName == assetTypeReference.AsmName;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.ClassName.GetHashCode();
			num = num * 23 + this.Namespace.GetHashCode();
			return num * 23 + this.AsmName.GetHashCode();
		}

		// Token: 0x04000194 RID: 404
		public static readonly AssetTypeReference TERMINUS = new AssetTypeReference("Terminus", "UnityEngine.DMAT", "FAKE_ASM");
	}
}

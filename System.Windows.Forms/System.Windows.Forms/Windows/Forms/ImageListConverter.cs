using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020000D8 RID: 216
	internal class ImageListConverter : ComponentConverter
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00022ECC File Offset: 0x000210CC
		public ImageListConverter()
			: base(typeof(ImageList))
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}

using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200012D RID: 301
	public class SerializedCustomEditorForRenderPipeline
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00013260 File Offset: 0x00011460
		public SerializedCustomEditorForRenderPipeline(BinaryReader reader)
		{
			this.customEditorName = reader.ReadAlignedString();
			this.renderPipelineType = reader.ReadAlignedString();
		}

		// Token: 0x04000840 RID: 2112
		public string customEditorName;

		// Token: 0x04000841 RID: 2113
		public string renderPipelineType;
	}
}

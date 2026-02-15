using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000B5 RID: 181
	public interface INameTransform
	{
		// Token: 0x0600059A RID: 1434
		string TransformFile(string name);

		// Token: 0x0600059B RID: 1435
		string TransformDirectory(string name);
	}
}

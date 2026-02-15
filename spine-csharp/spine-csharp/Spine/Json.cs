using System;
using System.IO;
using SharpJson;

namespace Spine
{
	// Token: 0x02000064 RID: 100
	public static class Json
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000E813 File Offset: 0x0000CA13
		public static object Deserialize(TextReader text)
		{
			return new JsonDecoder
			{
				parseNumbersAsFloat = true
			}.Decode(text.ReadToEnd());
		}
	}
}

using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000046 RID: 70
	public class PathTransformer : INameTransform
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000A763 File Offset: 0x00008963
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				if (!name.EndsWith("/", StringComparison.Ordinal))
				{
					name += "/";
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public string TransformFile(string name)
		{
			if (name != null)
			{
				name = name.Replace("\\", "/");
				name = PathUtils.DropPathRoot(name);
				name = name.Trim(new char[] { '/' });
				for (int i = name.IndexOf("//", StringComparison.Ordinal); i >= 0; i = name.IndexOf("//", StringComparison.Ordinal))
				{
					name = name.Remove(i, 1);
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}
	}
}

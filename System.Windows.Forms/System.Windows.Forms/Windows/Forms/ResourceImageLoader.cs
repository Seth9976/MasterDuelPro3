using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000149 RID: 329
	internal class ResourceImageLoader
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x00039B8C File Offset: 0x00037D8C
		internal static Bitmap Get(string name)
		{
			Stream manifestResourceStream = ResourceImageLoader.assembly.GetManifestResourceStream(name);
			if (manifestResourceStream == null)
			{
				Console.WriteLine("Failed to read {0}", name);
				return null;
			}
			return new Bitmap(manifestResourceStream);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00039BBC File Offset: 0x00037DBC
		internal static Icon GetIcon(string name)
		{
			Stream manifestResourceStream = ResourceImageLoader.assembly.GetManifestResourceStream(name);
			if (manifestResourceStream == null)
			{
				Console.WriteLine("Failed to read {0}", name);
				return null;
			}
			return new Icon(manifestResourceStream);
		}

		// Token: 0x0400084C RID: 2124
		private static Assembly assembly = typeof(ResourceImageLoader).Assembly;
	}
}

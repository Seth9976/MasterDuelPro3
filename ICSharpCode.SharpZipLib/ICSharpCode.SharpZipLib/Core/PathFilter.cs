using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000B9 RID: 185
	public class PathFilter : IScanFilter
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public PathFilter(string filter)
		{
			this.nameFilter_ = new NameFilter(filter);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		public virtual bool IsMatch(string name)
		{
			bool flag = false;
			if (name != null)
			{
				string text = ((name.Length > 0) ? Path.GetFullPath(name) : "");
				flag = this.nameFilter_.IsMatch(text);
			}
			return flag;
		}

		// Token: 0x04000444 RID: 1092
		private readonly NameFilter nameFilter_;
	}
}

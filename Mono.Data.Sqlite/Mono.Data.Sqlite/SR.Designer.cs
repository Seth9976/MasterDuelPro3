using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000025 RID: 37
	[DebuggerNonUserCode]
	internal class SR
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SR.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("SR", typeof(SR).Assembly);
					SR.resourceMan = resourceManager;
				}
				return SR.resourceMan;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000D1FA File Offset: 0x0000B3FA
		internal static string DataTypes
		{
			get
			{
				return SR.ResourceManager.GetString("DataTypes", SR.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000D210 File Offset: 0x0000B410
		internal static string Keywords
		{
			get
			{
				return SR.ResourceManager.GetString("Keywords", SR.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000D226 File Offset: 0x0000B426
		internal static string MetaDataCollections
		{
			get
			{
				return SR.ResourceManager.GetString("MetaDataCollections", SR.resourceCulture);
			}
		}

		// Token: 0x040000BA RID: 186
		private static ResourceManager resourceMan;

		// Token: 0x040000BB RID: 187
		private static CultureInfo resourceCulture;
	}
}

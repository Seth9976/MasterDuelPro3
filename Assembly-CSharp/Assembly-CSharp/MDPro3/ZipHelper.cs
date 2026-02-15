using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace MDPro3
{
	// Token: 0x02001258 RID: 4696
	public class ZipHelper
	{
		// Token: 0x06008A50 RID: 35408 RVA: 0x001127A8 File Offset: 0x001109A8
		public static void Initialize()
		{
			ZipHelper.Dispose();
			if (!Directory.Exists("Expansions"))
			{
				Directory.CreateDirectory("Expansions");
			}
			foreach (string zip in Directory.GetFiles("Expansions", "*.ypk"))
			{
				ZipHelper.zips.Add(new ZipFile(zip));
			}
			foreach (string zip2 in Directory.GetFiles("Expansions", "*.zip"))
			{
				ZipHelper.zips.Add(new ZipFile(zip2));
			}
			ZipHelper.zips.Add(new ZipFile("Data/script.zip"));
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x0011284C File Offset: 0x00110A4C
		public static void Dispose()
		{
			foreach (ZipFile zipFile in ZipHelper.zips)
			{
				zipFile.Dispose();
			}
			ZipHelper.zips.Clear();
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x001128A8 File Offset: 0x00110AA8
		public static List<string> GetAllCdbTempPath()
		{
			List<string> returnValue = new List<string>();
			foreach (ZipFile zip in ZipHelper.zips)
			{
				if (!zip.Name.ToLower().EndsWith("script.zip"))
				{
					foreach (string file in zip.EntryFileNames)
					{
						if (file.ToLower().EndsWith(".cdb"))
						{
							ZipEntry zipEntry = zip[file];
							if (!Directory.Exists("TempFolder/"))
							{
								Directory.CreateDirectory("TempFolder/");
							}
							string tempFile = Path.Combine(Path.GetFullPath("TempFolder/"), file);
							zipEntry.Extract(Path.GetFullPath("TempFolder/"), ExtractExistingFileAction.OverwriteSilently);
							returnValue.Add(tempFile);
						}
					}
				}
			}
			return returnValue;
		}

		// Token: 0x0400C5E7 RID: 50663
		public static List<ZipFile> zips = new List<ZipFile>();
	}
}

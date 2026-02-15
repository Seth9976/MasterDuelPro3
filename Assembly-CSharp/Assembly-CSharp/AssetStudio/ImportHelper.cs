using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Org.Brotli.Dec;

namespace AssetStudio
{
	// Token: 0x02000168 RID: 360
	public static class ImportHelper
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00015C14 File Offset: 0x00013E14
		public static void MergeSplitAssets(string path, bool allDirectories = false)
		{
			foreach (string text in Directory.GetFiles(path, "*.split0", allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
			{
				string destFile = Path.GetFileNameWithoutExtension(text);
				string destPath = Path.GetDirectoryName(text);
				string destFull = Path.Combine(destPath, destFile);
				if (!File.Exists(destFull))
				{
					string[] splitParts = Directory.GetFiles(destPath, destFile + ".split*");
					using (FileStream destStream = File.Create(destFull))
					{
						for (int i = 0; i < splitParts.Length; i++)
						{
							using (FileStream sourceStream = File.OpenRead(destFull + ".split" + i.ToString()))
							{
								sourceStream.CopyTo(destStream);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public static string[] ProcessingSplitFiles(List<string> selectFile)
		{
			List<string> list = (from x in selectFile
				where x.Contains(".split")
				select Path.Combine(Path.GetDirectoryName(x), Path.GetFileNameWithoutExtension(x))).Distinct<string>().ToList<string>();
			selectFile.RemoveAll((string x) => x.Contains(".split"));
			foreach (string file in list)
			{
				if (File.Exists(file))
				{
					selectFile.Add(file);
				}
			}
			return selectFile.Distinct<string>().ToArray<string>();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public static FileReader DecompressGZip(FileReader reader)
		{
			FileReader fileReader;
			try
			{
				MemoryStream stream = new MemoryStream();
				using (GZipStream gs = new GZipStream(reader.BaseStream, CompressionMode.Decompress))
				{
					gs.CopyTo(stream);
				}
				stream.Position = 0L;
				fileReader = new FileReader(reader.FullPath, stream);
			}
			finally
			{
				if (reader != null)
				{
					((IDisposable)reader).Dispose();
				}
			}
			return fileReader;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015E44 File Offset: 0x00014044
		public static FileReader DecompressBrotli(FileReader reader)
		{
			FileReader fileReader;
			try
			{
				MemoryStream stream = new MemoryStream();
				using (BrotliInputStream brotliStream = new BrotliInputStream(reader.BaseStream))
				{
					brotliStream.CopyTo(stream);
				}
				stream.Position = 0L;
				fileReader = new FileReader(reader.FullPath, stream);
			}
			finally
			{
				if (reader != null)
				{
					((IDisposable)reader).Dispose();
				}
			}
			return fileReader;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Better.StreamingAssets;
using UnityEngine;

// Token: 0x02000002 RID: 2
public static class BetterStreamingAssets
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static string Root
	{
		get
		{
			return BetterStreamingAssets.LooseFilesImpl.s_root;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
	public static void Initialize()
	{
		BetterStreamingAssets.LooseFilesImpl.Initialize(Application.dataPath, Application.streamingAssetsPath);
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	// (remove) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
	public static event Func<string, bool> CompressedStreamingAssetFound
	{
		add
		{
		}
		remove
		{
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000206C File Offset: 0x0000026C
	public static bool FileExists(string path)
	{
		BetterStreamingAssets.ReadInfo info;
		return BetterStreamingAssets.LooseFilesImpl.TryGetInfo(path, out info);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	public static bool DirectoryExists(string path)
	{
		return BetterStreamingAssets.LooseFilesImpl.DirectoryExists(path);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000028C
	public static AssetBundleCreateRequest LoadAssetBundleAsync(string path, uint crc = 0U)
	{
		BetterStreamingAssets.ReadInfo info = BetterStreamingAssets.GetInfoOrThrow(path);
		return AssetBundle.LoadFromFileAsync(info.readPath, crc, (ulong)info.offset);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020B4 File Offset: 0x000002B4
	public static AssetBundle LoadAssetBundle(string path, uint crc = 0U)
	{
		BetterStreamingAssets.ReadInfo info = BetterStreamingAssets.GetInfoOrThrow(path);
		return AssetBundle.LoadFromFile(info.readPath, crc, (ulong)info.offset);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020DA File Offset: 0x000002DA
	public static Stream OpenRead(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length == 0)
		{
			throw new ArgumentException("Empty path", "path");
		}
		return BetterStreamingAssets.LooseFilesImpl.OpenRead(path);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
	public static StreamReader OpenText(string path)
	{
		Stream str = BetterStreamingAssets.OpenRead(path);
		StreamReader streamReader;
		try
		{
			streamReader = new StreamReader(str);
		}
		catch (Exception)
		{
			if (str != null)
			{
				str.Dispose();
			}
			throw;
		}
		return streamReader;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002144 File Offset: 0x00000344
	public static string ReadAllText(string path)
	{
		string text;
		using (StreamReader sr = BetterStreamingAssets.OpenText(path))
		{
			text = sr.ReadToEnd();
		}
		return text;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000217C File Offset: 0x0000037C
	public static string[] ReadAllLines(string path)
	{
		List<string> lines = new List<string>();
		using (StreamReader sr = BetterStreamingAssets.OpenText(path))
		{
			string line;
			while ((line = sr.ReadLine()) != null)
			{
				lines.Add(line);
			}
		}
		return lines.ToArray();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021CC File Offset: 0x000003CC
	public static byte[] ReadAllBytes(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length == 0)
		{
			throw new ArgumentException("Empty path", "path");
		}
		return BetterStreamingAssets.LooseFilesImpl.ReadAllBytes(path);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021FA File Offset: 0x000003FA
	public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
	{
		return BetterStreamingAssets.LooseFilesImpl.GetFiles(path, searchPattern, searchOption);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
	public static string[] GetFiles(string path)
	{
		return BetterStreamingAssets.GetFiles(path, null);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000220D File Offset: 0x0000040D
	public static string[] GetFiles(string path, string searchPattern)
	{
		return BetterStreamingAssets.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002218 File Offset: 0x00000418
	private static BetterStreamingAssets.ReadInfo GetInfoOrThrow(string path)
	{
		BetterStreamingAssets.ReadInfo result;
		if (!BetterStreamingAssets.LooseFilesImpl.TryGetInfo(path, out result))
		{
			BetterStreamingAssets.ThrowFileNotFound(path);
		}
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002236 File Offset: 0x00000436
	private static void ThrowFileNotFound(string path)
	{
		throw new FileNotFoundException("File not found", path);
	}

	// Token: 0x02000003 RID: 3
	internal struct ReadInfo
	{
		// Token: 0x04000001 RID: 1
		public string readPath;

		// Token: 0x04000002 RID: 2
		public long size;

		// Token: 0x04000003 RID: 3
		public long offset;

		// Token: 0x04000004 RID: 4
		public uint crc32;
	}

	// Token: 0x02000004 RID: 4
	internal static class LooseFilesImpl
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002243 File Offset: 0x00000443
		public static void Initialize(string dataPath, string streamingAssetsPath)
		{
			BetterStreamingAssets.LooseFilesImpl.s_root = Path.GetFullPath(streamingAssetsPath).Replace('\\', '/').TrimEnd('/');
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002260 File Offset: 0x00000460
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (!Directory.Exists(BetterStreamingAssets.LooseFilesImpl.s_root))
			{
				return BetterStreamingAssets.LooseFilesImpl.s_emptyArray;
			}
			path = PathUtil.NormalizeRelativePath(path, true);
			string[] files = Directory.GetFiles(BetterStreamingAssets.LooseFilesImpl.s_root + path, searchPattern ?? "*", searchOption);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Substring(BetterStreamingAssets.LooseFilesImpl.s_root.Length + 1).Replace('\\', '/');
			}
			return files;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022D4 File Offset: 0x000004D4
		public static bool TryGetInfo(string path, out BetterStreamingAssets.ReadInfo info)
		{
			path = PathUtil.NormalizeRelativePath(path, false);
			info = default(BetterStreamingAssets.ReadInfo);
			string fullPath = BetterStreamingAssets.LooseFilesImpl.s_root + path;
			if (!File.Exists(fullPath))
			{
				return false;
			}
			info.readPath = fullPath;
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002310 File Offset: 0x00000510
		public static bool DirectoryExists(string path)
		{
			string normalized = PathUtil.NormalizeRelativePath(path, false);
			return Directory.Exists(BetterStreamingAssets.LooseFilesImpl.s_root + normalized);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002338 File Offset: 0x00000538
		public static byte[] ReadAllBytes(string path)
		{
			BetterStreamingAssets.ReadInfo info;
			if (!BetterStreamingAssets.LooseFilesImpl.TryGetInfo(path, out info))
			{
				BetterStreamingAssets.ThrowFileNotFound(path);
			}
			return File.ReadAllBytes(info.readPath);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002360 File Offset: 0x00000560
		public static Stream OpenRead(string path)
		{
			BetterStreamingAssets.ReadInfo info;
			if (!BetterStreamingAssets.LooseFilesImpl.TryGetInfo(path, out info))
			{
				BetterStreamingAssets.ThrowFileNotFound(path);
			}
			Stream fileStream = File.OpenRead(info.readPath);
			Stream stream;
			try
			{
				stream = new SubReadOnlyStream(fileStream, false);
			}
			catch (Exception)
			{
				fileStream.Dispose();
				throw;
			}
			return stream;
		}

		// Token: 0x04000005 RID: 5
		public static string s_root;

		// Token: 0x04000006 RID: 6
		private static string[] s_emptyArray = new string[0];
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace AssetsTools.NET.Extra
{
	// Token: 0x0200007A RID: 122
	public class AssetsManager
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00018440 File Offset: 0x00016640
		internal string GetFileLookupKey(string path)
		{
			return Path.GetFullPath(path).ToLower();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00018460 File Offset: 0x00016660
		private void LoadAssetsFileDependencies(AssetsFileInstance fileInst, string path, BundleFileInstance bunInst)
		{
			bool flag = bunInst == null;
			if (flag)
			{
				this.LoadDependencies(fileInst);
			}
			else
			{
				this.LoadBundleDependencies(fileInst, bunInst, Path.GetDirectoryName(path));
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018490 File Offset: 0x00016690
		private AssetsFileInstance LoadAssetsFileCacheless(Stream stream, string path, bool loadDeps, BundleFileInstance bunInst = null)
		{
			AssetsFileInstance assetsFileInstance = new AssetsFileInstance(stream, path);
			assetsFileInstance.parentBundle = bunInst;
			string fileLookupKey = this.GetFileLookupKey(path);
			this.FileLookup[fileLookupKey] = assetsFileInstance;
			this.Files.Add(assetsFileInstance);
			if (loadDeps)
			{
				this.LoadAssetsFileDependencies(assetsFileInstance, path, bunInst);
			}
			bool useQuickLookup = this.UseQuickLookup;
			if (useQuickLookup)
			{
				assetsFileInstance.file.GenerateQuickLookup();
			}
			return assetsFileInstance;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00018504 File Offset: 0x00016704
		public AssetsFileInstance LoadAssetsFile(Stream stream, string path, bool loadDeps, BundleFileInstance bunInst = null)
		{
			string fileLookupKey = this.GetFileLookupKey(path);
			AssetsFileInstance assetsFileInstance;
			bool flag = this.FileLookup.TryGetValue(fileLookupKey, out assetsFileInstance);
			AssetsFileInstance assetsFileInstance2;
			if (flag)
			{
				if (loadDeps)
				{
					this.LoadAssetsFileDependencies(assetsFileInstance, path, bunInst);
				}
				assetsFileInstance2 = assetsFileInstance;
			}
			else
			{
				assetsFileInstance2 = this.LoadAssetsFileCacheless(stream, path, loadDeps, bunInst);
			}
			return assetsFileInstance2;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00018558 File Offset: 0x00016758
		public AssetsFileInstance LoadAssetsFile(FileStream stream, bool loadDeps = false)
		{
			return this.LoadAssetsFileCacheless(stream, stream.Name, loadDeps, null);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001857C File Offset: 0x0001677C
		public AssetsFileInstance LoadAssetsFile(string path, bool loadDeps = false)
		{
			string fileLookupKey = this.GetFileLookupKey(path);
			AssetsFileInstance assetsFileInstance;
			bool flag = this.FileLookup.TryGetValue(fileLookupKey, out assetsFileInstance);
			AssetsFileInstance assetsFileInstance2;
			if (flag)
			{
				assetsFileInstance2 = assetsFileInstance;
			}
			else
			{
				assetsFileInstance2 = this.LoadAssetsFile(File.OpenRead(path), loadDeps);
			}
			return assetsFileInstance2;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000185BC File Offset: 0x000167BC
		public bool UnloadAssetsFile(string path)
		{
			string fileLookupKey = this.GetFileLookupKey(path);
			AssetsFileInstance assetsFileInstance;
			bool flag = this.FileLookup.TryGetValue(fileLookupKey, out assetsFileInstance);
			bool flag2;
			if (flag)
			{
				this.monoTypeTreeTemplateFieldCache.Remove(assetsFileInstance);
				this.monoCldbTemplateFieldCache.Remove(assetsFileInstance);
				this.refTypeManagerCache.Remove(assetsFileInstance);
				this.Files.Remove(assetsFileInstance);
				this.FileLookup.Remove(fileLookupKey);
				assetsFileInstance.file.Close();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001863C File Offset: 0x0001683C
		public bool UnloadAssetsFile(AssetsFileInstance fileInst)
		{
			fileInst.file.Close();
			bool flag = this.Files.Contains(fileInst);
			bool flag2;
			if (flag)
			{
				this.monoTypeTreeTemplateFieldCache.Remove(fileInst);
				this.monoCldbTemplateFieldCache.Remove(fileInst);
				this.refTypeManagerCache.Remove(fileInst);
				string fileLookupKey = this.GetFileLookupKey(fileInst.path);
				this.FileLookup.Remove(fileLookupKey);
				this.Files.Remove(fileInst);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000186C0 File Offset: 0x000168C0
		public bool UnloadAllAssetsFiles(bool clearCache = false)
		{
			if (clearCache)
			{
				this.templateFieldCache.Clear();
				this.monoTemplateFieldCache.Clear();
			}
			this.monoTypeTreeTemplateFieldCache.Clear();
			this.monoCldbTemplateFieldCache.Clear();
			this.refTypeManagerCache.Clear();
			bool flag = this.Files.Count != 0;
			bool flag2;
			if (flag)
			{
				foreach (AssetsFileInstance assetsFileInstance in this.Files)
				{
					assetsFileInstance.file.Close();
				}
				this.Files.Clear();
				this.FileLookup.Clear();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00018798 File Offset: 0x00016998
		public BundleFileInstance LoadBundleFile(Stream stream, string path, bool unpackIfPacked = true)
		{
			string fileLookupKey = this.GetFileLookupKey(path);
			BundleFileInstance bundleFileInstance;
			bool flag = this.BundleLookup.TryGetValue(fileLookupKey, out bundleFileInstance);
			BundleFileInstance bundleFileInstance2;
			if (flag)
			{
				bundleFileInstance2 = bundleFileInstance;
			}
			else
			{
				bundleFileInstance = new BundleFileInstance(stream, path, unpackIfPacked);
				this.Bundles.Add(bundleFileInstance);
				this.BundleLookup[fileLookupKey] = bundleFileInstance;
				bundleFileInstance2 = bundleFileInstance;
			}
			return bundleFileInstance2;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000187F0 File Offset: 0x000169F0
		public BundleFileInstance LoadBundleFile(FileStream stream, bool unpackIfPacked = true)
		{
			return this.LoadBundleFile(stream, Path.GetFullPath(stream.Name), unpackIfPacked);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00018818 File Offset: 0x00016A18
		public BundleFileInstance LoadBundleFile(string path, bool unpackIfPacked = true)
		{
			return this.LoadBundleFile(File.OpenRead(path), unpackIfPacked);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00018838 File Offset: 0x00016A38
		public bool UnloadBundleFile(string path)
		{
			string fileLookupKey = this.GetFileLookupKey(path);
			BundleFileInstance bundleFileInstance;
			bool flag = this.BundleLookup.TryGetValue(fileLookupKey, out bundleFileInstance);
			bool flag2;
			if (flag)
			{
				bundleFileInstance.file.Close();
				foreach (AssetsFileInstance assetsFileInstance in bundleFileInstance.loadedAssetsFiles)
				{
					assetsFileInstance.file.Close();
				}
				this.Bundles.Remove(bundleFileInstance);
				this.BundleLookup.Remove(fileLookupKey);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000188E4 File Offset: 0x00016AE4
		public bool UnloadBundleFile(BundleFileInstance bunInst)
		{
			bunInst.file.Close();
			foreach (AssetsFileInstance assetsFileInstance in bunInst.loadedAssetsFiles)
			{
				this.UnloadAssetsFile(assetsFileInstance);
			}
			bunInst.loadedAssetsFiles.Clear();
			bool flag = this.Bundles.Contains(bunInst);
			bool flag2;
			if (flag)
			{
				string fileLookupKey = this.GetFileLookupKey(bunInst.path);
				this.BundleLookup.Remove(fileLookupKey);
				this.Bundles.Remove(bunInst);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001899C File Offset: 0x00016B9C
		public bool UnloadAllBundleFiles()
		{
			bool flag = this.Bundles.Count != 0;
			bool flag2;
			if (flag)
			{
				foreach (BundleFileInstance bundleFileInstance in this.Bundles)
				{
					bundleFileInstance.file.Close();
					foreach (AssetsFileInstance assetsFileInstance in bundleFileInstance.loadedAssetsFiles)
					{
						this.UnloadAssetsFile(assetsFileInstance);
					}
					bundleFileInstance.loadedAssetsFiles.Clear();
				}
				this.Bundles.Clear();
				this.BundleLookup.Clear();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00018A88 File Offset: 0x00016C88
		public AssetsFileInstance LoadAssetsFileFromBundle(BundleFileInstance bunInst, int index, bool loadDeps = false)
		{
			string text = Path.Combine(bunInst.path, bunInst.file.GetFileName(index));
			string fileLookupKey = this.GetFileLookupKey(text);
			AssetsFileInstance assetsFileInstance;
			bool flag = !this.FileLookup.TryGetValue(fileLookupKey, out assetsFileInstance);
			AssetsFileInstance assetsFileInstance3;
			if (flag)
			{
				bool flag2 = bunInst.file.IsAssetsFile(index);
				if (flag2)
				{
					long num;
					long num2;
					bunInst.file.GetFileRange(index, out num, out num2);
					SegmentStream segmentStream = new SegmentStream(bunInst.DataStream, num, num2);
					AssetsFileInstance assetsFileInstance2 = this.LoadAssetsFile(segmentStream, text, loadDeps, bunInst);
					bunInst.loadedAssetsFiles.Add(assetsFileInstance2);
					assetsFileInstance3 = assetsFileInstance2;
				}
				else
				{
					assetsFileInstance3 = null;
				}
			}
			else
			{
				assetsFileInstance3 = assetsFileInstance;
			}
			return assetsFileInstance3;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00018B30 File Offset: 0x00016D30
		public AssetsFileInstance LoadAssetsFileFromBundle(BundleFileInstance bunInst, string name, bool loadDeps = false)
		{
			int fileIndex = bunInst.file.GetFileIndex(name);
			bool flag = fileIndex < 0;
			AssetsFileInstance assetsFileInstance;
			if (flag)
			{
				assetsFileInstance = null;
			}
			else
			{
				assetsFileInstance = this.LoadAssetsFileFromBundle(bunInst, fileIndex, loadDeps);
			}
			return assetsFileInstance;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00018B64 File Offset: 0x00016D64
		public ClassDatabaseFile LoadClassDatabase(Stream stream)
		{
			this.ClassDatabase = new ClassDatabaseFile();
			this.ClassDatabase.Read(new AssetsFileReader(stream));
			return this.ClassDatabase;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00018B9C File Offset: 0x00016D9C
		public ClassDatabaseFile LoadClassDatabase(string path)
		{
			return this.LoadClassDatabase(File.OpenRead(path));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00018BBC File Offset: 0x00016DBC
		public ClassDatabaseFile LoadClassDatabaseFromPackage(UnityVersion version)
		{
			return this.ClassDatabase = this.ClassPackage.GetClassDatabase(version);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00018BE4 File Offset: 0x00016DE4
		public ClassDatabaseFile LoadClassDatabaseFromPackage(string version)
		{
			return this.ClassDatabase = this.ClassPackage.GetClassDatabase(version);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00018C0C File Offset: 0x00016E0C
		public ClassPackageFile LoadClassPackage(Stream stream)
		{
			this.ClassPackage = new ClassPackageFile();
			this.ClassPackage.Read(new AssetsFileReader(stream));
			return this.ClassPackage;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00018C44 File Offset: 0x00016E44
		public ClassPackageFile LoadClassPackage(string path)
		{
			return this.LoadClassPackage(File.OpenRead(path));
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00018C62 File Offset: 0x00016E62
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00018C6A File Offset: 0x00016E6A
		public bool UseTemplateFieldCache { get; set; } = false;

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00018C73 File Offset: 0x00016E73
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00018C7B File Offset: 0x00016E7B
		public bool UseMonoTemplateFieldCache { get; set; } = false;

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00018C84 File Offset: 0x00016E84
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00018C8C File Offset: 0x00016E8C
		public bool UseRefTypeManagerCache { get; set; } = false;

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00018C95 File Offset: 0x00016E95
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00018C9D File Offset: 0x00016E9D
		public bool UseQuickLookup { get; set; } = false;

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00018CA6 File Offset: 0x00016EA6
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00018CAE File Offset: 0x00016EAE
		public ClassDatabaseFile ClassDatabase { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00018CB7 File Offset: 0x00016EB7
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00018CBF File Offset: 0x00016EBF
		public ClassPackageFile ClassPackage { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00018CC8 File Offset: 0x00016EC8
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public List<AssetsFileInstance> Files { get; private set; } = new List<AssetsFileInstance>();

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00018CD9 File Offset: 0x00016ED9
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00018CE1 File Offset: 0x00016EE1
		public Dictionary<string, AssetsFileInstance> FileLookup { get; private set; } = new Dictionary<string, AssetsFileInstance>();

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00018CEA File Offset: 0x00016EEA
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00018CF2 File Offset: 0x00016EF2
		public List<BundleFileInstance> Bundles { get; private set; } = new List<BundleFileInstance>();

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00018CFB File Offset: 0x00016EFB
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00018D03 File Offset: 0x00016F03
		public Dictionary<string, BundleFileInstance> BundleLookup { get; private set; } = new Dictionary<string, BundleFileInstance>();

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00018D0C File Offset: 0x00016F0C
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00018D14 File Offset: 0x00016F14
		public IMonoBehaviourTemplateGenerator MonoTempGenerator { get; set; } = null;

		// Token: 0x06000485 RID: 1157 RVA: 0x00018D20 File Offset: 0x00016F20
		public void UnloadAll(bool unloadClassData = false)
		{
			this.UnloadAllAssetsFiles(true);
			this.UnloadAllBundleFiles();
			IMonoBehaviourTemplateGenerator monoTempGenerator = this.MonoTempGenerator;
			if (monoTempGenerator != null)
			{
				monoTempGenerator.Dispose();
			}
			if (unloadClassData)
			{
				this.ClassPackage = null;
				this.ClassDatabase = null;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00018D68 File Offset: 0x00016F68
		public void LoadDependencies(AssetsFileInstance ofFile)
		{
			string directoryName = Path.GetDirectoryName(ofFile.path);
			for (int i = 0; i < ofFile.file.Metadata.Externals.Count; i++)
			{
				string depPath = ofFile.file.Metadata.Externals[i].PathName;
				bool flag = depPath == string.Empty;
				if (!flag)
				{
					bool flag2 = this.Files.FindIndex((AssetsFileInstance f) => Path.GetFileName(f.path).ToLower() == Path.GetFileName(depPath).ToLower()) == -1;
					if (flag2)
					{
						string text = Path.Combine(directoryName, depPath);
						string text2 = Path.Combine(directoryName, Path.GetFileName(depPath));
						bool flag3 = File.Exists(text);
						if (flag3)
						{
							this.LoadAssetsFile(text, true);
						}
						else
						{
							bool flag4 = File.Exists(text2);
							if (flag4)
							{
								this.LoadAssetsFile(text2, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00018E68 File Offset: 0x00017068
		public void LoadBundleDependencies(AssetsFileInstance ofFile, BundleFileInstance ofBundle, string path)
		{
			for (int i = 0; i < ofFile.file.Metadata.Externals.Count; i++)
			{
				string depPath = ofFile.file.Metadata.Externals[i].PathName;
				bool flag = this.Files.FindIndex((AssetsFileInstance f) => Path.GetFileName(f.path).ToLower() == Path.GetFileName(depPath).ToLower()) == -1;
				if (flag)
				{
					string bunPath = Path.GetFileName(depPath);
					int num = Array.FindIndex<AssetBundleDirectoryInfo>(ofBundle.file.BlockAndDirInfo.DirectoryInfos, (AssetBundleDirectoryInfo d) => Path.GetFileName(d.Name) == bunPath);
					string text = Path.Combine(path, "..");
					string text2 = Path.Combine(text, depPath);
					string text3 = Path.Combine(text, Path.GetFileName(depPath));
					string text4 = Path.Combine(path, depPath);
					string text5 = Path.Combine(path, Path.GetFileName(depPath));
					bool flag2 = num != -1;
					if (flag2)
					{
						this.LoadAssetsFileFromBundle(ofBundle, num, true);
					}
					else
					{
						bool flag3 = File.Exists(text4);
						if (flag3)
						{
							this.LoadAssetsFile(text4, true);
						}
						else
						{
							bool flag4 = File.Exists(text5);
							if (flag4)
							{
								this.LoadAssetsFile(text5, true);
							}
							else
							{
								bool flag5 = File.Exists(text2);
								if (flag5)
								{
									this.LoadAssetsFile(text2, true);
								}
								else
								{
									bool flag6 = File.Exists(text3);
									if (flag6)
									{
										this.LoadAssetsFile(text3, true);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00019000 File Offset: 0x00017200
		public RefTypeManager GetRefTypeManager(AssetsFileInstance inst)
		{
			RefTypeManager refTypeManager;
			bool flag = this.UseRefTypeManagerCache && this.refTypeManagerCache.TryGetValue(inst, out refTypeManager);
			RefTypeManager refTypeManager2;
			if (flag)
			{
				refTypeManager2 = refTypeManager;
			}
			else
			{
				refTypeManager = new RefTypeManager();
				refTypeManager.FromTypeTree(inst.file.Metadata);
				bool flag2 = this.MonoTempGenerator != null;
				if (flag2)
				{
					refTypeManager.WithMonoTemplateGenerator(inst.file.Metadata, this.MonoTempGenerator, this.UseMonoTemplateFieldCache ? this.monoTemplateFieldCache : null);
				}
				bool useRefTypeManagerCache = this.UseRefTypeManagerCache;
				if (useRefTypeManagerCache)
				{
					this.refTypeManagerCache[inst] = refTypeManager;
				}
				refTypeManager2 = refTypeManager;
			}
			return refTypeManager2;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000190A4 File Offset: 0x000172A4
		public AssetTypeTemplateField GetTemplateBaseField(AssetsFileInstance inst, AssetFileInfo info, AssetReadFlags readFlags = AssetReadFlags.None)
		{
			long absoluteByteStart = info.AbsoluteByteStart;
			ushort scriptIndex = inst.file.GetScriptIndex(info);
			return this.GetTemplateBaseField(inst, inst.file.Reader, absoluteByteStart, info.TypeId, scriptIndex, readFlags);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000190E8 File Offset: 0x000172E8
		public AssetTypeTemplateField GetTemplateBaseField(AssetsFileInstance inst, AssetsFileReader reader, long absByteStart, int typeId, ushort scriptIndex, AssetReadFlags readFlags)
		{
			AssetsFile file = inst.file;
			AssetTypeTemplateField assetTypeTemplateField = null;
			bool typeTreeEnabled = inst.file.Metadata.TypeTreeEnabled;
			bool flag = Net35Polyfill.HasFlag(readFlags, AssetReadFlags.PreferEditor);
			bool flag2 = Net35Polyfill.HasFlag(readFlags, AssetReadFlags.ForceFromCldb);
			bool flag3 = Net35Polyfill.HasFlag(readFlags, AssetReadFlags.SkipMonoBehaviourFields);
			bool flag4 = this.UseTemplateFieldCache && typeId != 114 && this.templateFieldCache.TryGetValue(typeId, out assetTypeTemplateField);
			AssetTypeTemplateField assetTypeTemplateField2;
			if (flag4)
			{
				assetTypeTemplateField2 = assetTypeTemplateField;
			}
			else
			{
				bool flag5 = typeTreeEnabled && !flag2;
				if (flag5)
				{
					bool flag6 = this.UseMonoTemplateFieldCache && typeId == 114;
					if (flag6)
					{
						Dictionary<ushort, AssetTypeTemplateField> dictionary;
						AssetTypeTemplateField assetTypeTemplateField3;
						bool flag7 = this.monoTypeTreeTemplateFieldCache.TryGetValue(inst, out dictionary) && dictionary.TryGetValue(scriptIndex, out assetTypeTemplateField3);
						if (flag7)
						{
							return assetTypeTemplateField3;
						}
					}
					TypeTreeType typeTreeType = file.Metadata.FindTypeTreeTypeByID(typeId, scriptIndex);
					bool flag8 = typeTreeType != null && typeTreeType.Nodes.Count > 0;
					if (flag8)
					{
						assetTypeTemplateField = new AssetTypeTemplateField();
						assetTypeTemplateField.FromTypeTree(typeTreeType);
						bool flag9 = this.UseTemplateFieldCache && typeId != 114;
						if (flag9)
						{
							this.templateFieldCache[typeId] = assetTypeTemplateField;
						}
						else
						{
							bool flag10 = this.UseMonoTemplateFieldCache && (long)typeId == 114L;
							if (flag10)
							{
								Dictionary<ushort, AssetTypeTemplateField> dictionary2;
								bool flag11 = !this.monoTypeTreeTemplateFieldCache.TryGetValue(inst, out dictionary2);
								if (flag11)
								{
									dictionary2 = (this.monoTypeTreeTemplateFieldCache[inst] = new Dictionary<ushort, AssetTypeTemplateField>());
								}
								dictionary2[scriptIndex] = assetTypeTemplateField;
							}
						}
						return assetTypeTemplateField;
					}
				}
				bool flag12 = this.UseTemplateFieldCache && this.UseMonoTemplateFieldCache && typeId == 114;
				if (flag12)
				{
					bool flag13 = this.templateFieldCache.TryGetValue(typeId, out assetTypeTemplateField);
					if (flag13)
					{
						assetTypeTemplateField = assetTypeTemplateField.Clone();
					}
				}
				bool flag14 = assetTypeTemplateField == null;
				if (flag14)
				{
					ClassDatabaseType classDatabaseType = this.ClassDatabase.FindAssetClassByID(typeId);
					bool flag15 = classDatabaseType == null;
					if (flag15)
					{
						return null;
					}
					assetTypeTemplateField = new AssetTypeTemplateField();
					assetTypeTemplateField.FromClassDatabase(this.ClassDatabase, classDatabaseType, flag);
					bool useTemplateFieldCache = this.UseTemplateFieldCache;
					if (useTemplateFieldCache)
					{
						bool flag16 = typeId == 114;
						if (flag16)
						{
							this.templateFieldCache[typeId] = assetTypeTemplateField.Clone();
						}
						else
						{
							this.templateFieldCache[typeId] = assetTypeTemplateField;
						}
					}
				}
				bool flag17 = typeId == 114 && this.MonoTempGenerator != null && !flag3;
				if (flag17)
				{
					AssetTypeValueField assetTypeValueField = assetTypeTemplateField.MakeValue(reader, absByteStart, null);
					AssetPPtr assetPPtr = AssetPPtr.FromField(assetTypeValueField["m_Script"]);
					bool flag18 = !assetPPtr.IsNull();
					if (flag18)
					{
						bool flag19 = assetPPtr.FileId == 0;
						AssetsFileInstance assetsFileInstance;
						if (flag19)
						{
							assetsFileInstance = inst;
						}
						else
						{
							assetsFileInstance = inst.GetDependency(this, assetPPtr.FileId - 1);
						}
						bool flag20 = assetsFileInstance == null;
						if (flag20)
						{
							return assetTypeTemplateField;
						}
						Dictionary<long, AssetTypeTemplateField> dictionary3 = null;
						bool useMonoTemplateFieldCache = this.UseMonoTemplateFieldCache;
						if (useMonoTemplateFieldCache)
						{
							bool flag21 = this.monoCldbTemplateFieldCache.TryGetValue(assetsFileInstance, out dictionary3);
							if (flag21)
							{
								AssetTypeTemplateField assetTypeTemplateField4;
								bool flag22 = dictionary3.TryGetValue(assetPPtr.PathId, out assetTypeTemplateField4);
								if (flag22)
								{
									return assetTypeTemplateField4;
								}
							}
							else
							{
								dictionary3 = (this.monoCldbTemplateFieldCache[assetsFileInstance] = new Dictionary<long, AssetTypeTemplateField>());
							}
						}
						AssetFileInfo assetInfo = assetsFileInstance.file.GetAssetInfo(assetPPtr.PathId);
						long absoluteByteStart = assetInfo.AbsoluteByteStart;
						int typeId2 = assetInfo.TypeId;
						ushort scriptIndex2 = assetsFileInstance.file.GetScriptIndex(assetInfo);
						string text;
						string text2;
						string text3;
						bool monoScriptInfo = this.GetMonoScriptInfo(assetsFileInstance, absoluteByteStart, typeId2, scriptIndex2, out text, out text2, out text3, readFlags);
						bool flag23 = text.EndsWith(".dll");
						if (flag23)
						{
							text = text.Substring(0, text.Length - 4);
						}
						bool flag24 = monoScriptInfo;
						if (flag24)
						{
							AssetTypeReference assetTypeReference = new AssetTypeReference(text3, text2, text);
							bool useMonoTemplateFieldCache2 = this.UseMonoTemplateFieldCache;
							if (useMonoTemplateFieldCache2)
							{
								AssetTypeTemplateField assetTypeTemplateField5;
								bool flag25 = this.monoTemplateFieldCache.TryGetValue(assetTypeReference, out assetTypeTemplateField5);
								if (flag25)
								{
									dictionary3[assetPPtr.PathId] = assetTypeTemplateField5;
									return assetTypeTemplateField5;
								}
							}
							AssetTypeTemplateField templateField = this.MonoTempGenerator.GetTemplateField(assetTypeTemplateField, text, text2, text3, new UnityVersion(file.Metadata.UnityVersion));
							bool flag26 = templateField != null;
							if (flag26)
							{
								assetTypeTemplateField = templateField;
								bool useMonoTemplateFieldCache3 = this.UseMonoTemplateFieldCache;
								if (useMonoTemplateFieldCache3)
								{
									dictionary3[assetPPtr.PathId] = (this.monoTemplateFieldCache[assetTypeReference] = assetTypeTemplateField);
									return assetTypeTemplateField;
								}
							}
						}
					}
				}
				assetTypeTemplateField2 = assetTypeTemplateField;
			}
			return assetTypeTemplateField2;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00019598 File Offset: 0x00017798
		private bool GetMonoScriptInfo(AssetsFileInstance inst, long absFilePos, int typeId, ushort scriptIndex, out string assemblyName, out string nameSpace, out string className, AssetReadFlags readFlags)
		{
			assemblyName = null;
			nameSpace = null;
			className = null;
			AssetTypeTemplateField templateBaseField = this.GetTemplateBaseField(inst, null, absFilePos, typeId, scriptIndex, readFlags);
			bool flag = templateBaseField == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				inst.file.Reader.Position = absFilePos;
				AssetTypeValueField assetTypeValueField = templateBaseField.MakeValue(inst.file.Reader, null);
				assemblyName = assetTypeValueField["m_AssemblyName"].AsString;
				nameSpace = assetTypeValueField["m_Namespace"].AsString;
				className = assetTypeValueField["m_ClassName"].AsString;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00019630 File Offset: 0x00017830
		public AssetTypeTemplateField CreateTemplateBaseField(AssetsFileInstance inst, int id, ushort scriptIndex = 65535)
		{
			AssetsFile file = inst.file;
			AssetTypeTemplateField assetTypeTemplateField = new AssetTypeTemplateField();
			bool typeTreeEnabled = file.Metadata.TypeTreeEnabled;
			if (typeTreeEnabled)
			{
				TypeTreeType typeTreeType = file.Metadata.FindTypeTreeTypeByID(id, scriptIndex);
				assetTypeTemplateField.FromTypeTree(typeTreeType);
			}
			else
			{
				bool flag = id != 114 || scriptIndex == ushort.MaxValue;
				if (flag)
				{
					ClassDatabaseType classDatabaseType = this.ClassDatabase.FindAssetClassByID(id);
					assetTypeTemplateField.FromClassDatabase(this.ClassDatabase, classDatabaseType, false);
				}
				else
				{
					bool flag2 = this.MonoTempGenerator == null;
					if (flag2)
					{
						throw new Exception("MonoTempGenerator must be non-null to create a MonoBehaviour!");
					}
					AssetTypeReference assetsFileScriptInfo = AssetHelper.GetAssetsFileScriptInfo(this, inst, (int)scriptIndex);
					AssetTypeTemplateField templateBaseField = this.GetTemplateBaseField(inst, file.Reader, -1L, 114, scriptIndex, AssetReadFlags.SkipMonoBehaviourFields);
					UnityVersion unityVersion = new UnityVersion(file.Metadata.UnityVersion);
					assetTypeTemplateField = this.MonoTempGenerator.GetTemplateField(templateBaseField, assetsFileScriptInfo.AsmName, assetsFileScriptInfo.Namespace, assetsFileScriptInfo.ClassName, unityVersion);
				}
			}
			return assetTypeTemplateField;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001972C File Offset: 0x0001792C
		public AssetTypeValueField CreateValueBaseField(AssetsFileInstance inst, int id, ushort scriptIndex = 65535)
		{
			AssetTypeTemplateField assetTypeTemplateField = this.CreateTemplateBaseField(inst, id, scriptIndex);
			return ValueBuilder.DefaultValueFieldFromTemplate(assetTypeTemplateField);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00019750 File Offset: 0x00017950
		public AssetTypeValueField GetBaseField(AssetsFileInstance inst, AssetFileInfo info, AssetReadFlags readFlags = AssetReadFlags.None)
		{
			AssetTypeTemplateField templateBaseField = this.GetTemplateBaseField(inst, info, readFlags);
			RefTypeManager refTypeManager = this.GetRefTypeManager(inst);
			return templateBaseField.MakeValue(inst.file.Reader, info.AbsoluteByteStart, refTypeManager);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00019790 File Offset: 0x00017990
		public AssetTypeValueField GetBaseField(AssetsFileInstance inst, long pathId, AssetReadFlags readFlags = AssetReadFlags.None)
		{
			AssetFileInfo assetInfo = inst.file.GetAssetInfo(pathId);
			return this.GetBaseField(inst, assetInfo, readFlags);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000197B8 File Offset: 0x000179B8
		public AssetExternal GetExtAsset(AssetsFileInstance relativeTo, int fileId, long pathId, bool onlyGetInfo = false, AssetReadFlags readFlags = AssetReadFlags.None)
		{
			AssetExternal assetExternal = new AssetExternal
			{
				info = null,
				baseField = null,
				file = null
			};
			bool flag = fileId == 0 && pathId == 0L;
			AssetExternal assetExternal2;
			if (flag)
			{
				assetExternal2 = assetExternal;
			}
			else
			{
				bool flag2 = fileId != 0;
				if (flag2)
				{
					AssetsFileInstance dependency = relativeTo.GetDependency(this, fileId - 1);
					bool flag3 = dependency == null;
					if (flag3)
					{
						assetExternal2 = assetExternal;
					}
					else
					{
						assetExternal.file = dependency;
						assetExternal.info = dependency.file.GetAssetInfo(pathId);
						bool flag4 = assetExternal.info == null;
						if (flag4)
						{
							assetExternal2 = assetExternal;
						}
						else
						{
							bool flag5 = !onlyGetInfo;
							if (flag5)
							{
								assetExternal.baseField = this.GetBaseField(dependency, assetExternal.info, readFlags);
							}
							else
							{
								assetExternal.baseField = null;
							}
							assetExternal2 = assetExternal;
						}
					}
				}
				else
				{
					assetExternal.file = relativeTo;
					assetExternal.info = relativeTo.file.GetAssetInfo(pathId);
					bool flag6 = assetExternal.info == null;
					if (flag6)
					{
						assetExternal2 = assetExternal;
					}
					else
					{
						bool flag7 = !onlyGetInfo;
						if (flag7)
						{
							assetExternal.baseField = this.GetBaseField(relativeTo, assetExternal.info, readFlags);
						}
						else
						{
							assetExternal.baseField = null;
						}
						assetExternal2 = assetExternal;
					}
				}
			}
			return assetExternal2;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000198F0 File Offset: 0x00017AF0
		public AssetExternal GetExtAsset(AssetsFileInstance relativeTo, AssetTypeValueField pptrField, bool onlyGetInfo = false, AssetReadFlags readFlags = AssetReadFlags.None)
		{
			int asInt = pptrField["m_FileID"].AsInt;
			long asLong = pptrField["m_PathID"].AsLong;
			return this.GetExtAsset(relativeTo, asInt, asLong, onlyGetInfo, readFlags);
		}

		// Token: 0x04000408 RID: 1032
		private readonly Dictionary<int, AssetTypeTemplateField> templateFieldCache = new Dictionary<int, AssetTypeTemplateField>();

		// Token: 0x04000409 RID: 1033
		private readonly Dictionary<AssetTypeReference, AssetTypeTemplateField> monoTemplateFieldCache = new Dictionary<AssetTypeReference, AssetTypeTemplateField>();

		// Token: 0x0400040A RID: 1034
		private readonly Dictionary<AssetsFileInstance, Dictionary<ushort, AssetTypeTemplateField>> monoTypeTreeTemplateFieldCache = new Dictionary<AssetsFileInstance, Dictionary<ushort, AssetTypeTemplateField>>();

		// Token: 0x0400040B RID: 1035
		private readonly Dictionary<AssetsFileInstance, Dictionary<long, AssetTypeTemplateField>> monoCldbTemplateFieldCache = new Dictionary<AssetsFileInstance, Dictionary<long, AssetTypeTemplateField>>();

		// Token: 0x0400040C RID: 1036
		private readonly Dictionary<AssetsFileInstance, RefTypeManager> refTypeManagerCache = new Dictionary<AssetsFileInstance, RefTypeManager>();
	}
}

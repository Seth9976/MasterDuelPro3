using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AssetStudio
{
	// Token: 0x02000087 RID: 135
	public class AssetsManager : MonoBehaviour
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000A104 File Offset: 0x00008304
		public void LoadFiles(params string[] files)
		{
			ImportHelper.MergeSplitAssets(Path.GetDirectoryName(Path.GetFullPath(files[0])), false);
			string[] toReadFile = ImportHelper.ProcessingSplitFiles(files.ToList<string>());
			this.Load(toReadFile);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A138 File Offset: 0x00008338
		public void LoadFolder(string path)
		{
			ImportHelper.MergeSplitAssets(path, true);
			string[] toReadFile = ImportHelper.ProcessingSplitFiles(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList<string>());
			this.Load(toReadFile);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A16A File Offset: 0x0000836A
		public IEnumerator LoadFolderAsync(string path)
		{
			ImportHelper.MergeSplitAssets(path, true);
			string[] toReadFile = ImportHelper.ProcessingSplitFiles(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList<string>());
			IEnumerator ie = this.LoadAsync(toReadFile);
			while (ie.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A180 File Offset: 0x00008380
		public void Load(string[] files)
		{
			foreach (string file in files)
			{
				this.importFiles.Add(file);
				this.importFilesHash.Add(Path.GetFileName(file));
			}
			Progress.Reset();
			for (int i = 0; i < this.importFiles.Count; i++)
			{
				this.LoadFile(this.importFiles[i]);
				Progress.Report(i + 1, this.importFiles.Count);
			}
			this.importFiles.Clear();
			this.importFilesHash.Clear();
			this.noexistFiles.Clear();
			this.assetsFileListHash.Clear();
			this.ReadAssets();
			this.ProcessAssets();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A237 File Offset: 0x00008437
		private IEnumerator LoadAsync(string[] files)
		{
			foreach (string file in files)
			{
				bool found = false;
				using (List<AssetBundleRobber.AssetbundleInfo>.Enumerator enumerator = AssetBundleRobber.files.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.path == file.Substring(file.Length - 8))
						{
							found = true;
							break;
						}
					}
				}
				using (List<AssetBundleRobber.AssetbundleInfo>.Enumerator enumerator = DuelLinksRobber.files.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.path == file.Substring(file.Length - 8))
						{
							found = true;
							break;
						}
					}
				}
				if (!found)
				{
					this.importFiles.Add(file);
					this.importFilesHash.Add(Path.GetFileName(file));
				}
			}
			int j;
			for (int i = 0; i < this.importFiles.Count; i = j + 1)
			{
				this.LoadFile(this.importFiles[i]);
				string text = "LoadAssets: ";
				j = i + 1;
				string text2 = j.ToString();
				string text3 = "/";
				j = this.importFiles.Count;
				AssetBundleRobber.SetHint(text + text2 + text3 + j.ToString());
				yield return null;
				j = i;
			}
			this.importFiles.Clear();
			this.importFilesHash.Clear();
			this.noexistFiles.Clear();
			this.assetsFileListHash.Clear();
			IEnumerator ie = this.ReadAssetsAsync();
			while (ie.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A250 File Offset: 0x00008450
		private void LoadFile(string fullName)
		{
			FileReader reader = new FileReader(fullName);
			this.LoadFile(reader);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A26C File Offset: 0x0000846C
		private void LoadFile(FileReader reader)
		{
			switch (reader.FileType)
			{
			case FileType.AssetsFile:
				this.LoadAssetsFile(reader);
				return;
			case FileType.BundleFile:
				this.LoadBundleFile(reader, null);
				return;
			case FileType.WebFile:
				this.LoadWebFile(reader);
				return;
			case FileType.ResourceFile:
				break;
			case FileType.GZipFile:
				this.LoadFile(ImportHelper.DecompressGZip(reader));
				return;
			case FileType.BrotliFile:
				this.LoadFile(ImportHelper.DecompressBrotli(reader));
				return;
			case FileType.ZipFile:
				this.LoadZipFile(reader);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A2E0 File Offset: 0x000084E0
		private void LoadAssetsFile(FileReader reader)
		{
			if (!this.assetsFileListHash.Contains(reader.FileName))
			{
				Logger.Info("Loading " + reader.FullPath);
				try
				{
					SerializedFile assetsFile = new SerializedFile(reader, this);
					this.CheckStrippedVersion(assetsFile);
					this.assetsFileList.Add(assetsFile);
					this.assetsFileListHash.Add(assetsFile.fileName);
					foreach (FileIdentifier fileIdentifier in assetsFile.m_Externals)
					{
						string sharedFileName = fileIdentifier.fileName;
						if (!this.importFilesHash.Contains(sharedFileName))
						{
							string sharedFilePath = Path.Combine(Path.GetDirectoryName(reader.FullPath), sharedFileName);
							if (!this.noexistFiles.Contains(sharedFilePath))
							{
								if (!File.Exists(sharedFilePath))
								{
									string[] findFiles = Directory.GetFiles(Path.GetDirectoryName(reader.FullPath), sharedFileName, SearchOption.AllDirectories);
									if (findFiles.Length != 0)
									{
										sharedFilePath = findFiles[0];
									}
								}
								if (File.Exists(sharedFilePath))
								{
									this.importFiles.Add(sharedFilePath);
									this.importFilesHash.Add(sharedFileName);
								}
								else
								{
									this.noexistFiles.Add(sharedFilePath);
								}
							}
						}
					}
					return;
				}
				catch (Exception e)
				{
					Logger.Error("Error while reading assets file " + reader.FullPath, e);
					reader.Dispose();
					return;
				}
			}
			Logger.Info("Skipping " + reader.FullPath);
			reader.Dispose();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A460 File Offset: 0x00008660
		private void LoadAssetsFromMemory(FileReader reader, string originalPath, string unityVersion = null)
		{
			if (!this.assetsFileListHash.Contains(reader.FileName))
			{
				try
				{
					SerializedFile assetsFile = new SerializedFile(reader, this);
					assetsFile.originalPath = originalPath;
					if (!string.IsNullOrEmpty(unityVersion) && assetsFile.header.m_Version < SerializedFileFormatVersion.Unknown_7)
					{
						assetsFile.SetVersion(unityVersion);
					}
					this.CheckStrippedVersion(assetsFile);
					this.assetsFileList.Add(assetsFile);
					this.assetsFileListHash.Add(assetsFile.fileName);
					return;
				}
				catch (Exception e)
				{
					Logger.Error("Error while reading assets file " + reader.FullPath + " from " + Path.GetFileName(originalPath), e);
					this.resourceFileReaders.Add(reader.FileName, reader);
					return;
				}
			}
			Logger.Info(string.Concat(new string[] { "Skipping ", originalPath, " (", reader.FileName, ")" }));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A554 File Offset: 0x00008754
		private void LoadBundleFile(FileReader reader, string originalPath = null)
		{
			Logger.Info("Loading " + reader.FullPath);
			try
			{
				BundleFile bundleFile = new BundleFile(reader);
				foreach (StreamFile file in bundleFile.fileList)
				{
					FileReader subReader = new FileReader(Path.Combine(Path.GetDirectoryName(reader.FullPath), file.fileName), file.stream);
					if (subReader.FileType == FileType.AssetsFile)
					{
						this.LoadAssetsFromMemory(subReader, originalPath ?? reader.FullPath, bundleFile.m_Header.unityRevision);
					}
					else
					{
						this.resourceFileReaders[file.fileName] = subReader;
					}
				}
			}
			catch (Exception e)
			{
				string str = "Error while reading bundle file " + reader.FullPath;
				if (originalPath != null)
				{
					str = str + " from " + Path.GetFileName(originalPath);
				}
				Logger.Error(str, e);
			}
			finally
			{
				reader.Dispose();
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A650 File Offset: 0x00008850
		private void LoadWebFile(FileReader reader)
		{
			Logger.Info("Loading " + reader.FullPath);
			try
			{
				foreach (StreamFile file in new WebFile(reader).fileList)
				{
					FileReader subReader = new FileReader(Path.Combine(Path.GetDirectoryName(reader.FullPath), file.fileName), file.stream);
					switch (subReader.FileType)
					{
					case FileType.AssetsFile:
						this.LoadAssetsFromMemory(subReader, reader.FullPath, null);
						break;
					case FileType.BundleFile:
						this.LoadBundleFile(subReader, reader.FullPath);
						break;
					case FileType.WebFile:
						this.LoadWebFile(subReader);
						break;
					case FileType.ResourceFile:
						this.resourceFileReaders[file.fileName] = subReader;
						break;
					}
				}
			}
			catch (Exception e)
			{
				Logger.Error("Error while reading web file " + reader.FullPath, e);
			}
			finally
			{
				reader.Dispose();
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A754 File Offset: 0x00008954
		private void LoadZipFile(FileReader reader)
		{
			Logger.Info("Loading " + reader.FileName);
			try
			{
				using (ZipArchive archive = new ZipArchive(reader.BaseStream, 0))
				{
					List<string> splitFiles = new List<string>();
					foreach (ZipArchiveEntry entry in archive.Entries)
					{
						if (entry.Name.Contains(".split"))
						{
							string baseName = Path.GetFileNameWithoutExtension(entry.Name);
							string basePath = Path.Combine(Path.GetDirectoryName(entry.FullName), baseName);
							if (!splitFiles.Contains(basePath))
							{
								splitFiles.Add(basePath);
								this.importFilesHash.Add(baseName);
							}
						}
						else
						{
							this.importFilesHash.Add(entry.Name);
						}
					}
					foreach (string basePath2 in splitFiles)
					{
						try
						{
							Stream splitStream = new MemoryStream();
							int i = 0;
							for (;;)
							{
								string path = string.Format("{0}.split{1}", basePath2, i++);
								ZipArchiveEntry entry2 = archive.GetEntry(path);
								if (entry2 != null)
								{
									using (Stream entryStream = entry2.Open())
									{
										entryStream.CopyTo(splitStream);
										continue;
									}
									break;
								}
								break;
							}
							splitStream.Seek(0L, SeekOrigin.Begin);
							FileReader entryReader = new FileReader(basePath2, splitStream);
							this.LoadFile(entryReader);
						}
						catch (Exception e)
						{
							Logger.Error("Error while reading zip split file " + basePath2, e);
						}
					}
					foreach (ZipArchiveEntry entry3 in archive.Entries)
					{
						try
						{
							string dummyPath = Path.Combine(Path.GetDirectoryName(reader.FullPath), reader.FileName, entry3.FullName);
							Stream streamReader = new MemoryStream();
							using (Stream entryStream2 = entry3.Open())
							{
								entryStream2.CopyTo(streamReader);
							}
							streamReader.Position = 0L;
							FileReader entryReader2 = new FileReader(dummyPath, streamReader);
							this.LoadFile(entryReader2);
							if (entryReader2.FileType == FileType.ResourceFile)
							{
								entryReader2.Position = 0L;
								if (!this.resourceFileReaders.ContainsKey(entry3.Name))
								{
									this.resourceFileReaders.Add(entry3.Name, entryReader2);
								}
							}
						}
						catch (Exception e2)
						{
							Logger.Error("Error while reading zip entry " + entry3.FullName, e2);
						}
					}
				}
			}
			catch (Exception e3)
			{
				Logger.Error("Error while reading zip file " + reader.FileName, e3);
			}
			finally
			{
				reader.Dispose();
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public void CheckStrippedVersion(SerializedFile assetsFile)
		{
			if (assetsFile.IsVersionStripped && string.IsNullOrEmpty(this.SpecifyUnityVersion))
			{
				throw new Exception("The Unity version has been stripped, please set the version in the options");
			}
			if (!string.IsNullOrEmpty(this.SpecifyUnityVersion))
			{
				assetsFile.SetVersion(this.SpecifyUnityVersion);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000AB20 File Offset: 0x00008D20
		public void Clear()
		{
			foreach (SerializedFile serializedFile in this.assetsFileList)
			{
				serializedFile.Objects.Clear();
				serializedFile.reader.Close();
			}
			this.assetsFileList.Clear();
			foreach (KeyValuePair<string, BinaryReader> resourceFileReader in this.resourceFileReaders)
			{
				resourceFileReader.Value.Close();
			}
			this.resourceFileReaders.Clear();
			this.assetsFileIndexCache.Clear();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		private void ReadAssets()
		{
			Logger.Info("Read assets...");
			int progressCount = this.assetsFileList.Sum((SerializedFile x) => x.m_Objects.Count);
			int i = 0;
			Progress.Reset();
			foreach (SerializedFile assetsFile in this.assetsFileList)
			{
				foreach (ObjectInfo objectInfo in assetsFile.m_Objects)
				{
					ObjectReader objectReader = new ObjectReader(assetsFile.reader, assetsFile, objectInfo);
					try
					{
						ClassIDType type = objectReader.type;
						AssetStudio.Object obj;
						if (type <= ClassIDType.AnimatorController)
						{
							if (type <= ClassIDType.MeshFilter)
							{
								if (type <= ClassIDType.Material)
								{
									if (type == ClassIDType.GameObject)
									{
										obj = new GameObject(objectReader);
										goto IL_038B;
									}
									if (type == ClassIDType.Transform)
									{
										obj = new Transform(objectReader);
										goto IL_038B;
									}
									if (type == ClassIDType.Material)
									{
										obj = new Material(objectReader);
										goto IL_038B;
									}
								}
								else
								{
									if (type == ClassIDType.MeshRenderer)
									{
										obj = new MeshRenderer(objectReader);
										goto IL_038B;
									}
									if (type == ClassIDType.Texture2D)
									{
										obj = new Texture2D(objectReader);
										goto IL_038B;
									}
									if (type == ClassIDType.MeshFilter)
									{
										obj = new MeshFilter(objectReader);
										goto IL_038B;
									}
								}
							}
							else if (type <= ClassIDType.TextAsset)
							{
								if (type == ClassIDType.Mesh)
								{
									obj = new Mesh(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.Shader)
								{
									obj = new Shader(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.TextAsset)
								{
									obj = new TextAsset(objectReader);
									goto IL_038B;
								}
							}
							else if (type <= ClassIDType.AudioClip)
							{
								if (type == ClassIDType.AnimationClip)
								{
									obj = new AnimationClip(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.AudioClip)
								{
									obj = new AudioClip(objectReader);
									goto IL_038B;
								}
							}
							else
							{
								if (type == ClassIDType.Avatar)
								{
									obj = new Avatar(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.AnimatorController)
								{
									obj = new AnimatorController(objectReader);
									goto IL_038B;
								}
							}
						}
						else if (type <= ClassIDType.AssetBundle)
						{
							if (type <= ClassIDType.Font)
							{
								if (type == ClassIDType.Animator)
								{
									obj = new Animator(objectReader);
									goto IL_038B;
								}
								switch (type)
								{
								case ClassIDType.Animation:
									obj = new Animation(objectReader);
									goto IL_038B;
								case (ClassIDType)112:
								case (ClassIDType)113:
									break;
								case ClassIDType.MonoBehaviour:
									obj = new AssetStudioMonoBehaviour(objectReader);
									goto IL_038B;
								case ClassIDType.MonoScript:
									obj = new MonoScript(objectReader);
									goto IL_038B;
								default:
									if (type == ClassIDType.Font)
									{
										obj = new Font(objectReader);
										goto IL_038B;
									}
									break;
								}
							}
							else
							{
								if (type == ClassIDType.PlayerSettings)
								{
									obj = new PlayerSettings(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.SkinnedMeshRenderer)
								{
									obj = new SkinnedMeshRenderer(objectReader);
									goto IL_038B;
								}
								if (type == ClassIDType.AssetBundle)
								{
									obj = new AssetBundle(objectReader);
									goto IL_038B;
								}
							}
						}
						else if (type <= ClassIDType.Sprite)
						{
							if (type == ClassIDType.ResourceManager)
							{
								obj = new ResourceManager(objectReader);
								goto IL_038B;
							}
							if (type == ClassIDType.MovieTexture)
							{
								obj = new MovieTexture(objectReader);
								goto IL_038B;
							}
							if (type == ClassIDType.Sprite)
							{
								obj = new Sprite(objectReader);
								goto IL_038B;
							}
						}
						else if (type <= ClassIDType.RectTransform)
						{
							if (type == ClassIDType.AnimatorOverrideController)
							{
								obj = new AnimatorOverrideController(objectReader);
								goto IL_038B;
							}
							if (type == ClassIDType.RectTransform)
							{
								obj = new RectTransform(objectReader);
								goto IL_038B;
							}
						}
						else
						{
							if (type == ClassIDType.VideoClip)
							{
								obj = new VideoClip(objectReader);
								goto IL_038B;
							}
							if (type == ClassIDType.SpriteAtlas)
							{
								obj = new SpriteAtlas(objectReader);
								goto IL_038B;
							}
						}
						obj = new AssetStudio.Object(objectReader);
						IL_038B:
						assetsFile.AddObject(obj);
					}
					catch (Exception e)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine("Unable to load object").AppendLine("Assets " + assetsFile.fileName).AppendLine("Path " + assetsFile.originalPath)
							.AppendLine(string.Format("Type {0}", objectReader.type))
							.AppendLine(string.Format("PathID {0}", objectInfo.m_PathID))
							.Append(e);
						Logger.Error(stringBuilder.ToString());
					}
					Progress.Report(++i, progressCount);
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B0A4 File Offset: 0x000092A4
		private IEnumerator ReadAssetsAsync()
		{
			this.assetsFileList.Sum((SerializedFile x) => x.m_Objects.Count);
			int i = 0;
			Progress.Reset();
			foreach (SerializedFile assetsFile in this.assetsFileList)
			{
				int num = i;
				i = num + 1;
				foreach (ObjectInfo objectInfo in assetsFile.m_Objects)
				{
					ObjectReader objectReader = new ObjectReader(assetsFile.reader, assetsFile, objectInfo);
					try
					{
						if (objectReader.type == ClassIDType.AssetBundle)
						{
							assetsFile.AddObject(new AssetBundle(objectReader));
						}
					}
					catch (Exception e)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine("Unable to load object").AppendLine("Assets " + assetsFile.fileName).AppendLine("Path " + assetsFile.originalPath)
							.AppendLine(string.Format("Type {0}", objectReader.type))
							.AppendLine(string.Format("PathID {0}", objectInfo.m_PathID))
							.Append(e);
						Logger.Error(stringBuilder.ToString());
					}
				}
				AssetBundleRobber.SetHint("ReadAssets: " + i.ToString() + "/" + this.assetsFileList.Count.ToString());
				yield return null;
			}
			List<SerializedFile>.Enumerator enumerator = default(List<SerializedFile>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B0B4 File Offset: 0x000092B4
		private void ProcessAssets()
		{
			Logger.Info("Process Assets...");
			foreach (SerializedFile serializedFile in this.assetsFileList)
			{
				foreach (AssetStudio.Object obj in serializedFile.Objects)
				{
					GameObject m_GameObject = obj as GameObject;
					if (m_GameObject != null)
					{
						PPtr<Component>[] components = m_GameObject.m_Components;
						for (int i = 0; i < components.Length; i++)
						{
							Component m_Component;
							if (components[i].TryGet(out m_Component))
							{
								Transform m_Transform = m_Component as Transform;
								if (m_Transform == null)
								{
									MeshRenderer m_MeshRenderer = m_Component as MeshRenderer;
									if (m_MeshRenderer == null)
									{
										MeshFilter m_MeshFilter = m_Component as MeshFilter;
										if (m_MeshFilter == null)
										{
											SkinnedMeshRenderer m_SkinnedMeshRenderer = m_Component as SkinnedMeshRenderer;
											if (m_SkinnedMeshRenderer == null)
											{
												Animator m_Animator = m_Component as Animator;
												if (m_Animator == null)
												{
													Animation m_Animation = m_Component as Animation;
													if (m_Animation != null)
													{
														m_GameObject.m_Animation = m_Animation;
													}
												}
												else
												{
													m_GameObject.m_Animator = m_Animator;
												}
											}
											else
											{
												m_GameObject.m_SkinnedMeshRenderer = m_SkinnedMeshRenderer;
											}
										}
										else
										{
											m_GameObject.m_MeshFilter = m_MeshFilter;
										}
									}
									else
									{
										m_GameObject.m_MeshRenderer = m_MeshRenderer;
									}
								}
								else
								{
									m_GameObject.m_Transform = m_Transform;
								}
							}
						}
					}
					else
					{
						SpriteAtlas m_SpriteAtlas = obj as SpriteAtlas;
						if (m_SpriteAtlas != null)
						{
							PPtr<Sprite>[] packedSprites = m_SpriteAtlas.m_PackedSprites;
							for (int i = 0; i < packedSprites.Length; i++)
							{
								Sprite m_Sprite;
								if (packedSprites[i].TryGet(out m_Sprite))
								{
									if (m_Sprite.m_SpriteAtlas.IsNull)
									{
										m_Sprite.m_SpriteAtlas.Set(m_SpriteAtlas);
									}
									else
									{
										SpriteAtlas m_SpriteAtlaOld;
										m_Sprite.m_SpriteAtlas.TryGet(out m_SpriteAtlaOld);
										if (m_SpriteAtlaOld.m_IsVariant)
										{
											m_Sprite.m_SpriteAtlas.Set(m_SpriteAtlas);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B2AC File Offset: 0x000094AC
		private IEnumerator ProcessAssetsAsync()
		{
			int i = 0;
			foreach (SerializedFile serializedFile in this.assetsFileList)
			{
				int j = i;
				i = j + 1;
				foreach (AssetStudio.Object obj in serializedFile.Objects)
				{
					GameObject m_GameObject = obj as GameObject;
					if (m_GameObject != null)
					{
						PPtr<Component>[] components = m_GameObject.m_Components;
						for (j = 0; j < components.Length; j++)
						{
							Component m_Component;
							if (components[j].TryGet(out m_Component))
							{
								Transform m_Transform = m_Component as Transform;
								if (m_Transform == null)
								{
									MeshRenderer m_MeshRenderer = m_Component as MeshRenderer;
									if (m_MeshRenderer == null)
									{
										MeshFilter m_MeshFilter = m_Component as MeshFilter;
										if (m_MeshFilter == null)
										{
											SkinnedMeshRenderer m_SkinnedMeshRenderer = m_Component as SkinnedMeshRenderer;
											if (m_SkinnedMeshRenderer == null)
											{
												Animator m_Animator = m_Component as Animator;
												if (m_Animator == null)
												{
													Animation m_Animation = m_Component as Animation;
													if (m_Animation != null)
													{
														m_GameObject.m_Animation = m_Animation;
													}
												}
												else
												{
													m_GameObject.m_Animator = m_Animator;
												}
											}
											else
											{
												m_GameObject.m_SkinnedMeshRenderer = m_SkinnedMeshRenderer;
											}
										}
										else
										{
											m_GameObject.m_MeshFilter = m_MeshFilter;
										}
									}
									else
									{
										m_GameObject.m_MeshRenderer = m_MeshRenderer;
									}
								}
								else
								{
									m_GameObject.m_Transform = m_Transform;
								}
							}
						}
					}
					else
					{
						SpriteAtlas m_SpriteAtlas = obj as SpriteAtlas;
						if (m_SpriteAtlas != null)
						{
							PPtr<Sprite>[] packedSprites = m_SpriteAtlas.m_PackedSprites;
							for (j = 0; j < packedSprites.Length; j++)
							{
								Sprite m_Sprite;
								if (packedSprites[j].TryGet(out m_Sprite))
								{
									if (m_Sprite.m_SpriteAtlas.IsNull)
									{
										m_Sprite.m_SpriteAtlas.Set(m_SpriteAtlas);
									}
									else
									{
										SpriteAtlas m_SpriteAtlaOld;
										m_Sprite.m_SpriteAtlas.TryGet(out m_SpriteAtlaOld);
										if (m_SpriteAtlaOld.m_IsVariant)
										{
											m_Sprite.m_SpriteAtlas.Set(m_SpriteAtlas);
										}
									}
								}
							}
						}
					}
				}
				AssetBundleRobber.SetHint("ProcessAssets: " + i.ToString() + "/" + this.assetsFileList.Count.ToString());
				yield return null;
			}
			List<SerializedFile>.Enumerator enumerator = default(List<SerializedFile>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000355 RID: 853
		public string SpecifyUnityVersion;

		// Token: 0x04000356 RID: 854
		public List<SerializedFile> assetsFileList = new List<SerializedFile>();

		// Token: 0x04000357 RID: 855
		internal Dictionary<string, int> assetsFileIndexCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000358 RID: 856
		internal Dictionary<string, BinaryReader> resourceFileReaders = new Dictionary<string, BinaryReader>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000359 RID: 857
		private List<string> importFiles = new List<string>();

		// Token: 0x0400035A RID: 858
		private HashSet<string> importFilesHash = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400035B RID: 859
		private HashSet<string> noexistFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400035C RID: 860
		private HashSet<string> assetsFileListHash = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}

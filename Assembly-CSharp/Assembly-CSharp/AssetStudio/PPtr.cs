using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000105 RID: 261
	public sealed class PPtr<T> where T : Object
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public PPtr(ObjectReader reader)
		{
			this.m_FileID = reader.ReadInt32();
			this.m_PathID = ((reader.m_Version < SerializedFileFormatVersion.Unknown_14) ? ((long)reader.ReadInt32()) : reader.ReadInt64());
			this.assetsFile = reader.assetsFile;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		private bool TryGetAssetsFile(out SerializedFile result)
		{
			result = null;
			if (this.m_FileID == 0)
			{
				result = this.assetsFile;
				return true;
			}
			if (this.m_FileID > 0 && this.m_FileID - 1 < this.assetsFile.m_Externals.Count)
			{
				AssetsManager assetsManager = this.assetsFile.assetsManager;
				List<SerializedFile> assetsFileList = assetsManager.assetsFileList;
				Dictionary<string, int> assetsFileIndexCache = assetsManager.assetsFileIndexCache;
				if (this.index == -2)
				{
					FileIdentifier m_External = this.assetsFile.m_Externals[this.m_FileID - 1];
					string name = m_External.fileName;
					if (!assetsFileIndexCache.TryGetValue(name, out this.index))
					{
						this.index = assetsFileList.FindIndex((SerializedFile x) => x.fileName.Equals(name, StringComparison.OrdinalIgnoreCase));
						assetsFileIndexCache.Add(name, this.index);
					}
				}
				if (this.index >= 0)
				{
					result = assetsFileList[this.index];
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		public bool TryGet(out T result)
		{
			SerializedFile sourceFile;
			Object obj;
			if (this.TryGetAssetsFile(out sourceFile) && sourceFile.ObjectsDic.TryGetValue(this.m_PathID, out obj))
			{
				T variable = obj as T;
				if (variable != null)
				{
					result = variable;
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011C38 File Offset: 0x0000FE38
		public bool TryGet<T2>(out T2 result) where T2 : Object
		{
			SerializedFile sourceFile;
			Object obj;
			if (this.TryGetAssetsFile(out sourceFile) && sourceFile.ObjectsDic.TryGetValue(this.m_PathID, out obj))
			{
				T2 variable = obj as T2;
				if (variable != null)
				{
					result = variable;
					return true;
				}
			}
			result = default(T2);
			return false;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011C8C File Offset: 0x0000FE8C
		public void Set(T m_Object)
		{
			string name = m_Object.assetsFile.fileName;
			if (string.Equals(this.assetsFile.fileName, name, StringComparison.OrdinalIgnoreCase))
			{
				this.m_FileID = 0;
			}
			else
			{
				this.m_FileID = this.assetsFile.m_Externals.FindIndex((FileIdentifier x) => string.Equals(x.fileName, name, StringComparison.OrdinalIgnoreCase));
				if (this.m_FileID == -1)
				{
					this.assetsFile.m_Externals.Add(new FileIdentifier
					{
						fileName = m_Object.assetsFile.fileName
					});
					this.m_FileID = this.assetsFile.m_Externals.Count;
				}
				else
				{
					this.m_FileID++;
				}
			}
			AssetsManager assetsManager = this.assetsFile.assetsManager;
			List<SerializedFile> assetsFileList = assetsManager.assetsFileList;
			Dictionary<string, int> assetsFileIndexCache = assetsManager.assetsFileIndexCache;
			if (!assetsFileIndexCache.TryGetValue(name, out this.index))
			{
				this.index = assetsFileList.FindIndex((SerializedFile x) => x.fileName.Equals(name, StringComparison.OrdinalIgnoreCase));
				assetsFileIndexCache.Add(name, this.index);
			}
			this.m_PathID = m_Object.m_PathID;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00011DBA File Offset: 0x0000FFBA
		public bool IsNull
		{
			get
			{
				return this.m_PathID == 0L || this.m_FileID < 0;
			}
		}

		// Token: 0x0400076A RID: 1898
		public int m_FileID;

		// Token: 0x0400076B RID: 1899
		public long m_PathID;

		// Token: 0x0400076C RID: 1900
		private SerializedFile assetsFile;

		// Token: 0x0400076D RID: 1901
		private int index = -2;
	}
}

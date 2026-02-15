using System;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x0200013E RID: 318
	[MovedFrom(false, "UnityEngine.Rendering", "Unity.RenderPipelines.Core.Runtime", "ProbeVolumeBakingSet.StreamableAsset")]
	[Serializable]
	internal class ProbeVolumeStreamableAsset
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00020B59 File Offset: 0x0001ED59
		public string assetGUID
		{
			get
			{
				return this.m_AssetGUID;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00020B61 File Offset: 0x0001ED61
		public TextAsset asset
		{
			get
			{
				return this.m_Asset;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00020B69 File Offset: 0x0001ED69
		public int elementSize
		{
			get
			{
				return this.m_ElementSize;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00020B71 File Offset: 0x0001ED71
		public SerializedDictionary<int, ProbeVolumeStreamableAsset.StreamableCellDesc> streamableCellDescs
		{
			get
			{
				return this.m_StreamableCellDescs;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00020B7C File Offset: 0x0001ED7C
		public ProbeVolumeStreamableAsset(string apvStreamingAssetsPath, SerializedDictionary<int, ProbeVolumeStreamableAsset.StreamableCellDesc> cellDescs, int elementSize, string bakingSetGUID, string assetGUID)
		{
			this.m_AssetGUID = assetGUID;
			this.m_StreamableCellDescs = cellDescs;
			this.m_ElementSize = elementSize;
			this.m_StreamableAssetPath = Path.Combine(Path.Combine(apvStreamingAssetsPath, bakingSetGUID), this.m_AssetGUID + ".bytes");
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00020BE9 File Offset: 0x0001EDE9
		internal void RefreshAssetPath()
		{
			this.m_FinalAssetPath = Path.Combine(Application.streamingAssetsPath, this.m_StreamableAssetPath);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00020C01 File Offset: 0x0001EE01
		public string GetAssetPath()
		{
			if (string.IsNullOrEmpty(this.m_FinalAssetPath))
			{
				this.RefreshAssetPath();
			}
			return this.m_FinalAssetPath;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00020C1C File Offset: 0x0001EE1C
		public unsafe bool FileExists()
		{
			if (this.m_Asset != null)
			{
				return true;
			}
			FileInfoResult result;
			AsyncReadManager.GetFileInfo(this.GetAssetPath(), &result).JobHandle.Complete();
			return result.FileState == FileState.Exists;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00020C60 File Offset: 0x0001EE60
		public long GetFileSize()
		{
			return new FileInfo(this.GetAssetPath()).Length;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00020C72 File Offset: 0x0001EE72
		public bool IsOpen()
		{
			return this.m_AssetFileHandle.IsValid();
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00020C7F File Offset: 0x0001EE7F
		public FileHandle OpenFile()
		{
			if (this.m_AssetFileHandle.IsValid())
			{
				return this.m_AssetFileHandle;
			}
			this.m_AssetFileHandle = AsyncReadManager.OpenFileAsync(this.GetAssetPath());
			return this.m_AssetFileHandle;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00020CAC File Offset: 0x0001EEAC
		public void CloseFile()
		{
			if (this.m_AssetFileHandle.IsValid() && this.m_AssetFileHandle.JobHandle.IsCompleted)
			{
				this.m_AssetFileHandle.Close(default(JobHandle));
			}
			this.m_AssetFileHandle = default(FileHandle);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.m_AssetGUID);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public void Dispose()
		{
			if (this.m_AssetFileHandle.IsValid())
			{
				this.m_AssetFileHandle.Close(default(JobHandle)).Complete();
				this.m_AssetFileHandle = default(FileHandle);
			}
		}

		// Token: 0x040005E9 RID: 1513
		[SerializeField]
		[FormerlySerializedAs("assetGUID")]
		private string m_AssetGUID = "";

		// Token: 0x040005EA RID: 1514
		[SerializeField]
		[FormerlySerializedAs("streamableAssetPath")]
		private string m_StreamableAssetPath = "";

		// Token: 0x040005EB RID: 1515
		[SerializeField]
		[FormerlySerializedAs("elementSize")]
		private int m_ElementSize;

		// Token: 0x040005EC RID: 1516
		[SerializeField]
		[FormerlySerializedAs("streamableCellDescs")]
		private SerializedDictionary<int, ProbeVolumeStreamableAsset.StreamableCellDesc> m_StreamableCellDescs = new SerializedDictionary<int, ProbeVolumeStreamableAsset.StreamableCellDesc>();

		// Token: 0x040005ED RID: 1517
		[SerializeField]
		private TextAsset m_Asset;

		// Token: 0x040005EE RID: 1518
		private string m_FinalAssetPath;

		// Token: 0x040005EF RID: 1519
		private FileHandle m_AssetFileHandle;

		// Token: 0x0200013F RID: 319
		[MovedFrom(false, "UnityEngine.Rendering", "Unity.RenderPipelines.Core.Runtime", "ProbeVolumeBakingSet.StreamableAsset.StreamableCellDesc")]
		[Serializable]
		public struct StreamableCellDesc
		{
			// Token: 0x040005F0 RID: 1520
			public int offset;

			// Token: 0x040005F1 RID: 1521
			public int elementCount;
		}
	}
}

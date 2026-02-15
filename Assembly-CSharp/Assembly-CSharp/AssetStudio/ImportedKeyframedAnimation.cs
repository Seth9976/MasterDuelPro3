using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200015B RID: 347
	public class ImportedKeyframedAnimation
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00015926 File Offset: 0x00013B26
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001592E File Offset: 0x00013B2E
		public string Name { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00015937 File Offset: 0x00013B37
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001593F File Offset: 0x00013B3F
		public float SampleRate { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00015948 File Offset: 0x00013B48
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00015950 File Offset: 0x00013B50
		public List<ImportedAnimationKeyframedTrack> TrackList { get; set; }

		// Token: 0x06000446 RID: 1094 RVA: 0x0001595C File Offset: 0x00013B5C
		public ImportedAnimationKeyframedTrack FindTrack(string path)
		{
			ImportedAnimationKeyframedTrack track = this.TrackList.Find((ImportedAnimationKeyframedTrack x) => x.Path == path);
			if (track == null)
			{
				track = new ImportedAnimationKeyframedTrack
				{
					Path = path
				};
				this.TrackList.Add(track);
			}
			return track;
		}
	}
}

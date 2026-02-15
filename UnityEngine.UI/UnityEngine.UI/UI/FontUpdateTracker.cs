using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200001D RID: 29
	public static class FontUpdateTracker
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000054A8 File Offset: 0x000036A8
		public static void TrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			HashSet<Text> exists;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out exists);
			if (exists == null)
			{
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt += FontUpdateTracker.RebuildForFont;
				}
				exists = new HashSet<Text>();
				FontUpdateTracker.m_Tracked.Add(t.font, exists);
			}
			exists.Add(t);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005518 File Offset: 0x00003718
		private static void RebuildForFont(Font f)
		{
			HashSet<Text> texts;
			FontUpdateTracker.m_Tracked.TryGetValue(f, out texts);
			if (texts == null)
			{
				return;
			}
			foreach (Text text in texts)
			{
				text.FontTextureChanged();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005578 File Offset: 0x00003778
		public static void UntrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			HashSet<Text> texts;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out texts);
			if (texts == null)
			{
				return;
			}
			texts.Remove(t);
			if (texts.Count == 0)
			{
				FontUpdateTracker.m_Tracked.Remove(t.font);
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt -= FontUpdateTracker.RebuildForFont;
				}
			}
		}

		// Token: 0x0400006C RID: 108
		private static Dictionary<Font, HashSet<Text>> m_Tracked = new Dictionary<Font, HashSet<Text>>();
	}
}

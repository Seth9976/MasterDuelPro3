using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x02000007 RID: 7
	public class Animation
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002954 File Offset: 0x00000B54
		public Animation(string name, ExposedList<Timeline> timelines, float duration)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.name = name;
			this.SetTimelines(timelines);
			this.duration = duration;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002984 File Offset: 0x00000B84
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000298C File Offset: 0x00000B8C
		public ExposedList<Timeline> Timelines
		{
			get
			{
				return this.timelines;
			}
			set
			{
				this.SetTimelines(value);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002998 File Offset: 0x00000B98
		public void SetTimelines(ExposedList<Timeline> timelines)
		{
			if (timelines == null)
			{
				throw new ArgumentNullException("timelines", "timelines cannot be null.");
			}
			this.timelines = timelines;
			int idCount = 0;
			int timelinesCount = timelines.Count;
			Timeline[] timelinesItems = timelines.Items;
			for (int t = 0; t < timelinesCount; t++)
			{
				idCount += timelinesItems[t].PropertyIds.Length;
			}
			string[] propertyIds = new string[idCount];
			int currentId = 0;
			for (int t2 = 0; t2 < timelinesCount; t2++)
			{
				string[] ids = timelinesItems[t2].PropertyIds;
				int i = 0;
				int idsLength = ids.Length;
				while (i < idsLength)
				{
					propertyIds[currentId++] = ids[i];
					i++;
				}
			}
			this.timelineIds = new HashSet<string>(propertyIds);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002A43 File Offset: 0x00000C43
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002A4B File Offset: 0x00000C4B
		public float Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A54 File Offset: 0x00000C54
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A5C File Offset: 0x00000C5C
		public bool HasTimeline(string[] propertyIds)
		{
			foreach (string id in propertyIds)
			{
				if (this.timelineIds.Contains(id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A90 File Offset: 0x00000C90
		public void Apply(Skeleton skeleton, float lastTime, float time, bool loop, ExposedList<Event> events, float alpha, MixBlend blend, MixDirection direction)
		{
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (loop && this.duration != 0f)
			{
				time %= this.duration;
				if (lastTime > 0f)
				{
					lastTime %= this.duration;
				}
			}
			Timeline[] timelines = this.timelines.Items;
			int i = 0;
			int j = this.timelines.Count;
			while (i < j)
			{
				timelines[i].Apply(skeleton, lastTime, time, events, alpha, blend, direction);
				i++;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A54 File Offset: 0x00000C54
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0400001C RID: 28
		internal string name;

		// Token: 0x0400001D RID: 29
		internal ExposedList<Timeline> timelines;

		// Token: 0x0400001E RID: 30
		internal HashSet<string> timelineIds;

		// Token: 0x0400001F RID: 31
		internal float duration;
	}
}

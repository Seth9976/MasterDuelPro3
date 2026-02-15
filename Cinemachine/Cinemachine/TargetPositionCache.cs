using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200009F RID: 159
	internal class TargetPositionCache
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00015F9E File Offset: 0x0001419E
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00015FA5 File Offset: 0x000141A5
		public static TargetPositionCache.Mode CacheMode
		{
			get
			{
				return TargetPositionCache.m_CacheMode;
			}
			set
			{
				if (value == TargetPositionCache.m_CacheMode)
				{
					return;
				}
				TargetPositionCache.m_CacheMode = value;
				switch (value)
				{
				default:
					TargetPositionCache.ClearCache();
					return;
				case TargetPositionCache.Mode.Record:
					TargetPositionCache.ClearCache();
					return;
				case TargetPositionCache.Mode.Playback:
					TargetPositionCache.CreatePlaybackCurves();
					return;
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00015FD9 File Offset: 0x000141D9
		public static bool IsRecording
		{
			get
			{
				return TargetPositionCache.UseCache && TargetPositionCache.m_CacheMode == TargetPositionCache.Mode.Record;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00015FEC File Offset: 0x000141EC
		public static bool CurrentPlaybackTimeValid
		{
			get
			{
				return TargetPositionCache.UseCache && TargetPositionCache.m_CacheMode == TargetPositionCache.Mode.Playback && TargetPositionCache.HasCurrentTime;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00016004 File Offset: 0x00014204
		public static bool IsEmpty
		{
			get
			{
				return TargetPositionCache.CacheTimeRange.IsEmpty;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001601E File Offset: 0x0001421E
		public static TargetPositionCache.TimeRange CacheTimeRange
		{
			get
			{
				return TargetPositionCache.m_CacheTimeRange;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00016025 File Offset: 0x00014225
		public static bool HasCurrentTime
		{
			get
			{
				return TargetPositionCache.m_CacheTimeRange.Contains(TargetPositionCache.CurrentTime);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00016036 File Offset: 0x00014236
		public static void ClearCache()
		{
			TargetPositionCache.m_Cache = ((TargetPositionCache.CacheMode == TargetPositionCache.Mode.Disabled) ? null : new Dictionary<Transform, TargetPositionCache.CacheEntry>());
			TargetPositionCache.m_CacheTimeRange = TargetPositionCache.TimeRange.Empty;
			TargetPositionCache.CurrentTime = 0f;
			TargetPositionCache.CurrentFrame = 0;
			TargetPositionCache.IsCameraCut = false;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001606C File Offset: 0x0001426C
		private static void CreatePlaybackCurves()
		{
			if (TargetPositionCache.m_Cache == null)
			{
				TargetPositionCache.m_Cache = new Dictionary<Transform, TargetPositionCache.CacheEntry>();
			}
			foreach (KeyValuePair<Transform, TargetPositionCache.CacheEntry> keyValuePair in TargetPositionCache.m_Cache)
			{
				keyValuePair.Value.CreateCurves();
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000160B4 File Offset: 0x000142B4
		public static Vector3 GetTargetPosition(Transform target)
		{
			if (!TargetPositionCache.UseCache || TargetPositionCache.CacheMode == TargetPositionCache.Mode.Disabled)
			{
				return target.position;
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Record && !TargetPositionCache.m_CacheTimeRange.IsEmpty && TargetPositionCache.CurrentTime < TargetPositionCache.m_CacheTimeRange.Start - 0.1f)
			{
				TargetPositionCache.ClearCache();
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Playback && !TargetPositionCache.HasCurrentTime)
			{
				return target.position;
			}
			TargetPositionCache.CacheEntry entry;
			if (!TargetPositionCache.m_Cache.TryGetValue(target, out entry))
			{
				if (TargetPositionCache.CacheMode != TargetPositionCache.Mode.Record)
				{
					return target.position;
				}
				entry = new TargetPositionCache.CacheEntry();
				TargetPositionCache.m_Cache.Add(target, entry);
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Record)
			{
				entry.AddRawItem(TargetPositionCache.CurrentTime, TargetPositionCache.IsCameraCut, target);
				TargetPositionCache.m_CacheTimeRange.Include(TargetPositionCache.CurrentTime);
				return target.position;
			}
			if (entry.Curve == null)
			{
				return target.position;
			}
			return entry.Curve.Evaluate(TargetPositionCache.CurrentTime).Pos;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000161A0 File Offset: 0x000143A0
		public static Quaternion GetTargetRotation(Transform target)
		{
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Disabled)
			{
				return target.rotation;
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Record && !TargetPositionCache.m_CacheTimeRange.IsEmpty && TargetPositionCache.CurrentTime < TargetPositionCache.m_CacheTimeRange.Start - 0.1f)
			{
				TargetPositionCache.ClearCache();
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Playback && !TargetPositionCache.HasCurrentTime)
			{
				return target.rotation;
			}
			TargetPositionCache.CacheEntry entry;
			if (!TargetPositionCache.m_Cache.TryGetValue(target, out entry))
			{
				if (TargetPositionCache.CacheMode != TargetPositionCache.Mode.Record)
				{
					return target.rotation;
				}
				entry = new TargetPositionCache.CacheEntry();
				TargetPositionCache.m_Cache.Add(target, entry);
			}
			if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Record)
			{
				if (TargetPositionCache.m_CacheTimeRange.End <= TargetPositionCache.CurrentTime)
				{
					entry.AddRawItem(TargetPositionCache.CurrentTime, TargetPositionCache.IsCameraCut, target);
					TargetPositionCache.m_CacheTimeRange.Include(TargetPositionCache.CurrentTime);
				}
				return target.rotation;
			}
			return entry.Curve.Evaluate(TargetPositionCache.CurrentTime).Rot;
		}

		// Token: 0x04000350 RID: 848
		public static bool UseCache;

		// Token: 0x04000351 RID: 849
		public const float CacheStepSize = 0.016666668f;

		// Token: 0x04000352 RID: 850
		private static TargetPositionCache.Mode m_CacheMode;

		// Token: 0x04000353 RID: 851
		public static float CurrentTime;

		// Token: 0x04000354 RID: 852
		public static int CurrentFrame;

		// Token: 0x04000355 RID: 853
		public static bool IsCameraCut;

		// Token: 0x04000356 RID: 854
		private static Dictionary<Transform, TargetPositionCache.CacheEntry> m_Cache;

		// Token: 0x04000357 RID: 855
		private static TargetPositionCache.TimeRange m_CacheTimeRange;

		// Token: 0x04000358 RID: 856
		private const float kWraparoundSlush = 0.1f;

		// Token: 0x020000A0 RID: 160
		public enum Mode
		{
			// Token: 0x0400035A RID: 858
			Disabled,
			// Token: 0x0400035B RID: 859
			Record,
			// Token: 0x0400035C RID: 860
			Playback
		}

		// Token: 0x020000A1 RID: 161
		private class CacheCurve
		{
			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060003CA RID: 970 RVA: 0x00016286 File Offset: 0x00014486
			public int Count
			{
				get
				{
					return this.m_Cache.Count;
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00016293 File Offset: 0x00014493
			public CacheCurve(float startTime, float endTime, float stepSize)
			{
				this.StepSize = stepSize;
				this.StartTime = startTime;
				this.m_Cache = new List<TargetPositionCache.CacheCurve.Item>(Mathf.CeilToInt((this.StepSize * 0.5f + endTime - startTime) / this.StepSize));
			}

			// Token: 0x060003CC RID: 972 RVA: 0x000162D0 File Offset: 0x000144D0
			public void Add(TargetPositionCache.CacheCurve.Item item)
			{
				this.m_Cache.Add(item);
			}

			// Token: 0x060003CD RID: 973 RVA: 0x000162E0 File Offset: 0x000144E0
			public void AddUntil(TargetPositionCache.CacheCurve.Item item, float time, bool isCut)
			{
				int prevIndex = this.m_Cache.Count - 1;
				float prevTime = (float)prevIndex * this.StepSize;
				float timeRange = time - this.StartTime - prevTime;
				if (isCut)
				{
					for (float t = this.StepSize; t <= timeRange; t += this.StepSize)
					{
						this.Add(item);
					}
					return;
				}
				TargetPositionCache.CacheCurve.Item prev = this.m_Cache[prevIndex];
				for (float t2 = this.StepSize; t2 <= timeRange; t2 += this.StepSize)
				{
					this.Add(TargetPositionCache.CacheCurve.Item.Lerp(prev, item, t2 / timeRange));
				}
			}

			// Token: 0x060003CE RID: 974 RVA: 0x0001636C File Offset: 0x0001456C
			public TargetPositionCache.CacheCurve.Item Evaluate(float time)
			{
				int numItems = this.m_Cache.Count;
				if (numItems == 0)
				{
					return TargetPositionCache.CacheCurve.Item.Empty;
				}
				float s = time - this.StartTime;
				int index = Mathf.Clamp(Mathf.FloorToInt(s / this.StepSize), 0, numItems - 1);
				TargetPositionCache.CacheCurve.Item v = this.m_Cache[index];
				if (index == numItems - 1)
				{
					return v;
				}
				return TargetPositionCache.CacheCurve.Item.Lerp(v, this.m_Cache[index + 1], (s - (float)index * this.StepSize) / this.StepSize);
			}

			// Token: 0x0400035D RID: 861
			public float StartTime;

			// Token: 0x0400035E RID: 862
			public float StepSize;

			// Token: 0x0400035F RID: 863
			private List<TargetPositionCache.CacheCurve.Item> m_Cache;

			// Token: 0x020000A2 RID: 162
			public struct Item
			{
				// Token: 0x060003CF RID: 975 RVA: 0x000163EC File Offset: 0x000145EC
				public static TargetPositionCache.CacheCurve.Item Lerp(TargetPositionCache.CacheCurve.Item a, TargetPositionCache.CacheCurve.Item b, float t)
				{
					return new TargetPositionCache.CacheCurve.Item
					{
						Pos = Vector3.LerpUnclamped(a.Pos, b.Pos, t),
						Rot = Quaternion.SlerpUnclamped(a.Rot, b.Rot, t)
					};
				}

				// Token: 0x170000DF RID: 223
				// (get) Token: 0x060003D0 RID: 976 RVA: 0x00016434 File Offset: 0x00014634
				public static TargetPositionCache.CacheCurve.Item Empty
				{
					get
					{
						return new TargetPositionCache.CacheCurve.Item
						{
							Rot = Quaternion.identity
						};
					}
				}

				// Token: 0x04000360 RID: 864
				public Vector3 Pos;

				// Token: 0x04000361 RID: 865
				public Quaternion Rot;
			}
		}

		// Token: 0x020000A3 RID: 163
		private class CacheEntry
		{
			// Token: 0x060003D1 RID: 977 RVA: 0x00016458 File Offset: 0x00014658
			public void AddRawItem(float time, bool isCut, Transform target)
			{
				float endTime = time - 0.016666668f;
				int maxItem = this.RawItems.Count - 1;
				int lastToKeep = maxItem;
				while (lastToKeep >= 0 && this.RawItems[lastToKeep].Time > endTime)
				{
					lastToKeep--;
				}
				if (lastToKeep == maxItem)
				{
					this.RawItems.Add(new TargetPositionCache.CacheEntry.RecordingItem
					{
						Time = time,
						IsCut = isCut,
						Item = new TargetPositionCache.CacheCurve.Item
						{
							Pos = target.position,
							Rot = target.rotation
						}
					});
					return;
				}
				int trimStart = lastToKeep + 2;
				if (trimStart <= maxItem)
				{
					this.RawItems.RemoveRange(trimStart, this.RawItems.Count - trimStart);
				}
				this.RawItems[lastToKeep + 1] = new TargetPositionCache.CacheEntry.RecordingItem
				{
					Time = time,
					IsCut = isCut,
					Item = new TargetPositionCache.CacheCurve.Item
					{
						Pos = target.position,
						Rot = target.rotation
					}
				};
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x00016568 File Offset: 0x00014768
			public void CreateCurves()
			{
				int maxItem = this.RawItems.Count - 1;
				float startTime = ((maxItem < 0) ? 0f : this.RawItems[0].Time);
				float endTime = ((maxItem < 0) ? 0f : this.RawItems[maxItem].Time);
				this.Curve = new TargetPositionCache.CacheCurve(startTime, endTime, 0.016666668f);
				this.Curve.Add((maxItem < 0) ? TargetPositionCache.CacheCurve.Item.Empty : this.RawItems[0].Item);
				for (int i = 1; i <= maxItem; i++)
				{
					this.Curve.AddUntil(this.RawItems[i].Item, this.RawItems[i].Time, this.RawItems[i].IsCut);
				}
				this.RawItems.Clear();
			}

			// Token: 0x04000362 RID: 866
			public TargetPositionCache.CacheCurve Curve;

			// Token: 0x04000363 RID: 867
			private List<TargetPositionCache.CacheEntry.RecordingItem> RawItems = new List<TargetPositionCache.CacheEntry.RecordingItem>();

			// Token: 0x020000A4 RID: 164
			private struct RecordingItem
			{
				// Token: 0x04000364 RID: 868
				public float Time;

				// Token: 0x04000365 RID: 869
				public bool IsCut;

				// Token: 0x04000366 RID: 870
				public TargetPositionCache.CacheCurve.Item Item;
			}
		}

		// Token: 0x020000A5 RID: 165
		public struct TimeRange
		{
			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001665E File Offset: 0x0001485E
			public bool IsEmpty
			{
				get
				{
					return this.End < this.Start;
				}
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x0001666E File Offset: 0x0001486E
			public bool Contains(float time)
			{
				return time >= this.Start && time <= this.End;
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x00016688 File Offset: 0x00014888
			public static TargetPositionCache.TimeRange Empty
			{
				get
				{
					return new TargetPositionCache.TimeRange
					{
						Start = float.MaxValue,
						End = float.MinValue
					};
				}
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x000166B6 File Offset: 0x000148B6
			public void Include(float time)
			{
				this.Start = Mathf.Min(this.Start, time);
				this.End = Mathf.Max(this.End, time);
			}

			// Token: 0x04000367 RID: 871
			public float Start;

			// Token: 0x04000368 RID: 872
			public float End;
		}
	}
}

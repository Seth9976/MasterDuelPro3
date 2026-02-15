using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A0 RID: 416
	internal struct InclusiveRange
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0002A49A File Offset: 0x0002869A
		public InclusiveRange(short startEnd)
		{
			this.start = startEnd;
			this.end = startEnd;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002A4AA File Offset: 0x000286AA
		public InclusiveRange(short start, short end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002A4BA File Offset: 0x000286BA
		public void Expand(short index)
		{
			this.start = Math.Min(this.start, index);
			this.end = Math.Max(this.end, index);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002A4E0 File Offset: 0x000286E0
		public void Clamp(short min, short max)
		{
			this.start = Math.Max(min, this.start);
			this.end = Math.Min(max, this.end);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0002A506 File Offset: 0x00028706
		public bool isEmpty
		{
			get
			{
				return this.end < this.start;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002A516 File Offset: 0x00028716
		public bool Contains(short index)
		{
			return index >= this.start && index <= this.end;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002A52F File Offset: 0x0002872F
		public static InclusiveRange Merge(InclusiveRange a, InclusiveRange b)
		{
			return new InclusiveRange(Math.Min(a.start, b.start), Math.Max(a.end, b.end));
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002A558 File Offset: 0x00028758
		public static InclusiveRange empty
		{
			get
			{
				return new InclusiveRange(short.MaxValue, short.MinValue);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002A569 File Offset: 0x00028769
		public override string ToString()
		{
			return string.Format("[{0}, {1}]", this.start, this.end);
		}

		// Token: 0x04000921 RID: 2337
		public short start;

		// Token: 0x04000922 RID: 2338
		public short end;
	}
}

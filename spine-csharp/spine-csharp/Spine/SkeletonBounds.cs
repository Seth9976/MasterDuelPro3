using System;

namespace Spine
{
	// Token: 0x02000076 RID: 118
	public class SkeletonBounds
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000158A1 File Offset: 0x00013AA1
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000158A9 File Offset: 0x00013AA9
		public ExposedList<BoundingBoxAttachment> BoundingBoxes { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000158B2 File Offset: 0x00013AB2
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000158BA File Offset: 0x00013ABA
		public ExposedList<Polygon> Polygons { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000158C3 File Offset: 0x00013AC3
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000158CB File Offset: 0x00013ACB
		public float MinX
		{
			get
			{
				return this.minX;
			}
			set
			{
				this.minX = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000158D4 File Offset: 0x00013AD4
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000158DC File Offset: 0x00013ADC
		public float MinY
		{
			get
			{
				return this.minY;
			}
			set
			{
				this.minY = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000158E5 File Offset: 0x00013AE5
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x000158ED File Offset: 0x00013AED
		public float MaxX
		{
			get
			{
				return this.maxX;
			}
			set
			{
				this.maxX = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000158F6 File Offset: 0x00013AF6
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000158FE File Offset: 0x00013AFE
		public float MaxY
		{
			get
			{
				return this.maxY;
			}
			set
			{
				this.maxY = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00015907 File Offset: 0x00013B07
		public float Width
		{
			get
			{
				return this.maxX - this.minX;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00015916 File Offset: 0x00013B16
		public float Height
		{
			get
			{
				return this.maxY - this.minY;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015925 File Offset: 0x00013B25
		public SkeletonBounds()
		{
			this.BoundingBoxes = new ExposedList<BoundingBoxAttachment>();
			this.Polygons = new ExposedList<Polygon>();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015950 File Offset: 0x00013B50
		public void Update(Skeleton skeleton, bool updateAabb)
		{
			ExposedList<BoundingBoxAttachment> boundingBoxes = this.BoundingBoxes;
			ExposedList<Polygon> polygons = this.Polygons;
			Slot[] slots = skeleton.slots.Items;
			int slotCount = skeleton.slots.Count;
			boundingBoxes.Clear(true);
			int i = 0;
			int j = polygons.Count;
			while (i < j)
			{
				this.polygonPool.Add(polygons.Items[i]);
				i++;
			}
			polygons.Clear(true);
			for (int k = 0; k < slotCount; k++)
			{
				Slot slot = slots[k];
				if (slot.bone.active)
				{
					BoundingBoxAttachment boundingBox = slot.attachment as BoundingBoxAttachment;
					if (boundingBox != null)
					{
						boundingBoxes.Add(boundingBox);
						int poolCount = this.polygonPool.Count;
						Polygon polygon;
						if (poolCount > 0)
						{
							polygon = this.polygonPool.Items[poolCount - 1];
							this.polygonPool.RemoveAt(poolCount - 1);
						}
						else
						{
							polygon = new Polygon();
						}
						polygons.Add(polygon);
						int count = boundingBox.worldVerticesLength;
						polygon.Count = count;
						if (polygon.Vertices.Length < count)
						{
							polygon.Vertices = new float[count];
						}
						boundingBox.ComputeWorldVertices(slot, polygon.Vertices);
					}
				}
			}
			if (updateAabb)
			{
				this.AabbCompute();
				return;
			}
			this.minX = -2.1474836E+09f;
			this.minY = -2.1474836E+09f;
			this.maxX = 2.1474836E+09f;
			this.maxY = 2.1474836E+09f;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00015AC4 File Offset: 0x00013CC4
		private void AabbCompute()
		{
			float minX = 2.1474836E+09f;
			float minY = 2.1474836E+09f;
			float maxX = -2.1474836E+09f;
			float maxY = -2.1474836E+09f;
			Polygon[] polygons = this.Polygons.Items;
			int i = 0;
			int j = this.Polygons.Count;
			while (i < j)
			{
				Polygon polygon = polygons[i];
				float[] vertices = polygon.Vertices;
				int ii = 0;
				int nn = polygon.Count;
				while (ii < nn)
				{
					float x = vertices[ii];
					float y = vertices[ii + 1];
					minX = Math.Min(minX, x);
					minY = Math.Min(minY, y);
					maxX = Math.Max(maxX, x);
					maxY = Math.Max(maxY, y);
					ii += 2;
				}
				i++;
			}
			this.minX = minX;
			this.minY = minY;
			this.maxX = maxX;
			this.maxY = maxY;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00015B89 File Offset: 0x00013D89
		public bool AabbContainsPoint(float x, float y)
		{
			return x >= this.minX && x <= this.maxX && y >= this.minY && y <= this.maxY;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public bool AabbIntersectsSegment(float x1, float y1, float x2, float y2)
		{
			float minX = this.minX;
			float minY = this.minY;
			float maxX = this.maxX;
			float maxY = this.maxY;
			if ((x1 <= minX && x2 <= minX) || (y1 <= minY && y2 <= minY) || (x1 >= maxX && x2 >= maxX) || (y1 >= maxY && y2 >= maxY))
			{
				return false;
			}
			float i = (y2 - y1) / (x2 - x1);
			float y3 = i * (minX - x1) + y1;
			if (y3 > minY && y3 < maxY)
			{
				return true;
			}
			y3 = i * (maxX - x1) + y1;
			if (y3 > minY && y3 < maxY)
			{
				return true;
			}
			float x3 = (minY - y1) / i + x1;
			if (x3 > minX && x3 < maxX)
			{
				return true;
			}
			x3 = (maxY - y1) / i + x1;
			return x3 > minX && x3 < maxX;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015C64 File Offset: 0x00013E64
		public bool AabbIntersectsSkeleton(SkeletonBounds bounds)
		{
			return this.minX < bounds.maxX && this.maxX > bounds.minX && this.minY < bounds.maxY && this.maxY > bounds.minY;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015CA0 File Offset: 0x00013EA0
		public bool ContainsPoint(Polygon polygon, float x, float y)
		{
			float[] vertices = polygon.Vertices;
			int nn = polygon.Count;
			int prevIndex = nn - 2;
			bool inside = false;
			for (int ii = 0; ii < nn; ii += 2)
			{
				float vertexY = vertices[ii + 1];
				float prevY = vertices[prevIndex + 1];
				if ((vertexY < y && prevY >= y) || (prevY < y && vertexY >= y))
				{
					float vertexX = vertices[ii];
					if (vertexX + (y - vertexY) / (prevY - vertexY) * (vertices[prevIndex] - vertexX) < x)
					{
						inside = !inside;
					}
				}
				prevIndex = ii;
			}
			return inside;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00015D1C File Offset: 0x00013F1C
		public BoundingBoxAttachment ContainsPoint(float x, float y)
		{
			Polygon[] polygons = this.Polygons.Items;
			int i = 0;
			int j = this.Polygons.Count;
			while (i < j)
			{
				if (this.ContainsPoint(polygons[i], x, y))
				{
					return this.BoundingBoxes.Items[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015D6C File Offset: 0x00013F6C
		public BoundingBoxAttachment IntersectsSegment(float x1, float y1, float x2, float y2)
		{
			Polygon[] polygons = this.Polygons.Items;
			int i = 0;
			int j = this.Polygons.Count;
			while (i < j)
			{
				if (this.IntersectsSegment(polygons[i], x1, y1, x2, y2))
				{
					return this.BoundingBoxes.Items[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015DBC File Offset: 0x00013FBC
		public bool IntersectsSegment(Polygon polygon, float x1, float y1, float x2, float y2)
		{
			float[] vertices = polygon.Vertices;
			int nn = polygon.Count;
			float width12 = x1 - x2;
			float height12 = y1 - y2;
			float det = x1 * y2 - y1 * x2;
			float x3 = vertices[nn - 2];
			float y3 = vertices[nn - 1];
			for (int ii = 0; ii < nn; ii += 2)
			{
				float x4 = vertices[ii];
				float y4 = vertices[ii + 1];
				float det2 = x3 * y4 - y3 * x4;
				float width13 = x3 - x4;
				float height13 = y3 - y4;
				float det3 = width12 * height13 - height12 * width13;
				float x5 = (det * width13 - width12 * det2) / det3;
				if (((x5 >= x3 && x5 <= x4) || (x5 >= x4 && x5 <= x3)) && ((x5 >= x1 && x5 <= x2) || (x5 >= x2 && x5 <= x1)))
				{
					float y5 = (det * height13 - height12 * det2) / det3;
					if (((y5 >= y3 && y5 <= y4) || (y5 >= y4 && y5 <= y3)) && ((y5 >= y1 && y5 <= y2) || (y5 >= y2 && y5 <= y1)))
					{
						return true;
					}
				}
				x3 = x4;
				y3 = y4;
			}
			return false;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00015ECC File Offset: 0x000140CC
		public Polygon GetPolygon(BoundingBoxAttachment attachment)
		{
			int index = this.BoundingBoxes.IndexOf(attachment);
			if (index != -1)
			{
				return this.Polygons.Items[index];
			}
			return null;
		}

		// Token: 0x04000285 RID: 645
		private ExposedList<Polygon> polygonPool = new ExposedList<Polygon>();

		// Token: 0x04000286 RID: 646
		private float minX;

		// Token: 0x04000287 RID: 647
		private float minY;

		// Token: 0x04000288 RID: 648
		private float maxX;

		// Token: 0x04000289 RID: 649
		private float maxY;
	}
}

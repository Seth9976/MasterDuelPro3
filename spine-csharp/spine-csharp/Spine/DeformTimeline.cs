using System;

namespace Spine
{
	// Token: 0x02000022 RID: 34
	public class DeformTimeline : CurveTimeline, ISlotTimeline
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005188 File Offset: 0x00003388
		public DeformTimeline(int frameCount, int bezierCount, int slotIndex, VertexAttachment attachment)
			: base(frameCount, bezierCount, new string[] { string.Concat(new string[]
			{
				12.ToString(),
				"|",
				slotIndex.ToString(),
				"|",
				attachment.Id.ToString()
			}) })
		{
			this.slotIndex = slotIndex;
			this.attachment = attachment;
			this.vertices = new float[frameCount][];
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005205 File Offset: 0x00003405
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000520D File Offset: 0x0000340D
		public VertexAttachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005215 File Offset: 0x00003415
		public float[][] Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000521D File Offset: 0x0000341D
		public void SetFrame(int frame, float time, float[] vertices)
		{
			this.frames[frame] = time;
			this.vertices[frame] = vertices;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005234 File Offset: 0x00003434
		public void setBezier(int bezier, int frame, int value, float time1, float value1, float cx1, float cy1, float cx2, float cy2, float time2, float value2)
		{
			float[] curves = this.curves;
			int i = this.FrameCount + bezier * 18;
			if (value == 0)
			{
				curves[frame] = (float)(2 + i);
			}
			float tmpx = (time1 - cx1 * 2f + cx2) * 0.03f;
			float tmpy = cy2 * 0.03f - cy1 * 0.06f;
			float dddx = ((cx1 - cx2) * 3f - time1 + time2) * 0.006f;
			float dddy = (cy1 - cy2 + 0.33333334f) * 0.018f;
			float ddx = tmpx * 2f + dddx;
			float ddy = tmpy * 2f + dddy;
			float dx = (cx1 - time1) * 0.3f + tmpx + dddx * 0.16666667f;
			float dy = cy1 * 0.3f + tmpy + dddy * 0.16666667f;
			float x = time1 + dx;
			float y = dy;
			int j = i + 18;
			while (i < j)
			{
				curves[i] = x;
				curves[i + 1] = y;
				dx += ddx;
				dy += ddy;
				ddx += dddx;
				ddy += dddy;
				x += dx;
				y += dy;
				i += 2;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005348 File Offset: 0x00003548
		private float GetCurvePercent(float time, int frame)
		{
			float[] curves = this.curves;
			int i = (int)curves[frame];
			if (i == 0)
			{
				float x = this.frames[frame];
				return (time - x) / (this.frames[frame + this.FrameEntries] - x);
			}
			if (i == 1)
			{
				return 0f;
			}
			i -= 2;
			if (curves[i] > time)
			{
				float x2 = this.frames[frame];
				return curves[i + 1] * (time - x2) / (curves[i] - x2);
			}
			int j = i + 18;
			for (i += 2; i < j; i += 2)
			{
				if (curves[i] >= time)
				{
					float x3 = curves[i - 2];
					float y = curves[i - 1];
					return y + (time - x3) / (curves[i] - x3) * (curves[i + 1] - y);
				}
			}
			float x4 = curves[j - 2];
			float y2 = curves[j - 1];
			return y2 + (1f - y2) * (time - x4) / (this.frames[frame + this.FrameEntries] - x4);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005428 File Offset: 0x00003628
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (!slot.bone.active)
			{
				return;
			}
			VertexAttachment vertexAttachment = slot.attachment as VertexAttachment;
			if (vertexAttachment == null || vertexAttachment.TimelineAttachment != this.attachment)
			{
				return;
			}
			ExposedList<float> deformArray = slot.deform;
			if (deformArray.Count == 0)
			{
				blend = MixBlend.Setup;
			}
			float[][] vertices = this.vertices;
			int vertexCount = vertices[0].Length;
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					deformArray.Clear(true);
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				if (alpha == 1f)
				{
					deformArray.Clear(true);
					return;
				}
				if (deformArray.Capacity < vertexCount)
				{
					deformArray.Capacity = vertexCount;
				}
				deformArray.Count = vertexCount;
				float[] deform = deformArray.Items;
				if (vertexAttachment.bones == null)
				{
					float[] setupVertices = vertexAttachment.vertices;
					for (int i = 0; i < vertexCount; i++)
					{
						deform[i] += (setupVertices[i] - deform[i]) * alpha;
					}
					return;
				}
				alpha = 1f - alpha;
				for (int j = 0; j < vertexCount; j++)
				{
					deform[j] *= alpha;
				}
				return;
			}
			else
			{
				if (deformArray.Capacity < vertexCount)
				{
					deformArray.Capacity = vertexCount;
				}
				deformArray.Count = vertexCount;
				float[] deform = deformArray.Items;
				if (time >= frames[frames.Length - 1])
				{
					float[] lastVertices = vertices[frames.Length - 1];
					if (alpha == 1f)
					{
						if (blend != MixBlend.Add)
						{
							Array.Copy(lastVertices, 0, deform, 0, vertexCount);
							return;
						}
						if (vertexAttachment.bones == null)
						{
							float[] setupVertices2 = vertexAttachment.vertices;
							for (int k = 0; k < vertexCount; k++)
							{
								deform[k] += lastVertices[k] - setupVertices2[k];
							}
							return;
						}
						for (int l = 0; l < vertexCount; l++)
						{
							deform[l] += lastVertices[l];
						}
						return;
					}
					else
					{
						switch (blend)
						{
						case MixBlend.Setup:
						{
							if (vertexAttachment.bones == null)
							{
								float[] setupVertices3 = vertexAttachment.vertices;
								for (int m = 0; m < vertexCount; m++)
								{
									float setup = setupVertices3[m];
									deform[m] = setup + (lastVertices[m] - setup) * alpha;
								}
								return;
							}
							for (int n = 0; n < vertexCount; n++)
							{
								deform[n] = lastVertices[n] * alpha;
							}
							return;
						}
						case MixBlend.First:
						case MixBlend.Replace:
						{
							for (int i2 = 0; i2 < vertexCount; i2++)
							{
								deform[i2] += (lastVertices[i2] - deform[i2]) * alpha;
							}
							return;
						}
						case MixBlend.Add:
						{
							if (vertexAttachment.bones == null)
							{
								float[] setupVertices4 = vertexAttachment.vertices;
								for (int i3 = 0; i3 < vertexCount; i3++)
								{
									deform[i3] += (lastVertices[i3] - setupVertices4[i3]) * alpha;
								}
								return;
							}
							for (int i4 = 0; i4 < vertexCount; i4++)
							{
								deform[i4] += lastVertices[i4] * alpha;
							}
							return;
						}
						default:
							return;
						}
					}
				}
				else
				{
					int frame = Timeline.Search(frames, time);
					float percent = this.GetCurvePercent(time, frame);
					float[] prevVertices = vertices[frame];
					float[] nextVertices = vertices[frame + 1];
					if (alpha == 1f)
					{
						if (blend != MixBlend.Add)
						{
							for (int i5 = 0; i5 < vertexCount; i5++)
							{
								float prev = prevVertices[i5];
								deform[i5] = prev + (nextVertices[i5] - prev) * percent;
							}
							return;
						}
						if (vertexAttachment.bones == null)
						{
							float[] setupVertices5 = vertexAttachment.vertices;
							for (int i6 = 0; i6 < vertexCount; i6++)
							{
								float prev2 = prevVertices[i6];
								deform[i6] += prev2 + (nextVertices[i6] - prev2) * percent - setupVertices5[i6];
							}
							return;
						}
						for (int i7 = 0; i7 < vertexCount; i7++)
						{
							float prev3 = prevVertices[i7];
							deform[i7] += prev3 + (nextVertices[i7] - prev3) * percent;
						}
						return;
					}
					else
					{
						switch (blend)
						{
						case MixBlend.Setup:
						{
							if (vertexAttachment.bones == null)
							{
								float[] setupVertices6 = vertexAttachment.vertices;
								for (int i8 = 0; i8 < vertexCount; i8++)
								{
									float prev4 = prevVertices[i8];
									float setup2 = setupVertices6[i8];
									deform[i8] = setup2 + (prev4 + (nextVertices[i8] - prev4) * percent - setup2) * alpha;
								}
								return;
							}
							for (int i9 = 0; i9 < vertexCount; i9++)
							{
								float prev5 = prevVertices[i9];
								deform[i9] = (prev5 + (nextVertices[i9] - prev5) * percent) * alpha;
							}
							return;
						}
						case MixBlend.First:
						case MixBlend.Replace:
						{
							for (int i10 = 0; i10 < vertexCount; i10++)
							{
								float prev6 = prevVertices[i10];
								deform[i10] += (prev6 + (nextVertices[i10] - prev6) * percent - deform[i10]) * alpha;
							}
							return;
						}
						case MixBlend.Add:
						{
							if (vertexAttachment.bones == null)
							{
								float[] setupVertices7 = vertexAttachment.vertices;
								for (int i11 = 0; i11 < vertexCount; i11++)
								{
									float prev7 = prevVertices[i11];
									deform[i11] += (prev7 + (nextVertices[i11] - prev7) * percent - setupVertices7[i11]) * alpha;
								}
								return;
							}
							for (int i12 = 0; i12 < vertexCount; i12++)
							{
								float prev8 = prevVertices[i12];
								deform[i12] += (prev8 + (nextVertices[i12] - prev8) * percent) * alpha;
							}
							return;
						}
						default:
							return;
						}
					}
				}
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly int slotIndex;

		// Token: 0x0400007F RID: 127
		private readonly VertexAttachment attachment;

		// Token: 0x04000080 RID: 128
		internal float[][] vertices;
	}
}

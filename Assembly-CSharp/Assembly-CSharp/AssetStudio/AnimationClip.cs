using System;

namespace AssetStudio
{
	// Token: 0x020000B9 RID: 185
	public sealed class AnimationClip : NamedObject
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public AnimationClip(ObjectReader reader)
			: base(reader)
		{
			if (this.version[0] >= 5)
			{
				this.m_Legacy = reader.ReadBoolean();
			}
			else if (this.version[0] >= 4)
			{
				this.m_AnimationType = (AnimationType)reader.ReadInt32();
				if (this.m_AnimationType == AnimationType.Legacy)
				{
					this.m_Legacy = true;
				}
			}
			else
			{
				this.m_Legacy = true;
			}
			this.m_Compressed = reader.ReadBoolean();
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				this.m_UseHighQualityCurve = reader.ReadBoolean();
			}
			reader.AlignStream();
			int numRCurves = reader.ReadInt32();
			this.m_RotationCurves = new QuaternionCurve[numRCurves];
			for (int i = 0; i < numRCurves; i++)
			{
				this.m_RotationCurves[i] = new QuaternionCurve(reader);
			}
			int numCRCurves = reader.ReadInt32();
			this.m_CompressedRotationCurves = new CompressedAnimationCurve[numCRCurves];
			for (int j = 0; j < numCRCurves; j++)
			{
				this.m_CompressedRotationCurves[j] = new CompressedAnimationCurve(reader);
			}
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 3))
			{
				int numEulerCurves = reader.ReadInt32();
				this.m_EulerCurves = new Vector3Curve[numEulerCurves];
				for (int k = 0; k < numEulerCurves; k++)
				{
					this.m_EulerCurves[k] = new Vector3Curve(reader);
				}
			}
			int numPCurves = reader.ReadInt32();
			this.m_PositionCurves = new Vector3Curve[numPCurves];
			for (int l = 0; l < numPCurves; l++)
			{
				this.m_PositionCurves[l] = new Vector3Curve(reader);
			}
			int numSCurves = reader.ReadInt32();
			this.m_ScaleCurves = new Vector3Curve[numSCurves];
			for (int m = 0; m < numSCurves; m++)
			{
				this.m_ScaleCurves[m] = new Vector3Curve(reader);
			}
			int numFCurves = reader.ReadInt32();
			this.m_FloatCurves = new FloatCurve[numFCurves];
			for (int n = 0; n < numFCurves; n++)
			{
				this.m_FloatCurves[n] = new FloatCurve(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				int numPtrCurves = reader.ReadInt32();
				this.m_PPtrCurves = new PPtrCurve[numPtrCurves];
				for (int i2 = 0; i2 < numPtrCurves; i2++)
				{
					this.m_PPtrCurves[i2] = new PPtrCurve(reader);
				}
			}
			this.m_SampleRate = reader.ReadSingle();
			this.m_WrapMode = reader.ReadInt32();
			if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 4))
			{
				this.m_Bounds = new AABB(reader);
			}
			if (this.version[0] >= 4)
			{
				this.m_MuscleClipSize = reader.ReadUInt32();
				this.m_MuscleClip = new ClipMuscleConstant(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				this.m_ClipBindingConstant = new AnimationClipBindingConstant(reader);
			}
			if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 3))
			{
				reader.ReadBoolean();
				reader.ReadBoolean();
				reader.AlignStream();
			}
			int numEvents = reader.ReadInt32();
			this.m_Events = new AnimationEvent[numEvents];
			for (int i3 = 0; i3 < numEvents; i3++)
			{
				this.m_Events[i3] = new AnimationEvent(reader);
			}
			if (this.version[0] >= 2017)
			{
				reader.AlignStream();
			}
		}

		// Token: 0x040005CA RID: 1482
		public AnimationType m_AnimationType;

		// Token: 0x040005CB RID: 1483
		public bool m_Legacy;

		// Token: 0x040005CC RID: 1484
		public bool m_Compressed;

		// Token: 0x040005CD RID: 1485
		public bool m_UseHighQualityCurve;

		// Token: 0x040005CE RID: 1486
		public QuaternionCurve[] m_RotationCurves;

		// Token: 0x040005CF RID: 1487
		public CompressedAnimationCurve[] m_CompressedRotationCurves;

		// Token: 0x040005D0 RID: 1488
		public Vector3Curve[] m_EulerCurves;

		// Token: 0x040005D1 RID: 1489
		public Vector3Curve[] m_PositionCurves;

		// Token: 0x040005D2 RID: 1490
		public Vector3Curve[] m_ScaleCurves;

		// Token: 0x040005D3 RID: 1491
		public FloatCurve[] m_FloatCurves;

		// Token: 0x040005D4 RID: 1492
		public PPtrCurve[] m_PPtrCurves;

		// Token: 0x040005D5 RID: 1493
		public float m_SampleRate;

		// Token: 0x040005D6 RID: 1494
		public int m_WrapMode;

		// Token: 0x040005D7 RID: 1495
		public AABB m_Bounds;

		// Token: 0x040005D8 RID: 1496
		public uint m_MuscleClipSize;

		// Token: 0x040005D9 RID: 1497
		public ClipMuscleConstant m_MuscleClip;

		// Token: 0x040005DA RID: 1498
		public AnimationClipBindingConstant m_ClipBindingConstant;

		// Token: 0x040005DB RID: 1499
		public AnimationEvent[] m_Events;
	}
}

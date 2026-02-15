using System;

namespace AssetStudio
{
	// Token: 0x020000B4 RID: 180
	public class ClipMuscleConstant
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000D440 File Offset: 0x0000B640
		public ClipMuscleConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_DeltaPose = new HumanPose(reader);
			this.m_StartX = new xform(reader);
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 5))
			{
				this.m_StopX = new xform(reader);
			}
			this.m_LeftFootStartX = new xform(reader);
			this.m_RightFootStartX = new xform(reader);
			if (version[0] < 5)
			{
				this.m_MotionStartX = new xform(reader);
				this.m_MotionStopX = new xform(reader);
			}
			this.m_AverageSpeed = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
			this.m_Clip = new Clip(reader);
			this.m_StartTime = reader.ReadSingle();
			this.m_StopTime = reader.ReadSingle();
			this.m_OrientationOffsetY = reader.ReadSingle();
			this.m_Level = reader.ReadSingle();
			this.m_CycleOffset = reader.ReadSingle();
			this.m_AverageAngularSpeed = reader.ReadSingle();
			this.m_IndexArray = reader.ReadInt32Array();
			if (version[0] < 4 || (version[0] == 4 && version[1] < 3))
			{
				reader.ReadInt32Array();
			}
			int numDeltas = reader.ReadInt32();
			this.m_ValueArrayDelta = new ValueDelta[numDeltas];
			for (int i = 0; i < numDeltas; i++)
			{
				this.m_ValueArrayDelta[i] = new ValueDelta(reader);
			}
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 3))
			{
				this.m_ValueArrayReferencePose = reader.ReadSingleArray();
			}
			this.m_Mirror = reader.ReadBoolean();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_LoopTime = reader.ReadBoolean();
			}
			this.m_LoopBlend = reader.ReadBoolean();
			this.m_LoopBlendOrientation = reader.ReadBoolean();
			this.m_LoopBlendPositionY = reader.ReadBoolean();
			this.m_LoopBlendPositionXZ = reader.ReadBoolean();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 5))
			{
				this.m_StartAtOrigin = reader.ReadBoolean();
			}
			this.m_KeepOriginalOrientation = reader.ReadBoolean();
			this.m_KeepOriginalPositionY = reader.ReadBoolean();
			this.m_KeepOriginalPositionXZ = reader.ReadBoolean();
			this.m_HeightFromFeet = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x04000599 RID: 1433
		public HumanPose m_DeltaPose;

		// Token: 0x0400059A RID: 1434
		public xform m_StartX;

		// Token: 0x0400059B RID: 1435
		public xform m_StopX;

		// Token: 0x0400059C RID: 1436
		public xform m_LeftFootStartX;

		// Token: 0x0400059D RID: 1437
		public xform m_RightFootStartX;

		// Token: 0x0400059E RID: 1438
		public xform m_MotionStartX;

		// Token: 0x0400059F RID: 1439
		public xform m_MotionStopX;

		// Token: 0x040005A0 RID: 1440
		public Vector3 m_AverageSpeed;

		// Token: 0x040005A1 RID: 1441
		public Clip m_Clip;

		// Token: 0x040005A2 RID: 1442
		public float m_StartTime;

		// Token: 0x040005A3 RID: 1443
		public float m_StopTime;

		// Token: 0x040005A4 RID: 1444
		public float m_OrientationOffsetY;

		// Token: 0x040005A5 RID: 1445
		public float m_Level;

		// Token: 0x040005A6 RID: 1446
		public float m_CycleOffset;

		// Token: 0x040005A7 RID: 1447
		public float m_AverageAngularSpeed;

		// Token: 0x040005A8 RID: 1448
		public int[] m_IndexArray;

		// Token: 0x040005A9 RID: 1449
		public ValueDelta[] m_ValueArrayDelta;

		// Token: 0x040005AA RID: 1450
		public float[] m_ValueArrayReferencePose;

		// Token: 0x040005AB RID: 1451
		public bool m_Mirror;

		// Token: 0x040005AC RID: 1452
		public bool m_LoopTime;

		// Token: 0x040005AD RID: 1453
		public bool m_LoopBlend;

		// Token: 0x040005AE RID: 1454
		public bool m_LoopBlendOrientation;

		// Token: 0x040005AF RID: 1455
		public bool m_LoopBlendPositionY;

		// Token: 0x040005B0 RID: 1456
		public bool m_LoopBlendPositionXZ;

		// Token: 0x040005B1 RID: 1457
		public bool m_StartAtOrigin;

		// Token: 0x040005B2 RID: 1458
		public bool m_KeepOriginalOrientation;

		// Token: 0x040005B3 RID: 1459
		public bool m_KeepOriginalPositionY;

		// Token: 0x040005B4 RID: 1460
		public bool m_KeepOriginalPositionXZ;

		// Token: 0x040005B5 RID: 1461
		public bool m_HeightFromFeet;
	}
}

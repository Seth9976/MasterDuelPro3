using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E0 RID: 224
	internal sealed class MotionVectorsPersistentData
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00015590 File Offset: 0x00013790
		internal MotionVectorsPersistentData()
		{
			this.Reset();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00015621 File Offset: 0x00013821
		internal int lastFrameIndex
		{
			get
			{
				return this.m_LastFrameIndex[0];
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001562B File Offset: 0x0001382B
		internal Matrix4x4 viewProjection
		{
			get
			{
				return this.m_ViewProjection[0];
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00015639 File Offset: 0x00013839
		internal Matrix4x4 previousViewProjection
		{
			get
			{
				return this.m_PreviousViewProjection[0];
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00015647 File Offset: 0x00013847
		internal Matrix4x4[] viewProjectionStereo
		{
			get
			{
				return this.m_ViewProjection;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001564F File Offset: 0x0001384F
		internal Matrix4x4[] previousViewProjectionStereo
		{
			get
			{
				return this.m_PreviousViewProjection;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00015657 File Offset: 0x00013857
		internal Matrix4x4[] projectionStereo
		{
			get
			{
				return this.m_Projection;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001565F File Offset: 0x0001385F
		internal Matrix4x4[] previousProjectionStereo
		{
			get
			{
				return this.m_PreviousProjection;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00015667 File Offset: 0x00013867
		internal Matrix4x4[] previousPreviousProjectionStereo
		{
			get
			{
				return this.m_PreviousPreviousProjection;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001566F File Offset: 0x0001386F
		internal Matrix4x4[] viewStereo
		{
			get
			{
				return this.m_View;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00015677 File Offset: 0x00013877
		internal Matrix4x4[] previousViewStereo
		{
			get
			{
				return this.m_PreviousView;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001567F File Offset: 0x0001387F
		internal Matrix4x4[] previousPreviousViewStereo
		{
			get
			{
				return this.m_PreviousPreviousView;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00015687 File Offset: 0x00013887
		internal float deltaTime
		{
			get
			{
				return this.m_deltaTime;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001568F File Offset: 0x0001388F
		internal float lastDeltaTime
		{
			get
			{
				return this.m_lastDeltaTime;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00015697 File Offset: 0x00013897
		internal Vector3 worldSpaceCameraPos
		{
			get
			{
				return this.m_worldSpaceCameraPos;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001569F File Offset: 0x0001389F
		internal Vector3 previousWorldSpaceCameraPos
		{
			get
			{
				return this.m_previousWorldSpaceCameraPos;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000156A7 File Offset: 0x000138A7
		internal Vector3 previousPreviousWorldSpaceCameraPos
		{
			get
			{
				return this.m_previousPreviousWorldSpaceCameraPos;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000156B0 File Offset: 0x000138B0
		public void Reset()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_Projection[i] = Matrix4x4.identity;
				this.m_View[i] = Matrix4x4.identity;
				this.m_ViewProjection[i] = Matrix4x4.identity;
				this.m_PreviousProjection[i] = Matrix4x4.identity;
				this.m_PreviousView[i] = Matrix4x4.identity;
				this.m_PreviousViewProjection[i] = Matrix4x4.identity;
				this.m_PreviousProjection[i] = Matrix4x4.identity;
				this.m_PreviousView[i] = Matrix4x4.identity;
				this.m_PreviousViewProjection[i] = Matrix4x4.identity;
				this.m_LastFrameIndex[i] = -1;
				this.m_PrevAspectRatio[i] = -1f;
			}
			this.m_deltaTime = 0f;
			this.m_lastDeltaTime = 0f;
			this.m_worldSpaceCameraPos = Vector3.zero;
			this.m_previousWorldSpaceCameraPos = Vector3.zero;
			this.m_previousPreviousWorldSpaceCameraPos = Vector3.zero;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000157B5 File Offset: 0x000139B5
		private static int GetXRMultiPassId(XRPass xr)
		{
			if (!xr.enabled)
			{
				return 0;
			}
			return xr.multipassId;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000157C8 File Offset: 0x000139C8
		public void Update(UniversalCameraData cameraData)
		{
			int eyeIndex = MotionVectorsPersistentData.GetXRMultiPassId(cameraData.xr);
			bool flag = !cameraData.xr.enabled || cameraData.xr.singlePassEnabled || eyeIndex == 0;
			int frameIndex = Time.frameCount;
			if (flag)
			{
				bool flag2 = this.m_LastFrameIndex[0] == -1;
				float deltaTime = Time.deltaTime;
				Vector3 worldSpaceCameraPos = cameraData.camera.transform.position;
				if (flag2)
				{
					this.m_lastDeltaTime = deltaTime;
					this.m_deltaTime = deltaTime;
					this.m_previousPreviousWorldSpaceCameraPos = worldSpaceCameraPos;
					this.m_previousWorldSpaceCameraPos = worldSpaceCameraPos;
					this.m_worldSpaceCameraPos = worldSpaceCameraPos;
				}
				this.m_lastDeltaTime = this.m_deltaTime;
				this.m_deltaTime = deltaTime;
				this.m_previousPreviousWorldSpaceCameraPos = this.m_previousWorldSpaceCameraPos;
				this.m_previousWorldSpaceCameraPos = this.m_worldSpaceCameraPos;
				this.m_worldSpaceCameraPos = worldSpaceCameraPos;
			}
			bool aspectChanged = this.m_PrevAspectRatio[eyeIndex] != cameraData.aspectRatio;
			if (this.m_LastFrameIndex[eyeIndex] != frameIndex || aspectChanged)
			{
				bool isPreviousFrameDataInvalid = this.m_LastFrameIndex[eyeIndex] == -1 || aspectChanged;
				int numActiveViews = (cameraData.xr.enabled ? cameraData.xr.viewCount : 1);
				for (int viewIndex = 0; viewIndex < numActiveViews; viewIndex++)
				{
					int targetIndex = viewIndex + eyeIndex;
					Matrix4x4 gpuP = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(viewIndex), true);
					Matrix4x4 gpuV = cameraData.GetViewMatrix(viewIndex);
					Matrix4x4 gpuVP = gpuP * gpuV;
					if (isPreviousFrameDataInvalid)
					{
						this.m_PreviousPreviousProjection[targetIndex] = gpuP;
						this.m_PreviousProjection[targetIndex] = gpuP;
						this.m_Projection[targetIndex] = gpuP;
						this.m_PreviousPreviousView[targetIndex] = gpuV;
						this.m_PreviousView[targetIndex] = gpuV;
						this.m_View[targetIndex] = gpuV;
						this.m_ViewProjection[targetIndex] = gpuVP;
						this.m_PreviousViewProjection[targetIndex] = gpuVP;
					}
					this.m_PreviousPreviousProjection[targetIndex] = this.m_PreviousProjection[targetIndex];
					this.m_PreviousProjection[targetIndex] = this.m_Projection[targetIndex];
					this.m_Projection[targetIndex] = gpuP;
					this.m_PreviousPreviousView[targetIndex] = this.m_PreviousView[targetIndex];
					this.m_PreviousView[targetIndex] = this.m_View[targetIndex];
					this.m_View[targetIndex] = gpuV;
					this.m_PreviousViewProjection[targetIndex] = this.m_ViewProjection[targetIndex];
					this.m_ViewProjection[targetIndex] = gpuVP;
				}
				this.m_LastFrameIndex[eyeIndex] = frameIndex;
				this.m_PrevAspectRatio[eyeIndex] = cameraData.aspectRatio;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015A68 File Offset: 0x00013C68
		public void SetGlobalMotionMatrices(RasterCommandBuffer cmd, XRPass xr)
		{
			int passID = MotionVectorsPersistentData.GetXRMultiPassId(xr);
			if (xr.enabled && xr.singlePassEnabled)
			{
				cmd.SetGlobalMatrixArray(ShaderPropertyId.previousViewProjectionNoJitterStereo, this.previousViewProjectionStereo);
				cmd.SetGlobalMatrixArray(ShaderPropertyId.viewProjectionNoJitterStereo, this.viewProjectionStereo);
				return;
			}
			cmd.SetGlobalMatrix(ShaderPropertyId.previousViewProjectionNoJitter, this.previousViewProjectionStereo[passID]);
			cmd.SetGlobalMatrix(ShaderPropertyId.viewProjectionNoJitter, this.viewProjectionStereo[passID]);
		}

		// Token: 0x040004E6 RID: 1254
		private const int k_EyeCount = 2;

		// Token: 0x040004E7 RID: 1255
		private readonly Matrix4x4[] m_Projection = new Matrix4x4[2];

		// Token: 0x040004E8 RID: 1256
		private readonly Matrix4x4[] m_View = new Matrix4x4[2];

		// Token: 0x040004E9 RID: 1257
		private readonly Matrix4x4[] m_ViewProjection = new Matrix4x4[2];

		// Token: 0x040004EA RID: 1258
		private readonly Matrix4x4[] m_PreviousProjection = new Matrix4x4[2];

		// Token: 0x040004EB RID: 1259
		private readonly Matrix4x4[] m_PreviousView = new Matrix4x4[2];

		// Token: 0x040004EC RID: 1260
		private readonly Matrix4x4[] m_PreviousViewProjection = new Matrix4x4[2];

		// Token: 0x040004ED RID: 1261
		private readonly Matrix4x4[] m_PreviousPreviousProjection = new Matrix4x4[2];

		// Token: 0x040004EE RID: 1262
		private readonly Matrix4x4[] m_PreviousPreviousView = new Matrix4x4[2];

		// Token: 0x040004EF RID: 1263
		private readonly int[] m_LastFrameIndex = new int[2];

		// Token: 0x040004F0 RID: 1264
		private readonly float[] m_PrevAspectRatio = new float[2];

		// Token: 0x040004F1 RID: 1265
		private float m_deltaTime;

		// Token: 0x040004F2 RID: 1266
		private float m_lastDeltaTime;

		// Token: 0x040004F3 RID: 1267
		private Vector3 m_worldSpaceCameraPos;

		// Token: 0x040004F4 RID: 1268
		private Vector3 m_previousWorldSpaceCameraPos;

		// Token: 0x040004F5 RID: 1269
		private Vector3 m_previousPreviousWorldSpaceCameraPos;
	}
}

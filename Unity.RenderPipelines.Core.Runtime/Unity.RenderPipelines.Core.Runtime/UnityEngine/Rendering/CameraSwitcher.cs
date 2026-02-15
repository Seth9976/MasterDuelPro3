using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001C RID: 28
	public class CameraSwitcher : MonoBehaviour
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000048A0 File Offset: 0x00002AA0
		private void OnEnable()
		{
			this.m_OriginalCamera = base.GetComponent<Camera>();
			this.m_CurrentCamera = this.m_OriginalCamera;
			if (this.m_OriginalCamera == null)
			{
				Debug.LogError("Camera Switcher needs a Camera component attached");
				return;
			}
			this.m_CurrentCameraIndex = this.GetCameraCount() - 1;
			this.m_CameraNames = new GUIContent[this.GetCameraCount()];
			this.m_CameraIndices = new int[this.GetCameraCount()];
			for (int i = 0; i < this.m_Cameras.Length; i++)
			{
				Camera cam = this.m_Cameras[i];
				if (cam != null)
				{
					this.m_CameraNames[i] = new GUIContent(cam.name);
				}
				else
				{
					this.m_CameraNames[i] = new GUIContent("null");
				}
				this.m_CameraIndices[i] = i;
			}
			this.m_CameraNames[this.GetCameraCount() - 1] = new GUIContent("Original Camera");
			this.m_CameraIndices[this.GetCameraCount() - 1] = this.GetCameraCount() - 1;
			this.m_DebugEntry = new DebugUI.EnumField
			{
				displayName = "Camera Switcher",
				getter = () => this.m_CurrentCameraIndex,
				setter = delegate(int value)
				{
					this.SetCameraIndex(value);
				},
				enumNames = this.m_CameraNames,
				enumValues = this.m_CameraIndices,
				getIndex = () => this.m_DebugEntryEnumIndex,
				setIndex = delegate(int value)
				{
					this.m_DebugEntryEnumIndex = value;
				}
			};
			DebugManager.instance.GetPanel("Camera", true, 0, false).children.Add(this.m_DebugEntry);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004A2B File Offset: 0x00002C2B
		private void OnDisable()
		{
			if (this.m_DebugEntry != null && this.m_DebugEntry.panel != null)
			{
				this.m_DebugEntry.panel.children.Remove(this.m_DebugEntry);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A5E File Offset: 0x00002C5E
		private int GetCameraCount()
		{
			return this.m_Cameras.Length + 1;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A6A File Offset: 0x00002C6A
		private Camera GetNextCamera()
		{
			if (this.m_CurrentCameraIndex == this.m_Cameras.Length)
			{
				return this.m_OriginalCamera;
			}
			return this.m_Cameras[this.m_CurrentCameraIndex];
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A90 File Offset: 0x00002C90
		private void SetCameraIndex(int index)
		{
			if (index > 0 && index < this.GetCameraCount())
			{
				this.m_CurrentCameraIndex = index;
				if (this.m_CurrentCamera == this.m_OriginalCamera)
				{
					this.m_OriginalCameraPosition = this.m_OriginalCamera.transform.position;
					this.m_OriginalCameraRotation = this.m_OriginalCamera.transform.rotation;
				}
				this.m_CurrentCamera = this.GetNextCamera();
				if (this.m_CurrentCamera != null)
				{
					if (this.m_CurrentCamera == this.m_OriginalCamera)
					{
						this.m_OriginalCamera.transform.position = this.m_OriginalCameraPosition;
						this.m_OriginalCamera.transform.rotation = this.m_OriginalCameraRotation;
					}
					base.transform.position = this.m_CurrentCamera.transform.position;
					base.transform.rotation = this.m_CurrentCamera.transform.rotation;
				}
			}
		}

		// Token: 0x0400007C RID: 124
		public Camera[] m_Cameras;

		// Token: 0x0400007D RID: 125
		private int m_CurrentCameraIndex = -1;

		// Token: 0x0400007E RID: 126
		private Camera m_OriginalCamera;

		// Token: 0x0400007F RID: 127
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x04000080 RID: 128
		private Quaternion m_OriginalCameraRotation;

		// Token: 0x04000081 RID: 129
		private Camera m_CurrentCamera;

		// Token: 0x04000082 RID: 130
		private GUIContent[] m_CameraNames;

		// Token: 0x04000083 RID: 131
		private int[] m_CameraIndices;

		// Token: 0x04000084 RID: 132
		private DebugUI.EnumField m_DebugEntry;

		// Token: 0x04000085 RID: 133
		private int m_DebugEntryEnumIndex;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	internal class SendMouseEvents
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002354 File Offset: 0x00000554
		private static void UpdateMouse()
		{
			bool flag = SendMouseEvents.s_GetMouseState != null;
			if (flag)
			{
				KeyValuePair<int, Vector2> state = SendMouseEvents.s_GetMouseState();
				SendMouseEvents.s_MousePosition = state.Value;
				SendMouseEvents.s_MouseButtonPressedThisFrame = state.Key == 2;
				SendMouseEvents.s_MouseButtonIsPressed = state.Key != 0;
			}
			else
			{
				bool flag2 = !Input.CheckDisabled();
				if (flag2)
				{
					SendMouseEvents.s_MousePosition = Input.mousePosition;
					SendMouseEvents.s_MouseButtonPressedThisFrame = Input.GetMouseButtonDown(0);
					SendMouseEvents.s_MouseButtonIsPressed = Input.GetMouseButton(0);
				}
				else
				{
					SendMouseEvents.s_MousePosition = default(Vector2);
					SendMouseEvents.s_MouseButtonPressedThisFrame = false;
					SendMouseEvents.s_MouseButtonIsPressed = false;
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000023F5 File Offset: 0x000005F5
		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			SendMouseEvents.s_MouseUsed = true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002400 File Offset: 0x00000600
		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			SendMouseEvents.UpdateMouse();
			Vector2 mousePosition = SendMouseEvents.s_MousePosition;
			int camerasCount = Camera.allCamerasCount;
			bool flag = SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != camerasCount;
			if (flag)
			{
				SendMouseEvents.m_Cameras = new Camera[camerasCount];
			}
			Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int hitIndex = 0; hitIndex < SendMouseEvents.m_CurrentHit.Length; hitIndex++)
			{
				SendMouseEvents.m_CurrentHit[hitIndex] = default(SendMouseEvents.HitInfo);
			}
			bool flag2 = !SendMouseEvents.s_MouseUsed;
			if (flag2)
			{
				foreach (Camera camera in SendMouseEvents.m_Cameras)
				{
					bool flag3 = camera == null || (skipRTCameras != 0 && camera.targetTexture != null);
					if (!flag3)
					{
						int displayIndex = camera.targetDisplay;
						Vector3 eventPosition = Display.RelativeMouseAt(mousePosition);
						bool flag4 = eventPosition != Vector3.zero;
						if (flag4)
						{
							int eventDisplayIndex = (int)eventPosition.z;
							bool flag5 = eventDisplayIndex != displayIndex;
							if (flag5)
							{
								goto IL_0368;
							}
							float w = (float)Screen.width;
							float h = (float)Screen.height;
							bool flag6 = displayIndex > 0 && displayIndex < Display.displays.Length;
							if (flag6)
							{
								w = (float)Display.displays[displayIndex].systemWidth;
								h = (float)Display.displays[displayIndex].systemHeight;
							}
							Vector2 pos = new Vector2(eventPosition.x / w, eventPosition.y / h);
							bool flag7 = pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f;
							if (flag7)
							{
								goto IL_0368;
							}
						}
						else
						{
							eventPosition = mousePosition;
						}
						bool flag8 = !camera.pixelRect.Contains(eventPosition);
						if (!flag8)
						{
							bool flag9 = camera.eventMask == 0;
							if (!flag9)
							{
								Ray screenProjectionRay = camera.ScreenPointToRay(eventPosition);
								float projectionDirection = screenProjectionRay.direction.z;
								float distanceToClipPlane = (Mathf.Approximately(0f, projectionDirection) ? float.PositiveInfinity : Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / projectionDirection));
								GameObject hit3D = CameraRaycastHelper.RaycastTry(camera, screenProjectionRay, distanceToClipPlane, camera.cullingMask & camera.eventMask);
								bool flag10 = hit3D != null;
								if (flag10)
								{
									SendMouseEvents.m_CurrentHit[1].target = hit3D;
									SendMouseEvents.m_CurrentHit[1].camera = camera;
								}
								else
								{
									bool flag11 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag11)
									{
										SendMouseEvents.m_CurrentHit[1].target = null;
										SendMouseEvents.m_CurrentHit[1].camera = null;
									}
								}
								GameObject hit2D = CameraRaycastHelper.RaycastTry2D(camera, screenProjectionRay, distanceToClipPlane, camera.cullingMask & camera.eventMask);
								bool flag12 = hit2D != null;
								if (flag12)
								{
									SendMouseEvents.m_CurrentHit[2].target = hit2D;
									SendMouseEvents.m_CurrentHit[2].camera = camera;
								}
								else
								{
									bool flag13 = camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color;
									if (flag13)
									{
										SendMouseEvents.m_CurrentHit[2].target = null;
										SendMouseEvents.m_CurrentHit[2].camera = null;
									}
								}
							}
						}
					}
					IL_0368:;
				}
			}
			for (int hitIndex2 = 0; hitIndex2 < SendMouseEvents.m_CurrentHit.Length; hitIndex2++)
			{
				SendMouseEvents.SendEvents(hitIndex2, SendMouseEvents.m_CurrentHit[hitIndex2]);
			}
			SendMouseEvents.s_MouseUsed = false;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027C0 File Offset: 0x000009C0
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseDownThisFrame = SendMouseEvents.s_MouseButtonPressedThisFrame;
			bool mousePressed = SendMouseEvents.s_MouseButtonIsPressed;
			bool flag = mouseDownThisFrame;
			if (flag)
			{
				bool flag2 = hit;
				if (flag2)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else
			{
				bool flag3 = !mousePressed;
				if (flag3)
				{
					bool flag4 = SendMouseEvents.m_MouseDownHit[i];
					if (flag4)
					{
						bool flag5 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]);
						if (flag5)
						{
							SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
						}
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
						SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
					}
				}
				else
				{
					bool flag6 = SendMouseEvents.m_MouseDownHit[i];
					if (flag6)
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
					}
				}
			}
			bool flag7 = SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]);
			if (flag7)
			{
				bool flag8 = hit;
				if (flag8)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				bool flag9 = SendMouseEvents.m_LastHit[i];
				if (flag9)
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				bool flag10 = hit;
				if (flag10)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x0400002F RID: 47
		private static bool s_MouseUsed = false;

		// Token: 0x04000030 RID: 48
		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000031 RID: 49
		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000032 RID: 50
		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[3];

		// Token: 0x04000033 RID: 51
		private static Camera[] m_Cameras;

		// Token: 0x04000034 RID: 52
		public static Func<KeyValuePair<int, Vector2>> s_GetMouseState;

		// Token: 0x04000035 RID: 53
		private static Vector2 s_MousePosition;

		// Token: 0x04000036 RID: 54
		private static bool s_MouseButtonPressedThisFrame;

		// Token: 0x04000037 RID: 55
		private static bool s_MouseButtonIsPressed;

		// Token: 0x0200000C RID: 12
		private struct HitInfo
		{
			// Token: 0x0600003D RID: 61 RVA: 0x0000297D File Offset: 0x00000B7D
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00002990 File Offset: 0x00000B90
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x000029C0 File Offset: 0x00000BC0
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x04000038 RID: 56
			public GameObject target;

			// Token: 0x04000039 RID: 57
			public Camera camera;
		}
	}
}

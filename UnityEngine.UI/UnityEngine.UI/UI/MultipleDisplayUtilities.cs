using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200005B RID: 91
	internal static class MultipleDisplayUtilities
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00010650 File Offset: 0x0000E850
		public static bool GetRelativeMousePositionForDrag(PointerEventData eventData, ref Vector2 position)
		{
			int pressDisplayIndex = eventData.pointerPressRaycast.displayIndex;
			Vector3 relativePosition = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if ((int)relativePosition.z != pressDisplayIndex)
			{
				return false;
			}
			position = ((pressDisplayIndex != 0) ? relativePosition : eventData.position);
			return true;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000106A0 File Offset: 0x0000E8A0
		internal static Vector3 GetRelativeMousePositionForRaycast(PointerEventData eventData)
		{
			Vector3 eventPosition = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (eventPosition == Vector3.zero)
			{
				eventPosition = eventData.position;
			}
			if (eventData.displayIndex > 0)
			{
				eventPosition.z = (float)eventData.displayIndex;
			}
			return eventPosition;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public static Vector3 RelativeMouseAtScaled(Vector2 position, int displayIndex)
		{
			if (Display.main.renderingWidth != Display.main.systemWidth || Display.main.renderingHeight != Display.main.systemHeight)
			{
				float systemAspectRatio = (float)Display.main.systemWidth / (float)Display.main.systemHeight;
				Vector2 sizePlusPadding = new Vector2((float)Display.main.renderingWidth, (float)Display.main.renderingHeight);
				Vector2 padding = Vector2.zero;
				if (Screen.fullScreen)
				{
					float aspectRatio = (float)Screen.width / (float)Screen.height;
					if ((float)Display.main.systemHeight * aspectRatio < (float)Display.main.systemWidth)
					{
						sizePlusPadding.x = (float)Display.main.renderingHeight * systemAspectRatio;
						padding.x = (sizePlusPadding.x - (float)Display.main.renderingWidth) * 0.5f;
					}
					else
					{
						sizePlusPadding.y = (float)Display.main.renderingWidth / systemAspectRatio;
						padding.y = (sizePlusPadding.y - (float)Display.main.renderingHeight) * 0.5f;
					}
				}
				Vector2 sizePlusPositivePadding = sizePlusPadding - padding;
				if (position.y < -padding.y || position.y > sizePlusPositivePadding.y || position.x < -padding.x || position.x > sizePlusPositivePadding.x)
				{
					Vector2 adjustedPosition = position;
					if (!Screen.fullScreen)
					{
						adjustedPosition.x -= (float)(Display.main.renderingWidth - Display.main.systemWidth) * 0.5f;
						adjustedPosition.y -= (float)(Display.main.renderingHeight - Display.main.systemHeight) * 0.5f;
					}
					else
					{
						adjustedPosition += padding;
						adjustedPosition.x *= (float)Display.main.systemWidth / sizePlusPadding.x;
						adjustedPosition.y *= (float)Display.main.systemHeight / sizePlusPadding.y;
					}
					Vector3 relativePos = new Vector3(adjustedPosition.x, adjustedPosition.y, (float)displayIndex);
					if (relativePos.z != 0f)
					{
						return relativePos;
					}
				}
				return new Vector3(position.x, position.y, 0f);
			}
			return new Vector3(position.x, position.y, (float)displayIndex);
		}
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CF RID: 1231
	public static class VisualElementExtensions
	{
		// Token: 0x060022CA RID: 8906 RVA: 0x0007FF60 File Offset: 0x0007E160
		public static void StretchToParentSize(this VisualElement elem)
		{
			bool flag = elem == null;
			if (flag)
			{
				throw new ArgumentNullException("elem");
			}
			IStyle styleAccess = elem.style;
			styleAccess.position = Position.Absolute;
			styleAccess.left = 0f;
			styleAccess.top = 0f;
			styleAccess.right = 0f;
			styleAccess.bottom = 0f;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007FFDC File Offset: 0x0007E1DC
		public static void AddManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = ele;
			}
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007FFFC File Offset: 0x0007E1FC
		public static void RemoveManipulator(this VisualElement ele, IManipulator manipulator)
		{
			bool flag = manipulator != null;
			if (flag)
			{
				manipulator.target = null;
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0008001C File Offset: 0x0007E21C
		public static Vector2 WorldToLocal(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformInverse, p);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00080050 File Offset: 0x0007E250
		public static Vector2 LocalToWorld(this VisualElement ele, Vector2 p)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.MultiplyMatrix44Point2(ele.worldTransformRef, p);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00080084 File Offset: 0x0007E284
		public static Rect WorldToLocal(this VisualElement ele, Rect r)
		{
			bool flag = ele == null;
			if (flag)
			{
				throw new ArgumentNullException("ele");
			}
			return VisualElement.CalculateConservativeRect(ele.worldTransformInverse, r);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000800B8 File Offset: 0x0007E2B8
		public static Vector2 ChangeCoordinatesTo(this VisualElement src, VisualElement dest, Vector2 point)
		{
			return dest.WorldToLocal(src.LocalToWorld(point));
		}
	}
}

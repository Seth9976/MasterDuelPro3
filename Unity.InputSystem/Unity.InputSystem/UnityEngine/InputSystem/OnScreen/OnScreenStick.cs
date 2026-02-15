using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000135 RID: 309
	[AddComponentMenu("Input/On-Screen Stick")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/OnScreen.html#on-screen-sticks")]
	public class OnScreenStick : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x000477BD File Offset: 0x000459BD
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.BeginInteraction(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000477E8 File Offset: 0x000459E8
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.MoveStick(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00047813 File Offset: 0x00045A13
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			this.EndInteraction();
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00047824 File Offset: 0x00045A24
		private void Start()
		{
			if (this.m_UseIsolatedInputActions)
			{
				this.m_RaycastResults = new List<RaycastResult>();
				this.m_PointerEventData = new PointerEventData(EventSystem.current);
				if (this.m_PointerDownAction == null || this.m_PointerDownAction.bindings.Count == 0)
				{
					if (this.m_PointerDownAction == null)
					{
						this.m_PointerDownAction = new InputAction();
					}
					this.m_PointerDownAction.AddBinding("<Mouse>/leftButton", null, null, null);
					this.m_PointerDownAction.AddBinding("<Pen>/tip", null, null, null);
					this.m_PointerDownAction.AddBinding("<Touchscreen>/touch*/press", null, null, null);
					this.m_PointerDownAction.AddBinding("<XRController>/trigger", null, null, null);
				}
				if (this.m_PointerMoveAction == null || this.m_PointerMoveAction.bindings.Count == 0)
				{
					if (this.m_PointerMoveAction == null)
					{
						this.m_PointerMoveAction = new InputAction();
					}
					this.m_PointerMoveAction.AddBinding("<Mouse>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Pen>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Touchscreen>/touch*/position", null, null, null);
				}
				this.m_PointerDownAction.started += this.OnPointerDown;
				this.m_PointerDownAction.canceled += this.OnPointerUp;
				this.m_PointerDownAction.Enable();
				this.m_PointerMoveAction.Enable();
			}
			if (!(base.transform is RectTransform))
			{
				return;
			}
			this.m_StartPos = ((RectTransform)base.transform).anchoredPosition;
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			this.m_PointerDownPos = this.m_StartPos;
			GameObject gameObject = new GameObject("DynamicOriginClickable", new Type[] { typeof(Image) });
			gameObject.transform.SetParent(base.transform);
			Image image = gameObject.GetComponent<Image>();
			image.color = new Color(1f, 1f, 1f, 0f);
			RectTransform rectTransform = (RectTransform)gameObject.transform;
			rectTransform.sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			rectTransform.localScale = new Vector3(1f, 1f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			image.sprite = SpriteUtilities.CreateCircleSprite(16, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
			image.alphaHitTestMinimumThreshold = 0.5f;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00047AA1 File Offset: 0x00045CA1
		private void OnDestroy()
		{
			if (this.m_UseIsolatedInputActions)
			{
				this.m_PointerDownAction.started -= this.OnPointerDown;
				this.m_PointerDownAction.canceled -= this.OnPointerUp;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00047ADC File Offset: 0x00045CDC
		private void BeginInteraction(Vector2 pointerPosition, Camera uiCamera)
		{
			RectTransform canvasRectTransform = UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				Debug.LogError(base.GetWarningMessage());
				return;
			}
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				this.MoveStick(pointerPosition, uiCamera);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
			{
				Vector2 pointerDown;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out pointerDown);
				this.m_PointerDownPos = (((RectTransform)base.transform).anchoredPosition = pointerDown);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00047B74 File Offset: 0x00045D74
		private void MoveStick(Vector2 pointerPosition, Camera uiCamera)
		{
			RectTransform canvasRectTransform = UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				Debug.LogError(base.GetWarningMessage());
				return;
			}
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out position);
			Vector2 delta = position - this.m_PointerDownPos;
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				delta = Vector2.ClampMagnitude(delta, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + delta;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				delta = position - this.m_StartPos;
				delta = Vector2.ClampMagnitude(delta, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + delta;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
				delta = Vector2.ClampMagnitude(delta, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_PointerDownPos + delta;
				break;
			}
			Vector2 newPos = new Vector2(delta.x / this.movementRange, delta.y / this.movementRange);
			base.SendValueToControl<Vector2>(newPos);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00047CA0 File Offset: 0x00045EA0
		private void EndInteraction()
		{
			((RectTransform)base.transform).anchoredPosition = (this.m_PointerDownPos = this.m_StartPos);
			base.SendValueToControl<Vector2>(Vector2.zero);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00047CDC File Offset: 0x00045EDC
		private void OnPointerDown(InputAction.CallbackContext ctx)
		{
			Vector2 screenPosition = Vector2.zero;
			InputControl control = ctx.control;
			Pointer pointer = ((control != null) ? control.device : null) as Pointer;
			if (pointer != null)
			{
				screenPosition = pointer.position.ReadValue();
			}
			this.m_PointerEventData.position = screenPosition;
			EventSystem.current.RaycastAll(this.m_PointerEventData, this.m_RaycastResults);
			if (this.m_RaycastResults.Count == 0)
			{
				return;
			}
			bool stickSelected = false;
			foreach (RaycastResult result in this.m_RaycastResults)
			{
				if (!(result.gameObject != base.gameObject))
				{
					stickSelected = true;
					break;
				}
			}
			if (!stickSelected)
			{
				return;
			}
			this.BeginInteraction(screenPosition, this.GetCameraFromCanvas());
			this.m_PointerMoveAction.performed += this.OnPointerMove;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00047DCC File Offset: 0x00045FCC
		private void OnPointerMove(InputAction.CallbackContext ctx)
		{
			Vector2 screenPosition = ((Pointer)ctx.control.device).position.ReadValue();
			this.MoveStick(screenPosition, this.GetCameraFromCanvas());
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00047E02 File Offset: 0x00046002
		private void OnPointerUp(InputAction.CallbackContext ctx)
		{
			this.EndInteraction();
			this.m_PointerMoveAction.performed -= this.OnPointerMove;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00047E24 File Offset: 0x00046024
		private Camera GetCameraFromCanvas()
		{
			Canvas canvas = base.GetComponentInParent<Canvas>();
			RenderMode? renderMode = ((canvas != null) ? new RenderMode?(canvas.renderMode) : null);
			RenderMode? renderMode2 = renderMode;
			RenderMode renderMode3 = RenderMode.ScreenSpaceOverlay;
			if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)))
			{
				renderMode2 = renderMode;
				renderMode3 = RenderMode.ScreenSpaceCamera;
				if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)) || !(((canvas != null) ? canvas.worldCamera : null) == null))
				{
					return ((canvas != null) ? canvas.worldCamera : null) ?? Camera.main;
				}
			}
			return null;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00047EB0 File Offset: 0x000460B0
		private void OnDrawGizmosSelected()
		{
			RectTransform parentRectTransform = base.transform.parent as RectTransform;
			if (parentRectTransform == null)
			{
				return;
			}
			Gizmos.matrix = parentRectTransform.localToWorldMatrix;
			Vector2 startPos = parentRectTransform.anchoredPosition;
			if (Application.isPlaying)
			{
				startPos = this.m_StartPos;
			}
			Gizmos.color = new Color32(84, 173, 219, byte.MaxValue);
			Vector2 center = startPos;
			if (Application.isPlaying && this.m_Behaviour == OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				center = this.m_PointerDownPos;
			}
			this.DrawGizmoCircle(center, this.m_MovementRange);
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			Gizmos.color = new Color32(158, 84, 219, byte.MaxValue);
			this.DrawGizmoCircle(startPos, this.m_DynamicOriginRange);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00047F7C File Offset: 0x0004617C
		private void DrawGizmoCircle(Vector2 center, float radius)
		{
			for (int i = 0; i < 32; i++)
			{
				float radians = (float)i / 32f * 3.1415927f * 2f;
				float nextRadian = (float)(i + 1) / 32f * 3.1415927f * 2f;
				Gizmos.DrawLine(new Vector3(center.x + Mathf.Cos(radians) * radius, center.y + Mathf.Sin(radians) * radius, 0f), new Vector3(center.x + Mathf.Cos(nextRadian) * radius, center.y + Mathf.Sin(nextRadian) * radius, 0f));
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00048020 File Offset: 0x00046220
		private void UpdateDynamicOriginClickableArea()
		{
			Transform dynamicOriginTransform = base.transform.Find("DynamicOriginClickable");
			if (dynamicOriginTransform)
			{
				((RectTransform)dynamicOriginTransform).sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0004806E File Offset: 0x0004626E
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00048076 File Offset: 0x00046276
		public float movementRange
		{
			get
			{
				return this.m_MovementRange;
			}
			set
			{
				this.m_MovementRange = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0004807F File Offset: 0x0004627F
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00048087 File Offset: 0x00046287
		public float dynamicOriginRange
		{
			get
			{
				return this.m_DynamicOriginRange;
			}
			set
			{
				if (this.m_DynamicOriginRange != value)
				{
					this.m_DynamicOriginRange = value;
					this.UpdateDynamicOriginClickableArea();
				}
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0004809F File Offset: 0x0004629F
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x000480A7 File Offset: 0x000462A7
		public bool useIsolatedInputActions
		{
			get
			{
				return this.m_UseIsolatedInputActions;
			}
			set
			{
				this.m_UseIsolatedInputActions = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000480B0 File Offset: 0x000462B0
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x000480B8 File Offset: 0x000462B8
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x000480C1 File Offset: 0x000462C1
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x000480C9 File Offset: 0x000462C9
		public OnScreenStick.Behaviour behaviour
		{
			get
			{
				return this.m_Behaviour;
			}
			set
			{
				this.m_Behaviour = value;
			}
		}

		// Token: 0x04000727 RID: 1831
		private const string kDynamicOriginClickable = "DynamicOriginClickable";

		// Token: 0x04000728 RID: 1832
		[FormerlySerializedAs("movementRange")]
		[SerializeField]
		[Min(0f)]
		private float m_MovementRange = 50f;

		// Token: 0x04000729 RID: 1833
		[SerializeField]
		[Tooltip("Defines the circular region where the onscreen control may have it's origin placed.")]
		[Min(0f)]
		private float m_DynamicOriginRange = 100f;

		// Token: 0x0400072A RID: 1834
		[InputControl(layout = "Vector2")]
		[SerializeField]
		private string m_ControlPath;

		// Token: 0x0400072B RID: 1835
		[SerializeField]
		[Tooltip("Choose how the onscreen stick will move relative to it's origin and the press position.\n\nRelativePositionWithStaticOrigin: The control's center of origin is fixed. The control will begin un-actuated at it's centered position and then move relative to the pointer or finger motion.\n\nExactPositionWithStaticOrigin: The control's center of origin is fixed. The stick will immediately jump to the exact position of the click or touch and begin tracking motion from there.\n\nExactPositionWithDynamicOrigin: The control's center of origin is determined by the initial press position. The stick will begin un-actuated at this center position and then track the current pointer or finger position.")]
		private OnScreenStick.Behaviour m_Behaviour;

		// Token: 0x0400072C RID: 1836
		[SerializeField]
		[Tooltip("Set this to true to prevent cancellation of pointer events due to device switching. Cancellation will appear as the stick jumping back and forth between the pointer position and the stick center.")]
		private bool m_UseIsolatedInputActions;

		// Token: 0x0400072D RID: 1837
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer down events on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerDownAction;

		// Token: 0x0400072E RID: 1838
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer movement on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerMoveAction;

		// Token: 0x0400072F RID: 1839
		private Vector3 m_StartPos;

		// Token: 0x04000730 RID: 1840
		private Vector2 m_PointerDownPos;

		// Token: 0x04000731 RID: 1841
		[NonSerialized]
		private List<RaycastResult> m_RaycastResults;

		// Token: 0x04000732 RID: 1842
		[NonSerialized]
		private PointerEventData m_PointerEventData;

		// Token: 0x02000136 RID: 310
		public enum Behaviour
		{
			// Token: 0x04000734 RID: 1844
			RelativePositionWithStaticOrigin,
			// Token: 0x04000735 RID: 1845
			ExactPositionWithStaticOrigin,
			// Token: 0x04000736 RID: 1846
			ExactPositionWithDynamicOrigin
		}
	}
}

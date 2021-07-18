using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour
{

	public static CameraCtrl Instance;

	[SerializeField]
	private Transform CameraUpAndDownCtrl;

	[SerializeField]
	private Transform CameraZooomCtrl;

	[SerializeField]
	private Transform CameraRotateCtrl;

	/// <summary>
	/// 摄像机横向旋转参数
	/// </summary>
	private float CameraRotate_SPEED = 100f;

	/// <summary>
	/// 摄像机纵向旋转参数
	/// </summary>
	private float CameraUpAndDown_MIN = -50f;
	private float CameraUpAndDown_MAX = 28f;
	private float CameraUpAndDown_SPEED = 40f;

	/// <summary>
	/// 摄像机缩放参数
	/// </summary>
	private float CameraZooom_MIN = -4.5f;
	private float CameraZooom_MAX = -2.4f;
	private float CameraZooom_SPEED = 10f;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{

	}

	void Update()
	{

	}

	public void AutoLook(Vector3 pos)
	{
		CameraRotateCtrl.LookAt(pos);
	}

	public void SetCameraRotate(int type)
	{
		CameraRotateCtrl.transform.Rotate(0, Time.deltaTime * CameraRotate_SPEED * (type == 1 ? 1 : -1), 0);
	}

	public void SetCameraUpAndDown(int type)
	{
		CameraUpAndDownCtrl.transform.Rotate(Time.deltaTime * CameraUpAndDown_SPEED * (type == 1 ? 1 : -1), 0, 0);
		float uler_x = CameraUpAndDownCtrl.localEulerAngles.x;

		CameraUpAndDownCtrl.transform.localEulerAngles = new Vector3(Mathf.Clamp(uler_x < CameraUpAndDown_MAX ? uler_x : uler_x - 360, CameraUpAndDown_MIN, CameraUpAndDown_MAX), 0, 0);
	}

	public void SetCameraZoom(int type)
	{
		CameraZooomCtrl.Translate(Vector3.forward * Time.deltaTime * CameraZooom_SPEED * (type == 1 ? 1 : -1));
		CameraZooomCtrl.localPosition = new Vector3(0, 0, Mathf.Clamp(CameraZooomCtrl.localPosition.z, CameraZooom_MIN, CameraZooom_MAX));
	}
}

using UnityEngine;
using System.Collections;

public class XKGameVersionCtrl : MonoBehaviour {
	UILabel VersionLB;
	/**
	 * V1.2_20170515战火雄心-坦克大战:
	 * 1.处理第四关玩家合体时,如果游戏进入倒计时,画面会停止运动的bug.
	 * 
	 * #########################################################
	 * Version: V1.2_20170725战火雄心-坦克直升机联合作战:
	 * 1.在GameOver和“任务完成”出现时关闭所有气囊.
	 * 2.直升机气囊控制.
	 *   目前动作不丰富,要以飞机飞行姿态相配合.
	 * 3.气囊7号与8号信号另有用途,不再与5号,6号气囊相关;
	 *   7号信号用于控制前面灯,8号信号用于控制尾灯(坦克,直升机IO板信号管脚均一致）
	 * 4.因调试阶段,频繁开关机,发觉已校正的菜单信息丢失.
	 */
	public static string GameVersion = "Version: V1.2_20170725";
	// Use this for initialization
	void Start()
	{
		VersionLB = GetComponent<UILabel>();
		VersionLB.text = GameVersion;
	}
}
using UnityEngine;
using System.Collections;

public class XKGameVersionCtrl : MonoBehaviour {
	UILabel VersionLB;
	/**
	 * V1.2_20170515战火雄心-坦克大战:
	 * 1.处理第四关玩家合体时,如果游戏进入倒计时,画面会停止运动的bug.
	 * 
	 * #########################################################
	 * Version: V1.1_20170712战火雄心-坦克直升机联合作战:
	 * 1.添加机台气囊控制逻辑.
	 * 2.修改游戏难度.
	 * 3.添加机台硬件测试工具.
	 */
	public static string GameVersion = "Version: V1.1_20170712";
	// Use this for initialization
	void Start()
	{
		VersionLB = GetComponent<UILabel>();
		VersionLB.text = GameVersion;
	}
}
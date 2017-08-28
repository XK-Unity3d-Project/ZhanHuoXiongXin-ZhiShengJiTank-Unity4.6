using UnityEngine;
using System.Collections;

public class XKGameVersionCtrl : MonoBehaviour {
	UILabel VersionLB;
	/**
	 * V1.2_20170515战火雄心-坦克大战:
	 * 1.处理第四关玩家合体时,如果游戏进入倒计时,画面会停止运动的bug.
	 * 
	 * #########################################################
	 * Version: V1.3_20170808战火雄心-坦克直升机联合作战:
	 * 1.将坦克和直升机对调(坦克游戏为3屏游戏,控制6个气囊的动作,直升机为2个座椅气囊).
	 * 2.将游戏里的npc放大.
	 * 
	 * #########################################################
	 * Version: V1.4.1_20170828战火雄心-直升机大战:
	 * 1.处理直升机游戏显示为1屏的bug.
	 */
	public static string GameVersion = "Version: V1.4.1_20170828";
	// Use this for initialization
	void Start()
	{
		VersionLB = GetComponent<UILabel>();
		VersionLB.text = GameVersion;
	}
}
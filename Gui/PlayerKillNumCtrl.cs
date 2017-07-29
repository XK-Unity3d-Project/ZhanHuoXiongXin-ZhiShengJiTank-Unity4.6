using UnityEngine;
using System.Collections;

public class PlayerKillNumCtrl : MonoBehaviour {
	public PlayerEnum PlayerSt = PlayerEnum.Null;
	public UISprite[] KillNpcNum; //人型npc.
	public UISprite[] KillTKNum; //坦克.
	public UISprite[] KillFJNum; //飞机.
	public UISprite[] KillCBNum; //船舶.
	GameObject KillNpcObj;
	GameObject KillTKObj;
	GameObject KillFJObj;
	GameObject KillCBObj;
	bool IsShowKillNum;
	float TimeDelayVal = 1f;
	static bool IsQiangZhiShowKillNum;
	/**
	 * 保护时间,防止积分界面卡死导致游戏无法回到循环动画.
	 */
	float TimeLastVal = 0f;
	static bool IsEndPlayerKillNumCartoon;
	static PlayerKillNumCtrl InstanceOne;
	public static PlayerKillNumCtrl GetInstanceOne()
	{
		return InstanceOne;
	}

	static PlayerKillNumCtrl InstanceTwo;
	public static PlayerKillNumCtrl GetInstanceTwo()
	{
		return InstanceTwo;
	}

	// Use this for initialization
	void Start()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			InstanceOne = this;
			break;

		case PlayerEnum.PlayerTwo:
			InstanceTwo = this;
			break;
		}
		IsEndPlayerKillNumCartoon = false;
		IsQiangZhiShowKillNum = false;
		KillNpcObj = KillNpcNum[0].transform.parent.gameObject;
		KillTKObj = KillTKNum[0].transform.parent.gameObject;
		KillFJObj = KillFJNum[0].transform.parent.gameObject;
		KillCBObj = KillCBNum[0].transform.parent.gameObject;
		KillNpcObj.SetActive(false);
		KillTKObj.SetActive(false);
		KillFJObj.SetActive(false);
		KillCBObj.SetActive(false);
	}

	void Update()
	{
		if (!IsShowKillNum) {
			return;
		}

		if (IsEndPlayerKillNumCartoon) {
			return;
		}

		if (IsQiangZhiShowKillNum && Time.time - TimeLastVal > 2f) {
			EndPlayerKillNumCartoon();
			return;
		}

		if (Time.time - TimeLastVal >= 4.5f * TimeDelayVal && !IsQiangZhiShowKillNum) {
			IsQiangZhiShowKillNum = true;
			TimeLastVal = Time.time;
			InstanceOne.StopKillNpcNumCartoon(1);
			InstanceOne.StopKillTKNumCartoon(1);
			InstanceOne.StopKillFJNumCartoon(1);
			InstanceOne.StopKillCBNumCartoon(1);
			InstanceTwo.StopKillNpcNumCartoon(1);
			InstanceTwo.StopKillTKNumCartoon(1);
			InstanceTwo.StopKillFJNumCartoon(1);
			InstanceTwo.StopKillCBNumCartoon(1);
		}
	}

	public void ShowPlayerKillNum()
	{
		if (IsShowKillNum) {
			return;
		}
		IsShowKillNum = true;
		TimeLastVal = Time.time;
		ShowPlayerKillNpcNum();
	}

	void ShowPlayerKillNpcNum()
	{
		if (KillNpcObj.activeSelf) {
			return;
		}
		KillNpcObj.SetActive(true);
		XKGlobalData.GetInstance().PlayAudioJiFenGunDong();
		Invoke("StopKillNpcNum", TimeDelayVal);
	}

	void StopKillNpcNum()
	{
		StopKillNpcNumCartoon();
	}

	void StopKillNpcNumCartoon(byte key = 0)
	{
		int max = KillNpcNum.Length;
		UISpriteAnimation AniCom = null;

		int numVal = 0;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			numVal = XkGameCtrl.ShiBingNumPOne;
			break;

		case PlayerEnum.PlayerTwo:
			numVal = XkGameCtrl.ShiBingNumPTwo;
			break;
		}

		//bool isShowZero = false;
		int valTmp = 0;
		int powVal = 0;
		for (int i = 0; i < max; i++) {
			AniCom = KillNpcNum[i].GetComponent<UISpriteAnimation>();
			AniCom.enabled = false;

			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			/*if (!isShowZero) {
			    if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						KillNpcNum[i].enabled = false;
						continue;
					}
				}
			}*/
			KillNpcNum[i].spriteName = "KillNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
		if (key == 0) {
			ShowPlayerKillTKNum();
		}
		else {
			KillNpcObj.SetActive(true);
		}
	}

	void ShowPlayerKillTKNum()
	{
		if (KillTKObj.activeSelf) {
			return;
		}
		KillTKObj.SetActive(true);
		Invoke("StopKillTKNum", TimeDelayVal);
	}
	
	void StopKillTKNum()
	{
		StopKillTKNumCartoon();
	}

	void StopKillTKNumCartoon(byte key = 0)
	{
		int max = KillTKNum.Length;
		UISpriteAnimation AniCom = null;
		
		int numVal = 0;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			numVal = XkGameCtrl.CheLiangNumPOne;
			break;
			
		case PlayerEnum.PlayerTwo:
			numVal = XkGameCtrl.CheLiangNumPTwo;
			break;
		}
		
		//bool isShowZero = false;
		int valTmp = 0;
		int powVal = 0;
		for (int i = 0; i < max; i++) {
			AniCom = KillTKNum[i].GetComponent<UISpriteAnimation>();
			AniCom.enabled = false;
			
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			/*if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						KillTKNum[i].enabled = false;
						continue;
					}
				}
			}*/
			KillTKNum[i].spriteName = "KillNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
		if (key == 0) {
			ShowPlayerKillFJNum();
		}
		else {
			KillTKObj.SetActive(true);
		}
	}

	void ShowPlayerKillFJNum()
	{
		if (KillFJObj.activeSelf) {
			return;
		}
		KillFJObj.SetActive(true);
		Invoke("StopKillFJNum", TimeDelayVal);
	}
	
	void StopKillFJNum()
	{
		StopKillFJNumCartoon();
	}

	void StopKillFJNumCartoon(byte key = 0)
	{
		int max = KillFJNum.Length;
		UISpriteAnimation AniCom = null;
		
		int numVal = 0;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			numVal = XkGameCtrl.FeiJiNumPOne;
			break;
			
		case PlayerEnum.PlayerTwo:
			numVal = XkGameCtrl.FeiJiNumPTwo;
			break;
		}
		
		//bool isShowZero = false;
		int valTmp = 0;
		int powVal = 0;
		for (int i = 0; i < max; i++) {
			AniCom = KillFJNum[i].GetComponent<UISpriteAnimation>();
			AniCom.enabled = false;
			
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			/*if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						KillFJNum[i].enabled = false;
						continue;
					}
				}
			}*/
			KillFJNum[i].spriteName = "KillNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
		if (key == 0) {
			ShowPlayerKillCBNum();
		}
		else {
			KillFJObj.SetActive(true);
		}
	}

	void ShowPlayerKillCBNum()
	{
		if (KillCBObj.activeSelf) {
			return;
		}
		KillCBObj.SetActive(true);
		Invoke("StopKillCBNum", TimeDelayVal);
	}
	
	void StopKillCBNum()
	{
		StopKillCBNumCartoon();
	}

	void StopKillCBNumCartoon(byte key = 0)
	{
		int max = KillCBNum.Length;
		UISpriteAnimation AniCom = null;
		
		int numVal = 0;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			numVal = XkGameCtrl.ChuanBoNumPOne;
			break;
			
		case PlayerEnum.PlayerTwo:
			numVal = XkGameCtrl.ChuanBoNumPTwo;
			break;
		}
		
		//bool isShowZero = false;
		int valTmp = 0;
		int powVal = 0;
		for (int i = 0; i < max; i++) {
			AniCom = KillCBNum[i].GetComponent<UISpriteAnimation>();
			AniCom.enabled = false;
			
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			/*if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						KillCBNum[i].enabled = false;
						continue;
					}
				}
			}*/
			KillCBNum[i].spriteName = "KillNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
		if (key == 0) {
			EndPlayerKillNumCartoon();
		}
		else {
			KillCBObj.SetActive(true);
		}
	}

	void EndPlayerKillNumCartoon()
	{
		if (IsEndPlayerKillNumCartoon) {
			return;
		}
		IsEndPlayerKillNumCartoon = true;
		Debug.Log("EndPlayerKillNumCartoon...");
		
		XKGlobalData.GetInstance().StopAudioJiFenGunDong();
		if (XkGameCtrl.IsPlayGamePOne) {
			XunZhangZPCtrl.GetInstanceOne().ShowPlayerXunZhang();
		}

		if (XkGameCtrl.IsPlayGamePTwo) {
			XunZhangZPCtrl.GetInstanceTwo().ShowPlayerXunZhang();
		}
	}
}
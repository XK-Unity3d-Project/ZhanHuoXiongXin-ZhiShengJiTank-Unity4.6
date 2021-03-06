﻿#define QI_NANG_DAN_BIAN
//QI_NANG_DAN_BIAN 该定义控制飞机气囊逻辑为只打开四边型的某一边.
using UnityEngine;
using System.Collections;

public class XKPlayerDongGanCtrl : MonoBehaviour {
	public static bool IsQiNangDanBian;
	public PlayerTypeEnum PlayerSt = PlayerTypeEnum.TanKe;
	/**
QiNangStateTK[0] -> 前气囊
QiNangStateTK[1] -> 后气囊
QiNangStateTK[2] -> 左气囊
QiNangStateTK[3] -> 右气囊
	 */
	public static int[] QiNangStateTK = {0, 0, 0, 0};
	/**
QiNangStateFJ[0] -> 前气囊
QiNangStateFJ[1] -> 后气囊
QiNangStateFJ[2] -> 左气囊
QiNangStateFJ[3] -> 右气囊
	 */
	public static int[] QiNangStateFJ = {0, 0, 0, 0};
	Vector3 EulerAngle;
	bool IsHandlePlayerZYQN;
	// Use this for initialization
	void Start()
	{
		//GameTypeCtrl.AppTypeStatic = AppGameType.DanJiFeiJi; //test.
		QiNangStateTK = new int[]{0, 0, 0, 0};
		QiNangStateFJ = new int[]{0, 0, 0, 0};
		//避免联机状态时气囊控制错乱的问题.
		if (XkGameCtrl.GameJiTaiSt == GameJiTaiType.FeiJiJiTai && PlayerSt != PlayerTypeEnum.FeiJi) {
				enabled = false;
		}

		if (XkGameCtrl.GameJiTaiSt == GameJiTaiType.TanKeJiTai && PlayerSt != PlayerTypeEnum.TanKe) {
				enabled = false;
		}

		#if QI_NANG_DAN_BIAN
		IsQiNangDanBian = true;
		#endif
	}

	/**
	 * KeyZYQiNangState = 0 -> 左右气囊关闭.
	 * KeyZYQiNangState = 1 -> 左气囊关闭.
	 * KeyZYQiNangState = 2 -> 右气囊关闭.
	 */
	int KeyZYQiNangState;
	/**
	 * KeyQHQiNangState = 0 -> 前后气囊关闭.
	 * KeyQHQiNangState = 1 -> 前气囊关闭.
	 * KeyQHQiNangState = 2 -> 后气囊关闭.
	 */
	int KeyQHQiNangState;
	// Update is called once per frame
	void Update()
	{
		if (pcvr.DongGanState == 0) {
			return;
		}
		
		if (DaoJiShiCtrl.GetInstance().GetIsPlayDaoJishi()
		    || (!XkGameCtrl.IsActivePlayerOne && !XkGameCtrl.IsActivePlayerTwo)) {
			pcvr.CloseQiNangQian();
			pcvr.CloseQiNangHou();
			pcvr.CloseQiNangZuo();
			pcvr.CloseQiNangYou();
			return;
		}

		//PlayerSt = PlayerTypeEnum.TanKe; //test.
		float eulerAngleX = 0f;
		float eulerAngleZ = 0f;
		float offsetAngle = 0f;
		bool isUpdateQiNang = false;
		switch (PlayerSt) {
		case PlayerTypeEnum.TanKe:
			EulerAngle = transform.eulerAngles;
			if (EulerAngle.x > 180f) {
				EulerAngle.x -= 360f;
			}
			
			if (EulerAngle.z > 180f) {
				EulerAngle.z -= 360f;
			}
			
			eulerAngleX = EulerAngle.x;
			eulerAngleZ = EulerAngle.z;
			offsetAngle = 0f;
			#if QI_NANG_DAN_BIAN
			if (Mathf.Abs(eulerAngleX) >= Mathf.Abs(eulerAngleZ)) {
				eulerAngleZ = 0;
			}
			else {
				eulerAngleX = 0;
			}
			#endif

			if (Mathf.Abs(eulerAngleX) <= offsetAngle) {
				//前后气囊放气.
				if (KeyQHQiNangState != 0) {
					KeyQHQiNangState = 0;
					isUpdateQiNang = true;
				}
			}
			else if  (eulerAngleX < 0f) {
				//前气囊充气,后气囊放气.
				if (KeyQHQiNangState != 2) {
						KeyQHQiNangState = 2;
						isUpdateQiNang = true;
				}
			}
			else if (eulerAngleX > 0f) {
				//后气囊充气,前气囊放气.
				if (KeyQHQiNangState != 1) {
						KeyQHQiNangState = 1;
						isUpdateQiNang = true;
				}
			}
			
			if (Mathf.Abs(eulerAngleZ) <= offsetAngle) {
				//左右气囊放气.
				if (KeyZYQiNangState != 0) {
					KeyZYQiNangState = 0;
					isUpdateQiNang = true;
				}
			}
			else if (eulerAngleZ < 0f) {
				//左气囊充气,右气囊放气.
				if (KeyZYQiNangState != 2) {
						KeyZYQiNangState = 2;
						isUpdateQiNang = true;
				}
			}
			else if  (eulerAngleZ > 0f) {
				//右气囊充气,左气囊放气.
				if (KeyZYQiNangState != 1) {
						KeyZYQiNangState = 1;
						isUpdateQiNang = true;
				}
			}
			break;

		case PlayerTypeEnum.FeiJi:
			EulerAngle = transform.eulerAngles;
			if (EulerAngle.x > 180f) {
				EulerAngle.x -= 360f;
			}
			
			if (EulerAngle.z > 180f) {
				EulerAngle.z -= 360f;
			}
			eulerAngleX = EulerAngle.x;
			eulerAngleZ = EulerAngle.z;
			offsetAngle = 2f;
			
			#if QI_NANG_DAN_BIAN
			if (Mathf.Abs(eulerAngleX) >= Mathf.Abs(eulerAngleZ)) {
				eulerAngleZ = 0;
			}
			else {
				eulerAngleX = 0;
			}
			#endif

			if (Mathf.Abs(eulerAngleX) <= offsetAngle) {
				//前后气囊放气.
				if (KeyQHQiNangState != 0) {
						KeyQHQiNangState = 0;
						isUpdateQiNang = true;
				}
			}
			else if (eulerAngleX > 0f) {
				//前气囊放气,后气囊充气.
				if (KeyQHQiNangState != 1) {
						KeyQHQiNangState = 1;
						isUpdateQiNang = true;
				}
			}
			else if  (eulerAngleX < 0f) {
				//后气囊放气,前气囊充气.
				if (KeyQHQiNangState != 2) {
						KeyQHQiNangState = 2;
						isUpdateQiNang = true;
				}
			}
			
			if (Mathf.Abs(eulerAngleZ) <= offsetAngle) {
				//左右气囊放气.
				if (KeyZYQiNangState != 0) {
						KeyZYQiNangState = 0;
						isUpdateQiNang = true;
				}
			}
			else if  (eulerAngleZ > 0f) {
				//左气囊放气,右气囊充气.
				if (KeyZYQiNangState != 1) {
						KeyZYQiNangState = 1;
						isUpdateQiNang = true;
				}
			}
			else if (eulerAngleZ < 0f) {
				//右气囊放气,左气囊充气.
				if (KeyZYQiNangState != 2) {
						KeyZYQiNangState = 2;
						isUpdateQiNang = true;
				}
			}
			break;
		}

		if (isUpdateQiNang) {
			UpdateJiTaiDongGan();
		}
	}

		void UpdateJiTaiDongGan()
		{
				switch (PlayerSt) {
				case PlayerTypeEnum.TanKe:
						switch (KeyQHQiNangState) {
						case 0:
								QiNangStateTK[0] = 0;
								QiNangStateTK[1] = 0;
								if (KeyZYQiNangState == 0) {
									pcvr.CloseQiNangQian();
									pcvr.CloseQiNangHou();
								}
//								QiNangStateTK[0] = 0;
//								QiNangStateTK[1] = 0;
//								if (KeyZYQiNangState == 0) {
//										if (!pcvr.IsUse2QNLinkTank) {
//											pcvr.CloseQiNangQian();
//											pcvr.CloseQiNangHou();
//										}
//								}
								break;
						case 1:
								QiNangStateTK[0] = 0;
								QiNangStateTK[1] = 1;
								if (IsQiNangDanBian) {
									pcvr.OpenQiNangZuo();
									pcvr.CloseQiNangZuo();
									pcvr.OpenQiNangYou();
									pcvr.CloseQiNangYou();
									pcvr.OpenQiNangQian();
									pcvr.CloseQiNangHou(0);
								}
								pcvr.OpenQiNangHou();
								#if QI_NANG_DAN_BIAN
								pcvr.CloseQiNangQian(0);
								#else
								pcvr.CloseQiNangQian(KeyZYQiNangState);
								#endif

//								QiNangStateTK[0] = 0;
//								QiNangStateTK[1] = 1;
//								if (!pcvr.IsUse2QNLinkTank) {
//									pcvr.OpenQiNangHou();
//									pcvr.CloseQiNangQian(KeyZYQiNangState);
//								}
								break;
						case 2:
								QiNangStateTK[0] = 1;
								QiNangStateTK[1] = 0;
								if (IsQiNangDanBian) {
									pcvr.OpenQiNangZuo();
									pcvr.CloseQiNangZuo();
									pcvr.OpenQiNangYou();
									pcvr.CloseQiNangYou();
									pcvr.OpenQiNangHou();
									pcvr.CloseQiNangQian(0);
								}
								pcvr.OpenQiNangQian();
								#if QI_NANG_DAN_BIAN
								pcvr.CloseQiNangHou(0);
								#else
								pcvr.CloseQiNangHou(KeyZYQiNangState);
								#endif

//								QiNangStateTK[0] = 1;
//								QiNangStateTK[1] = 0;
//								if (!pcvr.IsUse2QNLinkTank) {
//										pcvr.OpenQiNangQian();
//										pcvr.CloseQiNangHou(KeyZYQiNangState);
//								}
//								else {
//										if (!IsHandlePlayerZYQN) {
//												IsHandlePlayerZYQN = true;
//												StartCoroutine(HandlePlayerZuoYiQiNang());
//										}
//								}
								break;
						}

						switch (KeyZYQiNangState) {
						case 0:
								QiNangStateTK[2] = 0;
								QiNangStateTK[3] = 0;
								if (KeyQHQiNangState == 0) {
										pcvr.CloseQiNangZuo();
										pcvr.CloseQiNangYou();
								}
//								QiNangStateTK[2] = 0;
//								QiNangStateTK[3] = 0;
//								if (KeyQHQiNangState == 0) {
//										if (!pcvr.IsUse2QNLinkTank) {
//											pcvr.CloseQiNangZuo();
//											pcvr.CloseQiNangYou();
//										}
//								}
								break;
						case 1:
								QiNangStateTK[2] = 0;
								QiNangStateTK[3] = 1;
								if (IsQiNangDanBian) {
									pcvr.OpenQiNangZuo();
									pcvr.CloseQiNangYou(0);
								}
								pcvr.OpenQiNangYou();
								#if QI_NANG_DAN_BIAN
								pcvr.CloseQiNangZuo(0);
								#else
								pcvr.CloseQiNangZuo(KeyQHQiNangState);
								#endif
//								QiNangStateTK[2] = 0;
//								QiNangStateTK[3] = 1;
//								if (!pcvr.IsUse2QNLinkTank) {
//									pcvr.OpenQiNangYou();
//									pcvr.CloseQiNangZuo(KeyQHQiNangState);
//								}
								break;
						case 2:
								QiNangStateTK[2] = 1;
								QiNangStateTK[3] = 0;
								if (IsQiNangDanBian) {
									pcvr.OpenQiNangYou();
									pcvr.CloseQiNangZuo(0);
								}
								pcvr.OpenQiNangZuo();
								#if QI_NANG_DAN_BIAN
								pcvr.CloseQiNangYou(0);
								#else
								pcvr.CloseQiNangYou(KeyQHQiNangState);
								#endif
//								QiNangStateTK[2] = 1;
//								QiNangStateTK[3] = 0;
//								if (!pcvr.IsUse2QNLinkTank) {
//									pcvr.OpenQiNangZuo();
//									pcvr.CloseQiNangYou(KeyQHQiNangState);
//								}
								break;
						}
						break;

				case PlayerTypeEnum.FeiJi:
						{
								//Debug.Log("KeyQHQiNangState "+KeyQHQiNangState+", KeyZYQiNangState "+KeyZYQiNangState);
								switch (KeyQHQiNangState) {
								case 0:
										QiNangStateFJ[0] = 0;
										QiNangStateFJ[1] = 0;
										if (KeyZYQiNangState == 0) {
											pcvr.CloseQiNangQian();
											pcvr.CloseQiNangHou();
										}
										break;
								case 1:
										QiNangStateFJ[0] = 0;
										QiNangStateFJ[1] = 1;
										if (IsQiNangDanBian) {
											pcvr.OpenQiNangQian();
											pcvr.CloseQiNangHou(0);
										}
										pcvr.OpenQiNangHou();
										#if QI_NANG_DAN_BIAN
										pcvr.CloseQiNangQian(0);
										#else
										pcvr.CloseQiNangQian(KeyZYQiNangState);
										#endif
										break;
								case 2:
										QiNangStateFJ[0] = 1;
										QiNangStateFJ[1] = 0;
										if (IsQiNangDanBian) {
											pcvr.OpenQiNangHou();
											pcvr.CloseQiNangQian(0);
										}
										pcvr.OpenQiNangQian();
										#if QI_NANG_DAN_BIAN
										pcvr.CloseQiNangHou(0);
										#else
										pcvr.CloseQiNangHou(KeyZYQiNangState);
										#endif
										break;
								}

								switch (KeyZYQiNangState) {
								case 0:
										QiNangStateFJ[2] = 0;
										QiNangStateFJ[3] = 0;
										if (KeyQHQiNangState == 0) {
												pcvr.CloseQiNangZuo();
												pcvr.CloseQiNangYou();
										}
										break;
								case 1:
										QiNangStateFJ[2] = 0;
										QiNangStateFJ[3] = 1;
										if (IsQiNangDanBian) {
											pcvr.OpenQiNangZuo();
											pcvr.CloseQiNangYou(0);
										}
										pcvr.OpenQiNangYou();
										#if QI_NANG_DAN_BIAN
										pcvr.CloseQiNangZuo(0);
										#else
										pcvr.CloseQiNangZuo(KeyQHQiNangState);
										#endif
										break;
								case 2:
										QiNangStateFJ[2] = 1;
										QiNangStateFJ[3] = 0;
										if (IsQiNangDanBian) {
											pcvr.OpenQiNangYou();
											pcvr.CloseQiNangZuo(0);
										}
										pcvr.OpenQiNangZuo();
										#if QI_NANG_DAN_BIAN
										pcvr.CloseQiNangYou(0);
										#else
										pcvr.CloseQiNangYou(KeyQHQiNangState);
										#endif
										break;
								}
						}
						break;
				}
		}

		IEnumerator HandlePlayerZuoYiQiNang()
		{
				pcvr.OpenQiNangQian();
				yield return new WaitForSeconds(2f);
				pcvr.CloseQiNangQian();
				yield return new WaitForSeconds(1f);
				IsHandlePlayerZYQN = false;
		}
}
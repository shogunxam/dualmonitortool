#pragma once

#include "SwapButtonsDll.h"
#include "ThemeDwm.h"

class CButtonList;

struct LayoutMetrics
{
	int m_nButtonWidth;
	int m_nButtonHeight;
	int m_nLeftBorder;
	int m_nRightBorder;
	int m_nTopBorder;
	int m_nBottomBorder;
	int m_nSpacing;
};

class LOCALMODE_API CLayoutManager
{
public:
	CLayoutManager(void);
	~CLayoutManager(void);


	void ReInit(HWND hWndFrame);

	void PrepareFloatBar(HWND hWndFloatBar);

	RECT GetBarRect(HWND hWndFrame, const CButtonList& buttonList);
	SIZE GetBarSize(const CButtonList& buttonList);

	void PaintBar(HWND hWndFloatBar, const CButtonList& buttonList);
	//int GetButtonWidth();
	int HitToIndex(int x, int y, const CButtonList& buttonList);


private:
	SIZE CalcBarOffsets(HWND hWndFrame);
	static bool IsPreVista();

private:
	CThemeDwm m_ThemeDwm;
	CThemeDwm* m_pCurTheme;
	struct LayoutMetrics m_LayoutMetrics;
};


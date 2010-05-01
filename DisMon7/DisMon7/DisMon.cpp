// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2010  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#include "stdafx.h"
#include <windows.h>
#include "DisMon.h"

// private
DisMon::DisMon(void)
{
	savedPathArraySize = 0;
	savedModeArraySize = 0;
	pSavedPathInfo = NULL;
	pSavedModeInfo = NULL;
	monitorsDisabled = false;
}

// private
DisMon::DisMon(DisMon& rhs)
{
}

// private
DisMon::~DisMon(void)
{
	FreeSavedInfo();
}

//static public
DisMon& DisMon::Instance()
{
	static DisMon instance;
	return instance;
}

//public 
bool DisMon::DisableAllSecondary()
{
	bool ret = true;

	UINT32 pathArraySize;
	UINT32 modeArraySize;
	GetDisplayConfigBufferSizes(QDC_ALL_PATHS, &pathArraySize, &modeArraySize);
	DISPLAYCONFIG_PATH_INFO* pPathInfo = new DISPLAYCONFIG_PATH_INFO[pathArraySize];
	DISPLAYCONFIG_MODE_INFO* pModeInfo = new DISPLAYCONFIG_MODE_INFO[modeArraySize];
	
	LONG err = QueryDisplayConfig(QDC_ALL_PATHS, 
		&pathArraySize, pPathInfo,
		&modeArraySize, pModeInfo,
		NULL);
	if (err == ERROR_SUCCESS)
	{
		// release any old copy of the configuration
		FreeSavedInfo();
		// save a copy so we can restore the original configuration
		savedPathArraySize = pathArraySize;
		savedModeArraySize = modeArraySize;
		pSavedPathInfo = new DISPLAYCONFIG_PATH_INFO[savedPathArraySize];
		pSavedModeInfo = new DISPLAYCONFIG_MODE_INFO[savedModeArraySize];
		UINT32 idx;
		// may be simpler to use memcpy?
		for (idx = 0 ; idx < pathArraySize; idx++)
		{
			pSavedPathInfo[idx] = pPathInfo[idx];
		}
		for (idx = 0 ; idx < modeArraySize; idx++)
		{
			pSavedModeInfo[idx] = pModeInfo[idx];
		}
		monitorsDisabled = false;

		// deactivate all paths where the source co-ords are not (0,0)
		for (idx = 0; idx < pathArraySize; idx++)
		{
			if (IsSecondaryModeIdx(pPathInfo[idx].sourceInfo.modeInfoIdx))
			{
				if (pPathInfo[idx].flags & DISPLAYCONFIG_PATH_ACTIVE)
				{
					pPathInfo[idx].flags &= ~DISPLAYCONFIG_PATH_ACTIVE;
					pPathInfo[idx].targetInfo.modeInfoIdx = DISPLAYCONFIG_PATH_MODE_IDX_INVALID;
					monitorsDisabled = true;
				}
			}
		}

		if (monitorsDisabled)
		{
			// we have deactivated 1 or more paths
			err = SetDisplayConfig(pathArraySize, pPathInfo,
				modeArraySize, pModeInfo,
				SDC_APPLY | SDC_ALLOW_CHANGES | SDC_USE_SUPPLIED_DISPLAY_CONFIG);
			if (err != ERROR_SUCCESS)
			{
				ThrowError("SetDisplayConfig Error", err);
			}
		}
	}
	else
	{
		ThrowError("QueryDisplayConfig Error", err);
	}

	return ret;
}

bool DisMon::IsSecondaryModeIdx(UINT32 idx)
{
	bool ret = false;

	if (idx != DISPLAYCONFIG_PATH_MODE_IDX_INVALID)
	{
		if (idx < savedModeArraySize)
		{
			if (pSavedModeInfo[idx].infoType == DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
			{
				if (pSavedModeInfo[idx].sourceMode.position.x != 0 || pSavedModeInfo[idx].sourceMode.position.y != 0)
				{
					ret = true;
				}
			}
		}
	}
	return ret;
}

void DisMon::ReEnable()
{
	if (pSavedPathInfo != NULL)
	{
		// restore previous path
		LONG err;
		err = SetDisplayConfig(savedPathArraySize, pSavedPathInfo,
			savedModeArraySize, pSavedModeInfo,
			SDC_APPLY | SDC_ALLOW_CHANGES | SDC_USE_SUPPLIED_DISPLAY_CONFIG);
		if (err != ERROR_SUCCESS)
		{
			ThrowError("SetDisplayConfig Error", err);
		}
	}
}

bool DisMon::MonitorsDisabled()
{
	return monitorsDisabled;
}

void DisMon::ThrowError(std::string s, LONG err)
{
	//throw gcnew ApplicationException(msg);
}

void DisMon::FreeSavedInfo()
{
	delete pSavedPathInfo;
	delete pSavedModeInfo;
	pSavedPathInfo = NULL;
	pSavedModeInfo = NULL;
}

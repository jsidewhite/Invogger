// invologgerDll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#include <iostream>
#include <string>
#include <sstream>
#include <list>
#include <mutex>

#include <windows.h>
#include <strsafe.h>
#include "invologgerDll.h"


using namespace std;


std::mutex g_i_mutex;  // protects g_i
//std::vector<int> keyBacklog;
std::list<int> keyBacklog;

HHOOK theHook;

void ShowGetLastError()
{
	// Retrieve the system error message for the last-error code

	wchar_t lpMsgBuf[100] = {'\0'};
	//lpMsgBuf = 
	LPVOID lpDisplayBuf;
	DWORD dw = GetLastError();

	FormatMessageW(
		FORMAT_MESSAGE_FROM_SYSTEM |
		FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL,
		dw,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		lpMsgBuf,
		100, NULL);

	// Display the error message and exit the process

	//lpDisplayBuf = (LPVOID)LocalAlloc(LMEM_ZEROINIT, (lstrlen((LPCTSTR)lpMsgBuf) + 40) * sizeof(TCHAR));
	//StringCchPrintf((LPTSTR)lpDisplayBuf, LocalSize(lpDisplayBuf) / sizeof(TCHAR), TEXT("Failed with error %d: %s"), dw, lpMsgBuf);

	wcout << L"Error unhooking: " << lpMsgBuf << endl;

	//delete(lpMsgBuf);
	//LocalFree(lpDisplayBuf);
}

LRESULT CALLBACK theHookProc(int code, WPARAM wParam, LPARAM lParam)
{
	std::lock_guard<std::mutex> lock(g_i_mutex);
	//throw
	//HandleHotkeySequencePress(VK_F2);

	KBDLLHOOKSTRUCT kbdStruct;

	//cout << "keyCode wParam: " << wParam << endl;


	if (code >= 0)
	{
		// the action is valid: HC_ACTION.
		if (wParam == WM_KEYDOWN)
		{
			// lParam is the pointer to the struct containing the data needed, so cast and assign it to kdbStruct.
			kbdStruct = *((KBDLLHOOKSTRUCT*)lParam);

			keyBacklog.push_back((int)kbdStruct.vkCode);

			// save to file
			cout << "keyCode: " << kbdStruct.vkCode  << endl;


			/*
			cout << "about to unhook: " << endl;
			int uh = Unhook();
			cout << "just idd the unhook: " << endl;
			cout << "unhooking: " << uh << endl;
			*/
		}
	}
	else
	{
		//return CallNextHookEx(theHook, code, wParam, lParam);
	}

	return CallNextHookEx(theHook, code, wParam, lParam);

	//return CallNextHookEx(theHook, code, wParam, lParam);
	/*

	UINT keyUint = (UINT)wParam;

	int theKey = (int)keyUint;
	keyBacklog.push_back(theKey);

	cout << "keyHookProc: " << theKey << endl;
	//catch_and_show(NULL, [&]() {
		//throw my_exception(std::to_wstring(key));
		//HandleHotkeySequencePress(VK_F2);
	//});

	if (keyBacklog.size() > 0)
	{
		//int key = keyBacklog.front();
		//keyBacklog.pop_front();
		//cout << "key: " << key << endl;
		//return key;
	}

	//cout << "sdfsdf" << endl;

	//return LRESULT{};
	return CallNextHookEx(theHook, code, wParam, lParam);
	*/
}


// This is an example of an exported function.
extern "C" __declspec(dllexport) int GetKey(void)
{
	std::lock_guard<std::mutex> lock(g_i_mutex);

	// lock
	if (keyBacklog.size() > 0)
	{
		int key = keyBacklog.front();
		keyBacklog.pop_front();
		//keyBacklog.pop_back();
		keyBacklog.empty();
		return key;
	}
	//unlock

	return 654;
}


// This is an example of an exported function.
extern "C" __declspec(dllexport) int SetHooks(void)
{
	HOOKPROC hookProc = theHookProc;
	HINSTANCE hmod = g_hModule;
	//DWORD dwThreadId = GetCurrentThreadId();
	DWORD dwThreadId = 0;


	theHook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hmod, dwThreadId);
	if (NULL == theHook)
	{
		HRESULT hr = HRESULT_FROM_WIN32(GetLastError());
		//DWORD lastError = GetLastError();
		std::wstringstream ss;
		ss << "Couldn't register the special Windows hook: 0x" << std::hex << hr << std::endl;
		//return ss.str();

		//std::wstring msg = L"Couldn't register the special Windows hook: " + std::to_wstring(hr);

		//ss.str()
		//throw my_exception(ss.str());
		//throw my_exception(L"Couldn't register the special Windows hook");
		return 99;
	}

	return 0;
}

// This is an example of an exported function.
extern "C" __declspec(dllexport) int Unhook(void)
{
	if (!UnhookWindowsHookEx(theHook))
	{
		ShowGetLastError();
		//DWORD err = HRESULT_FROM_WIN32(GetLastError());
		//cout << "Error unhooking: " << std::hex << err << endl;
		return 112;
	}

	return 0;
}





// This is an example of an exported variable
INVOLOGGERDLL_API int ninvologgerDll=0;

// This is an example of an exported function.
INVOLOGGERDLL_API int fninvologgerDll(void)
{
	return 42;
}
// This is an example of an exported function.
extern "C" __declspec(dllexport) int sdf(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see invologgerDll.h for the class definition
CinvologgerDll::CinvologgerDll()
{
    return;
}

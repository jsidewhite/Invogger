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

std::list<int> keyBacklog;
std::mutex g_keyBackLog_mutex;

HHOOK hookHandle_keyPress;
HHOOK hookHandle_mouseClick;

void WriteGetLastError()
{
	wchar_t buffer[100] = {'\0'};
	DWORD dw = GetLastError();

	FormatMessageW(
		FORMAT_MESSAGE_FROM_SYSTEM |
		FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL,
		dw,
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		buffer,
		100, NULL);

	wcout << L"Error message: " << buffer << endl;
}

LRESULT CALLBACK theHookProc_key(int code, WPARAM wParam, LPARAM lParam)
{
	std::lock_guard<std::mutex> lock(g_keyBackLog_mutex);

	KBDLLHOOKSTRUCT * keyboardInput;
	if (code >= 0)
	{
		if (wParam == WM_KEYDOWN)
		{
			keyboardInput = reinterpret_cast<KBDLLHOOKSTRUCT*>(lParam);
			auto keyCode = static_cast<int>(keyboardInput->vkCode);
			keyBacklog.push_back(keyCode);
			cout << "keyCode: " << keyCode << endl;
		}
	}

	return CallNextHookEx(hookHandle_keyPress, code, wParam, lParam);
}

LRESULT CALLBACK theHookProc_mouse(int code, WPARAM wParam, LPARAM lParam)
{
	MOUSEHOOKSTRUCT * mouseInput = reinterpret_cast<MOUSEHOOKSTRUCT *>(lParam);
	if (nullptr != mouseInput)
	{
		std::lock_guard<std::mutex> lock(g_keyBackLog_mutex);

		if (wParam == WM_LBUTTONDOWN)
		{
			keyBacklog.push_back(-201);
			cout << "left mouse";
			cout << ": " << hex << wParam << endl;
		}
		else if (wParam == WM_RBUTTONDOWN)
		{
			keyBacklog.push_back(-204);
			cout << "right mouse";
			cout << ": " << hex << wParam << endl;
		}
		else if (wParam == WM_MBUTTONDOWN)
		{
			keyBacklog.push_back(-207);
			cout << "middle mouse";
			cout << ": " << hex << wParam << endl;

			cout << "keyBacklog size : " << keyBacklog.size() << endl;
		}
		
		//cout << "Mouse position X = " << mouseInput->pt.x << " Mouse Position Y = " << mouseInput->pt.y << endl;
	}
	return CallNextHookEx(hookHandle_mouseClick, code, wParam, lParam);
}

extern "C" __declspec(dllexport) int GetKey(void)
{
	std::lock_guard<std::mutex> lock(g_keyBackLog_mutex);

	// lock
	if (keyBacklog.size() > 0)
	{
		int key = keyBacklog.front();
		keyBacklog.pop_front();
		//keyBacklog.pop_back();
		//keyBacklog.empty();
		keyBacklog.clear();
		return key;
	}
	//unlock

	return 654;
}

extern "C" __declspec(dllexport) int SetHooks(void)
{
	keyBacklog.clear();

	HINSTANCE hmod = g_hModule;
	//DWORD dwThreadId = GetCurrentThreadId();
	DWORD dwThreadId = 0;


	hookHandle_keyPress = SetWindowsHookEx(WH_KEYBOARD_LL, &theHookProc_key, hmod, dwThreadId);
	if (NULL == hookHandle_keyPress)
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

#if 1
	hookHandle_mouseClick = SetWindowsHookEx(WH_MOUSE_LL,& theHookProc_mouse, hmod, dwThreadId);
	if (NULL == hookHandle_mouseClick)
	{
		HRESULT hr = HRESULT_FROM_WIN32(GetLastError());
		std::wstringstream ss;
		ss << "Couldn't register the special Windows hook: 0x" << std::hex << hr << std::endl;
		return 98;
	}
#endif

	return 0;
}

// This is an example of an exported function.
extern "C" __declspec(dllexport) int Unhook(void)
{
	keyBacklog.clear();

	if (!UnhookWindowsHookEx(hookHandle_keyPress))
	{
		cout << "Error unhooking:";
		WriteGetLastError();
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

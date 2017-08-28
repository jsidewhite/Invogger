// invologgerDllConsumer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

#include "windows.h"



#include "W:\TASKS\2017-08-26_invogger\invologgerDll\invologgerDll\invologgerDll.h"

using namespace std;

void doit()
{
	//while (true)
	{
		int key = GetKey();
		if (65 == key)
		{
			//break;
			return;
		}
		cout << "key: " << key << endl;
		Sleep(1000);
	}
}

int main()
{
	ShowWindow(FindWindowA("ConsoleWindowClass", NULL), 1); // visible window

	if (0 != SetHooks())
	{
		return 1001;
	}

	//Unhook();

	doit();

	//Sleep(2000);
	// loop to keep the console application running.
	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0))
	{
		/*int key = GetKey();
		while (0 != key)
		{
			cout << "key: " << key << endl;
		}
		*/
		doit();
	}

    return 0;
}


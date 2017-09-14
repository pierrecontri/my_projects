// TestConsoleWin32.cpp : définit le point d'entrée pour l'application console.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	char tty[500];
	scanf_s("%s", &tty);
	printf("%s", tty);
	return 0;
}


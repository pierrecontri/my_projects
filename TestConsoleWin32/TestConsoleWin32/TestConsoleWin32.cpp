// TestConsoleWin32.cpp�: d�finit le point d'entr�e pour l'application console.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	char tty[500];
	scanf_s("%s", &tty);
	printf("%s", tty);
	return 0;
}


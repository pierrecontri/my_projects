// Il s'agit du fichier DLL principal.

#include "stdafx.h"

#include "MyDll.h"

char * MyDll::Commun::SayHello()
{
	return (char *) "Hello World !";
};
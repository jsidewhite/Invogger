// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the INVOLOGGERDLL_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// INVOLOGGERDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef INVOLOGGERDLL_EXPORTS
#define INVOLOGGERDLL_API __declspec(dllexport)
#else
#define INVOLOGGERDLL_API __declspec(dllimport)
#endif

// This class is exported from the invologgerDll.dll
class INVOLOGGERDLL_API CinvologgerDll {
public:
	CinvologgerDll(void);
	// TODO: add your methods here.
};

extern INVOLOGGERDLL_API int ninvologgerDll;

INVOLOGGERDLL_API int fninvologgerDll(void);
extern "C" __declspec(dllexport)  int sdf(void);

extern "C" __declspec(dllexport) int GetKey(void);
extern "C" __declspec(dllexport) int SetHooks(void);
extern "C" __declspec(dllexport) int Unhook(void);


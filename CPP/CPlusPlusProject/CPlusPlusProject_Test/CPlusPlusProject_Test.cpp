#include "pch.h"
#include "CppUnitTest.h"
#include "../CPlusPlusProject/CPlusPlusProject.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

/// <summary>
/// Visual Studio Untit-Testing (create Tests, run tests):
/// https://docs.microsoft.com/en-us/visualstudio/test/writing-unit-tests-for-c-cpp?view=vs-2022
/// 
/// Code-Coverage (Only Visual Studio Enterprise Edition):
/// https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2022&tabs=csharp
/// 
/// </summary>

namespace CPlusPlusProjectTest
{
	
	TEST_CLASS(CPlusPlusProjectTest)
	{
	public:
		/// <summary>
		/// Vorab-Info:
		/// Um die Testcases auszufuehren muessen das zu testende Projekt und aber auch die Test.dll (aktuelles Projekt) gebildet sein.
		/// </summary>
		

		TEST_METHOD(T001_SuccessReturn)
		{
			// Function returns true and the expected Result is also True
			Assert::IsTrue(SuccessReturn(), std::wstring(L"Funktionsrueckgabetyp ist falsch!").c_str());
		}

		TEST_METHOD(T002_SuccessReturnFail)
		{
			// Function returns true and the expected Result is False
			Assert::IsFalse(SuccessReturn(), std::wstring(L"Unerwarteter Wert!").c_str());
		}


		TEST_METHOD(T003_CorrectAddition)
		{
			int nExpected = 5;
			// Parameter AreEqual:
			// 1) Expected
			// 2) Actual (Function call)
			// 3) Message
			Assert::AreEqual(nExpected, Calculate(2, 3, true), std::wstring(L"Rechnung wird falsch berechnet!").c_str());
		}

		TEST_METHOD(T004_CorrectAdditionFail)
		{
			int nExpected = 7;
			// Parameter AreEqual:
			// 1) Expected
			// 2) Actual (Function call)
			// 3) Message
			Assert::AreEqual(nExpected, Calculate(2, 3, true), std::wstring(L"Falsch erwartetes Ergebnis\nErwartet: 7").c_str());
		}

		TEST_METHOD(T005_IncorrectAddition)
		{
			int nNotExpected = 17;
			Assert::AreNotEqual(nNotExpected, Calculate(12, 3, true), std::wstring(L"Darf nicht \"17\" sein!").c_str());
		}

		TEST_METHOD(T005_IncorrectAdditionFail)
		{
			int nNotExpected = 17;
			Assert::AreNotEqual(nNotExpected, Calculate(14, 3, true), std::wstring(L"Darf nicht \"17\" sein!").c_str());
		}
	};
}

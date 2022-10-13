// CPlusPlusProject.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

int main()
{

    printf("Hallo, das ist ein output!\n");
    std::cout << "Hello World!\n";
}

/// <summary>
/// Every-Time True
/// </summary>
/// <returns>TRUE -> hardcoded</returns>
bool SuccessReturn()
{
    bool isTrue = true;

    return isTrue;
}

/// <summary>
/// Easy Addition
/// </summary>
/// <param name="nFirstAddend">First Addend</param>
/// <param name="nSecondAddend">Second Addend</param>
/// <param name=""></param>
/// <returns>The sum of both addends</returns>
int Calculate(int nFirstAddend, int nSecondAddend, bool bCalculate)
{
    int nSum = 0;
    if (bCalculate)
    {
        nSum = nFirstAddend + nSecondAddend;
    }
    else
    {
        nSum = 12;
    }
    
    return nSum;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file

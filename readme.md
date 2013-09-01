# Code testing

I used Visual Studio 2012 and C# to implement following questions. For unit tests I used [nUnit](http://www.nunit.org/) framework (to launch nUnit tests in Visual Studio you need to have [nUnit test adapter](http://nunit.org/index.php?p=vsTestAdapter&r=2.6.2) or you can use [ReSharper](http://www.jetbrains.com/resharper/) to launch them).

## Question 1

Given a string, write a routine that converts the string to a long, without using the built in functions that would do this. Describe what (if any) limitations the code has. For example:

    long stringToLong(String s)
    {
        /* code goes here to convert a string to a long */
    }
    
    void test()
    { 
        long i = stringToLong("123");
        if (i == 123)
            // success
        else
            // failure
    }
    
### Answer

Function `stringToLong` is implemented in file [Parser.cs](sources/ZTest.Library/Parser.cs). Unit tests are implemented in [ParserSuites.cs](sources/ZTest.Suites/ParserSuites.cs). Current implementation has several limitations:

  1. Function cannot parse long values represented in HEX format, like `0x123` (unit test `StringToLong_HexValue_ThrowsFormatException`).
  2. Function cannot parse long values in strings, which contain group separators, like `1,000,000` (unit test `StringToLong_CultureSpecificInputValueWithGroupSeparators_ThrowsFormatException`(. 
  

## Question 2


Implement insert and delete in a tri-nary tree. A tri-nary tree is much like a binary tree but with three child nodes for each parent instead of two -- with the left node being values less than the parent, the right node values greater than the parent, and the middle nodes values equal to the parent.

For example, suppose I added the following nodes to the tree in this order: `5, 4, 9, 5, 7, 2, 2`. 

The resulting tree would look like this:

         5
       / | \
      4  5  9
     /     /
    2    7
    |
    2
    
### Answer

Tri-nary tree structure is implemented in file [TrinaryTree.cs](sources/ZTest.Library/TrinaryTree.cs). Current implementation allows to use any [value types](http://msdn.microsoft.com/en-us/library/s1ax56ch.aspx) as a structure values. Because all values are `value types` we can use one trick to store middle-node. On middle node you can store only values which are equal to parent node. That means that by middle-node direction we can have only values which are equal to parent node, so we do not need to store real nodes, we can just count what is the depth of middle nodes. 

To store any C# types in current structure we will need to change couple places: a) add validations for `null` values,  b) change how we store middle node, instead of simple counter we can use Stack for that. 

Unit tests for `TrinaryTree` are implemented in [TrinaryTreeSuites.cs](sources/ZTest.Suites/TrinaryTreeSuites.cs). This class contains only two methods, but in fact it contains 13 unit tests. One of unit tests `Remove_TestCase` expects test cases, which we provide with source `RemoveTestCaseSource` (see documentation for [TestCaseSourceAttribute](http://www.nunit.org/index.php?p=testCaseSource&r=2.6.2))
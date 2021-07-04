using Xunit;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Week_16_Programming_Assignment;

public class TestClass
{   
    [Fact]
    public void Self_Test()
    {
        Assert.Equal(0,0);
    }

    
    [Fact]
    public void Scan_Test()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        // (TruthValue, TruthValue)[] myAnswer = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");
        (TruthValue, TruthValue)[] myAnswer = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");

        // Assert
        Assert.Equal((2,5), ((myAnswer[0].Item1._ID, myAnswer[0].Item2._ID)));
        Assert.Equal((1,87), ((myAnswer[1].Item1._ID, myAnswer[1].Item2._ID)));
        Assert.Equal((-5,2), ((myAnswer[2].Item1._ID, myAnswer[2].Item2._ID)));

    }

    
    [Fact]
    public void Partial_Test_Real_Case_01()
    {
         // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
       // (TruthValue, TruthValue)[] myAnswer = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");
        (TruthValue, TruthValue)[] myAnswer = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");

        // Assert
        Assert.Equal((-16808, 75250),  ((myAnswer[0].Item1._ID, myAnswer[0].Item2._ID)));
        Assert.Equal((22891, 24500), ((myAnswer[51778].Item1._ID, myAnswer[51778].Item2._ID)));
        Assert.Equal((52249, 60667), ((myAnswer[99999].Item1._ID, myAnswer[99999].Item2._ID)));  
        Assert.Equal(100000, myAnswer.Length);
    }


    [Fact]
    public void Partial_Test_Case_PositiveList_01()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        SAT_2Solver sat_2Solver = new SAT_2Solver();

     //   (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");
        (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");


        // Act
        sat_2Solver.PapaDimitrious(truthTable);

        // Assert
        Assert.Equal(2, sat_2Solver._positiveID_Dict[2]._ID);     
        Assert.Equal(5, sat_2Solver._positiveID_Dict[5]._ID);
        Assert.Equal(1, sat_2Solver._positiveID_Dict[1]._ID);
        Assert.Equal(87, sat_2Solver._positiveID_Dict[87]._ID);
        Assert.Equal(2, sat_2Solver._positiveID_Dict[2]._ID);
        
        Assert.Equal(4, sat_2Solver._positiveID_Dict.Count);
    }

    
    [Fact]
    public void Partial_Test_Case_NegativeList_01()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        SAT_2Solver sat_2Solver = new SAT_2Solver();

      //  (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");
        (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");


        // Act
        sat_2Solver.PapaDimitrious(truthTable);
        int fix = sat_2Solver._negativeID_Dict.Count;

        // Assert
        Assert.Equal(-5, sat_2Solver._negativeID_Dict[-5]._ID);             
        Assert.Equal(1, fix);
    }

    [Fact]
    public void PapaDimitrious_Test_Case_01_True()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        SAT_2Solver sat_2Solver = new SAT_2Solver();    
        //  (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_02_True.txt");
        (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_02_True.txt");
        
        // Act
        bool myAnswer = sat_2Solver.PapaDimitrious(truthTable);

        // Assert
        Assert.True(myAnswer);
    }
    

        [Fact]
        public void PapaDimitrious_Test_Case_02_False()
        {
            // Arrange
            FileReader fileReader = new FileReader();
            SAT_2Solver sat_2Solver = new SAT_2Solver();    
            //  (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_03_False.txt");
            (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_03_False.txt");
            
            // Act
            bool myAnswer = sat_2Solver.PapaDimitrious(truthTable);

            // Assert
            Assert.False(myAnswer);
        }
}